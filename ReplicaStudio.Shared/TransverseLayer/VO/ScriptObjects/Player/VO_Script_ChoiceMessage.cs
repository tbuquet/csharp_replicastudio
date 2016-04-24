using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script_ChoiceMessage : VO_Line, IScriptable, IScriptableContainer
    {
        #region Properties
        public List<VO_LineChoices> Choices { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ChoiceMessage()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNode mainNode = new TreeNode();
            TreeViewColorTool MainTreeNode = new TreeViewColorTool();

            MainTreeNode.AddNewColorAndText(GlobalConstants.TREEVIEW_GREEN, "Show");
            MainTreeNode.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " choices...");

            mainNode.Name = code;
            mainNode.Text = MainTreeNode.GetJsonisedObject();
            mainNode.Tag = this;

            int i = 1;
            foreach (VO_LineChoices choice in Choices)
            {
                TreeNode choiceNode = new TreeNode();
                TreeViewColorTool TextColor = new TreeViewColorTool();

                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Choice ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, Convert.ToString(i));
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ": ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, choice.Choice);

                choiceNode.Name = code;
                choiceNode.Text = TextColor.GetJsonisedObject();
                choiceNode.Tag = this;
                foreach (IScriptable line in choice.SubLines)
                {
                    line.Valid = line.IsScriptValid();
                    List<TreeNode> nodes = line.RenderInScriptManager((i - 1).ToString());
                    foreach (TreeNode node in nodes)
                    {
                        choiceNode.Nodes.Add(node);
                    }
                }
                choiceNode.Nodes.Add((i - 1).ToString(), "...");
                mainNode.Nodes.Add(choiceNode);
                i++;
            }

            list.Add(mainNode);
            return list;
        }

        public List<VO_Line> GetLines(string code)
        {
            try
            {
                int convertedCode = Convert.ToInt32(code);
                return Choices[convertedCode].SubLines;
            }
            catch
            {
                return new List<VO_Line>();
            }
        }

        public IScriptable Clone()
        {
            IScriptable NewScript = (IScriptable)this.MemberwiseClone();
            VO_Script_ChoiceMessage CurrentObject = NewScript as VO_Script_ChoiceMessage;
            CurrentObject.Choices = new List<VO_LineChoices>();

            foreach (VO_LineChoices CurrentLineChoice in this.Choices)
            {
                VO_LineChoices NewLineChoice = CurrentLineChoice.Clone();
                CurrentObject.Choices.Add(NewLineChoice);
            }

            return NewScript;
        }

        public bool IsScriptValid()
        {
            if (Choices == null)
                return false;
            return true;
        }

        #endregion

        
    }
}
