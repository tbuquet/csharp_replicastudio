using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    public class VO_Script_RollBackWhile : VO_Line, IScriptable
    {
        #region Properties
        public VO_Line WhileLine { get; set; }
        #endregion

        #region Constructor
        public VO_Script_RollBackWhile()
        {
        }
        #endregion

        #region Methods
        public List<TreeNode> RenderInScriptManager(string code)
        {
            return new List<TreeNode>();
        }

        public bool IsScriptValid()
        {
            return true;
        }

        public IScriptable Clone()
        {
            IScriptable NewScript = (IScriptable)this.MemberwiseClone();
            return NewScript;
        }
        #endregion
    }
}
