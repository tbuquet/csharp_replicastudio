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
    public partial class ScriptChangePlayerAnimation : Form
    {
        public Boolean IsAdd = true;

        public Guid CharacterId { get; set; }
        public Enums.CharacterAnimationType AnimationType;
        public Guid CharacterAnimationType { get; set; }
        public bool Loop { get; set; }

        public ScriptChangePlayerAnimation()
        {
            InitializeComponent();
            CharacterId = Guid.Empty;
            CharacterAnimationType = Guid.Empty;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            cmbTypeAnimation.Items.Clear();

            cmbAnimation.DataSource = EnumHelper.ToList(typeof(Enums.CharacterAnimationType));
            cmbAnimation.DisplayMember = "Value";
            cmbAnimation.ValueMember = "Key";

            if (IsAdd == true)
            {
                CharacterId = Guid.Empty;
                characterButton1.ResetText();
                characterButton1.CharacterGuid = CharacterId;
                AnimationType = Enums.CharacterAnimationType.Standing;
                chkLoop.Checked = true;
                Loop = true;
                cmbAnimation.SelectedIndex = 0;
            }
            else
            {
                characterButton1.CharacterGuid = CharacterId;
                chkLoop.Checked = Loop;
                cmbAnimation.SelectedValue = AnimationType;
                ChangePlayerAnimation(sender, e);
            }
        }

        private void btnValidation(object sender, EventArgs e)
        {
            CharacterId = characterButton1.CharacterGuid;
            AnimationType = (Enums.CharacterAnimationType)((cmbAnimation.SelectedItem.GetType()).GetProperty("Key")).GetValue(cmbAnimation.SelectedItem, null);
            if (cmbTypeAnimation.SelectedItem != null)
            {
                VO_Base CurrentAnimation = (VO_Base) cmbTypeAnimation.SelectedItem;
                CharacterAnimationType = CurrentAnimation.Id;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChangePlayerAnimation(object sender, EventArgs e)
        {
            VO_Base SelectedAnim = null;
            cmbTypeAnimation.Items.Clear();
            if (characterButton1.CharacterGuid != Guid.Empty)
            {
                VO_PlayableCharacter playableCharacter = GameCore.Instance.GetPlayableCharacterById(characterButton1.CharacterGuid);

                List<VO_Base> AnimationList = GameCore.Instance.GetCharAnimations(playableCharacter.CharacterId);
                cmbTypeAnimation.DisplayMember = "Title";
                cmbTypeAnimation.ValueMember = "Id";
                foreach (VO_Base CurrentAnimation in AnimationList)
                {
                    cmbTypeAnimation.Items.Add(CurrentAnimation);
                    cmbTypeAnimation.SelectedIndex = 0;
                    if (CurrentAnimation.Id == CharacterAnimationType)
                        SelectedAnim = CurrentAnimation;
                }
                if (SelectedAnim != null)
                    cmbTypeAnimation.SelectedItem = SelectedAnim;
            }
        }

        private void chkLoop_CheckedChanged(object sender, EventArgs e)
        {
            Loop = chkLoop.Checked;
        }
    }
}
