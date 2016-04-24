using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptFreePlayerAnimation : Form
    {
        public Boolean IsAdd = true;

        public bool AllAnimation;
        public Guid CharacterId { get; set; }
        public Enums.CharacterAnimationType AnimationType;

        public ScriptFreePlayerAnimation()
        {
            InitializeComponent();
            CharacterId = Guid.Empty;
            AllAnimation = false;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            cmbAnimation.DataSource = EnumHelper.ToList(typeof(Enums.CharacterAnimationType));
            cmbAnimation.DisplayMember = "Value";
            cmbAnimation.ValueMember = "Key";

            if (IsAdd == true)
            {
                CharacterId = Guid.Empty;
                characterButton1.ResetText();
                characterButton1.CharacterGuid = CharacterId;
                AnimationType = Enums.CharacterAnimationType.Standing;
                cmbAnimation.SelectedIndex = 0;
                AllAnimation = false;
                chxAllAnimations.Checked = false;
                rdxCurrentCharacter.Checked = true;
            }
            else
            {
                characterButton1.CharacterGuid = CharacterId;
                cmbAnimation.SelectedValue = AnimationType;
                chxAllAnimations.Checked = AllAnimation;
                if (CharacterId == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                    rdxCurrentCharacter.Checked = true;
            }
        }

        private void btnValidation(object sender, EventArgs e)
        {
            if (rdxCurrentCharacter.Checked == true)
                CharacterId = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            else
                CharacterId = characterButton1.CharacterGuid;
            AnimationType = (Enums.CharacterAnimationType)((cmbAnimation.SelectedItem.GetType()).GetProperty("Key")).GetValue(cmbAnimation.SelectedItem, null);
            AllAnimation = chxAllAnimations.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel(object sender, EventArgs e)
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
