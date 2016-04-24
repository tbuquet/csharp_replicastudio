namespace ReplicaStudio.Editor.Forms
{
    partial class CoordsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoordsManager));
            this.btnUp = new System.Windows.Forms.Button();
            this.btnMiddle = new System.Windows.Forms.Button();
            this.grpCoords = new System.Windows.Forms.GroupBox();
            this.ddpY = new System.Windows.Forms.NumericUpDown();
            this.ddpX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.Preview = new System.Windows.Forms.PictureBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpMaps = new System.Windows.Forms.GroupBox();
            this.ddpMap = new System.Windows.Forms.ComboBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpCoords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpX)).BeginInit();
            this.grpPreview.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            this.pnlLeft.SuspendLayout();
            this.grpMaps.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnMiddle
            // 
            resources.ApplyResources(this.btnMiddle, "btnMiddle");
            this.btnMiddle.Name = "btnMiddle";
            this.btnMiddle.UseVisualStyleBackColor = true;
            this.btnMiddle.Click += new System.EventHandler(this.btnMiddle_Click);
            // 
            // grpCoords
            // 
            resources.ApplyResources(this.grpCoords, "grpCoords");
            this.grpCoords.Controls.Add(this.ddpY);
            this.grpCoords.Controls.Add(this.ddpX);
            this.grpCoords.Controls.Add(this.label1);
            this.grpCoords.Controls.Add(this.lblX);
            this.grpCoords.Controls.Add(this.btnRight);
            this.grpCoords.Controls.Add(this.btnLeft);
            this.grpCoords.Controls.Add(this.btnDown);
            this.grpCoords.Controls.Add(this.btnUp);
            this.grpCoords.Controls.Add(this.btnMiddle);
            this.grpCoords.Name = "grpCoords";
            this.grpCoords.TabStop = false;
            // 
            // ddpY
            // 
            resources.ApplyResources(this.ddpY, "ddpY");
            this.ddpY.Name = "ddpY";
            // 
            // ddpX
            // 
            resources.ApplyResources(this.ddpX, "ddpX");
            this.ddpX.Name = "ddpX";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblX
            // 
            resources.ApplyResources(this.lblX, "lblX");
            this.lblX.Name = "lblX";
            // 
            // btnRight
            // 
            resources.ApplyResources(this.btnRight, "btnRight");
            this.btnRight.Name = "btnRight";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            resources.ApplyResources(this.btnLeft, "btnLeft");
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpPreview
            // 
            resources.ApplyResources(this.grpPreview, "grpPreview");
            this.grpPreview.Controls.Add(this.pnlPreview);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.TabStop = false;
            // 
            // pnlPreview
            // 
            resources.ApplyResources(this.pnlPreview, "pnlPreview");
            this.pnlPreview.Controls.Add(this.Preview);
            this.pnlPreview.Name = "pnlPreview";
            // 
            // Preview
            // 
            resources.ApplyResources(this.Preview, "Preview");
            this.Preview.Name = "Preview";
            this.Preview.TabStop = false;
            this.Preview.Paint += new System.Windows.Forms.PaintEventHandler(this.Preview_Paint);
            this.Preview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Preview_MouseClick);
            // 
            // pnlLeft
            // 
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Controls.Add(this.grpMaps);
            this.pnlLeft.Controls.Add(this.btnCancel);
            this.pnlLeft.Controls.Add(this.btnOK);
            this.pnlLeft.Controls.Add(this.grpCoords);
            this.pnlLeft.Name = "pnlLeft";
            // 
            // grpMaps
            // 
            resources.ApplyResources(this.grpMaps, "grpMaps");
            this.grpMaps.Controls.Add(this.ddpMap);
            this.grpMaps.Controls.Add(this.lblMap);
            this.grpMaps.Name = "grpMaps";
            this.grpMaps.TabStop = false;
            // 
            // ddpMap
            // 
            resources.ApplyResources(this.ddpMap, "ddpMap");
            this.ddpMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpMap.FormattingEnabled = true;
            this.ddpMap.Name = "ddpMap";
            // 
            // lblMap
            // 
            resources.ApplyResources(this.lblMap, "lblMap");
            this.lblMap.Name = "lblMap";
            // 
            // pnlRight
            // 
            resources.ApplyResources(this.pnlRight, "pnlRight");
            this.pnlRight.Controls.Add(this.grpPreview);
            this.pnlRight.Name = "pnlRight";
            // 
            // CoordsManager
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoordsManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.grpCoords.ResumeLayout(false);
            this.grpCoords.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpX)).EndInit();
            this.grpPreview.ResumeLayout(false);
            this.pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.grpMaps.ResumeLayout(false);
            this.grpMaps.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnMiddle;
        private System.Windows.Forms.GroupBox grpCoords;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.NumericUpDown ddpY;
        private System.Windows.Forms.NumericUpDown ddpX;
        private System.Windows.Forms.GroupBox grpMaps;
        private System.Windows.Forms.ComboBox ddpMap;
        private System.Windows.Forms.Label lblMap;
    }
}