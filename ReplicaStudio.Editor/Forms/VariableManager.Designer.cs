namespace ReplicaStudio.Editor.Forms
{
    partial class VariableManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariableManager));
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.ddpValue = new System.Windows.Forms.NumericUpDown();
            this.lblInitialValue = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.ListVariables = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.grpInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // grpInformations
            // 
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Controls.Add(this.ddpValue);
            this.grpInformations.Controls.Add(this.lblInitialValue);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblTitle);
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // ddpValue
            // 
            resources.ApplyResources(this.ddpValue, "ddpValue");
            this.ddpValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ddpValue.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.ddpValue.Name = "ddpValue";
            // 
            // lblInitialValue
            // 
            resources.ApplyResources(this.lblInitialValue, "lblInitialValue");
            this.lblInitialValue.Name = "lblInitialValue";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ListVariables
            // 
            resources.ApplyResources(this.ListVariables, "ListVariables");
            this.ListVariables.CancelDeletion = false;
            this.ListVariables.DataSource = null;
            this.ListVariables.DoubleClickable = true;
            this.ListVariables.HideButtons = false;
            this.ListVariables.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListVariables.Name = "ListVariables";
            this.ListVariables.Title = "Variables";
            this.ListVariables.ItemChosen += new System.EventHandler(this.ListVariables_ItemChosen);
            this.ListVariables.ItemToCreate += new System.EventHandler(this.ListVariables_ItemToCreate);
            this.ListVariables.ItemToDelete += new System.EventHandler(this.ListVariables_ItemToDelete);
            this.ListVariables.ListIsEmpty += new System.EventHandler(this.ListVariables_ListIsEmpty);
            this.ListVariables.ItemDoubleClicked += new System.EventHandler(this.ListVariables_ItemDoubleClicked);
            // 
            // VariableManager
            // 
            this.AcceptButton = this.btnSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListVariables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VariableManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListVariables;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.Label lblInitialValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.NumericUpDown ddpValue;
    }
}