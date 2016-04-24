namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptCharacterAnimationFrequency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptCharacterAnimationFrequency));
            this.trkFrequency = new System.Windows.Forms.TrackBar();
            this.frqLabel = new System.Windows.Forms.Label();
            this.btnCancelFrequency = new System.Windows.Forms.Button();
            this.btnOkFrequency = new System.Windows.Forms.Button();
            this.btnResetFrequency = new System.Windows.Forms.Button();
            this.prctFrequency = new System.Windows.Forms.Label();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.cmbAnimation = new System.Windows.Forms.ComboBox();
            this.cmbCharacterList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trkFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // trkFrequency
            // 
            resources.ApplyResources(this.trkFrequency, "trkFrequency");
            this.trkFrequency.Maximum = 200;
            this.trkFrequency.Minimum = 1;
            this.trkFrequency.Name = "trkFrequency";
            this.trkFrequency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkFrequency.Value = 100;
            this.trkFrequency.Scroll += new System.EventHandler(this.trkFrequencyChange);
            // 
            // frqLabel
            // 
            resources.ApplyResources(this.frqLabel, "frqLabel");
            this.frqLabel.Name = "frqLabel";
            // 
            // btnCancelFrequency
            // 
            resources.ApplyResources(this.btnCancelFrequency, "btnCancelFrequency");
            this.btnCancelFrequency.Name = "btnCancelFrequency";
            this.btnCancelFrequency.UseVisualStyleBackColor = true;
            this.btnCancelFrequency.Click += new System.EventHandler(this.btnCancel);
            // 
            // btnOkFrequency
            // 
            resources.ApplyResources(this.btnOkFrequency, "btnOkFrequency");
            this.btnOkFrequency.Name = "btnOkFrequency";
            this.btnOkFrequency.UseVisualStyleBackColor = true;
            this.btnOkFrequency.Click += new System.EventHandler(this.btnValidation);
            // 
            // btnResetFrequency
            // 
            resources.ApplyResources(this.btnResetFrequency, "btnResetFrequency");
            this.btnResetFrequency.Name = "btnResetFrequency";
            this.btnResetFrequency.UseVisualStyleBackColor = true;
            this.btnResetFrequency.Click += new System.EventHandler(this.btnReset);
            // 
            // prctFrequency
            // 
            resources.ApplyResources(this.prctFrequency, "prctFrequency");
            this.prctFrequency.Name = "prctFrequency";
            // 
            // lblCharacter
            // 
            resources.ApplyResources(this.lblCharacter, "lblCharacter");
            this.lblCharacter.Name = "lblCharacter";
            // 
            // lblAnimation
            // 
            resources.ApplyResources(this.lblAnimation, "lblAnimation");
            this.lblAnimation.Name = "lblAnimation";
            // 
            // cmbAnimation
            // 
            resources.ApplyResources(this.cmbAnimation, "cmbAnimation");
            this.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimation.FormattingEnabled = true;
            this.cmbAnimation.Name = "cmbAnimation";
            // 
            // cmbCharacterList
            // 
            resources.ApplyResources(this.cmbCharacterList, "cmbCharacterList");
            this.cmbCharacterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCharacterList.FormattingEnabled = true;
            this.cmbCharacterList.Name = "cmbCharacterList";
            // 
            // ScriptCharacterAnimationFrequency
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.cmbCharacterList);
            this.Controls.Add(this.cmbAnimation);
            this.Controls.Add(this.lblAnimation);
            this.Controls.Add(this.lblCharacter);
            this.Controls.Add(this.prctFrequency);
            this.Controls.Add(this.btnResetFrequency);
            this.Controls.Add(this.btnOkFrequency);
            this.Controls.Add(this.btnCancelFrequency);
            this.Controls.Add(this.frqLabel);
            this.Controls.Add(this.trkFrequency);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptCharacterAnimationFrequency";
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.trkFrequency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trkFrequency;
        private System.Windows.Forms.Label frqLabel;
        private System.Windows.Forms.Button btnCancelFrequency;
        private System.Windows.Forms.Button btnOkFrequency;
        private System.Windows.Forms.Button btnResetFrequency;
        private System.Windows.Forms.Label prctFrequency;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.ComboBox cmbAnimation;
        private System.Windows.Forms.ComboBox cmbCharacterList;
    }
}