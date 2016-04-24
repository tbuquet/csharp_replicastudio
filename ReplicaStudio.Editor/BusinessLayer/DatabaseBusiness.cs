using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.BusinessLayer
{
    /// <summary>
    /// Code métier qui gère les opérations de base de la database. Principalement utilisée pour respecter le modèle de couches.
    /// </summary>
    public class DatabaseBusiness: BaseBusiness
    {
        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sauvegarde la DB
        /// </summary>
        public void SaveDB()
        {
            GameCore.Instance.SaveDB();
        }

        /// <summary>
        /// Restaure la DB
        /// </summary>
        public void RestoreDB()
        {
            GameCore.Instance.RestoreDB();
        }
        #endregion
    }
}
