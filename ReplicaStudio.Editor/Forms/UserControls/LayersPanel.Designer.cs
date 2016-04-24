namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class LayersPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayersPanel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.LblLayers = new System.Windows.Forms.ToolStripLabel();
            this.BtnCreateLayer = new System.Windows.Forms.ToolStripButton();
            this.BtnDeleteLayer = new System.Windows.Forms.ToolStripButton();
            this.Separator = new System.Windows.Forms.ToolStripSeparator();
            this.BtnUp = new System.Windows.Forms.ToolStripButton();
            this.BtnDown = new System.Windows.Forms.ToolStripButton();
            this.GrdLayers = new System.Windows.Forms.DataGridView();
            this.GrdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrdKey = new System.Windows.Forms.DataGridViewImageColumn();
            this.GrdHidden = new System.Windows.Forms.DataGridViewImageColumn();
            this.GrdColor = new System.Windows.Forms.DataGridViewImageColumn();
            this.GrdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            resources.ApplyResources(this.Toolbar, "Toolbar");
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblLayers,
            this.BtnCreateLayer,
            this.BtnDeleteLayer,
            this.Separator,
            this.BtnUp,
            this.BtnDown});
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.MouseEnter += new System.EventHandler(this.LayersPanel_MouseEnter);
            // 
            // LblLayers
            // 
            resources.ApplyResources(this.LblLayers, "LblLayers");
            this.LblLayers.Name = "LblLayers";
            // 
            // BtnCreateLayer
            // 
            resources.ApplyResources(this.BtnCreateLayer, "BtnCreateLayer");
            this.BtnCreateLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnCreateLayer.Image = global::ReplicaStudio.Editor.Properties.Resources.add;
            this.BtnCreateLayer.Name = "BtnCreateLayer";
            this.BtnCreateLayer.Click += new System.EventHandler(this.BtnCreateLayer_Click);
            // 
            // BtnDeleteLayer
            // 
            resources.ApplyResources(this.BtnDeleteLayer, "BtnDeleteLayer");
            this.BtnDeleteLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnDeleteLayer.Image = global::ReplicaStudio.Editor.Properties.Resources.delete;
            this.BtnDeleteLayer.Name = "BtnDeleteLayer";
            this.BtnDeleteLayer.Click += new System.EventHandler(this.BtnDeleteLayer_Click);
            // 
            // Separator
            // 
            resources.ApplyResources(this.Separator, "Separator");
            this.Separator.Name = "Separator";
            // 
            // BtnUp
            // 
            resources.ApplyResources(this.BtnUp, "BtnUp");
            this.BtnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnUp.Image = global::ReplicaStudio.Editor.Properties.Resources.up;
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // BtnDown
            // 
            resources.ApplyResources(this.BtnDown, "BtnDown");
            this.BtnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnDown.Image = global::ReplicaStudio.Editor.Properties.Resources.down;
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // GrdLayers
            // 
            resources.ApplyResources(this.GrdLayers, "GrdLayers");
            this.GrdLayers.AllowUserToAddRows = false;
            this.GrdLayers.AllowUserToDeleteRows = false;
            this.GrdLayers.AllowUserToResizeColumns = false;
            this.GrdLayers.AllowUserToResizeRows = false;
            this.GrdLayers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GrdLayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GrdLayers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GrdLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GrdLayers.ColumnHeadersVisible = false;
            this.GrdLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GrdId,
            this.GrdKey,
            this.GrdHidden,
            this.GrdColor,
            this.GrdName});
            this.GrdLayers.MultiSelect = false;
            this.GrdLayers.Name = "GrdLayers";
            this.GrdLayers.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            this.GrdLayers.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.GrdLayers.RowTemplate.Height = 40;
            this.GrdLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrdLayers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdLayers_CellClick);
            this.GrdLayers.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GrdLayers_RowValidating);
            this.GrdLayers.MouseEnter += new System.EventHandler(this.LayersPanel_MouseEnter);
            // 
            // GrdId
            // 
            resources.ApplyResources(this.GrdId, "GrdId");
            this.GrdId.Name = "GrdId";
            // 
            // GrdKey
            // 
            resources.ApplyResources(this.GrdKey, "GrdKey");
            this.GrdKey.Name = "GrdKey";
            this.GrdKey.ReadOnly = true;
            // 
            // GrdHidden
            // 
            resources.ApplyResources(this.GrdHidden, "GrdHidden");
            this.GrdHidden.Name = "GrdHidden";
            // 
            // GrdColor
            // 
            resources.ApplyResources(this.GrdColor, "GrdColor");
            this.GrdColor.Name = "GrdColor";
            // 
            // GrdName
            // 
            this.GrdName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.GrdName, "GrdName");
            this.GrdName.Name = "GrdName";
            // 
            // LayersPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GrdLayers);
            this.Controls.Add(this.Toolbar);
            this.Name = "LayersPanel";
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip Toolbar;
        private System.Windows.Forms.ToolStripLabel LblLayers;
        private System.Windows.Forms.DataGridView GrdLayers;
        private System.Windows.Forms.ToolStripButton BtnCreateLayer;
        private System.Windows.Forms.ToolStripButton BtnDeleteLayer;
        private System.Windows.Forms.ToolStripSeparator Separator;
        private System.Windows.Forms.ToolStripButton BtnUp;
        private System.Windows.Forms.ToolStripButton BtnDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrdId;
        private System.Windows.Forms.DataGridViewImageColumn GrdKey;
        private System.Windows.Forms.DataGridViewImageColumn GrdHidden;
        private System.Windows.Forms.DataGridViewImageColumn GrdColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrdName;

    }
}
