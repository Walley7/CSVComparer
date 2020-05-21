using CSACore.CSV;
using CSVComparer.CSVComparison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CSVComparer.CSVComparison {

    public class CSVCompareMergeIssue {
        //================================================================================
        public enum EType {
            PROGRAM_ERROR,
            LINKING,
            UNEQUAL,
            MERGE_LINKING,
            INCOMPATIBLE_DATA
        }
        
        //--------------------------------------------------------------------------------
        private EType                           mType;

        private string                          mDetails;
        private List<string>                    mDetailsList;

        private int?                            mLeftRow;
        private int?                            mRightRow;

        private List<string>                    mLeftValues = new List<string>();
        private List<string>                    mRightValues = new List<string>();

        private List<string>                    mLeftIssueColumns = new List<string>();
        private List<string>                    mRightIssueColumns = new List<string>();


        //================================================================================
        //--------------------------------------------------------------------------------
        public CSVCompareMergeIssue() { }

        //--------------------------------------------------------------------------------
        public CSVCompareMergeIssue(EType type, string details, int? leftRow, int? rightRow) {
            Initialise(type, details, leftRow, rightRow);
        }
        
        //--------------------------------------------------------------------------------
        public CSVCompareMergeIssue(EType type, int? leftRow, int? rightRow) {
            Initialise(type, leftRow, rightRow);
        }

        //--------------------------------------------------------------------------------
        // Allows re-using an instance to reduce memory churn.
        public void Initialise(EType type, string details, int? leftRow, int? rightRow) {
            // Reset
            if (mDetailsList != null)
                mDetailsList.Clear();
            mLeftValues.Clear();
            mRightValues.Clear();
            mLeftIssueColumns.Clear();
            mRightIssueColumns.Clear();

            // Issue
            mType = type;
            mDetails = details;
            mLeftRow = leftRow;
            mRightRow = rightRow;
        }

        //--------------------------------------------------------------------------------
        public void Initialise(EType type, int? leftRow, int? rightRow) { Initialise(type, "", leftRow, rightRow); }


        // TYPE ================================================================================
        //--------------------------------------------------------------------------------
        public EType Type { get { return mType; } }

        //--------------------------------------------------------------------------------
        public string TypeString {
            get {
                switch (mType) {
                    case EType.PROGRAM_ERROR:       return "Program Error";
                    case EType.LINKING:             return "Linking Failure";
                    case EType.UNEQUAL:             return "Inequality";
                    case EType.MERGE_LINKING:       return "Merged As Is";
                    case EType.INCOMPATIBLE_DATA:   return "Incompatible Data";
                    default:                        return "";
                }
            }
        }


        // DETAILS ================================================================================
        //--------------------------------------------------------------------------------
        public string Details { set { mDetails = value; } get { return mDetails; } }

        //--------------------------------------------------------------------------------
        public void AddDetails(string details, string[] leftIssueColumns = null, string[] rightIssueColumns = null) {
            mDetails += (HasDetails ? "\n" : "") + details;
            if (leftIssueColumns != null)
                AddLeftIssueColumns(leftIssueColumns);
            if (rightIssueColumns != null)
                AddRightIssueColumns(rightIssueColumns);
        }
        
        //--------------------------------------------------------------------------------
        public void AddDetailsHeading(string heading) {
            mDetails = heading + "\n- " + mDetails.Replace("\n", "\n- ");
        }

        //--------------------------------------------------------------------------------
        public bool HasDetails { get { return !string.IsNullOrEmpty(mDetails); } }

        //--------------------------------------------------------------------------------
        public List<string> DetailsBreakdown {
            get {
                if (mDetailsList == null)
                    mDetailsList = new List<string>(mDetails.Split('\n'));
                return mDetailsList;
            }
        }
        

        // ROWS ================================================================================
        //--------------------------------------------------------------------------------
        public int? LeftRow { get { return mLeftRow; } }
        public int? RightRow { get { return mRightRow; } }


        // VALUES ================================================================================
        //--------------------------------------------------------------------------------
        public void AddValues(CSVDataRow left, CSVDataRow right, IEnumerable<CSVMapping> mappings) {
            foreach (CSVMapping mapping in mappings) {
                if (mapping.Include || mapping.Key) {
                    mLeftValues.Add(left != null ? left.Value(mapping.LeftColumnName) : null);
                    mRightValues.Add(right != null ? right.Value(mapping.RightColumnName) : null);
                }
            }
        }

        //--------------------------------------------------------------------------------
        public List<string> LeftValues { get { return mLeftValues; } }
        public List<string> RightValues { get { return mRightValues; } }


        // ISSUE COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        public void AddLeftIssueColumns(params string[] columns) {
            foreach (string c in columns) {
                if (!mLeftIssueColumns.Contains(c))
                    mLeftIssueColumns.Add(c);
            }
        }

        //--------------------------------------------------------------------------------
        public void AddRightIssueColumns(params string[] columns) {
            foreach (string c in columns) {
                if (!mRightIssueColumns.Contains(c))
                    mRightIssueColumns.Add(c);
            }
        }

        //--------------------------------------------------------------------------------
        public List<string> LeftIssueColumns { get { return mLeftIssueColumns; } }
        public List<string> RightIssueColumns { get { return mRightIssueColumns; } }
        
        //--------------------------------------------------------------------------------
        public bool HasLeftIssueColumn(string column) { return mLeftIssueColumns.Contains(column); }
        public bool HasRightIssueColumn(string column) { return mRightIssueColumns.Contains(column); }
    }

}
