using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptAddPlayerAction : Form
    {
        #region Properties 

        public bool IsAdd = true;
        public Guid CharacterId = Guid.Empty;
        public Guid ActionId = Guid.Empty;

        #endregion

        #region Constructor

        public ScriptAddPlayerAction()
        {
            InitializeComponent();
        }

        #endregion

        #region  Methods

        private void OnLoad(object sender, EventArgs e)
        {
            cmbAction.Items.Clear();
            List<VO_Base> ActionList = GameCore.Instance.GetActions();
            foreach (VO_Base CurrentAction in ActionList)
            {
                if (CurrentAction.Id != new Guid(GlobalConstants.ACTION_GO_ID) && CurrentAction.Id != new Guid(GlobalConstants.ACTION_USE_ID))
                {
                    cmbAction.Items.Add(CurrentAction);
                    if (ActionId == CurrentAction.Id)
                        cmbAction.SelectedItem = CurrentAction;
                }
            }
            characterButton1.ResetText();
            if (IsAdd == false)
            {
                characterButton1.CharacterGuid = CharacterId;
                if (CharacterId == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                    rdxCurrentCharacter.Checked = true;
            }
            else if (IsAdd == true)
            {
                rdxCurrentCharacter.Checked = true;
            }
            cmbAction.DisplayMember = "Title";
            cmbAction.ValueMember = "Id";
        }

        private void AddPlayerAction_Ok(object sender, EventArgs e)
        {
            if (rdxCurrentCharacter.Checked == true)
                CharacterId = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            else
                CharacterId = characterButton1.CharacterGuid;
            if (cmbAction.SelectedItem != null)
            {
                VO_Base CurrentAction = (VO_Base)cmbAction.SelectedItem;
                ActionId = CurrentAction.Id;
            }
            else
                ActionId = Guid.Empty;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddPlayerAction_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SelectCharacter_Character(object sender, EventArgs e)
        {
            rdxCurrentCharacter.CheckedChanged -= new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged -= new EventHandler(SelectCharacter_Character);

            if (rdxtoCharacterRadio.Checked == true)
            {
                rdxtoCharacterRadio.Checked = true;
                rdxCurrentCharacter.Checked = false;
                CharacterId = characterButton1.CharacterGuid;
            }
            
            rdxCurrentCharacter.CheckedChanged += new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged += new EventHandler(SelectCharacter_Character);
        }

        private void SelectCharacter_CurrentCharacter(object sender, EventArgs e)
        {
            rdxCurrentCharacter.CheckedChanged -= new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged -= new EventHandler(SelectCharacter_Character);

            if (rdxCurrentCharacter.Checked == true)
            {
                rdxtoCharacterRadio.Checked = false;
                rdxCurrentCharacter.Checked = true;
                CharacterId = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            }

            rdxCurrentCharacter.CheckedChanged += new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged += new EventHandler(SelectCharacter_Character);
        }

        #endregion
    }
}
