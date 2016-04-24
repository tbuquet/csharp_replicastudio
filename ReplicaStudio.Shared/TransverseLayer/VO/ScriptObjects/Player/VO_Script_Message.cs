using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script_Message : VO_Line, IScriptable
    {
        #region Members
        public VO_Dialog Dialog { get; set; }
        #endregion

        #region Constructor
        public VO_Script_Message()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            if (Dialog != null && Dialog.Messages != null && Dialog.Messages.Count > 0)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_GREEN, "Show");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " message: ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Dialog.Messages[0].Text + "...");

                node.Text = TextColor.GetJsonisedObject();
                node.ToolTipText = "Show message: ";
                foreach (VO_Message message in Dialog.Messages)
                {
                    node.ToolTipText += "\r\n" + message.Text;
                }
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_GREEN, "Show");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " message: EMPTY");
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
            if (Dialog == null)
                return false;
            return true;
        }

        #endregion
    }
}
