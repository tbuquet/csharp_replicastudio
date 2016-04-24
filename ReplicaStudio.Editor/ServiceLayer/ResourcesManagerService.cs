using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.IO;
using ReplicaStudio.Editor.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    public class ResourcesManagerService : BaseService
    {
        #region Properties
        ResourcesManagerBusiness _rmBusiness;
        #endregion

        #region Constructor
        public ResourcesManagerService()
        {
            _rmBusiness = new ResourcesManagerBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Default method for binding all directories
        /// </summary>
        /// <param name="pProject"></param>
        /// <returns></returns>
        public List<VO_Directory> BindListFolder(VO_Project project)
        {
            List<VO_Directory> list = null;

            RunServiceTask(delegate
            {
                list = _rmBusiness.BindListFolder(project);
            }, Errors.ERROR_RESOURCESMANAGER_STR_LOAD, project.Title);

            return list;
        }

        /// <summary>
        /// Return the filtered Directories List
        /// </summary>
        /// <param name="pProject"></param>
        /// <param name="pFilter"></param>
        /// <returns></returns>
        public List<VO_Directory> BindListFolder(VO_Project project, String filter)
        {
            List<VO_Directory> list = null;

            RunServiceTask(delegate
            {
                list = _rmBusiness.BindListFolder(project, filter);        
            }, Errors.ERROR_RESOURCESMANAGER_STR_LOAD, project.Title, filter);

            return list;
        }        
        #endregion
    }
}

