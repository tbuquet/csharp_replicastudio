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
    public partial class ScriptRandom : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public Guid VariableId { get; set; }
        public VO_IntValue MinValue { get; set; }
        public VO_IntValue MaxValue { get; set; }

        #endregion

        public ScriptRandom()
        {
            InitializeComponent();
        }

        private void ScriptRandom_Load(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                ctrlVariable.VariableGuid = Guid.Empty;
                ctrlMinValue.VariableGuid = new VO_IntValue();
                ctrlMaxValue.VariableGuid = new VO_IntValue();
            }
            else
            {
                ctrlVariable.VariableGuid = VariableId;
                ctrlMaxValue.VariableGuid = MaxValue;
                ctrlMinValue.VariableGuid = MinValue;
            }
        }

        private void ScriptRandom_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ScriptRandom_Ok(object sender, EventArgs e)
        {
            MinValue = ctrlMinValue.VariableGuid;
            MaxValue = ctrlMaxValue.VariableGuid;
            VariableId = ctrlVariable.VariableGuid;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
