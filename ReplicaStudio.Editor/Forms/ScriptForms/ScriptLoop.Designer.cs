namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptLoop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptLoop));
            this.rdButton = new System.Windows.Forms.CheckBox();
            this.chkButtonActive = new System.Windows.Forms.CheckBox();
            this.rdVariable = new System.Windows.Forms.CheckBox();
            this.lblVariableIs = new System.Windows.Forms.Label();
            this.ddpOperator = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.varVariable2 = new ReplicaStudio.Editor.Forms.UserControls.VariableOrConstantButton();
            this.varVariable1 = new ReplicaStudio.Editor.Forms.UserControls.VariableButton();
            this.trgButton = new ReplicaStudio.Editor.Forms.UserControls.TriggerButton();
            this.SuspendLayout();
            // 
            // rdButton
            // 
            resources.ApplyResources(this.rdButton, "rdButton");
            this.rdButton.Name = "rdButton";
            this.rdButton.UseVisualStyleBackColor = true;
            this.rdButton.CheckedChanged += new System.EventHandler(this.rdButton_CheckedChanged);
            // 
            // chkButtonActive
            // 
            resources.ApplyResources(this.chkButtonActive, "chkButtonActive");
            this.chkButtonActive.Name = "chkButtonActive";
            this.chkButtonActive.UseVisualStyleBackColor = true;
            // 
            // rdVariable
            // 
            resources.ApplyResources(this.rdVariable, "rdVariable");
            this.rdVariable.Name = "rdVariable";
            this.rdVariable.UseVisualStyleBackColor = true;
            this.rdVariable.CheckedChanged += new System.EventHandler(this.rdVariable_CheckedChanged);
            // 
            // lblVariableIs
            // 
            resources.ApplyResources(this.lblVariableIs, "lblVariableIs");
            this.lblVariableIs.Name = "lblVariableIs";
            // 
            // ddpOperator
            // 
            resources.ApplyResources(this.ddpOperator, "ddpOperator");
            this.ddpOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpOperator.FormattingEnabled = true;
            this.ddpOperator.Name = "ddpOperator";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // varVariable2
            // 
            resources.ApplyResources(this.varVariable2, "varVariable2");
            this.varVariable2.Name = "varVariable2";
            this.varVariable2.VariableGuid = null;
            // 
            // varVariable1
            // 
            resources.ApplyResources(this.varVariable1, "varVariable1");
            this.varVariable1.Name = "varVariable1";
            this.varVariable1.VariableGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // trgButton
            // 
            resources.ApplyResources(this.trgButton, "trgButton");
            this.trgButton.Name = "trgButton";
            this.trgButton.TriggerGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // ScriptLoop
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.varVariable2);
            this.Controls.Add(this.ddpOperator);
            this.Controls.Add(this.lblVariableIs);
            this.Controls.Add(this.varVariable1);
            this.Controls.Add(this.rdVariable);
            this.Controls.Add(this.chkButtonActive);
            this.Controls.Add(this.trgButton);
            this.Controls.Add(this.rdButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "ScriptLoop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox rdButton;
        private UserControls.TriggerButton trgButton;
        private System.Windows.Forms.CheckBox chkButtonActive;
        private System.Windows.Forms.CheckBox rdVariable;
        private UserControls.VariableButton varVariable1;
        private System.Windows.Forms.Label lblVariableIs;
        private System.Windows.Forms.ComboBox ddpOperator;
        private UserControls.VariableOrConstantButton varVariable2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}