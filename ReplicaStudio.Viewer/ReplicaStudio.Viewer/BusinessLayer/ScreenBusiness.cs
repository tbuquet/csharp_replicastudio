using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Viewer.BusinessLayer
{
    /// <summary>
    /// Class Business de Title
    /// </summary>
    public class ScreenBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScreenBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renvoie les informations de terminology
        /// </summary>
        /// <returns>VO_Terminology</returns>
        public VO_Terminology GetTerminologyData()
        {
            return GameCore.Instance.Game.Terminology;
        }

        /// <summary>
        /// Renvoie les informations du project
        /// </summary>
        /// <returns>VO_Project</returns>
        public VO_Project GetProjectData()
        {
            return GameCore.Instance.Game.Project;
        }

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
