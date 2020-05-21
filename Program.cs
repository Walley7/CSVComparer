using CSACore.Core;
using CSACore.Utility;
using CSACoreWin.Core;
using CSVComparer.Forms;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CSVComparer {

    static class Program {
        //================================================================================
        //--------------------------------------------------------------------------------
        [STAThread]
        static void Main() {
            // Initialise
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialise - CSA
            try {
                CSA.Initialise(null);
                CSAWin.Initialise("CSA", "CSV Comparer");
            }
            catch (Exception e) {
                XtraMessageBox.Show("Failed to initialise: " + e.Message, "CSV Comparer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Run
            Application.Run(new MainForm());

            // Shutdown
            CSA.Shutdown();
        }
    }

}
