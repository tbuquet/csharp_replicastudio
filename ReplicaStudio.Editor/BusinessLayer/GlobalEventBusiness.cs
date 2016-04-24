using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère la database des items
    /// </summary>
    class GlobalEventBusiness : BaseBusiness
    {
        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GlobalEventBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créer un evenement global
        /// </summary>
        /// <returns>VO_GlobalEvent</returns>
        public VO_GlobalEvent CreateGlobalEvent() 
        {
            return ObjectsFactory.CreateGlobalEvent();
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetGlobalEvents();
        }
        #endregion
    }
}
