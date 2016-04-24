using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.Constants
{
    /// <summary>
    /// Classe d'erreur
    /// </summary>
    public class ViewerErrors
    {
        #region Load/Save du jeu
        /// <summary>
        /// Erreur non connue
        /// </summary>
        public const string LOAD_GAME_UNKNOWN = "The game has not been loaded properly";
        #endregion

        #region Title
        /// <summary>
        /// Erreur de chargement de menu
        /// </summary>
        public const string TITLE_LOAD_MENU = "Menu Data has not been loaded properly";
        #endregion

        #region Screen
        /// <summary>
        /// Erreur de chargement de project
        /// </summary>
        public const string PROJECT_LOAD_MENU = "Project Data has not been loaded properly";

        /// <summary>
        /// Erreur de chargement de terminology
        /// </summary>
        public const string TERMINOLOGY_LOAD_MENU = "Terminology Data has not been loaded properly";
        #endregion

        #region Stage
        /// <summary>
        /// Erreur de chargement de scène
        /// </summary>
        public const string STAGE_LOAD_MENU = "Stage Data has not been loaded properly";
        #endregion
    }
}
