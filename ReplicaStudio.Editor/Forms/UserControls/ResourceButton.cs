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
using System.IO;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    public partial class ResourceButton : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ResourcesManagerService _Service;

        /// <summary>
        /// Valeur string de la ressource
        /// </summary>
        string _ResourceStringValue;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand la valeur de la ressource change
        /// </summary>
        public event EventHandler ValueChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton associé
        /// </summary>
        public string ResourceString
        {
            get
            {
                return _ResourceStringValue;
            }
            set
            {
                _ResourceStringValue = value;
                txtButton.Text = Path.GetFileNameWithoutExtension(_ResourceStringValue);
            }
        }

        /// <summary>
        /// Filtre associé
        /// </summary>
        public string Filter
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ResourceButton()
        {
            InitializeComponent();
            _Service = new ResourcesManagerService();
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
            FormsManager.Instance.ResourcesManager.FormClosed += new FormClosedEventHandler(TriggerManager_FormClosed);
            FormsManager.Instance.ResourcesManager.SelectedFilePath = ResourceString;
            FormsManager.Instance.ResourcesManager.Filter = Filter;
            FormsManager.Instance.ResourcesManager.ShowDialog(this);
        }

        /// <summary>
        /// Action lors de la fermeture du TriggerManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TriggerManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.ResourcesManager.FormClosed -= new FormClosedEventHandler(TriggerManager_FormClosed);
            ResourceString = FormsManager.Instance.ResourcesManager.SelectedFilePath;
            if(this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
        #endregion
    }
}
