using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Editor.Forms.ScriptForms
{
    public partial class ScriptLoop : Form
    {
        #region Properties
        public VO_Script_Loop Loop { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ScriptLoop()
        {
            InitializeComponent();

            ddpOperator.DataSource = typeof(Enums.ComparativeOperator).ToList();
            ddpOperator.DisplayMember = "Value";
            ddpOperator.ValueMember = "Key";
        }
        #endregion

        #region Eventhandlers
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            trgButton.TriggerGuid = Loop.Button;
            chkButtonActive.Checked = Loop.ButtonValue;
            ddpOperator.SelectedValue = (Enums.ComparativeOperator)Loop.Operator;
            rdButton.Checked = Loop.UseButton;
            rdVariable.Checked = Loop.UseVariable;
            varVariable1.VariableGuid = Loop.Variable;
            varVariable2.VariableGuid = Loop.VariableValue;
        }

        /// <summary>
        /// Click sur Ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Loop == null)
            {
                Loop = new VO_Script_Loop();
                Loop.WhileSubLines = new List<VO_Line>();
            }
            Loop.Id = Guid.NewGuid();
            Loop.Button = trgButton.TriggerGuid;
            Loop.ButtonValue = chkButtonActive.Checked;
            Loop.Operator = (Enums.ComparativeOperator)((ddpOperator.SelectedItem.GetType()).GetProperty("Key")).GetValue(ddpOperator.SelectedItem, null);
            Loop.UseButton = rdButton.Checked;
            Loop.UseVariable = rdVariable.Checked;
            Loop.Variable = varVariable1.VariableGuid;
            Loop.VariableValue = varVariable2.VariableGuid;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Click sur close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Check bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rdButton.Checked)
            {
                trgButton.Enabled = true;
                chkButtonActive.Enabled = true;
            }
            else
            {
                trgButton.Enabled = false;
                chkButtonActive.Enabled = false;
            }
        }

        /// <summary>
        /// Check variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdVariable_CheckedChanged(object sender, EventArgs e)
        {
            if (rdVariable.Checked)
            {
                varVariable1.Enabled = true;
                varVariable2.Enabled = true;
                ddpOperator.Enabled = true;
                lblVariableIs.Enabled = true;
            }
            else
            {
                varVariable1.Enabled = false;
                varVariable2.Enabled = false;
                ddpOperator.Enabled = false;
                lblVariableIs.Enabled = false;
            }
        }
        #endregion
    }
}
