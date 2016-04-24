namespace ReplicaStudio.Editor.Forms
{
    partial class ImageColorManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageColorManager));
            this.lblRed = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.tbRed = new System.Windows.Forms.TrackBar();
            this.tbGreen = new System.Windows.Forms.TrackBar();
            this.tbBlue = new System.Windows.Forms.TrackBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.tbGrey = new System.Windows.Forms.TrackBar();
            this.lblGrey = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGrey)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRed
            // 
            resources.ApplyResources(this.lblRed, "lblRed");
            this.lblRed.Name = "lblRed";
            // 
            // lblGreen
            // 
            resources.ApplyResources(this.lblGreen, "lblGreen");
            this.lblGreen.Name = "lblGreen";
            // 
            // lblBlue
            // 
            resources.ApplyResources(this.lblBlue, "lblBlue");
            this.lblBlue.Name = "lblBlue";
            // 
            // tbRed
            // 
            resources.ApplyResources(this.tbRed, "tbRed");
            this.tbRed.Maximum = 255;
            this.tbRed.Minimum = -255;
            this.tbRed.Name = "tbRed";
            this.tbRed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRed.Scroll += new System.EventHandler(this.tbRed_Scroll);
            // 
            // tbGreen
            // 
            resources.ApplyResources(this.tbGreen, "tbGreen");
            this.tbGreen.Maximum = 255;
            this.tbGreen.Minimum = -255;
            this.tbGreen.Name = "tbGreen";
            this.tbGreen.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbGreen.Scroll += new System.EventHandler(this.tbGreen_Scroll);
            // 
            // tbBlue
            // 
            resources.ApplyResources(this.tbBlue, "tbBlue");
            this.tbBlue.Maximum = 255;
            this.tbBlue.Minimum = -255;
            this.tbBlue.Name = "tbBlue";
            this.tbBlue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbBlue.Scroll += new System.EventHandler(this.tbBlue_Scroll);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbOpacity
            // 
            resources.ApplyResources(this.tbOpacity, "tbOpacity");
            this.tbOpacity.Maximum = 255;
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbOpacity.Value = 255;
            this.tbOpacity.Scroll += new System.EventHandler(this.tbOpacity_Scroll);
            // 
            // lblOpacity
            // 
            resources.ApplyResources(this.lblOpacity, "lblOpacity");
            this.lblOpacity.Name = "lblOpacity";
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tbGrey
            // 
            resources.ApplyResources(this.tbGrey, "tbGrey");
            this.tbGrey.Maximum = 255;
            this.tbGrey.Name = "tbGrey";
            this.tbGrey.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbGrey.Scroll += new System.EventHandler(this.tbGrey_Scroll);
            // 
            // lblGrey
            // 
            resources.ApplyResources(this.lblGrey, "lblGrey");
            this.lblGrey.Name = "lblGrey";
            // 
            // ImageColorManager
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.tbGrey);
            this.Controls.Add(this.lblGrey);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tbOpacity);
            this.Controls.Add(this.lblOpacity);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbBlue);
            this.Controls.Add(this.tbGreen);
            this.Controls.Add(this.tbRed);
            this.Controls.Add(this.lblBlue);
            this.Controls.Add(this.lblGreen);
            this.Controls.Add(this.lblRed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageColorManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.tbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGrey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.TrackBar tbRed;
        private System.Windows.Forms.TrackBar tbGreen;
        private System.Windows.Forms.TrackBar tbBlue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TrackBar tbOpacity;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TrackBar tbGrey;
        private System.Windows.Forms.Label lblGrey;
    }
}