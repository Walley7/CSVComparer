using CSACore.Core;
using CSACore.CSV;
using CSACore.Profiling;
using CSVComparer.CSVComparison;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CSVComparer.CSV_Comparison {

    class CSVCompare {
        //================================================================================
        private BackgroundWorker                mBackgroundWorker;

        private string                          mLeftCSVFilePath;
        private string                          mRightCSVFilePath;

        private List<CSVMapping>                mMappings;

        private List<CSVCompareMergeIssue>      mIssues = new List<CSVCompareMergeIssue>();

        //--------------------------------------------------------------------------------
        public event ProgressEventDelegate      ProgressEvent;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CSVCompare(BackgroundWorker backgroundWorker, string leftCSVFilePath, string rightCSVFilePath, List<CSVMapping> mappings) {
            mBackgroundWorker = backgroundWorker;
            mLeftCSVFilePath = leftCSVFilePath;
            mRightCSVFilePath = rightCSVFilePath;
            mMappings = mappings;
        }


        // COMPARE ================================================================================
        //--------------------------------------------------------------------------------
        public bool Compare() {
            // CSVs
            CSVData left = new CSVData(mLeftCSVFilePath);
            CSVData right = new CSVData(mRightCSVFilePath);

            // Compare
            bool outcome = Compare(left, right);

            // Close
            left.Dispose();
            right.Dispose();

            // Return
            return outcome;
        }

        //--------------------------------------------------------------------------------
        private bool Compare(CSVData left, CSVData right) {
            // Mappings
            CSVMapping[] mappings = (from m in mMappings where left.Headers.Contains(m.LeftColumnName.ToLower()) && right.Headers.Contains(m.RightColumnName.ToLower()) select m).ToArray();
            CSVMapping[] keyMappings = (from k in mappings where k.Key select k).ToArray();

            List<CSVMapping> leftKeyMappings = new List<CSVMapping>();
            List<CSVMapping> rightKeyMappings = new List<CSVMapping>();
            foreach (CSVMapping k in keyMappings) {
                leftKeyMappings.Add(new CSVMapping(k, k.LeftColumnName, k.LeftColumnName));
                rightKeyMappings.Add(new CSVMapping(k, k.RightColumnName, k.RightColumnName));
            }

            // Key columns
            string[] leftKeyColumns = (from m in leftKeyMappings select m.LeftColumnName).ToArray();
            string[] rightKeyColumns = (from m in rightKeyMappings select m.RightColumnName).ToArray();

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
            List<CSVDataRow> comparedRightRows = new List<CSVDataRow>();

            // Left rows
            foreach (CSVDataRow leftRow in left.Rows) {
                // Cancellation
                if (mBackgroundWorker.CancellationPending)
                    return false;

                // Progress
                if (stopwatch.ElapsedMilliseconds - lastProgressTime >= 10) {
                    lastProgressTime = stopwatch.ElapsedMilliseconds;
                    timeRemaining = c > 0 ? new TimeSpan((stopwatch.ElapsedTicks / c) * (rowCount - c)) : TimeSpan.MaxValue;
                    ProgressEvent?.Invoke(0, c, rowCount, "Comparing CSVs...", c, rowCount, timeRemaining);
                }

                // Counting
                ++c;
                ++r;

                // Linking issues
                CSVCompareMergeIssue linkingIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.LINKING, r, null, leftRow, null, mappings);

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

                // Stop
                //if (linkingIssue.HasDetails)
                //    continue;

                // Matches
                List<CSVDataRow> matches = CSVMapping.MatchingRightRows(left, right, leftRow, keyMappings);
                if (matches.Count == 0)
                    linkingIssue.AddDetails(keyMappings.Count() > 0 ? "No matches found in the other file for this row's key values." : "Nothing to match with - this line is past the end line of the other file.", leftKeyColumns);
                else if (matches.Count > 1)
                    linkingIssue.AddDetails("Unique match could not be made - more than one row in the other file had matching key values.", leftKeyColumns);

                // Add linking issue
                if (linkingIssue.HasDetails) {
                    mIssues.Add(linkingIssue);
                    //continue;
                }

                // Compare matches
                foreach (CSVDataRow rightRow in matches) {
                    // Exclude right row
                    comparedRightRows.Add(rightRow);
                    ++c; // This row will be ignored when going through the right side rows

                    // Issue
                    CSVCompareMergeIssue unequalIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.UNEQUAL, r, rightRow.RowIndex + 1, leftRow, rightRow, mappings);

                    // Mappings
                    foreach (CSVMapping m in mappings) {
                        if (m.Include && !m.RowsEqual(leftRow, rightRow)) {
                            unequalIssue.AddDetails($"[{m.LeftColumnName}] and [{m.RightColumnName}] do not match ('{(leftRow.Value(m.LeftColumnName) ?? "").Replace("\n", "").Replace("'", "`")}', '{(rightRow.Value(m.RightColumnName) ?? "").Replace("\n", "").Replace("'", "`")}').",
                                                    new string[] { m.LeftColumnName }, new string[] { m.RightColumnName });
                        }
                    }

                    // Add issue
                    if (unequalIssue.HasDetails)
                        mIssues.Add(unequalIssue);

                }
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

                // Already compared rows
                if (comparedRightRows.Contains(rightRow))
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
                CSVCompareMergeIssue linkingIssue = new CSVCompareMergeIssue(CSVCompareMergeIssue.EType.LINKING, null, r, null, rightRow, mappings);

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
                    mIssues.Add(linkingIssue);
                    //continue;
                }
            }

            // Progress
            ProgressEvent?.Invoke(0, c, rowCount, "Comparing CSVs...", c, rowCount, TimeSpan.Zero);

            // Return
            return true;
        }


        // ISSUES ================================================================================
        //--------------------------------------------------------------------------------
        public List<CSVCompareMergeIssue> Issues { get { return mIssues; } }


        //================================================================================
        //********************************************************************************
        public delegate void ProgressEventDelegate(int percent, int? progress, int? progressMaximum, string stage, int row, int rowCount, TimeSpan timeRemaining);
    }

}
