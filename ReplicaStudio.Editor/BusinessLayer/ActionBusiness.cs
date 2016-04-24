using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.BusinessLayer;
using System.Collections.Generic;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère la database des items
    /// </summary>
    class ActionBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ActionBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créer un Item
        /// </summary>
        /// <returns>VO_Item</returns>
        public VO_Action CreateAction() 
        {
            return ObjectsFactory.CreateAction();
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetActions();
        }
        #endregion
    }
}
