using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms
{
    public partial class DialogManager : Form
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        DialogService _Service;

        /// <summary>
        /// Liste de characters
        /// </summary>
        List<VO_Base> _Characters;

        /// <summary>
        /// Message mis en cache pendant le Ressource Manager
        /// </summary>
        VO_Message _MessageInCacheForVoices;

        /// <summary>
        /// Utilisation des persos du stage
        /// </summary>
        bool _UseCurrentStage = false;

        /// <summary>
        /// Type de script
        /// </summary>
        Enums.ScriptType _ScriptType;
        #endregion

        #region Properties
        /// <summary>
        /// Objet Dialogue
        /// </summary>
        public VO_Dialog CurrentDialog
        {
            get;
            set;
        }

        /// <summary>
        /// Modifications annulées
        /// </summary>
        public bool CanceledChanges
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public DialogManager()
        {
            InitializeComponent();
            _Service = new DialogService();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Chargement du dialogue
        /// </summary>
        /// <param name="dialog">VO_Dialog</param>
        public void LoadDialog(VO_Dialog dialog, Enums.ScriptType scriptType)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.grdDialog.SuspendLayout();

            CurrentDialog = dialog.Clone();
            CanceledChanges = false;

            _ScriptType = scriptType;
            if (scriptType == Enums.ScriptType.AnimationEvents ||
                    scriptType == Enums.ScriptType.CharacterEvents ||
                    scriptType == Enums.ScriptType.Events ||
                    scriptType == Enums.ScriptType.StageEvents)
                _UseCurrentStage = true;
            else
                _UseCurrentStage = false;

            //Désactiver les eventhandlers
            grdDialog.CellValueChanged -= new DataGridViewCellEventHandler(grdDialog_CellValueChanged);
            grdDialog.CellClick -= new DataGridViewCellEventHandler(grdDialog_CellClick);

            //Charger les listes
            LoadLists();

            chkShowFaces.Checked = CurrentDialog.UseFaces;

            //Charger la gridview
            grdDialog.Rows.Clear();
            foreach (VO_Message message in CurrentDialog.Messages)
            {
                InsertMessage(message);
            }

            //Activer les eventhandlers
            grdDialog.CellValueChanged += new DataGridViewCellEventHandler(grdDialog_CellValueChanged);
            grdDialog.CellClick += new DataGridViewCellEventHandler(grdDialog_CellClick);

            this.grdDialog.ResumeLayout();
            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Charge les listes des actions et des characters
        /// </summary>
        private void LoadLists()
        {
            //Charger les characters
            _Characters = new List<VO_Base>();
            if (_UseCurrentStage)
                _Characters.Add(new VO_Base(new Guid(), GlobalConstants.UNKNOWN));
            _Characters.Add(new VO_Base(new Guid(GlobalConstants.CURRENT_PLAYER_ID), GlobalConstants.CURRENT_PLAYER));
            if (_UseCurrentStage)
            {
                foreach (VO_StageCharacter character in EditorHelper.Instance.GetCurrentStageInstance().ListCharacters)
                {
                    _Characters.Add(character);
                }
            }
        }

        /// <summary>
        /// Insére un message dans la gridview
        /// </summary>
        /// <param name="message">VO_Message</param>
        private void InsertMessage(VO_Message message)
        {
            DataGridViewRow row = new DataGridViewRow();

            //Id
            DataGridViewTextBoxCell cellId = new DataGridViewTextBoxCell();
            cellId.Value = message.Id;

            //Text
            DataGridViewTextBoxCell cellText = new DataGridViewTextBoxCell();
            cellText.Value = message.Text;
            //Character
            DataGridViewComboBoxCell cellCharacter = new DataGridViewComboBoxCell();
            cellCharacter.DisplayMember = "Title";
            cellCharacter.ValueMember = "Id";
            cellCharacter.DataSource = _Characters;
            cellCharacter.Value = message.Character;

            //Duration
            DataGridViewComboBoxCell cellDuration = new DataGridViewComboBoxCell();
            cellDuration.DisplayMember = "Title";
            cellDuration.ValueMember = "Id";
            cellDuration.DataSource = FormsTools.GetMessageDurationList();
            cellDuration.Value = message.Duration;

            //FontSize
            DataGridViewComboBoxCell cellFontSize = new DataGridViewComboBoxCell();
            cellFontSize.DisplayMember = "Title";
            cellFontSize.ValueMember = "Id";
            cellFontSize.DataSource = FormsTools.GetMessageFontSizeList();
            cellFontSize.Value = message.FontSize;

            //Sound
            DataGridViewButtonCell cellSound = new DataGridViewButtonCell();
            cellSound.Value = GlobalConstants.BUTTON_SOUND;

            row.Cells.Add(cellId);
            row.Cells.Add(cellText);
            row.Cells.Add(cellCharacter);
            row.Cells.Add(cellDuration);
            row.Cells.Add(cellFontSize);
            row.Cells.Add(cellSound);
            grdDialog.Rows.Add(row);
        }
        #endregion

        #region Eventhandlers
        /// <summary>
        /// Click sur annuler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CanceledChanges = true;
            this.Close();
        }

        /// <summary>
        /// Click sur OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Insérer une ligne en dessous de la ligne courante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertBelow_Click(object sender, EventArgs e)
        {
            //Ligne courante
            Guid currentLineId = new Guid();
            if (grdDialog.SelectedRows.Count == 1)
                currentLineId = (Guid)grdDialog.SelectedRows[0].Cells[0].Value;

            //Création du nouveau message
            VO_Message newMessage = _Service.CreateMessage();

            //Réorder
            List<VO_Message> newList = new List<VO_Message>();
            foreach (VO_Message message in CurrentDialog.Messages)
            {
                newList.Add(message);
                if (message.Id == currentLineId)
                    newList.Add(newMessage);
            }
            if (currentLineId == new Guid())
                newList.Add(newMessage);

            //Enregistrement
            CurrentDialog.Messages = newList;

            //Rechargement de la liste
            LoadDialog(CurrentDialog, _ScriptType);

            //Selection de la nouvelle ligne
            foreach (DataGridViewRow row in grdDialog.Rows)
            {
                Guid foreachRowGuid = (Guid)row.Cells[0].Value;
                if (foreachRowGuid == newMessage.Id)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        /// <summary>
        /// Insérer une ligne au dessus de ligne courante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertAbove_Click(object sender, EventArgs e)
        {
            //Ligne courante
            Guid currentLineId = new Guid();
            if (grdDialog.SelectedRows.Count == 1)
                currentLineId = (Guid)grdDialog.SelectedRows[0].Cells[0].Value;

            //Création du nouveau message
            VO_Message newMessage = _Service.CreateMessage();

            //Réorder
            List<VO_Message> newList = new List<VO_Message>();
            foreach (VO_Message message in CurrentDialog.Messages)
            {
                if (message.Id == currentLineId)
                    newList.Add(newMessage);
                newList.Add(message);
            }
            if (currentLineId == new Guid())
                newList.Add(newMessage);

            //Enregistrement
            CurrentDialog.Messages = newList;

            //Rechargement de la liste
            LoadDialog(CurrentDialog, _ScriptType);

            //Selection de la nouvelle ligne
            foreach (DataGridViewRow row in grdDialog.Rows)
            {
                Guid foreachRowGuid = (Guid)row.Cells[0].Value;
                if (foreachRowGuid == newMessage.Id)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        /// <summary>
        /// Click sur Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            //Ligne courante
            if (grdDialog.SelectedRows.Count == 1)
            {
                //Ligne à descendre
                Guid currentLineId = (Guid)grdDialog.SelectedRows[0].Cells[0].Value;
                VO_Message messageCached = null;

                //Réorder
                List<VO_Message> newList = new List<VO_Message>();
                foreach (VO_Message message in CurrentDialog.Messages)
                {
                    if (message.Id == currentLineId)
                        messageCached = message;
                    else
                    {
                        newList.Add(message);
                        if (messageCached != null)
                        {
                            newList.Add(messageCached);
                            messageCached = null;
                        }
                    }
                }
                if (messageCached != null)
                {
                    newList.Add(messageCached);
                    messageCached = null;
                }

                //Enregistrement
                CurrentDialog.Messages = newList;

                //Rechargement de la liste
                LoadDialog(CurrentDialog, _ScriptType);

                //Selection de l'ancienne ligne
                foreach (DataGridViewRow row in grdDialog.Rows)
                {
                    Guid foreachRowGuid = (Guid)row.Cells[0].Value;
                    if (foreachRowGuid == currentLineId)
                    {
                        row.Selected = true;
                    }
                    else
                    {
                        row.Selected = false;
                    }
                }
            }
        }

        /// <summary>
        /// Click sur Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            //Ligne courante
            if (grdDialog.SelectedRows.Count == 1)
            {
                //Ligne à descendre
                Guid currentLineId = (Guid)grdDialog.SelectedRows[0].Cells[0].Value;

                //Réorder
                List<VO_Message> newList = new List<VO_Message>();
                int index = 0;
                foreach (VO_Message message in CurrentDialog.Messages)
                {
                    if (message.Id == currentLineId)
                    {
                        if (index == 0)
                            newList.Insert(0, message);
                        else
                            newList.Insert(index - 1, message);
                    }
                    else
                    {
                        newList.Add(message);
                    }
                    index++;
                }

                //Enregistrement
                CurrentDialog.Messages = newList;

                //Rechargement de la liste
                LoadDialog(CurrentDialog, _ScriptType);

                //Selection de l'ancienne ligne
                foreach (DataGridViewRow row in grdDialog.Rows)
                {
                    Guid foreachRowGuid = (Guid)row.Cells[0].Value;
                    if (foreachRowGuid == currentLineId)
                    {
                        row.Selected = true;
                    }
                    else
                    {
                        row.Selected = false;
                    }
                }
            }
        }

        /// <summary>
        /// Supprimer un message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Ligne courante
            if (grdDialog.SelectedRows.Count == 1)
            {
                //Ligne à descendre
                Guid currentLineId = (Guid)grdDialog.SelectedRows[0].Cells[0].Value;

                //Suppression du message sélectionné
                CurrentDialog.Messages.RemoveAll(p => p.Id == currentLineId);

                //Rechargement de la liste
                LoadDialog(CurrentDialog, _ScriptType);
            }
        }

        /// <summary>
        /// Gestion des erreurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDialog_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdDialog.Rows.Count && e.ColumnIndex == 2)
            {
                Guid id = (Guid)grdDialog.Rows[e.RowIndex].Cells[0].Value;

                VO_Message message = CurrentDialog.Messages.Find(p => p.Id == id);
                grdDialog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = new Guid();
            }
        }

        /// <summary>
        /// Changement de valeur des les cellules.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDialog_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdDialog.Rows.Count)
            {
                Guid id = (Guid)grdDialog.Rows[e.RowIndex].Cells[0].Value;
                VO_Message message = CurrentDialog.Messages.Find(p => p.Id == id);

                if (message != null)
                {
                    switch (e.ColumnIndex)
                    {
                        case 1:
                            message.Text = (string)grdDialog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            break;
                        case 2:
                            message.Character = (Guid)grdDialog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            break;
                        case 3:
                            message.Duration = (int)grdDialog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            break;
                        case 4:
                            message.FontSize = (int)grdDialog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Click sur cellule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDialog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < grdDialog.Rows.Count && e.ColumnIndex == 5)
            {
                Guid id = (Guid)grdDialog.Rows[e.RowIndex].Cells[0].Value;
                _MessageInCacheForVoices = CurrentDialog.Messages.Find(p => p.Id == id).Clone();

                FormsManager.Instance.ResourcesManager.FormClosed += new FormClosedEventHandler(ResourcesManager_FormClosed);
                FormsManager.Instance.ResourcesManager.Filter = GlobalConstants.PROJECT_DIR_VOICES;
                FormsManager.Instance.ResourcesManager.SelectedFilePath = _MessageInCacheForVoices.Voice;
                FormsManager.Instance.ResourcesManager.ShowDialog(this);
            }
        }

        /// <summary>
        /// Fermeture Ressource Manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourcesManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsManager.Instance.ResourcesManager.FormClosed -= new FormClosedEventHandler(ResourcesManager_FormClosed);
            _MessageInCacheForVoices.Voice = FormsManager.Instance.ResourcesManager.SelectedFilePath;
            CurrentDialog.Messages.Find(p => p.Id == _MessageInCacheForVoices.Id).Voice = _MessageInCacheForVoices.Voice;
            FormsManager.Instance.ResourcesManager.SelectedFilePath = null;
            _MessageInCacheForVoices = null;
        }

        /// <summary>
        /// Show faces
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFaces_CheckedChanged(object sender, EventArgs e)
        {
            CurrentDialog.UseFaces = chkShowFaces.Checked;
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
