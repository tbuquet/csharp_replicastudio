using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReplicaStudio.Editor.TransverseLayer.Constants
{
    public class GeneralSettingsConstants
    {
        /// <summary>
        /// Mode debug
        /// </summary>
        public const bool DEFAULT_DEBUG = true;

        /// <summary>
        /// Activer la synchronisation verticale pour le mode Try
        /// </summary>
        public const bool DEFAULT_TRY_VSYNC = true;

        /// <summary>
        /// Dossier réservé sur Mes Documents
        /// </summary>
        public const string DEFAULT_FOLDER = "Replica Studio Projects";

        /// <summary>
        /// Nom de l'application dans la barre du programme
        /// </summary>
        public const string APPLICATION_NAME = "Replica Studio";

        ///// <summary>
        ///// Chemin du viewer par défaut
        ///// </summary>
        //public const string DEFAULT_VIEWER_PATH = "ReplicaStudio.Viewer";

        /// <summary>
        /// Taille d'un bloc transparent en px.
        /// </summary>
        public const int DEFAULT_TRANSPARENT_BLOCK_SIZE = 8;

        /// <summary>
        /// Couleur du premier type bloc transparent
        /// </summary>
        public static Color DEFAULT_TRANSPARENT_COLOR_1 = Color.White;

        /// <summary>
        /// Couleur du second type bloc transparent
        /// </summary>
        public static Color DEFAULT_TRANSPARENT_COLOR_2 = Color.Gray;

        /// <summary>
        /// Couleur de hotspot sélectionné
        /// </summary>
        public static Color DEFAULT_SELECTEDHOTSPOT_COLOR = Color.Yellow;

        /// <summary>
        /// Couleur de surlignement
        /// </summary>
        public static Pen DEFAULT_HIGHLIGHTING_COLOR = Pens.OrangeRed;

        /// <summary>
        /// Couleur de sélection de coordonnées
        /// </summary>
        public static Pen DEFAULT_SELECTION_COORDS = Pens.Red;

        /// <summary>
        /// Couleur de remplissage surligné
        /// </summary>
        public static Brush DEFAULT_HIGHLIGHTING_COLOR_BRUSH = Brushes.Orange;

        /// <summary>
        /// Brush remplissage surligné couleur
        /// </summary>
        public static Color DEFAULT_HIGHLIGHTING_COLOR_BRUSH_COLOR = Color.Orange;

        /// <summary>
        /// Fréquence par défaut
        /// </summary>
        public static int DEFAULT_FREQUENCY = 100;

        /// <summary>
        /// Padding de scène
        /// </summary>
        public static int DEFAULT_STAGE_PADDING = 50;

        /// <summary>
        /// Taille de police des messages
        /// </summary>
        public static int DEFAULT_MESSAGE_FONTSIZE = 20;

        /// <summary>
        /// Durée des messages (secondes)
        /// </summary>
        public static int DEFAULT_MESSAGE_DURATION = 3;

        /// <summary>
        /// Afficher la couche animation avec les masques
        /// </summary>
        public static bool DEFAULT_SHOW_ANIMATIONS_DURING_MASKS = false;

        /// <summary>
        /// Afficher la couche characters avec les masques
        /// </summary>
        public static bool DEFAULT_SHOW_CHARACTERS_DURING_MASKS = false;

        /// <summary>
        /// Opacité des masques par défaut
        /// </summary>
        public static int DEFAULT_MASKS_OPACITY = 90;

        /// <summary>
        /// Taille des points vecteurs
        /// </summary>
        public static int VECTOR_POINT_SIZE = 6;

        /// <summary>
        /// Activer le zoom à la souris
        /// </summary>
        public static bool ACTIVATE_ZOOM_WHEEL = true;

        /// <summary>
        /// Langue par défaut
        /// </summary>
        public static string DEFAULT_LANGUAGE = "en-US";
    }
}
