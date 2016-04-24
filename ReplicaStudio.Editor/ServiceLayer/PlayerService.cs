using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Editor.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    /// <summary>
    /// Classe service qui gère les characters dans la database
    /// </summary>
    public class PlayerService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        PlayerBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public PlayerService()
        {
            _Business = new PlayerBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Crée un character
        /// </summary>
        /// <returns>VO_Character</returns>
        public VO_PlayableCharacter CreatePlayer()
        {
            VO_PlayableCharacter character = null;

            RunServiceTask(delegate
            {
                character = _Business.CreatePlayer();
            }, Errors.ERROR_CHARACTER_STR_CREATE);

            return character;
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            List<VO_Base> list = null;

            RunServiceTask(delegate
            {
                list = _Business.ProvisionList();
            }, Errors.ERROR_STR_LIST_PROVISION);

            return list;
        }

        /// <summary>
        /// Récupérer la liste des animations d'un character
        /// </summary>
        /// <param name="pId">Id du character</param>
        /// <returns></returns>
        public List<VO_Base> GetCharacterTemplateList(Guid id)
        {
            List<VO_Base> list = null;

            RunServiceTask(delegate
            {
                list = _Business.GetCharacterTemplateList(id);
            }, Errors.ERROR_STR_LIST_PROVISION, id.ToString());

            return list;
        }
        #endregion
    }
}
