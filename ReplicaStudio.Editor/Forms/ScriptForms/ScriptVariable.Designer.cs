namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptVariable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptVariable));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblVariable = new System.Windows.Forms.Label();
            this.lblNewValue = new System.Windows.Forms.Label();
            this.VariableNew = new ReplicaStudio.Editor.Forms.UserControls.VariableOrConstantButton();
            this.VariableSelector = new ReplicaStudio.Editor.Forms.UserControls.VariableButton();
            this.OperatorType = new System.Windows.Forms.ComboBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ScriptVariable_Cancel);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ScriptVariable_Validation);
            // 
            // lblVariable
            // 
            resources.ApplyResources(this.lblVariable, "lblVariable");
            this.lblVariable.Name = "lblVariable";
            // 
            // lblNewValue
            // 
            resources.ApplyResources(this.lblNewValue, "lblNewValue");
            this.lblNewValue.Name = "lblNewValue";
            // 
            // VariableNew
            // 
            resources.ApplyResources(this.VariableNew, "VariableNew");
            this.VariableNew.Name = "VariableNew";
            this.VariableNew.VariableGuid = null;
            // 
            // VariableSelector
            // 
            resources.ApplyResources(this.VariableSelector, "VariableSelector");
            this.VariableSelector.Name = "VariableSelector";
            this.VariableSelector.VariableGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // OperatorType
            // 
            resources.ApplyResources(this.OperatorType, "OperatorType");
            this.OperatorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperatorType.FormattingEnabled = true;
            this.OperatorType.Name = "OperatorType";
            // 
            // lblOperator
            // 
            resources.ApplyResources(this.lblOperator, "lblOperator");
            this.lblOperator.Name = "lblOperator";
            // 
            // ScriptVariable
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.lblOperator);
            this.Controls.Add(this.OperatorType);
            this.Controls.Add(this.VariableNew);
            this.Controls.Add(this.lblNewValue);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.VariableSelector);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "ScriptVariable";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private UserControls.VariableButton VariableSelector;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.Label lblNewValue;
        private UserControls.VariableOrConstantButton VariableNew;
        private System.Windows.Forms.ComboBox OperatorType;
        private System.Windows.Forms.Label lblOperator;
    }
}