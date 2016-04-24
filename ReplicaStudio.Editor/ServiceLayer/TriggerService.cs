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
    public class TriggerService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        TriggerBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public TriggerService()
        {
            _Business = new TriggerBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Crée une classe
        /// </summary>
        /// <returns>VO_Class</returns>
        public VO_Trigger CreateTrigger()
        {
            VO_Trigger item = null;

            RunServiceTask(delegate
            {
                item = _Business.CreateTrigger();
            }, Errors.ERROR_TRIGGER_STR_CREATE);

            return item;
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
        /// Sauvegarde la base de données des boutons
        /// </summary>
        public void SaveTriggers()
        {
            RunServiceTask(delegate
            {
                _Business.SaveTriggers();
            }, Errors.ERROR_TRIGGER_STR_DBSAVE);
        }

        /// <summary>
        /// Restaure la base de données des boutons
        /// </summary>
        public void RestaureTriggers()
        {
            RunServiceTask(delegate
            {
                _Business.RestaureTriggers();
            }, Errors.ERROR_TRIGGER_STR_DBRESTORE);
        }
        #endregion
    }
}
