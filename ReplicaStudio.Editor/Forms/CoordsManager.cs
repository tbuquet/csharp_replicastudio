using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using System.Drawing.Imaging;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms
{
    public partial class CoordsManager : Form
    {
        #region Members
        /// <summary>
        /// Timer de fréquence
        /// </summary>
        Timer _FrequencyTimer = null;

        /// <summary>
        /// Lock pour éviter le lancement d'un autre refresh si le précédent n'est pas terminé.
        /// </summary>
        bool _CallBackLock = false;

        /// <summary>
        /// Taille et position du sprite courant
        /// </summary>
        Rectangle _CurrentSprite = new Rectangle();

        /// <summary>
        /// Référence au service
        /// </summary>
        CoordsService _Service;

        /// <summary>
        /// Attributs pour transparence
        /// </summary>
        ImageAttributes _Opacity;

        /// <summary>
        /// Brush de remplissage
        /// </summary>
        Brush _TransparentFillBrush;

        /// <summary>
        /// Image de stage
        /// </summary>
        Image _StageImage;
        #endregion

        #region Properties
        /// <summary>
        /// Objet source
        /// </summary>
        public Rectangle SourceObject
        {
            get;
            set;
        }

        /// <summary>
        /// Source objet avec map
        /// </summary>
        public VO_Coords SourceFullObject
        {
            get;
            set;
        }

        /// <summary>
        /// Résolution source où placer l'objet.
        /// </summary>
        public Size SourceResolution
        {
            get;
            set;
        }

        /// <summary>
        /// Utiliser le background de la scène actuelle.
        /// </summary>
        public bool UseStages
        {
            get;
            set;
        }

        /// <summary>
        /// Use Stage Background
        /// </summary>
        public bool UseStageBackground
        {
            get;
            set;
        }

        /// <summary>
        /// Use Animations
        /// </summary>
        public bool UseAnimations
        {
            get;
            set;
        }

        /// <summary>
        /// Source avec anim
        /// </summary>
        public VO_Animation SourceAnim
        {
            get;
            set;
        }

        /// <summary>
        /// Objet d'export
        /// </summary>
        public VO_Coords DestinationObject
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public CoordsManager()
        {
            InitializeComponent();
            _Service = new CoordsService();
            _Opacity = new ImageAttributes();
            _TransparentFillBrush = FormsTools.GetMasksFillingColors()[0];

            //Timer
            _FrequencyTimer = new Timer();
            _FrequencyTimer.Tick += new EventHandler(CallBack_Frequency);
            _FrequencyTimer.Interval = 200;
        }
        #endregion

        #region Methods
        #region Animation
        /// <summary>
        /// Méthode qui rafraichi l'AnimPreview et fait apparaitre le rectangle de surbrillance sur la surface Resource.
        /// </summary>
        public void RefreshPreview()
        {
            //Vérifie si l'anim a besoin d'être reset
            if (_CurrentSprite.X >= ImageManager.GetImageResource(PathTools.GetProjectPath(this.SourceAnim.AnimationType) + this.SourceAnim.ResourcePath).Width)
            {
                _CurrentSprite.X = 0;
                _CurrentSprite.Y += this.SourceAnim.SpriteHeight;
                if (_CurrentSprite.Y >= ImageManager.GetImageResource(PathTools.GetProjectPath(this.SourceAnim.AnimationType) + this.SourceAnim.ResourcePath).Height)
                {
                    _CurrentSprite.Y = 0;
                }
                _CurrentSprite.Width = this.SourceAnim.SpriteWidth;
                _CurrentSprite.Height = this.SourceAnim.SpriteHeight;
            }

            Preview.Refresh();
        }
        #endregion

        /// <summary>
        /// Charge les couleurs d'une matrice d'un calque
        /// </summary>
        /// <param name="pLayer">Calque</param>
        private void LoadNewMatrix(VO_Layer layer)
        {
            ColorMatrix opacity = new ColorMatrix();
            opacity.Matrix33 = layer.ColorTransformations.Opacity / 255.0f;
            _Opacity.SetColorMatrix(opacity, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        /// <summary>
        /// Charge les décors de la map
        /// </summary>
        private Image LoadMapDecors(Guid map)
        {
            VO_Stage stage = GameCore.Instance.GetStageById(map);
            Image output = new Bitmap(stage.Dimensions.Width, stage.Dimensions.Height);
            Graphics graphics = Graphics.FromImage(output);
            graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(), stage.Dimensions));

            foreach (VO_Layer layer in stage.ListLayers)
            {
                LoadNewMatrix(layer);
                foreach (VO_StageDecor decor in layer.ListDecors)
                {
                    Image decorImage = ImageManager.GetImageStageDecor(GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + decor.Filename);
                    graphics.DrawImage(decorImage, new Rectangle(decor.Location, decor.Size), 0, 0, decorImage.Width, decorImage.Height, GraphicsUnit.Pixel, _Opacity);
                } 
            }

            foreach (VO_Layer layer in stage.ListLayers)
            {
                foreach (VO_StageHotSpot hotSpot in layer.ListWalkableAreas)
                {
                    if (hotSpot.Points.Length > 1)
                        graphics.FillPolygon(_TransparentFillBrush, (Point[])hotSpot.Points);
                }
            }
            

            return output;
        }

        /// <summary>
        /// Charger les stages
        /// </summary>
        private void LoadStages()
        {
            ddpMap.DataSource = GameCore.Instance.GetStages();
            ddpMap.DisplayMember = "Title";
            ddpMap.ValueMember = "Id";
            if (SourceFullObject.Map != new Guid())
                ddpMap.SelectedValue = SourceFullObject.Map;
            else if (ddpMap.Items.Count > 0)
            {
                ddpMap.SelectedIndex = 0;
                SourceFullObject.Map = (Guid)ddpMap.SelectedValue;
                DestinationObject.Map = (Guid)ddpMap.SelectedValue;
            }
        }

        /// <summary>
        /// Démarre l'animation
        /// </summary>
        public void Start()
        {
            _FrequencyTimer.Start();
        }

        /// <summary>
        /// Stoppe l'animation
        /// </summary>
        public void Stop()
        {
            _FrequencyTimer.Stop();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Fonction qui est exécutée à la fin du Timer
        /// </summary>
        /// <param name="e"></param>
        private void CallBack_Frequency(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (this.SourceAnim != null)
                {
                    _CurrentSprite.X += this.SourceAnim.SpriteWidth;
                    if (!_CallBackLock)
                    {
                        _CallBackLock = true;
                        RefreshPreview();
                        _CallBackLock = false;
                    }
                }
            }
        }

        /// <summary>
        /// Survient à l'ouverture de la fenetre
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Maximiser l'écran
            this.WindowState = FormWindowState.Maximized;
            this.Stop();

            //Si UseStages, UseStageBackground doit être activé.
            if (UseStages)
                UseStageBackground = true;

            //Désactive les eventhandlers
            ddpX.ValueChanged -= new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged -= new EventHandler(ddpY_ValueChanged);
            if (UseStages)
                ddpMap.SelectedIndexChanged -= new EventHandler(ddpMap_SelectedIndexChanged);

            //Adapter la preview à la taille de la resolution source
            DestinationObject = new VO_Coords();
            Guid ensuredMapGuid = Guid.Empty;
            if (UseStages)
            {
                LoadStages();
                DestinationObject = new VO_Coords(SourceFullObject.Location, SourceFullObject.Map);
                VO_Stage stage = GameCore.Instance.Game.Stages.Find(p => p.Id == SourceFullObject.Map);
                if(stage != null)
                    ensuredMapGuid = stage.Id;
                if (ensuredMapGuid != Guid.Empty)
                    SourceResolution = GameCore.Instance.GetStageById(SourceFullObject.Map).Dimensions;
                else
                    SourceResolution = new Size(GameCore.Instance.Game.Project.Resolution.Width, GameCore.Instance.Game.Project.Resolution.Height);
                SourceObject = new Rectangle(SourceFullObject.Location, new Size(SourceObject.Width, SourceObject.Height));
            }
            else if (UseStageBackground)
            {
                SourceResolution = EditorHelper.Instance.GetCurrentStageInstance().Dimensions;
            }
            this.Preview.Width = SourceResolution.Width;
            this.Preview.Height = SourceResolution.Height;

            //Créer le décor
            if (_StageImage != null)
                _StageImage.Dispose();
            if (ensuredMapGuid != Guid.Empty)
            {
                _StageImage = LoadMapDecors(ensuredMapGuid);
                Preview.Image = _StageImage;
            }
            else if (UseStageBackground && EditorHelper.Instance.CurrentStage != Guid.Empty)
            {
                _StageImage = LoadMapDecors(EditorHelper.Instance.CurrentStage);
                Preview.Image = _StageImage;
            }
            else
            {
                Preview.Image = _Service.LoadBackground(new Size(this.Preview.Width, this.Preview.Height), false);
            }

            //Si Anim, on lance un timer
            if (UseAnimations)
            {
                _CurrentSprite.Width = this.SourceAnim.SpriteWidth;
                _CurrentSprite.Height = this.SourceAnim.SpriteHeight;
                _CurrentSprite.X = 0;
                _CurrentSprite.Y = 0;
                this.Start();
            }

            //Gère map ou non
            if (UseStages)
            {
                grpMaps.Visible = true;
                btnCancel.Location = new Point(97, 270);
                btnOK.Location = new Point(13, 270);
            }
            else
            {
                grpMaps.Visible = false;
                btnCancel.Location = new Point(97, 188);
                btnOK.Location = new Point(13, 188);
            }

            //Destination = source
            if (SourceObject.X > SourceResolution.Width - 1 || SourceObject.Y > SourceResolution.Height - 1)
                SourceObject = new Rectangle(0, 0, SourceObject.Width, SourceObject.Height);
            DestinationObject.Location = new Point(SourceObject.X, SourceObject.Y);

            //Binding des infos
            ddpX.Minimum = 0;
            ddpY.Minimum = 0;
            ddpX.Maximum = SourceResolution.Width - 1;
            ddpY.Maximum = SourceResolution.Height - 1;
            ddpX.Value = DestinationObject.Location.X;
            ddpY.Value = DestinationObject.Location.Y;

            //Réactive les eventhandlers
            ddpX.ValueChanged += new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged += new EventHandler(ddpY_ValueChanged);
            if(UseStages)
                ddpMap.SelectedIndexChanged += new EventHandler(ddpMap_SelectedIndexChanged);
        }

        /// <summary>
        /// La map source change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            DestinationObject.Map = (Guid)ddpMap.SelectedValue;

            Guid ensuredMapGuid = Guid.Empty;
            if (UseStages)
            {
                VO_Stage stage = GameCore.Instance.Game.Stages.Find(p => p.Id == DestinationObject.Map);
                if (stage != null)
                    ensuredMapGuid = stage.Id;
                if (ensuredMapGuid != Guid.Empty)
                    SourceResolution = GameCore.Instance.GetStageById(DestinationObject.Map).Dimensions;
                else
                    SourceResolution = new Size(GameCore.Instance.Game.Project.Resolution.Width, GameCore.Instance.Game.Project.Resolution.Height);
            }

            this.Preview.Width = SourceResolution.Width;
            this.Preview.Height = SourceResolution.Height;

            //Créer le décor
            if (_StageImage != null)
                _StageImage.Dispose();
            if (ensuredMapGuid == Guid.Empty)
                Preview.Image = _Service.LoadBackground(new Size(this.Preview.Width, this.Preview.Height), false);
            else
            {
                _StageImage = LoadMapDecors(ensuredMapGuid);
                Preview.Image = _StageImage;
            }

            //Désactive les eventhandlers
            ddpX.ValueChanged -= new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged -= new EventHandler(ddpY_ValueChanged);

            //Binding des infos
            ddpX.Maximum = SourceResolution.Width - 1;
            ddpY.Maximum = SourceResolution.Height - 1;
            DestinationObject.Location = new Point(0, 0);
            ddpX.Value = 0;
            ddpY.Value = 0;

            //Réactive les eventhandlers
            ddpX.ValueChanged += new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged += new EventHandler(ddpY_ValueChanged);
        }

        /// <summary>
        /// Dessin du point ou du rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Preview_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphic = e.Graphics;

            if (UseAnimations)
            {
                //Preview
                graphic.DrawImage(ImageManager.GetImageResource(PathTools.GetProjectPath(this.SourceAnim.AnimationType) + this.SourceAnim.ResourcePath), new Rectangle(new Point(0, 0), new Size(_CurrentSprite.Width, _CurrentSprite.Height)), new Rectangle(new Point(_CurrentSprite.X, _CurrentSprite.Y), new Size(_CurrentSprite.Width, _CurrentSprite.Height)), GraphicsUnit.Pixel);
            }

            //Cas objet = Point
            if (SourceObject.Width <= 1 && SourceObject.Height <= 1)
            {
                graphic.DrawLine(EditorSettings.Instance.SelectionCoords, new Point(DestinationObject.Location.X, DestinationObject.Location.Y - 5), new Point(DestinationObject.Location.X, DestinationObject.Location.Y + 5));
                graphic.DrawLine(EditorSettings.Instance.SelectionCoords, new Point(DestinationObject.Location.X - 5, DestinationObject.Location.Y), new Point(DestinationObject.Location.X + 5, DestinationObject.Location.Y));
            }
            //Cas objet = Rectangle
            else
            {
                graphic.DrawLine(EditorSettings.Instance.SelectionCoords, new Point(DestinationObject.Location.X, DestinationObject.Location.Y), new Point(DestinationObject.Location.X, DestinationObject.Location.Y + SourceObject.Height));
                graphic.DrawLine(EditorSettings.Instance.SelectionCoords, new Point(DestinationObject.Location.X, DestinationObject.Location.Y), new Point(DestinationObject.Location.X + SourceObject.Width, DestinationObject.Location.Y));
                graphic.DrawLine(EditorSettings.Instance.SelectionCoords, new Point(DestinationObject.Location.X + SourceObject.Width, DestinationObject.Location.Y), new Point(DestinationObject.Location.X + SourceObject.Width, DestinationObject.Location.Y + SourceObject.Height));
                graphic.DrawLine(EditorSettings.Instance.SelectionCoords, new Point(DestinationObject.Location.X, DestinationObject.Location.Y + SourceObject.Height), new Point(DestinationObject.Location.X + SourceObject.Width, DestinationObject.Location.Y + SourceObject.Height));
            }
        }

        /// <summary>
        /// Récupérer la position des coordonnées cliquées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Preview_MouseClick(object sender, MouseEventArgs e)
        {
            DestinationObject.Location = new Point(e.X, e.Y);
            Preview.Refresh();

            //Désactive les eventhandlers
            ddpX.ValueChanged -= new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged -= new EventHandler(ddpY_ValueChanged);

            ddpX.Value = DestinationObject.Location.X;
            ddpY.Value = DestinationObject.Location.Y;

            //Réactive les eventhandlers
            ddpX.ValueChanged += new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged += new EventHandler(ddpY_ValueChanged);
        }

        /// <summary>
        /// Valeur Y changée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpY_ValueChanged(object sender, EventArgs e)
        {
            DestinationObject.Location = new Point(DestinationObject.Location.X, Convert.ToInt32(ddpY.Value));
            Preview.Refresh();
        }

        /// <summary>
        /// Valeur X changée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpX_ValueChanged(object sender, EventArgs e)
        {
            DestinationObject.Location = new Point(Convert.ToInt32(ddpX.Value), DestinationObject.Location.Y);
            Preview.Refresh();
        }

        /// <summary>
        /// Click sur Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UseStageBackground = false;
            UseStages = false;
            DestinationObject.Location = new Point(SourceObject.X, SourceObject.Y);
            this.Close();
        }

        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            UseStageBackground = false;
            UseStages = false;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Click sur Middle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMiddle_Click(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            ddpX.ValueChanged -= new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged -= new EventHandler(ddpY_ValueChanged);

            //Cas objet = Point
            if (SourceObject.Width <= 1 && SourceObject.Height <= 1)
            {
                DestinationObject.Location = new Point(SourceResolution.Width / 2,SourceResolution.Height / 2);
            }
            //Cas objet = Rectangle
            else
            {
                DestinationObject.Location = new Point(SourceResolution.Width / 2 - SourceObject.Width / 2, SourceResolution.Height / 2 - SourceObject.Height / 2);
            }

            ddpX.Value = DestinationObject.Location.X;
            ddpY.Value = DestinationObject.Location.Y;

            Preview.Refresh();

            //Réactive les eventhandlers
            ddpX.ValueChanged += new EventHandler(ddpX_ValueChanged);
            ddpY.ValueChanged += new EventHandler(ddpY_ValueChanged);
        }

        /// <summary>
        /// Click sur Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            ddpY.ValueChanged -= new EventHandler(ddpY_ValueChanged);

            DestinationObject.Location = new Point(DestinationObject.Location.X, 0);
            ddpY.Value = DestinationObject.Location.Y;

            Preview.Refresh();

            //Réactive les eventhandlers
            ddpY.ValueChanged += new EventHandler(ddpY_ValueChanged);
        }

        /// <summary>
        /// Click sur Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            ddpY.ValueChanged -= new EventHandler(ddpY_ValueChanged);

            //Cas objet = Point
            if (SourceObject.Width <= 1 && SourceObject.Height <= 1)
            {
                DestinationObject.Location = new Point(DestinationObject.Location.X, SourceResolution.Height - 1);
            }
            //Cas objet = Rectangle
            else
            {
                int newHeight = SourceResolution.Height - 1 - SourceObject.Height;
                if (newHeight < 0)
                    newHeight = 0;
                DestinationObject.Location = new Point(DestinationObject.Location.X, newHeight);
            }

            ddpY.Value = DestinationObject.Location.Y;

            Preview.Refresh();

            //Réactive les eventhandlers
            ddpY.ValueChanged += new EventHandler(ddpY_ValueChanged);
        }

        /// <summary>
        /// Click sur Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            ddpX.ValueChanged -= new EventHandler(ddpX_ValueChanged);

            DestinationObject.Location = new Point(0, DestinationObject.Location.Y);
            ddpX.Value = DestinationObject.Location.X;

            Preview.Refresh();

            //Réactive les eventhandlers
            ddpX.ValueChanged += new EventHandler(ddpX_ValueChanged);
        }

        /// <summary>
        /// Click sur Right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            ddpX.ValueChanged -= new EventHandler(ddpX_ValueChanged);

            //Cas objet = Point
            if (SourceObject.Width <= 1 && SourceObject.Height <= 1)
            {
                DestinationObject.Location = new Point(SourceResolution.Width - 1, DestinationObject.Location.Y);
            }
            //Cas objet = Rectangle
            else
            {
                int newWidth = SourceResolution.Width - 1 - SourceObject.Width;
                if (newWidth < 0)
                    newWidth = 0;
                DestinationObject.Location = new Point(newWidth, DestinationObject.Location.Y);
            }

            ddpX.Value = DestinationObject.Location.X;

            Preview.Refresh();

            //Réactive les eventhandlers
            ddpX.ValueChanged += new EventHandler(ddpX_ValueChanged);
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
