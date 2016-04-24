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
    public partial class ScriptMoveCharacter : Form
    {
        #region Members
        private List<VO_StageCharacter> _CharacterList = new List<VO_StageCharacter>();
        #endregion

        #region Properties
        public VO_Script_MoveCharacter MoveCharacter { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScriptMoveCharacter()
        {
            InitializeComponent();
            crdCoords.UseStages = true;
            crdCoords.Coords = new Rectangle(new Point(), new Size());
            crdCoords.FullCoords = new VO_Coords();
        }
        #endregion

        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Chargement
            _CharacterList = new List<VO_StageCharacter>();
            foreach (VO_StageCharacter character in EditorHelper.Instance.GetCurrentStageInstance().ListCharacters)
            {
                _CharacterList.Add(character);
            }
            cbxListCharacter.DataSource = _CharacterList;
            cbxListCharacter.DisplayMember = "Title";
            cbxListCharacter.ValueMember = "Id";
            if (MoveCharacter.Character == Guid.Empty)
            {
                if (_CharacterList.Count <= 0)
                    cbxListCharacter.Enabled = false;
                else
                {
                    cbxListCharacter.Enabled = true;
                    cbxListCharacter.SelectedIndex = 0;
                }
            }
            else
                cbxListCharacter.SelectedValue = MoveCharacter.Character;

            //Binding
            crdCoords.Coords = new Rectangle(MoveCharacter.Coords.Location, new System.Drawing.Size());
            crdCoords.FullCoords = MoveCharacter.Coords;
        }
        #endregion

        #region EventHandlers
        private void ValidationButton_Click(object sender, EventArgs e)
        {
            if (cbxListCharacter.SelectedValue == null)
            {
                MessageBox.Show(Culture.Language.Notifications.NO_CHARACTER_SELECTION);
                return;
            }
            MoveCharacter.Character = (Guid)cbxListCharacter.SelectedValue;
            MoveCharacter.Coords = crdCoords.FullCoords;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
