namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabasePlayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabasePlayers));
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.ddpCharacterTemplate = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCharacterTemplate = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.grpAnimations = new System.Windows.Forms.GroupBox();
            this.crdStartingPosition = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.lblStartingPosition = new System.Windows.Forms.Label();
            this.ddpStartingDirection = new System.Windows.Forms.ComboBox();
            this.lblStartingDirection = new System.Windows.Forms.Label();
            this.AnimCharacter = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpLife = new System.Windows.Forms.GroupBox();
            this.ddpPVMax = new System.Windows.Forms.NumericUpDown();
            this.lblPVMax = new System.Windows.Forms.Label();
            this.lblPVStarting = new System.Windows.Forms.Label();
            this.ddpPVStarting = new System.Windows.Forms.NumericUpDown();
            this.chkLife = new System.Windows.Forms.CheckBox();
            this.grpInteractions = new System.Windows.Forms.GroupBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.ListAvailableItems = new System.Windows.Forms.ComboBox();
            this.ListSelectedItems = new System.Windows.Forms.ListBox();
            this.lblItems = new System.Windows.Forms.Label();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.ListAvailableActions = new System.Windows.Forms.ComboBox();
            this.ListSelectedActions = new System.Windows.Forms.ListBox();
            this.lblActions = new System.Windows.Forms.Label();
            this.ListPlayers = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpInformations.SuspendLayout();
            this.grpAnimations.SuspendLayout();
            this.grpLife.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpPVMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpPVStarting)).BeginInit();
            this.grpInteractions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInformations
            // 
            this.grpInformations.Controls.Add(this.ddpCharacterTemplate);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblCharacterTemplate);
            this.grpInformations.Controls.Add(this.lblName);
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // ddpCharacterTemplate
            // 
            this.ddpCharacterTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpCharacterTemplate.FormattingEnabled = true;
            resources.ApplyResources(this.ddpCharacterTemplate, "ddpCharacterTemplate");
            this.ddpCharacterTemplate.Name = "ddpCharacterTemplate";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // lblCharacterTemplate
            // 
            resources.ApplyResources(this.lblCharacterTemplate, "lblCharacterTemplate");
            this.lblCharacterTemplate.Name = "lblCharacterTemplate";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // grpAnimations
            // 
            this.grpAnimations.Controls.Add(this.crdStartingPosition);
            this.grpAnimations.Controls.Add(this.lblStartingPosition);
            this.grpAnimations.Controls.Add(this.ddpStartingDirection);
            this.grpAnimations.Controls.Add(this.lblStartingDirection);
            this.grpAnimations.Controls.Add(this.AnimCharacter);
            resources.ApplyResources(this.grpAnimations, "grpAnimations");
            this.grpAnimations.Name = "grpAnimations";
            this.grpAnimations.TabStop = false;
            // 
            // crdStartingPosition
            // 
            this.crdStartingPosition.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdStartingPosition.FullCoords = null;
            resources.ApplyResources(this.crdStartingPosition, "crdStartingPosition");
            this.crdStartingPosition.Name = "crdStartingPosition";
            this.crdStartingPosition.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdStartingPosition.UseStageBackground = true;
            this.crdStartingPosition.UseStages = true;
            // 
            // lblStartingPosition
            // 
            resources.ApplyResources(this.lblStartingPosition, "lblStartingPosition");
            this.lblStartingPosition.Name = "lblStartingPosition";
            // 
            // ddpStartingDirection
            // 
            this.ddpStartingDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpStartingDirection.FormattingEnabled = true;
            resources.ApplyResources(this.ddpStartingDirection, "ddpStartingDirection");
            this.ddpStartingDirection.Name = "ddpStartingDirection";
            // 
            // lblStartingDirection
            // 
            resources.ApplyResources(this.lblStartingDirection, "lblStartingDirection");
            this.lblStartingDirection.Name = "lblStartingDirection";
            // 
            // AnimCharacter
            // 
            this.AnimCharacter.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimCharacter.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.CharacterAnimation;
            resources.ApplyResources(this.AnimCharacter, "AnimCharacter");
            this.AnimCharacter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnimCharacter.Frequency = 100;
            this.AnimCharacter.LinkToAnimationManager = false;
            this.AnimCharacter.Name = "AnimCharacter";
            this.AnimCharacter.OriginPoint = true;
            this.AnimCharacter.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimCharacter.Row = 0;
            this.AnimCharacter.UseCustomFrequency = false;
            this.AnimCharacter.UseCustomRow = false;
            // 
            // grpLife
            // 
            this.grpLife.Controls.Add(this.ddpPVMax);
            this.grpLife.Controls.Add(this.lblPVMax);
            this.grpLife.Controls.Add(this.lblPVStarting);
            this.grpLife.Controls.Add(this.ddpPVStarting);
            this.grpLife.Controls.Add(this.chkLife);
            resources.ApplyResources(this.grpLife, "grpLife");
            this.grpLife.Name = "grpLife";
            this.grpLife.TabStop = false;
            // 
            // ddpPVMax
            // 
            resources.ApplyResources(this.ddpPVMax, "ddpPVMax");
            this.ddpPVMax.Name = "ddpPVMax";
            this.ddpPVMax.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblPVMax
            // 
            resources.ApplyResources(this.lblPVMax, "lblPVMax");
            this.lblPVMax.Name = "lblPVMax";
            // 
            // lblPVStarting
            // 
            resources.ApplyResources(this.lblPVStarting, "lblPVStarting");
            this.lblPVStarting.Name = "lblPVStarting";
            // 
            // ddpPVStarting
            // 
            resources.ApplyResources(this.ddpPVStarting, "ddpPVStarting");
            this.ddpPVStarting.Name = "ddpPVStarting";
            this.ddpPVStarting.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // chkLife
            // 
            resources.ApplyResources(this.chkLife, "chkLife");
            this.chkLife.Name = "chkLife";
            this.chkLife.UseVisualStyleBackColor = true;
            // 
            // grpInteractions
            // 
            this.grpInteractions.Controls.Add(this.btnAddItem);
            this.grpInteractions.Controls.Add(this.ListAvailableItems);
            this.grpInteractions.Controls.Add(this.ListSelectedItems);
            this.grpInteractions.Controls.Add(this.lblItems);
            this.grpInteractions.Controls.Add(this.btnAddAction);
            this.grpInteractions.Controls.Add(this.ListAvailableActions);
            this.grpInteractions.Controls.Add(this.ListSelectedActions);
            this.grpInteractions.Controls.Add(this.lblActions);
            resources.ApplyResources(this.grpInteractions, "grpInteractions");
            this.grpInteractions.Name = "grpInteractions";
            this.grpInteractions.TabStop = false;
            // 
            // btnAddItem
            // 
            resources.ApplyResources(this.btnAddItem, "btnAddItem");
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // ListAvailableItems
            // 
            this.ListAvailableItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ListAvailableItems.FormattingEnabled = true;
            resources.ApplyResources(this.ListAvailableItems, "ListAvailableItems");
            this.ListAvailableItems.Name = "ListAvailableItems";
            // 
            // ListSelectedItems
            // 
            this.ListSelectedItems.FormattingEnabled = true;
            resources.ApplyResources(this.ListSelectedItems, "ListSelectedItems");
            this.ListSelectedItems.Name = "ListSelectedItems";
            this.ListSelectedItems.DoubleClick += new System.EventHandler(this.ListSelectedItems_DoubleClick);
            // 
            // lblItems
            // 
            resources.ApplyResources(this.lblItems, "lblItems");
            this.lblItems.Name = "lblItems";
            // 
            // btnAddAction
            // 
            resources.ApplyResources(this.btnAddAction, "btnAddAction");
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            // 
            // ListAvailableActions
            // 
            this.ListAvailableActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ListAvailableActions.FormattingEnabled = true;
            resources.ApplyResources(this.ListAvailableActions, "ListAvailableActions");
            this.ListAvailableActions.Name = "ListAvailableActions";
            this.ListAvailableActions.VisibleChanged += new System.EventHandler(this.btnAddAction_VisibleChanged);
            // 
            // ListSelectedActions
            // 
            this.ListSelectedActions.FormattingEnabled = true;
            resources.ApplyResources(this.ListSelectedActions, "ListSelectedActions");
            this.ListSelectedActions.Name = "ListSelectedActions";
            this.ListSelectedActions.DoubleClick += new System.EventHandler(this.ListSelectedActions_DoubleClick);
            // 
            // lblActions
            // 
            resources.ApplyResources(this.lblActions, "lblActions");
            this.lblActions.Name = "lblActions";
            // 
            // ListPlayers
            // 
            this.ListPlayers.CancelDeletion = false;
            this.ListPlayers.DataSource = null;
            resources.ApplyResources(this.ListPlayers, "ListPlayers");
            this.ListPlayers.DoubleClickable = false;
            this.ListPlayers.HideButtons = false;
            this.ListPlayers.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListPlayers.Name = "ListPlayers";
            this.ListPlayers.Title = "Playable Characters";
            this.ListPlayers.ItemChosen += new System.EventHandler(this.ListCharacters_ItemChosen);
            this.ListPlayers.ItemToCreate += new System.EventHandler(this.ListCharacters_ItemToCreate);
            this.ListPlayers.ItemToDelete += new System.EventHandler(this.ListCharacters_ItemToDelete);
            this.ListPlayers.ListIsEmpty += new System.EventHandler(this.ListCharacters_ListIsEmpty);
            // 
            // DatabasePlayers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpInteractions);
            this.Controls.Add(this.ListPlayers);
            this.Controls.Add(this.grpLife);
            this.Controls.Add(this.grpAnimations);
            this.Controls.Add(this.grpInformations);
            this.Name = "DatabasePlayers";
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            this.grpAnimations.ResumeLayout(false);
            this.grpAnimations.PerformLayout();
            this.grpLife.ResumeLayout(false);
            this.grpLife.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpPVMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpPVStarting)).EndInit();
            this.grpInteractions.ResumeLayout(false);
            this.grpInteractions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox grpAnimations;
        private System.Windows.Forms.GroupBox grpLife;
        private System.Windows.Forms.CheckBox chkLife;
        private System.Windows.Forms.NumericUpDown ddpPVMax;
        private System.Windows.Forms.Label lblPVMax;
        private System.Windows.Forms.Label lblPVStarting;
        private System.Windows.Forms.NumericUpDown ddpPVStarting;
        private UserControls.AnimationControl AnimCharacter;
        private System.Windows.Forms.ComboBox ddpStartingDirection;
        private System.Windows.Forms.Label lblStartingDirection;
        private UserControls.ListItems ListPlayers;
        private System.Windows.Forms.Label lblStartingPosition;
        private System.Windows.Forms.ComboBox ddpCharacterTemplate;
        private System.Windows.Forms.Label lblCharacterTemplate;
        private System.Windows.Forms.GroupBox grpInteractions;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ComboBox ListAvailableItems;
        private System.Windows.Forms.ListBox ListSelectedItems;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.ComboBox ListAvailableActions;
        private System.Windows.Forms.ListBox ListSelectedActions;
        private System.Windows.Forms.Label lblActions;
        private UserControls.CoordsButton crdStartingPosition;
    }
}
