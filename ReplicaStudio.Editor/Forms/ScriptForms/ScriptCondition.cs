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
    public partial class ScriptCondition : Form
    {
        #region Properties
        public VO_Script_Condition Condition { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ScriptCondition()
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

            LoadCharacters();

            trgButton.TriggerGuid = Condition.Button;
            chkButtonActive.Checked = Condition.ButtonValue;
            ddpOperator.SelectedValue = (Enums.ComparativeOperator)Condition.Operator;
            ddpCharacter.SelectedValue = Condition.Player;
            rdButton.Checked = Condition.UseButton;
            rdPlayer.Checked = Condition.UsePlayer;
            rdVariable.Checked = Condition.UseVariable;
            varVariable1.VariableGuid = Condition.Variable;
            varVariable2.VariableGuid = Condition.VariableValue;
        }

        /// <summary>
        /// Click sur Ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Condition == null)
            {
                Condition = new VO_Script_Condition();
                Condition.IfSubLines = new List<VO_Line>();
                Condition.ElseSubLines = new List<VO_Line>();
            }
            Condition.Id = Guid.NewGuid();
            Condition.Button = trgButton.TriggerGuid;
            Condition.ButtonValue = chkButtonActive.Checked;
            Condition.Operator = (Enums.ComparativeOperator)((ddpOperator.SelectedItem.GetType()).GetProperty("Key")).GetValue(ddpOperator.SelectedItem, null);
            if(ddpCharacter.SelectedValue != null)
                Condition.Player = (Guid)ddpCharacter.SelectedValue;
            Condition.UseButton = rdButton.Checked;
            Condition.UsePlayer = rdPlayer.Checked;
            Condition.UseVariable = rdVariable.Checked;
            Condition.Variable = varVariable1.VariableGuid;
            Condition.VariableValue = varVariable2.VariableGuid;

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

        /// <summary>
        /// Check character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdPlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPlayer.Checked)
            {
                ddpCharacter.Enabled = true;
            }
            else
            {
                ddpCharacter.Enabled = false;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charger les personnages
        /// </summary>
        private void LoadCharacters()
        {
            ddpCharacter.DataSource = GameCore.Instance.GetPlayableCharacters();
            ddpCharacter.ValueMember = "Id";
            ddpCharacter.DisplayMember = "Title";
        }
        #endregion
    }
}
