namespace ReplicaStudio.Editor.Forms
{
    partial class TriggerManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerManager));
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.chkTrue = new System.Windows.Forms.CheckBox();
            this.lblInitialValue = new System.Windows.Forms.Label();
            this.ListTriggers = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.grpInformations.SuspendLayout();
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
            this.grpInformations.Controls.Add(this.chkTrue);
            this.grpInformations.Controls.Add(this.lblInitialValue);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblTitle);
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // chkTrue
            // 
            resources.ApplyResources(this.chkTrue, "chkTrue");
            this.chkTrue.Name = "chkTrue";
            this.chkTrue.UseVisualStyleBackColor = true;
            // 
            // lblInitialValue
            // 
            resources.ApplyResources(this.lblInitialValue, "lblInitialValue");
            this.lblInitialValue.Name = "lblInitialValue";
            // 
            // ListTriggers
            // 
            resources.ApplyResources(this.ListTriggers, "ListTriggers");
            this.ListTriggers.CancelDeletion = false;
            this.ListTriggers.DataSource = null;
            this.ListTriggers.DoubleClickable = true;
            this.ListTriggers.HideButtons = false;
            this.ListTriggers.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListTriggers.Name = "ListTriggers";
            this.ListTriggers.Title = "Bouton";
            this.ListTriggers.ItemChosen += new System.EventHandler(this.ListTriggers_ItemChosen);
            this.ListTriggers.ItemToCreate += new System.EventHandler(this.ListTriggers_ItemToCreate);
            this.ListTriggers.ItemToDelete += new System.EventHandler(this.ListTriggers_ItemToDelete);
            this.ListTriggers.ListIsEmpty += new System.EventHandler(this.ListTriggers_ListIsEmpty);
            this.ListTriggers.ItemDoubleClicked += new System.EventHandler(this.ListTriggers_ItemDoubleClicked);
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
            // TriggerManager
            // 
            this.AcceptButton = this.btnSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListTriggers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TriggerManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListTriggers;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.Label lblInitialValue;
        private System.Windows.Forms.CheckBox chkTrue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
    }
}