using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire des paramètres généraux
    /// </summary>
    public partial class GeneralSettings : Form
    {
        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GeneralSettings()
        {
            InitializeComponent();
            ddpMessageDuration.Minimum = GlobalConstants.DIALOG_MIN_DURATION;
            ddpMessageDuration.Maximum = GlobalConstants.DIALOG_MAX_DURATION;
            ddpMessageFontSize.DataSource = FormsTools.GetMessageFontSizeList();
            ddpMessageFontSize.DisplayMember = "Title";
            ddpMessageFontSize.ValueMember = "Id";
            ddpStagePadding.Minimum = 0;
            ddpStagePadding.Maximum = GlobalConstants.STAGE_PADDING_LIMIT;
            ddpVectorPointsSize.Minimum = 1;
            ddpVectorPointsSize.Maximum = 10;
            ddpAnimationDefaultFrequency.DataSource = FormsTools.GetAnimationFrequencyList();
            ddpAnimationDefaultFrequency.DisplayMember = "Title";
            ddpAnimationDefaultFrequency.ValueMember = "Id";
            ddpTransparentBlockSize.Items.Add(8);
            ddpTransparentBlockSize.Items.Add(16);
            ddpTransparentBlockSize.Items.Add(32);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sauvegarde les settings
        /// </summary>
        private void SaveSettings()
        {
            //Création de la VO
            VO_EditorSettings settings = new VO_EditorSettings();
            settings.ActivateZoomWithWheel = chkActivateZoomWithWheel.Checked;
            settings.AnimationFrequency = (int)ddpAnimationDefaultFrequency.SelectedValue;
            settings.GamesFolder = txtGameFolder.Text;
            settings.HighlightningBrush = colorChooseHighlightningBrush.SelectedVOColor;
            settings.HighlightningColor = colorChooseHighlightningColor.SelectedVOColor;
            settings.MessageDuration = Convert.ToInt32(ddpMessageDuration.Value);
            settings.MessageFontSize = Convert.ToInt32(ddpMessageFontSize.SelectedValue);
            settings.SelectedHotSpotColor = colorSelectedHotSpotColor.SelectedVOColor;
            settings.SelectionCoords = colorSelectionCoords.SelectedVOColor;
            settings.StagePadding = Convert.ToInt32(ddpStagePadding.Value);
            settings.ShowAnimationsWhileMasking = chkShowAnimations.Checked;
            settings.ShowCharactersWhileMasking = chkShowCharacters.Checked;
            settings.TransparentBlockSize = (int)ddpTransparentBlockSize.SelectedItem;
            settings.TransparentColor1 = colorTransparentBlockColor1.SelectedVOColor;
            settings.TransparentColor2 = colorTransparentBlockColor2.SelectedVOColor;
            settings.VectorPointsSize = Convert.ToInt32(ddpVectorPointsSize.Value);
            settings.ViewerPath = txtViewerPath.Text;

            EditorSettings.Instance.SaveSettings(settings);
        }

        /// <summary>
        /// Recharge les settings
        /// </summary>
        private void ReloadSettings()
        {
            EditorSettings.Instance.LoadEditorSettings();
            ImageManager.ResetResources();
            EditorHelper.Instance.ReloadTransparentBlocs();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Click Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Click OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            ReloadSettings();
            this.Close();
        }

        /// <summary>
        /// Choose GameFolder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseGameFolder_Click(object sender, EventArgs e)
        {
            OpenFolder.SelectedPath = EditorSettings.Instance.GamesFolder;
            if (OpenFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtGameFolder.Text = OpenFolder.SelectedPath;
        }

        /// <summary>
        /// Choose ViewerPath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseViewerPath_Click(object sender, EventArgs e)
        {
            OpenFile.InitialDirectory = Application.StartupPath;
            if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtViewerPath.Text = OpenFile.FileName;
        }
        #endregion

        #region Override
        /// <summary>
        /// Chargement
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            txtGameFolder.Text = EditorSettings.Instance.GamesFolder;
            txtViewerPath.Text = EditorSettings.Instance.ViewerPath;
            OpenFile.FileName = GlobalConstants.VIEWER_NAME;
            ddpMessageDuration.Value = EditorSettings.Instance.MessageDuration;
            ddpMessageFontSize.SelectedValue = EditorSettings.Instance.MessageFontSize;
            ddpAnimationDefaultFrequency.SelectedValue = EditorSettings.Instance.AnimationFrequency;
            ddpStagePadding.Value = EditorSettings.Instance.StagePadding;
            ddpTransparentBlockSize.SelectedItem = EditorSettings.Instance.TransparentBlockSize;
            ddpVectorPointsSize.Value = EditorSettings.Instance.VectorPointsSize;
            chkActivateZoomWithWheel.Checked = EditorSettings.Instance.ActivateZoomWithWheel;
            chkShowAnimations.Checked = EditorSettings.Instance.ShowAnimationsWhileMasking;
            chkShowCharacters.Checked = EditorSettings.Instance.ShowCharactersWhileMasking;
            colorTransparentBlockColor1.SelectedColor = EditorSettings.Instance.TransparentColor1;
            colorTransparentBlockColor2.SelectedColor = EditorSettings.Instance.TransparentColor2;
            colorChooseHighlightningColor.SelectedColor = EditorSettings.Instance.HighlightningColor.Color;
            colorChooseHighlightningBrush.SelectedColor = EditorSettings.Instance.HighlightningBrushColor;
            colorSelectionCoords.SelectedColor = EditorSettings.Instance.SelectionCoords.Color;
            colorSelectedHotSpotColor.SelectedColor = EditorSettings.Instance.SelectedHotSpotColor.Color;
        }

        /// <summary>
        /// Désactiver F4
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;
            else
                return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
