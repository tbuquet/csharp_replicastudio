using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_RunningScript : VO_Script
    {
        #region Properties
        public VO_Line CurrentLine { get; set; }

        public int WaitFrames { get; set; }

        public int ObjectState { get; set; }
        #endregion

        #region Constructor
        public VO_RunningScript()
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
