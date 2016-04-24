namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    partial class DatabaseClasses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseClasses));
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.btnCreateBadInteraction = new System.Windows.Forms.Button();
            this.grdBadInteractions = new System.Windows.Forms.DataGridView();
            this.lblBadInteractions = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.ListClasses = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BadInteractionsAction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BadInteractionsCharacter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BadInteractionsMessage = new System.Windows.Forms.DataGridViewButtonColumn();
            this.delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.grpInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBadInteractions)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInformations
            // 
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Controls.Add(this.btnCreateBadInteraction);
            this.grpInformations.Controls.Add(this.grdBadInteractions);
            this.grpInformations.Controls.Add(this.lblBadInteractions);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.lblName);
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // btnCreateBadInteraction
            // 
            resources.ApplyResources(this.btnCreateBadInteraction, "btnCreateBadInteraction");
            this.btnCreateBadInteraction.Name = "btnCreateBadInteraction";
            this.btnCreateBadInteraction.UseVisualStyleBackColor = true;
            this.btnCreateBadInteraction.Click += new System.EventHandler(this.btnCreateBadInteraction_Click);
            // 
            // grdBadInteractions
            // 
            resources.ApplyResources(this.grdBadInteractions, "grdBadInteractions");
            this.grdBadInteractions.AllowUserToAddRows = false;
            this.grdBadInteractions.AllowUserToDeleteRows = false;
            this.grdBadInteractions.AllowUserToResizeColumns = false;
            this.grdBadInteractions.AllowUserToResizeRows = false;
            this.grdBadInteractions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdBadInteractions.BackgroundColor = System.Drawing.Color.DarkGray;
            this.grdBadInteractions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdBadInteractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBadInteractions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.BadInteractionsAction,
            this.BadInteractionsCharacter,
            this.BadInteractionsMessage,
            this.delete});
            this.grdBadInteractions.Name = "grdBadInteractions";
            this.grdBadInteractions.RowHeadersVisible = false;
            this.grdBadInteractions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdBadInteractions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBadInteractions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdBadInteractions_DataError);
            // 
            // lblBadInteractions
            // 
            resources.ApplyResources(this.lblBadInteractions, "lblBadInteractions");
            this.lblBadInteractions.Name = "lblBadInteractions";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // ListClasses
            // 
            resources.ApplyResources(this.ListClasses, "ListClasses");
            this.ListClasses.CancelDeletion = false;
            this.ListClasses.DataSource = null;
            this.ListClasses.DoubleClickable = false;
            this.ListClasses.HideButtons = false;
            this.ListClasses.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListClasses.Name = "ListClasses";
            this.ListClasses.Title = "Classes";
            this.ListClasses.ItemChosen += new System.EventHandler(this.ListClasses_ItemChosen);
            this.ListClasses.ItemToCreate += new System.EventHandler(this.ListClasses_ItemToCreate);
            this.ListClasses.ItemToDelete += new System.EventHandler(this.ListClasses_ItemToDelete);
            this.ListClasses.ListIsEmpty += new System.EventHandler(this.ListClasses_ListIsEmpty);
            // 
            // id
            // 
            resources.ApplyResources(this.id, "id");
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // BadInteractionsAction
            // 
            resources.ApplyResources(this.BadInteractionsAction, "BadInteractionsAction");
            this.BadInteractionsAction.Name = "BadInteractionsAction";
            this.BadInteractionsAction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BadInteractionsAction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // BadInteractionsCharacter
            // 
            resources.ApplyResources(this.BadInteractionsCharacter, "BadInteractionsCharacter");
            this.BadInteractionsCharacter.Name = "BadInteractionsCharacter";
            this.BadInteractionsCharacter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BadInteractionsCharacter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // BadInteractionsMessage
            // 
            this.BadInteractionsMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.BadInteractionsMessage, "BadInteractionsMessage");
            this.BadInteractionsMessage.Name = "BadInteractionsMessage";
            this.BadInteractionsMessage.ReadOnly = true;
            this.BadInteractionsMessage.Text = "Choose";
            // 
            // delete
            // 
            this.delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.delete, "delete");
            this.delete.Name = "delete";
            this.delete.ReadOnly = true;
            // 
            // DatabaseClasses
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListClasses);
            this.Name = "DatabaseClasses";
            this.VisibleChanged += new System.EventHandler(this.DatabaseClasses_VisibleChanged);
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBadInteractions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListClasses;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DataGridView grdBadInteractions;
        private System.Windows.Forms.Label lblBadInteractions;
        private System.Windows.Forms.Button btnCreateBadInteraction;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewComboBoxColumn BadInteractionsAction;
        private System.Windows.Forms.DataGridViewComboBoxColumn BadInteractionsCharacter;
        private System.Windows.Forms.DataGridViewButtonColumn BadInteractionsMessage;
        private System.Windows.Forms.DataGridViewButtonColumn delete;
    }
}
