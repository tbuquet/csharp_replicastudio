using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointAndClickStudio.Editor;
using System.Windows.Forms;
using PointAndClickStudio.Editor.Forms;
using PointAndClickStudio.Shared.Constantes;

namespace PointAndClick_Studio.Classes
{
    class FormsManager
    {
        #region Members
        private static FormsManager instance;
        #endregion

        #region Properties
        public static FormsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FormsManager();
                }
                return instance;
            }
        }

        public AnimationManagerContainer AnimationManagerContainer { get; set; }

        public ResourcesManager ResourcesManager { get; set; }

        public EventActions EventActions { get; set; }
        #endregion

        #region Constructors
        private FormsManager()
        {
            AnimationManagerContainer = new AnimationManagerContainer();
            ResourcesManager = new ResourcesManager();
            EventActions = new EventActions();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Permet de charger les formulaires
        /// </summary>
        public void InitManager()
        {
        }

        /// <summary>
        /// Charge l'animation manager avec plusieurs paramètres
        /// </summary>
        /// <param name="pAnimationType">Type d'animation</param>
        /// <param name="pSelectedAnimation">Animation actuellement sélectionnée</param>
        /// <param name="pSelectedRow">Colonne actuellement utilisée</param>
        public void LoadAnimationManager(Enums.AnimationType pAnimationType, Guid pSelectedAnimation, int pSelectedRow)
        {
            FormsManager.instance.AnimationManagerContainer.ResetAnimationManager(pAnimationType, pSelectedAnimation, pSelectedRow);
            FormsManager.Instance.AnimationManagerContainer.ShowDialog();
        }

        /// <summary>
        /// Charge l'animation manager avec plusieurs paramètres
        /// </summary>
        /// <param name="pAnimationType">Type d'animation</param>
        public void LoadAnimationManager(Enums.AnimationType pAnimationType)
        {
            FormsManager.instance.AnimationManagerContainer.ResetAnimationManager(pAnimationType);
            FormsManager.Instance.AnimationManagerContainer.ShowDialog();
        }
        #endregion
    }
}
