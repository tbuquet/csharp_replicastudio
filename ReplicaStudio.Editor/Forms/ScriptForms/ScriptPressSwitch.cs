using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptPressSwitch : Form
    {
        #region Properties
        
        public Boolean IsAdd = true;
        public VO_Script_PressSwitch Switch = new VO_Script_PressSwitch();

        #endregion
        
        public ScriptPressSwitch()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                SelectedTrigger.TriggerGuid = Guid.Empty;
                Switch.Button = Guid.Empty;
                Switch.IsActive = false;
                btnEnabled.Checked = false;
                btnDisabled.Checked = true;
            }
            else
            {
                SelectedTrigger.TriggerGuid = Switch.Button;
                if (Switch.IsActive == true)
                {
                    btnEnabled.Checked = true;
                    btnDisabled.Checked = false;
                }
                else
                {
                    btnEnabled.Checked = false;
                    btnDisabled.Checked = true;
                }
            }
        }

        private void Switch_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Switch_Validation(object sender, EventArgs e)
        {
            Switch.Button = SelectedTrigger.TriggerGuid;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Switch_Enabled(object sender, EventArgs e)
        {
            btnDisabled.Checked = false;
            btnEnabled.Checked = true;
            Switch.IsActive = true;
        }

        private void Switch_Disabled(object sender, EventArgs e)
        {
            btnDisabled.Checked = true;
            btnEnabled.Checked = false;
            Switch.IsActive = false;
        }
    }
}
