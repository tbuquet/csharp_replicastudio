namespace ReplicaStudio.Editor.Forms
{
    partial class DialogManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogManager));
            this.grdDialog = new System.Windows.Forms.DataGridView();
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
            this.grpFaces = new System.Windows.Forms.GroupBox();
            this.chkShowFaces = new System.Windows.Forms.CheckBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.character = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fontsize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sound = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdDialog)).BeginInit();
            this.grpInsertions.SuspendLayout();
            this.grpOrder.SuspendLayout();
            this.grpDelete.SuspendLayout();
            this.grpFaces.SuspendLayout();
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
            this.text,
            this.character,
            this.duration,
            this.fontsize,
            this.sound});
            this.grdDialog.Name = "grdDialog";
            this.grdDialog.RowHeadersVisible = false;
            this.grdDialog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDialog.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdDialog_DataError);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            // grpFaces
            // 
            resources.ApplyResources(this.grpFaces, "grpFaces");
            this.grpFaces.Controls.Add(this.chkShowFaces);
            this.grpFaces.Name = "grpFaces";
            this.grpFaces.TabStop = false;
            // 
            // chkShowFaces
            // 
            resources.ApplyResources(this.chkShowFaces, "chkShowFaces");
            this.chkShowFaces.Name = "chkShowFaces";
            this.chkShowFaces.UseVisualStyleBackColor = true;
            this.chkShowFaces.CheckedChanged += new System.EventHandler(this.chkShowFaces_CheckedChanged);
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
            // character
            // 
            this.character.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.character, "character");
            this.character.Name = "character";
            // 
            // duration
            // 
            this.duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.duration, "duration");
            this.duration.Name = "duration";
            // 
            // fontsize
            // 
            this.fontsize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(this.fontsize, "fontsize");
            this.fontsize.Name = "fontsize";
            // 
            // sound
            // 
            this.sound.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.sound, "sound");
            this.sound.Name = "sound";
            this.sound.ReadOnly = true;
            // 
            // DialogManager
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.grpFaces);
            this.Controls.Add(this.grpDelete);
            this.Controls.Add(this.grpOrder);
            this.Controls.Add(this.grpInsertions);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.grdDialog)).EndInit();
            this.grpInsertions.ResumeLayout(false);
            this.grpOrder.ResumeLayout(false);
            this.grpDelete.ResumeLayout(false);
            this.grpFaces.ResumeLayout(false);
            this.grpFaces.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpFaces;
        private System.Windows.Forms.CheckBox chkShowFaces;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
        private System.Windows.Forms.DataGridViewComboBoxColumn character;
        private System.Windows.Forms.DataGridViewTextBoxColumn duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn fontsize;
        private System.Windows.Forms.DataGridViewButtonColumn sound;
    }
}