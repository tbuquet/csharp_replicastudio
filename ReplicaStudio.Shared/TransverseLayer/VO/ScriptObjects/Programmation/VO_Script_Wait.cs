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
    public class VO_Script_Wait : VO_Line, IScriptable
    {
        #region Properties
        public VO_IntValue SecondsToWait { get; set; }
        #endregion

        #region Constructor
        public VO_Script_Wait()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            if (SecondsToWait.VariableValue == Guid.Empty)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Wait");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " for ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, SecondsToWait.IntValue.ToString());
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " second(s)");
                node.Text = TextColor.GetJsonisedObject();
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Wait");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " for ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetVariableById(SecondsToWait.VariableValue).Title + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " value");

                node.Text = TextColor.GetJsonisedObject();
            }
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
            return true;
        }

        #endregion
    }
}
