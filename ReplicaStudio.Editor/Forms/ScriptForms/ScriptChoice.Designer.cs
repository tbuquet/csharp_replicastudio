namespace ReplicaStudio.Editor.Forms
{
    partial class ScriptChoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptChoice));
            this.grdDialog = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnInsertBelow = new System.Windows.Forms.Button();
            this.btnInsertAbove = new System.Windows.Forms.Button();
            this.grpInsertions = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.grpDelete = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdDialog)).BeginInit();
            this.grpInsertions.SuspendLayout();
            this.grpOrder.SuspendLayout();
            this.grpDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDialog
            // 
            resources.ApplyResources(this.grdDialog, "grdDialog");
            this.grdDialog.AllowUserToAddRows = false;
            this.grdDialog.AllowUserToDeleteRows = false;
            this.grdDialog.AllowUserToResizeColumns = false;
            this.grdDialog.AllowUserToResizeRows = false;
            this.grdDialog.BackgroundColor = System.Drawing.Color.DarkGray;
            this.grdDialog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdDialog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDialog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.text});
            this.grdDialog.Name = "grdDialog";
            this.grdDialog.RowHeadersVisible = false;
            this.grdDialog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDialog.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdDialog_DataError);
            // 
            // id
            // 
            resources.ApplyResources(this.id, "id");
            this.id.Name = "id";
            // 
            // text
            // 
            this.text.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.text, "text");
            this.text.Name = "text";
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
            // btnInsertBelow
            // 
            resources.ApplyResources(this.btnInsertBelow, "btnInsertBelow");
            this.btnInsertBelow.Name = "btnInsertBelow";
            this.btnInsertBelow.UseVisualStyleBackColor = true;
            this.btnInsertBelow.Click += new System.EventHandler(this.btnInsertBelow_Click);
            // 
            // btnInsertAbove
            // 
            resources.ApplyResources(this.btnInsertAbove, "btnInsertAbove");
            this.btnInsertAbove.Name = "btnInsertAbove";
            this.btnInsertAbove.UseVisualStyleBackColor = true;
            this.btnInsertAbove.Click += new System.EventHandler(this.btnInsertAbove_Click);
            // 
            // grpInsertions
            // 
            resources.ApplyResources(this.grpInsertions, "grpInsertions");
            this.grpInsertions.Controls.Add(this.btnInsertAbove);
            this.grpInsertions.Controls.Add(this.btnInsertBelow);
            this.grpInsertions.Name = "grpInsertions";
            this.grpInsertions.TabStop = false;
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grpOrder
            // 
            resources.ApplyResources(this.grpOrder, "grpOrder");
            this.grpOrder.Controls.Add(this.btnUp);
            this.grpOrder.Controls.Add(this.btnDown);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.TabStop = false;
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // grpDelete
            // 
            resources.ApplyResources(this.grpDelete, "grpDelete");
            this.grpDelete.Controls.Add(this.btnDelete);
            this.grpDelete.Name = "grpDelete";
            this.grpDelete.TabStop = false;
            // 
            // ScriptChoice
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.grpDelete);
            this.Controls.Add(this.grpOrder);
            this.Controls.Add(this.grpInsertions);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScriptChoice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.grdDialog)).EndInit();
            this.grpInsertions.ResumeLayout(false);
            this.grpOrder.ResumeLayout(false);
            this.grpDelete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdDialog;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnInsertBelow;
        private System.Windows.Forms.Button btnInsertAbove;
        private System.Windows.Forms.GroupBox grpInsertions;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox grpOrder;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.GroupBox grpDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
    }
}