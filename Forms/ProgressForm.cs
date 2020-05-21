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

    public partial class ProgressForm : XtraForm {
        //================================================================================
        private TimeSpan                        mTimeRemaining = TimeSpan.MaxValue;


        //================================================================================
        //--------------------------------------------------------------------------------
        public ProgressForm() {
            InitializeComponent();
        }


        // STATUS ================================================================================
        //--------------------------------------------------------------------------------
        public void SetCompleted() {
            pnlHideAnimation.BringToFront();
            pnlHideAnimation.Visible = true;
            btnCancel.Visible = false;
            btnOK.Visible = true;
        }


        // PROGRESS ================================================================================
        //--------------------------------------------------------------------------------
        public int Progress {
            set {
                prgProgress.Position = value;
                prpProgress.Caption = (int)(Math.Round(100.0 * (double)prgProgress.Position / (double)prgProgress.Properties.Maximum)) + "% complete";
            }
            get { return prgProgress.Position; }
        }

        //--------------------------------------------------------------------------------
        public int ProgressMaximum {
            set {
                prgProgress.Properties.Maximum = value;
                prpProgress.Caption = (int)(Math.Round(100.0 * (double)prgProgress.Position / (double)prgProgress.Properties.Maximum)) + "% complete";
            }
            get { return prgProgress.Properties.Maximum; }
        }

        //--------------------------------------------------------------------------------
        public string Description {
            set { lblDescription.Text = value; }
            get { return lblDescription.Text; }
        }

        //--------------------------------------------------------------------------------
        public string ItemDescription {
            set { lblItemDescription.Text = value; }
            get { return lblItemDescription.Text; }
        }

        //--------------------------------------------------------------------------------
        public string ItemProgress {
            set { lblItemProgress.Text = value; }
            get { return lblItemProgress.Text; }
        }

        //--------------------------------------------------------------------------------
        public TimeSpan TimeRemaining {
            set {
                mTimeRemaining = value;
                if (mTimeRemaining == TimeSpan.MaxValue)
                    lblTimeRemaining.Text = "Calculating...";
                else if (mTimeRemaining == TimeSpan.Zero)
                    lblTimeRemaining.Text = "";
                else
                    lblTimeRemaining.Text = "About " + (mTimeRemaining.Minutes > 0 ? mTimeRemaining.Minutes + " minutes" : mTimeRemaining.Seconds + " seconds");
            }
            get { return mTimeRemaining; }
        }
    }

}
