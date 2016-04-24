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
using ReplicaStudio.Editor.TransverseLayer;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire des conditions spécifiques aux animations dans l'Event Manager
    /// </summary>
    public partial class AnimationsConditions : UserControl
    {
        #region Members
        private Guid _AnimationId;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public AnimationsConditions()
        {
            InitializeComponent();
            TriggerExecutionTypeCombo.SelectedValueChanged -= new EventHandler(EventManager_TriggerExecutionTypeChanged);

            //Binding des Descriptions d'Enumeration sur la ComboBox
            TriggerExecutionTypeCombo.DataSource = typeof(Enums.TriggerExecutionType).ToList();
            TriggerExecutionTypeCombo.DisplayMember = "Value";
            TriggerExecutionTypeCombo.ValueMember = "Key";

            //Animation frequency
            ddpSpeed.DataSource = FormsTools.GetAnimationFrequencyList();
            ddpSpeed.DisplayMember = "Title";
            ddpSpeed.ValueMember = "Id";
        }

        #endregion

        #region Methods
        /// <summary>
        /// Charge les contrôles
        /// </summary>
        public void LoadControls(VO_Page currentPage, Guid animationId)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            //Désactiver les events
            TriggerExecutionTypeCombo.SelectedValueChanged -= new EventHandler(EventManager_TriggerExecutionTypeChanged);
            ddpSpeed.SelectedValueChanged += new EventHandler(ddpSpeed_SelectedValueChanged);
            chkStartFrozen.CheckedChanged -= new EventHandler(chkStartFrozen_CheckedChanged);

            _AnimationId = animationId;

            this.TriggerExecutionTypeCombo.SelectedValue = currentPage.TriggerExecution;
            this.ddpSpeed.SelectedValue = currentPage.AnimationFrequency;
            this.chkStartFrozen.Checked = currentPage.AnimationFrozenAtStart;

            LoadAnimation();

            //Activer les events
            TriggerExecutionTypeCombo.SelectedValueChanged += new EventHandler(EventManager_TriggerExecutionTypeChanged);
            ddpSpeed.SelectedValueChanged += new EventHandler(ddpSpeed_SelectedValueChanged);
            chkStartFrozen.CheckedChanged += new EventHandler(chkStartFrozen_CheckedChanged);
        }

        /// <summary>
        /// Charge l'animation
        /// </summary>
        private void LoadAnimation()
        {
            if (_AnimationId != Guid.Empty)
            {
                animationControl1.Frequency = (int)ddpSpeed.SelectedValue;
                animationControl1.LoadAnimation(_AnimationId);
                animationControl1.Start();
            }
        }
        #endregion

        #region Events
        void chkStartFrozen_CheckedChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].AnimationFrozenAtStart = chkStartFrozen.Checked;
        }

        void ddpSpeed_SelectedValueChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;

            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].AnimationFrequency = (int)ddpSpeed.SelectedValue;

            LoadAnimation();
        }

        private void EventManager_TriggerExecutionTypeChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;
            //Reflection de la Description d'Enum vers Enum Simple
            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].TriggerExecution = (Enums.TriggerExecutionType)((TriggerExecutionTypeCombo.SelectedItem.GetType()).GetProperty("Key")).GetValue(TriggerExecutionTypeCombo.SelectedItem, null);
        }
        #endregion

    }
}
