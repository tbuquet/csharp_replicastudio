namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class CharacterButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtButton = new System.Windows.Forms.TextBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.panelTextBox = new System.Windows.Forms.Panel();
            this.pnlButton.SuspendLayout();
            this.panelTextBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtButton
            // 
            this.txtButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtButton.Location = new System.Drawing.Point(0, 0);
            this.txtButton.Name = "txtButton";
            this.txtButton.ReadOnly = true;
            this.txtButton.ShortcutsEnabled = false;
            this.txtButton.Size = new System.Drawing.Size(138, 20);
            this.txtButton.TabIndex = 0;
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(-1, 0);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(24, 20);
            this.btnChoose.TabIndex = 1;
            this.btnChoose.Text = "...";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnChoose);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.Location = new System.Drawing.Point(138, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(22, 20);
            this.pnlButton.TabIndex = 2;
            // 
            // panelTextBox
            // 
            this.panelTextBox.Controls.Add(this.txtButton);
            this.panelTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTextBox.Location = new System.Drawing.Point(0, 0);
            this.panelTextBox.Name = "panelTextBox";
            this.panelTextBox.Size = new System.Drawing.Size(138, 20);
            this.panelTextBox.TabIndex = 3;
            // 
            // Character
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTextBox);
            this.Controls.Add(this.pnlButton);
            this.Name = "Character";
            this.Size = new System.Drawing.Size(160, 20);
            this.pnlButton.ResumeLayout(false);
            this.panelTextBox.ResumeLayout(false);
            this.panelTextBox.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtButton;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Panel panelTextBox;
    }
}
