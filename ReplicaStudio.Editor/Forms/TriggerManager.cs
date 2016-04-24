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
    public partial class TriggerManager : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        TriggerService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton sélectionné
        /// </summary>
        public Guid SelectedTrigger
        {
            get;
            set;
        }

        /// <summary>
        /// Bouton courant
        /// </summary>
        public VO_Trigger CurrentTrigger
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public TriggerManager()
        {
            InitializeComponent();
            _Service = new TriggerService();
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

            CurrentTrigger = null;
            ProvisionList();
            if (ListTriggers.DataSource.Count > 0)
            {
                Guid firstAction = ListTriggers.DataSource[0].Id;
                ListTriggers.SelectItem(firstAction);
                LoadTrigger(firstAction);
            }
            else
                ListTriggers_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListTriggers_ItemChosen(object sender, EventArgs e)
        {
            LoadTrigger(ListTriggers.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListTriggers_ItemToCreate(object sender, EventArgs e)
        {
            VO_Trigger newItem = _Service.CreateTrigger();
            newItem.Title = GlobalConstants.TRIGGER_NEW_ITEM;
            ListTriggers.AddItem(newItem.Id, newItem.Title);
            LoadTrigger(newItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListTriggers_ItemToDelete(object sender, EventArgs e)
        {
            CurrentTrigger.Delete();
            CurrentTrigger = null;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListTriggers_ListIsEmpty(object sender, EventArgs e)
        {
            grpInformations.Visible = false;
        }

        /// <summary>
        /// Code ajouté lors d'un double clic sur un item de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListTriggers_ItemDoubleClicked(object sender, EventArgs e)
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
            if(CurrentTrigger != null)
                SelectedTrigger = CurrentTrigger.Id;
            this.Close();
        }

        /// <summary>
        /// Click sur cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _Service.RestaureTriggers();
            this.Close();
        }

        /// <summary>
        /// Le titre a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ListTriggers.ChangeItemName(CurrentTrigger.Id, txtName.Text);
            CurrentTrigger.Title = txtName.Text;
        }

        /// <summary>
        /// La position initiale du bouton a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTrue_CheckedChanged(object sender, EventArgs e)
        {
            CurrentTrigger.Value = chkTrue.Checked;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge un bouton
        /// </summary>
        /// <param name="value">Id du bouton</param>
        public void LoadTrigger(Guid value)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentTrigger = GameCore.Instance.GetTriggerById(value);

            //Afficher les groupes
            grpInformations.Visible = true;

            //Désactiver events
            txtName.TextChanged -= new EventHandler(txtName_TextChanged);
            chkTrue.CheckedChanged -= new EventHandler(chkTrue_CheckedChanged);

            //Bind des infos dans les contrôles
            txtName.Text = CurrentTrigger.Title;
            chkTrue.Checked = CurrentTrigger.Value;

            //Activer les events
            txtName.TextChanged += new EventHandler(txtName_TextChanged);
            chkTrue.CheckedChanged += new EventHandler(chkTrue_CheckedChanged);

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charge la liste des boutons
        /// </summary>
        private void ProvisionList()
        {
            ListTriggers.DataSource = _Service.ProvisionList();
            ListTriggers.LoadList();
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
