using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script_Loop : VO_Line, IScriptable, IScriptableContainer
    {
        #region Properties
        #region Sous lignes
        public List<VO_Line> WhileSubLines { get; set; }
        #endregion

        #region Conditions
        //Compare bouton
        public bool UseButton { get; set; }
        public Guid Button { get; set; }
        public bool ButtonValue { get; set; }

        //Compare variable
        public bool UseVariable { get; set; }
        public Enums.ComparativeOperator Operator { get; set; }
        public Guid Variable { get; set; }
        public VO_IntValue VariableValue { get; set; }
        #endregion
        #endregion

        #region Constructor
        public VO_Script_Loop()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialise le script
        /// </summary>
        public void InitNewScript()
        {
            WhileSubLines = new List<VO_Line>();
            VariableValue = new VO_IntValue();
        }

        /// <summary>
        /// Met en place le string du If
        /// </summary>
        private void MakeScriptString(TreeViewColorTool TextColor)
        {
            if (UseButton)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "< Button: ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, GameCore.Instance.GetTriggerById(Button).Title);
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " is ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Convert.ToString(ButtonValue));
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " >");
            }
            if (UseVariable)
                if (VariableValue.VariableValue == Guid.Empty)
                {
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "< Variable: ");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, GameCore.Instance.GetVariableById(Variable).Title);
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " is ");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, Operator.ToString());
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, " " + VariableValue.IntValue);
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " >");
                }
                else
                {
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "< Variable: ");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, GameCore.Instance.GetVariableById(Variable).Title);
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " is ");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, Operator.ToString());
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, " " + GameCore.Instance.GetVariableById(VariableValue.VariableValue).Title);
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " >");
                }

            return;
        }

        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode ifCondition = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "Loop");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " (");
            MakeScriptString(TextColor);
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ")");

            ifCondition.Text = TextColor.GetJsonisedObject();
            ifCondition.Name = code;
            ifCondition.Tag = this;
            foreach (IScriptable line in WhileSubLines)
            {
                line.Valid = line.IsScriptValid();
                List<TreeNode> nodes = line.RenderInScriptManager("while");
                foreach (TreeNode node in nodes)
                {
                    ifCondition.Nodes.Add(node);
                }
            }
            ifCondition.Nodes.Add("while", "...");

            list.Add(ifCondition);
            return list;
        }
        #endregion

        #region Lines
        public List<VO_Line> GetLines(string code)
        {
            if (code == "while")
                return WhileSubLines;
            return null;
        }

        public IScriptable Clone()
        {
            IScriptable NewScript = (IScriptable)this.MemberwiseClone();
            VO_Script_Loop CurrentObject = NewScript as VO_Script_Loop;
            CurrentObject.WhileSubLines = new List<VO_Line>();

            foreach (VO_Line CurrentWhileLine in this.WhileSubLines)
            {
                IScriptable CurrentScriptLine = CurrentWhileLine as IScriptable;
                VO_Line NewSubLine = CurrentScriptLine.Clone() as VO_Line;
                CurrentObject.WhileSubLines.Add(NewSubLine);
            }

            return NewScript;
        }

        public bool IsScriptValid()
        {
            bool IsValid = true;
            if (WhileSubLines == null)
                IsValid = false;
            if (UseButton && ValidationTools.CheckObjectExistence(GameCore.Instance.GetTriggerById(Button)) == false)
            {
                Button = Guid.Empty;
                IsValid = false;
            }
            else if (UseVariable)
            {
                if (VariableValue.VariableValue == Guid.Empty)
                {
                    Variable = Guid.Empty;
                }
                else
                {
                    if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetVariableById(Variable)) == false)
                    {
                        Variable = Guid.Empty;
                        IsValid = false;
                    }
                    if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetVariableById(VariableValue.VariableValue)) == false)
                    {
                        VariableValue.VariableValue = Guid.Empty;
                        IsValid = false;
                    }
                }
            }
            return IsValid;
        }

        #endregion
    }
}
