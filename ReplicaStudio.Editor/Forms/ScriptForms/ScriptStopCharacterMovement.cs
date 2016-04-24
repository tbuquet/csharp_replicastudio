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

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptStopCharacterMovement : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public List<VO_StageCharacter> CharacterList = new List<VO_StageCharacter>();
        public VO_StageCharacter CurrentCharacter;

        #endregion

        public ScriptStopCharacterMovement()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            CharacterList = new List<VO_StageCharacter>();
            foreach (VO_StageCharacter character in EditorHelper.Instance.GetCurrentStageInstance().ListCharacters)
            {
                CharacterList.Add(character);
            }
            cbxListCharacter.DataSource = CharacterList;
            cbxListCharacter.DisplayMember = "Title";
            cbxListCharacter.ValueMember = "Id";

            cbxListCharacter.Enabled = true;

            if (IsAdd == true)
            {
                if (cbxListCharacter.Items.Count <= 0)
                {
                    cbxListCharacter.Enabled = false;
                    return;
                }
                cbxListCharacter.SelectedIndex = 0;
            }
            else if (IsAdd == false)
            {
                if (CurrentCharacter != null)
                    cbxListCharacter.SelectedValue = CurrentCharacter.Id;
            }
        }

        private void ScriptCharacterMovement_Ok(object sender, EventArgs e)
        {
            if (cbxListCharacter.Items.Count <= 0)
                MessageBox.Show(Culture.Language.Notifications.NO_CHARACTER_SELECTION);
            else
            {
                CurrentCharacter = (VO_StageCharacter)cbxListCharacter.SelectedItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ScriptCharacterMovement_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
