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
    public class CharacterBusiness : BaseBusiness
    {
        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public CharacterBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Crée un character
        /// </summary>
        /// <returns>VO_Character</returns>
        public VO_Character CreateCharacter()
        {
            return ObjectsFactory.CreateCharacter();
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            return GameCore.Instance.GetCharacters();
        }

        /// <summary>
        /// Récupérer la liste des animations d'un character
        /// </summary>
        /// <param name="pId">Id du character</param>
        /// <returns></returns>
        public List<VO_Base> GetCharacterAnimationList(Guid id)
        {
            VO_Character character = GameCore.Instance.GetCharacterById(id);
            return character.GetAnimations();
        }
        #endregion
    }
}
