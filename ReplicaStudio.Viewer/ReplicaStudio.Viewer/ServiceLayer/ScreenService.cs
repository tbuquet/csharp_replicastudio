using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.BusinessLayer;

namespace ReplicaStudio.Viewer.ServiceLayer
{
    /// <summary>
    /// Classe service de Title
    /// </summary>
    public class ScreenService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au Business
        /// </summary>
        ScreenBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public ScreenService()
        {
            _Business = new ScreenBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renvoie les informations de terminology
        /// </summary>
        /// <returns>VO_Menu</returns>
        public VO_Terminology GetTerminologyData()
        {
            VO_Terminology terminology = null;

            RunServiceTask(delegate
            {
                terminology = _Business.GetTerminologyData();
            });

            return terminology;
        }

        /// <summary>
        /// Renvoie les informations du menu
        /// </summary>
        /// <returns>VO_Menu</returns>
        public VO_Project GetProjectData()
        {
            VO_Project project = null;

            RunServiceTask(delegate
            {
                project = _Business.GetProjectData();
            });

            return project;
        }


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
            });

            return menu;
        }
        #endregion
    }
}
