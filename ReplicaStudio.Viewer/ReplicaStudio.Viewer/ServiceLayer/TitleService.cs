using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.BusinessLayer;
using ReplicaStudio.Viewer.TransverseLayer.Constants;

namespace ReplicaStudio.Viewer.ServiceLayer
{
    /// <summary>
    /// Classe service de Title
    /// </summary>
    public class TitleService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au Business
        /// </summary>
        TitleBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public TitleService()
        {
            _Business = new TitleBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renvoie les informations du menu
        /// </summary>
        /// <returns>VO_Menu</returns>
        public VO_Menu GetMenuData()
        {
            VO_Menu menu = null;

            RunServiceTask(delegate
            {
                menu = _Business.GetMenuData();
            }, ViewerErrors.TITLE_LOAD_MENU);

            return menu;
        }
        #endregion
    }
}
