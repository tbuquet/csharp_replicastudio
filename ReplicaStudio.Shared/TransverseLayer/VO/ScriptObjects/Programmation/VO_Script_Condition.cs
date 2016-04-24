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
    public class VO_Script_Condition : VO_Line, IScriptable, IScriptableContainer
    {
        #region Properties
        #region Sous lignes
        public List<VO_Line> IfSubLines { get; set; }
        public List<VO_Line> ElseSubLines { get; set; }
        #endregion

        #region Conditions
        //Compare bouton
        public bool UseButton { get; set; }
        public Guid Button{get;set;}
        public bool ButtonValue { get; set; }

        //Compare variable
        public bool UseVariable { get; set; }
        public Enums.ComparativeOperator Operator { get; set; }
        public Guid Variable { get; set; }
        public VO_IntValue VariableValue { get; set; }

        //Player
        public bool UsePlayer { get; set; }
        public Guid Player { get; set; }
        #endregion
        #endregion

        #region Constructor
        public VO_Script_Condition()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialise le script
        /// </summary>
        public void InitNewScript()
        {
            ElseSubLines = new List<VO_Line>();
            IfSubLines = new List<VO_Line>();
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
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE,  Operator.ToString());
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
            if (UsePlayer)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "< Player: ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, GameCore.Instance.GetCharacterById(Player).Title);
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " >");
            }

            return;
        }

        /// <summary>
        /// Ecris les lignes du treenode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode ifCondition = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();
            
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "If");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " (");
            MakeScriptString(TextColor);
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ")");

            ifCondition.Text = TextColor.GetJsonisedObject();
            ifCondition.Name = code;
            ifCondition.Tag = this;
            if (ifCondition.IsExpanded == false)
            {
                foreach (IScriptable line in IfSubLines)
                {
                    line.Valid = line.IsScriptValid();
                    List<TreeNode> nodes = line.RenderInScriptManager("if");
                    foreach (TreeNode node in nodes)
                    {
                        ifCondition.Nodes.Add(node);
                    }
                }

                ifCondition.Nodes.Add("if", "...");
            }
            TreeNode elseCondition = new TreeNode();
            TreeViewColorTool ElseColor = new TreeViewColorTool();

            ElseColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "Else");

            elseCondition.Text = ElseColor.GetJsonisedObject();
            elseCondition.Name = code;
            elseCondition.Tag = this;
            if (elseCondition.IsExpanded == false)
            {
                foreach (IScriptable line in ElseSubLines)
                {
                    line.Valid = line.IsScriptValid();
                    List<TreeNode> nodes = line.RenderInScriptManager("else");
                    foreach (TreeNode node in nodes)
                    {
                        elseCondition.Nodes.Add(node);
                    }
                }
                elseCondition.Nodes.Add("else", "...");
            }
            list.Add(ifCondition);
            list.Add(elseCondition);
            return list;
        }

        /// <summary>
        /// Récupère les lignes
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<VO_Line> GetLines(string code)
        {
            if (code == "if")
                return IfSubLines;
            else
                return ElseSubLines;
        }

        public IScriptable Clone()
        {
            IScriptable NewScript = (IScriptable)this.MemberwiseClone();
            VO_Script_Condition CurrentObject = NewScript as VO_Script_Condition;
            CurrentObject.IfSubLines = new List<VO_Line>();

            //If Line List
            foreach (VO_Line CurrentIfLine in this.IfSubLines)
            {
                IScriptable CurrentScriptLine = CurrentIfLine as IScriptable;
                VO_Line NewSubLine = CurrentScriptLine.Clone() as VO_Line;
                CurrentObject.IfSubLines.Add(NewSubLine);
            }

            //Else Line List
            CurrentObject.ElseSubLines = new List<VO_Line>();

            foreach (VO_Line CurrentElseLine in this.ElseSubLines)
            {
                IScriptable CurrentScriptLine = CurrentElseLine as IScriptable;
                VO_Line NewSubLine = CurrentScriptLine.Clone() as VO_Line;
                CurrentObject.ElseSubLines.Add(NewSubLine);
            }

            return NewScript;
        }

        public bool IsScriptValid()
        {
            bool IsValid = true;
            if (UseButton && ValidationTools.CheckObjectExistence(GameCore.Instance.GetTriggerById(Button)) == false)
            {
                Button = Guid.Empty;
                IsValid = false;
            }
            else if (UseVariable)
            {
                // Source Variable to Compare
                if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetVariableById(Variable)) == false)
                {
                    Variable = Guid.Empty;
                    IsValid = false;
                }
                // If Destination Comparison is not an Int but an other Variable
                if (VariableValue.VariableValue != Guid.Empty)
                {
                    // Check of Destination Variable to Compare
                    if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetVariableById(VariableValue.VariableValue)) == false)
                    {
                        VariableValue.VariableValue = Guid.Empty;
                        IsValid = false;
                    }
                }
            }
            if (UsePlayer && ValidationTools.CheckObjectExistence(GameCore.Instance.GetCharacterById(Player)) == false)
            {
                Player = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
