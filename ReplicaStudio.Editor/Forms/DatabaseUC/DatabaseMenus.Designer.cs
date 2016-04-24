namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseMenus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseMenus));
            this.grpMainMenu = new System.Windows.Forms.GroupBox();
            this.AnimMainMenu = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.chkActivateMain = new System.Windows.Forms.CheckBox();
            this.grpInGameMenu = new System.Windows.Forms.GroupBox();
            this.chkSaveMenu = new System.Windows.Forms.CheckBox();
            this.chkLoadingMenu = new System.Windows.Forms.CheckBox();
            this.chkEchapMenu = new System.Windows.Forms.CheckBox();
            this.lblEchapMenu = new System.Windows.Forms.Label();
            this.lblSaveMenu = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.grpInventory = new System.Windows.Forms.GroupBox();
            this.ddpGridHeight = new System.Windows.Forms.NumericUpDown();
            this.ddpGridWidth = new System.Windows.Forms.NumericUpDown();
            this.ddpItemHeight = new System.Windows.Forms.NumericUpDown();
            this.ddpItemWidth = new System.Windows.Forms.NumericUpDown();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.lblItemSize = new System.Windows.Forms.Label();
            this.crdInventoryBackground = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.lblInventoryBackground = new System.Windows.Forms.Label();
            this.crdInventoryPosition = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.crdBackButtonPosition = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.lblBackButtonPosition = new System.Windows.Forms.Label();
            this.AnimBackButton = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.lblBackButton = new System.Windows.Forms.Label();
            this.lblInventoryTablePosition = new System.Windows.Forms.Label();
            this.AnimInventoryMenu = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpMainMenu.SuspendLayout();
            this.grpInGameMenu.SuspendLayout();
            this.grpInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpGridWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpItemHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpItemWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMainMenu
            // 
            this.grpMainMenu.Controls.Add(this.AnimMainMenu);
            this.grpMainMenu.Controls.Add(this.chkActivateMain);
            resources.ApplyResources(this.grpMainMenu, "grpMainMenu");
            this.grpMainMenu.Name = "grpMainMenu";
            this.grpMainMenu.TabStop = false;
            // 
            // AnimMainMenu
            // 
            this.AnimMainMenu.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimMainMenu.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.Menu;
            resources.ApplyResources(this.AnimMainMenu, "AnimMainMenu");
            this.AnimMainMenu.BackColor = System.Drawing.Color.LightGray;
            this.AnimMainMenu.Frequency = 100;
            this.AnimMainMenu.LinkToAnimationManager = true;
            this.AnimMainMenu.Name = "AnimMainMenu";
            this.AnimMainMenu.OriginPoint = false;
            this.AnimMainMenu.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimMainMenu.Row = 0;
            this.AnimMainMenu.UseCustomFrequency = false;
            this.AnimMainMenu.UseCustomRow = false;
            // 
            // chkActivateMain
            // 
            resources.ApplyResources(this.chkActivateMain, "chkActivateMain");
            this.chkActivateMain.Name = "chkActivateMain";
            this.chkActivateMain.UseVisualStyleBackColor = true;
            // 
            // grpInGameMenu
            // 
            this.grpInGameMenu.Controls.Add(this.chkSaveMenu);
            this.grpInGameMenu.Controls.Add(this.chkLoadingMenu);
            this.grpInGameMenu.Controls.Add(this.chkEchapMenu);
            this.grpInGameMenu.Controls.Add(this.lblEchapMenu);
            this.grpInGameMenu.Controls.Add(this.lblSaveMenu);
            this.grpInGameMenu.Controls.Add(this.lblLoading);
            resources.ApplyResources(this.grpInGameMenu, "grpInGameMenu");
            this.grpInGameMenu.Name = "grpInGameMenu";
            this.grpInGameMenu.TabStop = false;
            // 
            // chkSaveMenu
            // 
            resources.ApplyResources(this.chkSaveMenu, "chkSaveMenu");
            this.chkSaveMenu.Name = "chkSaveMenu";
            this.chkSaveMenu.UseVisualStyleBackColor = true;
            // 
            // chkLoadingMenu
            // 
            resources.ApplyResources(this.chkLoadingMenu, "chkLoadingMenu");
            this.chkLoadingMenu.Name = "chkLoadingMenu";
            this.chkLoadingMenu.UseVisualStyleBackColor = true;
            // 
            // chkEchapMenu
            // 
            resources.ApplyResources(this.chkEchapMenu, "chkEchapMenu");
            this.chkEchapMenu.Name = "chkEchapMenu";
            this.chkEchapMenu.UseVisualStyleBackColor = true;
            // 
            // lblEchapMenu
            // 
            resources.ApplyResources(this.lblEchapMenu, "lblEchapMenu");
            this.lblEchapMenu.Name = "lblEchapMenu";
            // 
            // lblSaveMenu
            // 
            resources.ApplyResources(this.lblSaveMenu, "lblSaveMenu");
            this.lblSaveMenu.Name = "lblSaveMenu";
            // 
            // lblLoading
            // 
            resources.ApplyResources(this.lblLoading, "lblLoading");
            this.lblLoading.Name = "lblLoading";
            // 
            // grpInventory
            // 
            this.grpInventory.Controls.Add(this.ddpGridHeight);
            this.grpInventory.Controls.Add(this.ddpGridWidth);
            this.grpInventory.Controls.Add(this.ddpItemHeight);
            this.grpInventory.Controls.Add(this.ddpItemWidth);
            this.grpInventory.Controls.Add(this.lblGridSize);
            this.grpInventory.Controls.Add(this.lblItemSize);
            this.grpInventory.Controls.Add(this.crdInventoryBackground);
            this.grpInventory.Controls.Add(this.lblInventoryBackground);
            this.grpInventory.Controls.Add(this.crdInventoryPosition);
            this.grpInventory.Controls.Add(this.crdBackButtonPosition);
            this.grpInventory.Controls.Add(this.lblBackButtonPosition);
            this.grpInventory.Controls.Add(this.AnimBackButton);
            this.grpInventory.Controls.Add(this.lblBackButton);
            this.grpInventory.Controls.Add(this.lblInventoryTablePosition);
            this.grpInventory.Controls.Add(this.AnimInventoryMenu);
            resources.ApplyResources(this.grpInventory, "grpInventory");
            this.grpInventory.Name = "grpInventory";
            this.grpInventory.TabStop = false;
            // 
            // ddpGridHeight
            // 
            resources.ApplyResources(this.ddpGridHeight, "ddpGridHeight");
            this.ddpGridHeight.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ddpGridHeight.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ddpGridHeight.Name = "ddpGridHeight";
            this.ddpGridHeight.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // ddpGridWidth
            // 
            resources.ApplyResources(this.ddpGridWidth, "ddpGridWidth");
            this.ddpGridWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ddpGridWidth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ddpGridWidth.Name = "ddpGridWidth";
            this.ddpGridWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // ddpItemHeight
            // 
            resources.ApplyResources(this.ddpItemHeight, "ddpItemHeight");
            this.ddpItemHeight.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.ddpItemHeight.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ddpItemHeight.Name = "ddpItemHeight";
            this.ddpItemHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // ddpItemWidth
            // 
            resources.ApplyResources(this.ddpItemWidth, "ddpItemWidth");
            this.ddpItemWidth.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.ddpItemWidth.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ddpItemWidth.Name = "ddpItemWidth";
            this.ddpItemWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // lblGridSize
            // 
            resources.ApplyResources(this.lblGridSize, "lblGridSize");
            this.lblGridSize.Name = "lblGridSize";
            // 
            // lblItemSize
            // 
            resources.ApplyResources(this.lblItemSize, "lblItemSize");
            this.lblItemSize.Name = "lblItemSize";
            // 
            // crdInventoryBackground
            // 
            this.crdInventoryBackground.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdInventoryBackground.FullCoords = null;
            resources.ApplyResources(this.crdInventoryBackground, "crdInventoryBackground");
            this.crdInventoryBackground.Name = "crdInventoryBackground";
            this.crdInventoryBackground.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdInventoryBackground.UseStageBackground = false;
            this.crdInventoryBackground.UseStages = false;
            this.crdInventoryBackground.ValueChanged += new System.EventHandler(this.crdInventoryBackground_ValueChanged);
            // 
            // lblInventoryBackground
            // 
            resources.ApplyResources(this.lblInventoryBackground, "lblInventoryBackground");
            this.lblInventoryBackground.Name = "lblInventoryBackground";
            // 
            // crdInventoryPosition
            // 
            this.crdInventoryPosition.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdInventoryPosition.FullCoords = null;
            resources.ApplyResources(this.crdInventoryPosition, "crdInventoryPosition");
            this.crdInventoryPosition.Name = "crdInventoryPosition";
            this.crdInventoryPosition.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdInventoryPosition.UseStageBackground = false;
            this.crdInventoryPosition.UseStages = false;
            this.crdInventoryPosition.ValueChanged += new System.EventHandler(this.crdInventoryPosition_ValueChanged);
            // 
            // crdBackButtonPosition
            // 
            this.crdBackButtonPosition.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdBackButtonPosition.FullCoords = null;
            resources.ApplyResources(this.crdBackButtonPosition, "crdBackButtonPosition");
            this.crdBackButtonPosition.Name = "crdBackButtonPosition";
            this.crdBackButtonPosition.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdBackButtonPosition.UseStageBackground = false;
            this.crdBackButtonPosition.UseStages = false;
            this.crdBackButtonPosition.ValueChanged += new System.EventHandler(this.crdBackButtonPosition_ValueChanged);
            // 
            // lblBackButtonPosition
            // 
            resources.ApplyResources(this.lblBackButtonPosition, "lblBackButtonPosition");
            this.lblBackButtonPosition.Name = "lblBackButtonPosition";
            // 
            // AnimBackButton
            // 
            this.AnimBackButton.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimBackButton.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.Menu;
            resources.ApplyResources(this.AnimBackButton, "AnimBackButton");
            this.AnimBackButton.BackColor = System.Drawing.Color.LightGray;
            this.AnimBackButton.Frequency = 100;
            this.AnimBackButton.LinkToAnimationManager = true;
            this.AnimBackButton.Name = "AnimBackButton";
            this.AnimBackButton.OriginPoint = false;
            this.AnimBackButton.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimBackButton.Row = 0;
            this.AnimBackButton.UseCustomFrequency = false;
            this.AnimBackButton.UseCustomRow = false;
            // 
            // lblBackButton
            // 
            resources.ApplyResources(this.lblBackButton, "lblBackButton");
            this.lblBackButton.Name = "lblBackButton";
            // 
            // lblInventoryTablePosition
            // 
            resources.ApplyResources(this.lblInventoryTablePosition, "lblInventoryTablePosition");
            this.lblInventoryTablePosition.Name = "lblInventoryTablePosition";
            // 
            // AnimInventoryMenu
            // 
            this.AnimInventoryMenu.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimInventoryMenu.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.Menu;
            resources.ApplyResources(this.AnimInventoryMenu, "AnimInventoryMenu");
            this.AnimInventoryMenu.BackColor = System.Drawing.Color.LightGray;
            this.AnimInventoryMenu.Frequency = 100;
            this.AnimInventoryMenu.LinkToAnimationManager = true;
            this.AnimInventoryMenu.Name = "AnimInventoryMenu";
            this.AnimInventoryMenu.OriginPoint = false;
            this.AnimInventoryMenu.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimInventoryMenu.Row = 0;
            this.AnimInventoryMenu.UseCustomFrequency = false;
            this.AnimInventoryMenu.UseCustomRow = false;
            // 
            // DatabaseMenus
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpInventory);
            this.Controls.Add(this.grpInGameMenu);
            this.Controls.Add(this.grpMainMenu);
            this.Name = "DatabaseMenus";
            this.grpMainMenu.ResumeLayout(false);
            this.grpMainMenu.PerformLayout();
            this.grpInGameMenu.ResumeLayout(false);
            this.grpInGameMenu.PerformLayout();
            this.grpInventory.ResumeLayout(false);
            this.grpInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpGridWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpItemHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpItemWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMainMenu;
        private UserControls.AnimationControl AnimMainMenu;
        private System.Windows.Forms.CheckBox chkActivateMain;
        private System.Windows.Forms.GroupBox grpInGameMenu;
        private System.Windows.Forms.CheckBox chkSaveMenu;
        private System.Windows.Forms.CheckBox chkLoadingMenu;
        private System.Windows.Forms.CheckBox chkEchapMenu;
        private System.Windows.Forms.Label lblEchapMenu;
        private System.Windows.Forms.Label lblSaveMenu;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.GroupBox grpInventory;
        private UserControls.AnimationControl AnimInventoryMenu;
        private System.Windows.Forms.Label lblBackButton;
        private System.Windows.Forms.Label lblInventoryTablePosition;
        private System.Windows.Forms.Label lblBackButtonPosition;
        private UserControls.AnimationControl AnimBackButton;
        private UserControls.CoordsButton crdInventoryPosition;
        private UserControls.CoordsButton crdBackButtonPosition;
        private UserControls.CoordsButton crdInventoryBackground;
        private System.Windows.Forms.Label lblInventoryBackground;
        private System.Windows.Forms.Label lblGridSize;
        private System.Windows.Forms.Label lblItemSize;
        private System.Windows.Forms.NumericUpDown ddpGridHeight;
        private System.Windows.Forms.NumericUpDown ddpGridWidth;
        private System.Windows.Forms.NumericUpDown ddpItemHeight;
        private System.Windows.Forms.NumericUpDown ddpItemWidth;
    }
}
