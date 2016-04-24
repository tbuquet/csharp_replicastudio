namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptPressSwitch
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptPressSwitch));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SelectedTrigger = new ReplicaStudio.Editor.Forms.UserControls.TriggerButton();
            this.lblTrigger = new System.Windows.Forms.Label();
            this.btnDisabled = new System.Windows.Forms.RadioButton();
            this.btnEnabled = new System.Windows.Forms.RadioButton();
            this.lblActive = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Switch_Cancel);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.Switch_Validation);
            // 
            // SelectedTrigger
            // 
            resources.ApplyResources(this.SelectedTrigger, "SelectedTrigger");
            this.SelectedTrigger.Name = "SelectedTrigger";
            this.SelectedTrigger.TriggerGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // lblTrigger
            // 
            resources.ApplyResources(this.lblTrigger, "lblTrigger");
            this.lblTrigger.Name = "lblTrigger";
            // 
            // btnDisabled
            // 
            resources.ApplyResources(this.btnDisabled, "btnDisabled");
            this.btnDisabled.Name = "btnDisabled";
            this.btnDisabled.TabStop = true;
            this.btnDisabled.UseVisualStyleBackColor = true;
            this.btnDisabled.Click += new System.EventHandler(this.Switch_Disabled);
            // 
            // btnEnabled
            // 
            resources.ApplyResources(this.btnEnabled, "btnEnabled");
            this.btnEnabled.Name = "btnEnabled";
            this.btnEnabled.TabStop = true;
            this.btnEnabled.UseVisualStyleBackColor = true;
            this.btnEnabled.Click += new System.EventHandler(this.Switch_Enabled);
            // 
            // lblActive
            // 
            resources.ApplyResources(this.lblActive, "lblActive");
            this.lblActive.Name = "lblActive";
            // 
            // ScriptPressSwitch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.btnEnabled);
            this.Controls.Add(this.btnDisabled);
            this.Controls.Add(this.lblTrigger);
            this.Controls.Add(this.SelectedTrigger);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "ScriptPressSwitch";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private UserControls.TriggerButton SelectedTrigger;
        private System.Windows.Forms.Label lblTrigger;
        private System.Windows.Forms.RadioButton btnDisabled;
        private System.Windows.Forms.RadioButton btnEnabled;
        private System.Windows.Forms.Label lblActive;
    }
}