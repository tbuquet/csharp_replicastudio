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
    public class VO_Script_FreezePlayerAnimation : VO_Line, IScriptable
    {
        #region Properties
        public Guid Character { get; set; }
        public Enums.CharacterAnimationType AnimationType { get; set; }
        public bool FreezeAll { get; set; }
        #endregion

        #region Constructor
        public VO_Script_FreezePlayerAnimation()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            String CurrentPlayer;
            if (Character == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                CurrentPlayer = "Current Player";
            else
                CurrentPlayer = GameCore.Instance.GetPlayableCharacterById(Character).Title;
            if (FreezeAll == true)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Freeze");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " all Animations of Player ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + CurrentPlayer + "]");

                node.Text = TextColor.GetJsonisedObject();
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Freeze");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " Animation ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + AnimationType.GetDescription() + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " of Player ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + CurrentPlayer + "]");

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
            return IsValid;
        }

        #endregion
    }
}
