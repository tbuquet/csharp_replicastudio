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
    public class VO_Script_FreeCharacterAnimation : VO_Line, IScriptable
    {
        #region Properties
        public Guid Character { get; set; }
        public Enums.CharacterAnimationType AnimationType { get; set; }
        public bool FreeAll { get; set; }
        #endregion

        #region Constructor
        public VO_Script_FreeCharacterAnimation()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            if (FreeAll == true)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Free all Animations of Character ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetStageCharacter(Character).Title + "]");
                node.Text = TextColor.GetJsonisedObject();
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Free Animation ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + AnimationType.GetDescription() + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " of Character ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetStageCharacter(Character).Title + "]");

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
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetStageCharacter(Character)) == false)
            {
                Character = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
