﻿using System;
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
    public partial class ScriptCharacterAnimationFrequency : Form
    {
        public Boolean IsAdd = true;

        public int Frequency;
        public Guid CharacterId { get; set; }
        public Enums.CharacterAnimationType AnimationType;

        public ScriptCharacterAnimationFrequency()
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

            cmbAnimation.DataSource = EnumHelper.ToList(typeof(Enums.CharacterAnimationType));
            cmbAnimation.DisplayMember = "Value";
            cmbAnimation.ValueMember = "Key";

            if (IsAdd == true)
            {
                CharacterId = Guid.Empty;
                AnimationType = Enums.CharacterAnimationType.Standing;
                cmbAnimation.SelectedIndex = 0;
                trkFrequency.Value = GlobalConstants.PLAYER_NORMAL_SPEED; ;
                Frequency = GlobalConstants.PLAYER_NORMAL_SPEED; ;
                prctFrequency.Text = Convert.ToString(GlobalConstants.PLAYER_NORMAL_SPEED) + "%";
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
                cmbAnimation.SelectedValue = AnimationType;
                trkFrequency.Value = Frequency;
                prctFrequency.Text = Convert.ToString(Frequency) + "%";
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
                AnimationType = (Enums.CharacterAnimationType)((cmbAnimation.SelectedItem.GetType()).GetProperty("Key")).GetValue(cmbAnimation.SelectedItem, null);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnReset(object sender, EventArgs e)
        {
            trkFrequency.Value = GlobalConstants.PLAYER_NORMAL_SPEED; ;
            Frequency = GlobalConstants.PLAYER_NORMAL_SPEED; ;
            prctFrequency.Text = Convert.ToString(GlobalConstants.PLAYER_NORMAL_SPEED) + "%";
        }

        private void trkFrequencyChange(object sender, EventArgs e)
        {
            Frequency = trkFrequency.Value;
            prctFrequency.Text = Convert.ToString(trkFrequency.Value) + "%";
        }
    }
}
