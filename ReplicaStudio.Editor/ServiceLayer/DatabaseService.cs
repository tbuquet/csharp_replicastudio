using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.ServiceLayer
{
    /// <summary>
    /// Classe service de la database
    /// </summary>
    public class DatabaseService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        DatabaseBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseService()
        {
            _Business = new DatabaseBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sauvegarde la DB
        /// </summary>
        public void SaveDB()
        {
            RunServiceTask(delegate
            {
                _Business.SaveDB();
            }, Errors.ERROR_STR_DBSAVE);
        }

        /// <summary>
        /// Restaure la DB
        /// </summary>
        public void RestoreDB()
        {
            RunServiceTask(delegate
            {
                _Business.RestoreDB();
            }, Errors.ERROR_STR_DBRESTORE);
        }
        #endregion
    }
}
