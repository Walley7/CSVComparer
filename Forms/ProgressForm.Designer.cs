namespace CSVComparer.Forms {
    partial class ProgressForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.prpProgress = new DevExpress.XtraWaitForm.ProgressPanel();
            this.prgProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItemProgress = new System.Windows.Forms.Label();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlHideAnimation = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.prgProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(435, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.Visible = false;
            // 
            // prpProgress
            // 
            this.prpProgress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.prpProgress.Appearance.Options.UseBackColor = true;
            this.prpProgress.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F);
            this.prpProgress.AppearanceCaption.Options.UseFont = true;
            this.prpProgress.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.prpProgress.AppearanceDescription.Options.UseFont = true;
            this.prpProgress.BarAnimationElementThickness = 2;
            this.prpProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.prpProgress.Caption = "58% complete";
            this.prpProgress.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.prpProgress.Description = "";
            this.prpProgress.ImageHorzOffset = 20;
            this.prpProgress.LineAnimationElementHeight = 6;
            this.prpProgress.LineAnimationElementType = DevExpress.Utils.Animation.LineAnimationElementType.Rectangle;
            this.prpProgress.Location = new System.Drawing.Point(10, 10);
            this.prpProgress.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.prpProgress.Name = "prpProgress";
            this.prpProgress.Size = new System.Drawing.Size(500, 40);
            this.prpProgress.TabIndex = 29;
            this.prpProgress.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Line;
            // 
            // prgProgress
            // 
            this.prgProgress.EditValue = 50;
            this.prgProgress.Location = new System.Drawing.Point(10, 50);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Properties.FlowAnimationDelay = 2000;
            this.prgProgress.Properties.FlowAnimationDuration = 2000;
            this.prgProgress.Properties.FlowAnimationEnabled = true;
            this.prgProgress.Properties.LookAndFeel.SkinName = "VS2010";
            this.prgProgress.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.prgProgress.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.prgProgress.ShowProgressInTaskBar = true;
            this.prgProgress.Size = new System.Drawing.Size(500, 25);
            this.prgProgress.TabIndex = 30;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 85);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(98, 13);
            this.lblDescription.TabIndex = 31;
            this.lblDescription.Text = "Comparing WIPs...";
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.Location = new System.Drawing.Point(10, 100);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(32, 13);
            this.lblItemDescription.TabIndex = 32;
            this.lblItemDescription.Text = "Row:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Time remaining:";
            // 
            // lblItemProgress
            // 
            this.lblItemProgress.AutoSize = true;
            this.lblItemProgress.Location = new System.Drawing.Point(100, 100);
            this.lblItemProgress.Name = "lblItemProgress";
            this.lblItemProgress.Size = new System.Drawing.Size(53, 13);
            this.lblItemProgress.TabIndex = 34;
            this.lblItemProgress.Text = "1 of 9999";
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.Location = new System.Drawing.Point(100, 115);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(85, 13);
            this.lblTimeRemaining.TabIndex = 35;
            this.lblTimeRemaining.Text = "About 2 minutes";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(435, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "&Cancel";
            // 
            // pnlHideAnimation
            // 
            this.pnlHideAnimation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHideAnimation.Location = new System.Drawing.Point(0, 0);
            this.pnlHideAnimation.Name = "pnlHideAnimation";
            this.pnlHideAnimation.Size = new System.Drawing.Size(528, 20);
            this.pnlHideAnimation.TabIndex = 37;
            this.pnlHideAnimation.Visible = false;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 151);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.lblItemProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblItemDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.prgProgress);
            this.Controls.Add(this.prpProgress);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlHideAnimation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CSV Comparer";
            ((System.ComponentModel.ISupportInitialize)(this.prgProgress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraWaitForm.ProgressPanel prpProgress;
        private DevExpress.XtraEditors.ProgressBarControl prgProgress;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblItemDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItemProgress;
        private System.Windows.Forms.Label lblTimeRemaining;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel pnlHideAnimation;
    }
}