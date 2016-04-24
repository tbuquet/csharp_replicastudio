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
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Classes de la database
    /// </summary>
    public partial class DatabaseClasses : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        ClassService _Service;

        /// <summary>
        /// Liste des actions
        /// </summary>
        List<VO_Base> _Actions;

        /// <summary>
        /// Liste des characters
        /// </summary>
        List<VO_Base> _Characters;
        #endregion

        #region Properties
        /// <summary>
        /// Classe actuellement chargée
        /// </summary>
        VO_Class CurrentClass;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseClasses()
        {
            InitializeComponent();
            _Service = new ClassService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBClasses()
        {
            CurrentClass = null;
            ProvisionList();
            if (ListClasses.DataSource.Count > 0)
            {
                Guid firstAction = ListClasses.DataSource[0].Id;
                ListClasses.SelectItem(firstAction);
                LoadClass(firstAction);
            }
            else
                ListClasses_ListIsEmpty(this, new EventArgs());
        }

        /// <summary>
        /// Chargement d'une classe
        /// </summary>
        /// <param name="guid"></param>
        private void LoadClass(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentClass = GameCore.Instance.GetClassById(guid);

            //Afficher les groupes
            grpInformations.Visible = true;

            //Charger les listes
            LoadLists();

            //Désactiver events
            grdBadInteractions.CellValueChanged -=new DataGridViewCellEventHandler(grdBadInteractions_CellValueChanged);
            txtName.LostFocus -= new EventHandler(txtName_TextChanged);
            grdBadInteractions.CellClick -= new DataGridViewCellEventHandler(grdBadInteractions_CellClick);

            //Charger la gridview
            grdBadInteractions.Rows.Clear();
            foreach (VO_BadInteraction badInteraction in CurrentClass.BadInteractions)
            {
                InsertBadInteraction(badInteraction);
            }

            //Bind des infos dans les contrôles
            txtName.Text = CurrentClass.Title;

            //Activer les events
            grdBadInteractions.CellValueChanged += new DataGridViewCellEventHandler(grdBadInteractions_CellValueChanged);
            txtName.LostFocus += new EventHandler(txtName_TextChanged);
            grdBadInteractions.CellClick +=new DataGridViewCellEventHandler(grdBadInteractions_CellClick);

            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charge la liste de classes
        /// </summary>
        private void ProvisionList()
        {
            ListClasses.DataSource = _Service.ProvisionList();
            ListClasses.LoadList();
        }

        /// <summary>
        /// Ajoute une "mauvaise interaction" dans la gridview
        /// </summary>
        /// <param name="badInteraction"></param>
        private void InsertBadInteraction(VO_BadInteraction badInteraction)
        {
            DataGridViewRow row = new DataGridViewRow();

            //Id
            DataGridViewTextBoxCell cellId = new DataGridViewTextBoxCell();
            cellId.Value = badInteraction.Id;

            //Action
            DataGridViewComboBoxCell cellAction = new DataGridViewComboBoxCell();
            cellAction.DisplayMember = "Title";
            cellAction.ValueMember = "Id";
            cellAction.DataSource = _Actions;
            cellAction.Value = badInteraction.Action;

            //Action
            DataGridViewComboBoxCell cellCharacter = new DataGridViewComboBoxCell();
            cellCharacter.DisplayMember = "Title";
            cellCharacter.ValueMember = "Id";
            cellCharacter.DataSource = _Characters;
            cellCharacter.Value = badInteraction.Character;

            //Dialog
            DataGridViewButtonCell cellDialog = new DataGridViewButtonCell();
            cellDialog.Value = GlobalConstants.CLASS_CHOOSE_DIALOG;

            //Delete
            DataGridViewButtonCell cellDelete = new DataGridViewButtonCell();
            cellDelete.Value = GlobalConstants.GRIDVIEW_DELETE;

            row.Cells.Add(cellId);
            row.Cells.Add(cellAction);
            row.Cells.Add(cellCharacter);
            row.Cells.Add(cellDialog);
            row.Cells.Add(cellDelete);
            grdBadInteractions.Rows.Add(row);
        }

        /// <summary>
        /// Charge les listes des actions et des characters
        /// </summary>
        private void LoadLists()
        {
            //Charger les actions
            _Actions = new List<VO_Base>();
            _Actions.Add(new VO_Base(new Guid(), GlobalConstants.ALL));
            foreach (VO_Base action in GameCore.Instance.Game.Actions)
            {
                if(!((VO_Action)action).GoAction)
                    _Actions.Add(action);
            }
            
            //Charger les actions
            _Characters = new List<VO_Base>();
            _Characters.Add(new VO_Base(new Guid(), GlobalConstants.ALL));
            foreach (VO_Base character in GameCore.Instance.Game.PlayableCharacters)
            {
                _Characters.Add(character);
            }
        }

        /// <summary>
        /// Supprimer une Bad Interaction
        /// </summary>
        /// <param name="row">Ligne à supprimer</param>
        private void DeleteBadInteraction(DataGridViewRow row)
        {
            grdBadInteractions.Rows.Remove(row);

            Guid id = (Guid)row.Cells[0].Value;

            CurrentClass.BadInteractions.RemoveAll(p => p.Id == id);
        }

        /// <summary>
        /// Charger la fenêtre de dialogue
        /// </summary>
        /// <param name="row">Ligne référence</param>
        private void LoadDialogPanel(DataGridViewRow row)
        {
            Guid id = (Guid)row.Cells[0].Value;
            VO_Dialog dialog = CurrentClass.BadInteractions.Find(p => p.Id == id).Dialog;

            FormsManager.Instance.DialogManager.FormClosed += new FormClosedEventHandler(DialogManager_FormClosed);
            FormsManager.Instance.DialogManager.LoadDialog(dialog.Clone(), Enums.ScriptType.ClassDialogs);
            FormsManager.Instance.DialogManager.ShowDialog(this);
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListClasses_ItemChosen(object sender, EventArgs e)
        {
            LoadClass(ListClasses.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListClasses_ItemToCreate(object sender, EventArgs e)
        {
            VO_Class newItem = _Service.CreateClass();
            ListClasses.AddItem(newItem.Id, newItem.Title);
            LoadClass(newItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListClasses_ItemToDelete(object sender, EventArgs e)
        {
            CurrentClass.Delete();
            CurrentClass = null;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListClasses_ListIsEmpty(object sender, EventArgs e)
        {
            grpInformations.Visible = false;
        }

        /// <summary>
        /// Lors de l'edit du titre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListClasses.ChangeItemName(CurrentClass.Id, txtName.Text))
            {
                CurrentClass.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentClass.Title;
                MessageBox.Show(Errors.ERROR_UNIQUE_TITLE, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Bouton pour créer une nouvelle "bad interaction"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateBadInteraction_Click(object sender, EventArgs e)
        {
            grdBadInteractions.Rows.Add();
            CurrentClass.BadInteractions.Add(_Service.CreateBadInteraction());
            LoadClass(CurrentClass.Id);
        }

        /// <summary>
        /// Clic dans la gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdBadInteractions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdBadInteractions.Rows.Count)
            {
                DataGridViewRow currentRow = grdBadInteractions.Rows[e.RowIndex];

                switch (e.ColumnIndex)
                {
                    //Dialog
                    case 3:
                        LoadDialogPanel(currentRow);
                        break;
                    //Delete
                    case 4:
                        DeleteBadInteraction(currentRow);
                        break;
                }
            }
        }

        /// <summary>
        /// Enregistrer les valeurs drop down list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdBadInteractions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdBadInteractions.Rows.Count)
            {
                Guid id = (Guid)grdBadInteractions.Rows[e.RowIndex].Cells[0].Value;

                VO_BadInteraction badInteraction = CurrentClass.BadInteractions.Find(p => p.Id == id);
                badInteraction.Action = (Guid)grdBadInteractions.Rows[e.RowIndex].Cells[1].Value;
                badInteraction.Character = (Guid)grdBadInteractions.Rows[e.RowIndex].Cells[2].Value;
            }
        }

        /// <summary>
        /// Erreurs gérés en remplacant la ligne effacée par un Unknown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdBadInteractions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdBadInteractions.Rows.Count)
            {
                Guid id = (Guid)grdBadInteractions.Rows[e.RowIndex].Cells[0].Value;

                VO_BadInteraction badInteraction = CurrentClass.BadInteractions.Find(p => p.Id == id);
                grdBadInteractions.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = new Guid();
            }
        }

        /// <summary>
        /// Recharger le character si changement de visibilité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseClasses_VisibleChanged(object sender, EventArgs e)
        {
            if (CurrentClass != null)
            {
                LoadClass(CurrentClass.Id);
            }
        }

        /// <summary>
        /// Le dialogue manager se ferme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DialogManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.DialogManager.FormClosed -= new FormClosedEventHandler(DialogManager_FormClosed);
            if (!FormsManager.Instance.DialogManager.CanceledChanges)
            {
                VO_BadInteraction badInteraction = CurrentClass.BadInteractions.Find(p => p.Id == FormsManager.Instance.DialogManager.CurrentDialog.ParentObjectId);
                badInteraction.Dialog = FormsManager.Instance.DialogManager.CurrentDialog.Clone();
            }
        }
        #endregion     
    }
}
