using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    [Serializable]
    public class VO_GameState
    {
        #region Properties
        public List<VO_Trigger> Triggers { get; set; }
        public List<VO_Variable> Variables { get; set; }
        public List<VO_GameStateCharacter> Players { get; set; }
        public List<VO_GameStateCharacter> CurrentStagePNJ { get; set; }
        public Guid CurrentCharacter { get; set; }
        public List<VO_GameStateStage> Stages { get; set; }
        public List<VO_GameStateRunningScript> RunningScripts { get; set; }
        #endregion

        #region Constructors
        public VO_GameState()
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
