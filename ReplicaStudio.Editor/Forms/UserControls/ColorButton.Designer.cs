namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class ColorButton
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
            this.colorImage = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.colorImage)).BeginInit();
            this.SuspendLayout();
            // 
            // colorImage
            // 
            this.colorImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorImage.Location = new System.Drawing.Point(0, 0);
            this.colorImage.Name = "colorImage";
            this.colorImage.Size = new System.Drawing.Size(20, 20);
            this.colorImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.colorImage.TabIndex = 0;
            this.colorImage.TabStop = false;
            this.colorImage.Click += new System.EventHandler(this.colorImage_Click);
            // 
            // ColorButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.colorImage);
            this.Name = "ColorButton";
            this.Size = new System.Drawing.Size(20, 20);
            ((System.ComponentModel.ISupportInitialize)(this.colorImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox colorImage;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}
