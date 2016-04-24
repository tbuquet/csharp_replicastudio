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
    public class GameCoreTriggerSave
    {
        #region Données
        public List<VO_Trigger> Triggers;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GameCoreTriggerSave()
        {
            Triggers = new List<VO_Trigger>();
        }
        #endregion
    }
}
