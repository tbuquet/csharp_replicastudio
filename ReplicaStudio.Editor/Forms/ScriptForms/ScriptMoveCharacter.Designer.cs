namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptMoveCharacter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptMoveCharacter));
            this.lblCoords = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ValidationButton = new System.Windows.Forms.Button();
            this.crdCoords = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.lblMoveCharacter = new System.Windows.Forms.Label();
            this.cbxListCharacter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // lblMoveCharacter
            // 
            resources.ApplyResources(this.lblMoveCharacter, "lblMoveCharacter");
            this.lblMoveCharacter.Name = "lblMoveCharacter";
            // 
            // cbxListCharacter
            // 
            resources.ApplyResources(this.cbxListCharacter, "cbxListCharacter");
            this.cbxListCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxListCharacter.FormattingEnabled = true;
            this.cbxListCharacter.Name = "cbxListCharacter";
            // 
            // ScriptMoveCharacter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.cbxListCharacter);
            this.Controls.Add(this.lblMoveCharacter);
            this.Controls.Add(this.crdCoords);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ValidationButton);
            this.Controls.Add(this.lblCoords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptMoveCharacter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCoords;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ValidationButton;
        private UserControls.CoordsButton crdCoords;
        private System.Windows.Forms.Label lblMoveCharacter;
        private System.Windows.Forms.ComboBox cbxListCharacter;
    }
}