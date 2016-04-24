namespace ReplicaStudio.Editor.Forms
{
    partial class GeneralSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSettings));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpPath = new System.Windows.Forms.GroupBox();
            this.btnChooseViewerPath = new System.Windows.Forms.Button();
            this.txtViewerPath = new System.Windows.Forms.TextBox();
            this.btnChooseGameFolder = new System.Windows.Forms.Button();
            this.txtGameFolder = new System.Windows.Forms.TextBox();
            this.lblViewerPath = new System.Windows.Forms.Label();
            this.lblGameFolder = new System.Windows.Forms.Label();
            this.grpBackgrounds = new System.Windows.Forms.GroupBox();
            this.ddpTransparentBlockSize = new System.Windows.Forms.ComboBox();
            this.lblTransparentBlockSize = new System.Windows.Forms.Label();
            this.lblTransparentBlockColor2 = new System.Windows.Forms.Label();
            this.lblTransparentBlockColor1 = new System.Windows.Forms.Label();
            this.grpMessages = new System.Windows.Forms.GroupBox();
            this.ddpMessageFontSize = new System.Windows.Forms.ComboBox();
            this.ddpMessageDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessageDuration = new System.Windows.Forms.Label();
            this.grpStages = new System.Windows.Forms.GroupBox();
            this.chkActivateZoomWithWheel = new System.Windows.Forms.CheckBox();
            this.chkShowCharacters = new System.Windows.Forms.CheckBox();
            this.chkShowAnimations = new System.Windows.Forms.CheckBox();
            this.ddpStagePadding = new System.Windows.Forms.NumericUpDown();
            this.ddpVectorPointsSize = new System.Windows.Forms.NumericUpDown();
            this.lblVectorPointsSize = new System.Windows.Forms.Label();
            this.lblStagePadding = new System.Windows.Forms.Label();
            this.lblShowAnimationsWhileMasking = new System.Windows.Forms.Label();
            this.lblShowCharactersWhileMasking = new System.Windows.Forms.Label();
            this.lblActivateZoomWithWheel = new System.Windows.Forms.Label();
            this.lblSelectedHotSpotColor = new System.Windows.Forms.Label();
            this.lblSelectionCoords = new System.Windows.Forms.Label();
            this.lblHighlightningBrush = new System.Windows.Forms.Label();
            this.lblHighlightningColor = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddpAnimationDefaultFrequency = new System.Windows.Forms.ComboBox();
            this.lblAnimationDefaultFrequency = new System.Windows.Forms.Label();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.OpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.colorSelectedHotSpotColor = new ReplicaStudio.Editor.Forms.UserControls.ColorButton();
            this.colorSelectionCoords = new ReplicaStudio.Editor.Forms.UserControls.ColorButton();
            this.colorChooseHighlightningBrush = new ReplicaStudio.Editor.Forms.UserControls.ColorButton();
            this.colorChooseHighlightningColor = new ReplicaStudio.Editor.Forms.UserControls.ColorButton();
            this.colorTransparentBlockColor2 = new ReplicaStudio.Editor.Forms.UserControls.ColorButton();
            this.colorTransparentBlockColor1 = new ReplicaStudio.Editor.Forms.UserControls.ColorButton();
            this.grpPath.SuspendLayout();
            this.grpBackgrounds.SuspendLayout();
            this.grpMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpMessageDuration)).BeginInit();
            this.grpStages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpStagePadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpVectorPointsSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
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
            // grpPath
            // 
            resources.ApplyResources(this.grpPath, "grpPath");
            this.grpPath.Controls.Add(this.btnChooseViewerPath);
            this.grpPath.Controls.Add(this.txtViewerPath);
            this.grpPath.Controls.Add(this.btnChooseGameFolder);
            this.grpPath.Controls.Add(this.txtGameFolder);
            this.grpPath.Controls.Add(this.lblViewerPath);
            this.grpPath.Controls.Add(this.lblGameFolder);
            this.grpPath.Name = "grpPath";
            this.grpPath.TabStop = false;
            // 
            // btnChooseViewerPath
            // 
            resources.ApplyResources(this.btnChooseViewerPath, "btnChooseViewerPath");
            this.btnChooseViewerPath.Name = "btnChooseViewerPath";
            this.btnChooseViewerPath.UseVisualStyleBackColor = true;
            this.btnChooseViewerPath.Click += new System.EventHandler(this.btnChooseViewerPath_Click);
            // 
            // txtViewerPath
            // 
            resources.ApplyResources(this.txtViewerPath, "txtViewerPath");
            this.txtViewerPath.Name = "txtViewerPath";
            // 
            // btnChooseGameFolder
            // 
            resources.ApplyResources(this.btnChooseGameFolder, "btnChooseGameFolder");
            this.btnChooseGameFolder.Name = "btnChooseGameFolder";
            this.btnChooseGameFolder.UseVisualStyleBackColor = true;
            this.btnChooseGameFolder.Click += new System.EventHandler(this.btnChooseGameFolder_Click);
            // 
            // txtGameFolder
            // 
            resources.ApplyResources(this.txtGameFolder, "txtGameFolder");
            this.txtGameFolder.Name = "txtGameFolder";
            // 
            // lblViewerPath
            // 
            resources.ApplyResources(this.lblViewerPath, "lblViewerPath");
            this.lblViewerPath.Name = "lblViewerPath";
            // 
            // lblGameFolder
            // 
            resources.ApplyResources(this.lblGameFolder, "lblGameFolder");
            this.lblGameFolder.Name = "lblGameFolder";
            // 
            // grpBackgrounds
            // 
            resources.ApplyResources(this.grpBackgrounds, "grpBackgrounds");
            this.grpBackgrounds.Controls.Add(this.colorTransparentBlockColor2);
            this.grpBackgrounds.Controls.Add(this.colorTransparentBlockColor1);
            this.grpBackgrounds.Controls.Add(this.ddpTransparentBlockSize);
            this.grpBackgrounds.Controls.Add(this.lblTransparentBlockSize);
            this.grpBackgrounds.Controls.Add(this.lblTransparentBlockColor2);
            this.grpBackgrounds.Controls.Add(this.lblTransparentBlockColor1);
            this.grpBackgrounds.Name = "grpBackgrounds";
            this.grpBackgrounds.TabStop = false;
            // 
            // ddpTransparentBlockSize
            // 
            resources.ApplyResources(this.ddpTransparentBlockSize, "ddpTransparentBlockSize");
            this.ddpTransparentBlockSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpTransparentBlockSize.FormattingEnabled = true;
            this.ddpTransparentBlockSize.Name = "ddpTransparentBlockSize";
            // 
            // lblTransparentBlockSize
            // 
            resources.ApplyResources(this.lblTransparentBlockSize, "lblTransparentBlockSize");
            this.lblTransparentBlockSize.Name = "lblTransparentBlockSize";
            // 
            // lblTransparentBlockColor2
            // 
            resources.ApplyResources(this.lblTransparentBlockColor2, "lblTransparentBlockColor2");
            this.lblTransparentBlockColor2.Name = "lblTransparentBlockColor2";
            // 
            // lblTransparentBlockColor1
            // 
            resources.ApplyResources(this.lblTransparentBlockColor1, "lblTransparentBlockColor1");
            this.lblTransparentBlockColor1.Name = "lblTransparentBlockColor1";
            // 
            // grpMessages
            // 
            resources.ApplyResources(this.grpMessages, "grpMessages");
            this.grpMessages.Controls.Add(this.ddpMessageFontSize);
            this.grpMessages.Controls.Add(this.ddpMessageDuration);
            this.grpMessages.Controls.Add(this.label1);
            this.grpMessages.Controls.Add(this.lblMessageDuration);
            this.grpMessages.Name = "grpMessages";
            this.grpMessages.TabStop = false;
            // 
            // ddpMessageFontSize
            // 
            resources.ApplyResources(this.ddpMessageFontSize, "ddpMessageFontSize");
            this.ddpMessageFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpMessageFontSize.FormattingEnabled = true;
            this.ddpMessageFontSize.Name = "ddpMessageFontSize";
            // 
            // ddpMessageDuration
            // 
            resources.ApplyResources(this.ddpMessageDuration, "ddpMessageDuration");
            this.ddpMessageDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ddpMessageDuration.Name = "ddpMessageDuration";
            this.ddpMessageDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblMessageDuration
            // 
            resources.ApplyResources(this.lblMessageDuration, "lblMessageDuration");
            this.lblMessageDuration.Name = "lblMessageDuration";
            // 
            // grpStages
            // 
            resources.ApplyResources(this.grpStages, "grpStages");
            this.grpStages.Controls.Add(this.colorSelectedHotSpotColor);
            this.grpStages.Controls.Add(this.colorSelectionCoords);
            this.grpStages.Controls.Add(this.colorChooseHighlightningBrush);
            this.grpStages.Controls.Add(this.colorChooseHighlightningColor);
            this.grpStages.Controls.Add(this.chkActivateZoomWithWheel);
            this.grpStages.Controls.Add(this.chkShowCharacters);
            this.grpStages.Controls.Add(this.chkShowAnimations);
            this.grpStages.Controls.Add(this.ddpStagePadding);
            this.grpStages.Controls.Add(this.ddpVectorPointsSize);
            this.grpStages.Controls.Add(this.lblVectorPointsSize);
            this.grpStages.Controls.Add(this.lblStagePadding);
            this.grpStages.Controls.Add(this.lblShowAnimationsWhileMasking);
            this.grpStages.Controls.Add(this.lblShowCharactersWhileMasking);
            this.grpStages.Controls.Add(this.lblActivateZoomWithWheel);
            this.grpStages.Controls.Add(this.lblSelectedHotSpotColor);
            this.grpStages.Controls.Add(this.lblSelectionCoords);
            this.grpStages.Controls.Add(this.lblHighlightningBrush);
            this.grpStages.Controls.Add(this.lblHighlightningColor);
            this.grpStages.Name = "grpStages";
            this.grpStages.TabStop = false;
            // 
            // chkActivateZoomWithWheel
            // 
            resources.ApplyResources(this.chkActivateZoomWithWheel, "chkActivateZoomWithWheel");
            this.chkActivateZoomWithWheel.Name = "chkActivateZoomWithWheel";
            this.chkActivateZoomWithWheel.UseVisualStyleBackColor = true;
            // 
            // chkShowCharacters
            // 
            resources.ApplyResources(this.chkShowCharacters, "chkShowCharacters");
            this.chkShowCharacters.Name = "chkShowCharacters";
            this.chkShowCharacters.UseVisualStyleBackColor = true;
            // 
            // chkShowAnimations
            // 
            resources.ApplyResources(this.chkShowAnimations, "chkShowAnimations");
            this.chkShowAnimations.Name = "chkShowAnimations";
            this.chkShowAnimations.UseVisualStyleBackColor = true;
            // 
            // ddpStagePadding
            // 
            resources.ApplyResources(this.ddpStagePadding, "ddpStagePadding");
            this.ddpStagePadding.Name = "ddpStagePadding";
            // 
            // ddpVectorPointsSize
            // 
            resources.ApplyResources(this.ddpVectorPointsSize, "ddpVectorPointsSize");
            this.ddpVectorPointsSize.Name = "ddpVectorPointsSize";
            // 
            // lblVectorPointsSize
            // 
            resources.ApplyResources(this.lblVectorPointsSize, "lblVectorPointsSize");
            this.lblVectorPointsSize.Name = "lblVectorPointsSize";
            // 
            // lblStagePadding
            // 
            resources.ApplyResources(this.lblStagePadding, "lblStagePadding");
            this.lblStagePadding.Name = "lblStagePadding";
            // 
            // lblShowAnimationsWhileMasking
            // 
            resources.ApplyResources(this.lblShowAnimationsWhileMasking, "lblShowAnimationsWhileMasking");
            this.lblShowAnimationsWhileMasking.Name = "lblShowAnimationsWhileMasking";
            // 
            // lblShowCharactersWhileMasking
            // 
            resources.ApplyResources(this.lblShowCharactersWhileMasking, "lblShowCharactersWhileMasking");
            this.lblShowCharactersWhileMasking.Name = "lblShowCharactersWhileMasking";
            // 
            // lblActivateZoomWithWheel
            // 
            resources.ApplyResources(this.lblActivateZoomWithWheel, "lblActivateZoomWithWheel");
            this.lblActivateZoomWithWheel.Name = "lblActivateZoomWithWheel";
            // 
            // lblSelectedHotSpotColor
            // 
            resources.ApplyResources(this.lblSelectedHotSpotColor, "lblSelectedHotSpotColor");
            this.lblSelectedHotSpotColor.Name = "lblSelectedHotSpotColor";
            // 
            // lblSelectionCoords
            // 
            resources.ApplyResources(this.lblSelectionCoords, "lblSelectionCoords");
            this.lblSelectionCoords.Name = "lblSelectionCoords";
            // 
            // lblHighlightningBrush
            // 
            resources.ApplyResources(this.lblHighlightningBrush, "lblHighlightningBrush");
            this.lblHighlightningBrush.Name = "lblHighlightningBrush";
            // 
            // lblHighlightningColor
            // 
            resources.ApplyResources(this.lblHighlightningColor, "lblHighlightningColor");
            this.lblHighlightningColor.Name = "lblHighlightningColor";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ddpAnimationDefaultFrequency);
            this.groupBox1.Controls.Add(this.lblAnimationDefaultFrequency);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ddpAnimationDefaultFrequency
            // 
            resources.ApplyResources(this.ddpAnimationDefaultFrequency, "ddpAnimationDefaultFrequency");
            this.ddpAnimationDefaultFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddpAnimationDefaultFrequency.FormattingEnabled = true;
            this.ddpAnimationDefaultFrequency.Name = "ddpAnimationDefaultFrequency";
            // 
            // lblAnimationDefaultFrequency
            // 
            resources.ApplyResources(this.lblAnimationDefaultFrequency, "lblAnimationDefaultFrequency");
            this.lblAnimationDefaultFrequency.Name = "lblAnimationDefaultFrequency";
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "viewer.exe";
            resources.ApplyResources(this.OpenFile, "OpenFile");
            // 
            // OpenFolder
            // 
            resources.ApplyResources(this.OpenFolder, "OpenFolder");
            // 
            // colorSelectedHotSpotColor
            // 
            resources.ApplyResources(this.colorSelectedHotSpotColor, "colorSelectedHotSpotColor");
            this.colorSelectedHotSpotColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorSelectedHotSpotColor.Name = "colorSelectedHotSpotColor";
            this.colorSelectedHotSpotColor.SelectedColor = System.Drawing.Color.Empty;
            this.colorSelectedHotSpotColor.SelectedVOColor = null;
            // 
            // colorSelectionCoords
            // 
            resources.ApplyResources(this.colorSelectionCoords, "colorSelectionCoords");
            this.colorSelectionCoords.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorSelectionCoords.Name = "colorSelectionCoords";
            this.colorSelectionCoords.SelectedColor = System.Drawing.Color.Empty;
            this.colorSelectionCoords.SelectedVOColor = null;
            // 
            // colorChooseHighlightningBrush
            // 
            resources.ApplyResources(this.colorChooseHighlightningBrush, "colorChooseHighlightningBrush");
            this.colorChooseHighlightningBrush.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorChooseHighlightningBrush.Name = "colorChooseHighlightningBrush";
            this.colorChooseHighlightningBrush.SelectedColor = System.Drawing.Color.Empty;
            this.colorChooseHighlightningBrush.SelectedVOColor = null;
            // 
            // colorChooseHighlightningColor
            // 
            resources.ApplyResources(this.colorChooseHighlightningColor, "colorChooseHighlightningColor");
            this.colorChooseHighlightningColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorChooseHighlightningColor.Name = "colorChooseHighlightningColor";
            this.colorChooseHighlightningColor.SelectedColor = System.Drawing.Color.Empty;
            this.colorChooseHighlightningColor.SelectedVOColor = null;
            // 
            // colorTransparentBlockColor2
            // 
            resources.ApplyResources(this.colorTransparentBlockColor2, "colorTransparentBlockColor2");
            this.colorTransparentBlockColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorTransparentBlockColor2.Name = "colorTransparentBlockColor2";
            this.colorTransparentBlockColor2.SelectedColor = System.Drawing.Color.Empty;
            this.colorTransparentBlockColor2.SelectedVOColor = null;
            // 
            // colorTransparentBlockColor1
            // 
            resources.ApplyResources(this.colorTransparentBlockColor1, "colorTransparentBlockColor1");
            this.colorTransparentBlockColor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorTransparentBlockColor1.Name = "colorTransparentBlockColor1";
            this.colorTransparentBlockColor1.SelectedColor = System.Drawing.Color.Empty;
            this.colorTransparentBlockColor1.SelectedVOColor = null;
            // 
            // GeneralSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpStages);
            this.Controls.Add(this.grpMessages);
            this.Controls.Add(this.grpBackgrounds);
            this.Controls.Add(this.grpPath);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneralSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.grpPath.ResumeLayout(false);
            this.grpPath.PerformLayout();
            this.grpBackgrounds.ResumeLayout(false);
            this.grpBackgrounds.PerformLayout();
            this.grpMessages.ResumeLayout(false);
            this.grpMessages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpMessageDuration)).EndInit();
            this.grpStages.ResumeLayout(false);
            this.grpStages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddpStagePadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddpVectorPointsSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpPath;
        private System.Windows.Forms.Label lblGameFolder;
        private System.Windows.Forms.Label lblViewerPath;
        private System.Windows.Forms.Button btnChooseViewerPath;
        private System.Windows.Forms.TextBox txtViewerPath;
        private System.Windows.Forms.Button btnChooseGameFolder;
        private System.Windows.Forms.TextBox txtGameFolder;
        private System.Windows.Forms.GroupBox grpBackgrounds;
        private System.Windows.Forms.Label lblTransparentBlockColor2;
        private System.Windows.Forms.Label lblTransparentBlockColor1;
        private System.Windows.Forms.GroupBox grpMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessageDuration;
        private System.Windows.Forms.NumericUpDown ddpMessageDuration;
        private System.Windows.Forms.ComboBox ddpTransparentBlockSize;
        private System.Windows.Forms.Label lblTransparentBlockSize;
        private System.Windows.Forms.GroupBox grpStages;
        private System.Windows.Forms.Label lblSelectionCoords;
        private System.Windows.Forms.Label lblHighlightningBrush;
        private System.Windows.Forms.Label lblHighlightningColor;
        private System.Windows.Forms.Label lblSelectedHotSpotColor;
        private System.Windows.Forms.NumericUpDown ddpVectorPointsSize;
        private System.Windows.Forms.Label lblVectorPointsSize;
        private System.Windows.Forms.Label lblStagePadding;
        private System.Windows.Forms.Label lblShowAnimationsWhileMasking;
        private System.Windows.Forms.Label lblShowCharactersWhileMasking;
        private System.Windows.Forms.Label lblActivateZoomWithWheel;
        private System.Windows.Forms.NumericUpDown ddpStagePadding;
        private System.Windows.Forms.CheckBox chkShowAnimations;
        private System.Windows.Forms.CheckBox chkShowCharacters;
        private System.Windows.Forms.CheckBox chkActivateZoomWithWheel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ddpAnimationDefaultFrequency;
        private System.Windows.Forms.Label lblAnimationDefaultFrequency;
        private UserControls.ColorButton colorTransparentBlockColor1;
        private UserControls.ColorButton colorTransparentBlockColor2;
        private UserControls.ColorButton colorSelectedHotSpotColor;
        private UserControls.ColorButton colorSelectionCoords;
        private UserControls.ColorButton colorChooseHighlightningBrush;
        private UserControls.ColorButton colorChooseHighlightningColor;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.FolderBrowserDialog OpenFolder;
        private System.Windows.Forms.ComboBox ddpMessageFontSize;
    }
}