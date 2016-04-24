namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseItemsInteraction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseItemsInteraction));
            this.grpCommands = new System.Windows.Forms.GroupBox();
            this.ScriptManager = new ReplicaStudio.Editor.Forms.UserControls.ScriptManager();
            this.ListItems2 = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.ListItems1 = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCommands
            // 
            resources.ApplyResources(this.grpCommands, "grpCommands");
            this.grpCommands.Controls.Add(this.ScriptManager);
            this.grpCommands.Name = "grpCommands";
            this.grpCommands.TabStop = false;
            // 
            // ScriptManager
            // 
            resources.ApplyResources(this.ScriptManager, "ScriptManager");
            this.ScriptManager.Name = "ScriptManager";
            this.ScriptManager.Script = null;
            this.ScriptManager.ScriptUpdated += new System.EventHandler(this.ScriptManager_ScriptUpdated);
            // 
            // ListItems2
            // 
            resources.ApplyResources(this.ListItems2, "ListItems2");
            this.ListItems2.CancelDeletion = false;
            this.ListItems2.DataSource = null;
            this.ListItems2.DoubleClickable = false;
            this.ListItems2.HideButtons = true;
            this.ListItems2.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListItems2.Name = "ListItems2";
            this.ListItems2.Title = "Item 2";
            this.ListItems2.ItemChosen += new System.EventHandler(this.ListItems2_ItemChosen);
            this.ListItems2.ListIsEmpty += new System.EventHandler(this.ListItems2_ListIsEmpty);
            // 
            // ListItems1
            // 
            resources.ApplyResources(this.ListItems1, "ListItems1");
            this.ListItems1.CancelDeletion = false;
            this.ListItems1.DataSource = null;
            this.ListItems1.DoubleClickable = false;
            this.ListItems1.HideButtons = true;
            this.ListItems1.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListItems1.Name = "ListItems1";
            this.ListItems1.Title = "Item 1";
            this.ListItems1.ItemChosen += new System.EventHandler(this.ListItems1_ItemChosen);
            this.ListItems1.ListIsEmpty += new System.EventHandler(this.ListItems1_ListIsEmpty);
            // 
            // DatabaseItemsInteraction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpCommands);
            this.Controls.Add(this.ListItems2);
            this.Controls.Add(this.ListItems1);
            this.Name = "DatabaseItemsInteraction";
            this.VisibleChanged += new System.EventHandler(this.DatabaseItemsInteraction_VisibleChanged);
            this.grpCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListItems1;
        private UserControls.ListItems ListItems2;
        private System.Windows.Forms.GroupBox grpCommands;
        private UserControls.ScriptManager ScriptManager;
    }
}
