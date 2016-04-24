using ReplicaStudio.TransverseLayer;
namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class ListItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItems));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.PanelAction = new System.Windows.Forms.Panel();
            this.PanelList = new System.Windows.Forms.Panel();
            this.List = new ReplicaStudio.TransverseLayer.RefreshingListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.PanelAction.SuspendLayout();
            this.PanelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // PanelAction
            // 
            resources.ApplyResources(this.PanelAction, "PanelAction");
            this.PanelAction.Controls.Add(this.btnDelete);
            this.PanelAction.Controls.Add(this.btnAdd);
            this.PanelAction.Name = "PanelAction";
            // 
            // PanelList
            // 
            resources.ApplyResources(this.PanelList, "PanelList");
            this.PanelList.Controls.Add(this.List);
            this.PanelList.Controls.Add(this.lblTitle);
            this.PanelList.Name = "PanelList";
            // 
            // List
            // 
            resources.ApplyResources(this.List, "List");
            this.List.DisplayMember = "Title";
            this.List.FormattingEnabled = true;
            this.List.Name = "List";
            this.List.ValueMember = "Id";
            this.List.DoubleClick += new System.EventHandler(this.List_DoubleClick);
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Name = "lblTitle";
            // 
            // ListItems
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelList);
            this.Controls.Add(this.PanelAction);
            this.Name = "ListItems";
            this.PanelAction.ResumeLayout(false);
            this.PanelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel PanelAction;
        private System.Windows.Forms.Panel PanelList;
        private System.Windows.Forms.Label lblTitle;
        private RefreshingListBox List;
    }
}
