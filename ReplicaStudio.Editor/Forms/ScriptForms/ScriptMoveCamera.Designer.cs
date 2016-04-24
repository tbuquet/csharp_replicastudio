namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptMoveCamera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptMoveCamera));
            this.chkMovingSpeed = new System.Windows.Forms.RadioButton();
            this.lblCoords = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ValidationButton = new System.Windows.Forms.Button();
            this.chkMoveImmediately = new System.Windows.Forms.RadioButton();
            this.ddpMovingSpeed = new System.Windows.Forms.NumericUpDown();
            this.crdCoords = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            ((System.ComponentModel.ISupportInitialize)(this.ddpMovingSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // chkMovingSpeed
            // 
            resources.ApplyResources(this.chkMovingSpeed, "chkMovingSpeed");
            this.chkMovingSpeed.Name = "chkMovingSpeed";
            this.chkMovingSpeed.UseVisualStyleBackColor = true;
            // 
            // lblCoords
            // 
            resources.ApplyResources(this.lblCoords, "lblCoords");
            this.lblCoords.Name = "lblCoords";
            // 
            // CancelButton
            // 
            resources.ApplyResources(this.CancelButton, "CancelButton");
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ValidationButton
            // 
            resources.ApplyResources(this.ValidationButton, "ValidationButton");
            this.ValidationButton.Name = "ValidationButton";
            this.ValidationButton.UseVisualStyleBackColor = true;
            this.ValidationButton.Click += new System.EventHandler(this.ValidationButton_Click);
            // 
            // chkMoveImmediately
            // 
            resources.ApplyResources(this.chkMoveImmediately, "chkMoveImmediately");
            this.chkMoveImmediately.Checked = true;
            this.chkMoveImmediately.Name = "chkMoveImmediately";
            this.chkMoveImmediately.TabStop = true;
            this.chkMoveImmediately.UseVisualStyleBackColor = true;
            // 
            // ddpMovingSpeed
            // 
            resources.ApplyResources(this.ddpMovingSpeed, "ddpMovingSpeed");
            this.ddpMovingSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ddpMovingSpeed.Name = "ddpMovingSpeed";
            this.ddpMovingSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // crdCoords
            // 
            resources.ApplyResources(this.crdCoords, "crdCoords");
            this.crdCoords.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdCoords.FullCoords = null;
            this.crdCoords.Name = "crdCoords";
            this.crdCoords.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdCoords.UseStageBackground = false;
            this.crdCoords.UseStages = false;
            // 
            // ScriptMoveCamera
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.ddpMovingSpeed);
            this.Controls.Add(this.crdCoords);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ValidationButton);
            this.Controls.Add(this.lblCoords);
            this.Controls.Add(this.chkMoveImmediately);
            this.Controls.Add(this.chkMovingSpeed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptMoveCamera";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.ddpMovingSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton chkMovingSpeed;
        private System.Windows.Forms.Label lblCoords;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ValidationButton;
        private UserControls.CoordsButton crdCoords;
        private System.Windows.Forms.RadioButton chkMoveImmediately;
        private System.Windows.Forms.NumericUpDown ddpMovingSpeed;
    }
}