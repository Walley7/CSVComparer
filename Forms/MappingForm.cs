using CSVComparer.CSVComparison;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CSVComparer.Forms {

    public partial class MappingForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        private CSVMapping                              mMapping;

        private BindingList<CSVMapping.ForcedMatching>  mForcedMatchings;


        //================================================================================
        //--------------------------------------------------------------------------------
        public MappingForm(CSVMapping mapping) {
            // Initialise
            InitializeComponent();

            // Mapping
            mMapping = mapping;

            // Controls
            cboType.Properties.Items.AddRange(Enum.GetValues(typeof(CSVMapping.EType)));
            cboMergeRule.Properties.Items.AddRange(Enum.GetValues(typeof(CSVMapping.EMergeRule)));
        }
        

        // LOADING / SAVING ================================================================================
        //--------------------------------------------------------------------------------
        private void MappingForm_Load(object sender, EventArgs e) {
            // Load
            txtLeftColumn.Text = mMapping.LeftColumn.Name;
            txtRightColumn.Text = mMapping.RightColumn.Name;

            cboType.SelectedItem = mMapping.Type;
            cboMergeRule.SelectedItem = mMapping.MergeRule;
            
            chkIgnoreCase.Checked = mMapping.IgnoreCase;
            chkIgnoreSymbols.Checked = mMapping.IgnoreSymbols;
            chkIgnoreWhitespace.Checked = mMapping.IgnoreWhitespace;
            txtAllowedSymbols.Text = mMapping.AllowedSymbols;

            chkRemoveCase.Checked = mMapping.RemoveCase;
            txtPrefix.Text = mMapping.Prefix;
            txtSuffix.Text = mMapping.Suffix;

            chkDecimalPrecision.Checked = (mMapping.DecimalPrecision >= 0);

            spnDecimalPrecision.Value = Math.Max(mMapping.DecimalPrecision, 0);

            // Forced matchings
            mForcedMatchings = new BindingList<CSVMapping.ForcedMatching>(mMapping.ForcedMatchingsCopy);
            grdForcedMatchings.DataSource = mForcedMatchings;

            // Refresh
            RefreshControls();
        }
        
        //--------------------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e) {
            // Save
            mMapping.Type = (CSVMapping.EType)cboType.SelectedItem;
            mMapping.MergeRule = (CSVMapping.EMergeRule)cboMergeRule.SelectedItem;
            
            mMapping.IgnoreCase = chkIgnoreCase.Checked;
            mMapping.IgnoreSymbols = chkIgnoreSymbols.Checked;
            mMapping.IgnoreWhitespace = chkIgnoreWhitespace.Checked;
            mMapping.AllowedSymbols = txtAllowedSymbols.Text;

            mMapping.RemoveCase = chkRemoveCase.Checked;
            mMapping.Prefix = txtPrefix.Text;
            mMapping.Suffix = txtSuffix.Text;

            mMapping.DecimalPrecision = chkDecimalPrecision.Checked ? (int)spnDecimalPrecision.Value : -1;

            // Forced matchings
            mMapping.ForcedMatchings.Clear();
            mMapping.ForcedMatchings.AddRange(mForcedMatchings);
        }
        

        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void cboType_SelectedValueChanged(object sender, EventArgs e) { RefreshControls(); }
        private void chkDecimalPrecision_CheckedChanged(object sender, EventArgs e) { RefreshControls(); }
        
        //--------------------------------------------------------------------------------
        private void txtAllowedSymbols_Leave(object sender, EventArgs e) {
            txtAllowedSymbols.Text = txtAllowedSymbols.Text.Trim();
        }

        //--------------------------------------------------------------------------------
        private void RefreshControls() {
            chkDecimalPrecision.Enabled = ((CSVMapping.EType)cboType.SelectedItem == CSVMapping.EType.NUMBER ||
                                           (CSVMapping.EType)cboType.SelectedItem == CSVMapping.EType.PERCENTAGE);
            spnDecimalPrecision.Enabled = chkDecimalPrecision.Enabled && chkDecimalPrecision.Checked;
        }


        // FORCED MATCHINGS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnAddForcedMatching_Click(object sender, EventArgs e) {
            mForcedMatchings.Add(new CSVMapping.ForcedMatching("", "", false));
        }
        
        //--------------------------------------------------------------------------------
        private void btnRemoveForcedMatching_Click(object sender, EventArgs e) {
            if (grvForcedMatchings.SelectedRowsCount == 0)
                return;
            if (XtraMessageBox.Show("Remove the selected forced matchings?", "CSV Comparer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                grvForcedMatchings.DeleteSelectedRows();
        }
    }

}
