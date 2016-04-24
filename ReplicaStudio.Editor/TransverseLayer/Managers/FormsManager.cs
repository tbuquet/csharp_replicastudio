using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Editor;
using System.Windows.Forms;
using ReplicaStudio.Editor.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.Forms.ScriptForms;

namespace ReplicaStudio.Editor.TransverseLayer.Managers
{
    /// <summary>
    /// Gestionnaire de formulaires utiles à toute l'application
    /// </summary>
    class FormsManager
    {
        #region Members
        /// <summary>
        /// Instance singleton
        /// </summary>
        private static FormsManager _Instance;
        #endregion

        #region Properties
        /// <summary>
        /// Instance singleton
        /// </summary>
        public static FormsManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FormsManager();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Référence à l'AnimationManagerContainer
        /// </summary>
        public AnimationManagerContainer AnimationManagerContainer { get; set; }

        /// <summary>
        /// Référence au ResourcesManager
        /// </summary>
        public ResourcesManager ResourcesManager { get; set; }

        /// <summary>
        /// Référence au DialogManager
        /// </summary>
        public DialogManager DialogManager { get; set; }

        /// <summary>
        /// Référence au TriggerManager
        /// </summary>
        public TriggerManager TriggerManager { get; set; }

        /// <summary>
        /// Référence au ItemManager
        /// </summary>
        public ItemManager ItemManager { get; set; }

        /// <summary>
        /// Référence au CharacterManager
        /// </summary>
        public CharacterManager CharacterManager { get; set; }

        /// <summary>
        /// Référence au VariableManager
        /// </summary>
        public VariableManager VariableManager { get; set; }
        
        /// <summary>
        /// Référence au CoordsManager
        /// </summary>
        public CoordsManager CoordsManager { get; set; }

        /// <summary>
        /// Référence au EventManager
        /// </summary>
        public EventManager EventManager { get; set; }

        /// <summary>
        /// Référence au EventActions
        /// </summary>
        public EventActions EventActions { get; set; }

        /// <summary>
        /// Référence au ScriptCondition
        /// </summary>
        public ScriptCondition ScriptCondition { get; set; }

        /// <summary>
        /// Référence au ScriptLoop
        /// </summary>
        public ScriptLoop ScriptLoop { get; set; }

        /// <summary>
        /// Référence au ScriptChoice
        /// </summary>
        public ScriptChoice ScriptChoice { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        private FormsManager()
        {
            AnimationManagerContainer = new AnimationManagerContainer();
            ResourcesManager = new ResourcesManager();
            DialogManager = new DialogManager();
            TriggerManager = new TriggerManager();
            ItemManager = new ItemManager();
            CharacterManager = new CharacterManager();
            VariableManager = new VariableManager();
            CoordsManager = new CoordsManager();
            EventManager = new EventManager();
            EventActions = new EventActions();
            ScriptCondition = new ScriptCondition();
            ScriptLoop = new ScriptLoop();
            ScriptChoice = new ScriptChoice();
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
        public void LoadAnimationManager(Enums.AnimationType animationType, Guid selectedAnimation)
        {
            FormsManager.Instance.AnimationManagerContainer.ResetAnimationManager(animationType, selectedAnimation);
            FormsManager.Instance.AnimationManagerContainer.ShowDialog();
        }

        /// <summary>
        /// Charge l'animation manager avec plusieurs paramètres
        /// </summary>
        /// <param name="pAnimationType">Type d'animation</param>
        public void LoadAnimationManager(Enums.AnimationType animationType)
        {
            FormsManager.Instance.AnimationManagerContainer.ResetAnimationManager(animationType);
            FormsManager.Instance.AnimationManagerContainer.ShowDialog();
        }
        #endregion
    }
}
