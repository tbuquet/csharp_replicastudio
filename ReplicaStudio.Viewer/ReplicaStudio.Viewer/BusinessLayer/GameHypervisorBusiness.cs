using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.IO;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer;

namespace ReplicaStudio.Viewer.BusinessLayer
{
    /// <summary>
    /// Classe gérant les principales fonctions  de transition du jeu
    /// </summary>
    public class GameHypervisorBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public GameHypervisorBusiness()
        {
        }
        #endregion

        #region Methods
        #region Sauvegarde/Chargement
        /// <summary>
        /// Charge le jeu dans le GameCore.
        /// </summary>
        /// <param name="path">Url du jeu</param>
        /// <returns>Etat du chargement</returns>
        public ViewerEnums.LoadingState LoadGame(string path)
        {
            //Chargement des données brutes du jeu
            if (path.EndsWith(GlobalConstants.EXT_EXPORTED_GAME))
                return LoadExportedGame(path);
            else if (path.EndsWith(GlobalConstants.EXT_PROJECT))
                GameCore.Instance.LoadProject(path);

            //Vérifications
            //TODO

            //Validation
            return ViewerEnums.LoadingState.OK;
        }

        /// <summary>
        /// Charge un jeu binaire dans le GameCore
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ViewerEnums.LoadingState LoadExportedGame(string path)
        {
            GameCore.Instance.Game = (VO_Game)AppTools.LoadObjectFromFile(path);
            GameCore.Instance.Game.Project.RootPath = Path.GetDirectoryName(path) + "\\";
            GameCore.Instance.Game.Project.ProjectFileName = Path.GetFileNameWithoutExtension(path);

            DebugConsole.ReleaseMode = true;
            ViewerSettings.ActivateSound = true;

            //Validation
            return ViewerEnums.LoadingState.OK;
        }
        #endregion

        #region Tools
        #endregion
        #endregion
    }
}
