namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptCallGlobalEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptCallGlobalEvent));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGlobalEvent = new System.Windows.Forms.Label();
            this.cmbGlobalEvent = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ScriptCallGlobalEvent_Ok);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ScriptCallGlobalEvent_Cancel);
            // 
            // lblGlobalEvent
            // 
            resources.ApplyResources(this.lblGlobalEvent, "lblGlobalEvent");
            this.lblGlobalEvent.Name = "lblGlobalEvent";
            // 
            // cmbGlobalEvent
            // 
            resources.ApplyResources(this.cmbGlobalEvent, "cmbGlobalEvent");
            this.cmbGlobalEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGlobalEvent.FormattingEnabled = true;
            this.cmbGlobalEvent.Name = "cmbGlobalEvent";
            // 
            // ScriptCallGlobalEvent
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.cmbGlobalEvent);
            this.Controls.Add(this.lblGlobalEvent);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "ScriptCallGlobalEvent";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblGlobalEvent;
        private System.Windows.Forms.ComboBox cmbGlobalEvent;
    }
}