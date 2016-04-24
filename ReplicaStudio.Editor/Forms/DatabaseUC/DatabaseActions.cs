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

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Actions de la database
    /// </summary>
    public partial class DatabaseActions : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ActionService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Action actuellement chargée
        /// </summary>
        VO_Action CurrentAction;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseActions()
        {
            InitializeComponent();
            _Service = new ActionService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBActions()
        {
            CurrentAction = null;
            ProvisionList();
            if (ListActions.DataSource.Count > 0)
            {
                Guid firstAction = ListActions.DataSource[0].Id;
                ListActions.SelectItem(firstAction);
                LoadAction(firstAction);
            }
            else
                ListActions_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Charge la liste de actions
        /// </summary>
        private void ProvisionList()
        {
            ListActions.DataSource = _Service.ProvisionList();
            ListActions.LoadList();
        }

        /// <summary>
        /// Chargement d'un item
        /// </summary>
        /// <param name="guid"></param>
        private void LoadAction(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentAction = GameCore.Instance.GetActionById(guid);

            //Désactiver les eventhandler
            txtDescription.TextChanged -= new EventHandler(txtDescription_TextChanged);
            txtName.LostFocus -= new EventHandler(txtName_TextChanged);

            //Afficher les groupes
            grpIcons.Visible = true;
            grpInformations.Visible = true;

            if (CurrentAction.InventoryIcon != new Guid())
            {
                AnimInventory.LoadAnimation(CurrentAction.InventoryIcon);
                AnimInventory.Start();
            }
            else
                AnimInventory.LoadAnimation(new Guid());

            if (CurrentAction.Icon != new Guid())
            {
                AnimIcon.LoadAnimation(CurrentAction.Icon);
                AnimIcon.Start();
            }
            else
                AnimIcon.LoadAnimation(new Guid());

            if (CurrentAction.ActiveIcon != new Guid())
            {
                AnimActiveIcon.LoadAnimation(CurrentAction.ActiveIcon);
                AnimActiveIcon.Start();
            }
            else
                AnimActiveIcon.LoadAnimation(new Guid());


            //Bind des infos dans les contrôles
            txtDescription.Text = CurrentAction.Description;
            txtName.Text = CurrentAction.Title;

            //Activer les eventhandler
            txtDescription.TextChanged += new EventHandler(txtDescription_TextChanged);
            txtName.LostFocus += new EventHandler(txtName_TextChanged);

            Cursor.Current = DefaultCursor;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListActions_ItemChosen(object sender, EventArgs e)
        {
            LoadAction(ListActions.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListActions_ItemToCreate(object sender, EventArgs e)
        {
            VO_Action vNewItem = _Service.CreateAction();
            ListActions.AddItem(vNewItem.Id, vNewItem.Title);
            LoadAction(vNewItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListActions_ItemToDelete(object sender, EventArgs e)
        {
            bool deletable = true;
            foreach (VO_Action action in GameCore.Instance.Game.Actions)
            {
                if ((action.Id == CurrentAction.Id && action.GoAction) || (action.Id == CurrentAction.Id && action.UseAction))
                {
                     deletable = false;
                }
            }

            if (!deletable)
            {
                MessageBox.Show(Errors.ERROR_ITEM_DELETION, Errors.ERROR_BOX_TITLE);
                ListActions.CancelDeletion = true;
            }
            else
            {
                CurrentAction.Delete();
                CurrentAction = null;
            }
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListActions_ListIsEmpty(object sender, EventArgs e)
        {
            grpIcons.Visible = false;
            grpInformations.Visible = false;
        }

        /// <summary>
        /// Lors de l'edit du titre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListActions.ChangeItemName(CurrentAction.Id, txtName.Text))
            {
                CurrentAction.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentAction.Title;
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
            CurrentAction.Description = txtDescription.Text;
        }

        /// <summary>
        /// Chargement animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimInventory_AnimationLoading(object sender, EventArgs e)
        {
            CurrentAction.InventoryIcon = AnimInventory.Animation;
        }

        /// <summary>
        /// Chargement animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimIcon_AnimationLoading(object sender, EventArgs e)
        {
            CurrentAction.Icon = AnimIcon.Animation;
        }

        /// <summary>
        /// Chargement animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimActiveIcon_AnimationLoading(object sender, EventArgs e)
        {
            CurrentAction.ActiveIcon = AnimActiveIcon.Animation;
        }
        #endregion
    }
}
