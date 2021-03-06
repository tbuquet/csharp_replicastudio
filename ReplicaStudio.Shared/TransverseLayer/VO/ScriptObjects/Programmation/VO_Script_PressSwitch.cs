﻿using System;
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
    public class VO_Script_PressSwitch : VO_Line, IScriptable
    {
        #region Properties
        public Guid Button { get; set; }
        public Boolean IsActive { get; set; }
        #endregion

        #region Constructor
        public VO_Script_PressSwitch()
        {
            Button = Guid.Empty;
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Trigger ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetTriggerById(Button).Title + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " is ");

            if (IsActive == true)
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_GREEN, "Enabled");
            else
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_RED, "Disabled");

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
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetTriggerById(Button)) == false)
            {
                Button = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
