namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class EventConditions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventConditions));
            this.grpTrigger = new System.Windows.Forms.GroupBox();
            this.ddpTrigger = new System.Windows.Forms.ComboBox();
            this.grpTrigger.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTrigger
            // 
            resources.ApplyResources(this.grpTrigger, "grpTrigger");
            this.grpTrigger.Controls.Add(this.ddpTrigger);
            this.grpTrigger.Name = "grpTrigger";
            this.grpTrigger.TabStop = false;
            // 
            // ddpTrigger
            // 
            resources.ApplyResources(this.ddpTrigger, "ddpTrigger");
            this.ddpTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpTrigger.FormattingEnabled = true;
            this.ddpTrigger.Name = "ddpTrigger";
            // 
            // EventConditions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpTrigger);
            this.Name = "EventConditions";
            this.grpTrigger.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTrigger;
        private System.Windows.Forms.ComboBox ddpTrigger;
    }
}
