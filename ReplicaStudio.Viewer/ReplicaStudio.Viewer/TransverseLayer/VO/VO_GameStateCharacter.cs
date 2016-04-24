using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    [Serializable]
    public class VO_GameStateCharacter
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public VO_Coords Coords { get; set; }
        public Guid CurrentAnim { get; set; }
        public Enums.Movement CurrentDirection { get; set; }
        public List<Guid> Items { get; set; }
        public List<Guid> Actions { get; set; }
        public bool IsTalking { get; set; }
        public List<Point> CurrentPath { get; set; }
        public int CurrentExecutingPage { get; set; }
        #endregion

        #region Constructor
        public VO_GameStateCharacter()
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
