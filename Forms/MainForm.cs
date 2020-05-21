using CSACore.Core;
using CSACore.CSV;
using CSACore.Utility;
using CSACoreWin.Core;
using CSVComparer.CSV_Comparison;
using CSVComparer.CSVComparison;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CSVComparer.Forms {

    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        public const string                     DEFAULT_THEME = "Black";


        //================================================================================
        private ProgressForm                    mProgressForm = null;

        private string                          mLeftCSVFilePath;
        private string                          mRightCSVFilePath;

        private List<CSVColumn>                 mLeftColumnsList = new List<CSVColumn>();
        private BindingList<CSVColumn>          mLeftColumns;

        private List<CSVColumn>                 mRightColumnsList = new List<CSVColumn>();
        private BindingList<CSVColumn>          mRightColumns;

        private CSVMappingManager               mCSVMappingManager = new CSVMappingManager();

        private CompareMergeResults             mLatestCompareMergeResults = null;

        private Alert                           mAlert = null;

        private CSVMapping.EMergeRule           mPreviousMergeRule;

        private string                          mTheme = "";


        //================================================================================
        //--------------------------------------------------------------------------------
        public MainForm() {
            // Initialise
            InitializeComponent();

            // Theme
            Theme = DEFAULT_THEME;

            // Recent files
            CSAWin.ApplicationData.LoadRecentFiles("csv");
            CSAWin.ApplicationData.LoadRecentFiles("mapping");

            // Layout
            UpdateLayout();

            // Columns
            mLeftColumns = new BindingList<CSVColumn>(mLeftColumnsList);
            mRightColumns = new BindingList<CSVColumn>(mRightColumnsList);
            grdLeftColumns.DataSource = mLeftColumns;
            grvLeftColumns.ActiveFilterString = "[Visible] = true";
            grdRightColumns.DataSource = mRightColumns;
            grvRightColumns.ActiveFilterString = "[Visible] = true";

            // Mappings
            grdMappings.DataSource = mCSVMappingManager.CSVMappings;
            cboMappings_Type.Items.AddRange(Enum.GetValues(typeof(CSVMapping.EType)));
            cboMappings_MergeRule.Items.AddRange(Enum.GetValues(typeof(CSVMapping.EMergeRule)));

            // Refresh
            RefreshMappingControls();
        }

        
        // FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void MainForm_Shown(object sender, EventArgs e) {
            UpdateTheme();
        }

        //--------------------------------------------------------------------------------
        private void MainForm_Resize(object sender, EventArgs e) {
            UpdateLayout();
            if (mAlert != null)
                mAlert.UpdateLayout();
        }

        //--------------------------------------------------------------------------------
        private void MainForm_Move(object sender, EventArgs e) {
            if (mAlert != null)
                mAlert.UpdateLayout();
        }
        
        //--------------------------------------------------------------------------------
        private void MainForm_Deactivate(object sender, EventArgs e) {
            if (mAlert != null)
                mAlert.form.Hide();
        }
        

        // LAYOUT ================================================================================
        //--------------------------------------------------------------------------------
        private void UpdateLayout() {
            // Tiles - dimensions
            tlbCSVTiles.WideTileWidth = (lycMain.Width - 43) / 2;

            // Column mappings
            esiColumnMappingControlsTop.Height = (esiColumnMappingControlsTop.Height + esiColumnMappingControlsBottom.Height) / 2;
        }


        // COMPARISON / MERGING ================================================================================
        //--------------------------------------------------------------------------------
        private void btnCompareMerge_Click(object sender, EventArgs e) {
            // No mappings
            if (mCSVMappingManager.CSVMappings.Count == 0) {
                XtraMessageBox.Show("Please add at least one mapping.", "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Keys
            //if ((from m in mCSVMappingManager.CSVMappings where m.Key && HasLeftColumn(m.LeftColumnName) && HasRightColumn(m.RightColumnName) select m).Count() == 0) {
            //    XtraMessageBox.Show("Please mark at least one valid mapping as a key.", "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            // Missing columns
            if (!HasAllColumns) {
                if (XtraMessageBox.Show("Some of the mappings are not valid for the selected files - these will be ignored. Continue?", "CSV Comparer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            // Compare / merge
            switch (btnCompareMerge.Text) {
                case "Compare": Compare(); break;
                case "Merge":   Merge(); break;
            }
        }
        

        // COMPARISON ================================================================================
        //--------------------------------------------------------------------------------
        private void Compare() {
            // Create progress form
            CreateProgressForm("Loading files...");

            // Compare / merge
            CompareArgs args = new CompareArgs(mLeftCSVFilePath, mRightCSVFilePath, mCSVMappingManager.CSVMappingsCopy);
            bgwCompare.RunWorkerAsync(args);

            // Show progress form
            DialogResult result = ShowProgressForm();

            // Outcome
            if (result == DialogResult.OK) {
                // Results
                if (mLatestCompareMergeResults != null) {
                    CompareResultsForm compareResultsForm = new CompareResultsForm(mLatestCompareMergeResults.issues, mCSVMappingManager.CSVMappings);
                    compareResultsForm.Text = $"CSV Comparer - Comparison Log";
                    compareResultsForm.ShowDialog();
                }
            }
            else if (result == DialogResult.Cancel && bgwCompare.IsBusy) {
                // Cancellation
                bgwCompare.CancelAsync();
                while (bgwCompare.IsBusy) {
                    Application.DoEvents();
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnCompareMerge_Compare_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { btnCompareMerge.Text = "Compare"; }
        private void btnCompareMerge_Merge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { btnCompareMerge.Text = "Merge"; }
        private void btnCompareMerge_CompareMerge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { btnCompareMerge.Text = "Compare && Merge"; }
        
        //--------------------------------------------------------------------------------
        private void bgwCompare_DoWork(object sender, DoWorkEventArgs e) {
            // Input / output
            CompareArgs args = (CompareArgs)e.Argument;
            CompareMergeResults results = new CompareMergeResults();

            // Compare
            try {
                CSVCompare compare = new CSVCompare(bgwCompare, args.leftCSVFilePath, args.rightCSVFilePath, args.mappings);
                compare.ProgressEvent += (int percent, int? progress, int? progressMaximum, string stage, int row, int rowCount, TimeSpan timeRemaining) => {
                    bgwCompare.ReportProgress(percent, new CompareMergeProgress(progress, progressMaximum, stage, row, rowCount, timeRemaining));
                };

                if (compare.Compare())
                    results.issues = compare.Issues;
                else
                    e.Cancel = true;
            }
            catch (Exception ex) { results.exception = ex; }

            // Results
            e.Result = results;
        }
        
        //--------------------------------------------------------------------------------
        private void bgwCompare_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            CompareMergeProgress progress = (CompareMergeProgress)e.UserState;
            UpdateProgressForm(progress.stage, progress.progress, progress.progressMaximum, progress.rowProgress, progress.timeRemaining);
        }
        
        //--------------------------------------------------------------------------------
        private void bgwCompare_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            // Reset
            mCSVMappingManager.ClearMatchLookups();
            mLatestCompareMergeResults = null;

            // Cancellation
            if (e.Cancelled)
                return;

            // Results
            CompareMergeResults results = (CompareMergeResults)e.Result;
            if (results.exception != null) {
                XtraMessageBox.Show(results.exception.Message, "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseProgressForm();
            }

            mLatestCompareMergeResults = results;

            // Complete
            CompleteProgressForm("Done!");
        }
      

        // MERGE ================================================================================
        //--------------------------------------------------------------------------------
        private void Merge() {
            // Inputs
            if (dlgOutputCSV.ShowDialog() != DialogResult.OK)
                return;
            string outputPath = dlgOutputCSV.FileName;

            // Create progress form
            CreateProgressForm("Loading files...");

            // Compare / merge
            MergeArgs args = new MergeArgs(mLeftCSVFilePath, mRightCSVFilePath, mCSVMappingManager.CSVMappingsCopy, outputPath);
            bgwMerge.RunWorkerAsync(args);

            // Show progress form
            DialogResult result = ShowProgressForm();

            // Outcome
            if (result == DialogResult.OK) {
                // Results
                if (mLatestCompareMergeResults != null) {
                    CompareResultsForm compareResultsForm = new CompareResultsForm(mLatestCompareMergeResults.issues, mCSVMappingManager.CSVMappings);
                    compareResultsForm.Text = $"CSV Comparer - Merge Log";
                    compareResultsForm.ShowDialog();
                }

                // Merged file
                if (!string.IsNullOrEmpty(outputPath))
                    Process.Start(outputPath);
            }
            else if (result == DialogResult.Cancel && bgwCompare.IsBusy) {
                // Cancellation
                bgwMerge.CancelAsync();
                while (bgwMerge.IsBusy) {
                    Application.DoEvents();
                }
            }
        }

        //--------------------------------------------------------------------------------
        private void bgwMerge_DoWork(object sender, DoWorkEventArgs e) {
            // Input / output
            MergeArgs args = (MergeArgs)e.Argument;
            CompareMergeResults results = new CompareMergeResults();

            // Merge
            try {
                CSVMerge merge = new CSVMerge(bgwMerge, args.leftCSVFilePath, args.rightCSVFilePath, args.mappings, args.outputPath);
                merge.ProgressEvent += (int percent, int? progress, int? progressMaximum, string stage, int row, int rowCount, TimeSpan timeRemaining) => {
                    bgwMerge.ReportProgress(percent, new CompareMergeProgress(progress, progressMaximum, stage, row, rowCount, timeRemaining));
                };

                if (merge.Merge())
                    results.issues = merge.Issues;
                else
                    e.Cancel = true;
            }
            catch (Exception ex) { results.exception = ex; }

            // Results
            e.Result = results;
        }
        
        //--------------------------------------------------------------------------------
        private void bgwMerge_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            CompareMergeProgress progress = (CompareMergeProgress)e.UserState;
            UpdateProgressForm(progress.stage, progress.progress, progress.progressMaximum, progress.rowProgress, progress.timeRemaining);
        }
        
        //--------------------------------------------------------------------------------
        private void bgwMerge_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            // Reset
            mCSVMappingManager.ClearMatchLookups();
            mLatestCompareMergeResults = null;

            // Cancellation
            if (e.Cancelled)
                return;

            // Results
            CompareMergeResults results = (CompareMergeResults)e.Result;
            if (results.exception != null) {
                XtraMessageBox.Show(results.exception.Message, "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseProgressForm();
            }

            mLatestCompareMergeResults = results;

            // Complete
            CompleteProgressForm("Done!");
        }


        // CONTROLS - TILES ================================================================================
        //--------------------------------------------------------------------------------
        private void tlbCSVTiles_ItemClick(object sender, TileItemEventArgs e) {
            // Path
            string filePath = (e.Item == tbiLeftCSV ? mLeftCSVFilePath : mRightCSVFilePath);

            // Checks
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            // Open
            try { Process.Start(filePath); }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message, "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        //--------------------------------------------------------------------------------
        private void tlbCSVTiles_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e) {
            switch (e.Item.Name) {
                case "btnCSVTiles_Browse":
                    // Browse
                    if (dlgLoadCSV.ShowDialog() != DialogResult.OK)
                        return;

                    // Load CSV
                    LoadCSV((TileItem)e.DataItem, dlgLoadCSV.FileName);
                    break;

                case "btnCSVTiles_Recent":
                    // Clear items
                    mnuRecentCSVs.ItemLinks.Clear();

                    // Recent CSVs
                    int i = 0;
                    foreach (string f in CSAWin.ApplicationData.RecentFiles("csv")) {
                        if (i >= 10)
                            break;
                        BarButtonItem item = new BarButtonItem();
                        item.Caption = $"{++i}  " + f;
                        item.ItemClick += (object s, ItemClickEventArgs args) => { LoadCSV((TileItem)e.DataItem, f); };
                        mnuRecentCSVs.AddItem(item);
                    }

                    // Show
                    mnuRecentCSVs.ShowPopup(Cursor.Position);
                    break;
            }
        }

        //--------------------------------------------------------------------------------
        private void tlbCSVTiles_DragOver(object sender, DragEventArgs e) {
            // Reset
            e.Effect = DragDropEffects.None;

            // Hit info
            TileControlHitInfo hitInfo = tlbCSVTiles.CalcHitInfo(tlbCSVTiles.PointToClient(new Point(e.X, e.Y)));
            if (hitInfo.InItem) {
                string csvFilename = DraggedCSVFile(e.Data);
                if (csvFilename != null)
                    e.Effect = DragDropEffects.Link;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void tlbCSVTiles_DragDrop(object sender, DragEventArgs e) {
            // Hit info
            TileControlHitInfo hitInfo = tlbCSVTiles.CalcHitInfo(tlbCSVTiles.PointToClient(new Point(e.X, e.Y)));
            if (hitInfo.InItem) {
                // Dragged file
                string filePath = DraggedCSVFile(e.Data);
                if (filePath == null)
                    return;

                // Load CSV
                LoadCSV(hitInfo.ItemInfo.Item, filePath);
            }
        }

        //--------------------------------------------------------------------------------
        private string DraggedCSVFile(IDataObject data) {
            // Checks
            if (!data.GetDataPresent(DataFormats.FileDrop))
                return null;

            // Files
            string[] filePaths = (string[])data.GetData(DataFormats.FileDrop);
            foreach (string f in filePaths) {
                if (Path.GetExtension(f).ToLower().Equals(".csv"))
                    return f;
            }

            // Return
            return null;
        }


        // CONTROLS - MENU ================================================================================
        //--------------------------------------------------------------------------------
        private void btnMainMenu_File_OpenLeftCSV_ItemClick(object sender, ItemClickEventArgs e) {
            if (dlgLoadCSV.ShowDialog() != DialogResult.OK)
                return;
            LoadCSV(tbiLeftCSV, dlgLoadCSV.FileName);
        }
        
        //--------------------------------------------------------------------------------
        private void btnMainMenu_File_OpenRightCSV_ItemClick(object sender, ItemClickEventArgs e) {
            if (dlgLoadCSV.ShowDialog() != DialogResult.OK)
                return;
            LoadCSV(tbiRightCSV, dlgLoadCSV.FileName);
        }

        //--------------------------------------------------------------------------------
        private void btnMainMenu_File_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            // Prompt
            FlyoutAction flyoutAction = new FlyoutAction() { Caption = "Exit?", Description = "Exit the application?" };
            flyoutAction.Commands.Add(new FlyoutCommand() { Text = "Yes", Result = DialogResult.Yes });
            flyoutAction.Commands.Add(new FlyoutCommand() { Text = "No", Result = DialogResult.No });

            FlyoutProperties flyoutProperties = new FlyoutProperties() { Style = FlyoutStyle.MessageBox, Margin = new Padding(100) };
            if (FlyoutDialog.Show(this, flyoutAction, flyoutProperties) != DialogResult.Yes)
                return;
                
            // Close
            Close();
        }
        
        //--------------------------------------------------------------------------------
        private void barMainMenu_Options_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }
        
        //--------------------------------------------------------------------------------
        private void barMainMenu_Licence_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }
        
        //--------------------------------------------------------------------------------
        private void btnMainMenu_Help_ItemClick(object sender, ItemClickEventArgs e) {
            ShowAlert("This is a test alert that is very long and lengthy to test the cut off limit at the right edge.", grdMappings);
        }
        
        //--------------------------------------------------------------------------------        
        private void barMainMenu_About_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }


        // CSV ================================================================================
        //--------------------------------------------------------------------------------
        private void LoadCSV(TileItem tile, string filePath) {
            // File path
            if (tile == tbiLeftCSV)
                mLeftCSVFilePath = filePath;
            else if (tile == tbiRightCSV)
                mRightCSVFilePath = filePath;

            // Variables
            CSVReader csv = null;

            // Load
            try {
                // CSV
                csv = new CSVReader(filePath);

                // Tile
                tile.Elements[1].Text = Path.GetFileName(filePath);
                tile.Elements[2].Text = $"{csv.RowCount} rows";
                tile.Elements[3].Text = $"{string.Format("{0:n0}", (decimal)(new FileInfo(filePath).Length) / 1024.0m)} KB";

                // Columns
                LoadCSVColumns(tile, csv);
                RefreshColumns();

                // Mappings
                grdMappings.RefreshDataSource();

                //need to clear all indexes then refresh
                //also need to remove columns from left and right columns that are in mappings
                //comparison: csv.ReadColumns([matchColumns]) - reads every row value for the specified columns

                // Recent
                CSAWin.ApplicationData.AddRecentFile("csv", filePath);
            }
            catch (Exception e) {
                XtraMessageBox.Show(this, e.Message, "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                if (csv != null)
                    csv.Dispose();
            }
        }

        //--------------------------------------------------------------------------------
        private void LoadCSVColumns(TileItem tile, CSVReader csv) {
            // List
            BindingList<CSVColumn> columns = (tile == tbiLeftCSV ? mLeftColumns : mRightColumns);

            // Columns
            columns.Clear();
            for (int i = 0; i < csv.HeaderIndices.Count(); ++i) {
                columns.Add(new CSVColumn(csv.CasedHeader(i)));
            }

            // Refresh
            RefreshMappingControls();
        }
        

        // COLUMNS ================================================================================
        //--------------------------------------------------------------------------------
        private void RefreshColumns() {
            // Columns
            foreach (CSVColumn c in mLeftColumns) {
                c.Visible = true;
            }

            foreach (CSVColumn c in mRightColumns) {
                c.Visible = true;
            }

            // Mappings
            foreach (CSVMapping m in mCSVMappingManager.CSVMappings) {
                CSVColumn leftColumn = LeftColumn(m.LeftColumn.Name);
                if (leftColumn != null)
                    leftColumn.Visible = false;

                CSVColumn rightColumn = RightColumn(m.RightColumn.Name);
                if (rightColumn != null)
                    rightColumn.Visible = false;
            }

            // Refresh
            grdLeftColumns.RefreshDataSource();
            grdRightColumns.RefreshDataSource();
            grdMappings.Refresh();
            RefreshMappingControls();
        }

        //--------------------------------------------------------------------------------
        private CSVColumn LeftColumn(string column) {
            foreach (CSVColumn c in mLeftColumns) {
                if (c.Name.Equals(column))
                    return c;
            }
            return null;
        }

        //--------------------------------------------------------------------------------
        private bool HasLeftColumn(string column) { return LeftColumn(column) != null; }

        //--------------------------------------------------------------------------------
        private CSVColumn RightColumn(string column) {
            foreach (CSVColumn c in mRightColumns) {
                if (c.Name.Equals(column))
                    return c;
            }
            return null;
        }

        //--------------------------------------------------------------------------------
        private bool HasRightColumn(string column) { return RightColumn(column) != null; }

        //--------------------------------------------------------------------------------
        private bool HasAllColumns {
            get {
                foreach (CSVMapping m in mCSVMappingManager.CSVMappings) {
                    if (!HasLeftColumn(m.LeftColumn.Name) || !HasRightColumn(m.RightColumn.Name))
                        return false;
                }
                return true;
            }
        }


        // MAPPINGS - CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnAddMapping_Click(object sender, EventArgs e) {
            // Checks
            if (grvLeftColumns.SelectedRowsCount != 1/* || grvRightColumns.SelectedRowsCount != 0*/)
                return;

            // Add mapping
            AddMapping((CSVColumn)grvLeftColumns.GetRow(grvLeftColumns.GetSelectedRows().First()), (CSVColumn)grvRightColumns.GetRow(grvRightColumns.GetSelectedRows().First()));
        }
        
        //--------------------------------------------------------------------------------
        private void AddMapping(CSVColumn left, CSVColumn right, bool refresh = true) {
            // Mapping
            mCSVMappingManager.CSVMappings.Add(new CSVMapping(false, true, left, right));

            // Remove columns
            left.Visible = false;
            right.Visible = false;

            // Refresh
            if (refresh) {
                grdLeftColumns.RefreshDataSource();
                grdRightColumns.RefreshDataSource();
                RefreshMappingControls();
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnRemoveMapping_Click(object sender, EventArgs e) { RemoveMappings(); }

        //--------------------------------------------------------------------------------
        private void RemoveMappings(bool prompt = true) {
            // Checks
            if (grvMappings.SelectedRowsCount == 0)
                return;

            // Prompt
            if (prompt && XtraMessageBox.Show("Remove the selected mappings?", "CSV Comparer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Add columns
            foreach (int r in grvMappings.GetSelectedRows()) {
                CSVMapping mapping = (CSVMapping)grvMappings.GetRow(r);

                foreach (CSVColumn c in mLeftColumns) {
                    if (c.Name.Equals(mapping.LeftColumn.Name)) {
                        c.Visible = true;
                        break;
                    }
                }
                
                foreach (CSVColumn c in mRightColumns) {
                    if (c.Name.Equals(mapping.RightColumn.Name)) {
                        c.Visible = true;
                        break;
                    }
                }
            }

            // Remove mappings
            grvMappings.DeleteSelectedRows();
            grdLeftColumns.RefreshDataSource();
            grdRightColumns.RefreshDataSource();

            // Refresh
            RefreshMappingControls();
        }
        
        //--------------------------------------------------------------------------------
        private void btnAutomap_Click(object sender, EventArgs e) {
            // Checks
            if (grvLeftColumns.RowCount == 0 || grvRightColumns.RowCount == 0)
                return;

            // Variables
            Dictionary<CSVColumn, Tuple<double, CSVColumn>> mappings = new Dictionary<CSVColumn, Tuple<double, CSVColumn>>();

            // Automap
            foreach (CSVColumn l in (from c in mLeftColumns where c.Visible select c)) {
                double bestScore = double.MaxValue;
                CSVColumn matchingColumn = null;

                // Compare
                foreach (CSVColumn r in (from c in mRightColumns where c.Visible select c)) {
                    double score = UString.DistanceJaroWrinkler(l.Name, r.Name);
                    if (score < bestScore) {
                        bestScore = score;
                        matchingColumn = r;
                    }
                }

                // Add mapping
                if (!mappings.ContainsKey(matchingColumn))
                    mappings.Add(matchingColumn, new Tuple<double, CSVColumn>(bestScore, l));
                else if (bestScore < mappings[matchingColumn].Item1)
                    mappings[matchingColumn] = new Tuple<double, CSVColumn>(bestScore, l);
                Debug.WriteLine($"'{l}' = '{matchingColumn}'  ({bestScore})");
            }

            // Apply mappings
            foreach (KeyValuePair<CSVColumn, Tuple<double, CSVColumn>> m in mappings) {
                AddMapping(m.Value.Item2, m.Key);
            }

            // Refresh
            grdLeftColumns.RefreshDataSource();
            grdRightColumns.RefreshDataSource();
            RefreshMappingControls();
        }
        
        //--------------------------------------------------------------------------------
        private void btnClearMappings_Click(object sender, EventArgs e) {
            // Prompt
            if (XtraMessageBox.Show("Remove all mappings?", "CSV Comparer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Clear
            grvMappings.SelectAll();
            RemoveMappings(false);
        }

        //--------------------------------------------------------------------------------
        private void RefreshMappingControls() {
            btnAddMapping.Enabled = (grvLeftColumns.RowCount > 0 && grvRightColumns.RowCount > 0);
            btnRemoveMapping.Enabled = (mCSVMappingManager.CSVMappings.Count > 0);
            btnAutomap.Enabled = (grvLeftColumns.RowCount > 0 && grvRightColumns.RowCount > 0) || (mCSVMappingManager.CSVMappings.Count > 0);
            btnClearMappings.Enabled = (grvLeftColumns.RowCount > 0 && grvRightColumns.RowCount > 0) || (mCSVMappingManager.CSVMappings.Count > 0);
            btnSaveMappings.Enabled = (mCSVMappingManager.CSVMappings.Count > 0);
        }

        
        // MAPPINGS - GRID ================================================================================
        //--------------------------------------------------------------------------------
        private void grvMappings_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e) {
            // Mapping
            CSVMapping mapping = (CSVMapping)grvMappings.GetRow(e.RowHandle);

            // Columns
            bool hasLeftColumn = HasLeftColumn(mapping.LeftColumn.Name);
            bool hasRightColumn = HasRightColumn(mapping.RightColumn.Name);

            // Row
            if (!hasLeftColumn || !hasRightColumn) {
                if ((e.Column == grdMappings_LeftColumn && !hasLeftColumn) || (e.Column == grdMappings_RightColumn && !hasRightColumn)) {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                }
                else
                    e.Appearance.ForeColor = Color.FromArgb(255, 127, 127);
            }
        }

        //--------------------------------------------------------------------------------
        private void grvMappings_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
            if (e.Column == grdMappings_MergeRule) {
                CSVMapping mapping = (CSVMapping)grvMappings.GetRow(e.RowHandle);
                mPreviousMergeRule = mapping.MergeRule;
            }
        }
        
        //--------------------------------------------------------------------------------
        private void grvMappings_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) {
            if (e.Column == grdMappings_Type) {
                // Type
                CSVMapping mapping = (CSVMapping)grvMappings.GetRow(e.RowHandle);
                if (mapping.RestrictMergeRuleByType())
                    ShowAlert($"Merge rule set to '{mapping.MergeRule}' due to type change.", grdMappings);
            }
            else if (e.Column == grdMappings_MergeRule) {
                // Merge rule
                CSVMapping mapping = (CSVMapping)grvMappings.GetRow(e.RowHandle);
                if (!mapping.MergeRuleAllowedForType(mapping.MergeRule)) {
                    ShowAlert($"Merge rule '{mapping.MergeRule}' not allowed with type.", grdMappings);
                    mapping.MergeRule = mPreviousMergeRule;
                }
            }
        }
        
        //--------------------------------------------------------------------------------
        private void btnMappings_Settings_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            // Checks
            if (grvMappings.SelectedRowsCount == 0)
                return;

            // Mapping
            CSVMapping mapping = (CSVMapping)grvMappings.GetRow(grvMappings.GetSelectedRows().First());
            MappingForm mappingForm = new MappingForm(mapping);
            if (mappingForm.ShowDialog() == DialogResult.OK)
                grdMappings.RefreshDataSource();
        }
        
        //--------------------------------------------------------------------------------
        private void btnMappings_Remove_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) { RemoveMappings(); }
        

        // MAPPINGS - SAVING / LOADING ================================================================================
        //--------------------------------------------------------------------------------
        private void btnSaveMappings_Click(object sender, EventArgs e) {
            // Browse
            if (dlgSaveMappings.ShowDialog() != DialogResult.OK)
                return;

            // Save
            mCSVMappingManager.SaveJSON(dlgSaveMappings.FileName);

            // Recent
            CSAWin.ApplicationData.AddRecentFile("mapping", dlgSaveMappings.FileName);
        }

        //--------------------------------------------------------------------------------
        private void btnLoadMappings_Click(object sender, EventArgs e) {
            if (dlgLoadMappings.ShowDialog() != DialogResult.OK)
                return;
            LoadMappings(dlgLoadMappings.FileName);
        }

        //--------------------------------------------------------------------------------
        private void LoadMappings(string filename) {
            // Load
            mCSVMappingManager.LoadJSON(filename);
            RefreshColumns();

            // Recent
            CSAWin.ApplicationData.AddRecentFile("mapping", filename);
        }

        //--------------------------------------------------------------------------------
        private void mnuRecentMappings_BeforePopup(object sender, CancelEventArgs e) {
            // Clear items
            mnuRecentMappings.ItemLinks.Clear();

            // Recent mappings
            int i = 0;
            foreach (string f in CSAWin.ApplicationData.RecentFiles("mapping")) {
                if (i >= 10)
                    break;
                BarButtonItem item = new BarButtonItem();
                item.Caption = $"{++i}  " + f;
                item.ItemClick += (object s, ItemClickEventArgs args) => { LoadMappings(f); };
                mnuRecentMappings.AddItem(item);
            }
        }


        // PROGRESS FORM ================================================================================
        //--------------------------------------------------------------------------------
        private void CreateProgressForm(string description = "") {
            // Complete existing
            CompleteProgressForm(description);

            // Create
            mProgressForm = new ProgressForm();
            mProgressForm.Progress = 0;
            mProgressForm.ProgressMaximum = 100;
            mProgressForm.Description = description;
            mProgressForm.ItemProgress = "";
            mProgressForm.TimeRemaining = TimeSpan.MaxValue;

            // Task bar progress
            prgTaskBarProgress.Position = mProgressForm.Progress;
            prgTaskBarProgress.Properties.Maximum = mProgressForm.ProgressMaximum;
            prgTaskBarProgress.ShowProgressInTaskBar = true;
        }
        
        //--------------------------------------------------------------------------------
        private DialogResult ShowProgressForm() {
            return mProgressForm.ShowDialog();
        }
        
        //--------------------------------------------------------------------------------
        private void UpdateProgressForm(string description, int? progress, int? progressMaximum, string itemProgress, TimeSpan timeRemaining) {
            // Progress form
            mProgressForm.Description = description;
            if (progress != null)
                mProgressForm.Progress = (int)progress;
            if (progressMaximum != null)
                mProgressForm.ProgressMaximum = (int)progressMaximum;
            mProgressForm.ItemProgress = itemProgress;
            mProgressForm.TimeRemaining = timeRemaining;
            
            // Task bar progress
            prgTaskBarProgress.Position = mProgressForm.Progress;
            prgTaskBarProgress.Properties.Maximum = mProgressForm.ProgressMaximum;
        }
        
        //--------------------------------------------------------------------------------
        private void CompleteProgressForm(string description) {
            if (mProgressForm != null) {
                // Update
                mProgressForm.Description = description;
                mProgressForm.Progress = mProgressForm.ProgressMaximum;
                mProgressForm.TimeRemaining = TimeSpan.Zero;
                
                // Task bar progress
                prgTaskBarProgress.ShowProgressInTaskBar = false;

                // Complete
                mProgressForm.SetCompleted();
                mProgressForm = null;
            }
        }

        //--------------------------------------------------------------------------------
        private void CloseProgressForm() {
            if (mProgressForm != null) {
                mProgressForm.Close();
                mProgressForm = null;
                prgTaskBarProgress.ShowProgressInTaskBar = false;
            }
        }

        
        // ALERTS ================================================================================
        //--------------------------------------------------------------------------------
        private void ShowAlert(string alert, Control anchor = null) {
            // Hide previous alerts
            if (mAlert != null) {
                mAlert.form.Hide();
                mAlert = null;
            }

            // Show
            alcAlerts.Show(this, "", alert);

            foreach (AlertForm f in alcAlerts.AlertFormList) {
                if (f.Visible) {
                    mAlert = new Alert(f, this, anchor);
                    mAlert.UpdateLayout();
                }
            }
        }


        // THEME ================================================================================
        //--------------------------------------------------------------------------------
        public string Theme {
            set {
                mTheme = value;
                UserLookAndFeel.Default.SetSkinStyle(DevExpressThemeName(mTheme));
                UpdateTheme();
            }
            get { return mTheme; }
        }
        
        //--------------------------------------------------------------------------------
        private string DevExpressThemeName(string theme) {
            switch (theme) {
                case "Black":       return "Office 2016 Black";
                case "Grey":        return "Office 2016 Dark";
                case "White":       return "Office 2016 Colorful";
                default:            return theme;
            }
        }

        //--------------------------------------------------------------------------------
        private void UpdateTheme() {
            // Tiles - background colours
            float backgroundBrightness = grvMappings.PaintAppearance.Empty.BackColor.GetBrightness() * 255.0f;
            tlbCSVTiles.AppearanceItem.Normal.BackColor = Color.FromArgb((int)Math.Round(40.139f + 0.4700461f * backgroundBrightness),     // 41.383f + 0.7004608f
                                                                         (int)Math.Round(41.383f + 0.4700461f * backgroundBrightness),     // 41.383f + 0.7004608f
                                                                         (int)Math.Round(41.383f + 0.4700461f * backgroundBrightness));    // 41.383f + 0.7004608f
            tlbCSVTiles.AppearanceItem.Normal.BackColor2 = Color.FromArgb((int)Math.Round(50.139f + 0.4700461f * backgroundBrightness),    // 31.383f + 0.7004608f
                                                                          (int)Math.Round(50.139f + 0.4700461f * backgroundBrightness),    // 31.383f + 0.7004608f
                                                                          (int)Math.Round(50.139f + 0.4700461f * backgroundBrightness));   // 31.383f + 0.7004608f
            tlbCSVTiles.AppearanceItem.Normal.BorderColor = Color.FromArgb((int)Math.Round(103.199f + 0.0737327f * backgroundBrightness),  // 94.443f + 0.3041475f
                                                                           (int)Math.Round(103.199f + 0.0737327f * backgroundBrightness),  // 94.443f + 0.3041475f
                                                                           (int)Math.Round(103.199f + 0.0737327f * backgroundBrightness)); // 94.443f + 0.3041475f

            // Tiles - text colour
            tlbCSVTiles.AppearanceItem.Normal.ForeColor = grvMappings.PaintAppearance.Row.ForeColor;

            // Tiles - item colours
            tbiLeftCSV.Elements[0].ImageOptions.SvgImageColorizationMode = (Theme.Equals("Black") ? SvgImageColorizationMode.Default : SvgImageColorizationMode.None);
            tbiRightCSV.Elements[0].ImageOptions.SvgImageColorizationMode = (Theme.Equals("Black") ? SvgImageColorizationMode.Default : SvgImageColorizationMode.None);
            
            // Tiles - button colours
            tlbCSVTiles.ContextButtons[0].ImageOptionsCollection["ItemNormal"].SvgImageColorizationMode = (Theme.Equals("Black") ? SvgImageColorizationMode.Full : SvgImageColorizationMode.None);
            tlbCSVTiles.ContextButtons[1].ImageOptionsCollection["ItemNormal"].SvgImageColorizationMode = (Theme.Equals("Black") ? SvgImageColorizationMode.Full : SvgImageColorizationMode.None);
        }

        
        //================================================================================
        //********************************************************************************
        class Alert {
            public AlertForm form;
            public Form anchorForm;
            public Control anchorControl;

            public Alert(AlertForm form, Form anchorForm, Control anchorControl = null) {
                this.form = form;
                this.anchorForm = anchorForm;
                this.anchorControl = anchorControl;
            }

            public void UpdateLayout() {
                form.Width = (anchorControl == null ? anchorForm.Width : anchorControl.Width);
                form.Height = 50;
                form.Location = new Point(anchorForm.Location.X + (anchorControl != null ? anchorControl.Location.X + 1: 0),
                                          anchorForm.Location.Y + (anchorControl != null ? anchorControl.Bottom + 9 : 0));
            }
        }

        //********************************************************************************
        class CompareArgs {
            public string leftCSVFilePath;
            public string rightCSVFilePath;
            public List<CSVMapping> mappings;

            public CompareArgs(string leftCSVFilePath, string rightCSVFilePath, List<CSVMapping> mappings) {
                this.leftCSVFilePath = leftCSVFilePath;
                this.rightCSVFilePath = rightCSVFilePath;
                this.mappings = mappings;
            }
        }

        //********************************************************************************
        class MergeArgs {
            public string leftCSVFilePath;
            public string rightCSVFilePath;
            public List<CSVMapping> mappings;
            public string outputPath;

            public MergeArgs(string leftCSVFilePath, string rightCSVFilePath, List<CSVMapping> mappings, string outputPath) {
                this.leftCSVFilePath = leftCSVFilePath;
                this.rightCSVFilePath = rightCSVFilePath;
                this.mappings = mappings;
                this.outputPath = outputPath;
            }
        }

        //********************************************************************************
        class CompareMergeProgress {
            public int? progress = null;
            public int? progressMaximum = null;
            public string stage;
            public string rowProgress;
            public TimeSpan timeRemaining;

            public CompareMergeProgress(int? progress, int? progressMaximum, string stage, int row, int rowCount, TimeSpan timeRemaining) {
                this.progress = progress;
                this.progressMaximum = progressMaximum;
                this.stage = stage;
                this.rowProgress = row + " of " + rowCount;
                this.timeRemaining = timeRemaining;
            }
        }

        //********************************************************************************
        class CompareMergeResults {
            public Exception exception = null;
            public List<CSVCompareMergeIssue> issues = new List<CSVCompareMergeIssue>();
        }
    }

}
