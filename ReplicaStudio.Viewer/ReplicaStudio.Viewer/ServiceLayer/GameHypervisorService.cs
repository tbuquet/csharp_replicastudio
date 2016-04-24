using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.BusinessLayer;

namespace ReplicaStudio.Viewer.ServiceLayer
{
    public class GameHypervisorService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au Business
        /// </summary>
        GameHypervisorBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public GameHypervisorService()
        {
            _Business = new GameHypervisorBusiness();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Charge le jeu dans le GameCore.
        /// </summary>
        /// <param name="path">Url du jeu</param>
        /// <returns>Etat du chargement</returns>
        public ViewerEnums.LoadingState LoadGame(string path)
        {
            ViewerEnums.LoadingState state = ViewerEnums.LoadingState.Unloaded;

            RunServiceTask(delegate
            {
                state = _Business.LoadGame(path);
            });

            return state;
        }
        #endregion
    }
}
