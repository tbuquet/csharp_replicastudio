namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptChangePlayerHP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptChangePlayerHP));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblVariable = new System.Windows.Forms.Label();
            this.lblNewValue = new System.Windows.Forms.Label();
            this.OperatorType = new System.Windows.Forms.ComboBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.VariableNew = new ReplicaStudio.Editor.Forms.UserControls.VariableOrConstantButton();
            this.rdxCurrentCharacter = new System.Windows.Forms.RadioButton();
            this.rdxtoCharacterRadio = new System.Windows.Forms.RadioButton();
            this.characterButton1 = new ReplicaStudio.Editor.Forms.UserControls.CharacterButton();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ScriptChangePlayerHP_Cancel);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ScriptChangePlayerHP_Validation);
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
            // OperatorType
            // 
            this.OperatorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OperatorType.FormattingEnabled = true;
            resources.ApplyResources(this.OperatorType, "OperatorType");
            this.OperatorType.Name = "OperatorType";
            // 
            // lblOperator
            // 
            resources.ApplyResources(this.lblOperator, "lblOperator");
            this.lblOperator.Name = "lblOperator";
            // 
            // VariableNew
            // 
            resources.ApplyResources(this.VariableNew, "VariableNew");
            this.VariableNew.Name = "VariableNew";
            this.VariableNew.VariableGuid = null;
            // 
            // rdxCurrentCharacter
            // 
            resources.ApplyResources(this.rdxCurrentCharacter, "rdxCurrentCharacter");
            this.rdxCurrentCharacter.Checked = true;
            this.rdxCurrentCharacter.Name = "rdxCurrentCharacter";
            this.rdxCurrentCharacter.TabStop = true;
            this.rdxCurrentCharacter.UseVisualStyleBackColor = true;
            this.rdxCurrentCharacter.CheckedChanged += new System.EventHandler(this.SelectCharacter_CurrentCharacter);
            // 
            // rdxtoCharacterRadio
            // 
            resources.ApplyResources(this.rdxtoCharacterRadio, "rdxtoCharacterRadio");
            this.rdxtoCharacterRadio.Name = "rdxtoCharacterRadio";
            this.rdxtoCharacterRadio.UseVisualStyleBackColor = true;
            this.rdxtoCharacterRadio.CheckedChanged += new System.EventHandler(this.SelectCharacter_Character);
            // 
            // characterButton1
            // 
            this.characterButton1.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.characterButton1, "characterButton1");
            this.characterButton1.Name = "characterButton1";
            this.characterButton1.UsePlayableCharacter = true;
            // 
            // ScriptChangePlayerHP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.rdxCurrentCharacter);
            this.Controls.Add(this.rdxtoCharacterRadio);
            this.Controls.Add(this.characterButton1);
            this.Controls.Add(this.lblOperator);
            this.Controls.Add(this.OperatorType);
            this.Controls.Add(this.VariableNew);
            this.Controls.Add(this.lblNewValue);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "ScriptChangePlayerHP";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblVariable;
        private System.Windows.Forms.Label lblNewValue;
        private UserControls.VariableOrConstantButton VariableNew;
        private System.Windows.Forms.ComboBox OperatorType;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.RadioButton rdxCurrentCharacter;
        private System.Windows.Forms.RadioButton rdxtoCharacterRadio;
        private UserControls.CharacterButton characterButton1;
    }
}