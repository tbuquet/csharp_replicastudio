using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script_MoveCamera : VO_Line, IScriptable
    {
        #region Properties
        public VO_Coords Coords { get; set; }
        public bool UseImmediately { get; set; }
        public int Speed { get; set; }
        #endregion

        #region Constructor
        public VO_Script_MoveCamera()
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
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " camera to ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + Coords.ToString() + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ", ");

            if (UseImmediately)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "immediately.");
                node.Text = TextColor.GetJsonisedObject();
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "with a speed of ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Convert.ToString(Speed));
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ".");
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
            if (ValidationTools.CheckObjectExistence(Coords) == false)
                return false;
            return true;
        }

        #endregion
    }
}
