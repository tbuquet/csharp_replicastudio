using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire qui liste des items
    /// </summary>
    public partial class ListItems : UserControl
    {
        #region Members
        /// <summary>
        /// Titre du formulaire d'items
        /// </summary>
        string _Title = string.Empty;

        /// <summary>
        /// Cacher les boutons
        /// </summary>
        bool _HideButtons;
        #endregion

        #region Events
        /// <summary>
        /// Survient lorsqu'un item est choisi
        /// </summary>
        public event EventHandler ItemChosen;

        /// <summary>
        /// Survient lors de la création d'un item
        /// </summary>
        public event EventHandler ItemToCreate;

        /// <summary>
        /// Survient lors de la suppression d'un item
        /// </summary>
        public event EventHandler ItemToDelete;

        //Survient lors que la liste est vide
        public event EventHandler ListIsEmpty;

        /// <summary>
        /// Survient lorsqu'un item a été double cliqué (facultatif)
        /// </summary>
        public event EventHandler ItemDoubleClicked;
        #endregion

        #region Properties
        /// <summary>
        /// Titre du formulaire
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                lblTitle.Text = value;
            }
        }

        /// <summary>
        /// Pour annuler une suppression
        /// </summary>
        public bool CancelDeletion
        {
            get;
            set;
        }

        /// <summary>
        /// Datasource du formulaire
        /// </summary>
        public List<VO_Base> DataSource
        {
            get;
            set;
        }

        /// <summary>
        /// Guid de l'item sélectionné
        /// </summary>
        public Guid ItemSelectedValue
        {
            get;
            set;
        }

        /// <summary>
        /// Définie si un item peut être sélectionné en double cliquant dessus
        /// </summary>
        public bool DoubleClickable
        {
            get;
            set;
        }

        /// <summary>
        /// Cache les boutons
        /// </summary>
        public bool HideButtons
        {
            get
            {
                return _HideButtons;
            }
            set
            {
                _HideButtons = value;
                if (_HideButtons)
                    PanelAction.Visible = false;
                else
                    PanelAction.Visible = true;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ListItems()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Provisionne la liste avec les données DataSource
        /// </summary>
        public void LoadList()
        {
            this.List.SelectedIndexChanged -= new System.EventHandler(this.List_SelectedIndexChanged);
            List.Items.Clear();
            if (DataSource != null)
            {
                foreach (VO_Base vBase in (List<VO_Base>)DataSource)
                {
                    List.Items.Add(vBase);
                }
            }
            if(List.Items.Count > 0)
                List.SelectedIndex = 0;
            else
                this.ListIsEmpty(this, new EventArgs());
            this.List.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
            this.List_SelectedIndexChanged(this, new EventArgs());
        }

        /// <summary>
        /// Provisionne la liste avec les données DataSource
        /// </summary>
        public void LoadList(Guid itemToSelect)
        {
            this.List.SelectedIndexChanged -= new System.EventHandler(this.List_SelectedIndexChanged);
            List.Items.Clear();
            bool itemFound = false;
            if (DataSource != null)
            {
                int i = 0;
                foreach (VO_Base vBase in (List<VO_Base>)DataSource)
                {
                    List.Items.Add(vBase);
                    if (vBase.Id == itemToSelect)
                    {
                        List.SelectedIndex = i;
                        itemFound = true;
                    }
                    i++;
                }
            }
            if (List.Items.Count > 0 && !itemFound)
                List.SelectedIndex = 0;
            else
                this.ListIsEmpty(this, new EventArgs());
            this.List.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
            this.List_SelectedIndexChanged(this, new EventArgs());
        }

        /// <summary>
        /// Change le nom d'un item
        /// </summary>
        /// <param name="pId">ID de l'item à changer</param>
        /// <param name="pTitle">Nouveau nom</param>
        public bool ChangeItemName(Guid id, string title)
        {
            //Cherche si nom en commun
            foreach (object item in List.Items)
            {
                if (((VO_Base)item).Title == title)
                {
                    return false;
                }
            }

            foreach (object item in List.Items)
            {
                if (((VO_Base)item).Id == id)
                {
                    ((VO_Base)item).Title = title;
                    break;
                }
            }
            this.List.SelectedIndexChanged -= new System.EventHandler(this.List_SelectedIndexChanged);
            List.RefreshItems();
            this.List.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);

            return true;
        }

        /// <summary>
        /// Ajoute un nouvel item à la liste
        /// </summary>
        /// <param name="pId">Id de l'item à ajouter</param>
        /// <param name="pTitle">Titre de l'item</param>
        public void AddItem(Guid id, string title)
        {
            VO_Base voBase = new VO_Base();
            voBase.Id = id;
            voBase.Title = title;
            List.Items.Add(voBase);
            SelectItem(voBase.Id);
        }

        /// <summary>
        /// Selectionne un item dans la liste sans déclencher d'EventHandler.
        /// </summary>
        /// <param name="pId">Id du nouvel item sélectionné</param>
        public void SelectItem(Guid id)
        {
            int i = 0;
            foreach (VO_Base item in List.Items)
            {
                if (item.Id == id)
                {
                    this.List.SelectedIndexChanged -= new System.EventHandler(this.List_SelectedIndexChanged);
                    List.SelectedIndex = i;
                    this.List.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
                    return;
                }
                i++;
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Survient lors du changement d'un item voulu par l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (List.SelectedIndex != -1)
            {
                ItemSelectedValue = ((VO_Base)List.SelectedItem).Id;
                this.ItemChosen(this, new EventArgs());
            }
            else if (List.Items.Count > 0)
                List.SelectedIndex = 0;
            else
                this.ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Click sur Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (List.SelectedItem != null)
            {
                ItemSelectedValue = ((VO_Base)List.SelectedItem).Id;
                this.ItemToDelete(this, new EventArgs());
                if(!CancelDeletion)
                    List.Items.RemoveAt(List.SelectedIndex);
                CancelDeletion = false;
            }
        }

        /// <summary>
        /// Click sur Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.ItemToCreate(this, new EventArgs());
        }

        /// <summary>
        /// Double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClickable)
            {
                this.ItemDoubleClicked(this, new EventArgs());
            }
        }
        #endregion
    }
}
