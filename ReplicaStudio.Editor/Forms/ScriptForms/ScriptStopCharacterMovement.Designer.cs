namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptStopCharacterMovement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptStopCharacterMovement));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStopCharacterMovement = new System.Windows.Forms.Label();
            this.cbxListCharacter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ScriptCharacterMovement_Ok);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ScriptCharacterMovement_Cancel);
            // 
            // lblStopCharacterMovement
            // 
            resources.ApplyResources(this.lblStopCharacterMovement, "lblStopCharacterMovement");
            this.lblStopCharacterMovement.Name = "lblStopCharacterMovement";
            // 
            // cbxListCharacter
            // 
            resources.ApplyResources(this.cbxListCharacter, "cbxListCharacter");
            this.cbxListCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListCharacter.FormattingEnabled = true;
            this.cbxListCharacter.Name = "cbxListCharacter";
            // 
            // ScriptStopCharacterMovement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.cbxListCharacter);
            this.Controls.Add(this.lblStopCharacterMovement);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "ScriptStopCharacterMovement";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStopCharacterMovement;
        private System.Windows.Forms.ComboBox cbxListCharacter;
    }
}