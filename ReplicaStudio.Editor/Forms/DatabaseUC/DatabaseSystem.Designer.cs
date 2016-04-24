namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseSystem));
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.ddpCharacterStart = new System.Windows.Forms.ComboBox();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.grpLoadingScreen = new System.Windows.Forms.GroupBox();
            this.AnimLoading = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpGame = new System.Windows.Forms.GroupBox();
            this.crdLifeBar = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.rscLifeBarDeco = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.rscLifeBar = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.lblLifeBarPosition = new System.Windows.Forms.Label();
            this.lblLifeBarDeco = new System.Windows.Forms.Label();
            this.lblLifeBar = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGameOverMusic = new System.Windows.Forms.Label();
            this.grpGameOver = new System.Windows.Forms.GroupBox();
            this.ScriptManager = new ReplicaStudio.Editor.Forms.UserControls.ScriptManager();
            this.grpMusics = new System.Windows.Forms.GroupBox();
            this.rscMainMenuMusic = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.lblMainMusic = new System.Windows.Forms.Label();
            this.rscGameOverMusic = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.grpParams = new System.Windows.Forms.GroupBox();
            this.rscSystemGUI = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.lblSystemGUI = new System.Windows.Forms.Label();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.radMov8 = new System.Windows.Forms.RadioButton();
            this.radMov4 = new System.Windows.Forms.RadioButton();
            this.lblCharacterMovements = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpSounds = new System.Windows.Forms.GroupBox();
            this.rscChoiceButtonSound = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.lblChoiceButton = new System.Windows.Forms.Label();
            this.rscMovementButtonSound = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.lblMovButton = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPrincipalMusic = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resourceButton2 = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.resourceButton3 = new ReplicaStudio.Editor.Forms.UserControls.ResourceButton();
            this.grpStart.SuspendLayout();
            this.grpLoadingScreen.SuspendLayout();
            this.grpGame.SuspendLayout();
            this.grpGameOver.SuspendLayout();
            this.grpMusics.SuspendLayout();
            this.grpParams.SuspendLayout();
            this.grpSounds.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStart
            // 
            this.grpStart.Controls.Add(this.ddpCharacterStart);
            this.grpStart.Controls.Add(this.lblCharacter);
            resources.ApplyResources(this.grpStart, "grpStart");
            this.grpStart.Name = "grpStart";
            this.grpStart.TabStop = false;
            // 
            // ddpCharacterStart
            // 
            this.ddpCharacterStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpCharacterStart.FormattingEnabled = true;
            resources.ApplyResources(this.ddpCharacterStart, "ddpCharacterStart");
            this.ddpCharacterStart.Name = "ddpCharacterStart";
            // 
            // lblCharacter
            // 
            resources.ApplyResources(this.lblCharacter, "lblCharacter");
            this.lblCharacter.Name = "lblCharacter";
            // 
            // grpLoadingScreen
            // 
            this.grpLoadingScreen.Controls.Add(this.AnimLoading);
            resources.ApplyResources(this.grpLoadingScreen, "grpLoadingScreen");
            this.grpLoadingScreen.Name = "grpLoadingScreen";
            this.grpLoadingScreen.TabStop = false;
            // 
            // AnimLoading
            // 
            this.AnimLoading.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimLoading.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.Menu;
            resources.ApplyResources(this.AnimLoading, "AnimLoading");
            this.AnimLoading.BackColor = System.Drawing.Color.LightGray;
            this.AnimLoading.Frequency = 100;
            this.AnimLoading.LinkToAnimationManager = true;
            this.AnimLoading.Name = "AnimLoading";
            this.AnimLoading.OriginPoint = false;
            this.AnimLoading.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimLoading.Row = 0;
            this.AnimLoading.UseCustomFrequency = false;
            this.AnimLoading.UseCustomRow = false;
            this.AnimLoading.AnimationLoading += new System.EventHandler(this.AnimLoading_AnimationLoading);
            // 
            // grpGame
            // 
            this.grpGame.Controls.Add(this.crdLifeBar);
            this.grpGame.Controls.Add(this.rscLifeBarDeco);
            this.grpGame.Controls.Add(this.rscLifeBar);
            this.grpGame.Controls.Add(this.lblLifeBarPosition);
            this.grpGame.Controls.Add(this.lblLifeBarDeco);
            this.grpGame.Controls.Add(this.lblLifeBar);
            this.grpGame.Controls.Add(this.txtAuthor);
            this.grpGame.Controls.Add(this.lblAuthor);
            this.grpGame.Controls.Add(this.txtTitle);
            this.grpGame.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.grpGame, "grpGame");
            this.grpGame.Name = "grpGame";
            this.grpGame.TabStop = false;
            // 
            // crdLifeBar
            // 
            this.crdLifeBar.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdLifeBar.FullCoords = null;
            resources.ApplyResources(this.crdLifeBar, "crdLifeBar");
            this.crdLifeBar.Name = "crdLifeBar";
            this.crdLifeBar.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdLifeBar.UseStageBackground = false;
            this.crdLifeBar.UseStages = false;
            // 
            // rscLifeBarDeco
            // 
            this.rscLifeBarDeco.Filter = null;
            resources.ApplyResources(this.rscLifeBarDeco, "rscLifeBarDeco");
            this.rscLifeBarDeco.Name = "rscLifeBarDeco";
            this.rscLifeBarDeco.ResourceString = null;
            // 
            // rscLifeBar
            // 
            this.rscLifeBar.Filter = null;
            resources.ApplyResources(this.rscLifeBar, "rscLifeBar");
            this.rscLifeBar.Name = "rscLifeBar";
            this.rscLifeBar.ResourceString = null;
            // 
            // lblLifeBarPosition
            // 
            resources.ApplyResources(this.lblLifeBarPosition, "lblLifeBarPosition");
            this.lblLifeBarPosition.Name = "lblLifeBarPosition";
            // 
            // lblLifeBarDeco
            // 
            resources.ApplyResources(this.lblLifeBarDeco, "lblLifeBarDeco");
            this.lblLifeBarDeco.Name = "lblLifeBarDeco";
            // 
            // lblLifeBar
            // 
            resources.ApplyResources(this.lblLifeBar, "lblLifeBar");
            this.lblLifeBar.Name = "lblLifeBar";
            // 
            // txtAuthor
            // 
            resources.ApplyResources(this.txtAuthor, "txtAuthor");
            this.txtAuthor.Name = "txtAuthor";
            // 
            // lblAuthor
            // 
            resources.ApplyResources(this.lblAuthor, "lblAuthor");
            this.lblAuthor.Name = "lblAuthor";
            // 
            // txtTitle
            // 
            resources.ApplyResources(this.txtTitle, "txtTitle");
            this.txtTitle.Name = "txtTitle";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblGameOverMusic
            // 
            resources.ApplyResources(this.lblGameOverMusic, "lblGameOverMusic");
            this.lblGameOverMusic.Name = "lblGameOverMusic";
            // 
            // grpGameOver
            // 
            this.grpGameOver.Controls.Add(this.ScriptManager);
            resources.ApplyResources(this.grpGameOver, "grpGameOver");
            this.grpGameOver.Name = "grpGameOver";
            this.grpGameOver.TabStop = false;
            // 
            // ScriptManager
            // 
            resources.ApplyResources(this.ScriptManager, "ScriptManager");
            this.ScriptManager.Name = "ScriptManager";
            this.ScriptManager.Script = null;
            // 
            // grpMusics
            // 
            this.grpMusics.Controls.Add(this.rscMainMenuMusic);
            this.grpMusics.Controls.Add(this.lblMainMusic);
            this.grpMusics.Controls.Add(this.rscGameOverMusic);
            this.grpMusics.Controls.Add(this.lblGameOverMusic);
            resources.ApplyResources(this.grpMusics, "grpMusics");
            this.grpMusics.Name = "grpMusics";
            this.grpMusics.TabStop = false;
            // 
            // rscMainMenuMusic
            // 
            this.rscMainMenuMusic.Filter = null;
            resources.ApplyResources(this.rscMainMenuMusic, "rscMainMenuMusic");
            this.rscMainMenuMusic.Name = "rscMainMenuMusic";
            this.rscMainMenuMusic.ResourceString = null;
            // 
            // lblMainMusic
            // 
            resources.ApplyResources(this.lblMainMusic, "lblMainMusic");
            this.lblMainMusic.Name = "lblMainMusic";
            // 
            // rscGameOverMusic
            // 
            this.rscGameOverMusic.Filter = null;
            resources.ApplyResources(this.rscGameOverMusic, "rscGameOverMusic");
            this.rscGameOverMusic.Name = "rscGameOverMusic";
            this.rscGameOverMusic.ResourceString = null;
            // 
            // grpParams
            // 
            this.grpParams.Controls.Add(this.rscSystemGUI);
            this.grpParams.Controls.Add(this.lblSystemGUI);
            this.grpParams.Controls.Add(this.txtResolution);
            this.grpParams.Controls.Add(this.radMov8);
            this.grpParams.Controls.Add(this.radMov4);
            this.grpParams.Controls.Add(this.lblCharacterMovements);
            this.grpParams.Controls.Add(this.label2);
            resources.ApplyResources(this.grpParams, "grpParams");
            this.grpParams.Name = "grpParams";
            this.grpParams.TabStop = false;
            // 
            // rscSystemGUI
            // 
            this.rscSystemGUI.Filter = null;
            resources.ApplyResources(this.rscSystemGUI, "rscSystemGUI");
            this.rscSystemGUI.Name = "rscSystemGUI";
            this.rscSystemGUI.ResourceString = null;
            // 
            // lblSystemGUI
            // 
            resources.ApplyResources(this.lblSystemGUI, "lblSystemGUI");
            this.lblSystemGUI.Name = "lblSystemGUI";
            // 
            // txtResolution
            // 
            resources.ApplyResources(this.txtResolution, "txtResolution");
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.VisibleChanged += new System.EventHandler(this.txtResolution_VisibleChanged);
            // 
            // radMov8
            // 
            resources.ApplyResources(this.radMov8, "radMov8");
            this.radMov8.Name = "radMov8";
            this.radMov8.TabStop = true;
            this.radMov8.UseVisualStyleBackColor = true;
            // 
            // radMov4
            // 
            resources.ApplyResources(this.radMov4, "radMov4");
            this.radMov4.Name = "radMov4";
            this.radMov4.TabStop = true;
            this.radMov4.UseVisualStyleBackColor = true;
            // 
            // lblCharacterMovements
            // 
            resources.ApplyResources(this.lblCharacterMovements, "lblCharacterMovements");
            this.lblCharacterMovements.Name = "lblCharacterMovements";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // grpSounds
            // 
            this.grpSounds.Controls.Add(this.rscChoiceButtonSound);
            this.grpSounds.Controls.Add(this.lblChoiceButton);
            this.grpSounds.Controls.Add(this.rscMovementButtonSound);
            this.grpSounds.Controls.Add(this.lblMovButton);
            resources.ApplyResources(this.grpSounds, "grpSounds");
            this.grpSounds.Name = "grpSounds";
            this.grpSounds.TabStop = false;
            // 
            // rscChoiceButtonSound
            // 
            this.rscChoiceButtonSound.Filter = null;
            resources.ApplyResources(this.rscChoiceButtonSound, "rscChoiceButtonSound");
            this.rscChoiceButtonSound.Name = "rscChoiceButtonSound";
            this.rscChoiceButtonSound.ResourceString = null;
            // 
            // lblChoiceButton
            // 
            resources.ApplyResources(this.lblChoiceButton, "lblChoiceButton");
            this.lblChoiceButton.Name = "lblChoiceButton";
            // 
            // rscMovementButtonSound
            // 
            this.rscMovementButtonSound.Filter = null;
            resources.ApplyResources(this.rscMovementButtonSound, "rscMovementButtonSound");
            this.rscMovementButtonSound.Name = "rscMovementButtonSound";
            this.rscMovementButtonSound.ResourceString = null;
            // 
            // lblMovButton
            // 
            resources.ApplyResources(this.lblMovButton, "lblMovButton");
            this.lblMovButton.Name = "lblMovButton";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblPrincipalMusic
            // 
            resources.ApplyResources(this.lblPrincipalMusic, "lblPrincipalMusic");
            this.lblPrincipalMusic.Name = "lblPrincipalMusic";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resourceButton2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.resourceButton3);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // resourceButton2
            // 
            this.resourceButton2.Filter = null;
            resources.ApplyResources(this.resourceButton2, "resourceButton2");
            this.resourceButton2.Name = "resourceButton2";
            this.resourceButton2.ResourceString = null;
            // 
            // resourceButton3
            // 
            this.resourceButton3.Filter = null;
            resources.ApplyResources(this.resourceButton3, "resourceButton3");
            this.resourceButton3.Name = "resourceButton3";
            this.resourceButton3.ResourceString = null;
            // 
            // DatabaseSystem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSounds);
            this.Controls.Add(this.grpParams);
            this.Controls.Add(this.grpMusics);
            this.Controls.Add(this.grpGameOver);
            this.Controls.Add(this.grpGame);
            this.Controls.Add(this.grpLoadingScreen);
            this.Controls.Add(this.grpStart);
            this.Name = "DatabaseSystem";
            this.grpStart.ResumeLayout(false);
            this.grpStart.PerformLayout();
            this.grpLoadingScreen.ResumeLayout(false);
            this.grpLoadingScreen.PerformLayout();
            this.grpGame.ResumeLayout(false);
            this.grpGame.PerformLayout();
            this.grpGameOver.ResumeLayout(false);
            this.grpMusics.ResumeLayout(false);
            this.grpMusics.PerformLayout();
            this.grpParams.ResumeLayout(false);
            this.grpParams.PerformLayout();
            this.grpSounds.ResumeLayout(false);
            this.grpSounds.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.ComboBox ddpCharacterStart;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.GroupBox grpLoadingScreen;
        private UserControls.AnimationControl AnimLoading;
        private System.Windows.Forms.GroupBox grpGame;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.GroupBox grpGameOver;
        private UserControls.ScriptManager ScriptManager;
        private System.Windows.Forms.GroupBox grpMusics;
        private System.Windows.Forms.Label lblGameOverMusic;
        private System.Windows.Forms.Label lblLifeBarPosition;
        private System.Windows.Forms.Label lblLifeBarDeco;
        private System.Windows.Forms.Label lblLifeBar;
        private System.Windows.Forms.GroupBox grpParams;
        private System.Windows.Forms.RadioButton radMov8;
        private System.Windows.Forms.RadioButton radMov4;
        private System.Windows.Forms.Label lblCharacterMovements;
        private System.Windows.Forms.Label label2;
        private UserControls.ResourceButton rscGameOverMusic;
        private UserControls.ResourceButton rscMainMenuMusic;
        private System.Windows.Forms.Label lblMainMusic;
        private System.Windows.Forms.GroupBox grpSounds;
        private UserControls.ResourceButton resourceButton2;
        private System.Windows.Forms.Label label1;
        private UserControls.ResourceButton resourceButton3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPrincipalMusic;
        private UserControls.ResourceButton rscChoiceButtonSound;
        private System.Windows.Forms.Label lblChoiceButton;
        private UserControls.ResourceButton rscMovementButtonSound;
        private System.Windows.Forms.Label lblMovButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.CoordsButton crdLifeBar;
        private UserControls.ResourceButton rscLifeBarDeco;
        private UserControls.ResourceButton rscLifeBar;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.Label lblSystemGUI;
        private UserControls.ResourceButton rscSystemGUI;
    }
}
