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
using ReplicaStudio.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire de preview d'animation
    /// </summary>
    public partial class AnimationControl : UserControl
    {
        #region Members
        /// <summary>
        /// Lien vers la couche Service
        /// </summary>
        AnimationService _Service;

        /// <summary>
        /// Size de la ressource originale
        /// </summary>
        Size _OriginalResourceSize;

        /// <summary>
        /// Lock pour éviter le lancement d'un autre refresh si le précédent n'est pas terminé.
        /// </summary>
        bool _CallBackLock = false;

        /// <summary>
        /// Timer de fréquence
        /// </summary>
        Timer _FrequencyTimer = null;

        /// <summary>
        /// Taille et position du sprite courant
        /// </summary>
        Rectangle _CurrentSprite = new Rectangle();

        /// <summary>
        /// Activer ou non le double click vers un Animation Manager
        /// </summary>
        bool _LinkToAnimationManager = false;

        /// <summary>
        /// Animation courante
        /// </summary>
        VO_Animation _CurrentAnim = null;
        #endregion

        #region Events
        public event EventHandler AnimationLoading;
        #endregion

        #region Properties
        /// <summary>
        /// Ajoute le lien vers l'animation manager
        /// </summary>
        public bool LinkToAnimationManager
        {
            get
            {
                return _LinkToAnimationManager;
            }
            set
            {
                if (value)
                    AnimPreview.DoubleClick += new EventHandler(AnimationControl_DoubleClick);
                else
                    AnimPreview.DoubleClick -= new EventHandler(AnimationControl_DoubleClick);
                _LinkToAnimationManager = value;
            }
        }

        /// <summary>
        /// Filtre d'animation pour l'animation manager
        /// </summary>
        public Enums.AnimationType AnimationFilter { get; set; }

        /// <summary>
        /// Animation cible
        /// </summary>
        public Guid Animation { get; set; }

        /// <summary>
        /// Surcharger la fréquence de l'animation
        /// </summary>
        public int Frequency
        {
            get
            {
                return _FrequencyTimer.Interval;
            }
            set
            {
                _FrequencyTimer.Interval = value;
            }
        }

        /// <summary>
        /// Surcharger la ligne (utilisée pour persos)
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Surcharger la fréquence?
        /// </summary>
        public bool UseCustomFrequency { get; set; }

        /// <summary>
        /// Surcharger la ligne?
        /// </summary>
        public bool UseCustomRow { get; set; }

        /// <summary>
        /// Guid ParentCharacter
        /// </summary>
        public Guid ParentCharacter { get; set; }

        /// <summary>
        /// Setter le point d'origine
        /// </summary>
        public bool OriginPoint { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public AnimationControl()
        {
            //Initialisation du composant
            InitializeComponent();

            //Initialisation de la couche Service
            _Service = new AnimationService();

            //Timer
            _FrequencyTimer = new Timer();
            _FrequencyTimer.Tick += new EventHandler(CallBack_Frequency);

            //Border Colorisation
            this.AnimPreview.MouseEnter += new EventHandler(MouseEnterScript);
            this.AnimPreview.MouseLeave += new EventHandler(MouseLeaveScript);
        }
        #endregion

        #region EventHandlers

        /// <summary>
        /// Fonction qui est exécutée au passage à la souris
        /// </summary>
        /// <param name="e"></param>
        private void MouseEnterScript(object sender, EventArgs e)
        {
            if (LinkToAnimationManager)
            {
                this.BackColor = Color.Orange;
                this.BorderStyle = BorderStyle.None;
                this.Padding = new System.Windows.Forms.Padding(2);
            }
        }

        /// <summary>
        /// Fonction qui est exécutée quand la souris ne focus plus
        /// </summary>
        /// <param name="e"></param>
        private void MouseLeaveScript(object sender, EventArgs e)
        {
            if (LinkToAnimationManager)
            {
                this.BackColor = Color.LightGray;
                this.BorderStyle = BorderStyle.None;
                this.Padding = new System.Windows.Forms.Padding(2);
            }
        }

        /// <summary>
        /// Fonction qui est exécutée à la fin du Timer
        /// </summary>
        /// <param name="e"></param>
        private void CallBack_Frequency(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (_CurrentAnim != null)
                {
                    _CurrentSprite.X += _CurrentAnim.SpriteWidth;
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
        /// Lance l'animation manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AnimationControl_DoubleClick(object sender, EventArgs e)
        {
            FormsManager.Instance.AnimationManagerContainer.FormClosed += new FormClosedEventHandler(AnimationManagerContainer_FormClosed);
            FormsManager.Instance.AnimationManagerContainer.ParentCharacter = this.ParentCharacter;
            FormsManager.Instance.AnimationManagerContainer.OriginPoint = this.OriginPoint;
            if(_CurrentAnim != null && _CurrentAnim.AnimationType == Enums.AnimationType.CharacterAnimation)
                _Service.SaveAnim(AnimationFilter, this.ParentCharacter);
            else
                _Service.SaveAnim(AnimationFilter);
            if(_CurrentAnim == null || _CurrentAnim.Id == new Guid())
                FormsManager.Instance.LoadAnimationManager(AnimationFilter);
            else
                FormsManager.Instance.LoadAnimationManager(AnimationFilter, _CurrentAnim.Id);
        }

        /// <summary>
        /// Récupération de la valeur de l'animation manager.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AnimationManagerContainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadAnimation(FormsManager.Instance.AnimationManagerContainer.AnimationId);
            this.Start();
            FormsManager.Instance.AnimationManagerContainer.OriginPoint = false;
            FormsManager.Instance.AnimationManagerContainer.FormClosed -= new FormClosedEventHandler(AnimationManagerContainer_FormClosed);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Méthode qui charge l'animation courante
        /// </summary>
        public void LoadAnimation(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Stop Timer
            _FrequencyTimer.Stop();

            //Anim courante
            Animation = guid;
            if(this.AnimationLoading != null)
                this.AnimationLoading(this, new EventArgs());

            if(AnimationFilter == Enums.AnimationType.CharacterAnimation)
                _CurrentAnim = _Service.LoadVOObject(ParentCharacter, Animation);
            else
                _CurrentAnim = _Service.LoadVOObject(AnimationFilter, Animation);

            //Rechargement de la ressource
            if (_CurrentAnim != null && _CurrentAnim.ResourcePath != null)
            {
                //Mise en place du sprite root.
                _CurrentSprite.X = 0;
                _CurrentSprite.Width = _CurrentAnim.SpriteWidth;
                _CurrentSprite.Height = _CurrentAnim.SpriteHeight;
                if(UseCustomRow)
                    _CurrentSprite.Y = Row * _CurrentSprite.Height;
                else
                _CurrentSprite.Y = _CurrentAnim.Row * _CurrentSprite.Height;
                if (!string.IsNullOrEmpty(_CurrentAnim.ResourcePath))
                {
                    _Service.LoadSurfaceFromURI(PathTools.GetProjectPath(AnimationFilter) + _CurrentAnim.ResourcePath);
                }
                else
                {
                    _Service.LoadSurfaceFromURI(string.Empty);
                    _CurrentSprite.Y = 0;
                }
                

                //Timer
                if(UseCustomFrequency)
                    _FrequencyTimer.Interval = 10000 / Frequency;
                else
                    _FrequencyTimer.Interval = 10000 / _CurrentAnim.Frequency;
            }
            //TODO: A vérif si toujours nécessaire
            else
            {
                _Service.LoadEmptySurface();
                _CurrentSprite.X = 0;
                _CurrentSprite.Width = 1;
                _CurrentSprite.Height = 1;
            }
            //FINTODO

            //Récupération de la taille de la ressource originale
            _OriginalResourceSize = _Service.GetSizeOfResource();

            RefreshPreview();

            Cursor.Current = DefaultCursor;
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

        /// <summary>
        /// Méthode qui rafraichi l'AnimPreview et fait apparaitre le rectangle de surbrillance sur la surface Resource.
        /// </summary>
        public void RefreshPreview()
        {
            //Vérifie si l'anim a besoin d'être reset
            if (_CurrentSprite.X >= _OriginalResourceSize.Width)
                ResetPreview(false);

            if (AnimPreview.Image != null)
                AnimPreview.Image.Dispose();
            AnimPreview.Image = _Service.RefreshAnimation(AnimPreview.DisplayRectangle, new Rectangle(new Point(_CurrentSprite.X, _CurrentSprite.Y), _CurrentSprite.Size));
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
            _CurrentSprite.Width = _CurrentAnim.SpriteWidth;
            _CurrentSprite.Height = _CurrentAnim.SpriteHeight;
        }
        #endregion
    }
}
