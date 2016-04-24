namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptFreePlayerAnimation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptFreePlayerAnimation));
            this.btnCancelFrequency = new System.Windows.Forms.Button();
            this.btnOkFrequency = new System.Windows.Forms.Button();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.cmbAnimation = new System.Windows.Forms.ComboBox();
            this.chxAllAnimations = new System.Windows.Forms.CheckBox();
            this.rdxCurrentCharacter = new System.Windows.Forms.RadioButton();
            this.rdxtoCharacterRadio = new System.Windows.Forms.RadioButton();
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
            // chxAllAnimations
            // 
            resources.ApplyResources(this.chxAllAnimations, "chxAllAnimations");
            this.chxAllAnimations.Name = "chxAllAnimations";
            this.chxAllAnimations.UseVisualStyleBackColor = true;
            // 
            // rdxCurrentCharacter
            // 
            resources.ApplyResources(this.rdxCurrentCharacter, "rdxCurrentCharacter");
            this.rdxCurrentCharacter.Checked = true;
            this.rdxCurrentCharacter.Name = "rdxCurrentCharacter";
            this.rdxCurrentCharacter.TabStop = true;
            this.rdxCurrentCharacter.UseVisualStyleBackColor = true;
            this.rdxCurrentCharacter.CheckedChanged += new System.EventHandler(this.SelectCharacter_CurrentCharacter);
            // 
            // rdxtoCharacterRadio
            // 
            resources.ApplyResources(this.rdxtoCharacterRadio, "rdxtoCharacterRadio");
            this.rdxtoCharacterRadio.Name = "rdxtoCharacterRadio";
            this.rdxtoCharacterRadio.UseVisualStyleBackColor = true;
            this.rdxtoCharacterRadio.CheckedChanged += new System.EventHandler(this.SelectCharacter_Character);
            // 
            // characterButton1
            // 
            this.characterButton1.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.characterButton1, "characterButton1");
            this.characterButton1.Name = "characterButton1";
            this.characterButton1.UsePlayableCharacter = true;
            // 
            // ScriptFreePlayerAnimation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.characterButton1);
            this.Controls.Add(this.rdxCurrentCharacter);
            this.Controls.Add(this.rdxtoCharacterRadio);
            this.Controls.Add(this.chxAllAnimations);
            this.Controls.Add(this.cmbAnimation);
            this.Controls.Add(this.lblAnimation);
            this.Controls.Add(this.btnOkFrequency);
            this.Controls.Add(this.btnCancelFrequency);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptFreePlayerAnimation";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelFrequency;
        private System.Windows.Forms.Button btnOkFrequency;
        private System.Windows.Forms.Label lblAnimation;
        private System.Windows.Forms.ComboBox cmbAnimation;
        private System.Windows.Forms.CheckBox chxAllAnimations;
        private System.Windows.Forms.RadioButton rdxCurrentCharacter;
        private System.Windows.Forms.RadioButton rdxtoCharacterRadio;
        private UserControls.CharacterButton characterButton1;
    }
}