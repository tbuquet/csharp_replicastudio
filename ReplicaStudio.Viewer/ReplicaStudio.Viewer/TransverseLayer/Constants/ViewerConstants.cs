using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.Constants
{
    public class ViewerConstants
    {
        #region Généraux
        /// <summary>
        /// TODO: Rendre dynamique
        /// </summary>
        public const string FONT_MAINFONT = "Arial";

        /// <summary>
        /// FPS
        /// </summary>
        public const int FPS = 60;

        /// <summary>
        /// Transparence de background
        /// </summary>
        public const float MENU_BACKGROUND_TRANSPARENCY = 0.50F;
        #endregion

        #region PathFinder
        public const int PATHFINDER_SEARCHLIMIT = 15000;
        #endregion

        #region Arguments
        /// <summary>
        /// Argument Jeu
        /// </summary>
        public const string ARG_GAME = "-game";

        /// <summary>
        /// Argument FullScreen
        /// </summary>
        public const string ARG_FULLSCREEN = "-fullscreen";

        /// <summary>
        /// Argument Vertical Synchronisation
        /// </summary>
        public const string ARG_VSYNC = "-vsync";

        /// <summary>
        /// Argument activation du son
        /// </summary>
        public const string ARG_SOUND = "-sound";
        #endregion

        #region Title
        /// <summary>
        /// Espace entre les différentes options du menu
        /// </summary>
        public const int TITLE_MENU_PADDING = 25;
        #endregion

        #region Message
        /// <summary>
        /// Previous page
        /// </summary>
        public const string MESS_CHOICES_PREVIOUS_GUID = "A86FCE9A-2A4D-427E-B916-14A4BAE1C1C0";

        /// <summary>
        /// Next page
        /// </summary>
        public const string MESS_CHOICES_NEXT_GUID = "EE071446-A8C3-4A73-9268-5A5F3D7A2D19";

        /// <summary>
        /// Nombre de choix par page
        /// </summary>
        public const int MESS_CHOICES_MAX_PER_PAGE = 5;

        /// <summary>
        /// Taille minimum d'une ligne de dialogue pour une résolution standard 640x480
        /// </summary>
        public const int MESS_MINIMUM_WIDTH = 200;

        /// <summary>
        /// Taille maximum d'une ligne de dialogue pour une résolution standard 640x480
        /// </summary>
        public const int MESS_MAXIMUM_WIDTH = 300;

        /// <summary>
        /// Padding entre le texte et le perso
        /// </summary>
        public const int MESS_PADDING_CHARACTER = 5;

        /// <summary>
        /// Padding par rapport aux bordures de la résolution
        /// </summary>
        public const int MESS_PADDING_BORDER = 5;

        /// <summary>
        /// Padding entre les lignes
        /// </summary>
        public const int MESS_PADDING_LINES = 2;
        #endregion

        #region Save/Loads
        public const string SaveName = "save_{0}.sav";
        #endregion
    }
}
