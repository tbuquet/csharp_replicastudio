using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms
{
    public partial class ItemManager : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ItemService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton sélectionné
        /// </summary>
        public Guid SelectedItem
        {
            get;
            set;
        }

        /// <summary>
        /// Bouton courant
        /// </summary>
        public VO_Item CurrentItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ItemManager()
        {
            InitializeComponent();
            _Service = new ItemService();
        }
        #endregion

        #region Eventhandlers
        /// <summary>
        /// Au chargement du controle
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CurrentItem = null;
            ProvisionList();
            if (ListItems.DataSource.Count > 0)
            {
                Guid firstAction = ListItems.DataSource[0].Id;
                ListItems.SelectItem(firstAction);
                LoadItem(firstAction);
            }
            else
                ListItems_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemChosen(object sender, EventArgs e)
        {
            LoadItem(ListItems.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemToCreate(object sender, EventArgs e)
        {
            VO_Item newItem = _Service.CreateItem();
            newItem.Title = GlobalConstants.ITEM_NEW_ITEM;
            ListItems.AddItem(newItem.Id, newItem.Title);
            LoadItem(newItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemToDelete(object sender, EventArgs e)
        {
            CurrentItem.Delete();
            CurrentItem = null;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ListIsEmpty(object sender, EventArgs e)
        {
            //grpInformations.Visible = false;
        }

        /// <summary>
        /// Code ajouté lors d'un double clic sur un item de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemDoubleClicked(object sender, EventArgs e)
        {
            this.btnSelect_Click(this, new EventArgs());
        }

        /// <summary>
        /// Click sur select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(CurrentItem != null)
                SelectedItem = CurrentItem.Id;
            this.Close();
        }

        /// <summary>
        /// Click sur cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Le titre a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// La position initiale du bouton a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTrue_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge un bouton
        /// </summary>
        /// <param name="value">Id du bouton</param>
        public void LoadItem(Guid value)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentItem = GameCore.Instance.GetItemById(value);

            ////Afficher les groupes
            //grpInformations.Visible = true;

            //Bind des infos dans les contrôles

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charge la liste des boutons
        /// </summary>
        private void ProvisionList()
        {
            ListItems.DataSource = _Service.ProvisionList();
            ListItems.LoadList();
        }
        #endregion

        #region Override
        /// <summary>
        /// Désactiver F4
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
                return true;
            else
                return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
