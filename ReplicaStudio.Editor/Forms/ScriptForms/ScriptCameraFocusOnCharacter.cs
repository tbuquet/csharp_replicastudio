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
    public partial class ScriptCameraFocusOnCharacter : Form
    {
        #region Members
        private List<VO_StageCharacter> _CharacterList = new List<VO_StageCharacter>();
        #endregion

        #region Properties
        public VO_Script_FocusOnCharacter FocusOnCharacter { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScriptCameraFocusOnCharacter()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Désactive les eventhandlers
            chkMoveImmediately.CheckedChanged += new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged += new EventHandler(chkMovingSpeed_CheckedChanged);

            //Chargement
            _CharacterList = new List<VO_StageCharacter>();
            foreach (VO_StageCharacter character in EditorHelper.Instance.GetCurrentStageInstance().ListCharacters)
            {
                _CharacterList.Add(character);
            }
            cbxListCharacter.DataSource = _CharacterList;
            cbxListCharacter.DisplayMember = "Title";
            cbxListCharacter.ValueMember = "Id";
            cbxListCharacter.Enabled = true;
            if (FocusOnCharacter.Character == Guid.Empty)
            {
                if (cbxListCharacter.Items.Count <= 0)
                {
                    cbxListCharacter.Enabled = false;
                    return;
                }
                cbxListCharacter.SelectedIndex = 0;
            }
            else
                cbxListCharacter.SelectedValue = FocusOnCharacter.Character;

            //Binding
            ddpMovingSpeed.Value = FocusOnCharacter.Speed;
            chkMovingSpeed.Checked = !FocusOnCharacter.UseImmediately;
            chkMoveImmediately.Checked = FocusOnCharacter.UseImmediately;

            //Réactive les eventhandlers
            chkMoveImmediately.CheckedChanged += new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged += new EventHandler(chkMovingSpeed_CheckedChanged);
        }

        void chkMovingSpeed_CheckedChanged(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            chkMoveImmediately.CheckedChanged -= new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged -= new EventHandler(chkMovingSpeed_CheckedChanged);

            chkMoveImmediately.Checked = false;
            chkMovingSpeed.Checked = true;
            ddpMovingSpeed.Enabled = true;

            //Réactive les eventhandlers
            chkMoveImmediately.CheckedChanged += new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged += new EventHandler(chkMovingSpeed_CheckedChanged);
        }

        void chkMoveImmediately_CheckedChanged(object sender, EventArgs e)
        {
            //Désactive les eventhandlers
            chkMoveImmediately.CheckedChanged -= new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged -= new EventHandler(chkMovingSpeed_CheckedChanged);

            chkMoveImmediately.Checked = true;
            chkMovingSpeed.Checked = false;
            ddpMovingSpeed.Enabled = false;

            //Réactive les eventhandlers
            chkMoveImmediately.CheckedChanged += new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged += new EventHandler(chkMovingSpeed_CheckedChanged);
        }
        #endregion

        #region Eventhandlers
        private void ValidationButton_Click(object sender, EventArgs e)
        {
            if (cbxListCharacter.Items.Count <= 0)
                MessageBox.Show(Culture.Language.Notifications.NO_CHARACTER_SELECTION);
            else
            {
                FocusOnCharacter.Character = (Guid)cbxListCharacter.SelectedValue;
                FocusOnCharacter.Speed = Convert.ToInt32(ddpMovingSpeed.Value);
                FocusOnCharacter.UseImmediately = chkMoveImmediately.Checked;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
