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

    public class VO_Script_ChangeCurrentCharacter : VO_Line, IScriptable
    {
        #region Properties
        public Guid Character { get; set; }
        public VO_Coords Coords { get; set; }
        public bool UseOldCoords { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ChangeCurrentCharacter()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNode node = new TreeNode();

            TreeViewColorTool TextColor = new TreeViewColorTool();
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_ORANGE, "Change ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[Current Character]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, " to ");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + GameCore.Instance.GetPlayableCharacterById(Character).Title + "]");
            TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ",");

            if (UseOldCoords)
                TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Old position");
            else
            {
                VO_Stage stage = GameCore.Instance.GetStageById(Coords.Map);
                if (stage != null)
                {
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, "Position: ");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, "[" + stage.Title + "]");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLACK, ", ");
                    TextColor.AddNewColorAndText(GlobalConstants.TREEVIEW_BLUE, Coords.Location.ToString());
                }
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
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetPlayableCharacterById(Character)) == false)
            {
                Character = Guid.Empty;
                IsValid = false;
            }
            else if (UseOldCoords == false && (ValidationTools.CheckObjectExistence(Coords) == false || ValidationTools.CheckMapExistence(Coords) == false))
            {
                Coords.Map = Guid.Empty;
                IsValid = false;
            }
            return IsValid;
        }

        #endregion
    }
}
