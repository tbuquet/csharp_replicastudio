using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire de resources manager
    /// </summary>
    public partial class ResourcesManager : Form
    {
        #region Members
        /// <summary>
        /// Liste de répertoires
        /// </summary>
        List<VO_Directory> Directories = new List<VO_Directory>();

        /// <summary>
        /// Référence à la couche service
        /// </summary>
        ResourcesManagerService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Fichier sélectionné
        /// </summary>
        public string SelectedFilePath { get; set; }

        /// <summary>
        /// Filtre
        /// </summary>
        public string Filter { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ResourcesManager()
        {
            _Service = new ResourcesManagerService();
            InitializeComponent();
            btnDelete.Enabled = false;

            //TODO: Afficher le Bouton Preview (Voir Designer du RessourcesManager)
        }
        #endregion

        #region EventHandlers
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (string.IsNullOrEmpty(Filter))
            {
                Directories = _Service.BindListFolder(GameCore.Instance.Game.Project);
                btnSelect.Visible = false;
                btnDeselect.Visible = false;
            }
            else
            {
                Directories = _Service.BindListFolder(GameCore.Instance.Game.Project, Filter);
                btnSelect.Visible = true;
                btnDeselect.Visible = true;
            }
            ListFolders.DataSource = Directories.Select(d => d.Name).ToList();

            if (string.IsNullOrEmpty(SelectedFilePath))
                btnDeselect.Enabled = false;
            else
                btnDeselect.Enabled = true;
        }

        /// <summary>
        /// Close the resources manager window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Enable delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListFiles();
        }

        /// <summary>
        /// Delete the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ListFiles.SelectedItem != null)
            {
                string selectedFile = ListFiles.SelectedItem.ToString();
                string fileLocation = GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + Directories[ListFolders.SelectedIndex].Path + "\\" + selectedFile;

                File.Delete(fileLocation);
                BindListFiles();
            }
        }

        /// <summary>
        /// Add file to selected folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog filesToUp = new OpenFileDialog();
            filesToUp.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            filesToUp.RestoreDirectory = false;
            filesToUp.Filter = Directories[ListFolders.SelectedIndex].Extensions;
            if (filesToUp.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = filesToUp.FileName;
                string sourceFile = selectedFile;
                string destFile = Directories[ListFolders.SelectedIndex].Path + "\\" + Path.GetFileName(selectedFile);
                File.Copy(sourceFile, GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + destFile, true);
                BindListFiles();
            }
            else
            {
                //TODO: Manage error into log
            }
        }

        /// <summary>
        /// Renvoie un fichier en double cliquant dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListFiles_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Filter) && ListFiles.SelectedItems.Count != 0)
            {
                SelectedFilePath = ListFiles.SelectedValue.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Déselectionner l'item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeselect_Click(object sender, EventArgs e)
        {
            ListFiles.SelectedItem = null;
            SelectedFilePath = null;
            this.Close();
        }

        /// <summary>
        /// Sélectionner une ressource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            ListFiles_DoubleClick(this, new EventArgs());
        }

        /// <summary>
        /// Gestion du bouton select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListFiles.SelectedItems.Count != 0)
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Update the datasource of list files
        /// </summary>
        /// <param name="selectedDir"></param>
        private void BindListFiles()
        {
            List<string> files = new List<string>();
            string resourcesPath = GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES;
            string directory = resourcesPath + Directories[ListFolders.SelectedIndex].Path;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string[] rawFiles = Directory.GetFiles(directory);
            foreach (string rawFile in rawFiles)
            {
                files.Add(Path.GetFileName(rawFile));
            }
            btnDelete.Enabled = files.Count == 0 ? false : true;
            ListFiles.DataSource = files;
            ListFiles.SelectedItem = null;
            if (!string.IsNullOrEmpty(SelectedFilePath))
                ListFiles.SelectedItem = SelectedFilePath;
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
