using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptItem : Form
    {
        #region Properties

        // Business Related
        public Boolean IsAdd = true;
        public Guid CharacterGuid = Guid.Empty;
        public Guid ItemGuid = Guid.Empty;

        #endregion

        #region Constructor
        
        public ScriptItem()
        {
            InitializeComponent();
        }
        
        #endregion

        #region EventHandlers

        private void OnLoad(object sender, EventArgs e)
        {
            rdxtoCharacterRadio.Checked = false;
            rdxCurrentCharacter.Checked = true;
            if (IsAdd == true)
            {
                ItemGuid = Guid.Empty;
                CharacterGuid = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            }
            itemButton1.ItemGuid = ItemGuid;
            characterButton1.CharacterGuid = CharacterGuid;
            if (CharacterGuid == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
            {
                rdxCurrentCharacter.Checked = true;
                rdxtoCharacterRadio.Checked = false;
            }
            else
            {
                rdxCurrentCharacter.Checked = false;
                rdxtoCharacterRadio.Checked = true;
            }
        }

        private void ScriptItem_Validation(object sender, EventArgs e)
        {
            if (rdxtoCharacterRadio.Checked == true)
                CharacterGuid = characterButton1.CharacterGuid;
            else
                CharacterGuid = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
            ItemGuid = itemButton1.ItemGuid;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ScriptItem_Cancel(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void ScriptItem_ToCharSelected(object sender, EventArgs e)
        {
            rdxCurrentCharacter.Checked = false;
            rdxtoCharacterRadio.Checked = true;
            characterButton1.Enabled = true;
            CharacterGuid = characterButton1.CharacterGuid;
        }

        private void ScriptItem_ToCurrentCharSelected(object sender, EventArgs e)
        {
            rdxCurrentCharacter.Checked = true;
            rdxtoCharacterRadio.Checked = false;
            characterButton1.Enabled = false;
            CharacterGuid = new Guid(GlobalConstants.CURRENT_PLAYER_ID);
        }

        #endregion
    }
}
