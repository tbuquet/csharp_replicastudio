using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    public class ClassService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        ClassBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ClassService()
        {
            _Business = new ClassBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Crée une classe
        /// </summary>
        /// <returns>VO_Class</returns>
        public VO_Class CreateClass()
        {
            VO_Class vClass = null;

            RunServiceTask(delegate
            {
                vClass = _Business.CreateClass();
            }, Errors.ERROR_CLASS_STR_CREATE);

            return vClass;
        }

        /// <summary>
        /// Charge la liste de classes
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
        /// Créateur de Bad Interactions
        /// </summary>
        /// <returns>Bad Interaction</returns>
        public VO_BadInteraction CreateBadInteraction()
        {
            VO_BadInteraction badInteraction = null;

            RunServiceTask(delegate
            {
                badInteraction = _Business.CreateBadInteraction();
            }, Errors.ERROR_CLASS_BADINTERACTION_CREATE);

            return badInteraction;
        }
        #endregion
    }
}
