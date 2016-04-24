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
    public class ActionService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        ActionBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ActionService()
        {
            _Business = new ActionBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Crée un item
        /// </summary>
        /// <returns>VO_Item</returns>
        public VO_Action CreateAction()
        {
            VO_Action action = null;

            RunServiceTask(delegate
            {
                action =  _Business.CreateAction();
            }, Errors.ERROR_ACTION_STR_CREATE);

            return action;
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList()
        {
            List<VO_Base> list = new List<VO_Base>();

            RunServiceTask(delegate
            {
                list = _Business.ProvisionList();
            }, Errors.ERROR_STR_LIST_PROVISION);

            return list;
        }
        #endregion
    }
}
