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
    public class VO_Script_FocusOnCharacter : VO_Line, IScriptable
    {
        #region Properties
        public Guid Character { get; set; }
        public bool UseImmediately { get; set; }
        public int Speed { get; set; }
        #endregion

        #region Constructor
        public VO_Script_FocusOnCharacter()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, "Focus");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " camera on character ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetStageCharacter(Character).Title + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ", ");

            if (UseImmediately)
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "immediately.");
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "with a speed of ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Convert.ToString(Speed));
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ".");
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
