namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptChangePlayerAnimation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptChangePlayerAnimation));
            this.btnCancelFrequency = new System.Windows.Forms.Button();
            this.btnOkFrequency = new System.Windows.Forms.Button();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.cmbAnimation = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbTypeAnimation = new System.Windows.Forms.ComboBox();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.lblLoop = new System.Windows.Forms.Label();
            this.characterButton1 = new ReplicaStudio.Editor.Forms.UserControls.CharacterButton();
            this.SuspendLayout();
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
            // lblAnimation
            // 
            resources.ApplyResources(this.lblAnimation, "lblAnimation");
            this.lblAnimation.Name = "lblAnimation";
            // 
            // cmbAnimation
            // 
            this.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimation.FormattingEnabled = true;
            resources.ApplyResources(this.cmbAnimation, "cmbAnimation");
            this.cmbAnimation.Name = "cmbAnimation";
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // cmbTypeAnimation
            // 
            this.cmbTypeAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeAnimation.FormattingEnabled = true;
            resources.ApplyResources(this.cmbTypeAnimation, "cmbTypeAnimation");
            this.cmbTypeAnimation.Name = "cmbTypeAnimation";
            // 
            // lblCharacter
            // 
            resources.ApplyResources(this.lblCharacter, "lblCharacter");
            this.lblCharacter.Name = "lblCharacter";
            // 
            // chkLoop
            // 
            resources.ApplyResources(this.chkLoop, "chkLoop");
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.UseVisualStyleBackColor = true;
            this.chkLoop.CheckedChanged += new System.EventHandler(this.chkLoop_CheckedChanged);
            // 
            // lblLoop
            // 
            resources.ApplyResources(this.lblLoop, "lblLoop");
            this.lblLoop.Name = "lblLoop";
            // 
            // characterButton1
            // 
            this.characterButton1.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.characterButton1, "characterButton1");
            this.characterButton1.Name = "characterButton1";
            this.characterButton1.UsePlayableCharacter = true;
            this.characterButton1.ValueChanged += new System.EventHandler(this.ChangePlayerAnimation);
            // 
            // ScriptChangePlayerAnimation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.lblLoop);
            this.Controls.Add(this.chkLoop);
            this.Controls.Add(this.lblCharacter);
            this.Controls.Add(this.cmbTypeAnimation);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.characterButton1);
            this.Controls.Add(this.cmbAnimation);
            this.Controls.Add(this.lblAnimation);
            this.Controls.Add(this.btnOkFrequency);
            this.Controls.Add(this.btnCancelFrequency);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptChangePlayerAnimation";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelFrequency;
        private System.Windows.Forms.Button btnOkFrequency;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.ComboBox cmbAnimation;
        private UserControls.CharacterButton characterButton1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbTypeAnimation;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.Label lblLoop;
    }
}