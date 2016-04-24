using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Viewer.BusinessLayer
{
    /// <summary>
    /// Class Business de Title
    /// </summary>
    public class TitleBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public TitleBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renvoie les informations du menu
        /// </summary>
        /// <returns>VO_Menu</returns>
        public VO_Menu GetMenuData()
        {
            return GameCore.Instance.Game.Menu;
        }
        #endregion
    }
}
