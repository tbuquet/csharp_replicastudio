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
    public class VO_Script_AddPlayerAction : VO_Line, IScriptable
    {
        #region Properties

        public Guid CharacterId { get; set; }
        public Guid ActionId { get; set; }

        #endregion

        #region Constructor

        public VO_Script_AddPlayerAction()
        {
            CharacterId = Guid.NewGuid();
            ActionId = Guid.NewGuid();
        }

        #endregion

        #region Methods

        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_GREEN, "Add");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " Action ");

            if (CharacterId == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetActionById(ActionId).Title + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to this Character ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[Current Player]");
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetActionById(ActionId).Title + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to this Character ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetPlayableCharacterById(CharacterId).Title + "]");
            }

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
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetActionById(ActionId)) == false)
            {
                ActionId = Guid.Empty;
                IsValid = false;
            }
            if (CharacterId != new Guid(GlobalConstants.CURRENT_PLAYER_ID) && ValidationTools.CheckObjectExistence(GameCore.Instance.GetPlayableCharacterById(CharacterId)) == false)
            {
                CharacterId = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
