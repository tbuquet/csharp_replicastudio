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
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms
{
    public partial class VariableManager : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        VariableService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Bouton sélectionné
        /// </summary>
        public Guid SelectedVariable
        {
            get;
            set;
        }

        /// <summary>
        /// Variable courante
        /// </summary>
        public VO_Variable CurrentVariable
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VariableManager()
        {
            InitializeComponent();
            _Service = new VariableService();
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

            CurrentVariable = null;
            ProvisionList();
            if (ListVariables.DataSource.Count > 0)
            {
                Guid firstAction = ListVariables.DataSource[0].Id;
                ListVariables.SelectItem(firstAction);
                LoadVariable(firstAction);
            }
            else
                ListVariables_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListVariables_ItemChosen(object sender, EventArgs e)
        {
            LoadVariable(ListVariables.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListVariables_ItemToCreate(object sender, EventArgs e)
        {
            VO_Variable newItem = _Service.CreateVariable();
            newItem.Title = GlobalConstants.VARIABLE_NEW_ITEM;
            ListVariables.AddItem(newItem.Id, newItem.Title);
            LoadVariable(newItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListVariables_ItemToDelete(object sender, EventArgs e)
        {
            CurrentVariable.Delete();
            CurrentVariable = null;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListVariables_ListIsEmpty(object sender, EventArgs e)
        {
            grpInformations.Visible = false;
        }

        /// <summary>
        /// Code ajouté lors d'un double clic sur un item de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListVariables_ItemDoubleClicked(object sender, EventArgs e)
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
            if (CurrentVariable != null)
                SelectedVariable = CurrentVariable.Id;
            this.Close();
        }

        /// <summary>
        /// Click sur cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _Service.RestaureVariables();
            this.Close();
        }

        /// <summary>
        /// Le titre a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ListVariables.ChangeItemName(CurrentVariable.Id, txtName.Text);
            CurrentVariable.Title = txtName.Text;
        }

        /// <summary>
        /// La valeur du textbox a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ddpValue_ValueChanged(object sender, EventArgs e)
        {
            CurrentVariable.Value = ConvertTools.CastInt(ddpValue.Value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge un bouton
        /// </summary>
        /// <param name="value">Id du bouton</param>
        public void LoadVariable(Guid value)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentVariable = GameCore.Instance.GetVariableById(value);

            //Afficher les groupes
            grpInformations.Visible = true;

            //Désactiver events
            txtName.TextChanged -= new EventHandler(txtName_TextChanged);
            ddpValue.ValueChanged -=new EventHandler(ddpValue_ValueChanged);

            //Bind des infos dans les contrôles
            txtName.Text = CurrentVariable.Title;
            ddpValue.Value = CurrentVariable.Value;

            //Activer les events
            txtName.TextChanged += new EventHandler(txtName_TextChanged);
            ddpValue.ValueChanged += new EventHandler(ddpValue_ValueChanged);

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charge la liste des boutons
        /// </summary>
        private void ProvisionList()
        {
            ListVariables.DataSource = _Service.ProvisionList();
            ListVariables.LoadList();
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
