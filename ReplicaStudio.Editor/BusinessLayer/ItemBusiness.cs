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
    class ItemBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ItemBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créer un Item
        /// </summary>
        /// <returns>VO_Item</returns>
        public VO_Item CreateItem() 
        {
            return ObjectsFactory.CreateItem();
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetItems();
        }
        #endregion

        #region Errors Methods
        #endregion
    }
}
