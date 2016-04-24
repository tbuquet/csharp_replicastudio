using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script_HidePlayer : VO_Line, IScriptable
    {
        #region Properties
        #endregion

        #region Constructor

        public VO_Script_HidePlayer()
        {
        }

        #endregion

        #region Methods

        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeViewColorTool TextColor = new TreeViewColorTool();
            TreeNode node = new TreeNode();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Hide");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " player");

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
            return true;
        }

        #endregion
    }
}
