using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.ServiceLayer;

namespace ReplicaStudio.Editor
{
    /// <summary>
    /// Formulaire conteneur d'un animation manager
    /// </summary>
    public partial class AnimationManagerContainer : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        AnimationService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Animation cible
        /// </summary>
        public Guid AnimationId { get; set; }

        /// <summary>
        /// Guid du character associé, si besoin
        /// </summary>
        public Guid ParentCharacter { get; set; }

        /// <summary>
        /// Setter le point d'origine?
        /// </summary>
        public bool OriginPoint { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public AnimationManagerContainer()
        {
            InitializeComponent();
            InitializeSDL();
            _Service = new AnimationService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Recharge les données à envoyer à l'Animation manager
        /// </summary>
        /// <param name="pAnimationType">Type d'animation</param>
        /// <param name="pSelectedAnimation">Id de l'animation sélectionnée</param>
        public void ResetAnimationManager(Enums.AnimationType animationType, Guid selectedAnimation)
        {
            AnimationManager.ParentCharacter = this.ParentCharacter;
            AnimationManager.AnimationType = animationType;
            AnimationManager.OriginPoint = this.OriginPoint;
            AnimationManager.CurrentAnimation = new VO_Animation(selectedAnimation, animationType);
            AnimationId = selectedAnimation;
        }

        /// <summary>
        /// Recharge les données à envoyer à l'Animation manager quand celle ci n'existe pas encore
        /// </summary>
        /// <param name="pAnimationType">Type d'animation</param>
        public void ResetAnimationManager(Enums.AnimationType animationType)
        {
            AnimationManager.ParentCharacter = this.ParentCharacter;
            AnimationManager.CurrentAnimation = null;
            AnimationManager.OriginPoint = this.OriginPoint;
            AnimationId = new Guid();
            AnimationManager.AnimationType = animationType;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Click sur Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _Service.RestaureAnim(AnimationManager.AnimationType);
            Cursor.Current = DefaultCursor;
            this.Close();
        }

        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (AnimationManager.CurrentAnimation != null)
            {
                AnimationId = AnimationManager.CurrentAnimation.Id;
            }
            else
            {
                AnimationId = new Guid();
            }
            _Service.SaveAnim(AnimationManager.AnimationType);
            Cursor.Current = DefaultCursor;
            this.Close();
        }

        /// <summary>
        /// Click sur Apply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _Service.SaveAnim(AnimationManager.AnimationType);
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
