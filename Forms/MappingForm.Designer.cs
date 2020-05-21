namespace CSVComparer.Forms {
    partial class MappingForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingForm));
            this.chkIgnoreCase = new DevExpress.XtraEditors.CheckEdit();
            this.chkIgnoreSymbols = new DevExpress.XtraEditors.CheckEdit();
            this.chkIgnoreWhitespace = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAllowedSymbols = new DevExpress.XtraEditors.TextEdit();
            this.spnDecimalPrecision = new DevExpress.XtraEditors.SpinEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtLeftColumn = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRightColumn = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboMergeRule = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.chkDecimalPrecision = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.chkRemoveCase = new DevExpress.XtraEditors.CheckEdit();
            this.txtPrefix = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtSuffix = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.grdForcedMatchings = new DevExpress.XtraGrid.GridControl();
            this.grvForcedMatchings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnAddForcedMatching = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveForcedMatching = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.chkIgnoreCase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIgnoreSymbols.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIgnoreWhitespace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllowedSymbols.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDecimalPrecision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeftColumn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRightColumn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMergeRule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDecimalPrecision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemoveCase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSuffix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdForcedMatchings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvForcedMatchings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIgnoreCase
            // 
            this.chkIgnoreCase.Location = new System.Drawing.Point(115, 121);
            this.chkIgnoreCase.Name = "chkIgnoreCase";
            this.chkIgnoreCase.Properties.Caption = " Ignore Case";
            this.chkIgnoreCase.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkIgnoreCase.Size = new System.Drawing.Size(123, 19);
            this.chkIgnoreCase.TabIndex = 0;
            // 
            // chkIgnoreSymbols
            // 
            this.chkIgnoreSymbols.Location = new System.Drawing.Point(225, 121);
            this.chkIgnoreSymbols.Name = "chkIgnoreSymbols";
            this.chkIgnoreSymbols.Properties.Caption = " Ignore Symbols";
            this.chkIgnoreSymbols.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkIgnoreSymbols.Size = new System.Drawing.Size(123, 19);
            this.chkIgnoreSymbols.TabIndex = 1;
            // 
            // chkIgnoreWhitespace
            // 
            this.chkIgnoreWhitespace.Location = new System.Drawing.Point(335, 121);
            this.chkIgnoreWhitespace.Name = "chkIgnoreWhitespace";
            this.chkIgnoreWhitespace.Properties.Caption = " Ignore Whitespace";
            this.chkIgnoreWhitespace.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkIgnoreWhitespace.Size = new System.Drawing.Size(123, 19);
            this.chkIgnoreWhitespace.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 149);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(79, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Allowed Symbols";
            // 
            // txtAllowedSymbols
            // 
            this.txtAllowedSymbols.Location = new System.Drawing.Point(115, 146);
            this.txtAllowedSymbols.Name = "txtAllowedSymbols";
            this.txtAllowedSymbols.Size = new System.Drawing.Size(406, 20);
            this.txtAllowedSymbols.TabIndex = 5;
            this.txtAllowedSymbols.Leave += new System.EventHandler(this.txtAllowedSymbols_Leave);
            // 
            // spnDecimalPrecision
            // 
            this.spnDecimalPrecision.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnDecimalPrecision.Location = new System.Drawing.Point(139, 300);
            this.spnDecimalPrecision.Name = "spnDecimalPrecision";
            this.spnDecimalPrecision.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.spnDecimalPrecision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnDecimalPrecision.Properties.Mask.EditMask = "#0";
            this.spnDecimalPrecision.Properties.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.spnDecimalPrecision.Size = new System.Drawing.Size(100, 20);
            this.spnDecimalPrecision.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(365, 557);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(446, 557);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 304);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(81, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Decimal Precision";
            // 
            // txtLeftColumn
            // 
            this.txtLeftColumn.Location = new System.Drawing.Point(115, 12);
            this.txtLeftColumn.Name = "txtLeftColumn";
            this.txtLeftColumn.Properties.ReadOnly = true;
            this.txtLeftColumn.Size = new System.Drawing.Size(200, 20);
            this.txtLeftColumn.TabIndex = 11;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 13);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "Columns";
            // 
            // txtRightColumn
            // 
            this.txtRightColumn.Location = new System.Drawing.Point(321, 12);
            this.txtRightColumn.Name = "txtRightColumn";
            this.txtRightColumn.Properties.ReadOnly = true;
            this.txtRightColumn.Size = new System.Drawing.Size(200, 20);
            this.txtRightColumn.TabIndex = 13;
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Location = new System.Drawing.Point(0, 38);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(800, 2);
            this.labelControl4.TabIndex = 14;
            this.labelControl4.Text = "labelControl4";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 50);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Type";
            // 
            // cboType
            // 
            this.cboType.Location = new System.Drawing.Point(115, 47);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboType.Size = new System.Drawing.Size(200, 20);
            this.cboType.TabIndex = 16;
            this.cboType.SelectedValueChanged += new System.EventHandler(this.cboType_SelectedValueChanged);
            // 
            // cboMergeRule
            // 
            this.cboMergeRule.Location = new System.Drawing.Point(115, 73);
            this.cboMergeRule.Name = "cboMergeRule";
            this.cboMergeRule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMergeRule.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboMergeRule.Size = new System.Drawing.Size(200, 20);
            this.cboMergeRule.TabIndex = 17;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 76);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(54, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Merge Rule";
            // 
            // labelControl7
            // 
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl7.Location = new System.Drawing.Point(0, 99);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(800, 2);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "labelControl7";
            // 
            // chkDecimalPrecision
            // 
            this.chkDecimalPrecision.Location = new System.Drawing.Point(115, 301);
            this.chkDecimalPrecision.Name = "chkDecimalPrecision";
            this.chkDecimalPrecision.Properties.Caption = "";
            this.chkDecimalPrecision.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkDecimalPrecision.Size = new System.Drawing.Size(18, 19);
            this.chkDecimalPrecision.TabIndex = 20;
            this.chkDecimalPrecision.CheckedChanged += new System.EventHandler(this.chkDecimalPrecision_CheckedChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(12, 109);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(67, 13);
            this.labelControl8.TabIndex = 21;
            this.labelControl8.Text = "Comparison";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(12, 182);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(46, 13);
            this.labelControl9.TabIndex = 23;
            this.labelControl9.Text = "Merging";
            // 
            // labelControl10
            // 
            this.labelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl10.Location = new System.Drawing.Point(0, 172);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(800, 2);
            this.labelControl10.TabIndex = 22;
            this.labelControl10.Text = "labelControl10";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(12, 281);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(128, 13);
            this.labelControl11.TabIndex = 25;
            this.labelControl11.Text = "Comparison && Merging";
            // 
            // labelControl12
            // 
            this.labelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl12.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl12.Location = new System.Drawing.Point(0, 271);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(800, 2);
            this.labelControl12.TabIndex = 24;
            this.labelControl12.Text = "labelControl12";
            // 
            // chkRemoveCase
            // 
            this.chkRemoveCase.Location = new System.Drawing.Point(115, 194);
            this.chkRemoveCase.Name = "chkRemoveCase";
            this.chkRemoveCase.Properties.Caption = "Remove Case";
            this.chkRemoveCase.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkRemoveCase.Size = new System.Drawing.Size(123, 19);
            this.chkRemoveCase.TabIndex = 26;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(115, 219);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(406, 20);
            this.txtPrefix.TabIndex = 28;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(12, 222);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(28, 13);
            this.labelControl13.TabIndex = 27;
            this.labelControl13.Text = "Prefix";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(115, 245);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(406, 20);
            this.txtSuffix.TabIndex = 30;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(12, 248);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(28, 13);
            this.labelControl14.TabIndex = 29;
            this.labelControl14.Text = "Suffix";
            // 
            // grdForcedMatchings
            // 
            this.grdForcedMatchings.Location = new System.Drawing.Point(12, 355);
            this.grdForcedMatchings.MainView = this.grvForcedMatchings;
            this.grdForcedMatchings.Name = "grdForcedMatchings";
            this.grdForcedMatchings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grdForcedMatchings.Size = new System.Drawing.Size(509, 173);
            this.grdForcedMatchings.TabIndex = 31;
            this.grdForcedMatchings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvForcedMatchings});
            // 
            // grvForcedMatchings
            // 
            this.grvForcedMatchings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.grvForcedMatchings.GridControl = this.grdForcedMatchings;
            this.grvForcedMatchings.Name = "grvForcedMatchings";
            this.grvForcedMatchings.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grvForcedMatchings.OptionsCustomization.AllowColumnMoving = false;
            this.grvForcedMatchings.OptionsCustomization.AllowColumnResizing = false;
            this.grvForcedMatchings.OptionsCustomization.AllowGroup = false;
            this.grvForcedMatchings.OptionsSelection.MultiSelect = true;
            this.grvForcedMatchings.OptionsView.ShowGroupPanel = false;
            this.grvForcedMatchings.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "LeftValue";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "RightValue";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn3.FieldName = "ExactMatch";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // btnAddForcedMatching
            // 
            this.btnAddForcedMatching.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddForcedMatching.ImageOptions.Image")));
            this.btnAddForcedMatching.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAddForcedMatching.Location = new System.Drawing.Point(12, 532);
            this.btnAddForcedMatching.Name = "btnAddForcedMatching";
            this.btnAddForcedMatching.Size = new System.Drawing.Size(50, 24);
            this.btnAddForcedMatching.TabIndex = 32;
            this.btnAddForcedMatching.Click += new System.EventHandler(this.btnAddForcedMatching_Click);
            // 
            // btnRemoveForcedMatching
            // 
            this.btnRemoveForcedMatching.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveForcedMatching.ImageOptions.Image")));
            this.btnRemoveForcedMatching.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRemoveForcedMatching.Location = new System.Drawing.Point(66, 532);
            this.btnRemoveForcedMatching.Name = "btnRemoveForcedMatching";
            this.btnRemoveForcedMatching.Size = new System.Drawing.Size(50, 24);
            this.btnRemoveForcedMatching.TabIndex = 33;
            this.btnRemoveForcedMatching.Click += new System.EventHandler(this.btnRemoveForcedMatching_Click);
            // 
            // labelControl15
            // 
            this.labelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl15.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl15.Location = new System.Drawing.Point(0, 326);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(800, 2);
            this.labelControl15.TabIndex = 34;
            this.labelControl15.Text = "labelControl15";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(12, 336);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(99, 13);
            this.labelControl16.TabIndex = 35;
            this.labelControl16.Text = "Forced Matchings";
            // 
            // MappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 592);
            this.Controls.Add(this.labelControl16);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.btnRemoveForcedMatching);
            this.Controls.Add(this.btnAddForcedMatching);
            this.Controls.Add(this.grdForcedMatchings);
            this.Controls.Add(this.txtSuffix);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.chkRemoveCase);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.chkDecimalPrecision);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.cboMergeRule);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtRightColumn);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtLeftColumn);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.spnDecimalPrecision);
            this.Controls.Add(this.txtAllowedSymbols);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.chkIgnoreWhitespace);
            this.Controls.Add(this.chkIgnoreSymbols);
            this.Controls.Add(this.chkIgnoreCase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MappingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mapping";
            this.Load += new System.EventHandler(this.MappingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkIgnoreCase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIgnoreSymbols.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIgnoreWhitespace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAllowedSymbols.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDecimalPrecision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeftColumn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRightColumn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMergeRule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDecimalPrecision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemoveCase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSuffix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdForcedMatchings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvForcedMatchings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkIgnoreCase;
        private DevExpress.XtraEditors.CheckEdit chkIgnoreSymbols;
        private DevExpress.XtraEditors.CheckEdit chkIgnoreWhitespace;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAllowedSymbols;
        private DevExpress.XtraEditors.SpinEdit spnDecimalPrecision;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtLeftColumn;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtRightColumn;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cboType;
        private DevExpress.XtraEditors.ComboBoxEdit cboMergeRule;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CheckEdit chkDecimalPrecision;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.CheckEdit chkRemoveCase;
        private DevExpress.XtraEditors.TextEdit txtPrefix;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtSuffix;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraGrid.GridControl grdForcedMatchings;
        private DevExpress.XtraGrid.Views.Grid.GridView grvForcedMatchings;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btnAddForcedMatching;
        private DevExpress.XtraEditors.SimpleButton btnRemoveForcedMatching;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl16;
    }
}