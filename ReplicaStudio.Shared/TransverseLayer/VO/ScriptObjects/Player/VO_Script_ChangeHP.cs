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
    public class VO_Script_ChangeHP : VO_Line, IScriptable
    {
        #region Properties
        public Guid CharacterId { get; set; }
        public Enums.ChangeOperator Operator { get; set; }
        public VO_IntValue Value { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ChangeHP()
        {
            Operator = Enums.ChangeOperator.Set;
            CharacterId = new Guid();
            Value = new VO_IntValue();
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();

            TreeNode node = new TreeNode();
            TreeViewColorTool TextColor = new TreeViewColorTool();

            String PlayerName;

            if (CharacterId == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                PlayerName = "Current Player";
            else
                PlayerName = GameCore.Instance.GetPlayableCharacterById(CharacterId).Title;

            if (Value.VariableValue == Guid.Empty)
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, Operator.GetDescription() + " ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Convert.ToString(Value.IntValue));
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " HP on ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + PlayerName + "]");
            }
            else
            {
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, Operator.GetDescription() + " ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetVariableById(Value.VariableValue).Title + "]");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " HP on ");
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + PlayerName + "]");
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
            if (CharacterId != new Guid(GlobalConstants.CURRENT_PLAYER_ID) && ValidationTools.CheckObjectExistence(GameCore.Instance.GetPlayableCharacterById(CharacterId)) == false)
            {
                CharacterId = Guid.Empty;
                IsValid = false;
            }
            if (Value.VariableValue != Guid.Empty && ValidationTools.CheckObjectExistence(GameCore.Instance.GetVariableById(Value.VariableValue)) == false)
            {
                Value.VariableValue = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
