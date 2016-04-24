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
    public partial class ScriptMusicFrequency : Form
    {
        public Boolean IsAdd = true;

        public int Frequency;

        public ScriptMusicFrequency()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                trkFrequency.Value = GlobalConstants.MUSIC_NORMAL_SPEED; ;
                Frequency = GlobalConstants.MUSIC_NORMAL_SPEED; ;
                prctFrequency.Text = Convert.ToString(GlobalConstants.MUSIC_NORMAL_SPEED) + "%";
            }
            else
            {
                trkFrequency.Value = Frequency;
                prctFrequency.Text = Convert.ToString(Frequency) + "%";
            }
        }

        private void btnValidation(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnReset(object sender, EventArgs e)
        {
            trkFrequency.Value = GlobalConstants.MUSIC_NORMAL_SPEED; ;
            Frequency = GlobalConstants.MUSIC_NORMAL_SPEED; ;
            prctFrequency.Text = Convert.ToString(GlobalConstants.MUSIC_NORMAL_SPEED) + "%";
        }

        private void trkFrequencyChange(object sender, EventArgs e)
        {
            Frequency = trkFrequency.Value;
            prctFrequency.Text = Convert.ToString(trkFrequency.Value) + "%";
        }
    }
}
