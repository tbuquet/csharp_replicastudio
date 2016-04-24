using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère la database des characters
    /// </summary>
    public class PlayerBusiness : BaseBusiness
    {
        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public PlayerBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Crée un character
        /// </summary>
        /// <returns>VO_Character</returns>
        public VO_PlayableCharacter CreatePlayer()
        {
            return ObjectsFactory.CreatePlayableCharacter();
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetPlayableCharacters();
        }

        /// <summary>
        /// Récupérer la liste des animations d'un character
        /// </summary>
        /// <param name="pId">Id du character</param>
        /// <returns></returns>
        public List<VO_Base> GetCharacterTemplateList(Guid id)
        {
            return GameCore.Instance.GetCharacters();
        }
        #endregion
    }
}
