using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptWait : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public VO_IntValue WaitTime = new VO_IntValue();

        #endregion

        public ScriptWait()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                nbSeconds.VariableGuid = new VO_IntValue();
                nbSeconds.VariableGuid.IntValue = EditorConstants.WAIT_MIN_VALUE;
            }
            else
            {
                nbSeconds.VariableGuid = WaitTime;
            }
            // Implement EventHandler
        }

        private void ScriptWait_Ok(object sender, EventArgs e)
        {
            WaitTime = nbSeconds.VariableGuid;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ScriptWait_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
