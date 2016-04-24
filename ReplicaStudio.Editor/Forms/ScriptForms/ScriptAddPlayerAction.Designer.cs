namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptAddPlayerAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptAddPlayerAction));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.characterButton1 = new ReplicaStudio.Editor.Forms.UserControls.CharacterButton();
            this.lblChooseAction = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.rdxCurrentCharacter = new System.Windows.Forms.RadioButton();
            this.rdxtoCharacterRadio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.AddPlayerAction_Ok);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.AddPlayerAction_Cancel);
            // 
            // characterButton1
            // 
            this.characterButton1.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.characterButton1, "characterButton1");
            this.characterButton1.Name = "characterButton1";
            this.characterButton1.UsePlayableCharacter = true;
            // 
            // lblChooseAction
            // 
            resources.ApplyResources(this.lblChooseAction, "lblChooseAction");
            this.lblChooseAction.Name = "lblChooseAction";
            // 
            // cmbAction
            // 
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.FormattingEnabled = true;
            resources.ApplyResources(this.cmbAction, "cmbAction");
            this.cmbAction.Name = "cmbAction";
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
            // ScriptAddPlayerAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.rdxCurrentCharacter);
            this.Controls.Add(this.rdxtoCharacterRadio);
            this.Controls.Add(this.cmbAction);
            this.Controls.Add(this.lblChooseAction);
            this.Controls.Add(this.characterButton1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "ScriptAddPlayerAction";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private UserControls.CharacterButton characterButton1;
        private System.Windows.Forms.Label lblChooseAction;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.RadioButton rdxCurrentCharacter;
        private System.Windows.Forms.RadioButton rdxtoCharacterRadio;
    }
}