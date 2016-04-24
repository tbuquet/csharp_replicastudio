using System.Windows.Forms;
namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class StagePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StagePanel));
            this.CContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenu_NewAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_NewCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_NewHotspot = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_EditForm = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_MoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_MoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_MoveFirst = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_MoveLast = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenu_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSurface = new System.Windows.Forms.PictureBox();
            this.CContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSurface)).BeginInit();
            this.SuspendLayout();
            // 
            // CContextMenu
            // 
            resources.ApplyResources(this.CContextMenu, "CContextMenu");
            this.CContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenu_NewAnimation,
            this.ContextMenu_NewCharacter,
            this.ContextMenu_NewHotspot,
            this.ContextMenu_EditForm,
            this.ContextMenu_Separator1,
            this.ContextMenu_MoveUp,
            this.ContextMenu_MoveDown,
            this.ContextMenu_MoveFirst,
            this.ContextMenu_MoveLast,
            this.ContextMenu_Separator2,
            this.ContextMenu_Cut,
            this.ContextMenu_Copy,
            this.ContextMenu_Paste,
            this.ContextMenu_Delete});
            this.CContextMenu.Name = "ContextMenu";
            this.CContextMenu.ShowImageMargin = false;
            this.CContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.CContextMenu_Opening);
            // 
            // ContextMenu_NewAnimation
            // 
            resources.ApplyResources(this.ContextMenu_NewAnimation, "ContextMenu_NewAnimation");
            this.ContextMenu_NewAnimation.Name = "ContextMenu_NewAnimation";
            this.ContextMenu_NewAnimation.Click += new System.EventHandler(this.ContextMenu_NewAnimation_Click);
            // 
            // ContextMenu_NewCharacter
            // 
            resources.ApplyResources(this.ContextMenu_NewCharacter, "ContextMenu_NewCharacter");
            this.ContextMenu_NewCharacter.Name = "ContextMenu_NewCharacter";
            this.ContextMenu_NewCharacter.Click += new System.EventHandler(this.ContextMenu_NewCharacter_Click);
            // 
            // ContextMenu_NewHotspot
            // 
            resources.ApplyResources(this.ContextMenu_NewHotspot, "ContextMenu_NewHotspot");
            this.ContextMenu_NewHotspot.Name = "ContextMenu_NewHotspot";
            this.ContextMenu_NewHotspot.Click += new System.EventHandler(this.ContextMenu_NewEvent_Click);
            // 
            // ContextMenu_EditForm
            // 
            resources.ApplyResources(this.ContextMenu_EditForm, "ContextMenu_EditForm");
            this.ContextMenu_EditForm.Name = "ContextMenu_EditForm";
            this.ContextMenu_EditForm.Click += new System.EventHandler(this.ContextMenu_EditForm_Click);
            // 
            // ContextMenu_Separator1
            // 
            resources.ApplyResources(this.ContextMenu_Separator1, "ContextMenu_Separator1");
            this.ContextMenu_Separator1.Name = "ContextMenu_Separator1";
            // 
            // ContextMenu_MoveUp
            // 
            resources.ApplyResources(this.ContextMenu_MoveUp, "ContextMenu_MoveUp");
            this.ContextMenu_MoveUp.Name = "ContextMenu_MoveUp";
            this.ContextMenu_MoveUp.Click += new System.EventHandler(this.ContextMenu_MoveUp_Click);
            // 
            // ContextMenu_MoveDown
            // 
            resources.ApplyResources(this.ContextMenu_MoveDown, "ContextMenu_MoveDown");
            this.ContextMenu_MoveDown.Name = "ContextMenu_MoveDown";
            this.ContextMenu_MoveDown.Click += new System.EventHandler(this.ContextMenu_MoveDown_Click);
            // 
            // ContextMenu_MoveFirst
            // 
            resources.ApplyResources(this.ContextMenu_MoveFirst, "ContextMenu_MoveFirst");
            this.ContextMenu_MoveFirst.Name = "ContextMenu_MoveFirst";
            this.ContextMenu_MoveFirst.Click += new System.EventHandler(this.ContextMenu_MoveFirst_Click);
            // 
            // ContextMenu_MoveLast
            // 
            resources.ApplyResources(this.ContextMenu_MoveLast, "ContextMenu_MoveLast");
            this.ContextMenu_MoveLast.Name = "ContextMenu_MoveLast";
            this.ContextMenu_MoveLast.Click += new System.EventHandler(this.ContextMenu_MoveLast_Click);
            // 
            // ContextMenu_Separator2
            // 
            resources.ApplyResources(this.ContextMenu_Separator2, "ContextMenu_Separator2");
            this.ContextMenu_Separator2.Name = "ContextMenu_Separator2";
            // 
            // ContextMenu_Cut
            // 
            resources.ApplyResources(this.ContextMenu_Cut, "ContextMenu_Cut");
            this.ContextMenu_Cut.Name = "ContextMenu_Cut";
            // 
            // ContextMenu_Copy
            // 
            resources.ApplyResources(this.ContextMenu_Copy, "ContextMenu_Copy");
            this.ContextMenu_Copy.Name = "ContextMenu_Copy";
            // 
            // ContextMenu_Paste
            // 
            resources.ApplyResources(this.ContextMenu_Paste, "ContextMenu_Paste");
            this.ContextMenu_Paste.Name = "ContextMenu_Paste";
            // 
            // ContextMenu_Delete
            // 
            resources.ApplyResources(this.ContextMenu_Delete, "ContextMenu_Delete");
            this.ContextMenu_Delete.Name = "ContextMenu_Delete";
            this.ContextMenu_Delete.Click += new System.EventHandler(this.ContextMenu_Delete_Click);
            // 
            // MainSurface
            // 
            resources.ApplyResources(this.MainSurface, "MainSurface");
            this.MainSurface.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.MainSurface.Name = "MainSurface";
            this.MainSurface.TabStop = false;
            this.MainSurface.Click += new System.EventHandler(this.MainSurface_Click);
            this.MainSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainSurface_MouseDown);
            this.MainSurface.MouseEnter += new System.EventHandler(this.StagePanel_MouseEnter);
            this.MainSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainSurface_MouseMove);
            this.MainSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainSurface_MouseUp);
            // 
            // StagePanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ContextMenuStrip = this.CContextMenu;
            this.Controls.Add(this.MainSurface);
            this.Name = "StagePanel";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.StagePanel_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.StagePanel_DragEnter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StagePanel_KeyUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.StagePanel_MouseWheel);
            this.CContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSurface)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip CContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_NewCharacter;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_NewHotspot;
        private System.Windows.Forms.ToolStripSeparator ContextMenu_Separator1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Cut;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Copy;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Paste;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_Delete;
        private System.Windows.Forms.ToolStripSeparator ContextMenu_Separator2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenu_NewAnimation;
        private System.Windows.Forms.PictureBox MainSurface;
        private ToolStripMenuItem ContextMenu_MoveUp;
        private ToolStripMenuItem ContextMenu_MoveDown;
        private ToolStripMenuItem ContextMenu_MoveFirst;
        private ToolStripMenuItem ContextMenu_MoveLast;
        private ToolStripMenuItem ContextMenu_EditForm;
    }
}
