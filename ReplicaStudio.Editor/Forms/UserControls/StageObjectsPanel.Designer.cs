namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class StageObjectsPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StageObjectsPanel));
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.LblObjects = new System.Windows.Forms.ToolStripLabel();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEvent = new System.Windows.Forms.ToolStripButton();
            this.StageObjectsTreeView = new System.Windows.Forms.TreeView();
            this.Toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            resources.ApplyResources(this.Toolbar, "Toolbar");
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblObjects,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnEvent});
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.MouseEnter += new System.EventHandler(this.StageObjectsPanel_MouseEnter);
            // 
            // LblObjects
            // 
            resources.ApplyResources(this.LblObjects, "LblObjects");
            this.LblObjects.Name = "LblObjects";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::ReplicaStudio.Editor.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btnEvent
            // 
            resources.ApplyResources(this.btnEvent, "btnEvent");
            this.btnEvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEvent.Image = global::ReplicaStudio.Editor.Properties.Resources._event;
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // StageObjectsTreeView
            // 
            resources.ApplyResources(this.StageObjectsTreeView, "StageObjectsTreeView");
            this.StageObjectsTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StageObjectsTreeView.Name = "StageObjectsTreeView";
            this.StageObjectsTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StageObjectsTreeView_KeyUp);
            this.StageObjectsTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StageObjectsTreeView_MouseClick);
            this.StageObjectsTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.StageObjectsTreeView_MouseDoubleClick);
            this.StageObjectsTreeView.MouseEnter += new System.EventHandler(this.StageObjectsPanel_MouseEnter);
            // 
            // StageObjectsPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StageObjectsTreeView);
            this.Controls.Add(this.Toolbar);
            this.Name = "StageObjectsPanel";
            this.Leave += new System.EventHandler(this.StageObjectsPanel_Leave);
            this.MouseEnter += new System.EventHandler(this.StageObjectsPanel_MouseEnter);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip Toolbar;
        private System.Windows.Forms.ToolStripLabel LblObjects;
        private System.Windows.Forms.TreeView StageObjectsTreeView;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEvent;
    }
}
