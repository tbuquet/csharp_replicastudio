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
    public partial class ScriptChoice : Form
    {
        #region Properties
        /// <summary>
        /// Objet Dialogue
        /// </summary>
        public VO_Script_ChoiceMessage ChoiceMessage { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScriptChoice()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadScript(ChoiceMessage);
        }

        /// <summary>
        /// Initialise le script
        /// </summary>
        public void InitNewScript()
        {
            ChoiceMessage.Choices = new List<VO_LineChoices>();
        }

        /// <summary>
        /// Chargement du script
        /// </summary>
        /// <param name="choiceMessage"></param>
        public void LoadScript(VO_Script_ChoiceMessage choiceMessage)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.grdDialog.SuspendLayout();

            //Désactiver les eventhandlers
            grdDialog.CellValueChanged -= new DataGridViewCellEventHandler(grdDialog_CellValueChanged);

            //Charger la gridview
            grdDialog.Rows.Clear();
            foreach (VO_LineChoices message in ChoiceMessage.Choices)
            {
                InsertMessage(message);
            }

            //Activer les eventhandlers
            grdDialog.CellValueChanged += new DataGridViewCellEventHandler(grdDialog_CellValueChanged);

            this.grdDialog.ResumeLayout();
            Cursor.Current = DefaultCursor;
        }

        /// <summary>
        /// Insére un message dans la gridview
        /// </summary>
        /// <param name="message">VO_Message</param>
        private void InsertMessage(VO_LineChoices message)
        {
            DataGridViewRow row = new DataGridViewRow();

            //Id
            DataGridViewTextBoxCell cellId = new DataGridViewTextBoxCell();
            cellId.Value = message.Id;

            //Text
            DataGridViewTextBoxCell cellText = new DataGridViewTextBoxCell();
            cellText.Value = message.Choice;
            
            row.Cells.Add(cellId);
            row.Cells.Add(cellText);
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
            VO_LineChoices newMessage = new VO_LineChoices();
            newMessage.Id = Guid.NewGuid();
            newMessage.SubLines = new List<VO_Line>();

            //Réorder
            List<VO_LineChoices> newList = new List<VO_LineChoices>();
            foreach (VO_LineChoices message in ChoiceMessage.Choices)
            {
                newList.Add(message);
                if(message.Id == currentLineId)
                    newList.Add(newMessage);
            }
            if(currentLineId == new Guid())
                newList.Add(newMessage);

            //Enregistrement
            ChoiceMessage.Choices = newList;

            //Rechargement de la liste
            LoadScript(ChoiceMessage);

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
            VO_LineChoices newMessage = new VO_LineChoices();
            newMessage.Id = Guid.NewGuid();
            newMessage.SubLines = new List<VO_Line>();

            //Réorder
            List<VO_LineChoices> newList = new List<VO_LineChoices>();
            foreach (VO_LineChoices message in ChoiceMessage.Choices)
            {
                if (message.Id == currentLineId)
                    newList.Add(newMessage);
                newList.Add(message);
            }
            if (currentLineId == new Guid())
                newList.Add(newMessage);

            //Enregistrement
            ChoiceMessage.Choices = newList;

            //Rechargement de la liste
            LoadScript(ChoiceMessage);

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
                VO_LineChoices messageCached = null;

                //Réorder
                List<VO_LineChoices> newList = new List<VO_LineChoices>();
                foreach (VO_LineChoices message in ChoiceMessage.Choices)
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
                ChoiceMessage.Choices = newList;

                //Rechargement de la liste
                LoadScript(ChoiceMessage);

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
                List<VO_LineChoices> newList = new List<VO_LineChoices>();
                int index = 0;
                foreach (VO_LineChoices message in ChoiceMessage.Choices)
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
                ChoiceMessage.Choices = newList;

                //Rechargement de la liste
                LoadScript(ChoiceMessage);

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
                ChoiceMessage.Choices.RemoveAll(p => p.Id == currentLineId);

                //Rechargement de la liste
                LoadScript(ChoiceMessage);
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

                VO_LineChoices message = ChoiceMessage.Choices.Find(p => p.Id == id);
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
                VO_LineChoices message = ChoiceMessage.Choices.Find(p => p.Id == id);

                if (message != null)
                {
                    switch (e.ColumnIndex)
                    {
                        case 1:
                            message.Choice = (string)grdDialog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            break;
                    }
                }
            }
        }
        #endregion  
    }
}
