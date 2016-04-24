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
    public class VO_Script_MovePlayer : VO_Line, IScriptable
    {
        #region Properties
        public VO_Coords Coords { get; set; }

        public bool PlayerPasseMuraille { get; set; }

        public bool CanBeInterrupted { get; set; }
        #endregion

        #region Constructor
        public VO_Script_MovePlayer()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Player will move there: ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE,  Coords.ToString());

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
            if (ValidationTools.CheckObjectExistence(Coords) == false)
                return false;
            return true;
        }

        #endregion
    }
}
