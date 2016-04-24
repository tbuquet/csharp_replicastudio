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
using System.Threading;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Editor.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire du panneau de projet
    /// </summary>
    public partial class StageObjectsPanel : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        LayersPanelService _Service;

        /// <summary>
        /// Booléen qui enregistre si une touche clavier est relâchée
        /// </summary>
        bool _KeyUp = false;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand la souris survole le contrôle
        /// </summary>
        public event EventHandler MouseEnterCustom;

        /// <summary>
        /// Survient quand la scène doit être rafraichie
        /// </summary>
        public event EventHandler StageNeedsToBeRefreshed;

        /// <summary>
        /// Survient quand la selection doit être rafraichie
        /// </summary>
        public event EventHandler StageNeedsToRefreshSelection;

        /// <summary>
        /// Survient lorsqu'un item est choisi
        /// </summary>
        public event EventHandler ItemChosen;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public StageObjectsPanel()
        {
            InitializeComponent();
            InitializeTree();
            _Service = new LayersPanelService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reset le StageObjects Panel
        /// </summary>
        public void ResetStageObjectsPanel()
        {
            StageObjectsTreeView.Nodes.Clear();
        }

        /// <summary>
        /// Initialise l'arbre et assigne les icônes des types d'objets.
        /// </summary>
        private void InitializeTree()
        {
            StageObjectsTreeView.ImageList = new ImageList();
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.folder.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.folder.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.animation.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.animation.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.decor.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.decor.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.character.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.ProjectPanel.character.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StateObjectPanel.hotspot.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StateObjectPanel.hotspot.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StateObjectPanel.region.gif")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StateObjectPanel.region.gif")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StateObjectPanel.walk.png")));
            StageObjectsTreeView.ImageList.Images.Add(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StateObjectPanel.walk.png")));
        }

        /// <summary>
        /// Rafraichi le ProjectPanel
        /// </summary>
        public void RefreshStageObjectsPanel()
        {
            if (EditorHelper.Instance.CurrentStage == Guid.Empty)
            {
                StageObjectsTreeView.Nodes.Clear();
                return;
            }

            StageObjectsTreeView.Nodes.Clear();

            VO_Layer currentLayer = EditorHelper.Instance.GetCurrentLayerInstance();
            VO_Stage currentStage = EditorHelper.Instance.GetCurrentStageInstance();
            switch (EditorHelper.Instance.CurrentStageState)
            {
                case Enums.StagePanelState.Decors:
                    foreach (VO_StageDecor decor in currentLayer.ListDecors)
                    {
                        StageObjectsTreeView.Nodes.Add(decor.Id.ToString(), decor.Title, EditorConstants.STAGEOBJECTSPANEL_DECOR_INACTIVE, EditorConstants.STAGEOBJECTSPANEL_DECOR_ACTIVE);
                    }
                    break;
                case Enums.StagePanelState.Objects:
                    foreach (VO_StageAnimation anim in currentLayer.ListAnimations)
                    {
                        StageObjectsTreeView.Nodes.Add(anim.Id.ToString(), anim.Title, EditorConstants.STAGEOBJECTSPANEL_ANIMATION_INACTIVE, EditorConstants.STAGEOBJECTSPANEL_ANIMATION_ACTIVE);
                    }
                    break;
                case Enums.StagePanelState.Characters:
                    foreach (VO_StageCharacter character in currentStage.ListCharacters)
                    {
                        StageObjectsTreeView.Nodes.Add(character.Id.ToString(), character.Title, EditorConstants.STAGEOBJECTSPANEL_CHARACTER_INACTIVE, EditorConstants.STAGEOBJECTSPANEL_CHARACTER_ACTIVE);
                    }
                    break;
                case Enums.StagePanelState.HotSpots:
                    foreach (VO_StageHotSpot decor in currentStage.ListHotSpots)
                    {
                        StageObjectsTreeView.Nodes.Add(decor.Id.ToString(), decor.Title, EditorConstants.STAGEOBJECTSPANEL_HOTSPOT_INACTIVE, EditorConstants.STAGEOBJECTSPANEL_HOTSPOT_ACTIVE);
                    }
                    break;
                case Enums.StagePanelState.WalkableAreas:
                    foreach (VO_StageHotSpot decor in currentLayer.ListWalkableAreas)
                    {
                        StageObjectsTreeView.Nodes.Add(decor.Id.ToString(), decor.Title, EditorConstants.STAGEOBJECTSPANEL_WALK_INACTIVE, EditorConstants.STAGEOBJECTSPANEL_WALK_ACTIVE);
                    }
                    break;
                case Enums.StagePanelState.Regions:
                    foreach (VO_StageHotSpot decor in currentStage.ListRegions)
                    {
                        StageObjectsTreeView.Nodes.Add(decor.Id.ToString(), decor.Title, EditorConstants.STAGEOBJECTSPANEL_REGION_INACTIVE, EditorConstants.STAGEOBJECTSPANEL_REGION_ACTIVE);
                    }
                    break;
            }
        }

        /// <summary>
        /// Désactive les boutons
        /// </summary>
        public void DesactivateButtons()
        {
            btnDelete.Enabled = false;
            btnEvent.Enabled = false;
        }

        /// <summary>
        /// Récupère un objet de scène depuis une node
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>StageObject</returns>
        private VO_StageObject GetStageObjectFromNode(TreeNode node)
        {
            Guid id = new Guid(((TreeNode)node).Name);
            VO_StageObject stageObject = null;

            DesactivateButtons();
            btnDelete.Enabled = true;
            switch (EditorHelper.Instance.CurrentStageState)
            {
                case Enums.StagePanelState.Objects:
                    stageObject = EditorHelper.Instance.GetCurrentLayerInstance().ListAnimations.Find(p => p.Id == id);
                    btnEvent.Enabled = true;
                    break;
                case Enums.StagePanelState.Decors:
                    stageObject = EditorHelper.Instance.GetCurrentLayerInstance().ListDecors.Find(p => p.Id == id);
                    break;
                case Enums.StagePanelState.Characters:
                    stageObject = EditorHelper.Instance.GetCurrentStageInstance().ListCharacters.Find(p => p.Id == id);
                    btnEvent.Enabled = true;
                    break;
                case Enums.StagePanelState.HotSpots:
                    if (EditorHelper.Instance.CurrentDrawingTool == Enums.DrawingTools.Pointer)
                    {
                        stageObject = EditorHelper.Instance.GetCurrentStageInstance().ListHotSpots.Find(p => p.Id == id);
                        btnEvent.Enabled = true;
                    }
                    break;
                case Enums.StagePanelState.WalkableAreas:
                    if (EditorHelper.Instance.CurrentDrawingTool == Enums.DrawingTools.Pointer)
                    {
                        stageObject = EditorHelper.Instance.GetCurrentLayerInstance().ListWalkableAreas.Find(p => p.Id == id);
                        btnEvent.Enabled = false;
                    }
                    break;
                case Enums.StagePanelState.Regions:
                    if (EditorHelper.Instance.CurrentDrawingTool == Enums.DrawingTools.Pointer)
                    {
                        stageObject = EditorHelper.Instance.GetCurrentStageInstance().ListRegions.Find(p => p.Id == id);
                        btnEvent.Enabled = false;
                    }
                    break;
            }
            return stageObject;
        }

        /// <summary>
        /// Lance l'EventManager
        /// </summary>
        private void LoadEventManager()
        {
            if (EditorHelper.Instance.SelectedObjects.Count == 1)
            {
                FormsManager.Instance.EventManager.CurrentStageObject = EditorHelper.Instance.SelectedObjects[0];
                switch (EditorHelper.Instance.SelectedObjects[0].ObjectType)
                {
                    case Enums.StageObjectType.Animations:
                        FormsManager.Instance.EventManager.EventType = Enums.EventType.Animation;
                        break;
                    case Enums.StageObjectType.Characters:
                        FormsManager.Instance.EventManager.EventType = Enums.EventType.Character;
                        break;
                    case Enums.StageObjectType.HotSpots:
                        FormsManager.Instance.EventManager.EventType = Enums.EventType.Event;
                        break;
                    case Enums.StageObjectType.Walkables:
                    case Enums.StageObjectType.Regions:
                    case Enums.StageObjectType.Decors:
                        return;
                }

                FormsManager.Instance.EventManager.CurrentEvent = EditorHelper.Instance.SelectedObjects[0].Event;
                GameCore.Instance.SaveEvent(FormsManager.Instance.EventManager.CurrentEvent);
                FormsManager.Instance.EventManager.FormClosing += new FormClosingEventHandler(EventManager_FormClosing);
                FormsManager.Instance.EventManager.ShowDialog(this);
            }
        }

        /// <summary>
        /// Valide si l'utilisateur est dans le bon mode pour effectuer des actions.
        /// </summary>
        private bool ValidateObjectAction()
        {
            if (EditorHelper.Instance.SelectedObjects.Count == 1)
            {
                VO_StageObject objectStage = EditorHelper.Instance.SelectedObjects[0];

                if (objectStage.ObjectType == Enums.StageObjectType.Animations && EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Objects)
                    return false;
                if (objectStage.ObjectType == Enums.StageObjectType.Characters && EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Characters)
                    return false;
                if (objectStage.ObjectType == Enums.StageObjectType.Decors && EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Decors)
                    return false;
                if (objectStage.ObjectType == Enums.StageObjectType.HotSpots && EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.HotSpots)
                    return false;
                if (objectStage.ObjectType == Enums.StageObjectType.Walkables && EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.WalkableAreas)
                    return false;
                if (objectStage.ObjectType == Enums.StageObjectType.Regions && EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Regions)
                    return false;
                return true;
            }
            return false;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Fermeture de l'Event Manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormsManager.Instance.EventManager.FormClosing -= new FormClosingEventHandler(EventManager_FormClosing);
            EditorHelper.Instance.SelectedObjects[0].Event = FormsManager.Instance.EventManager.CurrentEvent;
        }

        /// <summary>
        /// Gestion des flèches du clavier et bouton supprimer
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Haut
            if (keyData == (Keys.RButton | Keys.MButton | Keys.Space))
            {
                MoveObject(0, -1);
                return true;
            }
            //Gauche
            else if (keyData == (Keys.LButton | Keys.MButton | Keys.Space))
            {
                MoveObject(-1, 0);
                return true;
            }
            //Droite
            else if (keyData == (Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space))
            {
                MoveObject(1, 0);
                return true;
            }
            //Bas
            else if (keyData == (Keys.Back | Keys.Space))
            {
                MoveObject(0, 1);
                return true;
            }
            //Suppression
            else if (keyData == (Keys.RButton | Keys.MButton | Keys.Back | Keys.Space))
                DeleteSelectedItem();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Déplace un objet sur la scène
        /// </summary>
        /// <param name="pX">Position en X</param>
        /// <param name="pY">Position en Y</param>
        public void MoveObject(int x, int y)
        {
            if (EditorHelper.Instance.SelectedObjects.Count == 1 && ValidateObjectAction())
            {
                VO_StageObject objectStage = EditorHelper.Instance.SelectedObjects[0];
                Rectangle oldPosition = new Rectangle(objectStage.Location, objectStage.Size);
                objectStage.Location = new Point(EditorHelper.Instance.SelectedObjects[0].Location.X + x, EditorHelper.Instance.SelectedObjects[0].Location.Y + y);
                objectStage.Size = EditorHelper.Instance.SelectedObjects[0].Size;

                //Mouvement spécial hotspots
                if (objectStage.ObjectType == Enums.StageObjectType.HotSpots || objectStage.ObjectType == Enums.StageObjectType.Walkables || objectStage.ObjectType == Enums.StageObjectType.Regions)
                {
                    int movX = objectStage.Location.X - oldPosition.X;
                    int movY = objectStage.Location.Y - oldPosition.Y;
                    VO_StageHotSpot hotspot = (VO_StageHotSpot)objectStage;
                    MovePoints(hotspot.Points, movX, movY);
                }

                _KeyUp = true;
                this.StageNeedsToRefreshSelection(this, new EventArgs());
            }
            ReloadItemChosen();
        }

        /// <summary>
        /// Synchronise les points avec le padding de stage
        /// </summary>
        /// <param name="points">liste de points</param>
        /// <returns>liste de point</returns>
        public Point[] MovePoints(Point[] points, int x, int y)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X += x;
                points[i].Y += y;
            }
            return points;
        }

        /// <summary>
        /// Détermine si on doit afficher des infos d'un item dans le Status Bar et l'Information Panel
        /// </summary>
        private void ReloadItemChosen()
        {
            if (EditorHelper.Instance.SelectedObjects.Count == 1)
                this.ItemChosen(EditorHelper.Instance.SelectedObjects[0], new EventArgs());
            else
                this.ItemChosen(null, new EventArgs());
        }

        /// <summary>
        /// Lorsque les touches claviers sont relachées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            if (_KeyUp)
            {
                this.StageNeedsToBeRefreshed(this, new EventArgs());
                _KeyUp = false;
                ReloadItemChosen();
            }
        }

        /// <summary>
        /// Supprime les items sélectionnés
        /// </summary>
        public void DeleteSelectedItem()
        {
            if (EditorHelper.Instance.SelectedObjects.Count == 1 && ValidateObjectAction())
            {
                EditorHelper.Instance.SelectedObjects[0].Delete();
                EditorHelper.Instance.SelectedObjects = new List<VO_StageObject>();
                this.StageNeedsToBeRefreshed(this, new EventArgs());
                RefreshStageObjectsPanel();
                ReloadItemChosen();
            }
        }

        /// <summary>
        /// Click sur un item pour le sélectionner dans le stage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode selectedNode = StageObjectsTreeView.GetNodeAt(e.X, e.Y);
                if (selectedNode != null)
                {
                    VO_StageObject stageObject = GetStageObjectFromNode(selectedNode);
                    if (stageObject != null)
                    {
                        EditorHelper.Instance.SelectedObjects = new List<VO_StageObject>();
                        EditorHelper.Instance.SelectedObjects.Add(stageObject);
                        if (stageObject.ObjectType == Enums.StageObjectType.HotSpots || stageObject.ObjectType == Enums.StageObjectType.Walkables || stageObject.ObjectType == Enums.StageObjectType.Regions)
                        {
                            EditorHelper.Instance.SelectedHotSpot = (VO_StageHotSpot)stageObject;
                            EditorHelper.Instance.SelectedHotSpotPoint = -1;
                            EditorHelper.Instance.HotSpotEditionMode = true;
                        }
                        this.StageNeedsToBeRefreshed(this, new EventArgs());
                        ReloadItemChosen();
                    }
                }
                else
                {
                    DesactivateButtons();
                }
            }
        }

        /// <summary>
        /// Double click pour ouvrir l'event manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode selectedNode = StageObjectsTreeView.GetNodeAt(e.X, e.Y);
                if (selectedNode != null && selectedNode.Parent != null)
                {
                    VO_StageObject stageObject = GetStageObjectFromNode(selectedNode);
                    if (stageObject != null)
                    {
                        if(btnEvent.Enabled)
                            LoadEventManager();
                    }
                }
            }
        }

        /// <summary>
        /// Click sur Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (StageObjectsTreeView.SelectedNode != null)
            {
                DeleteSelectedItem();
            }
        }

        /// <summary>
        /// Click sur Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEvent_Click(object sender, EventArgs e)
        {
            LoadEventManager();
        }

        /// <summary>
        /// Survient quand la souris entre dans le contrôle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsPanel_MouseEnter(object sender, EventArgs e)
        {
            this.MouseEnterCustom(null, new EventArgs());
        }

        /// <summary>
        /// Lorsque le controle n'est plus le controle actif
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StageObjectsPanel_Leave(object sender, EventArgs e)
        {
            DesactivateButtons();
        }
        #endregion
    }
}
