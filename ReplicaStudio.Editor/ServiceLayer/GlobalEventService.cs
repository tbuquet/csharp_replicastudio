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
    public class GlobalEventService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        GlobalEventBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GlobalEventService()
        {
            _Business = new GlobalEventBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Crée une classe
        /// </summary>
        /// <returns>VO_Class</returns>
        public VO_GlobalEvent CreateGlobalEvent()
        {
            VO_GlobalEvent globalEvent = null;

            RunServiceTask(delegate
            {
                globalEvent = _Business.CreateGlobalEvent();
            }, Errors.ERROR_GLOBALEVENT_STR_CREATE);

            return globalEvent;
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
        #endregion
    }
}
