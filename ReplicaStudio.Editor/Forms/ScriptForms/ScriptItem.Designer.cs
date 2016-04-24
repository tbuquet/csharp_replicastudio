namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptItem));
            this.lblItem = new System.Windows.Forms.Label();
            this.ValidationButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.itemButton1 = new ReplicaStudio.Editor.Forms.UserControls.ItemButton();
            this.characterButton1 = new ReplicaStudio.Editor.Forms.UserControls.CharacterButton();
            this.rdxtoCharacterRadio = new System.Windows.Forms.RadioButton();
            this.rdxCurrentCharacter = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblItem
            // 
            resources.ApplyResources(this.lblItem, "lblItem");
            this.lblItem.Name = "lblItem";
            // 
            // ValidationButton
            // 
            resources.ApplyResources(this.ValidationButton, "ValidationButton");
            this.ValidationButton.Name = "ValidationButton";
            this.ValidationButton.UseVisualStyleBackColor = true;
            this.ValidationButton.Click += new System.EventHandler(this.ScriptItem_Validation);
            // 
            // CancelButton
            // 
            resources.ApplyResources(this.CancelButton, "CancelButton");
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.ScriptItem_Cancel);
            // 
            // itemButton1
            // 
            this.itemButton1.ItemGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.itemButton1, "itemButton1");
            this.itemButton1.Name = "itemButton1";
            // 
            // characterButton1
            // 
            this.characterButton1.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.characterButton1, "characterButton1");
            this.characterButton1.Name = "characterButton1";
            this.characterButton1.UsePlayableCharacter = true;
            // 
            // rdxtoCharacterRadio
            // 
            resources.ApplyResources(this.rdxtoCharacterRadio, "rdxtoCharacterRadio");
            this.rdxtoCharacterRadio.Name = "rdxtoCharacterRadio";
            this.rdxtoCharacterRadio.UseVisualStyleBackColor = true;
            this.rdxtoCharacterRadio.Click += new System.EventHandler(this.ScriptItem_ToCharSelected);
            // 
            // rdxCurrentCharacter
            // 
            resources.ApplyResources(this.rdxCurrentCharacter, "rdxCurrentCharacter");
            this.rdxCurrentCharacter.Checked = true;
            this.rdxCurrentCharacter.Name = "rdxCurrentCharacter";
            this.rdxCurrentCharacter.TabStop = true;
            this.rdxCurrentCharacter.UseVisualStyleBackColor = true;
            this.rdxCurrentCharacter.Click += new System.EventHandler(this.ScriptItem_ToCurrentCharSelected);
            // 
            // ScriptItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.rdxCurrentCharacter);
            this.Controls.Add(this.rdxtoCharacterRadio);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ValidationButton);
            this.Controls.Add(this.itemButton1);
            this.Controls.Add(this.characterButton1);
            this.Controls.Add(this.lblItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptItem";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblItem;
        private UserControls.CharacterButton characterButton1;
        private UserControls.ItemButton itemButton1;
        private System.Windows.Forms.Button ValidationButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.RadioButton rdxtoCharacterRadio;
        private System.Windows.Forms.RadioButton rdxCurrentCharacter;
    }
}