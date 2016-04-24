using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.ServiceLayer
{
    /// <summary>
    /// Classe service du projet
    /// </summary>
    public class ProjectService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        ProjectBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ProjectService()
        {
            _Business = new ProjectBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge les résolutions compatibles avec l'application.
        /// </summary>
        /// <returns>Liste de résolutions</returns>
        public List<VO_Resolution> LoadResolutions()
        {
            List<VO_Resolution> list = null;

            RunServiceTask(delegate
            {
                list = _Business.LoadResolutions();
            }, Errors.ERROR_PROJECT_STR_LOAD);

            return list;
        }

        /// <summary>
        /// Lance l'export
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Enums.ExportState LaunchExport(string path)
        {
            Enums.ExportState output = Enums.ExportState.InProgress;

            RunServiceTask(delegate
            {
                output = _Business.LaunchExport(path);
            }, Errors.ERROR_PROJECT_STR_EXPORTED);

            return output;
        }

        /// <summary>
        /// Création d'un projet
        /// </summary>
        /// <param name="pProject">VO_Project</param>
        public bool CreateProject(VO_Project project)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                _Business.CreateProject(project);
                output = true;
            }, Errors.ERROR_PROJECT_STR_CREATE);

            return output;
        }

        /// <summary>
        /// Sauvegarde l'intégralité du projet (xml général et maps)
        /// </summary>
        public void SaveProject()
        {
            RunServiceTask(delegate
            {
                _Business.SaveProject();
            }, Errors.ERROR_PROJECT_STR_SAVE);
        }

        /// <summary>
        /// Charger un projet
        /// </summary>
        /// <param name="pPath">Lien vers le fichier XML de chargement.</param>
        public bool LoadProject(string path)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.LoadProject(path);
            }, Errors.ERROR_PROJECT_STR_LOAD, path);

            return output;
        }

        /// <summary>
        /// Vérifie si un projet à créer existe à l'emplacement indiqué.
        /// </summary>
        /// <param name="pFile">Chemin vers le projet</param>
        /// <param name="pTitle">Titre du projet</param>
        /// <returns>True si le projet existe, false sinon</returns>
        public bool CheckIfProjectExist(string file, string title)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.CheckIfProjectExist(file, title);
            }, Errors.ERROR_PROJECT_STR_LOAD, file, title);

            return output;
        }

        /// <summary>
        /// Lancer un projet
        /// </summary>
        /// <param name="fullscreen"></param>
        public void LaunchProject(bool fullscreen)
        {
            RunServiceTask(delegate
            {
                _Business.LaunchProject(fullscreen);
            }, Errors.ERROR_PROJECT_STR_LOAD, Errors.ERROR_PROJECT_LAUNCH);
        }

        /// <summary>
        /// Lancer un programme externe
        /// </summary>
        /// <param name="path"></param>
        public void LaunchExternalTool(string path)
        {
            RunServiceTask(delegate
            {
                _Business.LaunchExternalTool(path);
            }, Errors.ERROR_PROJECT_STR_LOAD, Errors.ERROR_PROJECT_LAUNCH);
        }
        #endregion
    }
}
