namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class ScriptManager
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
            this.components = new System.ComponentModel.Container();
            this.CContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenu_AddBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_AddAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Modify = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.Manager = new System.Windows.Forms.TreeView();
            this.CContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CContextMenu
            // 
            this.CContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_AddBelow,
            this.ContextMenu_AddAbove,
            this.ContextMenu_Modify,
            this.ContextMenu_Delete,
            this.ContextMenu_Separator1,
            this.ContextMenu_Cut,
            this.ContextMenu_Copy,
            this.ContextMenu_Paste});
            this.CContextMenu.Name = "ContextMenu";
            this.CContextMenu.ShowImageMargin = false;
            this.CContextMenu.Size = new System.Drawing.Size(128, 186);
            // 
            // ContextMenu_AddBelow
            // 
            this.ContextMenu_AddBelow.Name = "ContextMenu_AddBelow";
            this.ContextMenu_AddBelow.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_AddBelow.Text = "Add below";
            this.ContextMenu_AddBelow.Click += new System.EventHandler(this.ScriptManager_AddBelow);
            // 
            // ContextMenu_AddAbove
            // 
            this.ContextMenu_AddAbove.Name = "ContextMenu_AddAbove";
            this.ContextMenu_AddAbove.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_AddAbove.Text = "Add above";
            this.ContextMenu_AddAbove.Click += new System.EventHandler(this.ScriptManager_AddAbove);
            // 
            // ContextMenu_Modify
            // 
            this.ContextMenu_Modify.Name = "ContextMenu_Modify";
            this.ContextMenu_Modify.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_Modify.Text = "Modify";
            this.ContextMenu_Modify.Click += new System.EventHandler(this.ScriptManager_Modify);
            // 
            // ContextMenu_Delete
            // 
            this.ContextMenu_Delete.Name = "ContextMenu_Delete";
            this.ContextMenu_Delete.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_Delete.Text = "Delete";
            this.ContextMenu_Delete.Click += new System.EventHandler(this.ScriptManager_Delete);
            // 
            // ContextMenu_Separator1
            // 
            this.ContextMenu_Separator1.Name = "ContextMenu_Separator1";
            this.ContextMenu_Separator1.Size = new System.Drawing.Size(124, 6);
            // 
            // ContextMenu_Cut
            // 
            this.ContextMenu_Cut.Enabled = false;
            this.ContextMenu_Cut.Name = "ContextMenu_Cut";
            this.ContextMenu_Cut.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_Cut.Text = "Cut";
            // 
            // ContextMenu_Copy
            // 
            this.ContextMenu_Copy.Enabled = false;
            this.ContextMenu_Copy.Name = "ContextMenu_Copy";
            this.ContextMenu_Copy.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_Copy.Text = "Copy";
            // 
            // ContextMenu_Paste
            // 
            this.ContextMenu_Paste.Enabled = false;
            this.ContextMenu_Paste.Name = "ContextMenu_Paste";
            this.ContextMenu_Paste.Size = new System.Drawing.Size(127, 22);
            this.ContextMenu_Paste.Text = "Paste";
            // 
            // Manager
            // 
            this.Manager.ContextMenuStrip = this.CContextMenu;
            this.Manager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Manager.Location = new System.Drawing.Point(0, 0);
            this.Manager.Name = "Manager";
            this.Manager.ShowLines = false;
            this.Manager.ShowNodeToolTips = true;
            this.Manager.Size = new System.Drawing.Size(381, 443);
            this.Manager.TabIndex = 1;
            this.Manager.DoubleClick += new System.EventHandler(this.ScriptManager_DoubleClickModification);
            this.Manager.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Manager_MouseClick);
            // 
            // ScriptManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.CContextMenu;
            this.Controls.Add(this.Manager);
            this.Name = "ScriptManager";
            this.Size = new System.Drawing.Size(381, 443);
            this.CContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip CContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_AddBelow;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Modify;
        private System.Windows.Forms.ToolStripSeparator ContextMenu_Separator1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Cut;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Copy;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Paste;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Delete;
        private System.Windows.Forms.TreeView Manager;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_AddAbove;
    }
}
