namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseCharacters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseCharacters));
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.TalkingFace = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.lblTalkingFace = new System.Windows.Forms.Label();
            this.ddpSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblFace = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.AnimFace = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpAnimations = new System.Windows.Forms.GroupBox();
            this.btnColorPalette = new System.Windows.Forms.Button();
            this.lblPalette = new System.Windows.Forms.Label();
            this.ddpTalkingAnim = new System.Windows.Forms.ComboBox();
            this.lblTalkingAnim = new System.Windows.Forms.Label();
            this.ddpWalkingAnim = new System.Windows.Forms.ComboBox();
            this.lblWalkingAnim = new System.Windows.Forms.Label();
            this.ddpStandingAnim = new System.Windows.Forms.ComboBox();
            this.lblStandingAnim = new System.Windows.Forms.Label();
            this.AnimCharacter = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpDialogs = new System.Windows.Forms.GroupBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnChoose = new System.Windows.Forms.Button();
            this.ddpFont = new System.Windows.Forms.ComboBox();
            this.lblFont = new System.Windows.Forms.Label();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.ListCharacters = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpSpeed)).BeginInit();
            this.grpAnimations.SuspendLayout();
            this.grpDialogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInformations
            // 
            this.grpInformations.Controls.Add(this.TalkingFace);
            this.grpInformations.Controls.Add(this.lblTalkingFace);
            this.grpInformations.Controls.Add(this.ddpSpeed);
            this.grpInformations.Controls.Add(this.lblSpeed);
            this.grpInformations.Controls.Add(this.lblFace);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblName);
            this.grpInformations.Controls.Add(this.AnimFace);
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // TalkingFace
            // 
            this.TalkingFace.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.TalkingFace.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.CharacterFace;
            resources.ApplyResources(this.TalkingFace, "TalkingFace");
            this.TalkingFace.BackColor = System.Drawing.Color.LightGray;
            this.TalkingFace.Frequency = 100;
            this.TalkingFace.LinkToAnimationManager = true;
            this.TalkingFace.Name = "TalkingFace";
            this.TalkingFace.OriginPoint = false;
            this.TalkingFace.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.TalkingFace.Row = 0;
            this.TalkingFace.UseCustomFrequency = false;
            this.TalkingFace.UseCustomRow = false;
            this.TalkingFace.AnimationLoading += new System.EventHandler(this.TalkingFace_AnimationLoading);
            // 
            // lblTalkingFace
            // 
            resources.ApplyResources(this.lblTalkingFace, "lblTalkingFace");
            this.lblTalkingFace.Name = "lblTalkingFace";
            // 
            // ddpSpeed
            // 
            resources.ApplyResources(this.ddpSpeed, "ddpSpeed");
            this.ddpSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ddpSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ddpSpeed.Name = "ddpSpeed";
            this.ddpSpeed.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblSpeed
            // 
            resources.ApplyResources(this.lblSpeed, "lblSpeed");
            this.lblSpeed.Name = "lblSpeed";
            // 
            // lblFace
            // 
            resources.ApplyResources(this.lblFace, "lblFace");
            this.lblFace.Name = "lblFace";
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
            // AnimFace
            // 
            this.AnimFace.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimFace.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.CharacterFace;
            resources.ApplyResources(this.AnimFace, "AnimFace");
            this.AnimFace.BackColor = System.Drawing.Color.LightGray;
            this.AnimFace.Frequency = 100;
            this.AnimFace.LinkToAnimationManager = true;
            this.AnimFace.Name = "AnimFace";
            this.AnimFace.OriginPoint = false;
            this.AnimFace.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimFace.Row = 0;
            this.AnimFace.UseCustomFrequency = false;
            this.AnimFace.UseCustomRow = false;
            this.AnimFace.AnimationLoading += new System.EventHandler(this.AnimFace_AnimationLoading);
            // 
            // grpAnimations
            // 
            this.grpAnimations.Controls.Add(this.btnColorPalette);
            this.grpAnimations.Controls.Add(this.lblPalette);
            this.grpAnimations.Controls.Add(this.ddpTalkingAnim);
            this.grpAnimations.Controls.Add(this.lblTalkingAnim);
            this.grpAnimations.Controls.Add(this.ddpWalkingAnim);
            this.grpAnimations.Controls.Add(this.lblWalkingAnim);
            this.grpAnimations.Controls.Add(this.ddpStandingAnim);
            this.grpAnimations.Controls.Add(this.lblStandingAnim);
            this.grpAnimations.Controls.Add(this.AnimCharacter);
            resources.ApplyResources(this.grpAnimations, "grpAnimations");
            this.grpAnimations.Name = "grpAnimations";
            this.grpAnimations.TabStop = false;
            // 
            // btnColorPalette
            // 
            resources.ApplyResources(this.btnColorPalette, "btnColorPalette");
            this.btnColorPalette.Name = "btnColorPalette";
            this.btnColorPalette.UseVisualStyleBackColor = true;
            // 
            // lblPalette
            // 
            resources.ApplyResources(this.lblPalette, "lblPalette");
            this.lblPalette.Name = "lblPalette";
            // 
            // ddpTalkingAnim
            // 
            this.ddpTalkingAnim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpTalkingAnim.FormattingEnabled = true;
            resources.ApplyResources(this.ddpTalkingAnim, "ddpTalkingAnim");
            this.ddpTalkingAnim.Name = "ddpTalkingAnim";
            // 
            // lblTalkingAnim
            // 
            resources.ApplyResources(this.lblTalkingAnim, "lblTalkingAnim");
            this.lblTalkingAnim.Name = "lblTalkingAnim";
            // 
            // ddpWalkingAnim
            // 
            this.ddpWalkingAnim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpWalkingAnim.FormattingEnabled = true;
            resources.ApplyResources(this.ddpWalkingAnim, "ddpWalkingAnim");
            this.ddpWalkingAnim.Name = "ddpWalkingAnim";
            // 
            // lblWalkingAnim
            // 
            resources.ApplyResources(this.lblWalkingAnim, "lblWalkingAnim");
            this.lblWalkingAnim.Name = "lblWalkingAnim";
            // 
            // ddpStandingAnim
            // 
            this.ddpStandingAnim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpStandingAnim.FormattingEnabled = true;
            resources.ApplyResources(this.ddpStandingAnim, "ddpStandingAnim");
            this.ddpStandingAnim.Name = "ddpStandingAnim";
            // 
            // lblStandingAnim
            // 
            resources.ApplyResources(this.lblStandingAnim, "lblStandingAnim");
            this.lblStandingAnim.Name = "lblStandingAnim";
            // 
            // AnimCharacter
            // 
            this.AnimCharacter.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimCharacter.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.CharacterAnimation;
            resources.ApplyResources(this.AnimCharacter, "AnimCharacter");
            this.AnimCharacter.BackColor = System.Drawing.Color.LightGray;
            this.AnimCharacter.Frequency = 100;
            this.AnimCharacter.LinkToAnimationManager = true;
            this.AnimCharacter.Name = "AnimCharacter";
            this.AnimCharacter.OriginPoint = true;
            this.AnimCharacter.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.AnimCharacter.Row = 0;
            this.AnimCharacter.UseCustomFrequency = false;
            this.AnimCharacter.UseCustomRow = false;
            this.AnimCharacter.AnimationLoading += new System.EventHandler(this.AnimCharacter_AnimationLoading);
            // 
            // grpDialogs
            // 
            this.grpDialogs.Controls.Add(this.lblColor);
            this.grpDialogs.Controls.Add(this.btnChoose);
            this.grpDialogs.Controls.Add(this.ddpFont);
            this.grpDialogs.Controls.Add(this.lblFont);
            resources.ApplyResources(this.grpDialogs, "grpDialogs");
            this.grpDialogs.Name = "grpDialogs";
            this.grpDialogs.TabStop = false;
            // 
            // lblColor
            // 
            resources.ApplyResources(this.lblColor, "lblColor");
            this.lblColor.Name = "lblColor";
            // 
            // btnChoose
            // 
            resources.ApplyResources(this.btnChoose, "btnChoose");
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // ddpFont
            // 
            this.ddpFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ddpFont, "ddpFont");
            this.ddpFont.FormattingEnabled = true;
            this.ddpFont.Name = "ddpFont";
            // 
            // lblFont
            // 
            resources.ApplyResources(this.lblFont, "lblFont");
            this.lblFont.Name = "lblFont";
            // 
            // ListCharacters
            // 
            this.ListCharacters.CancelDeletion = false;
            this.ListCharacters.DataSource = null;
            resources.ApplyResources(this.ListCharacters, "ListCharacters");
            this.ListCharacters.DoubleClickable = false;
            this.ListCharacters.HideButtons = false;
            this.ListCharacters.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListCharacters.Name = "ListCharacters";
            this.ListCharacters.Title = "Characters";
            this.ListCharacters.ItemChosen += new System.EventHandler(this.ListCharacters_ItemChosen);
            this.ListCharacters.ItemToCreate += new System.EventHandler(this.ListCharacters_ItemToCreate);
            this.ListCharacters.ItemToDelete += new System.EventHandler(this.ListCharacters_ItemToDelete);
            this.ListCharacters.ListIsEmpty += new System.EventHandler(this.ListCharacters_ListIsEmpty);
            // 
            // DatabaseCharacters
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListCharacters);
            this.Controls.Add(this.grpDialogs);
            this.Controls.Add(this.grpAnimations);
            this.Controls.Add(this.grpInformations);
            this.Name = "DatabaseCharacters";
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpSpeed)).EndInit();
            this.grpAnimations.ResumeLayout(false);
            this.grpAnimations.PerformLayout();
            this.grpDialogs.ResumeLayout(false);
            this.grpDialogs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox grpAnimations;
        private System.Windows.Forms.GroupBox grpDialogs;
        private UserControls.AnimationControl AnimFace;
        private System.Windows.Forms.Label lblFace;
        private UserControls.AnimationControl AnimCharacter;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ColorDialog ColorDialog;
        private UserControls.ListItems ListCharacters;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.NumericUpDown ddpSpeed;
        private System.Windows.Forms.ComboBox ddpWalkingAnim;
        private System.Windows.Forms.Label lblWalkingAnim;
        private System.Windows.Forms.ComboBox ddpStandingAnim;
        private System.Windows.Forms.Label lblStandingAnim;
        private System.Windows.Forms.ComboBox ddpTalkingAnim;
        private System.Windows.Forms.Label lblTalkingAnim;
        private System.Windows.Forms.ComboBox ddpFont;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.Button btnColorPalette;
        private System.Windows.Forms.Label lblPalette;
        private UserControls.AnimationControl TalkingFace;
        private System.Windows.Forms.Label lblTalkingFace;
    }
}
