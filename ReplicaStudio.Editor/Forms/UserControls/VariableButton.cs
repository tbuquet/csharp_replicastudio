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
    public partial class VariableButton : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        VariableService _Service;

        /// <summary>
        /// Valeur guid du bouton
        /// </summary>
        Guid _VariableGuidValue;
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
        public Guid VariableGuid
        {
            get
            {
                return _VariableGuidValue;
            }
            set
            {
                _VariableGuidValue = value;
                VO_Base variable = GameCore.Instance.GetVariables().Find(p => p.Id == VariableGuid);
                if (variable != null)
                    txtButton.Text = variable.Title;
                else
                    txtButton.Text = GlobalConstants.UNKNOWN;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VariableButton()
        {
            InitializeComponent();
            _Service = new VariableService();
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
            _Service.SaveVariables();
            FormsManager.Instance.VariableManager.FormClosed += new FormClosedEventHandler(VariableManager_FormClosed);
            FormsManager.Instance.VariableManager.SelectedVariable = VariableGuid;
            FormsManager.Instance.VariableManager.ShowDialog(this);
        }

        /// <summary>
        /// Action lors de la fermeture du TriggerManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VariableManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.VariableManager.FormClosed -= new FormClosedEventHandler(VariableManager_FormClosed);
            VariableGuid = FormsManager.Instance.VariableManager.SelectedVariable;
            if(this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
        #endregion
    }
}
