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
    class TriggerBusiness : BaseBusiness
    {
        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public TriggerBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créer un bouton
        /// </summary>
        /// <returns>VO_Trigger</returns>
        public VO_Trigger CreateTrigger() 
        {
            return ObjectsFactory.CreateTrigger();
        }

        /// <summary>
        /// Charge la liste de boutons
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetTriggers();
        }

        /// <summary>
        /// Sauvegarde la base de données de boutons
        /// </summary>
        public void SaveTriggers()
        {
            GameCore.Instance.SaveTriggers();
        }

        /// <summary>
        /// Restaure la base de données d'animations
        /// </summary>
        public void RestaureTriggers()
        {
            GameCore.Instance.RestoreTriggers();
        }
        #endregion
    }
}
