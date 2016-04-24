namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptWait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptWait));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblSecond = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.nbSeconds = new ReplicaStudio.Editor.Forms.UserControls.VariableOrConstantButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ScriptWait_Cancel);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ScriptWait_Ok);
            // 
            // lblSecond
            // 
            resources.ApplyResources(this.lblSecond, "lblSecond");
            this.lblSecond.Name = "lblSecond";
            // 
            // lblWait
            // 
            resources.ApplyResources(this.lblWait, "lblWait");
            this.lblWait.Name = "lblWait";
            // 
            // nbSeconds
            // 
            resources.ApplyResources(this.nbSeconds, "nbSeconds");
            this.nbSeconds.Name = "nbSeconds";
            this.nbSeconds.VariableGuid = null;
            // 
            // ScriptWait
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.nbSeconds);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.lblSecond);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "ScriptWait";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.Label lblWait;
        private UserControls.VariableOrConstantButton nbSeconds;
    }
}