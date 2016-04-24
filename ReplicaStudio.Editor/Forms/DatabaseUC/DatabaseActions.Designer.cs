namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseActions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseActions));
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpIcons = new System.Windows.Forms.GroupBox();
            this.lblActiveIcon = new System.Windows.Forms.Label();
            this.AnimActiveIcon = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.AnimIcon = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.lblIcon = new System.Windows.Forms.Label();
            this.AnimInventory = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.lblIconInventory = new System.Windows.Forms.Label();
            this.ListActions = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpInformations.SuspendLayout();
            this.grpIcons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInformations
            // 
            this.grpInformations.Controls.Add(this.txtDescription);
            this.grpInformations.Controls.Add(this.lblDescription);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.label1);
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // ListActions
            // 
            this.ListActions.CancelDeletion = false;
            this.ListActions.DataSource = null;
            resources.ApplyResources(this.ListActions, "ListActions");
            this.ListActions.DoubleClickable = false;
            this.ListActions.HideButtons = false;
            this.ListActions.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListActions.Name = "ListActions";
            this.ListActions.Title = "Actions";
            this.ListActions.ItemChosen += new System.EventHandler(this.ListActions_ItemChosen);
            this.ListActions.ItemToCreate += new System.EventHandler(this.ListActions_ItemToCreate);
            this.ListActions.ItemToDelete += new System.EventHandler(this.ListActions_ItemToDelete);
            this.ListActions.ListIsEmpty += new System.EventHandler(this.ListActions_ListIsEmpty);
            // 
            // DatabaseActions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpIcons);
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListActions);
            this.Name = "DatabaseActions";
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            this.grpIcons.ResumeLayout(false);
            this.grpIcons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListActions;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox grpIcons;
        private System.Windows.Forms.Label lblActiveIcon;
        private UserControls.AnimationControl AnimActiveIcon;
        private UserControls.AnimationControl AnimIcon;
        private System.Windows.Forms.Label lblIcon;
        private UserControls.AnimationControl AnimInventory;
        private System.Windows.Forms.Label lblIconInventory;
    }
}
