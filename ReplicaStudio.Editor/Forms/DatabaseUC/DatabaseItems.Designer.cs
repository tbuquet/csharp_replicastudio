namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseItems));
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grpIcons = new System.Windows.Forms.GroupBox();
            this.lblActiveIcon = new System.Windows.Forms.Label();
            this.AnimActiveIcon = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.AnimIcon = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.lblIcon = new System.Windows.Forms.Label();
            this.AnimInventory = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.lblIconInventory = new System.Windows.Forms.Label();
            this.grpView = new System.Windows.Forms.GroupBox();
            this.listActions = new System.Windows.Forms.ListBox();
            this.ViewScript = new ReplicaStudio.Editor.Forms.UserControls.ScriptManager();
            this.ListItems = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpInformations.SuspendLayout();
            this.grpIcons.SuspendLayout();
            this.grpView.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInformations
            // 
            this.grpInformations.Controls.Add(this.txtDescription);
            this.grpInformations.Controls.Add(this.lblDescription);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblName);
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
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
            // grpIcons
            // 
            this.grpIcons.Controls.Add(this.lblActiveIcon);
            this.grpIcons.Controls.Add(this.AnimActiveIcon);
            this.grpIcons.Controls.Add(this.AnimIcon);
            this.grpIcons.Controls.Add(this.lblIcon);
            this.grpIcons.Controls.Add(this.AnimInventory);
            this.grpIcons.Controls.Add(this.lblIconInventory);
            resources.ApplyResources(this.grpIcons, "grpIcons");
            this.grpIcons.Name = "grpIcons";
            this.grpIcons.TabStop = false;
            // 
            // lblActiveIcon
            // 
            resources.ApplyResources(this.lblActiveIcon, "lblActiveIcon");
            this.lblActiveIcon.Name = "lblActiveIcon";
            // 
            // AnimActiveIcon
            // 
            this.AnimActiveIcon.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimActiveIcon.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.IconAnimation;
            resources.ApplyResources(this.AnimActiveIcon, "AnimActiveIcon");
            this.AnimActiveIcon.BackColor = System.Drawing.Color.LightGray;
            this.AnimActiveIcon.Frequency = 100;
            this.AnimActiveIcon.LinkToAnimationManager = true;
            this.AnimActiveIcon.Name = "AnimActiveIcon";
            this.AnimActiveIcon.OriginPoint = true;
            this.AnimActiveIcon.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimActiveIcon.Row = 0;
            this.AnimActiveIcon.UseCustomFrequency = false;
            this.AnimActiveIcon.UseCustomRow = false;
            this.AnimActiveIcon.AnimationLoading += new System.EventHandler(this.AnimActiveIcon_AnimationLoading);
            // 
            // AnimIcon
            // 
            this.AnimIcon.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimIcon.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.IconAnimation;
            resources.ApplyResources(this.AnimIcon, "AnimIcon");
            this.AnimIcon.BackColor = System.Drawing.Color.LightGray;
            this.AnimIcon.Frequency = 100;
            this.AnimIcon.LinkToAnimationManager = true;
            this.AnimIcon.Name = "AnimIcon";
            this.AnimIcon.OriginPoint = true;
            this.AnimIcon.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimIcon.Row = 0;
            this.AnimIcon.UseCustomFrequency = false;
            this.AnimIcon.UseCustomRow = false;
            this.AnimIcon.AnimationLoading += new System.EventHandler(this.AnimIcon_AnimationLoading);
            // 
            // lblIcon
            // 
            resources.ApplyResources(this.lblIcon, "lblIcon");
            this.lblIcon.Name = "lblIcon";
            // 
            // AnimInventory
            // 
            this.AnimInventory.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimInventory.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.IconAnimation;
            resources.ApplyResources(this.AnimInventory, "AnimInventory");
            this.AnimInventory.BackColor = System.Drawing.Color.LightGray;
            this.AnimInventory.Frequency = 100;
            this.AnimInventory.LinkToAnimationManager = true;
            this.AnimInventory.Name = "AnimInventory";
            this.AnimInventory.OriginPoint = true;
            this.AnimInventory.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimInventory.Row = 0;
            this.AnimInventory.UseCustomFrequency = false;
            this.AnimInventory.UseCustomRow = false;
            this.AnimInventory.AnimationLoading += new System.EventHandler(this.AnimInventory_AnimationLoading);
            // 
            // lblIconInventory
            // 
            resources.ApplyResources(this.lblIconInventory, "lblIconInventory");
            this.lblIconInventory.Name = "lblIconInventory";
            // 
            // grpView
            // 
            this.grpView.Controls.Add(this.listActions);
            this.grpView.Controls.Add(this.ViewScript);
            resources.ApplyResources(this.grpView, "grpView");
            this.grpView.Name = "grpView";
            this.grpView.TabStop = false;
            // 
            // listActions
            // 
            resources.ApplyResources(this.listActions, "listActions");
            this.listActions.FormattingEnabled = true;
            this.listActions.Name = "listActions";
            this.listActions.SelectedIndexChanged += new System.EventHandler(this.listActions_SelectedIndexChanged);
            // 
            // ViewScript
            // 
            resources.ApplyResources(this.ViewScript, "ViewScript");
            this.ViewScript.Name = "ViewScript";
            this.ViewScript.Script = null;
            // 
            // ListItems
            // 
            this.ListItems.CancelDeletion = false;
            this.ListItems.DataSource = null;
            resources.ApplyResources(this.ListItems, "ListItems");
            this.ListItems.DoubleClickable = false;
            this.ListItems.HideButtons = false;
            this.ListItems.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListItems.Name = "ListItems";
            this.ListItems.Title = "Items";
            this.ListItems.ItemChosen += new System.EventHandler(this.ListItems_ItemChosen);
            this.ListItems.ItemToCreate += new System.EventHandler(this.ListItems_ItemToCreate);
            this.ListItems.ItemToDelete += new System.EventHandler(this.ListItems_ItemToDelete);
            this.ListItems.ListIsEmpty += new System.EventHandler(this.ListItems_ListIsEmpty);
            // 
            // DatabaseItems
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpView);
            this.Controls.Add(this.grpIcons);
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListItems);
            this.Name = "DatabaseItems";
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            this.grpIcons.ResumeLayout(false);
            this.grpIcons.PerformLayout();
            this.grpView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListItems;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox grpIcons;
        private System.Windows.Forms.Label lblIcon;
        private UserControls.AnimationControl AnimInventory;
        private System.Windows.Forms.Label lblIconInventory;
        private System.Windows.Forms.Label lblActiveIcon;
        private UserControls.AnimationControl AnimActiveIcon;
        private UserControls.AnimationControl AnimIcon;
        private System.Windows.Forms.GroupBox grpView;
        private UserControls.ScriptManager ViewScript;
        private System.Windows.Forms.ListBox listActions;
    }
}
