using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère le panneau de projet
    /// </summary>
    public class ProjectPanelBusiness : BaseBusiness
    {
        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ProjectPanelBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère les fichiers décors
        /// </summary>
        /// <returns>Liste de fichiers</returns>
        public List<string> GetDecors()
        {
            List<string> decors = new List<string>();

            string[] files = Directory.GetFiles(PathTools.GetProjectPath(Enums.ProjectPath.Decors));
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file).ToUpper();
                if (extension == ".JPG" || extension == ".JPEG" || extension == ".PNG")
                    decors.Add(file.ToLower().Replace(GameCore.Instance.Game.Project.RootPath.ToLower() + GlobalConstants.PROJECT_DIR_RESOURCES.ToLower(), string.Empty));
            }

            return decors;
        }

        /// <summary>
        /// Récupère les fichiers musiques
        /// </summary>
        /// <returns>Liste de fichiers</returns>
        public List<string> GetMusics()
        {
            List<string> decors = new List<string>();

            string[] files = Directory.GetFiles(PathTools.GetProjectPath(Enums.ProjectPath.Musics), GlobalConstants.EXT_MUSIC_PATTERN);
            foreach (string file in files)
                decors.Add(file);
            return decors;
        }
        #endregion
    }
}
