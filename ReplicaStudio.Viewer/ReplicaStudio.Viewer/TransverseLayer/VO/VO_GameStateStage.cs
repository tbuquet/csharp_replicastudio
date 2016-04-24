using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    [Serializable]
    public class VO_GameStateStage
    {
        #region Properties
        public Guid StageId { get; set; }
        public bool StartScriptDone { get; set; }
        public bool EndScriptDone { get; set; }
        #endregion

        #region Constructors
        public VO_GameStateStage()
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
