using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Editor.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    /// <summary>
    /// Classe service du panneau projet
    /// </summary>
    public class ProjectPanelService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence à business
        /// </summary>
        ProjectPanelBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ProjectPanelService()
        {
            _Business = new ProjectPanelBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère les fichiers décors
        /// </summary>
        /// <returns>Liste de fichiers</returns>
        public List<string> GetDecors()
        {
            List<string> list = null;

            RunServiceTask(delegate
            {
                list = _Business.GetDecors();
            }, Errors.ERROR_PROJECT_STR_LOAD_DECORS);

            return list;
        }

        /// <summary>
        /// Récupère les fichiers musiques
        /// </summary>
        /// <returns>Liste de fichiers</returns>
        public List<string> GetMusics()
        {
            List<string> list = null;

            RunServiceTask(delegate
            {
                list = _Business.GetMusics();
            }, Errors.ERROR_PROJECT_STR_LOAD_MUSICS);

            return list;
        }
        #endregion
    }
}
