namespace ReplicaStudio.Editor.Forms.UserControls
{
    partial class AnimationControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationControl));
            this.AnimPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // AnimPreview
            // 
            this.AnimPreview.AccessibleDescription = "SdlDotNet SurfaceControl";
            this.AnimPreview.AccessibleName = "SurfaceControl";
            this.AnimPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.AnimPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimPreview.InitialImage = null;
            this.AnimPreview.Location = new System.Drawing.Point(0, 0);
            this.AnimPreview.Name = "AnimPreview";
            this.AnimPreview.Size = new System.Drawing.Size(124, 171);
            this.AnimPreview.TabIndex = 0;
            this.AnimPreview.TabStop = false;
            // 
            // AnimationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.AnimPreview);
            this.Name = "AnimationControl";
            this.Size = new System.Drawing.Size(124, 171);
            ((System.ComponentModel.ISupportInitialize)(this.AnimPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox AnimPreview;
    }
}
