using ReplicaStudio.Shared.TransverseLayer;
namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class AnimationManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationManager));
            this.grpInformations = new System.Windows.Forms.GroupBox();
            this.ddpSpriteH = new System.Windows.Forms.NumericUpDown();
            this.ddpSpriteW = new System.Windows.Forms.NumericUpDown();
            this.ddpRows = new System.Windows.Forms.NumericUpDown();
            this.lblRows = new System.Windows.Forms.Label();
            this.ddpFrequency = new System.Windows.Forms.NumericUpDown();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblDimensions = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.AnimPreview = new System.Windows.Forms.PictureBox();
            this.grpResource = new System.Windows.Forms.GroupBox();
            this.Resource = new System.Windows.Forms.PictureBox();
            this.ListAnimations = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.btnOriginPoint = new System.Windows.Forms.Button();
            this.grpInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpSpriteH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpSpriteW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpFrequency)).BeginInit();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreview)).BeginInit();
            this.grpResource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Resource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInformations
            // 
            this.grpInformations.Controls.Add(this.ddpSpriteH);
            this.grpInformations.Controls.Add(this.ddpSpriteW);
            this.grpInformations.Controls.Add(this.ddpRows);
            this.grpInformations.Controls.Add(this.lblRows);
            this.grpInformations.Controls.Add(this.ddpFrequency);
            this.grpInformations.Controls.Add(this.lblFrequency);
            this.grpInformations.Controls.Add(this.lblX);
            this.grpInformations.Controls.Add(this.lblDimensions);
            this.grpInformations.Controls.Add(this.txtName);
            this.grpInformations.Controls.Add(this.label1);
            resources.ApplyResources(this.grpInformations, "grpInformations");
            this.grpInformations.Name = "grpInformations";
            this.grpInformations.TabStop = false;
            // 
            // ddpSpriteH
            // 
            resources.ApplyResources(this.ddpSpriteH, "ddpSpriteH");
            this.ddpSpriteH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ddpSpriteH.Name = "ddpSpriteH";
            this.ddpSpriteH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ddpSpriteW
            // 
            resources.ApplyResources(this.ddpSpriteW, "ddpSpriteW");
            this.ddpSpriteW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ddpSpriteW.Name = "ddpSpriteW";
            this.ddpSpriteW.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ddpRows
            // 
            resources.ApplyResources(this.ddpRows, "ddpRows");
            this.ddpRows.Name = "ddpRows";
            // 
            // lblRows
            // 
            resources.ApplyResources(this.lblRows, "lblRows");
            this.lblRows.Name = "lblRows";
            // 
            // ddpFrequency
            // 
            resources.ApplyResources(this.ddpFrequency, "ddpFrequency");
            this.ddpFrequency.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.ddpFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ddpFrequency.Name = "ddpFrequency";
            this.ddpFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblFrequency
            // 
            resources.ApplyResources(this.lblFrequency, "lblFrequency");
            this.lblFrequency.Name = "lblFrequency";
            // 
            // lblX
            // 
            resources.ApplyResources(this.lblX, "lblX");
            this.lblX.Name = "lblX";
            // 
            // lblDimensions
            // 
            resources.ApplyResources(this.lblDimensions, "lblDimensions");
            this.lblDimensions.Name = "lblDimensions";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.AnimPreview);
            resources.ApplyResources(this.grpPreview, "grpPreview");
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.TabStop = false;
            // 
            // AnimPreview
            // 
            resources.ApplyResources(this.AnimPreview, "AnimPreview");
            this.AnimPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.AnimPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnimPreview.Name = "AnimPreview";
            this.AnimPreview.TabStop = false;
            // 
            // grpResource
            // 
            this.grpResource.Controls.Add(this.Resource);
            resources.ApplyResources(this.grpResource, "grpResource");
            this.grpResource.Name = "grpResource";
            this.grpResource.TabStop = false;
            // 
            // Resource
            // 
            resources.ApplyResources(this.Resource, "Resource");
            this.Resource.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.Resource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Resource.Name = "Resource";
            this.Resource.TabStop = false;
            this.Resource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Resource_MouseDown);
            // 
            // ListAnimations
            // 
            this.ListAnimations.CancelDeletion = false;
            this.ListAnimations.DataSource = null;
            resources.ApplyResources(this.ListAnimations, "ListAnimations");
            this.ListAnimations.DoubleClickable = false;
            this.ListAnimations.HideButtons = false;
            this.ListAnimations.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListAnimations.Name = "ListAnimations";
            this.ListAnimations.Title = "Animations";
            this.ListAnimations.ItemChosen += new System.EventHandler(this.ListAnimations_ItemChosen);
            this.ListAnimations.ItemToCreate += new System.EventHandler(this.ListAnimations_ItemToCreate);
            this.ListAnimations.ItemToDelete += new System.EventHandler(this.ListAnimations_ItemToDelete);
            this.ListAnimations.ListIsEmpty += new System.EventHandler(this.ListAnimations_ListIsEmpty);
            // 
            // btnOriginPoint
            // 
            resources.ApplyResources(this.btnOriginPoint, "btnOriginPoint");
            this.btnOriginPoint.Name = "btnOriginPoint";
            this.btnOriginPoint.UseVisualStyleBackColor = true;
            this.btnOriginPoint.Click += new System.EventHandler(this.btnOriginPoint_Click);
            // 
            // AnimationManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOriginPoint);
            this.Controls.Add(this.grpResource);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.grpInformations);
            this.Controls.Add(this.ListAnimations);
            this.Name = "AnimationManager";
            this.VisibleChanged += new System.EventHandler(this.AnimationManager_VisibleChanged);
            this.grpInformations.ResumeLayout(false);
            this.grpInformations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpSpriteH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpSpriteW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpFrequency)).EndInit();
            this.grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreview)).EndInit();
            this.grpResource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Resource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ListItems ListAnimations;
        private System.Windows.Forms.GroupBox grpInformations;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblDimensions;
        private System.Windows.Forms.NumericUpDown ddpFrequency;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.GroupBox grpResource;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.NumericUpDown ddpRows;
        private System.Windows.Forms.PictureBox AnimPreview;
        private System.Windows.Forms.NumericUpDown ddpSpriteH;
        private System.Windows.Forms.NumericUpDown ddpSpriteW;
        private System.Windows.Forms.PictureBox Resource;
        private System.Windows.Forms.Button btnOriginPoint;
    }
}
