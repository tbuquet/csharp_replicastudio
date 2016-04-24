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
    public class VO_Script_MoveCharacter : VO_Line, IScriptable
    {
        #region Properties
        public Guid Character { get; set; }
        public VO_Coords Coords { get; set; }
        #endregion

        #region Constructor
        public VO_Script_MoveCharacter()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, "Move");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " Character ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetStageCharacter(Character).Title + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Coords.ToString());

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
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetStageCharacter(Character)) == false)
            {
                Character = Guid.Empty;
                IsValid = false;
            }
            if (ValidationTools.CheckObjectExistence(Coords) == false)
            {
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
