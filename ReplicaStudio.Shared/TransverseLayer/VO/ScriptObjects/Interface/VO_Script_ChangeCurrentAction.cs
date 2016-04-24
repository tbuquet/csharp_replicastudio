using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Script_ChangeCurrentAction : VO_Line, IScriptable
    {
        #region Properties
        public Guid Action { get; set; }
        #endregion

        #region Constructor
        public VO_Script_ChangeCurrentAction()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            return new List<TreeNode>();
        }

        public IScriptable Clone()
        {
            IScriptable NewScript = (IScriptable)this.MemberwiseClone();
            return NewScript;
        }

        public bool IsScriptValid()
        {
            if (ValidationTools.CheckObjectExistence(GameCore.Instance.GetActionById(Action)) == false)
            {
                Action = Guid.Empty;
                return false;
            }
            return true;
        }

        #endregion
    }
}
