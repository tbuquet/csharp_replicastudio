using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Drawing.Drawing2D;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer;
using System.Reflection;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire de la scène
    /// </summary>
    public partial class StagePanel : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        StageService _Service;

        /// <summary>
        /// Booléen qui enregistre si une touche clavier est relâchée
        /// </summary>
        bool _KeyUp = false;

        /// <summary>
        /// Booléen qui enregistre si un bouton de souris est enfoncée
        /// </summary>
        bool _MouseDown = false;

        /// <summary>
        /// Booléen qui enregistre si la souris est en mouvement
        /// </summary>
        bool _MouseMove = false;

        /// <summary>
        /// 
        /// </summary>
        private Point _PreviousScrollPosition = new Point();
        #endregion

        #region Events
        /// <summary>
        /// Survient lorsqu'un mode doit être changé
        /// </summary>
        public event EventHandler CurrentModeHasChanged;

        /// <summary>
        /// Survient lorsque la souris change de position
        /// </summary>
        public event EventHandler MouseChangePosition;

        /// <summary>
        /// Survient lorsqu'un item est choisi
        /// </summary>
        public event EventHandler ItemChosen;

        /// <summary>
        /// Survient quand la souris survole le contrôle
        /// </summary>
        public event EventHandler MouseEnterCustom;

        /// <summary>
        /// Survient quand un objet est crée ou supprimé
        /// </summary>
        public event EventHandler NeedToRefreshStageObjects;

        /// <summary>
        /// Survient quand un hotspot est terminé d'éditer
        /// </summary>
        public event EventHandler HotSpotEditionCompleted;

        /// <summary>
        /// Survient quand un hotspot entre en édition
        /// </summary>
        public event EventHandler HotSpotEditionBeginning;

        /// <summary>
        /// Survient quand le zoom doit changer
        /// </summary>
        public event EventHandler ZoomChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Curseur de dessin
        /// </summary>
        public Cursor DrawingCursor
        {
            get;
            set;
        }

        /// <summary>
        /// Curseur de dessin
        /// </summary>
        public Cursor AddVectorCursor
        {
            get;
            set;
        }

        /// <summary>
        /// Curseur de dessin
        /// </summary>
        public Cursor RemoveVectorCursor
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public StagePanel()
        {
            InitializeComponent();
            _Service = new StageService();
            HideEditEventsContextMenu();
            DesactivateCopyPastContextMenu();

            #region Creation des curseurs
            DrawingCursor = FormsTools.LoadCursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StagePanel.drawing_pen.cur"));
            AddVectorCursor = FormsTools.LoadCursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StagePanel.drawing_pen_add.cur"));
            RemoveVectorCursor = FormsTools.LoadCursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.StagePanel.drawing_pen_delete.cur"));
            #endregion

            this.LostFocus += new EventHandler(StagePanel_LostFocus);
            this.GotFocus += new EventHandler(StagePanel_GotFocus);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Déselectionne les objets
        /// </summary>
        public void DeselectStageObjects()
        {
            EditorHelper.Instance.SelectedObjects = new List<VO_StageObject>();
            MainSurface.Refresh();
        }

        /// <summary>
        /// Recharge les ressources de scène
        /// </summary>
        public void ResetStageResources()
        {
            _Service.ResetStageResources();
        }

        /// <summary>
        /// Charge un stage en fonction d'un fichier pStage dont le contenu est en mémoire.
        /// </summary>
        /// <param name="stage">Nom du fichier stage</param>
        public void LoadStage(Guid stage)
        {
            UnloadStage();

            //Chargement Scène et Calque
            VO_Stage voStage = GameCore.Instance.GetStageById(stage);
            foreach (VO_Layer layer in voStage.ListLayers)
            {
                if (layer.MainLayer)
                {
                    LoadStage(stage, layer.Id);
                    break;
                }
            }
        }

        /// <summary>
        /// Décharge le stage courant
        /// </summary>
        public void UnloadStage()
        {
            this.Visible = false;

            //Reset des ressources
            EditorHelper.Instance.SelectedObjects = new List<VO_StageObject>();
            _Service.ResetStageResources();
        }

        /// <summary>
        /// Charge un stage en fonction d'un fichier pStage dont le contenu est en mémoire.
        /// </summary>
        /// <param name="stage">Nom du fichier stage</param>
        /// <param name="layer">Id du calque à charger en même temps</param>
        public void LoadStage(Guid stage, Guid layer)
        {
            //Attente
            Cursor.Current = Cursors.WaitCursor;
            this.SuspendLayout();
            MainSurface.Paint -= new PaintEventHandler(MainSurface_Paint);

            //Chargement Scène et Calque
            VO_Stage voStage = GameCore.Instance.GetStageById(stage);
            EditorHelper.Instance.CurrentStage = stage;
            EditorHelper.Instance.CurrentLayer = layer;

            //Redimmenssionnements
            int stageWidth = (voStage.Dimensions.Width + (2 * EditorSettings.Instance.StagePadding));
            int stageHeight = (voStage.Dimensions.Height + (2 * EditorSettings.Instance.StagePadding));
            this.Width = stageWidth * EditorHelper.Instance.CurrentZoom;
            this.Height = stageHeight * EditorHelper.Instance.CurrentZoom;

            //Mise en mémoire du background.
            _Service.LoadEmptyBackground(new Size(stageWidth, stageHeight), voStage.Dimensions);

            //Fin de l'attente
            MainSurface.Paint += new PaintEventHandler(MainSurface_Paint);
            this.ResumeLayout();
            Cursor.Current = DefaultCursor;

            RefreshStage();

            _PreviousScrollPosition = new Point();
        }

        /// <summary>
        /// Rafraichi la scène
        /// </summary>
        public void RefreshStage()
        {
            //Attente
            Cursor.Current = Cursors.WaitCursor;
            this.SuspendLayout();
            MainSurface.Paint -= new PaintEventHandler(MainSurface_Paint);

            if (MainSurface.Image != null)
                MainSurface.Image.Dispose();
            MainSurface.Image = _Service.RefreshStage();

            //Fin de l'attente
            MainSurface.Paint += new PaintEventHandler(MainSurface_Paint);
            this.ResumeLayout();
            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Déplace un objet sur la scène
        /// </summary>
        /// <param name="pX">Position en X</param>
        /// <param name="pY">Position en Y</param>
        public void MoveObject(int x, int y)
        {
            if (EditorHelper.Instance.SelectedObjects.Count > 0)
            {
                //Itération sur tous les objets sélectionnés
                foreach (VO_StageObject vo in EditorHelper.Instance.SelectedObjects)
                {
                    Rectangle oldPosition = new Rectangle(vo.Location, vo.Size);
                    vo.Location = new Point(vo.Location.X + x, vo.Location.Y + y);
                    vo.Size = vo.Size;

                    //Cas typique des hotspots, on déplace également les points vectoriels.
                    if (vo.ObjectType == Enums.StageObjectType.HotSpots || vo.ObjectType == Enums.StageObjectType.Walkables || vo.ObjectType == Enums.StageObjectType.Regions)
                    {
                        int movX = vo.Location.X - oldPosition.X;
                        int movY = vo.Location.Y - oldPosition.Y;
                        VO_StageHotSpot hotspot = (VO_StageHotSpot)vo;
                        _Service.MovePoints(hotspot.Points, movX, movY);
                    }
                }
                _KeyUp = true;
                
                ReloadItemChosen();
                RefreshSelection();
            }
        }

        /// <summary>
        /// Supprime les items sélectionnés
        /// </summary>
        public void DeleteSelectedItem()
        {
            foreach (VO_StageObject vo in EditorHelper.Instance.SelectedObjects)
                vo.Delete();
            DeselectStageObjects();
            ReloadItemChosen();
            this.NeedToRefreshStageObjects(this, new EventArgs());
            this.RefreshStage();
        }

        /// <summary>
        /// Passe une mémoire une image de la scène antérieur et postérieur au calque et au mode courant.
        /// </summary>
        public void LoadMinusMaximusLayers()
        {
            _Service.LoadMinusMaximusLayers();
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
        /// Changer le curseur de la zone
        /// </summary>
        /// <param name="cursor">curseur</param>
        public void ChangeCursor(Cursor cursor)
        {
            this.Cursor = cursor;
        }

        /// <summary>
        /// Met à jour la location d'une zone hotspot
        /// </summary>
        /// <param name="objectStage"></param>
        public void UpdateAreaLocation(VO_StageHotSpot objectStage)
        {
            _Service.UpdateAreaLocation(objectStage);
        }
        #endregion

        #region EventHandlers
        #region ContextMenu
        /// <summary>
        /// Gestion de l'ouverture du menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //Cas de dessin
            if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
            {
                e.Cancel = true;
            }
        }

        #region Events Manager
        /// <summary>
        /// Click sur new character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_NewCharacter_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.EventManager.EventType = Enums.EventType.Character;
            FormsManager.Instance.EventManager.CurrentEvent = EditorHelper.Instance.SelectedObjects[0].Event;
            FormsManager.Instance.EventManager.CurrentStageObject = EditorHelper.Instance.SelectedObjects[0];
            GameCore.Instance.SaveEvent(FormsManager.Instance.EventManager.CurrentEvent);
            FormsManager.Instance.EventManager.FormClosing += new FormClosingEventHandler(EventManager_FormClosing);
            FormsManager.Instance.EventManager.ShowDialog(this);
        }

        /// <summary>
        /// Click sur new event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_NewEvent_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.SelectedObjects != null && EditorHelper.Instance.SelectedObjects.Count > 0)
            {
                FormsManager.Instance.EventManager.EventType = Enums.EventType.Event;
                FormsManager.Instance.EventManager.CurrentEvent = EditorHelper.Instance.SelectedObjects[0].Event;
                FormsManager.Instance.EventManager.CurrentStageObject = EditorHelper.Instance.SelectedObjects[0];
                GameCore.Instance.SaveEvent(FormsManager.Instance.EventManager.CurrentEvent);
                FormsManager.Instance.EventManager.FormClosing += new FormClosingEventHandler(EventManager_FormClosing);
                FormsManager.Instance.EventManager.ShowDialog(this);
            }
        }

        /// <summary>
        /// Click sur new animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_NewAnimation_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.EventManager.EventType = Enums.EventType.Animation;
            FormsManager.Instance.EventManager.CurrentEvent = EditorHelper.Instance.SelectedObjects[0].Event;
            FormsManager.Instance.EventManager.CurrentStageObject = EditorHelper.Instance.SelectedObjects[0];
            GameCore.Instance.SaveEvent(FormsManager.Instance.EventManager.CurrentEvent);
            FormsManager.Instance.EventManager.FormClosing += new FormClosingEventHandler(EventManager_FormClosing);
            FormsManager.Instance.EventManager.ShowDialog(this);
        }
        #endregion
        #endregion

        #region EventManager
        /// <summary>
        /// Lorsque l'event manager se ferme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EventManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormsManager.Instance.EventManager.FormClosing -= new FormClosingEventHandler(EventManager_FormClosing);
            EditorHelper.Instance.SelectedObjects[0].Event = FormsManager.Instance.EventManager.CurrentEvent;
        }
        #endregion

        #region DragDrop
        /// <summary>
        /// Rafraichi la sélection
        /// </summary>
        public void RefreshSelection()
        {
            MainSurface.Refresh();
        }

        /// <summary>
        /// Lorsqu'un item est déposée sur la scène
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StagePanel_DragDrop(object sender, DragEventArgs e)
        {
            Point relativePosition;
            relativePosition = _Service.GetDragStageCoords(this.PointToClient(new Point(e.X, e.Y)));

            switch (EditorHelper.Instance.DragDropObjectType)
            {
                case Enums.StageObjectType.Decors:
                    _Service.CreateDecor(relativePosition, EditorHelper.Instance.DragDropItemId);
                    if (EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Decors)
                    {
                        EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Decors;
                        this.CurrentModeHasChanged(this, new EventArgs());
                    }
                    break;
                case Enums.StageObjectType.Animations:
                    _Service.CreateAnimation(relativePosition, new Guid(EditorHelper.Instance.DragDropItemId));
                    if (EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Objects)
                    {
                        EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Objects;
                        this.CurrentModeHasChanged(this, new EventArgs());
                    }
                    break;
                case Enums.StageObjectType.Characters:
                    _Service.CreateCharacter(relativePosition, new Guid(EditorHelper.Instance.DragDropItemId));
                    if (EditorHelper.Instance.CurrentStageState != Enums.StagePanelState.Characters)
                    {
                        EditorHelper.Instance.CurrentStageState = Enums.StagePanelState.Characters;
                        this.CurrentModeHasChanged(this, new EventArgs());
                    }
                    break;
            }
            this.NeedToRefreshStageObjects(this, new EventArgs());
            RefreshStage();
        }

        /// <summary>
        /// Change l'icône de la souris pour afficher la possibilité du drag & drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StagePanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        #endregion

        #region Gestion des objets de la scène
        /// <summary>
        /// Click sur la scène
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainSurface_Click(object sender, EventArgs e)
        {
            this.Focus();
            if (!_MouseMove && EditorHelper.Instance.CurrentLayer != new Guid() && EditorHelper.Instance.CurrentDrawingTool == Enums.DrawingTools.Pointer)
            {
                Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    _Service.GetSelectedItem(mousePosition, true);
                    this.ItemChosen(null, new EventArgs());
                }
                else
                {
                    _Service.GetSelectedItem(mousePosition, false);
                    ReloadItemChosen();
                }
                LoadContextMenu();
                RefreshStage();
            }
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
                MoveObject(0, -1);
            //Gauche
            else if (keyData == (Keys.LButton | Keys.MButton | Keys.Space))
                MoveObject(-1, 0);
            //Droite
            else if (keyData == (Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space))
                MoveObject(1, 0);
            //Bas
            else if (keyData == (Keys.Back | Keys.Space))
                MoveObject(0, 1);
            //Suppression
            else if (keyData == (Keys.RButton | Keys.MButton | Keys.Back | Keys.Space))
                DeleteSelectedItem();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Transforme la scène pour les zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainSurface_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            switch (EditorHelper.Instance.CurrentZoom)
            {
                case 1:
                    e.Graphics.ScaleTransform(1F, 1F);
                    break;
                case 2:
                    e.Graphics.ScaleTransform(2F, 2F);
                    break;
                case 4:
                    e.Graphics.ScaleTransform(4F, 4F);
                    break;
                case 8:
                    e.Graphics.ScaleTransform(8F, 8F);
                    break;
            }
            e.Graphics.DrawImage(MainSurface.Image, 0, 0);

            //Mode dessin
            if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots)
            {
                _Service.RefreshVectorPoints(e.Graphics);
                if (EditorHelper.Instance.SelectedHotSpot != null && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer && !EditorHelper.Instance.HotSpotEditionMode)
                {
                    Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                    _Service.RefreshVectorMovingPoint(e.Graphics, mousePosition);
                }
            }

            foreach(VO_StageObject stageObj in EditorHelper.Instance.SelectedObjects)
                e.Graphics.DrawRectangle(EditorSettings.Instance.HighlightningColor, new Rectangle(new Point(stageObj.Location.X + EditorSettings.Instance.StagePadding, stageObj.Location.Y + EditorSettings.Instance.StagePadding), stageObj.Size));
        }

        /// <summary>
        /// Lorsque les touches claviers sont relachées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StagePanel_KeyUp(object sender, KeyEventArgs e)
        {
            if (_KeyUp)
            {
                RefreshStage();
                _KeyUp = false;
            }

            if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
            {
                //Mode édition
                if (EditorHelper.Instance.HotSpotEditionMode)
                {
                    ChangeCursor(DrawingCursor);
                }
            }
        }

        /// <summary>
        /// Lorsqu'un bouton souris est pressé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainSurface_MouseDown(object sender, MouseEventArgs e)
        {
            //Mode de dessin
            if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
            {
                Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                //Mode édition
                if (EditorHelper.Instance.SelectedHotSpot != null && EditorHelper.Instance.HotSpotEditionMode)
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if (!_Service.IsOnTheSelectedPolygon(mousePosition, true))
                            _Service.CreateNewPointToTheCurrentSelectedHotSpot(mousePosition, EditorHelper.Instance.SelectedHotSpot.Points.Length);
                        MainSurface.Refresh();
                    }
                    else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        if (_Service.RemovePointOfTheCurrentSelectedHotSpot(mousePosition))
                            MainSurface.Refresh();
                    }
                    else
                    {
                        EditorHelper.Instance.SelectedHotSpotPoint = _Service.GetVectorPoint(mousePosition);
                        _MouseDown = true;
                    }

                }
                //Mode création
                else if (EditorHelper.Instance.SelectedHotSpot == null)
                {
                    DeselectStageObjects();
                    switch (EditorHelper.Instance.CurrentStageState)
                    {
                        case Enums.StagePanelState.HotSpots:
                            EditorHelper.Instance.SelectedHotSpot = _Service.CreateHotSpot(mousePosition);
                            break;
                        case Enums.StagePanelState.WalkableAreas:
                            EditorHelper.Instance.SelectedHotSpot = _Service.CreateWalkableArea(mousePosition);
                            break;
                        case Enums.StagePanelState.Regions:
                            EditorHelper.Instance.SelectedHotSpot = _Service.CreateRegion(mousePosition);
                            break;
                    }
                    if (EditorHelper.Instance.SelectedHotSpot != null)
                    {
                        _MouseDown = true;
                        EditorHelper.Instance.SelectedHotSpotPoint = 0;
                        MainSurface.Refresh();
                    }
                }
                else
                {
                    _MouseDown = true;
                }
            }

            //Mode sélection d'objets
            if (EditorHelper.Instance.SelectedObjects.Count > 0)
            {
                Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                foreach (VO_StageObject vObject in EditorHelper.Instance.SelectedObjects)
                {
                    Rectangle rect = new Rectangle(vObject.Location, vObject.Size);
                    if (rect.IntersectsWith(new Rectangle(mousePosition, new Size(1, 1))))
                    {
                        _MouseDown = true;
                        _Service.StartObjectDrag(e.Location);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Lorsqu'un bouton souris est relâché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainSurface_MouseUp(object sender, MouseEventArgs e)
        {

            if (_MouseDown)
            {
                //Mode de dessin
                if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        //On "fixe" le vecteur courant
                        UpdateAreaLocation(EditorHelper.Instance.SelectedHotSpot);

                        EditorHelper.Instance.SelectedHotSpot = null;
                        EditorHelper.Instance.SelectedHotSpotPoint = -1;
                        if(EditorHelper.Instance.HotSpotEditionMode)
                            this.HotSpotEditionCompleted(this, new EventArgs());
                        EditorHelper.Instance.HotSpotEditionMode = false;
                        MainSurface.Refresh();
                        _MouseDown = false;
                    }
                    //Mode édition
                    if (EditorHelper.Instance.HotSpotEditionMode)
                        _MouseDown = false;
                    //Mode création
                    else if (EditorHelper.Instance.SelectedHotSpot != null)
                    {
                        Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                        _Service.CreateNewPointToTheCurrentSelectedHotSpot(mousePosition, EditorHelper.Instance.SelectedHotSpot.Points.Length);
                        MainSurface.Refresh();
                    }
                }
                else
                {
                    if (_MouseMove)
                        RefreshStage();
                    _MouseDown = false;
                    _MouseMove = false;
                    ReloadItemChosen();
                }
            }
        }

        /// <summary>
        /// Lorsque la souris se déplace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainSurface_MouseMove(object sender, MouseEventArgs e)
        {
            //Status Bar
            this.MouseChangePosition(_Service.GetDragStageCoords(new Point(e.X, e.Y)), new EventArgs());

            #region Dessins pour add ou suppression de vecteurs
            if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
            {
                //Mode édition
                if (EditorHelper.Instance.HotSpotEditionMode)
                {
                    Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if(_Service.IsOnTheSelectedPolygon(mousePosition,false))
                            ChangeCursor(AddVectorCursor);
                        else
                            ChangeCursor(DrawingCursor);
                    }
                    else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        if (_Service.GetVectorPoint(mousePosition) != -1)
                            ChangeCursor(RemoveVectorCursor);
                        else
                            ChangeCursor(DrawingCursor);
                    }
                    else
                    {
                        ChangeCursor(DrawingCursor);
                    }
                }
            }
            #endregion

            #region Drap & Drop
            if (_MouseDown)
            {
                //Modes dessin
                if (EditorHelper.Instance.CurrentStageState >= Enums.StagePanelState.HotSpots && EditorHelper.Instance.CurrentDrawingTool != Enums.DrawingTools.Pointer)
                {
                    //Mode édition
                    if (EditorHelper.Instance.HotSpotEditionMode && EditorHelper.Instance.SelectedHotSpotPoint > -1)
                    {
                        //Mode mouvement édition
                        Point mousePosition = _Service.GetDragStageCoords(this.PointToClient(Cursor.Position));
                        _Service.MoveVectorPoint(mousePosition, EditorHelper.Instance.SelectedHotSpotPoint);
                        MainSurface.Refresh();
                    }
                    else if (EditorHelper.Instance.SelectedHotSpot != null)
                        MainSurface.Refresh();
                }
                //Modes image
                else
                {
                    _Service.MoveObjectDragDrop(e.Location);
                    _MouseMove = true;
                    RefreshSelection();
                }
            }
            #endregion
        }

        /// <summary>
        /// Lors de l'activation de la roulette
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StagePanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (EditorSettings.Instance.ActivateZoomWithWheel && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
            {
                if (e.Delta >= 120 && EditorHelper.Instance.CurrentZoom < 8)
                {
                    EditorHelper.Instance.CurrentZoom *= 2;
                    this.ZoomChanged(this, new EventArgs());
                }
                else if (e.Delta <= -120 && EditorHelper.Instance.CurrentZoom > 1)
                {
                    EditorHelper.Instance.CurrentZoom /= 2;
                    this.ZoomChanged(this, new EventArgs());
                }
            }
        }
        #endregion

        #region Gestion du Context Menu
        public void LoadContextMenu()
        {
            List<VO_StageObject> selectedItems = EditorHelper.Instance.SelectedObjects;
            if (selectedItems.Count == 1)
            {
                HideEditEventsContextMenu();
                ActivateCopyPastContextMenu();
                ActivateMoveContextMenu();
                switch (selectedItems[0].ObjectType)
                {
                    case Enums.StageObjectType.Animations:
                        CContextMenu.Items["ContextMenu_Separator1"].Visible = true;
                        CContextMenu.Items["ContextMenu_NewAnimation"].Visible = true;
                        break;
                    case Enums.StageObjectType.Characters:
                        CContextMenu.Items["ContextMenu_Separator1"].Visible = true;
                        CContextMenu.Items["ContextMenu_NewCharacter"].Visible = true;
                        break;
                    case Enums.StageObjectType.HotSpots:
                        CContextMenu.Items["ContextMenu_Separator1"].Visible = true;
                        CContextMenu.Items["ContextMenu_NewHotspot"].Visible = true;
                        CContextMenu.Items["ContextMenu_EditForm"].Visible = true;
                        break;
                    case Enums.StageObjectType.Walkables:
                    case Enums.StageObjectType.Regions:
                        CContextMenu.Items["ContextMenu_Separator1"].Visible = true;
                        CContextMenu.Items["ContextMenu_EditForm"].Visible = true;
                        break;
                }
                CContextMenu.Enabled = true;
            }
            else if (selectedItems.Count != 0)
            {
                ActivateCopyPastContextMenu();
                ActivateMoveContextMenu();
                HideEditEventsContextMenu();
                CContextMenu.Enabled = true;
            }
            else
            {
                DesactivateMoveContextMenu();
                DesactivateCopyPastContextMenu();
                HideEditEventsContextMenu();
            }
        }

        /// <summary>
        /// Click sur MoveUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_MoveUp_Click(object sender, EventArgs e)
        {
            _Service.MoveObjectInPlan(Enums.Direction.Up, false);
            this.RefreshStage();
        }

        /// <summary>
        /// Click sur MoveDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_MoveDown_Click(object sender, EventArgs e)
        {
            _Service.MoveObjectInPlan(Enums.Direction.Down, false);
            this.RefreshStage();
        }

        /// <summary>
        /// Click sur MoveFirst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_MoveFirst_Click(object sender, EventArgs e)
        {
            _Service.MoveObjectInPlan(Enums.Direction.Up, true);
            this.RefreshStage();
        }

        /// <summary>
        /// Click sur MoveLast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_MoveLast_Click(object sender, EventArgs e)
        {
            _Service.MoveObjectInPlan(Enums.Direction.Down, true);
            this.RefreshStage();
        }

        /// <summary>
        /// Click sur editer la forme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_EditForm_Click(object sender, EventArgs e)
        {
            this.HotSpotEditionBeginning(this, new EventArgs());
        }

        /// <summary>
        /// Cacher les edit event
        /// </summary>
        public void HideEditEventsContextMenu()
        {
            CContextMenu.Items["ContextMenu_NewAnimation"].Visible = false;
            CContextMenu.Items["ContextMenu_NewCharacter"].Visible = false;
            CContextMenu.Items["ContextMenu_NewHotspot"].Visible = false;
            CContextMenu.Items["ContextMenu_EditForm"].Visible = false;
            CContextMenu.Items["ContextMenu_Separator1"].Visible = false;
        }

        /// <summary>
        /// Désactiver les options de copier/coller
        /// </summary>
        public void DesactivateCopyPastContextMenu()
        {
            CContextMenu.Items["ContextMenu_Cut"].Enabled = false;
            CContextMenu.Items["ContextMenu_Copy"].Enabled = false;
            CContextMenu.Items["ContextMenu_Paste"].Enabled = false;
            CContextMenu.Items["ContextMenu_Delete"].Enabled = false;
        }

        /// <summary>
        /// Activer les options de copier/coller
        /// </summary>
        public void ActivateCopyPastContextMenu()
        {
            CContextMenu.Items["ContextMenu_Cut"].Enabled = true;
            CContextMenu.Items["ContextMenu_Copy"].Enabled = true;
            CContextMenu.Items["ContextMenu_Paste"].Enabled = true;
            CContextMenu.Items["ContextMenu_Delete"].Enabled = true;
        }

        /// <summary>
        /// Désactiver les options de déplacement des éléments
        /// </summary>
        public void DesactivateMoveContextMenu()
        {
            CContextMenu.Items["ContextMenu_MoveUp"].Enabled = false;
            CContextMenu.Items["ContextMenu_MoveDown"].Enabled = false;
            CContextMenu.Items["ContextMenu_MoveFirst"].Enabled = false;
            CContextMenu.Items["ContextMenu_MoveLast"].Enabled = false;
        }

        /// <summary>
        /// Activer les options de déplacement des éléments
        /// </summary>
        public void ActivateMoveContextMenu()
        {
            CContextMenu.Items["ContextMenu_MoveUp"].Enabled = true;
            CContextMenu.Items["ContextMenu_MoveDown"].Enabled = true;
            CContextMenu.Items["ContextMenu_MoveFirst"].Enabled = true;
            CContextMenu.Items["ContextMenu_MoveLast"].Enabled = true;
        }
        #endregion

        #region Description
        /// <summary>
        /// Survient quand la souris entre dans le contrôle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StagePanel_MouseEnter(object sender, EventArgs e)
        {
            this.MouseEnterCustom(null, new EventArgs());
        }
        #endregion

        private void ContextMenu_Delete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItem();
        }

        private void StagePanel_LostFocus(object sender, EventArgs e)
        {
            _PreviousScrollPosition = ((Panel)this.Parent).AutoScrollPosition;
        }

        private void StagePanel_GotFocus(object sender, EventArgs e)
        {
            _PreviousScrollPosition.X *= -1;
            _PreviousScrollPosition.Y *= -1;
            ((Panel)this.Parent).AutoScrollPosition = _PreviousScrollPosition;
        }
        #endregion

        
    }
}
