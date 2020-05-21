using CSACore.Core;
using CSACore.CSV;
using CSVComparer.CSVComparison;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CSVComparer.CSV_Comparison {

    class CSVMerge {
        //================================================================================
        public enum EColumnNamingMode {
            LEFT,
            RIGHT,
            COMBINE
        }


        //================================================================================
        private BackgroundWorker                mBackgroundWorker;

        private string                          mLeftCSVFilePath;
        private string                          mRightCSVFilePath;

        private List<CSVMapping>                mMappings;

        private string                          mOutputPath;

        private EColumnNamingMode               mColumnNamingMode = EColumnNamingMode.COMBINE;

        private List<CSVCompareMergeIssue>           mIssues = new List<CSVCompareMergeIssue>();
        
        //--------------------------------------------------------------------------------
        public event ProgressEventDelegate      ProgressEvent;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CSVMerge(BackgroundWorker backgroundWorker, string leftCSVFilePath, string rightCSVFilePath, List<CSVMapping> mappings, string outputPath) {
            mBackgroundWorker = backgroundWorker;
            mLeftCSVFilePath = leftCSVFilePath;
            mRightCSVFilePath = rightCSVFilePath;
            mMappings = mappings;
            mOutputPath = outputPath;
        }


        // MERGE ================================================================================
        //--------------------------------------------------------------------------------
        public bool Merge() {
            // CSVs
            CSVData left = new CSVData(mLeftCSVFilePath);
            CSVData right = new CSVData(mRightCSVFilePath);

            // Merge
            CSVWriter mergedCSV = new CSVWriter(mOutputPath);
            bool outcome = false;
            try {
                outcome = Merge(mergedCSV, left, right);
            }
            catch (Exception e) { throw e; }
            finally {
                mergedCSV.Close();
            }

            // Close
            left.Dispose();
            right.Dispose();

            // Return
            return outcome;
        }

        //--------------------------------------------------------------------------------
        private bool Merge(CSVWriter mergedCSV, CSVData left, CSVData right) {
            // Mappings
            CSVMapping[] mappings = (from m in mMappings where left.Headers.Contains(m.LeftColumnName.ToLower()) && right.Headers.Contains(m.RightColumnName.ToLower()) select m).ToArray();
            CSVMapping[] keyMappings = (from k in mappings where k.Key select k).ToArray();
            CSVMapping[] includedMappings = (from m in mappings where m.Include select m).ToArray();

            List<CSVMapping> leftKeyMappings = new List<CSVMapping>();
            List<CSVMapping> rightKeyMappings = new List<CSVMapping>();
            foreach (CSVMapping k in keyMappings) {
                leftKeyMappings.Add(new CSVMapping(k, k.LeftColumnName, k.LeftColumnName));
                rightKeyMappings.Add(new CSVMapping(k, k.RightColumnName, k.RightColumnName));
            }

            // Key columns
            string[] leftKeyColumns = (from m in leftKeyMappings select m.LeftColumnName).ToArray();
            string[] rightKeyColumns = (from m in rightKeyMappings select m.RightColumnName).ToArray();

            // Headers
            List<string> leftMergedHeaders = new List<string>();
            List<string> rightMergedHeaders = new List<string>();

            // Headers - left
            for (int i = 0; i < left.CasedHeaders.Count; ++i) {
                string header = left.CasedHeaders[i];
                leftMergedHeaders.Add(header);

                CSVMapping[] headerMappings = (from m in includedMappings where m.LeftColumnName.Equals(header) select m).ToArray();
                if (headerMappings.Count() > 0) {
                    rightMergedHeaders.Add(headerMappings[0].RightColumnName);

                    switch (mColumnNamingMode) {
                        case EColumnNamingMode.LEFT: mergedCSV.WriteHeader(header); break;
                        case EColumnNamingMode.RIGHT: mergedCSV.WriteHeader(headerMappings[0].RightColumnName); break;
                        case EColumnNamingMode.COMBINE: mergedCSV.WriteHeader((header + " " + headerMappings[0].RightColumnName).Trim()); break;
                    }
                }
                else {
                    rightMergedHeaders.Add(null);
                    mergedCSV.WriteHeader(header);
                }
            }

            // Headers - right
            for (int i = 0; i < right.CasedHeaders.Count; ++i) {
                string header = right.CasedHeaders[i];
                CSVMapping[] headerMappings = (from m in includedMappings where m.RightColumnName.Equals(header) select m).ToArray();
                if (headerMappings.Count() == 0) {
                    leftMergedHeaders.Add(null);
                    rightMergedHeaders.Add(header);
                    mergedCSV.WriteHeader(header);
                }
            }

            // Headers - end row
            mergedCSV.WriteEndRow();

            // Progress
            int rowCount = left.Rows.Count + right.Rows.Count;
            int c = 0;
            int r = 0;
            TimeSpan timeRemaining = TimeSpan.MaxValue;

            // Stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();
            long lastProgressTime = stopwatch.ElapsedMilliseconds;
            long lastProgressTick = stopwatch.ElapsedTicks;

            // Auto WIP row tracking
            List<CSVDataRow> mergedRightRows = new List<CSVDataRow>();

            // Left rows
            foreach (CSVDataRow leftRow in left.Rows) {
                // Cancellation
                if (mBackgroundWorker.CancellationPending)
                    return false;

                // Progress
                if (stopwatch.ElapsedMilliseconds - lastProgressTime >= 10) {
                    lastProgressTime = stopwatch.ElapsedMilliseconds;
                    timeRemaining = c > 0 ? new TimeSpan((stopwatch.ElapsedTicks / c) * (rowCount - c)) : TimeSpan.MaxValue;
                    ProgressEvent?.Invoke(0, c, rowCount, "Merging CSVs...", c, rowCount, timeRemaining);
                }

                // Counting
                ++c;
                ++r;

                // Linking issues
                CSVCompareMergeIssue linkingIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.MERGE_LINKING, r, null, leftRow, null, mappings);

                // Keys
                foreach (CSVMapping k in keyMappings) {
                    if (leftRow.Value(k.LeftColumnName) == null)
                        linkingIssue.AddDetails($"Key value [{k.LeftColumnName}] is empty.", new string[] { k.LeftColumnName });
                }

                // Key collisions
                if (keyMappings.Count() > 0) {
                    List<CSVDataRow> keyCollisions = CSVMapping.MatchingRightRows(left, left, leftRow, leftKeyMappings);
                    if (keyCollisions.Count > 0)
                        linkingIssue.AddDetails($"Key values not unique - the following row numbers have the same keys: {string.Join(", ", from k in keyCollisions select k.RowIndex + 2)}", leftKeyColumns);
                }

                // Matches
                List<CSVDataRow> matches = CSVMapping.MatchingRightRows(left, right, leftRow, keyMappings);
                if (matches.Count == 0)
                    linkingIssue.AddDetails(keyMappings.Count() > 0 ? "No matches found in the other file for this row's key values." : "Nothing to match with - this line is past the end line of the other file.", leftKeyColumns);
                else if (matches.Count > 1)
                    linkingIssue.AddDetails("Unique match could not be made - more than one row in the other file had matching key values.", leftKeyColumns);

                // Add linking issue
                if (linkingIssue.HasDetails) {
                    linkingIssue.AddDetailsHeading("Row added to merge output as is. This was due to:");
                    mIssues.Add(linkingIssue);
                }

                // Write
                if (linkingIssue.HasDetails)
                    WriteRow(mergedCSV, leftMergedHeaders, leftRow);
                else {
                    // Right row
                    CSVDataRow rightRow = matches[0];
                    
                    // Exclude right row
                    mergedRightRows.Add(rightRow);
                    ++c; // This row will be ignored when going through the right side rows

                    // Merge
                    WriteMergedRow(mergedCSV, leftMergedHeaders, rightMergedHeaders, leftRow, rightRow, includedMappings);
                }

                // End row
                mergedCSV.WriteEndRow();
            }

            // Progress
            r = 0;  

            // Right rows
            foreach (CSVDataRow rightRow in right.Rows) {
                // Cancellation
                if (mBackgroundWorker.CancellationPending)
                    return false;
                
                // Counting
                ++r;

                // Already merged rows
                if (mergedRightRows.Contains(rightRow))
                    continue;

                // Progress
                if (stopwatch.ElapsedMilliseconds - lastProgressTime >= 10) {
                    lastProgressTime = stopwatch.ElapsedMilliseconds;
                    timeRemaining = c > 0 ? new TimeSpan((stopwatch.ElapsedTicks / c) * (rowCount - c)) : TimeSpan.MaxValue;
                    ProgressEvent?.Invoke(0, c, rowCount, "Comparing CSVs...", c, rowCount, timeRemaining);
                }

                // Counting
                ++c;

                // Linking issues
                CSVCompareMergeIssue linkingIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.MERGE_LINKING, null, r, null, rightRow, mappings);

                // Key collisions
                if (keyMappings.Count() > 0) {
                    List<CSVDataRow> keyCollisions = CSVMapping.MatchingLeftRows(right, right, rightRow, rightKeyMappings);
                    if (keyCollisions.Count > 0)
                        linkingIssue.AddDetails($"Key values not unique - the following row numbers have the same keys: {string.Join(", ", from k in keyCollisions select k.RowIndex + 2)}", null, rightKeyColumns);
                }

                // Matches
                List<CSVDataRow> matches = CSVMapping.MatchingLeftRows(left, right, rightRow, keyMappings);
                if (matches.Count == 0)
                    linkingIssue.AddDetails(keyMappings.Count() > 0 ? "No matches found in the other file for this row's key values." : "Nothing to match with - this line is past the end line of the other file.", null, rightKeyColumns);

                // Add linking issue
                if (linkingIssue.HasDetails) {
                    linkingIssue.AddDetailsHeading("Row added to merge output as is. This was due to:");
                    mIssues.Add(linkingIssue);
                }

                // Write
                WriteRow(mergedCSV, rightMergedHeaders, rightRow);
                mergedCSV.WriteEndRow();
            }

            // Progress
            ProgressEvent?.Invoke(0, c, rowCount, "Merging CSVs...", c, rowCount, TimeSpan.Zero);

            // Return
            return true;
        }
        
        //--------------------------------------------------------------------------------
        private void WriteRow(CSVWriter mergedCSV, List<string> headers, CSVDataRow row) {
            for (int i = 0; i < headers.Count; ++i) {
                mergedCSV.WriteValue(headers[i] != null ? row.Value(headers[i]) : "");
            }
        }
        
        //--------------------------------------------------------------------------------
        private void WriteMergedRow(CSVWriter mergedCSV, List<string> leftHeaders, List<string> rightHeaders,
                                    CSVDataRow leftRow, CSVDataRow rightRow, CSVMapping[] mappings)
        {
            // Issues
            CSVCompareMergeIssue programIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.PROGRAM_ERROR, leftRow.RowIndex + 1, rightRow.RowIndex + 1, leftRow, rightRow, mappings);
            CSVCompareMergeIssue incompatibleDataIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.INCOMPATIBLE_DATA, leftRow.RowIndex + 1, rightRow.RowIndex + 1, leftRow, rightRow, mappings);

            // Merge row
            for (int i = 0; i < leftHeaders.Count; ++i) {
                if (leftHeaders[i] != null && rightHeaders[i] != null) {
                    // Mapping
                    CSVMapping[] mergeMappings = (from m in mappings where m.LeftColumnName.Equals(leftHeaders[i]) select m).ToArray();

                    // Merge
                    CSVMapping.EMergeResult result = mergeMappings[0].MergedValue(leftRow, rightRow, out string mergedValue);
                    mergedCSV.WriteValue(mergedValue);

                    // Result
                    switch (result) {
                        case CSVMapping.EMergeResult.INVALID_TYPE:          programIssue.AddDetails($"The type '{mergeMappings[0].Type}' is invalid. Please contact Common Sense Apps."); break;
                        case CSVMapping.EMergeResult.INVALID_MERGE_RULE:    programIssue.AddDetails($"The type '{mergeMappings[0].Type}' is not compatible with the merge rule '{mergeMappings[0].MergeRule}'."); break;
                        case CSVMapping.EMergeResult.INVALID_NUMBERS:       incompatibleDataIssue.AddDetails($"One or both of '{leftHeaders[i]}' and '{rightHeaders[i]}' are not valid numbers ('{leftRow.Value(leftHeaders[i])}', '{rightRow.Value(rightHeaders[i])}')."); break;
                        case CSVMapping.EMergeResult.INVALID_DATES:         incompatibleDataIssue.AddDetails($"One or both of '{leftHeaders[i]}' and '{rightHeaders[i]}' are not validly formatted dates ('{leftRow.Value(leftHeaders[i])}', '{rightRow.Value(rightHeaders[i])}')."); break;
                    }
                }
                else if (leftHeaders[i] != null)
                    mergedCSV.WriteValue(leftRow.Value(leftHeaders[i]));
                else
                    mergedCSV.WriteValue(rightRow.Value(rightHeaders[i]));
            }

            // Add issues
            if (programIssue.HasDetails)
                mIssues.Add(programIssue);
            if (incompatibleDataIssue.HasDetails)
                mIssues.Add(incompatibleDataIssue);
        }


        // ISSUES ================================================================================
        //--------------------------------------------------------------------------------
        public List<CSVCompareMergeIssue> Issues { get { return mIssues; } }


        //================================================================================
        //********************************************************************************
        public delegate void ProgressEventDelegate(int percent, int? progress, int? progressMaximum, string stage, int row, int rowCount, TimeSpan timeRemaining);
    }

}
