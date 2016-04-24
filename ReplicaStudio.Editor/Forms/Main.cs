using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.ServiceLayer;
using ReplicaStudio.TransverseLayer;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using System.Diagnostics;
using ReplicaStudio.Editor.Properties;
using Ini;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms
{
    /// <summary>
    /// Formulaire principal
    /// </summary>
    public partial class Main : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ProjectService _Service;

        /// <summary>
        /// Référence au formulaire GeneralSettings
        /// </summary>
        GeneralSettings _GeneralSettings = null;

        /// <summary>
        /// Référence au formulaire Database
        /// </summary>
        Database _Database = null;

        /// <summary>
        /// Référence au formulaire About
        /// </summary>
        About _About = null;

        /// <summary>
        /// Référence au formulaire New Project
        /// </summary>
        NewProject _NewProject = null;

        /// <summary>
        /// Référence au formulaire New Stage
        /// </summary>
        NewStage _NewStage = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur de la fenêtre principale, charge SDL et pré-charge certaines fenêtres.
        /// </summary>
        public Main(string[] args)
        {
            this.Icon = Resources.AppIcon;
            this.Text = GeneralSettingsConstants.APPLICATION_NAME + " - " + AppTools.GetVersionLitteralName();
            InitializeComponent();
            InitializeStageWindow();
            InitProgram();

            //1 - Charger les paramètres
            LoadSettings(args);

            //2 - Precocher la langue courante
            CheckCurrentCulture();

            //TODO: Afficher Documentation (Voir Menu About)
            //TODO: Afficher WebSite  (Voir Menu About)
        }
        #endregion

        #region Methods
        //Précharge un projet passé en paramètre
        private void LoadSettings(string[] args)
        {
            if(args.Length == 1)
            {
                Cursor.Current = Cursors.WaitCursor;
                DeactivateProject(true);
                _Service.LoadProject(args[0]);
                ProjectPanel.RefreshProjectPanel();
                ActivateProject();
                Cursor.Current = DefaultCursor;
            }
        }

        //Coche la langue actuellement en cours, dans le menu langage
        private void CheckCurrentCulture()
        {
            string CurrentCulture = EditorSettings.Instance.GetValue(EditorConstants.CONFIG_KEY_LANGUAGE, GeneralSettingsConstants.DEFAULT_LANGUAGE);
            this.frenchToolStripMenuItem.Checked = false;
            this.englishToolStripMenuItem.Checked = false;
            if (CurrentCulture == "fr-FR")
                this.frenchToolStripMenuItem.Checked = true;
            else
                this.englishToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// Initialisation des différentes fenêtres
        /// </summary>
        public void InitProgram()
        {
            //Initialisations
            DeactivateProject(true);
            _Database = new Database();
            _Database.FormClosing += new FormClosingEventHandler(mComponent_FormClosing);
            _GeneralSettings = new GeneralSettings();
            _GeneralSettings.FormClosing += new FormClosingEventHandler(_GeneralSettings_FormClosing);
            _About = new About();
            _NewProject = new NewProject();
            _NewProject.FormClosing += new FormClosingEventHandler(_NewProject_FormClosing);
            _NewStage = new NewStage();
            _Service = new ProjectService();
            FormsManager.Instance.InitManager();

            //Resizing
            Main_Resize(null, new EventArgs());
        }

        /// <summary>
        /// Désactive tous les contrôles projet
        /// </summary>
        public void DeactivateProject(bool resetGameCore)
        {
            MainMenu_Save.Enabled = false;
            MainMenu_Build.Enabled = false;
            MainMenu_Drawing.Enabled = false;
            MainMenu_Zoom.Enabled = false;
            MainMenu_Edition.Enabled = false;
            MainMenu_Game.Enabled = false;
            MainMenu_Resources.Enabled = false;
            MainMenu_Mode.Enabled = false;
            MainMenu_ShowHideLeftPanel.Enabled = false;
            MainMenu_ShowHideRightPanel.Enabled = false;

            Toolbar_Save.Enabled = false;
            Toolbar_Copy.Enabled = false;
            Toolbar_CreateNewMap.Enabled = false;
            Toolbar_Cut.Enabled = false;
            Toolbar_Database.Enabled = false;
            Toolbar_Decor.Enabled = false;
            Toolbar_Delete.Enabled = false;
            Toolbar_Events.Enabled = false;
            Toolbar_Pencil.Enabled = false;
            Toolbar_Objects.Enabled = false;
            Toolbar_Characters.Enabled = false;
            Toolbar_Paste.Enabled = false;
            Toolbar_Pointer.Enabled = false;
            Toolbar_Regions.Enabled = false;
            Toolbar_ResourcesManager.Enabled = false;
            Toolbar_Try.Enabled = false;
            Toolbar_Walk.Enabled = false;
            Toolbar_Z11.Enabled = false;
            Toolbar_Z12.Enabled = false;
            Toolbar_Z14.Enabled = false;
            Toolbar_Z18.Enabled = false;

            LeftPanel.Visible = false;
            RightPanel.Visible = false;
            PanelMiddleSplitted.Visible = false;
            StagePanel.Visible = false;

            LayersPanel.ResetLayers();
            StageObjectsPanel.ResetStageObjectsPanel();
            StageObjectsPanel.DesactivateButtons();
            if(resetGameCore)
                GameCore.Instance.ResetGameCore();
            EditorHelper.Instance.ResetEditorHelper();
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Decors;
        }

        private void ActivateStage()
        {
            MainMenu_Drawing.Enabled = true;
            MainMenu_Zoom.Enabled = true;
            MainMenu_Mode.Enabled = true;
            MainMenu_DeleteStage.Enabled = true;

            Toolbar_Walk.Enabled = true;
            Toolbar_Z11.Enabled = true;
            Toolbar_Z12.Enabled = true;
            Toolbar_Z14.Enabled = true;
            Toolbar_Z18.Enabled = true;
            Toolbar_Decor.Enabled = true;
            Toolbar_Events.Enabled = true;
            Toolbar_Characters.Enabled = true;
            Toolbar_Objects.Enabled = true;
            Toolbar_Characters.Enabled = true;
            Toolbar_Regions.Enabled = true;
        }

        private void DeactivateStage()
        {
            MainMenu_Drawing.Enabled = false;
            MainMenu_Zoom.Enabled = false;
            MainMenu_Mode.Enabled = false;
            MainMenu_DeleteStage.Enabled = false;

            Toolbar_Walk.Enabled = false;
            Toolbar_Z11.Enabled = false;
            Toolbar_Z12.Enabled = false;
            Toolbar_Z14.Enabled = false;
            Toolbar_Z18.Enabled = false;
            Toolbar_Decor.Enabled = false;
            Toolbar_Events.Enabled = false;
            Toolbar_Pencil.Enabled = false;
            Toolbar_Characters.Enabled = false;
            Toolbar_Objects.Enabled = false;
            Toolbar_Characters.Enabled = false;
            Toolbar_Pointer.Enabled = false;
            Toolbar_Regions.Enabled = false;

            ResetStageModes();
            ResetZoomModes();
            EditorHelper.Instance.CurrentStage = Guid.Empty;
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Decors;
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            EditorHelper.Instance.CurrentZoom = 1;
            Toolbar_Decor.Checked = true;
            MainMenu_Decors.Checked = true;
            MainMenu_Z11.Checked = true;
            Toolbar_Z11.Checked = true;

            ProjectPanel.RefreshProjectPanel();
            LayersPanel.RefreshLayers();
            StageObjectsPanel.RefreshStageObjectsPanel();
            InfoPanel.LoadObject(null);

            this.Focus();
        }

        /// <summary>
        /// Active tous les contrôles projet
        /// </summary>
        public void ActivateProject()
        {
            MainMenu_Build.Enabled = true;
            MainMenu_Save.Enabled = true;
            //MainMenu_Edition.Enabled = true;
            MainMenu_Game.Enabled = true;
            MainMenu_Resources.Enabled = true;
            MainMenu_ShowHideLeftPanel.Enabled = true;
            MainMenu_ShowHideRightPanel.Enabled = true;

            Toolbar_Save.Enabled = true;
            Toolbar_CreateNewMap.Enabled = true;
            Toolbar_Database.Enabled = true;
            Toolbar_ResourcesManager.Enabled = true;
            Toolbar_Try.Enabled = true;
            
            LeftPanel.Visible = true;
            RightPanel.Visible = true;
            PanelMiddleSplitted.Visible = true;

            ResetStageModes();
            ResetZoomModes();
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Decors;
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            EditorHelper.Instance.CurrentZoom = 1;
            Toolbar_Decor.Checked = true;
            MainMenu_Decors.Checked = true;
            MainMenu_Z11.Checked = true;
            Toolbar_Z11.Checked = true;

            this.Focus();
        }

        #endregion

        #region EventHandlers
        #region GeneralSettings
        /// <summary>
        /// Click sur GeneralSettings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_GeneralSettings_Click(object sender, EventArgs e)
        {
            _GeneralSettings.ShowDialog(this);
        }

        /// <summary>
        /// Lorsque GeneralSettings se ferme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _GeneralSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(EditorHelper.Instance.CurrentStage != Guid.Empty)
                StagePanel.LoadStage(EditorHelper.Instance.CurrentStage, EditorHelper.Instance.CurrentLayer);
        }
        #endregion

        #region Database
        /// <summary>
        /// Click sur Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Database_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //On entre en mode édition de la database et on instancie le GameCore temporaire.
            GameCore.Instance.SaveDB();

            //On affiche la base.
            _Database.InitializeDatabase();
            _Database.ShowDialog(this);

            Cursor.Current = DefaultCursor;
        }
        #endregion

        #region ResourcesManager
        /// <summary>
        /// Click sur Resources Manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_ResourcesManager_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.ResourcesManager.Filter = string.Empty;
            FormsManager.Instance.ResourcesManager.SelectedFilePath = string.Empty;
            FormsManager.Instance.ResourcesManager.FormClosing += new FormClosingEventHandler(ResourcesManager_FormClosing);
            FormsManager.Instance.ResourcesManager.ShowDialog(this);
        }

        /// <summary>
        /// Survient lorsque le Resources Manager est fermé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ResourcesManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormsManager.Instance.ResourcesManager.FormClosing -= new FormClosingEventHandler(ResourcesManager_FormClosing);
            mComponent_FormClosing(null, new FormClosingEventArgs(CloseReason.UserClosing, false));
        }
        #endregion

        #region About
        /// <summary>
        /// Click sur AboutUs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_AboutUs_Click(object sender, EventArgs e)
        {
            _About.ShowDialog(this);
        }
        #endregion

        #region New Project & Stages
        /// <summary>
        /// Lorsqu'un hotspot est fini d'éditer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StagePanel_HotSpotEditionCompleted(object sender, System.EventArgs e)
        {
            MainMenu_Pointer_Click(this, new EventArgs());
        }

        /// <summary>
        /// Lorsqu'un hotspot entre en édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StagePanel_HotSpotEditionBeginning(object sender, System.EventArgs e)
        {
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pencil;

            /* Edition par clic sur hotspot puis click sur pointer */
            //EditorHelper.Instance.SelectedHotSpot = null;
            //EditorHelper.Instance.HotSpotEditionMode = false;
            /* * * * * * * * * * * * * * * * * * * * * * * * * * * */

            DecheckDrawingTools();
            MainMenu_Pencil.Checked = true;
            Toolbar_Pencil.Checked = true;
            StagePanel.DeselectStageObjects();
            StagePanel.ChangeCursor(StagePanel.DrawingCursor);
        }

        /// <summary>
        /// Click sur New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_New_Click(object sender, EventArgs e)
        {
            _NewProject.ShowDialog(this);
        }

        /// <summary>
        /// Lorsque NewProject se ferme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _NewProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GameCore.Instance.Game.Project != null && !string.IsNullOrEmpty(GameCore.Instance.Game.Project.RootPath))
            {
                DeactivateProject(false);
                LayersPanel.ResetLayers();
                ProjectPanel.RefreshProjectPanel();
                ActivateProject();
            }
        }

        /// <summary>
        /// Lorsqu'une scène a besoin d'être chargée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectPanel_StageLoading(object sender, EventArgs e)
        {
            StagePanel.LoadStage(new Guid(((TreeNode)sender).Name));
            LayersPanel.RefreshLayers();
            StageObjectsPanel.RefreshStageObjectsPanel();
            InfoPanel.LoadObject(EditorHelper.Instance.GetCurrentStageInstance());
            SetCurrentMapInfos();
            StagePanel.Visible = true;
            ActivateStage();
        }

        /// <summary>
        /// Pour créer une nouvelle scène
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_CreateNewMap_Click(object sender, EventArgs e)
        {
            if (_NewStage.ShowDialog(this) == DialogResult.OK)
            {
                if (GameCore.Instance.Game.Project != null && !string.IsNullOrEmpty(GameCore.Instance.Game.Project.RootPath))
                {
                    ProjectPanel.RefreshProjectPanel();
                }
                if (GameCore.Instance.Game.Stages.Count > 0)
                {
                    StagePanel.LoadStage(EditorHelper.Instance.CurrentStage);
                    LayersPanel.RefreshLayers();
                    StageObjectsPanel.RefreshStageObjectsPanel();
                    InfoPanel.LoadObject(EditorHelper.Instance.GetCurrentStageInstance());
                    SetCurrentMapInfos();
                    StagePanel.Visible = true;
                    ActivateStage();
                }
            }
        }

        private void MainMenu_DeleteStage_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.CurrentStage != Guid.Empty)
            {
                EditorHelper.Instance.GetCurrentStageInstance().Delete();
                StagePanel.UnloadStage();
                DeactivateStage();
            }
        }
        #endregion

        #region LayersPanel
        /// <summary>
        /// Lorsqu'une scène a besoin d'être rafraichie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayersPanel_StageNeedsToBeRefreshed(object sender, EventArgs e)
        {
            StagePanel.DeselectStageObjects();
            StagePanel.RefreshStage();
        }

        /// <summary>
        /// Lorsqu'un calque est supprimé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayersPanel_LayerDeleted(object sender, EventArgs e)
        {
            StagePanel.ResetStageResources();
            StagePanel.LoadMinusMaximusLayers();
            StagePanel.RefreshStage();
        }

        /// <summary>
        /// Lorsque les couleurs d'un calque sont changées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayersPanel_ColorTransformationChanged(object sender, EventArgs e)
        {
            StagePanel.ResetStageResources();
            StagePanel.RefreshStage();
        }

        /// <summary>
        /// Lorsque le calque courant est changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayersPanel_CurrentLayerHasChanged(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.CurrentStage != new Guid() && EditorHelper.Instance.CurrentLayer != new Guid())
            {
                StagePanel.DeselectStageObjects();
                StagePanel.ResetStageResources();
                StagePanel.LoadMinusMaximusLayers();
                LayersPanel_StageNeedsToBeRefreshed(sender, new EventArgs());
                StageObjectsPanel.RefreshStageObjectsPanel();
            }
        }
        #endregion

        #region StageObjectsPanel
        /// <summary>
        /// Survient quand le StageObjects doit être rechargé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StagePanel_NeedToRefreshStageObjects(object sender, System.EventArgs e)
        {
            StageObjectsPanel.RefreshStageObjectsPanel();
        }

        /// <summary>
        /// Stage besoin d'être rafraichi après selection d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsPanel_StageNeedsToBeRefreshed(object sender, EventArgs e)
        {
            StagePanel.RefreshStage();
            StagePanel.LoadContextMenu();
        }

        /// <summary>
        /// Sélection besoin d'être rafraichie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsPanel_StageNeedsToRefreshSelection(object sender, EventArgs e)
        {
            StagePanel.RefreshSelection();
        }
        #endregion

        #region Main
        /// <summary>
        /// Rafraichi le panel de solution lorsque la fenêtre est fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mComponent_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProjectPanel.RefreshProjectPanel();
            if (EditorHelper.Instance.CurrentStage != new Guid() && EditorHelper.Instance.CurrentLayer != new Guid())
            {
                StagePanel.LoadMinusMaximusLayers();
                LayersPanel_StageNeedsToBeRefreshed(sender, new EventArgs());
            }
        }

        /// <summary>
        /// Chargement d'un jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Load_Click(object sender, EventArgs e)
        {
            if (GameCore.Instance.Game.Project == null || string.IsNullOrEmpty(GameCore.Instance.Game.Project.RootPath) || MessageBox.Show(Notifications.Instance.LOAD_PROJECT, Notifications.Instance.NOTIFICATION, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                string path = EditorSettings.Instance.GamesFolder;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                OpenFile.InitialDirectory = path;
                OpenFile.ShowDialog();
                Cursor.Current = DefaultCursor;
            }
        }

        /// <summary>
        /// Fichier choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_FileOk(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DeactivateProject(true);
            _Service.LoadProject(OpenFile.FileName);
            ProjectPanel.RefreshProjectPanel();
            ActivateProject();
            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Click save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Save_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _Service.SaveProject();
            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Click exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Lorsque l'appli quitte 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GameCore.Instance.Game.Project != null)
            {
                DialogResult result = MessageBox.Show(Notifications.Instance.PROJECT_LEAVE_APP, Notifications.Instance.NOTIFICATION, MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        _Service.SaveProject();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                } 
            }
        }
        #endregion

        #region DrawingTools
        /// <summary>
        /// Click sur Pointer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Pointer_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.SelectedHotSpot != null)
                StagePanel.UpdateAreaLocation(EditorHelper.Instance.SelectedHotSpot);
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            EditorHelper.Instance.SelectedHotSpot = null;
            EditorHelper.Instance.SelectedHotSpotPoint = -1;
            EditorHelper.Instance.HotSpotEditionMode = false;
            DecheckDrawingTools();
            MainMenu_Pointer.Checked = true;
            Toolbar_Pointer.Checked = true;
            StagePanel.ChangeCursor(Cursors.Default);
            StagePanel.RefreshSelection();
        }

        /// <summary>
        /// Click sur Pencil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Pencil_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pencil;

            /* Edition par clic sur hotspot puis click sur pointer */
            EditorHelper.Instance.SelectedHotSpot = null;
            EditorHelper.Instance.HotSpotEditionMode = false;
            /* * * * * * * * * * * * * * * * * * * * * * * * * * * */

            DecheckDrawingTools();
            MainMenu_Pencil.Checked = true;
            Toolbar_Pencil.Checked = true;
            StagePanel.DeselectStageObjects();
            StagePanel.ChangeCursor(StagePanel.DrawingCursor);
        }
        #endregion

        #region Zooms
        /// <summary>
        /// Quand le zoom change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StagePanel_ZoomChanged(object sender, System.EventArgs e)
        {
            switch (EditorHelper.Instance.CurrentZoom)
            {
                case 1:
                    MainMenu_Z11_Click(this, new EventArgs());
                    break;
                case 2:
                    MainMenu_Z12_Click(this, new EventArgs());
                    break;
                case 4:
                    MainMenu_Z14_Click(this, new EventArgs());
                    break;
                case 8:
                    MainMenu_Z18_Click(this, new EventArgs());
                    break;
            }
        }

        /// <summary>
        /// Click sur Zoom 1:1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Z11_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentZoom = 1;

            ResetZoomModes();
            MainMenu_Z11.Checked = true;
            Toolbar_Z11.Checked = true;
            StagePanel.LoadStage(EditorHelper.Instance.CurrentStage, EditorHelper.Instance.CurrentLayer);
        }

        /// <summary>
        /// Click sur Zoom 1:2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Z12_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentZoom = 2;

            ResetZoomModes();
            MainMenu_Z12.Checked = true;
            Toolbar_Z12.Checked = true;
            StagePanel.LoadStage(EditorHelper.Instance.CurrentStage, EditorHelper.Instance.CurrentLayer);
        }

        /// <summary>
        /// Click sur Zoom 1:4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Z14_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentZoom = 4;

            ResetZoomModes();
            MainMenu_Z14.Checked = true;
            Toolbar_Z14.Checked = true;
            StagePanel.LoadStage(EditorHelper.Instance.CurrentStage, EditorHelper.Instance.CurrentLayer);
        }

        /// <summary>
        /// Click sur Zoom 1:8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Z18_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentZoom = 8;

            ResetZoomModes();
            MainMenu_Z18.Checked = true;
            Toolbar_Z18.Checked = true;
            StagePanel.LoadStage(EditorHelper.Instance.CurrentStage, EditorHelper.Instance.CurrentLayer);
        }
        
        #endregion

        #region Stage Modes
        void StagePanel_CurrentModeHasChanged(object sender, System.EventArgs e)
        {
            switch (EditorHelper.Instance.CurrentStageState)
            {
                case Enums.StagePanelState.Decors:
                    MainMenu_Decors_Click(this, new EventArgs());
                    break;
                case Enums.StagePanelState.Objects:
                    MainMenu_Objects_Click(this, new EventArgs());
                    break;
                case Enums.StagePanelState.Characters:
                    MainMenu_Characters_Click(this, new EventArgs());
                    break;
            }
        }

        /// <summary>
        /// Click sur Decors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Decors_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Decors;
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            ResetStageModes();
            DeactivateDrawingTools();
            Toolbar_Decor.Checked = true;
            MainMenu_Decors.Checked = true;
            StageObjectsPanel.DesactivateButtons();
            LayersPanel.Enabled = true;
            LayersPanel.Visible = true;
            LayersPanel_CurrentLayerHasChanged(this, new EventArgs());
        }

        /// <summary>
        /// Click sur Objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Objects_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Objects;
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            ResetStageModes();
            DeactivateDrawingTools();
            Toolbar_Objects.Checked = true;
            MainMenu_Objects.Checked = true;
            StageObjectsPanel.DesactivateButtons();
            LayersPanel.Enabled = true;
            LayersPanel.Visible = true;
            LayersPanel_CurrentLayerHasChanged(this, new EventArgs());
        }

        /// <summary>
        /// Click sur Characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Characters_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Characters;
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            ResetStageModes();
            DeactivateDrawingTools();
            Toolbar_Characters.Checked = true;
            MainMenu_Characters.Checked = true;
            StageObjectsPanel.DesactivateButtons();
            LayersPanel.Enabled = false;
            LayersPanel.Visible = false;
            LayersPanel_CurrentLayerHasChanged(this, new EventArgs());
        }

        /// <summary>
        /// Click sur Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Events_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.HotSpots;
            StageObjectsPanel.DesactivateButtons();
            LayersPanel_CurrentLayerHasChanged(this, new EventArgs());
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            ResetStageModes();
            ActivateDrawingTools();
            LayersPanel.Enabled = false;
            LayersPanel.Visible = false;
            Toolbar_Events.Checked = true;
            MainMenu_Events.Checked = true;
        }

        /// <summary>
        /// Click sur Walkable Areas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Walks_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.WalkableAreas;
            StageObjectsPanel.DesactivateButtons();
            LayersPanel_CurrentLayerHasChanged(this, new EventArgs());
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            ResetStageModes();
            ActivateDrawingTools();
            LayersPanel.Enabled = true;
            LayersPanel.Visible = true;
            Toolbar_Walk.Checked = true;
            MainMenu_Walks.Checked = true;
        }

        /// <summary>
        /// Click sur Regions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Regions_Click(object sender, EventArgs e)
        {
            EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Regions;
            StageObjectsPanel.DesactivateButtons();
            LayersPanel_CurrentLayerHasChanged(this, new EventArgs());
            EditorHelper.Instance.CurrentDrawingTool = Enums.DrawingTools.Pointer;
            ResetStageModes();
            ActivateDrawingTools();
            LayersPanel.Enabled = false;
            LayersPanel.Visible = false;
            Toolbar_Regions.Checked = true;
            MainMenu_Regions.Checked = true;
        }

        /// <summary>
        /// Reset les boutons de modes de scène
        /// </summary>
        private void ResetStageModes()
        {
            Toolbar_Decor.Checked = false;
            Toolbar_Regions.Checked = false;
            Toolbar_Objects.Checked = false;
            Toolbar_Walk.Checked = false;
            Toolbar_Events.Checked = false;
            Toolbar_Characters.Checked = false;
            MainMenu_Decors.Checked = false;
            MainMenu_Regions.Checked = false;
            MainMenu_Walks.Checked = false;
            MainMenu_Objects.Checked = false;
            MainMenu_Events.Checked = false;
            MainMenu_Characters.Checked = false;
            DeactivateDrawingTools();
        }

        /// <summary>
        /// Reset les boutons de modes de scène
        /// </summary>
        private void ResetZoomModes()
        {
            MainMenu_Z11.Checked = false;
            MainMenu_Z12.Checked = false;
            MainMenu_Z14.Checked = false;
            MainMenu_Z18.Checked = false;
            Toolbar_Z11.Checked = false;
            Toolbar_Z12.Checked = false;
            Toolbar_Z14.Checked = false;
            Toolbar_Z18.Checked = false;
        }

        /// <summary>
        /// Active les outils de dessin
        /// </summary>
        private void ActivateDrawingTools()
        {
            MainMenu_Pointer.Enabled = true;
            MainMenu_Pointer.Checked = false;
            MainMenu_Pencil.Enabled = true;
            MainMenu_Pencil.Checked = false;
            Toolbar_Pointer.Enabled = true;
            Toolbar_Pointer.Checked = false;
            Toolbar_Pencil.Enabled = true;
            Toolbar_Pencil.Checked = false;
            MainMenu_Pointer_Click(this, new EventArgs());
        }

        /// <summary>
        /// Désactive les outils de dessin
        /// </summary>
        private void DeactivateDrawingTools()
        {
            MainMenu_Pointer.Enabled = false;
            MainMenu_Pointer.Checked = false;
            MainMenu_Pencil.Enabled = false;
            MainMenu_Pencil.Checked = false;
            Toolbar_Pointer.Enabled = false;
            Toolbar_Pointer.Checked = false;
            Toolbar_Pencil.Enabled = false;
            Toolbar_Pencil.Checked = false;
            StagePanel.ChangeCursor(Cursors.Default);
        }

        /// <summary>
        /// Décoche les outils de dessins
        /// </summary>
        private void DecheckDrawingTools()
        {
            MainMenu_Pointer.Checked = false;
            MainMenu_Pencil.Checked = false;
            Toolbar_Pointer.Checked = false;
            Toolbar_Pencil.Checked = false;
        }
        #endregion

        #region StatusBar
        #region Status Descriptions
        private void MainMenu_New_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_NEW;
        }
        private void MainMenu_Load_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_LOAD;
        }
        private void MainMenu_Save_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_SAVE;
        }
        private void MainMenu_GeneralSettings_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_GENERALSETTINGS;
        }
        private void MainMenu_Exit_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_EXIT;
        }
        private void MainMenu_File_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_FILE;
        }
        private void MainMenu_Edition_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_EDITION;
        }
        private void MainMenu_Cut_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_CUT;
        }
        private void MainMenu_Copy_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_COPY;
        }
        private void MainMenu_Paste_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_PASTE;
        }
        private void MainMenu_View_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_VIEW;
        }
        private void MainMenu_ShowHideToolbar_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_VIEW_TOOLBAR;
        }
        private void MainMenu_ShowHideRightPanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_VIEW_LEFT_BAR;
        }
        private void MainMenu_ShowHideLeftPanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_VIEW_RIGHT_BAR;
        }
        private void MainMenu_Mode_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE;
        }
        private void MainMenu_Decors_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE_DECORS;
        }
        private void MainMenu_Objects_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE_OBJECTS;
        }
        private void MainMenu_Characters_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE_CHARACTERS;
        }
        private void MainMenu_Events_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE_EVENTS;
        }
        private void MainMenu_Walks_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE_WALKABLE;
        }
        private void MainMenu_Regions_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_MODE_REGIONS;
        }
        private void MainMenu_Drawing_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_DRAWING;
        }
        private void MainMenu_Pointer_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_PENCIL;
        }
        private void MainMenu_Rectangle_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_RECTANGLE;
        }
        private void MainMenu_Circle_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_CIRCLE;
        }
        private void MainMenu_Pencil_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_PENCIL;
        }
        private void MainMenu_ImportMaskFromFile_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_IMPORTMASK;
        }
        private void MainMenu_Zoom_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ZOOM;
        }
        private void MainMenu_Z11_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ZOOM11;
        }
        private void MainMenu_Z12_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ZOOM12;
        }
        private void MainMenu_Z14_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ZOOM14;
        }
        private void MainMenu_Z18_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ZOOM18;
        }
        private void MainMenu_Resources_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_RESOURCES;
        }
        private void MainMenu_CreateNewMap_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_CREATEMAP;
        }
        private void MainMenu_Database_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_DATABASE;
        }
        private void MainMenu_ResourcesManager_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_RESOURCESMANAGER;
        }
        private void MainMenu_SpriteCreator_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_SPRITECREATOR;
        }
        private void MainMenu_Build_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_BUILD;
        }
        private void MainMenu_ExportToPC_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_BUILDTOWINDOWS;
        }
        private void MainMenu_Game_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_GAME;
        }
        private void MainMenu_Try_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_TRY;
        }
        private void MainMenu_TryFullscreen_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_FULLSCREEN_TRY;
        }
        private void MainMenu_About_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ABOUT;
        }
        private void MainMenu_AboutUs_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_ABOUTUS;
        }
        private void MainMenu_Documentation_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_DOCUMENT;
        }
        private void MainMenu_Website_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_WEBSITE;
        }
        private void grdInfos_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_INFOPANEL;
        }
        private void StagePanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_STAGE;
        }
        private void ProjectPanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_PROJECTPANEL;
        }
        private void LayersPanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_LAYERSPANEL;
        }
        private void MainMenu_Delete_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_DELETE;
        }
        private void Main_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = string.Empty;
        }
        private void PreviewPanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_PREVIEWPANEL;
        }
        private void StageObjectsPanel_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_STAGEOBJECTSPANEL;
        }
        private void MainMenu_Language_MouseEnter(object sender, EventArgs e)
        {
            lblStatusDescription.Text = EditorConstants.Instance.STATUS_DESC_LANGUAGE;
        }
        #endregion

        #region Status Map
        /// <summary>
        /// Affiche les informations sur la map courante.
        /// </summary>
        private void SetCurrentMapInfos()
        {
            lblStatusMap.Text = EditorHelper.Instance.GetCurrentStageInstance().Title + " (" + EditorHelper.Instance.GetCurrentStageInstance().Dimensions.Width + " x " + EditorHelper.Instance.GetCurrentStageInstance().Dimensions.Height + ")";
        }
        #endregion

        #region Status Mouse
        /// <summary>
        /// Survient pour information de la position relative de la souris sur la scène
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StagePanel_MousePosition(object sender, System.EventArgs e)
        {
            Point point = (Point)sender;
            lblPoints.Text = point.X + "," + point.Y;
        }

        /// <summary>
        /// Survient lorsqu'un item est choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Component_ItemChosen(object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                VO_StageObject vo = (VO_StageObject)sender;
                InfoPanel.LoadObject(sender);
                lblStatusObjectFocus.Text = vo.Location.X + " x " + vo.Location.Y + " (" + vo.Id + ")";
            }
            else
            {
                lblStatusObjectFocus.Text = string.Empty;
                InfoPanel.LoadObject(EditorHelper.Instance.GetCurrentStageInstance());
            }
        }
        #endregion
        #endregion

        #region View
        /// <summary>
        /// Afficher/Masquer la toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_ShowHideToolbar_Click(object sender, EventArgs e)
        {
            if (Toolbar.Visible)
            {
                Toolbar.Visible = false;
                MainMenu_ShowHideToolbar.Checked = true;
            }
            else
            {
                Toolbar.Visible = true;
                MainMenu_ShowHideToolbar.Checked = false;
            }
        }

        /// <summary>
        /// Afficher/Masquer le menu de gauche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_ShowHideLeftPanel_Click(object sender, EventArgs e)
        {
            if (LeftPanel.Visible)
            {
                LeftPanel.Visible = false;
                MainMenu_ShowHideLeftPanel.Checked = true;
            }
            else
            {
                LeftPanel.Visible = true;
                MainMenu_ShowHideLeftPanel.Checked = false;
            }
        }

        /// <summary>
        /// Afficher/Masquer le menu de droite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_ShowHideRightPanel_Click(object sender, EventArgs e)
        {
            if (RightPanel.Visible)
            {
                RightPanel.Visible = false;
                MainMenu_ShowHideRightPanel.Checked = true;
            }
            else
            {
                RightPanel.Visible = true;
                MainMenu_ShowHideRightPanel.Checked = false;
            }
        }
        #endregion

        #region Try
        private void MainMenu_Try_Click(object sender, EventArgs e)
        {
            LaunchTry(false);
        }

        private void MainMenu_TryFullscreen_Click(object sender, EventArgs e)
        {
            LaunchTry(true);
        }

        /// <summary>
        /// Lance le jeu
        /// </summary>
        /// <param name="fullscreen"></param>
        private void LaunchTry(bool fullscreen)
        {
            DialogResult result = MessageBox.Show(Notifications.Instance.PROJECT_TRY, Notifications.Instance.NOTIFICATION, MessageBoxButtons.YesNo);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    _Service.SaveProject();
                    if (ProjectIntegrity.CheckCurrentProjectIntegrity() == false)
                    {
                        MessageBox.Show(Culture.Language.Notifications.INTEGRITY_FAILED + "\r\n\r\n" + String.Join<string>("\r\n", ProjectIntegrity.ErrorList));
                        return;
                    }
                    _Service.LaunchProject(fullscreen);
                    break;
            }
        }
        #endregion

        #region External Tools
        private void MainMenu_SpriteCreator_Click(object sender, EventArgs e)
        {
            _Service.LaunchExternalTool(Application.StartupPath + EditorConstants.PATH_SCRIPT_CREATOR);
        }
        #endregion

        #region Export
        /// <summary>
        /// Click Export PC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_ExportToPC_Click(object sender, EventArgs e)
        {
            _Service.SaveProject();
            if (ProjectIntegrity.CheckCurrentProjectIntegrity() == false)
            {
                MessageBox.Show(Culture.Language.Notifications.INTEGRITY_FAILED + "\r\n\r\n" + String.Join<string>("\r\n", ProjectIntegrity.ErrorList));
                return;
            }
            if (OpenDirectory.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DialogResult result = System.Windows.Forms.DialogResult.Yes;
                if (FormsTools.IsFolderEmpty(OpenDirectory.SelectedPath) == false)
                    result = MessageBox.Show(Culture.Language.Notifications.EXPORT_FILES_EXISTS, Notifications.Instance.NOTIFICATION, MessageBoxButtons.YesNo);  
                switch (result)
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                            Enums.ExportState export = _Service.LaunchExport(OpenDirectory.SelectedPath);
                            switch (export)
                            {
                                case Enums.ExportState.OK:
                                    MessageBox.Show(EditorConstants.Instance.EXPORT_SUCCESS, EditorConstants.Instance.EXPORT_SUCCESS);
                                    break;
                                case Enums.ExportState.Error:
                                    MessageBox.Show(Errors.ERROR_EXPORT, Errors.ERROR_BOX_TITLE);
                                    break;
                            }
                            break;
                }
            }
        }
        #endregion

        #region Language

        private void ChangeLanguageEnglish_Click(object sender, EventArgs e)
        {
            EditorSettings.Instance.Language = "en-US";
            EditorSettings.Instance.SaveSettings("en-US");
            MessageBox.Show("In order for your settings to take effect, you must restart the application.");
        }

        private void ChangeLanguageFrench_Click(object sender, EventArgs e)
        {
            EditorSettings.Instance.Language = "fr-FR";
            EditorSettings.Instance.SaveSettings("fr-FR");
            MessageBox.Show("Pour que vos paramètres soient pris en compte, vous devez redémarrer l'application.");
        }

        #endregion

        #endregion

        #region Resizing
        private void Main_Resize(object sender, EventArgs e)
        {
            //StatusBar
            lblStatusDescription.Width = lblStatusMap.Parent.Bounds.X - 3;
        }
        #endregion    
    }
}
