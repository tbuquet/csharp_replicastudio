using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptChangePlayerHP : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public Guid CharacterId { get; set; }
        public Enums.ChangeOperator Operator { get; set; }
        public VO_IntValue Value { get; set; }

        #endregion

        public ScriptChangePlayerHP()
        {
            InitializeComponent();
            OperatorType.DataSource = typeof(Enums.ChangeOperator).ToList();
            OperatorType.DisplayMember = "Value";
            OperatorType.ValueMember = "Key";
            CharacterId = Guid.Empty;
            Operator = Enums.ChangeOperator.Set;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            characterButton1.ResetText();
            if (IsAdd == true)
            {
                CharacterId = Guid.Empty;
                characterButton1.ResetText();
                characterButton1.CharacterGuid = Guid.Empty;
                VariableNew.Text = String.Empty;
                VariableNew.VariableGuid = new VO_IntValue();
                OperatorType.SelectedIndex = 0;
                rdxCurrentCharacter.Checked = true;
                CharacterId = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            }
            else
            {
                if (CharacterId != new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                    characterButton1.CharacterGuid = CharacterId;
                else
                    characterButton1.CharacterGuid = Guid.Empty;
                if (CharacterId == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                {
                    rdxCurrentCharacter.Checked = true;
                }
                else
                    rdxtoCharacterRadio.Checked = true;
                VariableNew.VariableGuid = Value;
                if (Value.VariableValue == Guid.Empty)
                    VariableNew.Text = Convert.ToString(Value.IntValue);
                else
                    VariableNew.Text = GameCore.Instance.GetVariableById(Value.VariableValue).Title;
                OperatorType.SelectedValue = Operator;
                characterButton1.ResetText();
                characterButton1.CharacterGuid = CharacterId;
            }
        }

        private void ScriptChangePlayerHP_Validation(object sender, EventArgs e)
        {
            if (rdxCurrentCharacter.Checked == true)
                CharacterId = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            else
                CharacterId = characterButton1.CharacterGuid;
            Value = VariableNew.VariableGuid;
            Operator = (Enums.ChangeOperator)((OperatorType.SelectedItem.GetType()).GetProperty("Key")).GetValue(OperatorType.SelectedItem, null);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ScriptChangePlayerHP_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SelectCharacter_Character(object sender, EventArgs e)
        {
            rdxCurrentCharacter.CheckedChanged -= new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged -= new EventHandler(SelectCharacter_Character);

            rdxtoCharacterRadio.Checked = true;
            rdxCurrentCharacter.Checked = false;
            CharacterId = characterButton1.CharacterGuid;

            rdxCurrentCharacter.CheckedChanged += new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged += new EventHandler(SelectCharacter_Character);
        }

        private void SelectCharacter_CurrentCharacter(object sender, EventArgs e)
        {
            rdxCurrentCharacter.CheckedChanged -= new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged -= new EventHandler(SelectCharacter_Character);

            rdxtoCharacterRadio.Checked = false;
            rdxCurrentCharacter.Checked = true;
            CharacterId = new Guid(GlobalConstants.CURRENT_PLAYER_ID);

            rdxCurrentCharacter.CheckedChanged += new EventHandler(SelectCharacter_CurrentCharacter);
            rdxtoCharacterRadio.CheckedChanged += new EventHandler(SelectCharacter_Character);
        }
    }
}
