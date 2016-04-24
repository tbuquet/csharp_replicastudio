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
    public class ItemService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        ItemBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ItemService()
        {
            _Business = new ItemBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Crée un item
        /// </summary>
        /// <returns>VO_Item</returns>
        public VO_Item CreateItem()
        {
            VO_Item item = null;

            RunServiceTask(delegate
            {
                item = _Business.CreateItem();
            }, Errors.ERROR_ITEM_STR_CREATE);

            return item;
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
        #endregion
    }
}
