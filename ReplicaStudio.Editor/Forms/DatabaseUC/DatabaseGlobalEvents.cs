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

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Global Events de la database
    /// </summary>
    public partial class DatabaseGlobalEvents : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        GlobalEventService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Evenement global actuellement chargée
        /// </summary>
        VO_GlobalEvent CurrentGlobalEvent;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseGlobalEvents()
        {
            InitializeComponent();
            _Service = new GlobalEventService();
            ListGlobalEvents.Title = Culture.Language.DatabaseRessources.DatabaseGlobalEvents;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBGlobalEvents()
        {
            CurrentGlobalEvent = null;
            ProvisionList();
            if (ListGlobalEvents.DataSource.Count > 0)
            {
                Guid firstAction = ListGlobalEvents.DataSource[0].Id;
                ListGlobalEvents.SelectItem(firstAction);
                LoadGlobalEvent(firstAction);
            }
            else
                ListGlobalEvents_ListIsEmpty(this, new EventArgs());
        }

        public void EnableTreeNodeColor()
        {
            this.ScriptManager.EnableDrawManager();
        }

        public void DisableTreeNodeColor()
        {
            this.ScriptManager.DisableDrawManager();
        }

        /// <summary>
        /// Charge la liste d'evenements globaux
        /// </summary>
        private void ProvisionList()
        {
            ListGlobalEvents.DataSource = _Service.ProvisionList();
            ListGlobalEvents.LoadList();
        }

        /// <summary>
        /// Charge un évènement global
        /// </summary>
        /// <param name="value"></param>
        private void LoadGlobalEvent(Guid value)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Code de chargement
            CurrentGlobalEvent = GameCore.Instance.GetGlobalEventById(value);

            //Afficher les groupes
            grpInformations.Visible = true;
            grpScript.Visible = true;

            //Désactiver events
            txtName.LostFocus -= new EventHandler(txtName_TextChanged);
            trgTrigger.ValueChanged -= new EventHandler(trgTrigger_ValueChanged);
            chkTrigger.CheckedChanged -= new EventHandler(chkTrigger_CheckedChanged);

            //Bind des infos dans les contrôles
            txtName.Text = CurrentGlobalEvent.Title;
            trgTrigger.TriggerGuid = CurrentGlobalEvent.Trigger;
            ScriptManager.LoadScript(CurrentGlobalEvent.Script);
            chkTrigger.Checked = CurrentGlobalEvent.UseTrigger;
            if (chkTrigger.Checked)
                trgTrigger.Enabled = true;
            else
                trgTrigger.Enabled = false;

            //Activer les events
            txtName.LostFocus += new EventHandler(txtName_TextChanged);
            trgTrigger.ValueChanged += new EventHandler(trgTrigger_ValueChanged);
            chkTrigger.CheckedChanged += new EventHandler(chkTrigger_CheckedChanged);

            Cursor.Current = DefaultCursor;
        }
        #endregion

        #region Eventhandlers
        /// <summary>
        /// Code ajouté lors de la sélection d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListGlobalEvents_ItemChosen(object sender, EventArgs e)
        {
            LoadGlobalEvent(ListGlobalEvents.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListGlobalEvents_ItemToCreate(object sender, EventArgs e)
        {
            VO_GlobalEvent newItem = _Service.CreateGlobalEvent();
            ListGlobalEvents.AddItem(newItem.Id, newItem.Title);
            LoadGlobalEvent(newItem.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'une action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListGlobalEvents_ItemToDelete(object sender, EventArgs e)
        {
            CurrentGlobalEvent.Delete();
            CurrentGlobalEvent = null;
        }

        /// <summary>
        /// Code ajouté lorsque la liste est vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListGlobalEvents_ListIsEmpty(object sender, EventArgs e)
        {
            grpInformations.Visible = false;
            grpScript.Visible = false;
        }

        /// <summary>
        /// Le titre a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListGlobalEvents.ChangeItemName(CurrentGlobalEvent.Id, txtName.Text))
            {
                CurrentGlobalEvent.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentGlobalEvent.Title;
                MessageBox.Show(Errors.ERROR_UNIQUE_TITLE, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Code ajouté lors de la modification d'un bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trgTrigger_ValueChanged(object sender, EventArgs e)
        {
            CurrentGlobalEvent.Trigger = trgTrigger.TriggerGuid;
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTrigger_CheckedChanged(object sender, EventArgs e)
        {
            CurrentGlobalEvent.UseTrigger = chkTrigger.Checked;

            if (chkTrigger.Checked)
                trgTrigger.Enabled = true;
            else
                trgTrigger.Enabled = false;
        }
        #endregion
    }
}
