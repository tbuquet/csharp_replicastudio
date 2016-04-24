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
    class VariableBusiness : BaseBusiness
    {
        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public VariableBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créer un bouton
        /// </summary>
        /// <returns>VO_Trigger</returns>
        public VO_Variable CreateVariable() 
        {
            return ObjectsFactory.CreateVariable();
        }

        /// <summary>
        /// Charge la liste de boutons
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetVariables();
        }

        /// <summary>
        /// Sauvegarde la base de données de boutons
        /// </summary>
        public void SaveVariables()
        {
            GameCore.Instance.SaveVariables();
        }

        /// <summary>
        /// Restaure la base de données d'animations
        /// </summary>
        public void RestaureVariables()
        {
            GameCore.Instance.RestoreVariables();
        }
        #endregion
    }
}
