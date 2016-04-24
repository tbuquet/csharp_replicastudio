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
    public partial class ItemButton : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ItemService _Service;

        /// <summary>
        /// Valeur guid du bouton
        /// </summary>
        Guid _ItemGuidValue;
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
        public Guid ItemGuid
        {
            get
            {
                return _ItemGuidValue;
            }
            set
            {
                _ItemGuidValue = value;
                VO_Base item = GameCore.Instance.GetItems().Find(p => p.Id == ItemGuid);
                if (item != null)
                    txtButton.Text = item.Title;
                else
                    txtButton.Text = GlobalConstants.UNKNOWN;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ItemButton()
        {
            InitializeComponent();
            _Service = new ItemService();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Ouvre le ItemManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FormsManager.Instance.ItemManager.FormClosed += new FormClosedEventHandler(ItemManager_FormClosed);
            FormsManager.Instance.ItemManager.SelectedItem = ItemGuid;
            FormsManager.Instance.ItemManager.ShowDialog(this);
        }

        /// <summary>
        /// Action lors de la fermeture du ItemManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ItemManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.ItemManager.FormClosed -= new FormClosedEventHandler(ItemManager_FormClosed);
            ItemGuid = FormsManager.Instance.ItemManager.SelectedItem;
            if (this.ValueChanged != null)
                this.ValueChanged(this, new EventArgs());
        }
        #endregion
    }
}
