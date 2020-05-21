using CSACore.CSV;
using CSACore.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace CSVComparer.CSVComparison {

    public class CSVMapping {
        //================================================================================
        public enum EType {
            TEXT,
            NUMBER,
            PERCENTAGE,
            DATE
        }

        public enum EMergeRule {
            APPEND,
            APPEND_WITH_SPACE,
            AVERAGE,
            ADD,
            SUBTRACT_FROM_LEFT,
            SUBTRACT_FROM_RIGHT,
            DIFFERENCE,
            MULTIPLY,
            MINIMUM,
            MAXIMUM,
            LEFT,
            RIGHT
        }

        public enum EMergeResult {
            SUCCESS,
            INVALID_TYPE,
            INVALID_MERGE_RULE,
            INVALID_NUMBERS,
            INVALID_DATES
        }


        //================================================================================
        private bool                            mKey = false;
        private bool                            mInclude = true;

        private CSVColumn                       mLeftColumn;
        private CSVColumn                       mRightColumn;

        private EType                           mType = EType.TEXT;

        private EMergeRule                      mMergeRule = EMergeRule.APPEND;

        private bool                            mIgnoreCase = false;
        private bool                            mIgnoreSymbols = false;
        private string                          mAllowedSymbols = "-.,%";
        private bool                            mIgnoreWhitespace = false;

        private bool                            mRemoveCase = false;
        private string                          mPrefix = "";
        private string                          mSuffix = "";

        private int                             mDecimalPrecision = -1; // -1 = full precision, 0 = whole number match suffices, 1 = 1 decimal suffices, etc

        private List<ForcedMatching>            mForcedMatchings = new List<ForcedMatching>();
        
        private List<MatchLookupCache>          mMatchLookups = new List<MatchLookupCache>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public CSVMapping(bool key, bool include, CSVColumn leftColumn, CSVColumn rightColumn) {
            mKey = key;
            mInclude = include;
            mLeftColumn = new CSVColumn(leftColumn.Name);
            mRightColumn = new CSVColumn(rightColumn.Name);
        }

        //--------------------------------------------------------------------------------
        public CSVMapping(CSVColumn leftColumn, CSVColumn rightColumn) : this(false, true, leftColumn, rightColumn) { }

        //--------------------------------------------------------------------------------
        public CSVMapping(JToken m) {
            LoadJSON(m);
        }

        //--------------------------------------------------------------------------------
        public CSVMapping(CSVMapping mapping) {
            mKey = mapping.mKey;
            mInclude = mapping.mInclude;

            mLeftColumn = new CSVColumn(mapping.mLeftColumn.Name);
            mRightColumn = new CSVColumn(mapping.mRightColumn.Name);
            
            mType = mapping.mType;

            mMergeRule = mapping.mMergeRule;
            
            mIgnoreCase = mapping.mIgnoreCase;
            mIgnoreSymbols = mapping.mIgnoreSymbols;
            mAllowedSymbols = mapping.mAllowedSymbols;
            mIgnoreWhitespace = mapping.mIgnoreWhitespace;
            mDecimalPrecision = mapping.mDecimalPrecision;

            mForcedMatchings = mapping.mForcedMatchings;
        }

        //--------------------------------------------------------------------------------
        public CSVMapping(CSVMapping mapping, string leftColumn, string rightColumn) : this(mapping) {
            mLeftColumn = new CSVColumn(leftColumn);
            mRightColumn = new CSVColumn(rightColumn);
        }


        // ROLE ================================================================================
        //--------------------------------------------------------------------------------
        public bool Key { set { mKey = value; } get { return mKey; } }
        public bool Include { set { mInclude = value; } get { return mInclude; } }


        // COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        public CSVColumn LeftColumn { set { mLeftColumn = value; } get { return mLeftColumn; } }
        public string LeftColumnName { get { return mLeftColumn.Name; } }
        public CSVColumn RightColumn { set { mRightColumn = value; } get { return mRightColumn; } }
        public bool RemoveCase { set { mRemoveCase = value; } get { return mRemoveCase; } }
        public string Prefix { set { mPrefix = value; } get { return mPrefix; } }
        public string Suffix { set { mSuffix = value; } get { return mSuffix; } }
        public string RightColumnName { get { return mRightColumn.Name; } }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public EType Type { set { mType = value; } get { return mType; } }

        //--------------------------------------------------------------------------------
        public string TypeString {
            set { Enum.TryParse(value, out mType); }
            get { return mType.ToString(); }
        }


        // MERGE RULE ================================================================================
        //--------------------------------------------------------------------------------
        public EMergeRule MergeRule { set { mMergeRule = value; } get { return mMergeRule; } }

        //--------------------------------------------------------------------------------
        public string MergeRuleString {
            set { Enum.TryParse(value, out mMergeRule); }
            get { return mMergeRule.ToString(); }
        }

        //--------------------------------------------------------------------------------
        public bool RestrictMergeRuleByType() {
            if (!MergeRuleAllowedForType(mMergeRule)) {
                mMergeRule = EMergeRule.APPEND;
                return true;
            }
            else
                return false;
        }

        //--------------------------------------------------------------------------------
        public bool MergeRuleAllowedForType(EMergeRule mergeRule) {
            switch (mergeRule) {
                case EMergeRule.APPEND:                 return true;
                case EMergeRule.APPEND_WITH_SPACE:      return true;
                case EMergeRule.AVERAGE:                return (mType == EType.NUMBER || mType == EType.PERCENTAGE || mType == EType.DATE);
                case EMergeRule.ADD:                    return (mType == EType.NUMBER || mType == EType.PERCENTAGE);
                case EMergeRule.SUBTRACT_FROM_LEFT:     return (mType == EType.NUMBER || mType == EType.PERCENTAGE);
                case EMergeRule.SUBTRACT_FROM_RIGHT:    return (mType == EType.NUMBER || mType == EType.PERCENTAGE);
                case EMergeRule.DIFFERENCE:             return (mType == EType.NUMBER || mType == EType.PERCENTAGE);
                case EMergeRule.MULTIPLY:               return (mType == EType.NUMBER || mType == EType.PERCENTAGE);
                case EMergeRule.MINIMUM:                return (mType == EType.NUMBER || mType == EType.PERCENTAGE || mType == EType.DATE);
                case EMergeRule.MAXIMUM:                return (mType == EType.NUMBER || mType == EType.PERCENTAGE || mType == EType.DATE);
                case EMergeRule.LEFT:                   return true;
                case EMergeRule.RIGHT:                  return true;
                default: return false;
            }
        }


        // OPTIONS ================================================================================
        //--------------------------------------------------------------------------------
        public bool IgnoreCase { set { mIgnoreCase = value; } get { return mIgnoreCase; } }
        public bool IgnoreSymbols { set { mIgnoreSymbols = value; } get { return mIgnoreSymbols; } }
        public string AllowedSymbols { set { mAllowedSymbols = value; } get { return mAllowedSymbols; } }
        public bool IgnoreWhitespace { set { mIgnoreWhitespace = value; } get { return mIgnoreWhitespace; } }
        public int DecimalPrecision { set { mDecimalPrecision = value; } get { return mDecimalPrecision; } }

        //--------------------------------------------------------------------------------
        public string OptionsString {
            get {
                return UString.Join("   ",
                                    IgnoreCase ? "/Abc" : "",
                                    IgnoreSymbols ? "/$" + (!string.IsNullOrWhiteSpace(AllowedSymbols) ? $"[{AllowedSymbols}]" : "") : "",
                                    IgnoreWhitespace ? "/_" : "",
                                    DecimalPrecision >= 0 ? $"#.{DecimalPrecision}" : "").Trim();
                /*return string.Join("  ",
                                   IgnoreCase ? "C" : "",
                                   IgnoreSymbols ? "$" : "",
                                   IgnoreSymbols && !string.IsNullOrWhiteSpace(AllowedSymbols) ? $"[{AllowedSymbols}]" : "",
                                   IgnoreWhitespace ? "_" : "",
                                   DecimalPrecision >= 0 ? $".{DecimalPrecision}" : "");*/
                /*return string.Join(", ",
                                   IgnoreCase ? "case sensitive" : "",
                                   IgnoreSymbols ? "ignore symbols" : "",
                                   IgnoreSymbols && !string.IsNullOrWhiteSpace(AllowedSymbols) ? $"allowed symbols = {AllowedSymbols}" : "",
                                   IgnoreWhitespace ? "ignore whitespace" : "",
                                   DecimalPrecision >= 0 ? $"decimal precision = {DecimalPrecision}" : "");*/
            }
        }


        // COMPARISON ================================================================================
        //--------------------------------------------------------------------------------
        public bool RowsEqual(CSVDataRow leftRow, CSVDataRow rightRow) {
            // Values
            string leftValue = ComparisonValue(leftRow.Value(LeftColumnName) ?? "");
            string rightValue = ComparisonValue(rightRow.Value(RightColumnName) ?? "");

            // Forced matchings
            foreach (ForcedMatching m in mForcedMatchings) {
                if (m.Matches(this, leftValue, rightValue))
                    return true;
            }

            // Compare
            switch (Type) {
                case EType.TEXT:        return RowsEqual_Text(leftValue, rightValue);
                case EType.NUMBER:      return RowsEqual_Number(leftValue, rightValue);
                case EType.PERCENTAGE:  return RowsEqual_Percentage(leftValue, rightValue);
                case EType.DATE:        return RowsEqual_Date(leftValue, rightValue);
                default:                return false;
            }
        }
        
        //--------------------------------------------------------------------------------
        private bool RowsEqual_Text(string leftValue, string rightValue) {
            return leftValue.Equals(rightValue);
        }
        
        //--------------------------------------------------------------------------------
        private bool RowsEqual_Number(string leftValue, string rightValue) {
            // Convert
            if (!decimal.TryParse(leftValue, out decimal leftNumber) || !decimal.TryParse(rightValue, out decimal rightNumber))
                return false;

            // Decimal precision
            if (DecimalPrecision >= 0) {
                leftNumber = decimal.Round(leftNumber, DecimalPrecision, MidpointRounding.AwayFromZero);
                rightNumber = decimal.Round(rightNumber, DecimalPrecision, MidpointRounding.AwayFromZero);
            }
            
            // Compare
            return (decimal.Compare(leftNumber, rightNumber) == 0);
        }
        
        //--------------------------------------------------------------------------------
        private bool RowsEqual_Percentage(string leftValue, string rightValue) {
            // Remove percentage symbol
            bool leftHasPercentSymbol = leftValue.Contains('%');
            bool rightHasPercentSymbol = rightValue.Contains('%');
            leftValue = new string((from c in leftValue where c != '%' select c).ToArray());
            rightValue = new string((from c in rightValue where c != '%' select c).ToArray());

            // Convert
            if (!decimal.TryParse(leftValue, out decimal leftPercentage) || !decimal.TryParse(rightValue, out decimal rightPercentage))
                return false;

            // Add percentage symbol
            leftPercentage = leftHasPercentSymbol ? leftPercentage : leftPercentage * 100m;
            rightPercentage = rightHasPercentSymbol ? rightPercentage : rightPercentage * 100m;
            
            // Decimal precision
            if (DecimalPrecision >= 0) {
                leftPercentage = decimal.Round(leftPercentage, DecimalPrecision, MidpointRounding.AwayFromZero);
                rightPercentage = decimal.Round(rightPercentage, DecimalPrecision, MidpointRounding.AwayFromZero);
            }
            
            // Compare
            return (decimal.Compare(leftPercentage, rightPercentage) == 0);
        }
        
        //--------------------------------------------------------------------------------
        private bool RowsEqual_Date(string leftValue, string rightValue) {
            if (!DateTime.TryParse(leftValue, out DateTime leftDate) || !DateTime.TryParse(rightValue, out DateTime rightDate))
                return false;
            return (DateTime.Compare(leftDate, rightDate) == 0);
        }

        //--------------------------------------------------------------------------------
        private string ComparisonValue(string value) {
            if (IgnoreSymbols)
                value = new string((from c in value where char.IsLetterOrDigit(c) || AllowedSymbols.Contains(c) select c).ToArray());
            if (IgnoreCase)
                value = value.ToLower();
            if (IgnoreWhitespace)
                value = Regex.Replace(value, @"\s+", "");
            return value;
        }

        //--------------------------------------------------------------------------------
        private string ComparisonValue_Text(string value) { return ComparisonValue(value ?? ""); }

        //--------------------------------------------------------------------------------
        private decimal? ComparisonValue_Number(string value) {
            // Convert
            value = ComparisonValue(value ?? "");
            if (!decimal.TryParse(value, out decimal number))
                return null;

            // Decimal precision
            return (DecimalPrecision >= 0 ? decimal.Round(number, DecimalPrecision, MidpointRounding.AwayFromZero) : number);
        }

        //--------------------------------------------------------------------------------
        private decimal? ComparisonValue_Percentage(string value) {
            // Convert
            value = ComparisonValue(value ?? "");

            // Remove percentage symbol
            bool hasPercentSymbol = value.Contains('%');
            value = new string((from c in value where c != '%' select c).ToArray());

            // Convert
            if (!decimal.TryParse(value, out decimal percentage))
                return null;

            // Add percentage symbol
            percentage = hasPercentSymbol ? percentage : percentage * 100m;

            // Decimal precision
            return (DecimalPrecision >= 0 ? decimal.Round(percentage, DecimalPrecision, MidpointRounding.AwayFromZero) : percentage);
        }

        //--------------------------------------------------------------------------------
        private DateTime? ComparisonValue_Date(string value) {
            // Convert
            value = ComparisonValue(value ?? "");
            if (!DateTime.TryParse(value, out DateTime date))
                return null;
            return date;
        }

        //--------------------------------------------------------------------------------
        private object GenericComparisonValue(string value) {
            switch (mType) {
                case EType.TEXT:        return ComparisonValue_Text(value);
                case EType.NUMBER:      return ComparisonValue_Number(value);
                case EType.PERCENTAGE:  return ComparisonValue_Percentage(value);
                case EType.DATE:        return ComparisonValue_Date(value);
                default:                return ComparisonValue_Text(value);
            }
        }


        // MERGING ================================================================================
        //--------------------------------------------------------------------------------
        public EMergeResult MergedValue(CSVDataRow leftRow, CSVDataRow rightRow, out string mergedValue) {
            // Values
            string leftValue = leftRow.Value(LeftColumnName) ?? "";
            string rightValue = rightRow.Value(RightColumnName) ?? "";

            // Remove case
            if (RemoveCase) {
                leftValue = leftValue.ToLower();
                rightValue = rightValue.ToLower();
            }

            // Variables
            EMergeResult result = EMergeResult.SUCCESS;
            mergedValue = "";

            // Merge
            switch (Type) {
                case EType.TEXT:        result = MergedValue_Text(leftValue, rightValue, out mergedValue); break;
                case EType.NUMBER:      result = MergedValue_Number(leftValue, rightValue, out mergedValue); break;
                case EType.PERCENTAGE:  result = MergedValue_Percentage(leftValue, rightValue, out mergedValue); break;
                case EType.DATE:        result = MergedValue_Date(leftValue, rightValue, out mergedValue); break;
                default:                result = EMergeResult.INVALID_TYPE; break;
            }

            // Suffix
            mergedValue = mPrefix + mergedValue + mSuffix;
            return result;
        }

        //--------------------------------------------------------------------------------
        private EMergeResult MergedValue_Text(string leftValue, string rightValue, out string mergedValue) {
            switch (mMergeRule) {
                case EMergeRule.APPEND:             mergedValue = leftValue + rightValue; break;
                case EMergeRule.APPEND_WITH_SPACE:  mergedValue = leftValue + " " + rightValue; break;
                case EMergeRule.LEFT:               mergedValue = leftValue; break;
                case EMergeRule.RIGHT:              mergedValue = rightValue; break;
                default:                            mergedValue = ""; return EMergeResult.INVALID_TYPE;
            }
            return EMergeResult.SUCCESS;
        }

        //--------------------------------------------------------------------------------
        private EMergeResult MergedValue_Number(string leftValue, string rightValue, out string mergedValue) {
            // Sanitise
            leftValue = UString.SanitisedNumber(leftValue);
            rightValue = UString.SanitisedNumber(rightValue);

            // Convert
            mergedValue = "";
            if (!decimal.TryParse(leftValue, out decimal leftNumber) || !decimal.TryParse(rightValue, out decimal rightNumber))
                return EMergeResult.INVALID_NUMBERS;
            decimal leftNumberRounded = (DecimalPrecision >= 0 ? decimal.Round(leftNumber, DecimalPrecision, MidpointRounding.AwayFromZero) : leftNumber);
            decimal rightNumberRounded = (DecimalPrecision >= 0 ? decimal.Round(rightNumber, DecimalPrecision, MidpointRounding.AwayFromZero) : rightNumber);

            // Variables
            decimal mergedNumber = 0.0m;

            // Merge
            switch (mMergeRule) {
                case EMergeRule.APPEND:                 mergedValue = leftNumberRounded.ToString() + rightNumberRounded.ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.APPEND_WITH_SPACE:      mergedValue = leftNumberRounded.ToString() + " " + rightNumberRounded.ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.AVERAGE:                mergedNumber = (leftNumber + rightNumber) / 2.0m; break;
                case EMergeRule.ADD:                    mergedNumber = leftNumber + rightNumber; break;
                case EMergeRule.SUBTRACT_FROM_LEFT:     mergedNumber = leftNumber - rightNumber; break;
                case EMergeRule.SUBTRACT_FROM_RIGHT:    mergedNumber = rightNumber - leftNumber; break;
                case EMergeRule.DIFFERENCE:             mergedNumber = Math.Abs(leftNumber - rightNumber); break;
                case EMergeRule.MULTIPLY:               mergedNumber = leftNumber * rightNumber; break;
                case EMergeRule.MINIMUM:                mergedValue = (leftNumber <= rightNumber ? leftNumberRounded : rightNumberRounded).ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.MAXIMUM:                mergedValue = (leftNumber >= rightNumber ? leftNumberRounded : rightNumberRounded).ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.LEFT:                   mergedValue = leftNumberRounded.ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.RIGHT:                  mergedValue = rightNumberRounded.ToString(); return EMergeResult.SUCCESS;
                default:                                mergedValue = ""; return EMergeResult.INVALID_MERGE_RULE;
            }

            // Return
            mergedValue = (DecimalPrecision >= 0 ? decimal.Round(mergedNumber, DecimalPrecision, MidpointRounding.AwayFromZero) : mergedNumber).ToString();
            return EMergeResult.SUCCESS;
        }

        //--------------------------------------------------------------------------------
        private EMergeResult MergedValue_Percentage(string leftValue, string rightValue, out string mergedValue) { return MergedValue_Number(leftValue, rightValue, out mergedValue); }

        //--------------------------------------------------------------------------------
        private EMergeResult MergedValue_Date(string leftValue, string rightValue, out string mergedValue) {
            // Convert
            mergedValue = "";
            if (!DateTime.TryParse(leftValue, out DateTime leftDate) || !DateTime.TryParse(rightValue, out DateTime rightDate))
                return EMergeResult.INVALID_DATES;

            // Merge
            switch (mMergeRule) {
                case EMergeRule.APPEND:                 mergedValue = leftDate.ToString() + rightDate.ToString(); break;
                case EMergeRule.APPEND_WITH_SPACE:      mergedValue = leftDate.ToString() + " " + rightDate.ToString(); break;
                case EMergeRule.AVERAGE:                mergedValue = new DateTime((leftDate.Ticks + rightDate.Ticks) / 2).ToString(); break;
                case EMergeRule.MINIMUM:                mergedValue = (leftDate <= rightDate ? leftDate : rightDate).ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.MAXIMUM:                mergedValue = (leftDate >= rightDate ? leftDate : rightDate).ToString(); return EMergeResult.SUCCESS;
                case EMergeRule.LEFT:                   mergedValue = leftDate.ToString(); break;
                case EMergeRule.RIGHT:                  mergedValue = rightDate.ToString(); break;
                default:                                mergedValue = ""; return EMergeResult.INVALID_MERGE_RULE;
            }

            // Return
            return EMergeResult.SUCCESS;
        }

        
        // MATCHING ================================================================================
        //--------------------------------------------------------------------------------
        // Finds the matching rows for a single mapping.
        /*private static List<CSVDataRow> MatchingRightRows(CSVDataRow leftRow, IEnumerable<CSVDataRow> rightRows, CSVMapping mapping) {
            List<CSVDataRow> matches = new List<CSVDataRow>();

            foreach (CSVDataRow r in rightRows) {
                if (mapping.RowsEqual(leftRow, r))
                    matches.Add(r);
            }

            return matches;
        }

        //--------------------------------------------------------------------------------
        // Finds the matching rows for a single mapping.
        private static List<CSVDataRow> MatchingLeftRows(IEnumerable<CSVDataRow> leftRows, CSVDataRow rightRow, CSVMapping mapping) {
            List<CSVDataRow> matches = new List<CSVDataRow>();

            foreach (CSVDataRow r in leftRows) {
                if (mapping.RowsEqual(r, rightRow))
                    matches.Add(r);
            }

            return matches;
        }*/


        //--------------------------------------------------------------------------------
        // Finds the matching rows for a single mapping.
        // Forced matchings are ignored here as they don't gel with using lookups.
        public List<CSVDataRow> MatchingRightRows(CSVDataRow leftRow, CSVData right, IEnumerable<CSVDataRow> rightRows) {
            // Match lookup
            MatchLookupCache lookup = MatchLookup(right, RightColumnName);

            // Matches
            List<CSVDataRow> matchingRows = new List<CSVDataRow>();
            matchingRows.AddRange(lookup.MatchingRows(leftRow.Value(LeftColumnName)));

            // Forced matches
            IEnumerable<ForcedMatching> forcedMatchValues = (from m in mForcedMatchings where m.IsLeftValue(this, leftRow.Value(LeftColumnName)) select m);
            foreach (ForcedMatching m in forcedMatchValues) {
                matchingRows.AddRange(lookup.ForcedMatchingRows(m.RightValue, m.ExactMatch));
            }

            // Intersect
            return rightRows.Intersect(matchingRows).ToList();
        }

        //--------------------------------------------------------------------------------
        // Finds the matching rows for a single mapping.
        public List<CSVDataRow> MatchingLeftRows(CSVData left, IEnumerable<CSVDataRow> leftRows, CSVDataRow rightRow) {
            // Match lookup
            MatchLookupCache lookup = MatchLookup(left, LeftColumnName);

            // Matches
            List<CSVDataRow> matchingRows = new List<CSVDataRow>();
            matchingRows.AddRange(lookup.MatchingRows(rightRow.Value(RightColumnName)));

            // Forced matches
            IEnumerable<ForcedMatching> forcedMatchValues = (from m in mForcedMatchings where m.IsRightValue(this, rightRow.Value(RightColumnName)) select m);
            foreach (ForcedMatching m in forcedMatchValues) {
                matchingRows.AddRange(lookup.ForcedMatchingRows(m.LeftValue, m.ExactMatch));
            }

            // Intersect
            return leftRows.Intersect(matchingRows).ToList();
        }

        //--------------------------------------------------------------------------------
        // Finds the matching rows for multiple mappings.
        public static List<CSVDataRow> MatchingRightRows(CSVData left, CSVData right, CSVDataRow leftRow, IEnumerable<CSVMapping> mappings) {
            // Variables;
            List<CSVDataRow> matches;
            
            // Match
            if (mappings.Count() > 0) {
                // Left rows
                matches = new List<CSVDataRow>(right.Rows);
                if (left == right)
                    matches.Remove(leftRow);

                // Matches
                foreach (CSVMapping m in mappings) {
                    matches = m.MatchingRightRows(leftRow, right, matches);
                }
            }
            else {
                // Match by row index
                matches = new List<CSVDataRow>();
                if (leftRow.RowIndex < right.Rows.Count)
                    matches.Add(right.Rows[leftRow.RowIndex]);
            }
            
            // Return
            return matches;
        }

        //--------------------------------------------------------------------------------
        // Finds the matching rows for multiple mappings.
        public static List<CSVDataRow> MatchingLeftRows(CSVData left, CSVData right, CSVDataRow rightRow, IEnumerable<CSVMapping> mappings) {
            // Variables;
            List<CSVDataRow> matches;
            
            // Match
            if (mappings.Count() > 0) {
                // Left rows
                matches = new List<CSVDataRow>(left.Rows);
                if (left == right)
                    matches.Remove(rightRow);

                // Matches
                foreach (CSVMapping m in mappings) {
                    matches = m.MatchingLeftRows(left, matches, rightRow);
                }

                return matches;
            }
            else {
                // Match by row index
                matches = new List<CSVDataRow>();
                if (rightRow.RowIndex < left.Rows.Count)
                    matches.Add(left.Rows[rightRow.RowIndex]);
            }
            
            // Return
            return matches;
        }


        // FORCED MATCHINGS ================================================================================
        //--------------------------------------------------------------------------------
        public List<ForcedMatching> ForcedMatchings { get { return mForcedMatchings; } }

        //--------------------------------------------------------------------------------
        public List<ForcedMatching> ForcedMatchingsCopy {
            get {
                List<ForcedMatching> copy = new List<ForcedMatching>();
                foreach (ForcedMatching f in mForcedMatchings) {
                    copy.Add(new ForcedMatching(f));
                }
                return copy;
            }
        }

        
        // MATCH LOOKUPS ================================================================================
        //--------------------------------------------------------------------------------
        public MatchLookupCache MatchLookup(CSVData matchData, string matchColumn) {
            foreach (MatchLookupCache l in mMatchLookups) {
                if (l.matchData == matchData && l.matchColumn.Equals(matchColumn))
                    return l;
            }

            MatchLookupCache lookup = new MatchLookupCache(this, matchData, matchColumn);
            mMatchLookups.Add(lookup);
            return lookup;
        }
        
        //--------------------------------------------------------------------------------
        public void ClearMatchLookups() {
            mMatchLookups.Clear();
        }


        // SAVING / LOADING ================================================================================
        //--------------------------------------------------------------------------------
        public void SaveJSON(JsonTextWriter writer) {
            // Comparison
            writer.WritePropertyName("Key"); writer.WriteValue(mKey);
            writer.WritePropertyName("Include"); writer.WriteValue(mInclude);

            // Columns
            writer.WritePropertyName("LeftColumn"); writer.WriteValue(mLeftColumn.Name);
            writer.WritePropertyName("RightColumn"); writer.WriteValue(mRightColumn.Name);

            // Type
            writer.WritePropertyName("Type"); writer.WriteValue((int)mType);

            // Merge rule
            writer.WritePropertyName("MergeRule"); writer.WriteValue((int)mMergeRule);

            // Options
            writer.WritePropertyName("IgnoreCase"); writer.WriteValue(mIgnoreCase);
            writer.WritePropertyName("IgnoreSymbols"); writer.WriteValue(mIgnoreSymbols);
            writer.WritePropertyName("AllowedSymbols"); writer.WriteValue(mAllowedSymbols);
            writer.WritePropertyName("IgnoreWhitespace"); writer.WriteValue(mIgnoreWhitespace);
            writer.WritePropertyName("RemoveCase"); writer.WriteValue(mRemoveCase);
            writer.WritePropertyName("Prefix"); writer.WriteValue(mPrefix);
            writer.WritePropertyName("Suffix"); writer.WriteValue(mSuffix);
            writer.WritePropertyName("DecimalPrecision"); writer.WriteValue(mDecimalPrecision);

            // Forced matchings
            writer.WritePropertyName("ForcedMatchings");
            writer.WriteStartArray();
            foreach (ForcedMatching m in mForcedMatchings) {
                writer.WriteStartObject();
                m.WriteJSON(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
        
        //--------------------------------------------------------------------------------
        public void LoadJSON(JToken token) {
            // Comparison
            mKey = (bool)token.SelectToken("Key");
            mInclude = (bool)token.SelectToken("Include");
            
            // Columns
            mLeftColumn = new CSVColumn((string)token.SelectToken("LeftColumn"));
            mRightColumn = new CSVColumn((string)token.SelectToken("RightColumn"));
            
            // Type
            mType = (EType)((int)token.SelectToken("Type"));
            
            // Merge rule
            mMergeRule = (EMergeRule)((int)token.SelectToken("MergeRule"));
            
            // Options
            mIgnoreCase = (bool)token.SelectToken("IgnoreCase");
            mIgnoreSymbols = (bool)token.SelectToken("IgnoreSymbols");
            mAllowedSymbols = (string)token.SelectToken("AllowedSymbols");
            mIgnoreWhitespace = (bool)token.SelectToken("IgnoreWhitespace");
            mRemoveCase = (bool)(token.SelectToken("RemoveCase") ?? false);
            mPrefix = (string)(token.SelectToken("Prefix") ?? "");
            mSuffix = (string)(token.SelectToken("Suffix") ?? "");
            mDecimalPrecision = (int)token.SelectToken("DecimalPrecision");

            // Forced matchings
            JArray forcedMatchings = (JArray)token.SelectToken("ForcedMatchings");
            if (forcedMatchings != null) {
                foreach (JToken m in forcedMatchings) {
                    mForcedMatchings.Add(new ForcedMatching(m));
                }
            }
        }


        //================================================================================
        //********************************************************************************
        public class ForcedMatching {
            public string leftValue;
            public string rightValue;
            public bool exactMatch;

            public ForcedMatching(string leftValue, string rightValue, bool exactMatch) {
                this.leftValue = leftValue;
                this.rightValue = rightValue;
                this.exactMatch = exactMatch;
            }

            public ForcedMatching(ForcedMatching other) {
                leftValue = other.leftValue;
                rightValue = other.rightValue;
                exactMatch = other.exactMatch;
            }

            public ForcedMatching(JToken token) {
                ReadJSON(token);
            }

            public bool Matches(CSVMapping mapping, string leftComparisonValue, string rightComparisonValue) {
                // Values
                leftComparisonValue = !exactMatch ? mapping.ComparisonValue(leftComparisonValue) : leftComparisonValue;
                rightComparisonValue = !exactMatch ? mapping.ComparisonValue(rightComparisonValue) : rightComparisonValue;
                string leftMatchValue = !exactMatch ? mapping.ComparisonValue(leftValue) : leftValue;
                string rightMatchValue = !exactMatch ? mapping.ComparisonValue(rightValue) : rightValue;

                // Compare
                return (leftComparisonValue.Equals(leftMatchValue) && rightComparisonValue.Equals(rightMatchValue));
            }

            public string LeftValue { set { leftValue = value; } get { return leftValue; } }
            public string RightValue { set { rightValue = value; } get { return rightValue; } }
            public bool ExactMatch { set { exactMatch = value; } get { return exactMatch; } }

            public bool IsLeftValue(CSVMapping mapping, string value) {
                value = !exactMatch ? mapping.ComparisonValue(value) : value;
                string leftMatchValue = !exactMatch ? mapping.ComparisonValue(leftValue) : leftValue;
                return value.Equals(leftMatchValue);
            }

            public bool IsRightValue(CSVMapping mapping, string value) {
                value = !exactMatch ? mapping.ComparisonValue(value) : value;
                string rightMatchValue = !exactMatch ? mapping.ComparisonValue(rightValue) : rightValue;
                return value.Equals(rightMatchValue);
            }

            public void WriteJSON(JsonTextWriter writer) {
                writer.WritePropertyName("LeftValue"); writer.WriteValue(leftValue);
                writer.WritePropertyName("RightValue"); writer.WriteValue(rightValue);
                writer.WritePropertyName("ExactMatch"); writer.WriteValue(exactMatch);
            }

            public void ReadJSON(JToken token) {
                leftValue = (string)token.SelectToken("LeftValue");
                rightValue = (string)token.SelectToken("RightValue");
                exactMatch = (bool)token.SelectToken("ExactMatch");
            }
        }
        
        //********************************************************************************
        public class MatchLookupCache {
            public CSVMapping mapping;
            public CSVData matchData;
            public string matchColumn;

            public ILookup<object, CSVDataRow> matchLookup;
            public ILookup<string, CSVDataRow> forcedMatchLookup;
            public ILookup<string, CSVDataRow> forcedMatchExactLookup;

            public MatchLookupCache(CSVMapping mapping, CSVData matchData, string matchColumn) {
                this.mapping = mapping;
                this.matchData = matchData;
                this.matchColumn = matchColumn;

                BuildLookups();
            }

            public void Dispose() {
                matchLookup = null;
            }

            private void BuildLookups() {
                List<KeyValuePair<object, CSVDataRow>> lookup = new List<KeyValuePair<object, CSVDataRow>>();
                List<KeyValuePair<string, CSVDataRow>> forcedLookup = new List<KeyValuePair<string, CSVDataRow>>();
                List<KeyValuePair<string, CSVDataRow>> forcedExactLookup = new List<KeyValuePair<string, CSVDataRow>>();

                foreach (CSVDataRow r in matchData.Rows) {
                    lookup.Add(new KeyValuePair<object, CSVDataRow>(mapping.GenericComparisonValue(r.Value(matchColumn) ?? ""), r));
                    forcedLookup.Add(new KeyValuePair<string, CSVDataRow>(mapping.ComparisonValue(r.Value(matchColumn) ?? ""), r));
                    forcedExactLookup.Add(new KeyValuePair<string, CSVDataRow>(mapping.ComparisonValue(r.Value(matchColumn) ?? ""), r));
                }

                matchLookup = lookup.ToLookup(i => i.Key, i => i.Value);
                forcedMatchLookup = forcedLookup.ToLookup(i => i.Key, i => i.Value);
                forcedMatchExactLookup = forcedExactLookup.ToLookup(i => i.Key, i => i.Value);
            }

            public IEnumerable<CSVDataRow> MatchingRows(string value) {
                object genericValue = mapping.GenericComparisonValue(value ?? "");
                return matchLookup[genericValue];
            }

            public IEnumerable<CSVDataRow> ForcedMatchingRows(string value, bool exact) {
                if (!exact)
                    return forcedMatchLookup[mapping.ComparisonValue(value ?? "")];
                else
                    return forcedMatchExactLookup[value];
            }
        }
    }

}
