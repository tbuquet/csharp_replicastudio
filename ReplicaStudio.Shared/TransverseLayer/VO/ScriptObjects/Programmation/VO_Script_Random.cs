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
    public class VO_Script_Random : VO_Line, IScriptable
    {
        #region Properties
        public Guid Variable { get; set; }
        public VO_IntValue MinValue { get; set; }
        public VO_IntValue MaxValue { get; set; }
        #endregion

        #region Constructor
        public VO_Script_Random()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            String MinValueStr = String.Empty;
            String MaxValueStr = String.Empty;

            if (MinValue.VariableValue == Guid.Empty)
                MinValueStr = MinValue.IntValue.ToString();
            else
                MinValueStr = "[" + GameCore.Instance.GetVariableById(MinValue.VariableValue).Title + "]";
            
            if (MaxValue.VariableValue == Guid.Empty)
                MaxValueStr = MaxValue.IntValue.ToString();
            else
                MaxValueStr = "[" + GameCore.Instance.GetVariableById(MaxValue.VariableValue).Title + "]";

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, GameCore.Instance.GetVariableById(Variable).Title);
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " will be Random between ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, MinValueStr);
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " and ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, MaxValueStr);

            node.Text = TextColor.GetJsonisedObject();
            node.Name = code;
            node.Tag = this;

            list.Add(node);

            return list;
        }

        public IScriptable Clone()
        {
            IScriptable NewScript = (IScriptable)this.MemberwiseClone();
            return NewScript;
        }

        public bool IsScriptValid()
        {
            bool IsValid = true;
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetVariableById(Variable)) == false)
            {
                Variable = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
