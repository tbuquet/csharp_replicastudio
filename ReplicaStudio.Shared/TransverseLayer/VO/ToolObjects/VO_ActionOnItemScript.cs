using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_ActionOnItemScript
    {
        #region Properties
        public Guid Id { get; set; }
        public VO_Script Script { get; set; }
        #endregion

        #region Constructors
        public VO_ActionOnItemScript()
        {
        }

        public VO_ActionOnItemScript(Guid actionId, VO_Script script)
        {
            Id = actionId;
            Script = script;
        }
        #endregion
    }
}
