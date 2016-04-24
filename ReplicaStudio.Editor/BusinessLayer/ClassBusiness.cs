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
    class ClassBusiness: BaseBusiness
    {
        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ClassBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créer une classe
        /// </summary>
        /// <returns>VO_Item</returns>
        public VO_Class CreateClass() 
        {
            return ObjectsFactory.CreateClass();
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetClasses();
        }

        /// <summary>
        /// Créateur de Bad Interactions
        /// </summary>
        /// <returns>Bad Interaction</returns>
        public VO_BadInteraction CreateBadInteraction()
        {
            return ObjectsFactory.CreateBadInteraction();
        }
        #endregion

        #region Errors Methods
        #endregion
    }
}
