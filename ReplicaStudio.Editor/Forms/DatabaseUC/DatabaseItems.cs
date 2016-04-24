using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;


namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Items de la database
    /// </summary>
    public partial class DatabaseItems : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ItemService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Item actuellement chargé
        /// </summary>
        VO_Item CurrentItem;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseItems()
        {
            InitializeComponent();
            _Service = new ItemService();
            ListItems.Title = Culture.Language.DatabaseRessources.DatabaseItems;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBItems()
        {
            CurrentItem = null;
            ProvisionList();
            if (ListItems.DataSource.Count > 0)
            {
                Guid firstItem = ListItems.DataSource[0].Id;
                ListItems.SelectItem(firstItem);
                LoadItem(firstItem);
            }
            else
                ListItems_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Charge la liste de items
        /// </summary>
        private void ProvisionList()
        {
            ListItems.DataSource = _Service.ProvisionList();
            ListItems.LoadList();
        }

        /// <summary>
        /// Chargement d'un item
        /// </summary>
        /// <param name="guid"></param>
        private void LoadItem(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentItem = GameCore.Instance.GetItemById(guid);

            //Désactiver les eventhandler
            txtDescription.TextChanged -= new EventHandler(txtDescription_TextChanged);
            txtName.LostFocus -= new EventHandler(txtName_TextChanged);

            //Afficher les groupes
            grpIcons.Visible = true;
            grpInformations.Visible = true;
            grpView.Visible = true;

            if (CurrentItem.InventoryIcon != new Guid())
            {
                AnimInventory.LoadAnimation(CurrentItem.InventoryIcon);
                AnimInventory.Start();
            }
            else
                AnimInventory.LoadAnimation(new Guid());

            if (CurrentItem.Icon != new Guid())
            {
                AnimIcon.LoadAnimation(CurrentItem.Icon);
                AnimIcon.Start();
            }
            else
                AnimIcon.LoadAnimation(new Guid());

            if (CurrentItem.ActiveIcon != new Guid())
            {
                AnimActiveIcon.LoadAnimation(CurrentItem.ActiveIcon);
                AnimActiveIcon.Start();
            }
            else
                AnimActiveIcon.LoadAnimation(new Guid());


            //Bind des infos dans les contrôles
            txtDescription.Text = CurrentItem.Description;
            txtName.Text = CurrentItem.Title;

            //Bind des actions
            listActions.Items.Clear();
            foreach (VO_Base action in GameCore.Instance.Game.Actions)
            {
                if (!((VO_Action)action).GoAction && !((VO_Action)action).UseAction)
                    listActions.Items.Add(action);
            }
            listActions.DisplayMember = "Title";
            listActions.ValueMember = "Id";
            if (listActions.Items.Count > 0)
            {
                listActions.SelectedIndex = 0;
            }

            //Activer les eventhandler
            txtDescription.TextChanged += new EventHandler(txtDescription_TextChanged);
            txtName.LostFocus += new EventHandler(txtName_TextChanged);

            Cursor.Current = DefaultCursor;
        }

        public void EnableTreeNodeColor()
        {
            this.ViewScript.EnableDrawManager();
        }

        public void DisableTreeNodeColor()
        {
            this.ViewScript.DisableDrawManager();
        }

        #endregion

        #region EventHandlers
        /// <summary>
        /// Code ajouté lors de la création d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemToCreate(object sender, EventArgs e)
        {
            VO_Item vNewItem = _Service.CreateItem();
            ListItems.AddItem(vNewItem.Id, vNewItem.Title);
            LoadItem(vNewItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemToDelete(object sender, EventArgs e)
        {
            CurrentItem.Delete();
            CurrentItem = null;
        }

        /// <summary>
        /// Code ajouté lors de la sélection d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ItemChosen(object sender, EventArgs e)
        {
            LoadItem(ListItems.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItems_ListIsEmpty(object sender, EventArgs e)
        {
            grpIcons.Visible = false;
            grpInformations.Visible = false;
            grpView.Visible = false;
        }

        /// <summary>
        /// Chargement animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimInventory_AnimationLoading(object sender, EventArgs e)
        {
            CurrentItem.InventoryIcon = AnimInventory.Animation;
        }

        /// <summary>
        /// Chargement animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimIcon_AnimationLoading(object sender, EventArgs e)
        {
            CurrentItem.Icon = AnimIcon.Animation;
        }

        /// <summary>
        /// Chargement animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimActiveIcon_AnimationLoading(object sender, EventArgs e)
        {
            CurrentItem.ActiveIcon = AnimActiveIcon.Animation;
        }

        /// <summary>
        /// Lors de l'edit du titre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListItems.ChangeItemName(CurrentItem.Id, txtName.Text))
            {
                CurrentItem.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentItem.Title;
                MessageBox.Show(Errors.ERROR_UNIQUE_TITLE, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Lors de l'edit de la description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            CurrentItem.Description = txtDescription.Text;
        }

        /// <summary>
        /// Charger un script
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            VO_ActionOnItemScript actionScript = CurrentItem.Scripts.Find(p => p.Id == ((VO_Base)listActions.SelectedItem).Id);
            if (actionScript == null)
            {
                actionScript = new VO_ActionOnItemScript(((VO_Base)listActions.SelectedItem).Id, ObjectsFactory.CreateScript(Enums.ScriptType.ItemEvents));
                CurrentItem.Scripts.Add(actionScript);
            }
            ViewScript.LoadScript(actionScript.Script);
        }
        #endregion
    }
}
