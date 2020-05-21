using CSACore.CSV;
using CSVComparer.CSVComparison;
using DevExpress.Data;
using DevExpress.Images;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CSVComparer.Forms {

    public partial class CompareResultsForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private List<CSVCompareMergeIssue>           mIssues;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CompareResultsForm(List<CSVCompareMergeIssue> issues, IEnumerable<CSVMapping> mappings) {
            // Initialise
            InitializeComponent();

            // Issues
            mIssues = issues;
            grdIssues.DataSource = mIssues;

            // Sequence
            /*GridColumn sequenceColumn = new GridColumn() {
                Name = $"grdIssues_Sequence",
                Caption = $"#",
                UnboundType = UnboundColumnType.String,
                FieldName = $"_sequence_", // Needed to cause CustomUnboundColumnData to fire - must be unique *and* different than any actual property names
                MinWidth = 50,
                MaxWidth = 50,
                Visible = true
            };
            grvIssues.Columns.Insert(0, sequenceColumn);*/

            // Issues appearance
            Color backColour = grvIssues.PaintAppearance.Empty.BackColor;
            if (backColour.GetBrightness() <= 0.5f)
                backColour = Color.FromArgb(backColour.R + 14, backColour.G + 14, backColour.B + 14);
            else
                backColour = Color.FromArgb(backColour.R - 14, backColour.G - 14, backColour.B - 14);

            grdIssues_LeftRow.AppearanceCell.BackColor = backColour;
            grdIssues_RightRow.AppearanceCell.BackColor = backColour;
            grdIssues_IssueType.AppearanceCell.BackColor = backColour;
            
            // Mappings
            int i = 0;
            foreach (CSVMapping m in (from m in mappings where m.Include || m.Key select m)) {
                // Left column
                GridColumn leftColumn = new GridColumn() {
                    Name = $"grdIssues_LeftValue{i}",
                    Caption = $"< {m.LeftColumnName}",
                    UnboundType = UnboundColumnType.String,
                    FieldName = $"_leftvalue{i}_", // Needed to cause CustomUnboundColumnData to fire - must be unique *and* different than any actual property names
                    Tag = new ColumnTag(true, i, m.LeftColumnName),
                    MinWidth = 200,
                    Visible = true
                };

                grvIssues.Columns.Add(leftColumn);
                leftColumn.AppearanceHeader.BackColor = Color.FromArgb(112, 229, 117);

                if (m.Key) {
                    leftColumn.ImageOptions.Alignment = StringAlignment.Near;
                    leftColumn.ImageOptions.Image = svgImages.GetImage("security_key");
                    leftColumn.AppearanceCell.Font = new Font(leftColumn.AppearanceCell.Font, FontStyle.Bold);
                }

                // Right column
                GridColumn rightColumn = new GridColumn() {
                    Name = $"grdIssues_RightValue{i}",
                    Caption = $"> {m.RightColumnName}",
                    UnboundType = UnboundColumnType.String,
                    FieldName = $"_rightvalue{i}_",
                    Tag = new ColumnTag(false, i, m.RightColumnName),
                    MinWidth = 200,
                    Visible = true
                };

                grvIssues.Columns.Add(rightColumn);
                rightColumn.AppearanceHeader.BackColor = Color.FromArgb(88, 184, 254);

                if (m.Key) {
                    rightColumn.ImageOptions.Alignment = StringAlignment.Near;
                    rightColumn.ImageOptions.Image = svgImages.GetImage("security_key");
                    rightColumn.AppearanceCell.Font = new Font(rightColumn.AppearanceCell.Font, FontStyle.Bold);
                }

                // Next
                ++i;
            }
        }


        // ISSUES ================================================================================
        //--------------------------------------------------------------------------------
        private void grvIssues_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e) {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        //--------------------------------------------------------------------------------
        private void grvIssues_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            CSVCompareMergeIssue issue = (CSVCompareMergeIssue)grvIssues.GetRow(e.RowHandle);
            ColumnTag tag = (ColumnTag)e.Column.Tag;
            if (tag != null && tag.IsIssueColumn(issue))
                e.Appearance.BackColor = tag.left ? GreenHighlightBackColour : BlueHighlightBackColour;
        }

        //--------------------------------------------------------------------------------
        private void grvIssues_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e) {
            // Issue
            CSVCompareMergeIssue issue = (CSVCompareMergeIssue)e.Row;

            // CSV values
            int valueIndex = ((ColumnTag)e.Column.Tag).valueIndex;
            if (e.Column.Name.StartsWith("grdIssues_LeftValue"))
                e.Value = valueIndex < issue.LeftValues.Count ? issue.LeftValues[valueIndex] : "";
            else if (e.Column.Name.StartsWith("grdIssues_RightValue"))
                e.Value = valueIndex < issue.RightValues.Count ? issue.RightValues[valueIndex] : "";
        }
        
        //--------------------------------------------------------------------------------
        private void grvIssues_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            CSVCompareMergeIssue issue = (CSVCompareMergeIssue)grvIssues.GetRow(e.FocusedRowHandle);
            memDetails.Text = issue.Details.Replace("\n", "\r\n");
        }
        
        //--------------------------------------------------------------------------------
        private void mnuIssues_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (e.ClickedItem == mnuIssues_Copy)
                grvIssues.CopyToClipboard();
        }


        // EXPORT ================================================================================
        //--------------------------------------------------------------------------------
        private void btnExportCSV_Click(object sender, EventArgs e) {           
            // Browse
            if (dlgExportCSV.ShowDialog() != DialogResult.OK)
                return;

            // Export
            CSVWriter csv = new CSVWriter(dlgExportCSV.FileName);

            // Headers
            foreach (GridColumn c in grvIssues.Columns) {
                csv.WriteHeader(c.Caption);
                if (c == grdIssues_IssueType)
                    csv.WriteHeader("Details");
            }
            csv.WriteEndRow();

            // Rows
            for (int j = 0; j < grvIssues.RowCount; ++j) {
                CSVCompareMergeIssue issue = (CSVCompareMergeIssue)grvIssues.GetRow(j);

                foreach (GridColumn c in grvIssues.Columns) {
                    csv.WriteValue(grvIssues.GetRowCellDisplayText(j, c));
                    if (c == grdIssues_IssueType)
                        csv.WriteValue(issue.Details);
                }
                
                csv.WriteEndRow();
            }

            // Close
            csv.Close();
        }

        
        // COLOURS ================================================================================
        //--------------------------------------------------------------------------------
        private Color GreenHighlightBackColour {
            get {
                float brightness = grvIssues.PaintAppearance.Empty.BackColor.GetBrightness() * 255.0f;
                return Color.FromArgb((int)Math.Round(-18.387f + 0.483871f * brightness),
                                      (int)Math.Round(36.383f + 0.7004608f * brightness),
                                      (int)Math.Round(-16.912f + 0.4976959f * brightness));
            }
        }
        
        //--------------------------------------------------------------------------------
        private Color BlueHighlightBackColour {
            get {
                float brightness = grvIssues.PaintAppearance.Empty.BackColor.GetBrightness() * 255.0f;
                return Color.FromArgb((int)Math.Round(-14.359f + 0.3778801f * brightness),
                                      (int)Math.Round(26.111f + 0.5760369f * brightness),
                                      (int)Math.Round(56.858f + 0.7142857f * brightness));
            }
        }

        //--------------------------------------------------------------------------------
        private Color RedHighlightBackColour {
            get {
                float brightness = grvIssues.PaintAppearance.Empty.BackColor.GetBrightness() * 255.0f;
                return Color.FromArgb((int)Math.Round(89.364f + 0.6221199f * brightness),
                                      (int)Math.Round(-34.322f + 0.9032258f * brightness),
                                      (int)Math.Round(-34.322f + 0.9032258f * brightness));
            }
        }
        
        //--------------------------------------------------------------------------------
        private Color RedHighlightForeColour {
            get {
                float brightness = grvIssues.PaintAppearance.Empty.BackColor.GetBrightness() * 255.0f;
                return Color.FromArgb((int)Math.Round(240.305 + -0.1658986 * brightness),
                                      (int)Math.Round(80.807 + -0.2580645 * brightness),
                                      (int)Math.Round(78.53 + -0.1981567 * brightness));
            }
        }
        

        //================================================================================
        //********************************************************************************
        private class ColumnTag {
            public bool left;
            public int valueIndex;
            public string columnName;

            public ColumnTag(bool left, int valueIndex, string columnName) {
                this.left = left;
                this.valueIndex = valueIndex;
                this.columnName = columnName;
            }

            public bool IsIssueColumn(CSVCompareMergeIssue issue) { return ((left && issue.HasLeftIssueColumn(columnName)) || (!left && issue.HasRightIssueColumn(columnName))); }
        }
    }

}
