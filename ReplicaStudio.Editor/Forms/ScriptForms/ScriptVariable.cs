using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptVariable : Form
    {
        #region Properties

        public Boolean IsAdd = true;
        public VO_Script_ChangeVariable CurrentVariable = new VO_Script_ChangeVariable();

        #endregion

        public ScriptVariable()
        {
            InitializeComponent();
            OperatorType.DataSource = typeof(Enums.ChangeOperator).ToList();
            OperatorType.DisplayMember = "Value";
            OperatorType.ValueMember = "Key";
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAdd == true)
            {
                VariableNew.Text = String.Empty;
                VariableNew.VariableGuid = new VO_IntValue();
                VariableSelector.VariableGuid = Guid.Empty;
                OperatorType.SelectedIndex = 0;
            }
            else
            {
                VariableNew.VariableGuid = CurrentVariable.Value;
                if (CurrentVariable.Value.VariableValue == Guid.Empty)
                    VariableNew.Text = Convert.ToString(CurrentVariable.Value.IntValue);
                else
                    VariableNew.Text = GameCore.Instance.GetVariableById(CurrentVariable.Value.VariableValue).Title;
                VariableSelector.VariableGuid = CurrentVariable.Variable;
                OperatorType.SelectedValue = CurrentVariable.Operator;
            }
        }

        private void ScriptVariable_Validation(object sender, EventArgs e)
        {
            CurrentVariable.Value = VariableNew.VariableGuid;
            CurrentVariable.Variable = VariableSelector.VariableGuid;
            CurrentVariable.Operator = (Enums.ChangeOperator)((OperatorType.SelectedItem.GetType()).GetProperty("Key")).GetValue(OperatorType.SelectedItem, null);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ScriptVariable_Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
