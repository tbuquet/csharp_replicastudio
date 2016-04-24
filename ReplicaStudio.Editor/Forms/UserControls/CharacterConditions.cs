using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire des conditions spécifiques aux characters dans l'Event Manager
    /// </summary>
    public partial class CharacterConditions : UserControl
    {
        #region Members
        private Guid _CharacterId;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public CharacterConditions()
        {
            InitializeComponent();

            //Character speed
            ddpSpeed.DataSource = FormsTools.GetMovementsSpeedList();
            ddpSpeed.DisplayMember = "Title";
            ddpSpeed.ValueMember = "Id";

            //Animation frequency
            ddpFrequency.DataSource = FormsTools.GetAnimationFrequencyList();
            ddpFrequency.DisplayMember = "Title";
            ddpFrequency.ValueMember = "Id";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge les contrôles
        /// </summary>
        public void LoadControls(VO_Page currentPage, Guid characterId)
        {
            if (currentPage.PageEventType == Enums.EventType.Character)
            {
                _CharacterId = characterId;

                //Désactivation des events
                ddpDirection.SelectedValueChanged -= new EventHandler(ddpDirection_SelectedValueChanged);
                ddpSpeed.SelectedValueChanged -= new EventHandler(ddpSpeed_SelectedValueChanged);
                ddpFrequency.SelectedValueChanged -= new EventHandler(ddpFrequency_SelectedValueChanged);
                ddpStandingType.SelectedValueChanged -= new EventHandler(ddpStandingType_SelectedValueChanged);
                ddpWalkingType.SelectedValueChanged -= new EventHandler(ddpWalkingType_SelectedValueChanged);
                ddpTalkingType.SelectedValueChanged -= new EventHandler(ddpTalkingType_SelectedValueChanged);

                List<VO_ListItem> list = FormsTools.GetMovementsList();
                ddpDirection.DataSource = list;
                ddpDirection.DisplayMember = "Title";
                ddpDirection.ValueMember = "Id";
                ddpDirection.SelectedValue = (int)currentPage.CharacterDirection;

                ddpSpeed.SelectedValue = currentPage.CharacterSpeed;
                ddpFrequency.SelectedValue = currentPage.CharacterAnimationFrequency;

                ddpStandingType.DataSource = GameCore.Instance.GetCharAnimations(characterId);
                ddpStandingType.DisplayMember = "Title";
                ddpStandingType.ValueMember = "Id";
                if (currentPage.CharacterStandingType == Guid.Empty)
                    ddpStandingType.SelectedIndex = 0;
                else
                    ddpStandingType.SelectedValue = currentPage.CharacterStandingType;

                ddpWalkingType.DataSource = GameCore.Instance.GetCharAnimations(characterId);
                ddpWalkingType.DisplayMember = "Title";
                ddpWalkingType.ValueMember = "Id";
                if (currentPage.CharacterWalkingType == Guid.Empty)
                    ddpWalkingType.SelectedIndex = 0;
                else
                    ddpWalkingType.SelectedValue = currentPage.CharacterWalkingType;

                ddpTalkingType.DataSource = GameCore.Instance.GetCharAnimations(characterId);
                ddpTalkingType.DisplayMember = "Title";
                ddpTalkingType.ValueMember = "Id";
                if (currentPage.CharacterTalkingType == Guid.Empty)
                    ddpTalkingType.SelectedIndex = 0;
                else
                    ddpTalkingType.SelectedValue = currentPage.CharacterTalkingType;

                LoadAnimation();

                //Réactivation des events
                ddpDirection.SelectedValueChanged += new EventHandler(ddpDirection_SelectedValueChanged);
                ddpSpeed.SelectedValueChanged += new EventHandler(ddpSpeed_SelectedValueChanged);
                ddpFrequency.SelectedValueChanged += new EventHandler(ddpFrequency_SelectedValueChanged);
                ddpStandingType.SelectedValueChanged += new EventHandler(ddpStandingType_SelectedValueChanged);
                ddpWalkingType.SelectedValueChanged += new EventHandler(ddpWalkingType_SelectedValueChanged);
                ddpTalkingType.SelectedValueChanged += new EventHandler(ddpTalkingType_SelectedValueChanged);
            }
        }

        /// <summary>
        /// Charge l'animation
        /// </summary>
        private void LoadAnimation()
        {
            if (_CharacterId != Guid.Empty)
            {
                animationControl1.ParentCharacter = _CharacterId;
                animationControl1.Frequency = (int)ddpFrequency.SelectedValue;
                animationControl1.Row = (int)ddpDirection.SelectedValue;
                animationControl1.LoadAnimation((Guid)ddpStandingType.SelectedValue);
                animationControl1.Start();
            }
        }
        #endregion

        #region EventHandlers
        void ddpDirection_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].CharacterDirection = (Enums.Movement)ddpDirection.SelectedValue;

            LoadAnimation();
        }

        void ddpSpeed_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].CharacterSpeed = (int)ddpSpeed.SelectedValue;
        }

        void ddpFrequency_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].CharacterAnimationFrequency = (int)ddpFrequency.SelectedValue;

            LoadAnimation();
        }

        void ddpStandingType_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].CharacterStandingType = (Guid)ddpStandingType.SelectedValue;

            LoadAnimation();
        }

        void ddpWalkingType_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].CharacterWalkingType = (Guid)ddpWalkingType.SelectedValue;
        }

        void ddpTalkingType_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].CharacterTalkingType = (Guid)ddpTalkingType.SelectedValue;
        }
        #endregion
    }
}
