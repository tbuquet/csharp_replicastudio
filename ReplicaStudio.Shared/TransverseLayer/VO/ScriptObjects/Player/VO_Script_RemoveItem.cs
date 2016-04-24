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
    public class VO_Script_RemoveItem : VO_Line, IScriptable
    {
        #region Properties
        public Guid Item { get; set; }
        public Guid Character { get; set; }
        #endregion

        #region Constructor
        public VO_Script_RemoveItem()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            if (Character == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Remove");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " Item ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetItemById(Item).Title + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to Character ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[Current Player]");

                node.Text = TextColor.GetJsonisedObject();
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Remove");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " Item ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetItemById(Item).Title + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to Character ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetPlayableCharacterById(Character).Title + "]");

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
            bool IsValid = true;
            if (Character != new Guid(GlobalConstants.CURRENT_PLAYER_ID) && ValidationTools.CheckObjectExistence(GameCore.Instance.GetPlayableCharacterById(Character)) == false)
            {
                Character = Guid.Empty;
                IsValid = false;
            }
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetItemById(Item)) == false)
            {
                Item = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
