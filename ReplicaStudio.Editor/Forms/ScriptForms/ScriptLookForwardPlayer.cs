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
    public partial class ScriptLookForwardPlayer : Form
    {
        public Boolean IsAdd = true;

        public Guid CharacterId { get; set; }

        public ScriptLookForwardPlayer()
        {
            InitializeComponent();
            CharacterId = Guid.Empty;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            cmbCharacterList.Items.Clear();
            VO_StageCharacter CurrentCharacter = null;
            foreach (VO_StageCharacter character in EditorHelper.Instance.GetCurrentStageInstance().ListCharacters)
            {
                cmbCharacterList.Items.Add(character);
                if (CharacterId == character.Id)
                    CurrentCharacter = character;
            }
            cmbCharacterList.DisplayMember = "Title";
            cmbCharacterList.ValueMember = "Id";

            cmbCharacterList.Enabled = true;

            if (IsAdd == true)
            {
                CharacterId = Guid.Empty;
                if (cmbCharacterList.Items.Count <= 0)
                {
                    cmbCharacterList.Enabled = false;
                    return;
                }
                cmbCharacterList.SelectedIndex = 0;
            }
            else
            {
                if (CurrentCharacter != null)
                    cmbCharacterList.SelectedItem = CurrentCharacter;
            }
        }

        private void btnValidation(object sender, EventArgs e)
        {
            if (cmbCharacterList.Items.Count <= 0)
                MessageBox.Show(Culture.Language.Notifications.NO_CHARACTER_SELECTION);
            else
            {
                VO_StageCharacter CurrentStageCharacter = (VO_StageCharacter)cmbCharacterList.SelectedItem;

                CharacterId = CurrentStageCharacter.Id;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
