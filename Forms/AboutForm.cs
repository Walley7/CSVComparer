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

    public partial class AboutForm : DevExpress.XtraEditors.XtraForm {
        //================================================================================
        //--------------------------------------------------------------------------------
        public AboutForm() {
            InitializeComponent();
        }
        

        // CONTROLS ================================================================================
        //--------------------------------------------------------------------------------
        private void btnWebPage_Click(object sender, EventArgs e) {
            Process.Start("http://www.customersystems.com.au/");
        }
    }

}
