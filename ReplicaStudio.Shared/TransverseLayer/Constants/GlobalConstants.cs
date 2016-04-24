using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.Constants
{
    public class GlobalConstants
    {
        /// <summary>
        /// Version de l'appli
        /// </summary>
        public const double PROJECT_VERSION = 1.0;

        /// <summary>
        /// Application Beta
        /// </summary>
        public const bool BETA_VERSION = true;

        /// <summary>
        /// Copyright
        /// </summary>
        public const string COPYRIGHT = "© Replica Studio";

        ///<summary>
        /// Viewer Exe Name
        ///</summary>
        public const string VIEWER_NAME = "ReplicaStudio.Viewer.exe";

        /// <summary>
        /// Settings
        /// </summary>
        public const string SETTINGS_FILENAME = "settings.xml";

        /// <summary>
        /// Clé de cryptage
        /// </summary>
        public const string CRYPT_KEY = "TeamRoXx";

        #region Objets généraux
        /// <summary>
        /// Objet "vide" ou lien "mort"
        /// </summary>
        public const string UNKNOWN = "#Not Found#";

        /// <summary>
        /// Objet "All"
        /// </summary>
        public const string ALL = "All";

        /// <summary>
        /// Delete d'une gridview
        /// </summary>
        public const string GRIDVIEW_DELETE = "X";

        /// <summary>
        /// Choisir un son
        /// </summary>
        public const string BUTTON_SOUND = "Choose sound...";
        #endregion

        #region Extensions
        /// <summary>
        /// Extension scène
        /// </summary>
        public const string EXT_EXPORTED_GAME = ".scn";

        /// <summary>
        /// Pattern de recherche des sprites
        /// </summary>
        public const string EXT_SPRITE_PATTERN = "*.png";

        /// <summary>
        /// Pattern de recherche de musiques
        /// </summary>
        public const string EXT_MUSIC_PATTERN = "*.mp3";

        /// <summary>
        /// Pattern de recherche de sons
        /// </summary>
        public const string EXT_SOUNDS_PATTERN = "*.mp3";

        /// <summary>
        /// Fichier Projet
        /// </summary>
        public const string EXT_PROJECT = ".pcs";
        #endregion

        #region Dialogs
        /// <summary>
        /// Joueur courant
        /// </summary>
        public const string CURRENT_PLAYER = "#Current player#";

        /// <summary>
        /// Id joueur courant
        /// </summary>
        public const string CURRENT_PLAYER_ID = "9E46ADB1-4D76-4163-AFC6-038CFD572B36";

        public const int DIALOG_MIN_DURATION = 1;
        public const int DIALOG_MAX_DURATION = 100;
        #endregion

        #region Performances
        /// <summary>
        /// Nbr de pixel reader
        /// </summary>
        public const int PERF_INGAME_BITSPERPIXEL = 16;

        /// <summary>
        /// Nombre max de calques
        /// </summary>
        public static int PERF_MAX_LAYERS = 10;

        /// <summary>
        /// Nombre max de décors par calque
        /// </summary>
        public static int PERF_MAX_DECORS_PER_LAYERS = 5;

        /// <summary>
        /// Nombre max de personnages par calque
        /// </summary>
        public static int PERF_MAX_CHARACTERS_PER_LAYERS = 10;

        /// <summary>
        /// Nombre max de hotspot par scene
        /// </summary>
        public static int PERF_MAX_HOTSPOT_PER_STAGE = 100;

        /// <summary>
        /// Nombre max de walk par scene
        /// </summary>
        public static int PERF_MAX_WALK_PER_STAGE = 10;

        /// <summary>
        /// Nombre max de regions par scene
        /// </summary>
        public static int PERF_MAX_REGIONS_PER_STAGE = 10;

        /// <summary>
        /// Nombre max d'anims par calque
        /// </summary>
        public static int PERF_MAX_ANIMATION_PER_LAYERS = 20;
        #endregion

        #region Project
        /// <summary>
        /// Dossier ressources
        /// </summary>
        public const string PROJECT_DIR_RESOURCES = "Resources";

        /// <summary>
        /// Dossier manuels
        /// </summary>
        public const string PROJECT_DIR_MANUALS = "Manuals";

        /// <summary>
        /// Dossier animations
        /// </summary>
        public const string PROJECT_DIR_ANIMATIONS = "Animations";

        /// <summary>
        /// Dossier animation d'objets
        /// </summary>
        public const string PROJECT_DIR_OBJECTANIMATIONS = "ObjectAnimations";

        /// <summary>
        /// Dossier faces de characters
        /// </summary>
        public const string PROJECT_DIR_CHARACTERFACES = "CharacterFaces";

        /// <summary>
        /// Dossier animations de characters
        /// </summary>
        public const string PROJECT_DIR_CHARACTERANIMATIONS = "CharacterAnimations";

        /// <summary>
        /// Dossier d'icônes
        /// </summary>
        public const string PROJECT_DIR_ICONS = "Icons";

        /// <summary>
        /// Dossier de menus
        /// </summary>
        public const string PROJECT_DIR_MENUS = "Menus";

        /// <summary>
        /// Dossiers décors
        /// </summary>
        public const string PROJECT_DIR_DECORS = "Decors";

        /// <summary>
        /// Dossier barres de vie
        /// </summary>
        public const string PROJECT_DIR_LIFEBAR = "LifeBar";

        /// <summary>
        /// Dossier musiques
        /// </summary>
        public const string PROJECT_DIR_MUSICS = "Musics";

        /// <summary>
        /// Dossier fonts
        /// </summary>
        public const string PROJECT_DIR_FONTS = "Fonts";

        /// <summary>
        /// Dossier GUI
        /// </summary>
        public const string PROJECT_DIR_GUIS = "GUI";

        /// <summary>
        /// Dossier sons
        /// </summary>
        public const string PROJECT_DIR_SOUNDS = "Sounds";

        /// <summary>
        /// Dossier voix
        /// </summary>
        public const string PROJECT_DIR_VOICES = "Voices";

        /// <summary>
        /// Dossier effets sonores
        /// </summary>
        public const string PROJECT_DIR_EFFECTS = "Effects";

        /// <summary>
        /// Dossier de maps
        /// </summary>
        public const string PROJECT_DIR_STAGES = "Stages";

        /// <summary>
        /// Dossier de maps
        /// </summary>
        public const string PROJECT_DIR_MATRIXES = "Matrixes";
        #endregion

        #region Stage
        /// <summary>
        /// Nom d'une nouvelle scène
        /// </summary>
        public const string STAGE_NEW_STAGE = "#New Stage#";

        /// <summary>
        /// Nouveau calque
        /// </summary>
        public const string STAGE_NEW_LAYER = "#New layer#";

        /// <summary>
        /// Limite de padding
        /// </summary>
        public const int STAGE_PADDING_LIMIT = 512;
        #endregion

        #region Animations
        /// <summary>
        /// Nom de la nouvelle animation
        /// </summary>
        public const string ANIMATIONS_NEW_ITEM = "#Animation{0}#";

        /// <summary>
        /// Lien vers la ressource image vide.
        /// </summary>
        public const string ANIMATION_EMPTY_RESOURCE = "ReplicaStudio.Editor.Resources.empty.gif";

        /// <summary>
        /// Largeur de l'image vide
        /// </summary>
        public const int ANIMATION_EMPTY_WIDTH = 100;

        /// <summary>
        /// Hauteur de l'image vide
        /// </summary>
        public const int ANIMATION_EMPTY_HEIGHT = 100;

        public const int ANIMATION_MIN_FREQUENCY = 1;
        public const int ANIMATION_NORMAL_FREQUENCY = 50;
        public const int ANIMATION_MAX_FREQUENCY = 200;
        #endregion

        #region Characters
        public const int CHARACTERS_MIN_RATIO = 1;
        public const int CHARACTERS_NORMAL_RATIO = 100;
        public const int CHARACTERS_MAX_RATIO = 200;

        /// <summary>
        /// Nom du nouveau character
        /// </summary>
        public const string CHARACTERS_NEW_ITEM = "#Character{0}#";

        /// <summary>
        /// Nom du nouveau character
        /// </summary>
        public const string PLAYABLECHARACTERS_NEW_ITEM = "#PlayableCharacter{0}#";

        /// <summary>
        /// Animation Standing
        /// </summary>
        public const string CHARACTERS_STANDING = "Standing";

        /// <summary>
        /// Animation Talking
        /// </summary>
        public const string CHARACTERS_TALKING = "Talking";

        /// <summary>
        /// Animation Walking
        /// </summary>
        public const string CHARACTERS_WALKING = "Walking";

        public const int CHARACTERS_MIN_SPEED = 1;
        public const int CHARACTERS_NORMAL_SPEED = 3;
        public const int CHARACTERS_MAX_SPEED = 10;
        #endregion

        #region Item
        /// <summary>
        /// Nom du nouvel item
        /// </summary>
        public const string ITEM_NEW_ITEM = "#Item{0}#";
        #endregion

        #region Action
        /// <summary>
        /// Nom de la nouvelle action
        /// </summary>
        public const string ACTION_NEW_ITEM = "#Action{0}#";

        /// <summary>
        /// Nom de l'action "Aller"
        /// </summary>
        public const string ACTION_GO = "Go";

        /// <summary>
        /// Nom de l'action "Utiliser"
        /// </summary>
        public const string ACTION_USE = "Use";

        /// <summary>
        /// Nom Id Go
        /// </summary>
        public const string ACTION_GO_ID = "B39ADDC1-F2D1-44D2-A372-5F9E6CF8F444";

        /// <summary>
        /// Nom Id Use
        /// </summary>
        public const string ACTION_USE_ID = "DA106845-2E36-43CC-8425-DC13F06D17BC";
        #endregion

        #region Event
        /// <summary>
        /// Nom du nouvel event
        /// </summary>
        public const string EVENT_NEW_ITEM = "#Event#";
        #endregion

        #region HotSpot
        /// <summary>
        /// Nom du nouvel hotspot
        /// </summary>
        public const string HOTSPOT_NEW_ITEM = "#HotSpot#";
        #endregion

        #region Walkable Areas
        /// <summary>
        /// Nom du nouvel walkable area
        /// </summary>
        public const string WALK_NEW_ITEM = "#Walkable area#";
        #endregion

        #region Regions
        /// <summary>
        /// Nom de la nouvelle region
        /// </summary>
        public const string REGION_NEW_ITEM = "#Region#";
        #endregion

        #region Class
        /// <summary>
        /// Nom de la nouvelle classe
        /// </summary>
        public const string CLASS_NEW_ITEM = "#Class{0}#";

        /// <summary>
        /// Choisir un dialogue
        /// </summary>
        public const string CLASS_CHOOSE_DIALOG = "Choose dialog...";
        #endregion

        #region Trigger
        /// <summary>
        /// Nom du nouveau bouton
        /// </summary>
        public const string TRIGGER_NEW_ITEM = "#Trigger#";
        #endregion

        #region Variable
        /// <summary>
        /// Nom de la nouvelle variable
        /// </summary>
        public const string VARIABLE_NEW_ITEM = "#Variable#";
        #endregion

        #region Musics

        public const int MUSIC_MIN_SPEED = 10;

        public const int MUSIC_NORMAL_SPEED = 100;

        public const int MUSIC_MAX_SPEED = 200;

        #endregion

        #region Player Speed

        public const int PLAYER_MIN_SPEED = 10;

        public const int PLAYER_NORMAL_SPEED = 100;

        public const int PLAYER_MAX_SPEED = 200;

        #endregion

        #region GlobalEvent
        /// <summary>
        /// Nom du nouveal evenement global
        /// </summary>
        public const string GLOBALEVENT_NEW_ITEM = "#GlobalEvent{0}#";
        #endregion

        #region TreeViewColors

        public const string TREEVIEW_BLACK = "#000000";

        public const string TREEVIEW_RED = "#910000";

        public const string TREEVIEW_GREEN = "#097500";

        public const string TREEVIEW_BLUE = "#4079D6";

        public const string TREEVIEW_ORANGE = "#FCAE05";

        #endregion
    }
}
