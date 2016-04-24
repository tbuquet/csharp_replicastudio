namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseGlobalEvents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseGlobalEvents));
            this.ListGlobalEvents = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.chkTrigger = new System.Windows.Forms.CheckBox();
            this.trgTrigger = new ReplicaStudio.Editor.Forms.UserControls.TriggerButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grpScript = new System.Windows.Forms.GroupBox();
            this.ScriptManager = new ReplicaStudio.Editor.Forms.UserControls.ScriptManager();
            this.grpInformations.SuspendLayout();
            this.grpScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListGlobalEvents
            // 
            resources.ApplyResources(this.ListGlobalEvents, "ListGlobalEvents");
            this.ListGlobalEvents.CancelDeletion = false;
            this.ListGlobalEvents.DataSource = null;
            this.ListGlobalEvents.DoubleClickable = false;
            this.ListGlobalEvents.HideButtons = false;
            this.ListGlobalEvents.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListGlobalEvents.Name = "ListGlobalEvents";
            this.ListGlobalEvents.Title = "Global Events";
            this.ListGlobalEvents.ItemChosen += new System.EventHandler(this.ListGlobalEvents_ItemChosen);
            this.ListGlobalEvents.ItemToCreate += new System.EventHandler(this.ListGlobalEvents_ItemToCreate);
            this.ListGlobalEvents.ItemToDelete += new System.EventHandler(this.ListGlobalEvents_ItemToDelete);
            this.ListGlobalEvents.ListIsEmpty += new System.EventHandler(this.ListGlobalEvents_ListIsEmpty);
            // 
            // grpInformations
            // 
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Controls.Add(this.chkTrigger);
            this.grpInformations.Controls.Add(this.trgTrigger);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblName);
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // chkTrigger
            // 
            resources.ApplyResources(this.chkTrigger, "chkTrigger");
            this.chkTrigger.Name = "chkTrigger";
            this.chkTrigger.UseVisualStyleBackColor = true;
            // 
            // trgTrigger
            // 
            resources.ApplyResources(this.trgTrigger, "trgTrigger");
            this.trgTrigger.Name = "trgTrigger";
            this.trgTrigger.TriggerGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // grpScript
            // 
            resources.ApplyResources(this.grpScript, "grpScript");
            this.grpScript.Controls.Add(this.ScriptManager);
            this.grpScript.Name = "grpScript";
            this.grpScript.TabStop = false;
            // 
            // ScriptManager
            // 
            resources.ApplyResources(this.ScriptManager, "ScriptManager");
            this.ScriptManager.Name = "ScriptManager";
            this.ScriptManager.Script = null;
            // 
            // DatabaseGlobalEvents
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpScript);
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListGlobalEvents);
            this.Name = "DatabaseGlobalEvents";
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            this.grpScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListGlobalEvents;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpScript;
        private UserControls.ScriptManager ScriptManager;
        private UserControls.TriggerButton trgTrigger;
        private System.Windows.Forms.CheckBox chkTrigger;
    }
}
