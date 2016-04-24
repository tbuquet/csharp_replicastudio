namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptRandom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptRandom));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.ctrlMinValue = new ReplicaStudio.Editor.Forms.UserControls.VariableOrConstantButton();
            this.ctrlMaxValue = new ReplicaStudio.Editor.Forms.UserControls.VariableOrConstantButton();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.lblVariable = new System.Windows.Forms.Label();
            this.ctrlVariable = new ReplicaStudio.Editor.Forms.UserControls.VariableButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ScriptRandom_Cancel);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ScriptRandom_Ok);
            // 
            // ctrlMinValue
            // 
            resources.ApplyResources(this.ctrlMinValue, "ctrlMinValue");
            this.ctrlMinValue.Name = "ctrlMinValue";
            this.ctrlMinValue.VariableGuid = null;
            // 
            // ctrlMaxValue
            // 
            resources.ApplyResources(this.ctrlMaxValue, "ctrlMaxValue");
            this.ctrlMaxValue.Name = "ctrlMaxValue";
            this.ctrlMaxValue.VariableGuid = null;
            // 
            // lblMinValue
            // 
            resources.ApplyResources(this.lblMinValue, "lblMinValue");
            this.lblMinValue.Name = "lblMinValue";
            // 
            // lblMaxValue
            // 
            resources.ApplyResources(this.lblMaxValue, "lblMaxValue");
            this.lblMaxValue.Name = "lblMaxValue";
            // 
            // lblVariable
            // 
            resources.ApplyResources(this.lblVariable, "lblVariable");
            this.lblVariable.Name = "lblVariable";
            // 
            // ctrlVariable
            // 
            resources.ApplyResources(this.ctrlVariable, "ctrlVariable");
            this.ctrlVariable.Name = "ctrlVariable";
            this.ctrlVariable.VariableGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // ScriptRandom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.ctrlVariable);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.lblMaxValue);
            this.Controls.Add(this.lblMinValue);
            this.Controls.Add(this.ctrlMaxValue);
            this.Controls.Add(this.ctrlMinValue);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "ScriptRandom";
            this.Load += new System.EventHandler(this.ScriptRandom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private UserControls.VariableOrConstantButton ctrlMinValue;
        private UserControls.VariableOrConstantButton ctrlMaxValue;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label lblVariable;
        private UserControls.VariableButton ctrlVariable;
    }
}