using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire des conditions spécifiques aux events dans l'Event Manager
    /// </summary>
    public partial class EventConditions : UserControl
    {
        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public EventConditions()
        {
            InitializeComponent();

            //Binding des Descriptions d'Enumeration sur la ComboBox
            ddpTrigger.DataSource = typeof(Enums.TriggerEventConditionType).ToList();
            ddpTrigger.DisplayMember = "Value";
            ddpTrigger.ValueMember = "Key";
        }

        #endregion

        #region Methods
        /// <summary>
        /// Charge les contrôles
        /// </summary>
        public void LoadControls(VO_Page currentPage)
        {
            ddpTrigger.SelectedValueChanged -= new EventHandler(EventManager_TriggerEventConditionTypeChanged);
            this.ddpTrigger.SelectedValue = currentPage.TriggerCondition;
            ddpTrigger.SelectedValueChanged += new EventHandler(EventManager_TriggerEventConditionTypeChanged);
        }
        #endregion

        #region Events

        private void EventManager_TriggerEventConditionTypeChanged(object sender, EventArgs e)
        {
            EventManager CurrentParent = ParentForm as EventManager;
            //Reflection de la Description d'Enum vers Enum Simple
            CurrentParent.CurrentEvent.PageList[CurrentParent.PageIndex].TriggerCondition = (Enums.TriggerEventConditionType)((ddpTrigger.SelectedItem.GetType()).GetProperty("Key")).GetValue(ddpTrigger.SelectedItem, null);
        }
        #endregion
    }
}
