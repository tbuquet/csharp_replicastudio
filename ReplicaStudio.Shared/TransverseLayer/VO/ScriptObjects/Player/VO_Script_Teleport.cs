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
    public class VO_Script_Teleport : VO_Line, IScriptable
    {
        #region Properties
        public VO_Coords Coords { get; set; }
        #endregion

        #region Constructor
        public VO_Script_Teleport()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TreeNode node = new TreeNode();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Player will move there: ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Coords.ToString());
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ", on Stage :");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, GameCore.Instance.GetStageById(Coords.Map).Title);

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
            if (ValidationTools.CheckObjectExistence(Coords) == false || ValidationTools.CheckMapExistence(Coords) == false)
            {
                Coords.Map = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
