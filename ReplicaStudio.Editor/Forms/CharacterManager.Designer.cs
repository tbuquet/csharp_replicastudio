namespace ReplicaStudio.Editor.Forms
{
    partial class CharacterManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterManager));
            this.ListCharacters = new ReplicaStudio.Editor.Forms.UserControls.ListItems();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListCharacters
            // 
            resources.ApplyResources(this.ListCharacters, "ListCharacters");
            this.ListCharacters.CancelDeletion = false;
            this.ListCharacters.DataSource = null;
            this.ListCharacters.DoubleClickable = true;
            this.ListCharacters.HideButtons = true;
            this.ListCharacters.ItemSelectedValue = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ListCharacters.Name = "ListCharacters";
            this.ListCharacters.Title = "Personnages";
            this.ListCharacters.ItemChosen += new System.EventHandler(this.ListCharacters_CharacterChosen);
            this.ListCharacters.ListIsEmpty += new System.EventHandler(this.ListCharacters_ListIsEmpty);
            this.ListCharacters.ItemDoubleClicked += new System.EventHandler(this.ListCharacters_CharacterDoubleClicked);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // CharacterManager
            // 
            this.AcceptButton = this.btnSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ListCharacters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ListItems ListCharacters;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
    }
}