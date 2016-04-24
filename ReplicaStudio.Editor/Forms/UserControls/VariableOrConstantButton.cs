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
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class VariableOrConstantButton : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        VariableService _Service;

        /// <summary>
        /// Valeur guid du bouton
        /// </summary>
        VO_IntValue _VariableGuidValue;
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
        public VO_IntValue VariableGuid
        {
            get
            {
                return _VariableGuidValue;
            }
            set
            {
                _VariableGuidValue = value;
                if (_VariableGuidValue != null)
                {
                    txtButton.TextChanged -= new EventHandler(txtButton_TextChanged);
                    if (VariableGuid.VariableValue == Guid.Empty)
                    {
                        txtButton.Text = VariableGuid.IntValue.ToString();
                    }
                    else
                    {
                        VO_Base variable = GameCore.Instance.GetVariables().Find(p => p.Id == VariableGuid.VariableValue);

                        if (variable != null)
                            txtButton.Text = variable.Title;
                        else
                            txtButton.Text = GlobalConstants.UNKNOWN;
                    }
                    txtButton.TextChanged += new EventHandler(txtButton_TextChanged);
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VariableOrConstantButton()
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
            if (VariableGuid == null)
                VariableGuid = new VO_IntValue();
            _Service.SaveVariables();
            FormsManager.Instance.VariableManager.FormClosed += new FormClosedEventHandler(VariableManager_FormClosed);
            FormsManager.Instance.VariableManager.SelectedVariable = VariableGuid.VariableValue;
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
            VO_IntValue intValue = new VO_IntValue();
            intValue.VariableValue = FormsManager.Instance.VariableManager.SelectedVariable;
            VariableGuid = intValue;
            if(this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }

        /// <summary>
        /// Changement de texte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtButton_TextChanged(object sender, EventArgs e)
        {
            if (VariableGuid == null)
                VariableGuid = new VO_IntValue();
            VariableGuid.VariableValue = Guid.Empty;
            if (!AlphanumericTools.IsNumeric(txtButton.Text) && !string.IsNullOrEmpty(txtButton.Text))
            {
                txtButton.Text = "";
                VariableGuid.IntValue = 0;
            }
            else if(!string.IsNullOrEmpty(txtButton.Text))
                VariableGuid.IntValue = Convert.ToInt32(txtButton.Text);
        }

        /// <summary>
        /// Selection du texte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtButton_Click(object sender, EventArgs e)
        {
            txtButton.SelectAll();
        }
        #endregion
    }
}
