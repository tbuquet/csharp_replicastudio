namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class CharacterConditions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterConditions));
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.animationControl1 = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpMovements = new System.Windows.Forms.GroupBox();
            this.ddpSpeed = new System.Windows.Forms.ComboBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.ddpDirection = new System.Windows.Forms.ComboBox();
            this.lblDirection = new System.Windows.Forms.Label();
            this.grp = new System.Windows.Forms.GroupBox();
            this.ddpTalkingType = new System.Windows.Forms.ComboBox();
            this.lblTypeTalking = new System.Windows.Forms.Label();
            this.ddpWalkingType = new System.Windows.Forms.ComboBox();
            this.lblTypeWalking = new System.Windows.Forms.Label();
            this.ddpFrequency = new System.Windows.Forms.ComboBox();
            this.lblAnimationSpeed = new System.Windows.Forms.Label();
            this.ddpStandingType = new System.Windows.Forms.ComboBox();
            this.lblAnimationStanding = new System.Windows.Forms.Label();
            this.grpPreview.SuspendLayout();
            this.grpMovements.SuspendLayout();
            this.grp.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPreview
            // 
            resources.ApplyResources(this.grpPreview, "grpPreview");
            this.grpPreview.Controls.Add(this.animationControl1);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.TabStop = false;
            // 
            // animationControl1
            // 
            resources.ApplyResources(this.animationControl1, "animationControl1");
            this.animationControl1.Animation = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.animationControl1.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.CharacterAnimation;
            this.animationControl1.Frequency = 100;
            this.animationControl1.LinkToAnimationManager = false;
            this.animationControl1.Name = "animationControl1";
            this.animationControl1.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.animationControl1.Row = 0;
            this.animationControl1.UseCustomFrequency = true;
            this.animationControl1.UseCustomRow = true;
            // 
            // grpMovements
            // 
            resources.ApplyResources(this.grpMovements, "grpMovements");
            this.grpMovements.Controls.Add(this.ddpSpeed);
            this.grpMovements.Controls.Add(this.lblSpeed);
            this.grpMovements.Name = "grpMovements";
            this.grpMovements.TabStop = false;
            // 
            // ddpSpeed
            // 
            resources.ApplyResources(this.ddpSpeed, "ddpSpeed");
            this.ddpSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpSpeed.Name = "ddpSpeed";
            // 
            // lblSpeed
            // 
            resources.ApplyResources(this.lblSpeed, "lblSpeed");
            this.lblSpeed.Name = "lblSpeed";
            // 
            // ddpDirection
            // 
            resources.ApplyResources(this.ddpDirection, "ddpDirection");
            this.ddpDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpDirection.FormattingEnabled = true;
            this.ddpDirection.Name = "ddpDirection";
            // 
            // lblDirection
            // 
            resources.ApplyResources(this.lblDirection, "lblDirection");
            this.lblDirection.Name = "lblDirection";
            // 
            // grp
            // 
            resources.ApplyResources(this.grp, "grp");
            this.grp.Controls.Add(this.ddpTalkingType);
            this.grp.Controls.Add(this.lblTypeTalking);
            this.grp.Controls.Add(this.ddpWalkingType);
            this.grp.Controls.Add(this.lblTypeWalking);
            this.grp.Controls.Add(this.ddpDirection);
            this.grp.Controls.Add(this.lblDirection);
            this.grp.Controls.Add(this.ddpFrequency);
            this.grp.Controls.Add(this.lblAnimationSpeed);
            this.grp.Controls.Add(this.ddpStandingType);
            this.grp.Controls.Add(this.lblAnimationStanding);
            this.grp.Name = "grp";
            this.grp.TabStop = false;
            // 
            // ddpTalkingType
            // 
            resources.ApplyResources(this.ddpTalkingType, "ddpTalkingType");
            this.ddpTalkingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpTalkingType.FormattingEnabled = true;
            this.ddpTalkingType.Name = "ddpTalkingType";
            // 
            // lblTypeTalking
            // 
            resources.ApplyResources(this.lblTypeTalking, "lblTypeTalking");
            this.lblTypeTalking.Name = "lblTypeTalking";
            // 
            // ddpWalkingType
            // 
            resources.ApplyResources(this.ddpWalkingType, "ddpWalkingType");
            this.ddpWalkingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpWalkingType.FormattingEnabled = true;
            this.ddpWalkingType.Name = "ddpWalkingType";
            // 
            // lblTypeWalking
            // 
            resources.ApplyResources(this.lblTypeWalking, "lblTypeWalking");
            this.lblTypeWalking.Name = "lblTypeWalking";
            // 
            // ddpFrequency
            // 
            resources.ApplyResources(this.ddpFrequency, "ddpFrequency");
            this.ddpFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpFrequency.FormattingEnabled = true;
            this.ddpFrequency.Name = "ddpFrequency";
            // 
            // lblAnimationSpeed
            // 
            resources.ApplyResources(this.lblAnimationSpeed, "lblAnimationSpeed");
            this.lblAnimationSpeed.Name = "lblAnimationSpeed";
            // 
            // ddpStandingType
            // 
            resources.ApplyResources(this.ddpStandingType, "ddpStandingType");
            this.ddpStandingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpStandingType.FormattingEnabled = true;
            this.ddpStandingType.Name = "ddpStandingType";
            // 
            // lblAnimationStanding
            // 
            resources.ApplyResources(this.lblAnimationStanding, "lblAnimationStanding");
            this.lblAnimationStanding.Name = "lblAnimationStanding";
            // 
            // CharacterConditions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp);
            this.Controls.Add(this.grpMovements);
            this.Controls.Add(this.grpPreview);
            this.Name = "CharacterConditions";
            this.grpPreview.ResumeLayout(false);
            this.grpPreview.PerformLayout();
            this.grpMovements.ResumeLayout(false);
            this.grpMovements.PerformLayout();
            this.grp.ResumeLayout(false);
            this.grp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPreview;
        private AnimationControl animationControl1;
        private System.Windows.Forms.GroupBox grpMovements;
        private System.Windows.Forms.ComboBox ddpSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.GroupBox grp;
        private System.Windows.Forms.ComboBox ddpStandingType;
        private System.Windows.Forms.Label lblAnimationStanding;
        private System.Windows.Forms.ComboBox ddpFrequency;
        private System.Windows.Forms.Label lblAnimationSpeed;
        private System.Windows.Forms.ComboBox ddpDirection;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.ComboBox ddpTalkingType;
        private System.Windows.Forms.Label lblTypeTalking;
        private System.Windows.Forms.ComboBox ddpWalkingType;
        private System.Windows.Forms.Label lblTypeWalking;
    }
}
