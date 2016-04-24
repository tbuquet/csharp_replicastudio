using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer
{
    public static class ViewerSettings
    {
        #region Members
        #endregion

        #region Properties
        public static bool Fullscreen { get; set; }

        public static string AppPath { get; set; }

        public static bool VerticalSync { get; set; }

        public static bool ActivateSound { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère le style d'affichage de la fenêtre de rendu
        /// </summary>
        /// <returns></returns>
        /*public static Styles GetFullScreenState()
        {
            if (Fullscreen)
                return Styles.Fullscreen;
            else
                return Styles.Titlebar;
        }*/
        #endregion
    }
}
