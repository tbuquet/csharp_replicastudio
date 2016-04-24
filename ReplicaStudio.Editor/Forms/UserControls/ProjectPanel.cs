using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.ServiceLayer;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire du panneau de projet
    /// </summary>
    public partial class ProjectPanel : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ProjectPanelService _Service;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand une nouvelle doit être chargée
        /// </summary>
        public event EventHandler StageLoading;

        /// <summary>
        /// Survient quand la souris survole le contrôle
        /// </summary>
        public event EventHandler MouseEnterCustom;

        /// <summary>
        /// Survient quand une suppression de Stage est demandé
        /// </summary>
        public event EventHandler StageDelete;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ProjectPanel()
        {
            InitializeComponent();
            InitializeTree();
            _Service = new ProjectPanelService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialise l'arbre et assigne les icônes des types d'objets.
        /// </summary>
        private void InitializeTree()
        {
            ProjectTreeView.ImageList = new ImageList();
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.folder.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.folder.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.animation.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.animation.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.decor.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.decor.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.stage.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.stage.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.music.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.music.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.character.png")));
            ProjectTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.character.png")));
        }

        /// <summary>
        /// Rafraichi le ProjectPanel
        /// </summary>
        public void RefreshProjectPanel()
        {
            ProjectTreeView.Nodes.Clear();
            TreeNode animations = ProjectTreeView.Nodes.Add("", EditorConstants.Instance.PROJECTPANEL_ANIMATIONS, EditorConstants.PROJECTPANEL_FOLDER_INACTIVE, EditorConstants.PROJECTPANEL_FOLDER_ACTIVE);
            animations.Tag = Enums.StageObjectType.Animations;
            TreeNode characters = ProjectTreeView.Nodes.Add("", EditorConstants.Instance.PROJECTPANEL_CHARACTERS, EditorConstants.PROJECTPANEL_FOLDER_INACTIVE, EditorConstants.PROJECTPANEL_FOLDER_ACTIVE);
            characters.Tag = Enums.StageObjectType.Characters;
            TreeNode decors = ProjectTreeView.Nodes.Add("", EditorConstants.Instance.PROJECTPANEL_DECORS, EditorConstants.PROJECTPANEL_FOLDER_INACTIVE, EditorConstants.PROJECTPANEL_FOLDER_ACTIVE);
            decors.Tag = Enums.StageObjectType.Decors;
            TreeNode musics = ProjectTreeView.Nodes.Add("", EditorConstants.Instance.PROJECTPANEL_MUSICS, EditorConstants.PROJECTPANEL_FOLDER_INACTIVE, EditorConstants.PROJECTPANEL_FOLDER_ACTIVE);
            musics.Tag = Enums.StageObjectType.Musics;
            TreeNode stages = ProjectTreeView.Nodes.Add("", EditorConstants.Instance.PROJECTPANEL_STAGES, EditorConstants.PROJECTPANEL_FOLDER_INACTIVE, EditorConstants.PROJECTPANEL_FOLDER_ACTIVE);
            stages.Tag = Enums.StageObjectType.Stages;

            //Animations
            foreach (VO_Base voBase in GameCore.Instance.Game.ObjectAnimations)
            {
                animations.Nodes.Add(voBase.Id.ToString(), voBase.Title, EditorConstants.PROJECTPANEL_ANIMATION_INACTIVE, EditorConstants.PROJECTPANEL_ANIMATION_ACTIVE);
            }

            //Characters
            foreach (VO_Base voBase in GameCore.Instance.Game.Characters)
            {
                characters.Nodes.Add(voBase.Id.ToString(), voBase.Title, EditorConstants.PROJECTPANEL_CHARACTER_INACTIVE, EditorConstants.PROJECTPANEL_CHARACTER_ACTIVE);
            }

            //Decors
            List<string> listDecors = _Service.GetDecors();
            foreach (string decor in listDecors)
            {
                decors.Nodes.Add(decor, Path.GetFileName(decor), EditorConstants.PROJECTPANEL_DECOR_INACTIVE, EditorConstants.PROJECTPANEL_DECOR_ACTIVE);
            }

            //Scènes
            foreach (VO_Stage stage in GameCore.Instance.Game.Stages)
            {
                stages.Nodes.Add(stage.Id.ToString(), stage.Title, EditorConstants.PROJECTPANEL_STAGE_INACTIVE, EditorConstants.PROJECTPANEL_STAGE_ACTIVE);
            }

            //Musics
            List<string> vListMusics = _Service.GetMusics();
            foreach (string music in vListMusics)
            {
                musics.Nodes.Add(music, Path.GetFileName(music), EditorConstants.PROJECTPANEL_MUSIC_INACTIVE, EditorConstants.PROJECTPANEL_MUSIC_ACTIVE);
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Item récupéré
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (((TreeNode)e.Item).SelectedImageIndex != EditorConstants.PROJECTPANEL_FOLDER_ACTIVE)
            {
                EditorHelper.Instance.DragDropObjectType = (Enums.StageObjectType)(((TreeNode)e.Item).Parent.Tag);
                EditorHelper.Instance.DragDropItemId = ((TreeNode)e.Item).Name;
                this.DoDragDrop(this, DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// Double click sur une scène pour la charger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (ProjectTreeView.SelectedNode != null && ProjectTreeView.SelectedNode.Parent != null  && ((Enums.StageObjectType)ProjectTreeView.SelectedNode.Parent.Tag) == Enums.StageObjectType.Stages)
                this.StageLoading(ProjectTreeView.SelectedNode, new EventArgs());
        }

        /// <summary>
        /// Survient quand la souris entre dans le contrôle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectPanel_MouseEnter(object sender, EventArgs e)
        {
            this.MouseEnterCustom(null, new EventArgs());
        }

        /// <summary>
        /// Click souris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            ProjectTreeView.SelectedNode = ProjectTreeView.GetNodeAt(e.X, e.Y);
        }

        /// <summary>
        /// Evenement Clavier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectTreeView_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (ProjectTreeView.SelectedNode != null && ProjectTreeView.SelectedNode.Parent != null && ((Enums.StageObjectType)ProjectTreeView.SelectedNode.Parent.Tag) == Enums.StageObjectType.Stages)
                {
                    DialogResult result = MessageBox.Show(Notifications.Instance.STAGE_DELETE, Notifications.Instance.NOTIFICATION, MessageBoxButtons.YesNo);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            this.StageLoading(ProjectTreeView.SelectedNode, new EventArgs());
                            if (EditorHelper.Instance.CurrentStage != Guid.Empty)
                                this.StageDelete(ProjectTreeView.SelectedNode, new EventArgs());
                            break;
                        case DialogResult.No:
                            break;
                    }
                    e.Handled = true;
                }
            }
        }
        #endregion
    }
}
