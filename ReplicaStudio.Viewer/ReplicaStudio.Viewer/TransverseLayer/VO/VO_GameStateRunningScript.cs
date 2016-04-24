using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    [Serializable]
    public class VO_GameStateRunningScript
    {
        #region Properties
        public Guid Script { get; set; }
        public Guid CurrentLine { get; set; }
        public int ObjectState { get; set; }
        public int WaitFrames { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_GameStateRunningScript()
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
