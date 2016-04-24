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
    public partial class ScriptMoveCamera : Form
    {
        #region Properties
        public VO_Script_MoveCamera MoveCamera { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScriptMoveCamera()
        {
            InitializeComponent();
            crdCoords.UseStageBackground = true;
            crdCoords.Coords = new Rectangle(new Point(), new Size());
        }
        #endregion

        #region Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Désactive les eventhandlers
            chkMoveImmediately.CheckedChanged -= new EventHandler(chkMoveImmediately_CheckedChanged);
            chkMovingSpeed.CheckedChanged -= new EventHandler(chkMovingSpeed_CheckedChanged);

            //Binding
            crdCoords.Coords = new Rectangle(MoveCamera.Coords.Location, new System.Drawing.Size());
            ddpMovingSpeed.Value = MoveCamera.Speed;
            chkMovingSpeed.Checked = !MoveCamera.UseImmediately;
            chkMoveImmediately.Checked = MoveCamera.UseImmediately;
            if (MoveCamera.UseImmediately)
            {
                chkMoveImmediately.Checked = true;
                chkMovingSpeed.Checked = false;
                ddpMovingSpeed.Enabled = false;
            }
            else
            {
                chkMoveImmediately.Checked = false;
                chkMovingSpeed.Checked = true;
                ddpMovingSpeed.Enabled = true;
            }

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
            MoveCamera.Coords = crdCoords.FullCoords;
            MoveCamera.Speed = Convert.ToInt32(ddpMovingSpeed.Value);
            MoveCamera.UseImmediately = chkMoveImmediately.Checked;
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
