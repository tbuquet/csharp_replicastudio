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
    public class VO_Script_ChangePlayerAnimation : VO_Line, IScriptable
    {
        #region Properties
        public Guid Character { get; set; }
        public Enums.CharacterAnimationType AnimationType { get; set; }
        public Guid Animation { get; set; }
        public bool Loop { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ChangePlayerAnimation()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            string activated = "deactivated";
            if (Loop)
                activated = "activated";

            VO_PlayableCharacter playableCharacter = GameCore.Instance.GetPlayableCharacterById(Character);

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, "Change");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " player ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + playableCharacter.Title + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " animation of type ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + AnimationType.GetDescription() + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetCharAnimationById(playableCharacter.CharacterId, Animation).Title + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ", loop ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + activated + "]");

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
            VO_PlayableCharacter playableCharacter = GameCore.Instance.GetPlayableCharacterById(Character);

            bool IsValid = true;
            if (ValidationTools.CheckObjectExistence(playableCharacter) == false)
            {
                Character = Guid.Empty;
                IsValid = false;
            }
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetCharAnimationById(playableCharacter.CharacterId, Animation)) == false)
            {
                Animation = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
