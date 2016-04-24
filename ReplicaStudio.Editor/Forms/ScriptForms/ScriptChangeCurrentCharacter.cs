using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptChangeCurrentCharacter : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public VO_Script_ChangeCurrentCharacter Character = new VO_Script_ChangeCurrentCharacter();

        #endregion

        public ScriptChangeCurrentCharacter()
        {
            InitializeComponent();
            crdCoords.UseStages = true;
            crdCoords.Coords = new Rectangle(new Point(), new Size());
            crdCoords.FullCoords = new VO_Coords();
        }

        private void ScriptChangeCurrentCharacter_Load(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                chgCharacter.CharacterGuid = Guid.Empty;
                chkCurrent.Checked = true;
                chkCoords.Checked = false;
                crdCoords.Enabled = false;
                Character = new VO_Script_ChangeCurrentCharacter();
            }
            else
            {
                chgCharacter.CharacterGuid = Character.Character;
                chkCoords.Checked = !Character.UseOldCoords;
                chkCurrent.Checked = Character.UseOldCoords;
                crdCoords.FullCoords = Character.Coords;
                crdCoords.Coords = new Rectangle(Character.Coords.Location, new Size());
            }
        }

        private void ChangeCurrentCharacter_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChangeCurrentCharacter_Ok(object sender, EventArgs e)
        {
            Character.Character = chgCharacter.CharacterGuid;
            Character.UseOldCoords = chkCurrent.Checked;
            Character.Coords = crdCoords.FullCoords;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chkCoords_CheckedChanged(object sender, EventArgs e)
        {
            chkCoords.CheckedChanged -= new EventHandler(chkCoords_CheckedChanged);
            chkCurrent.CheckedChanged -= new EventHandler(chkCurrent_CheckedChanged);

            chkCurrent.Checked = false;
            chkCoords.Checked = true;
            crdCoords.Enabled = true;

            chkCoords.CheckedChanged += new EventHandler(chkCoords_CheckedChanged);
            chkCurrent.CheckedChanged += new EventHandler(chkCurrent_CheckedChanged);
        }

        private void chkCurrent_CheckedChanged(object sender, EventArgs e)
        {
            chkCoords.CheckedChanged -= new EventHandler(chkCoords_CheckedChanged);
            chkCurrent.CheckedChanged -= new EventHandler(chkCurrent_CheckedChanged);

            chkCoords.Checked = false;
            crdCoords.Enabled = false;
            chkCurrent.Checked = true;

            chkCoords.CheckedChanged += new EventHandler(chkCoords_CheckedChanged);
            chkCurrent.CheckedChanged += new EventHandler(chkCurrent_CheckedChanged);
        }
    }
}
