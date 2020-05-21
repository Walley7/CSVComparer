namespace CSVComparer.Forms {
    partial class CompareResultsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareResultsForm));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.grdIssues_IssueType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdIssues_LeftRow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdIssues = new DevExpress.XtraGrid.GridControl();
            this.mnuIssues = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuIssues_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.grvIssues = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdIssues_RightRow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnMappings_Settings = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnMappings_Remove = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cboMappings_Type = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.cboMappings_MergeRule = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.memDetails = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.brmBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dlgExportCSV = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.svgImages = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).BeginInit();
            this.mnuIssues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvIssues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMappings_Settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMappings_Remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMappings_Type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMappings_MergeRule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brmBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImages)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIssues_IssueType
            // 
            this.grdIssues_IssueType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.grdIssues_IssueType.AppearanceCell.Options.UseFont = true;
            this.grdIssues_IssueType.Caption = "Issue Type";
            this.grdIssues_IssueType.FieldName = "TypeString";
            this.grdIssues_IssueType.MinWidth = 100;
            this.grdIssues_IssueType.Name = "grdIssues_IssueType";
            this.grdIssues_IssueType.Visible = true;
            this.grdIssues_IssueType.VisibleIndex = 2;
            this.grdIssues_IssueType.Width = 1130;
            // 
            // grdIssues_LeftRow
            // 
            this.grdIssues_LeftRow.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdIssues_LeftRow.AppearanceCell.Options.UseFont = true;
            this.grdIssues_LeftRow.AppearanceCell.Options.UseTextOptions = true;
            this.grdIssues_LeftRow.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.grdIssues_LeftRow.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(229)))), ((int)(((byte)(117)))));
            this.grdIssues_LeftRow.AppearanceHeader.Options.UseBackColor = true;
            this.grdIssues_LeftRow.Caption = "< Row #";
            this.grdIssues_LeftRow.FieldName = "LeftRow";
            this.grdIssues_LeftRow.MaxWidth = 70;
            this.grdIssues_LeftRow.MinWidth = 70;
            this.grdIssues_LeftRow.Name = "grdIssues_LeftRow";
            this.grdIssues_LeftRow.Visible = true;
            this.grdIssues_LeftRow.VisibleIndex = 0;
            this.grdIssues_LeftRow.Width = 70;
            // 
            // gridColumn17
            // 
            this.gridColumn17.MaxWidth = 25;
            this.gridColumn17.MinWidth = 25;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Width = 25;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Options";
            this.gridColumn14.FieldName = "OptionsString";
            this.gridColumn14.MinWidth = 60;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.Width = 423;
            // 
            // grdIssues
            // 
            this.grdIssues.ContextMenuStrip = this.mnuIssues;
            this.grdIssues.Location = new System.Drawing.Point(7, 7);
            this.grdIssues.MainView = this.grvIssues;
            this.grdIssues.Name = "grdIssues";
            this.grdIssues.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.btnMappings_Settings,
            this.btnMappings_Remove,
            this.cboMappings_Type,
            this.cboMappings_MergeRule});
            this.grdIssues.Size = new System.Drawing.Size(1184, 435);
            this.grdIssues.TabIndex = 29;
            this.grdIssues.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvIssues});
            // 
            // mnuIssues
            // 
            this.mnuIssues.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuIssues_Copy});
            this.mnuIssues.Name = "mnuIssues";
            this.mnuIssues.Size = new System.Drawing.Size(103, 26);
            this.mnuIssues.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuIssues_ItemClicked);
            // 
            // mnuIssues_Copy
            // 
            this.mnuIssues_Copy.Name = "mnuIssues_Copy";
            this.mnuIssues_Copy.Size = new System.Drawing.Size(102, 22);
            this.mnuIssues_Copy.Text = "Copy";
            // 
            // grvIssues
            // 
            this.grvIssues.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.grdIssues_LeftRow,
            this.grdIssues_RightRow,
            this.grdIssues_IssueType});
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.grdIssues_IssueType;
            gridFormatRule1.Name = "FormatLinkingIssue";
            formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(180)))), ((int)(((byte)(27)))));
            formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "Linking Failure";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.grvIssues.FormatRules.Add(gridFormatRule1);
            this.grvIssues.GridControl = this.grdIssues;
            this.grvIssues.IndicatorWidth = 50;
            this.grvIssues.Name = "grvIssues";
            this.grvIssues.OptionsBehavior.Editable = false;
            this.grvIssues.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvIssues.OptionsCustomization.AllowColumnMoving = false;
            this.grvIssues.OptionsCustomization.AllowQuickHideColumns = false;
            this.grvIssues.OptionsDetail.EnableMasterViewMode = false;
            this.grvIssues.OptionsSelection.MultiSelect = true;
            this.grvIssues.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvIssues.OptionsView.ShowGroupPanel = false;
            this.grvIssues.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grvIssues_CustomDrawRowIndicator);
            this.grvIssues.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvIssues_CustomDrawCell);
            this.grvIssues.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvIssues_FocusedRowChanged);
            this.grvIssues.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.grvIssues_CustomUnboundColumnData);
            // 
            // grdIssues_RightRow
            // 
            this.grdIssues_RightRow.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grdIssues_RightRow.AppearanceCell.Options.UseFont = true;
            this.grdIssues_RightRow.AppearanceCell.Options.UseTextOptions = true;
            this.grdIssues_RightRow.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.grdIssues_RightRow.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(184)))), ((int)(((byte)(254)))));
            this.grdIssues_RightRow.AppearanceHeader.Options.UseBackColor = true;
            this.grdIssues_RightRow.Caption = "> Row #";
            this.grdIssues_RightRow.FieldName = "RightRow";
            this.grdIssues_RightRow.MaxWidth = 70;
            this.grdIssues_RightRow.MinWidth = 70;
            this.grdIssues_RightRow.Name = "grdIssues_RightRow";
            this.grdIssues_RightRow.Visible = true;
            this.grdIssues_RightRow.VisibleIndex = 1;
            this.grdIssues_RightRow.Width = 70;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom;
            this.repositoryItemCheckEdit1.ImageOptions.ImageChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEdit1.ImageOptions.ImageChecked")));
            this.repositoryItemCheckEdit1.ImageOptions.ImageIndexChecked = 0;
            this.repositoryItemCheckEdit1.ImageOptions.ImageIndexGrayed = 0;
            this.repositoryItemCheckEdit1.ImageOptions.ImageIndexUnchecked = 0;
            this.repositoryItemCheckEdit1.ImageOptions.SvgImageChecked = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("repositoryItemCheckEdit1.ImageOptions.SvgImageChecked")));
            this.repositoryItemCheckEdit1.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // btnMappings_Settings
            // 
            this.btnMappings_Settings.AutoHeight = false;
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(14, 14);
            this.btnMappings_Settings.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnMappings_Settings.Name = "btnMappings_Settings";
            this.btnMappings_Settings.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btnMappings_Remove
            // 
            this.btnMappings_Remove.AutoHeight = false;
            editorButtonImageOptions2.SvgImageSize = new System.Drawing.Size(14, 14);
            this.btnMappings_Remove.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnMappings_Remove.Name = "btnMappings_Remove";
            this.btnMappings_Remove.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // cboMappings_Type
            // 
            this.cboMappings_Type.AutoHeight = false;
            this.cboMappings_Type.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMappings_Type.Name = "cboMappings_Type";
            this.cboMappings_Type.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // cboMappings_MergeRule
            // 
            this.cboMappings_MergeRule.AutoHeight = false;
            this.cboMappings_MergeRule.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMappings_MergeRule.Name = "cboMappings_MergeRule";
            this.cboMappings_MergeRule.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // memDetails
            // 
            this.memDetails.EditValue = "";
            this.memDetails.Location = new System.Drawing.Point(7, 466);
            this.memDetails.Name = "memDetails";
            this.memDetails.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.memDetails.Properties.Appearance.Options.UseFont = true;
            this.memDetails.Properties.ReadOnly = true;
            this.memDetails.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memDetails.Properties.WordWrap = false;
            this.memDetails.Size = new System.Drawing.Size(1184, 162);
            this.memDetails.StyleController = this.layoutControl1;
            this.memDetails.TabIndex = 31;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnExportCSV);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.grdIssues);
            this.layoutControl1.Controls.Add(this.memDetails);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1198, 669);
            this.layoutControl1.TabIndex = 43;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(891, 632);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(146, 30);
            this.btnExportCSV.StyleController = this.layoutControl1;
            this.btnExportCSV.TabIndex = 38;
            this.btnExportCSV.Text = "Export CSV";
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(1041, 632);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 30);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1198, 669);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdIssues;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1188, 439);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.memDetails;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 439);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(1188, 186);
            this.layoutControlItem2.Text = "Details";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(39, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 625);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(884, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.Location = new System.Drawing.Point(1034, 625);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(154, 34);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(154, 34);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnExportCSV;
            this.layoutControlItem4.Location = new System.Drawing.Point(884, 625);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(150, 34);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(150, 34);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // brmBarManager
            // 
            this.brmBarManager.DockControls.Add(this.barDockControlTop);
            this.brmBarManager.DockControls.Add(this.barDockControlBottom);
            this.brmBarManager.DockControls.Add(this.barDockControlLeft);
            this.brmBarManager.DockControls.Add(this.barDockControlRight);
            this.brmBarManager.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.brmBarManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1198, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 669);
            this.barDockControlBottom.Manager = this.brmBarManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1198, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.brmBarManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 669);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1198, 0);
            this.barDockControlRight.Manager = this.brmBarManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 669);
            // 
            // dlgExportCSV
            // 
            this.dlgExportCSV.Filter = "CSV (*.csv)|*.csv";
            // 
            // svgImages
            // 
            this.svgImages.Add("security_key", "image://svgimages/icon builder/security_key.svg");
            // 
            // CompareResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 669);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(600, 398);
            this.Name = "CompareResultsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSV Comparer - Comparison Results";
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).EndInit();
            this.mnuIssues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvIssues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMappings_Settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMappings_Remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMappings_Type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMappings_MergeRule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brmBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.GridControl grdIssues;
        private DevExpress.XtraGrid.Views.Grid.GridView grvIssues;
        private DevExpress.XtraGrid.Columns.GridColumn grdIssues_LeftRow;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn grdIssues_IssueType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboMappings_Type;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboMappings_MergeRule;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnMappings_Settings;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnMappings_Remove;
        private DevExpress.XtraGrid.Columns.GridColumn grdIssues_RightRow;
        private DevExpress.XtraEditors.MemoEdit memDetails;
        private DevExpress.XtraBars.BarManager brmBarManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ContextMenuStrip mnuIssues;
        private System.Windows.Forms.ToolStripMenuItem mnuIssues_Copy;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraEditors.XtraSaveFileDialog dlgExportCSV;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.Utils.SvgImageCollection svgImages;
    }
}