using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptChangeCharacterDirection : Form
    {
        #region Properties
        public Boolean IsAdd = true;
        public Enums.Movement Direction { get; set; }
        public Guid CurrentCharacterId { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ScriptChangeCharacterDirection()
        {
            InitializeComponent();
            CurrentCharacterId = Guid.Empty;
        }
        #endregion

        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsAdd == true)
            {
                CurrentCharacterId = Guid.Empty;
            }

            ddpMovements.SelectedIndexChanged += new EventHandler(ddpMovements_SelectedIndexChanged);

            //Character List
            VO_Stage CurrentStage = GameCore.Instance.GetStageById(EditorHelper.Instance.CurrentStage);
            cmbCharacterSelection.Items.Clear();
            cmbCharacterSelection.DisplayMember = "Title";
            cmbCharacterSelection.ValueMember = "Id";

            cmbCharacterSelection.Enabled = true;

            foreach (VO_StageCharacter item in CurrentStage.ListCharacters)
            {
                cmbCharacterSelection.Items.Add(item);
                cmbCharacterSelection.SelectedItem = item;
            }

            if (cmbCharacterSelection.Items.Count <= 0)
            {
                cmbCharacterSelection.Enabled = false;
                return;
            }

            if (IsAdd == false)
            {
                foreach (VO_StageCharacter CurrentCharacter in CurrentStage.ListCharacters)
                {
                    if (CurrentCharacter.Id == CurrentCharacterId)
                        cmbCharacterSelection.SelectedItem = CurrentCharacter;
                }
            }

            // Movement List
            List<VO_ListItem> list = FormsTools.GetMovementsList();
            ddpMovements.Items.Clear();
            ddpMovements.DisplayMember = "Title";
            ddpMovements.ValueMember = "Id";
            int i = 0;
            foreach (VO_ListItem item in list)
            {
                ddpMovements.Items.Add(item);
                if (item.Id == (int)Direction)
                    ddpMovements.SelectedItem = item;
                i++;
            }

            ddpMovements.SelectedIndexChanged += new EventHandler(ddpMovements_SelectedIndexChanged);
        }
        #endregion

        #region EventHandlers
        private void ddpMovements_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddpMovements.SelectedItem != null)
                Direction = (Enums.Movement)((VO_ListItem)ddpMovements.SelectedItem).Id;
        }

        private void ValidationButton_Click(object sender, EventArgs e)
        {
            if (cmbCharacterSelection.Items.Count <= 0)
                MessageBox.Show(Culture.Language.Notifications.NO_CHARACTER_SELECTION);
            else
            {
                VO_StageCharacter CtrlCharacter = (VO_StageCharacter)cmbCharacterSelection.SelectedItem;
                CurrentCharacterId = CtrlCharacter.Id;
                VO_ListItem CurrentMovement = (VO_ListItem)ddpMovements.SelectedItem;
                Direction = (Enums.Movement)CurrentMovement.Id;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
