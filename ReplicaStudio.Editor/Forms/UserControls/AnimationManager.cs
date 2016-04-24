using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.ServiceLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire de gestion d'animations, également partie intégrante du formulaire de Database
    /// </summary>
    public partial class AnimationManager : UserControl
    {
        #region Members
        /// <summary>
        /// Lien vers la couche service
        /// </summary>
        AnimationService _Service;

        /// <summary>
        /// Size de la ressource originale
        /// </summary>
        Size _OriginalResourceSize;

        /// <summary>
        /// Timer de fréquence
        /// </summary>
        Timer _FrequencyTimer = null;

        /// <summary>
        /// Taille et position du sprite courant
        /// </summary>
        Rectangle _CurrentSprite = new Rectangle();

        /// <summary>
        /// Lock pour éviter le lancement d'un autre refresh si le précédent n'est pas terminé.
        /// </summary>
        bool _CallBackLock = false;

        /// <summary>
        /// Si false, l'affichage de la resource est redimensionnée pour faire apparaître toute la ressource. Si true, la ressource apparait en taille normale.
        /// </summary>
        bool _ResourceFull = false;

        /// <summary>
        /// Valeur permettant de comparer à la nouvelle valeur lors d'un changement de width de sprite.
        /// </summary>
        int _CurrentWidth = 0;

        /// <summary>
        /// Valeur permettant de comparer à la nouvelle valeur lors d'un changement de height de sprite.
        /// </summary>
        int _CurrentHeight = 0;

        /// <summary>
        /// Nombre de lignes maximales
        /// </summary>
        int _NbrRows = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Type d'animation en entrée, permet de récupérer différents types d'animations.
        /// </summary>
        public Enums.AnimationType AnimationType
        {
            get;
            set;
        }

        /// <summary>
        /// Parent Character
        /// </summary>
        public Guid ParentCharacter { get; set; }

        /// <summary>
        /// Animation courante
        /// </summary>
        public VO_Animation CurrentAnimation { get; set; }

        /// <summary>
        /// Le control charge la configuration du point d'origine.
        /// </summary>
        public bool OriginPoint { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="pAnimationType">Type d'animation</param>
        public AnimationManager(Enums.AnimationType pAnimationType)
        {
            //Configuration par défaut
            InitializeComponent();

            //Initialisation de la couche Service
            _Service = new AnimationService();

            //AnimationType
            AnimationType = pAnimationType;

            //Frequency
            ddpFrequency.Minimum = GlobalConstants.ANIMATION_MIN_FREQUENCY;
            ddpFrequency.Maximum = GlobalConstants.ANIMATION_MAX_FREQUENCY;
            ddpFrequency.Value = GlobalConstants.ANIMATION_NORMAL_FREQUENCY;

            //Timer
            _FrequencyTimer = new Timer();
            _FrequencyTimer.Tick += new EventHandler(CallBack_Frequency);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Méthode qui charge l'animation courante
        /// </summary>
        public void LoadAnimation(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Suppression des eventhandler
            this.ddpSpriteW.ValueChanged -= new System.EventHandler(this.ddpSpriteW_ValueChanged);
            this.ddpSpriteH.ValueChanged -= new System.EventHandler(this.ddpSpriteH_ValueChanged);
            this.ddpFrequency.ValueChanged -= new System.EventHandler(this.ddpFrequency_ValueChanged);
            this.ddpRows.ValueChanged -= new System.EventHandler(this.ddpRows_ValueChanged);
            this.txtName.TextChanged -= new System.EventHandler(this.txtName_TextChanged);

            //Stop Timer
            _FrequencyTimer.Stop();

            //Anim courante
            if (AnimationType == Enums.AnimationType.CharacterAnimation)
                CurrentAnimation = _Service.LoadVOObject(ParentCharacter, guid);
            else
                CurrentAnimation = _Service.LoadVOObject(AnimationType, guid);

            if (CurrentAnimation.ResourcePath != null)
            {
                //Mise en place du sprite root.
                _CurrentSprite.X = 0;
                _CurrentSprite.Width = CurrentAnimation.SpriteWidth;
                _CurrentSprite.Height = CurrentAnimation.SpriteHeight;
                _CurrentSprite.Y = CurrentAnimation.Row * CurrentAnimation.SpriteHeight;
                if (!string.IsNullOrEmpty(CurrentAnimation.ResourcePath))
                {
                    _Service.LoadSurfaceFromURI(PathTools.GetProjectPath(AnimationType) + CurrentAnimation.ResourcePath);
                }
                else
                {
                    _Service.LoadSurfaceFromURI(string.Empty);
                }

                //Timer
                _FrequencyTimer.Interval = 10000 / CurrentAnimation.Frequency;
                _FrequencyTimer.Start();
            }
            else
            {
                _Service.LoadEmptySurface();
                _CurrentSprite.X = 0;
                CurrentAnimation.SpriteWidth = 1;
                CurrentAnimation.SpriteHeight = 1;
                _CurrentSprite.Width = CurrentAnimation.SpriteWidth;
                _CurrentSprite.Height = CurrentAnimation.SpriteHeight;
            }

            //Récupération de la taille de la ressource originale
            _OriginalResourceSize = _Service.GetSizeOfResource();

            //Mise à niveau de la taille des sprites
            if (CurrentAnimation.SpriteWidth > _OriginalResourceSize.Width)
                CurrentAnimation.SpriteWidth = _OriginalResourceSize.Width;
            if (CurrentAnimation.SpriteHeight > _OriginalResourceSize.Height)
                CurrentAnimation.SpriteHeight = _OriginalResourceSize.Height;

            //Mise en place des contrôles
            txtName.Text = CurrentAnimation.Title;
            
            _NbrRows = GetNbrRowsFromResource();
            if (_NbrRows > 0)
                ddpRows.Maximum = _NbrRows - 1;
            if (CurrentAnimation.Row > ddpRows.Maximum)
                CurrentAnimation.Row = ConvertTools.CastInt(ddpRows.Maximum);
            ddpRows.Value = CurrentAnimation.Row;
            ddpFrequency.Value = CurrentAnimation.Frequency;
            _CurrentWidth = CurrentAnimation.SpriteWidth;
            _CurrentHeight = CurrentAnimation.SpriteHeight;
            ddpSpriteW.Maximum = _OriginalResourceSize.Width;
            ddpSpriteH.Maximum = _OriginalResourceSize.Height;
            if (CurrentAnimation.SpriteWidth > ddpSpriteW.Maximum)
                ddpSpriteW.Value = ddpSpriteW.Maximum;
            else
                ddpSpriteW.Value = CurrentAnimation.SpriteWidth;
            if (CurrentAnimation.SpriteHeight > ddpSpriteH.Maximum)
                ddpSpriteH.Value = ddpSpriteH.Maximum;
            else
                ddpSpriteH.Value = CurrentAnimation.SpriteHeight;

            if (CurrentAnimation.ParentCharacter != new Guid())
            {
                ddpRows.Enabled = false;
                ddpSpriteH.Enabled = false;
                lblRows.Enabled = false;
                lblX.Enabled = false;
            }
            else
            {
                ddpRows.Enabled = true;
                ddpSpriteH.Enabled = true;
                lblRows.Enabled = true;
                lblX.Enabled = true;
            }

            this.ddpSpriteW.ValueChanged += new System.EventHandler(this.ddpSpriteW_ValueChanged);
            this.ddpSpriteH.ValueChanged += new System.EventHandler(this.ddpSpriteH_ValueChanged);
            this.ddpFrequency.ValueChanged += new System.EventHandler(this.ddpFrequency_ValueChanged);
            this.ddpRows.ValueChanged += new System.EventHandler(this.ddpRows_ValueChanged);
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);

            grpInformations.Visible = true;
            grpPreview.Visible = true;
            grpResource.Visible = true;

            RefreshPreview();

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Méthode qui rafraichi l'AnimPreview et fait apparaitre le rectangle de surbrillance sur la surface Resource.
        /// </summary>
        public void RefreshPreview()
        {
            //Vérifie si l'anim a besoin d'être reset
            if (_CurrentSprite.X >= _OriginalResourceSize.Width)
                ResetPreview(false);

            //Preview
            if (AnimPreview.Image != null)
                AnimPreview.Image.Dispose();
            AnimPreview.Image = _Service.RefreshAnimation(AnimPreview.DisplayRectangle, new Rectangle(new Point(_CurrentSprite.X, _CurrentSprite.Y), _CurrentSprite.Size));

            //Resource
            if (Resource.Image != null)
                Resource.Image.Dispose();
            Resource.Image = _Service.RefreshRessource(Resource.DisplayRectangle, new Rectangle(new Point(_CurrentSprite.X, _CurrentSprite.Y), _CurrentSprite.Size), _ResourceFull);
        }

        /// <summary>
        /// Replace l'animation à la première colonne.
        /// </summary>
        /// <param name="pIncludeRow">Replacer également à la première ligne</param>
        private void ResetPreview(bool includeRow)
        {
            _CurrentSprite.X = 0;
            if (includeRow)
                _CurrentSprite.Y = 0;
            _CurrentSprite.Width = CurrentAnimation.SpriteWidth;
            _CurrentSprite.Height = CurrentAnimation.SpriteHeight;
        }

        /// <summary>
        /// Charge la liste en fonction du type d'animation choisie
        /// </summary>
        private void ProvisionList()
        {
            if (AnimationType == Enums.AnimationType.CharacterAnimation)
                ListAnimations.DataSource = _Service.ProvisionList(ParentCharacter);
            else
                ListAnimations.DataSource = _Service.ProvisionList(AnimationType);
            if(CurrentAnimation != null && CurrentAnimation.Id != new Guid())
                ListAnimations.LoadList(CurrentAnimation.Id);
            else
                ListAnimations.LoadList();
        }

        /// <summary>
        /// Retourne un nombre de lignes utilisables dans une ressource suivant la taille du sprite sélectionné.
        /// </summary>
        /// <returns>Nombre de lignes d'une ressource</returns>
        public int GetNbrRowsFromResource()
        {
            return _OriginalResourceSize.Height / CurrentAnimation.SpriteHeight;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Fonction qui est exécutée à la fin du Timer
        /// </summary>
        /// <param name="e"></param>
        private void CallBack_Frequency(object sender, EventArgs e)
        {
            if (this.Visible && CurrentAnimation != null)
            {
                _CurrentSprite.X += CurrentAnimation.SpriteWidth;
                if (!_CallBackLock)
                {
                    _CallBackLock = true;
                    RefreshPreview();
                    _CallBackLock = false;
                }
            }
        }

        /// <summary>
        /// Déplace le curseur d'animation sur une autre ligne.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpRows_ValueChanged(object sender, EventArgs e)
        {
            _CurrentSprite.Y = Convert.ToInt32(ddpRows.Value) * _CurrentSprite.Height;
            CurrentAnimation.Row = ConvertTools.CastInt(ddpRows.Value);
            ResetPreview(false);
        }

        /// <summary>
        /// Change la fréquence de l'animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpFrequency_ValueChanged(object sender, EventArgs e)
        {
            CurrentAnimation.Frequency = Convert.ToInt32(ddpFrequency.Value);
            ResetPreview(false);
            _FrequencyTimer.Interval = 10000 / CurrentAnimation.Frequency;
        }

        /// <summary>
        /// Modifie la largeur du sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpSpriteW_ValueChanged(object sender, EventArgs e)
        {
            if (_CurrentWidth != ddpSpriteW.Value && _CurrentWidth < ddpSpriteW.Value)
            {
                if (_OriginalResourceSize.Width % ddpSpriteW.Value != 0)
                {
                    ddpSpriteW.Value++;
                    return;
                }
            }
            else if (_CurrentWidth != ddpSpriteW.Value && _CurrentWidth > ddpSpriteW.Value)
            {
                if (_OriginalResourceSize.Width % ddpSpriteW.Value != 0)
                {
                    ddpSpriteW.Value--;
                    return;
                }
            }
            CurrentAnimation.SpriteWidth = Convert.ToInt32(ddpSpriteW.Value);
            _CurrentWidth = CurrentAnimation.SpriteWidth;
            ResetPreview(false);
        }

        /// <summary>
        /// Modifie la hauteur du sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpSpriteH_ValueChanged(object sender, EventArgs e)
        {
            if (_CurrentHeight != ddpSpriteH.Value && _CurrentHeight < ddpSpriteH.Value)
            {
                if (_OriginalResourceSize.Height % ddpSpriteH.Value != 0)
                {
                    ddpSpriteH.Value++;
                    return;
                }
            }
            else if (_CurrentHeight != ddpSpriteH.Value && _CurrentHeight > ddpSpriteH.Value)
            {
                if (_OriginalResourceSize.Height % ddpSpriteH.Value != 0)
                {
                    ddpSpriteH.Value--;
                    return;
                }
            }
            CurrentAnimation.SpriteHeight = Convert.ToInt32(ddpSpriteH.Value);
            _CurrentHeight = CurrentAnimation.SpriteHeight;
            ResetPreview(true);
            _NbrRows = GetNbrRowsFromResource();
            ddpRows.Value = 0;
            ddpRows.Maximum = _NbrRows - 1;
        }

        /// <summary>
        /// Déplacement de la ressource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resource_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (_ResourceFull)
                    _ResourceFull = false;
                else
                    _ResourceFull = true;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                switch (AnimationType)
                {
                    case Enums.AnimationType.CharacterAnimation:
                        FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS;
                        break;
                    case Enums.AnimationType.CharacterFace:
                        FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_CHARACTERFACES;
                        break;
                    case Enums.AnimationType.IconAnimation:
                        FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_ICONS;
                        break;
                    case Enums.AnimationType.Menu:
                        FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_MENUS;
                        break;
                    case Enums.AnimationType.ObjectAnimation:
                        FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS;
                        break;
                }
                FormsManager.Instance.ResourcesManager.SelectedFilePath = CurrentAnimation.ResourcePath;
                FormsManager.Instance.ResourcesManager.FormClosing += new FormClosingEventHandler(ResourcesManager_FormClosing);
                FormsManager.Instance.ResourcesManager.ShowDialog();
            }
        }

        /// <summary>
        /// Lors de la fermeture du ressource manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ResourcesManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormsManager.Instance.ResourcesManager.FormClosing -= new FormClosingEventHandler(ResourcesManager_FormClosing);
            CurrentAnimation.ResourcePath = FormsManager.Instance.ResourcesManager.SelectedFilePath;
            LoadAnimation(CurrentAnimation.Id);

            if (AnimationType == Enums.AnimationType.CharacterAnimation)
            {
                int newValue = _OriginalResourceSize.Height / GameCore.Instance.Game.Project.MovementDirections;
                if (newValue <= 0)
                    newValue = 1;
                ddpSpriteH.Value = newValue;
            }
            _CurrentSprite.Y = ConvertTools.CastInt(ddpRows.Value) * _CurrentSprite.Height;
        }

        /// <summary>
        /// Charge une nouvelle animation en fonction de l'item choisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAnimations_ItemChosen(object sender, EventArgs e)
        {
            //CurrentRow = 0;
            LoadAnimation(ListAnimations.ItemSelectedValue);
        }

        /// <summary>
        /// Charge les bonnes données d'animation au moment où le formulaire devient visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (OriginPoint)
                    this.btnOriginPoint.Visible = true;
                else
                    this.btnOriginPoint.Visible = false;
                ProvisionList();
                if (ListAnimations.DataSource.Count <= 0)
                    ListAnimations_ListIsEmpty(this, new EventArgs());
                _CallBackLock = false;
            }
        }

        /// <summary>
        /// Gère les actions de l'animation manager lorsqu'un item est détruit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAnimations_ItemToDelete(object sender, EventArgs e)
        {
            _FrequencyTimer.Stop();
            CurrentAnimation.Delete();
            CurrentAnimation = null;
        }

        /// <summary>
        /// Masque tous les contrôles lorsqu'il n'y a pas d'item dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAnimations_ListIsEmpty(object sender, EventArgs e)
        {
            grpInformations.Visible = false;
            grpPreview.Visible = false;
            grpResource.Visible = false;
        }

        /// <summary>
        /// Code ajouté lors de la création d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListAnimations_ItemToCreate(object sender, EventArgs e)
        {
            VO_Animation newAnim = null;

            if (AnimationType == Enums.AnimationType.CharacterAnimation)
                newAnim = _Service.CreateAnimation(ParentCharacter);
            else
                newAnim = _Service.CreateAnimation(AnimationType);
            newAnim.Frequency = EditorSettings.Instance.AnimationFrequency;
            newAnim.SpriteWidth = 1;
            newAnim.SpriteHeight = 1;
            ListAnimations.AddItem(newAnim.Id, newAnim.Title);
            LoadAnimation(newAnim.Id);
        }

        /// <summary>
        /// Synchronise le label d'un item avec la liste d'items et le titre dans le formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListAnimations.ChangeItemName(CurrentAnimation.Id, txtName.Text))
            {
                CurrentAnimation.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentAnimation.Title;
                MessageBox.Show(Errors.ERROR_UNIQUE_TITLE, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Bouton d'origine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginPoint_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.CoordsManager.FormClosed += new FormClosedEventHandler(CoordsManager_FormClosed);
            FormsManager.Instance.CoordsManager.SourceObject = new Rectangle(this.CurrentAnimation.OriginPoint.X, this.CurrentAnimation.OriginPoint.Y,1,1);
            FormsManager.Instance.CoordsManager.SourceResolution = new Size(this.CurrentAnimation.SpriteWidth, this.CurrentAnimation.SpriteHeight);
            FormsManager.Instance.CoordsManager.SourceAnim = this.CurrentAnimation;
            FormsManager.Instance.CoordsManager.UseAnimations = true;
            FormsManager.Instance.CoordsManager.UseStageBackground = false;
            FormsManager.Instance.CoordsManager.UseStages = false;
            FormsManager.Instance.CoordsManager.ShowDialog(this);
        }

        void CoordsManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.CoordsManager.FormClosed -= new FormClosedEventHandler(CoordsManager_FormClosed);
            FormsManager.Instance.CoordsManager.SourceAnim = null;
            FormsManager.Instance.CoordsManager.UseAnimations = false;
            this.CurrentAnimation.OriginPoint = FormsManager.Instance.CoordsManager.DestinationObject.Location;
        }
        #endregion
    }
}