using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire de création de projet
    /// </summary>
    public partial class NewProject : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ProjectService _Service;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public NewProject()
        {
            InitializeComponent();
            _Service = new ProjectService();
            ddpResolution.DataSource = _Service.LoadResolutions();
            ddpResolution.DisplayMember = "Title";
            ddpResolution.SelectedIndex = 0;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Click sur bouton pour choisir un dossier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            string path = EditorSettings.Instance.GamesFolder;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            OpenFolder.SelectedPath = path;
            OpenFolder.ShowDialog();
            txtProjectFolder.Text = OpenFolder.SelectedPath;
        }

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
        /// Click Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            #region Check des Champs (Nom Projet et Dossier)

            string chkTitle = txtTitle.Text;
            string chkProjectFolder = txtProjectFolder.Text;

            chkTitle = chkTitle.Trim();
            chkProjectFolder = chkProjectFolder.Trim();

            #endregion

            if (string.IsNullOrEmpty(chkTitle))
                MessageBox.Show(Errors.PROJECT_TITLE_EMPTY, Errors.ERROR_BOX_TITLE);
            else if (string.IsNullOrEmpty(chkProjectFolder) || !Directory.Exists(chkProjectFolder))
                MessageBox.Show(Errors.PROJECT_FOLDER_INCORRECT, Errors.ERROR_BOX_TITLE);
            else if (!_Service.CheckIfProjectExist(chkProjectFolder, chkTitle) || MessageBox.Show(Notifications.Instance.PROJECT_EXIST, Notifications.Instance.NOTIFICATION, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (_Service.CreateProject(new VO_Project(txtTitle.Text, (VO_Resolution)ddpResolution.SelectedItem, txtProjectFolder.Text)))
                {
                    _Service.SaveProject();
                    this.Close();
                }
            }
            Cursor.Current = DefaultCursor;
        } 
        #endregion

        #region Override
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
