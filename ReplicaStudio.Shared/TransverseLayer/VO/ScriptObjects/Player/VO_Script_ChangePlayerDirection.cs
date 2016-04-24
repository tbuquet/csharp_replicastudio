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
    public class VO_Script_ChangePlayerDirection : VO_Line, IScriptable
    {
        #region Properties
        public Enums.Movement Direction { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ChangePlayerDirection()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TreeNode node = new TreeNode();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, "Change ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[Current player]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " direction: ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Direction.GetDescription());

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
            if (Direction == null)
                return false;
            return true;
        }

        #endregion
    }
}
