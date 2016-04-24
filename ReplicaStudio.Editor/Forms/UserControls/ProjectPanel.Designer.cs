namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class ProjectPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectPanel));
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.LblProject = new System.Windows.Forms.ToolStripLabel();
            this.ProjectTreeView = new System.Windows.Forms.TreeView();
            this.Toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblProject});
            resources.ApplyResources(this.Toolbar, "Toolbar");
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.MouseEnter += new System.EventHandler(this.ProjectPanel_MouseEnter);
            // 
            // LblProject
            // 
            resources.ApplyResources(this.LblProject, "LblProject");
            this.LblProject.Name = "LblProject";
            // 
            // ProjectTreeView
            // 
            this.ProjectTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.ProjectTreeView, "ProjectTreeView");
            this.ProjectTreeView.Name = "ProjectTreeView";
            this.ProjectTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ProjectTreeView_ItemDrag);
            this.ProjectTreeView.DoubleClick += new System.EventHandler(this.ProjectTreeView_DoubleClick);
            this.ProjectTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProjectTreeView_KeyPressed);
            this.ProjectTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ProjectTreeView_MouseClick);
            this.ProjectTreeView.MouseEnter += new System.EventHandler(this.ProjectPanel_MouseEnter);
            // 
            // ProjectPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProjectTreeView);
            this.Controls.Add(this.Toolbar);
            this.Name = "ProjectPanel";
            this.MouseEnter += new System.EventHandler(this.ProjectPanel_MouseEnter);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip Toolbar;
        private System.Windows.Forms.ToolStripLabel LblProject;
        private System.Windows.Forms.TreeView ProjectTreeView;
    }
}
