namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    partial class ScriptChangeCurrentCharacter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptChangeCurrentCharacter));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblChangeCurrentCharacter = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.chkCurrent = new System.Windows.Forms.CheckBox();
            this.chkCoords = new System.Windows.Forms.CheckBox();
            this.crdCoords = new ReplicaStudio.Editor.Forms.UserControls.CoordsButton();
            this.chgCharacter = new ReplicaStudio.Editor.Forms.UserControls.CharacterButton();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.ChangeCurrentCharacter_Ok);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.ChangeCurrentCharacter_Cancel);
            // 
            // lblChangeCurrentCharacter
            // 
            resources.ApplyResources(this.lblChangeCurrentCharacter, "lblChangeCurrentCharacter");
            this.lblChangeCurrentCharacter.Name = "lblChangeCurrentCharacter";
            // 
            // lblLocation
            // 
            resources.ApplyResources(this.lblLocation, "lblLocation");
            this.lblLocation.Name = "lblLocation";
            // 
            // chkCurrent
            // 
            resources.ApplyResources(this.chkCurrent, "chkCurrent");
            this.chkCurrent.Name = "chkCurrent";
            this.chkCurrent.UseVisualStyleBackColor = true;
            this.chkCurrent.CheckedChanged += new System.EventHandler(this.chkCurrent_CheckedChanged);
            // 
            // chkCoords
            // 
            resources.ApplyResources(this.chkCoords, "chkCoords");
            this.chkCoords.Name = "chkCoords";
            this.chkCoords.UseVisualStyleBackColor = true;
            this.chkCoords.CheckedChanged += new System.EventHandler(this.chkCoords_CheckedChanged);
            // 
            // crdCoords
            // 
            this.crdCoords.Coords = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.crdCoords.FullCoords = null;
            resources.ApplyResources(this.crdCoords, "crdCoords");
            this.crdCoords.Name = "crdCoords";
            this.crdCoords.SourceResolution = new System.Drawing.Size(0, 0);
            this.crdCoords.UseStageBackground = false;
            this.crdCoords.UseStages = false;
            // 
            // chgCharacter
            // 
            this.chgCharacter.CharacterGuid = new System.Guid("00000000-0000-0000-0000-000000000000");
            resources.ApplyResources(this.chgCharacter, "chgCharacter");
            this.chgCharacter.Name = "chgCharacter";
            this.chgCharacter.UsePlayableCharacter = true;
            // 
            // ScriptChangeCurrentCharacter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.crdCoords);
            this.Controls.Add(this.chkCoords);
            this.Controls.Add(this.chkCurrent);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblChangeCurrentCharacter);
            this.Controls.Add(this.chgCharacter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScriptChangeCurrentCharacter";
            this.Load += new System.EventHandler(this.ScriptChangeCurrentCharacter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private UserControls.CharacterButton chgCharacter;
        private System.Windows.Forms.Label lblChangeCurrentCharacter;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.CheckBox chkCurrent;
        private System.Windows.Forms.CheckBox chkCoords;
        private UserControls.CoordsButton crdCoords;
    }
}