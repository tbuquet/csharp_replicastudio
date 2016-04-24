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
    public partial class ScriptCameraFocusOnAnimation : Form
    {
        #region Members
        private List<VO_StageAnimation> _AnimationList = new List<VO_StageAnimation>();
        #endregion

        #region Properties
        public VO_Script_FocusOnAnimation FocusOnAnimation { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScriptCameraFocusOnAnimation()
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
            _AnimationList = new List<VO_StageAnimation>();
            foreach (VO_Layer layer in EditorHelper.Instance.GetCurrentStageInstance().ListLayers)
            {
                foreach (VO_StageAnimation animation in layer.ListAnimations)
                {
                    _AnimationList.Add(animation);
                }
            }
            cbxListCharacter.DataSource = _AnimationList;
            cbxListCharacter.DisplayMember = "Title";
            cbxListCharacter.ValueMember = "Id";
            cbxListCharacter.Enabled = true;
            if (FocusOnAnimation.Animation == Guid.Empty)
            {
                if (cbxListCharacter.Items.Count <= 0)
                {
                    cbxListCharacter.Enabled = false;
                    return;
                }
                cbxListCharacter.SelectedIndex = 0;
            }
            else
                cbxListCharacter.SelectedValue = FocusOnAnimation.Animation;

            //Binding
            ddpMovingSpeed.Value = FocusOnAnimation.Speed;
            chkMovingSpeed.Checked = !FocusOnAnimation.UseImmediately;
            chkMoveImmediately.Checked = FocusOnAnimation.UseImmediately;

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
                FocusOnAnimation.Animation = (Guid)cbxListCharacter.SelectedValue;
                FocusOnAnimation.Speed = Convert.ToInt32(ddpMovingSpeed.Value);
                FocusOnAnimation.UseImmediately = chkMoveImmediately.Checked;
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
