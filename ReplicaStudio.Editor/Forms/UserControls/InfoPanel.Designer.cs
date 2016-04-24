namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class InfoPanel
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
            this.InformationPanel = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // InformationPanel
            // 
            this.InformationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InformationPanel.Location = new System.Drawing.Point(0, 0);
            this.InformationPanel.Name = "InformationPanel";
            this.InformationPanel.Size = new System.Drawing.Size(180, 230);
            this.InformationPanel.TabIndex = 0;
            this.InformationPanel.ToolbarVisible = false;
            // 
            // InfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InformationPanel);
            this.Name = "InfoPanel";
            this.Size = new System.Drawing.Size(180, 230);
            this.MouseEnter += new System.EventHandler(this.PreviewPanel_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid InformationPanel;
    }
}
