using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class TriggerButton : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        TriggerService _Service;

        /// <summary>
        /// Valeur guid du bouton
        /// </summary>
        Guid _TriggerGuidValue;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand la valeur de l'id du boutton change
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton associé
        /// </summary>
        public Guid TriggerGuid
        {
            get
            {
                return _TriggerGuidValue;
            }
            set
            {
                _TriggerGuidValue = value;
                VO_Base trigger = GameCore.Instance.GetTriggers().Find(p => p.Id == TriggerGuid);
                if (trigger != null)
                    txtButton.Text = trigger.Title;
                else
                    txtButton.Text = GlobalConstants.UNKNOWN;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public TriggerButton()
        {
            InitializeComponent();
            _Service = new TriggerService();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Ouvre le TriggerManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            _Service.SaveTriggers();
            FormsManager.Instance.TriggerManager.FormClosed += new FormClosedEventHandler(TriggerManager_FormClosed);
            FormsManager.Instance.TriggerManager.SelectedTrigger = TriggerGuid;
            FormsManager.Instance.TriggerManager.ShowDialog(this);
        }

        /// <summary>
        /// Action lors de la fermeture du TriggerManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TriggerManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.TriggerManager.FormClosed -= new FormClosedEventHandler(TriggerManager_FormClosed);
            TriggerGuid = FormsManager.Instance.TriggerManager.SelectedTrigger;
            if(this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
        #endregion
    }
}
