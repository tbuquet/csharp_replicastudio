namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptChangeCharacterDirection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptChangeCharacterDirection));
            this.CancelButton = new System.Windows.Forms.Button();
            this.ValidationButton = new System.Windows.Forms.Button();
            this.lblDirection = new System.Windows.Forms.Label();
            this.ddpMovements = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCharacterSelection = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // lblDirection
            // 
            resources.ApplyResources(this.lblDirection, "lblDirection");
            this.lblDirection.Name = "lblDirection";
            // 
            // ddpMovements
            // 
            resources.ApplyResources(this.ddpMovements, "ddpMovements");
            this.ddpMovements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpMovements.FormattingEnabled = true;
            this.ddpMovements.Name = "ddpMovements";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbCharacterSelection
            // 
            resources.ApplyResources(this.cmbCharacterSelection, "cmbCharacterSelection");
            this.cmbCharacterSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCharacterSelection.FormattingEnabled = true;
            this.cmbCharacterSelection.Name = "cmbCharacterSelection";
            // 
            // ScriptChangeCharacterDirection
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.cmbCharacterSelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddpMovements);
            this.Controls.Add(this.lblDirection);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ValidationButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptChangeCharacterDirection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ValidationButton;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.ComboBox ddpMovements;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCharacterSelection;
    }
}