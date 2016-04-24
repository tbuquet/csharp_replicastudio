namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class AnimationsConditions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationsConditions));
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.animationControl1 = new ReplicaStudio.Editor.Forms.UserControls.AnimationControl();
            this.grpTrigger = new System.Windows.Forms.GroupBox();
            this.TriggerExecutionTypeCombo = new System.Windows.Forms.ComboBox();
            this.grp = new System.Windows.Forms.GroupBox();
            this.chkStartFrozen = new System.Windows.Forms.CheckBox();
            this.lblAnimationSpeed = new System.Windows.Forms.Label();
            this.ddpSpeed = new System.Windows.Forms.ComboBox();
            this.grpPreview.SuspendLayout();
            this.grpTrigger.SuspendLayout();
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
            this.animationControl1.AnimationFilter = ReplicaStudio.Shared.TransverseLayer.Constants.Enums.AnimationType.ObjectAnimation;
            this.animationControl1.Frequency = 100;
            this.animationControl1.LinkToAnimationManager = false;
            this.animationControl1.Name = "animationControl1";
            this.animationControl1.ParentCharacter = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.animationControl1.Row = 0;
            this.animationControl1.UseCustomFrequency = true;
            this.animationControl1.UseCustomRow = false;
            // 
            // grpTrigger
            // 
            resources.ApplyResources(this.grpTrigger, "grpTrigger");
            this.grpTrigger.Controls.Add(this.TriggerExecutionTypeCombo);
            this.grpTrigger.Name = "grpTrigger";
            this.grpTrigger.TabStop = false;
            // 
            // TriggerExecutionTypeCombo
            // 
            resources.ApplyResources(this.TriggerExecutionTypeCombo, "TriggerExecutionTypeCombo");
            this.TriggerExecutionTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TriggerExecutionTypeCombo.FormattingEnabled = true;
            this.TriggerExecutionTypeCombo.Name = "TriggerExecutionTypeCombo";
            this.TriggerExecutionTypeCombo.SelectedValueChanged += new System.EventHandler(this.EventManager_TriggerExecutionTypeChanged);
            // 
            // grp
            // 
            resources.ApplyResources(this.grp, "grp");
            this.grp.Controls.Add(this.chkStartFrozen);
            this.grp.Controls.Add(this.lblAnimationSpeed);
            this.grp.Controls.Add(this.ddpSpeed);
            this.grp.Name = "grp";
            this.grp.TabStop = false;
            // 
            // chkStartFrozen
            // 
            resources.ApplyResources(this.chkStartFrozen, "chkStartFrozen");
            this.chkStartFrozen.Name = "chkStartFrozen";
            this.chkStartFrozen.UseVisualStyleBackColor = true;
            // 
            // lblAnimationSpeed
            // 
            resources.ApplyResources(this.lblAnimationSpeed, "lblAnimationSpeed");
            this.lblAnimationSpeed.Name = "lblAnimationSpeed";
            // 
            // ddpSpeed
            // 
            resources.ApplyResources(this.ddpSpeed, "ddpSpeed");
            this.ddpSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpSpeed.FormattingEnabled = true;
            this.ddpSpeed.Name = "ddpSpeed";
            // 
            // AnimationsConditions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp);
            this.Controls.Add(this.grpTrigger);
            this.Controls.Add(this.grpPreview);
            this.Name = "AnimationsConditions";
            this.grpPreview.ResumeLayout(false);
            this.grpPreview.PerformLayout();
            this.grpTrigger.ResumeLayout(false);
            this.grp.ResumeLayout(false);
            this.grp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPreview;
        private AnimationControl animationControl1;
        private System.Windows.Forms.GroupBox grpTrigger;
        private System.Windows.Forms.ComboBox TriggerExecutionTypeCombo;
        private System.Windows.Forms.GroupBox grp;
        private System.Windows.Forms.Label lblAnimationSpeed;
        private System.Windows.Forms.ComboBox ddpSpeed;
        private System.Windows.Forms.CheckBox chkStartFrozen;
    }
}
