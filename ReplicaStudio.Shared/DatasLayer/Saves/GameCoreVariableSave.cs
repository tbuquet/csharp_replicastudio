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
    public class GameCoreVariableSave
    {
        #region Données
        public List<VO_Variable> Variables;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GameCoreVariableSave()
        {
            Variables = new List<VO_Variable>();
        }
        #endregion
    }
}
