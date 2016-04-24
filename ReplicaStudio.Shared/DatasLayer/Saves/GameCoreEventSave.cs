using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Shared.DatasLayer.Saves
{
    /// <summary>
    /// Classe Data clone
    /// </summary>
    public class GameCoreEventSave
    {
        #region Données
        public VO_Event Event;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GameCoreEventSave()
        {
            Event = new VO_Event();
        }
        #endregion
    }
}
