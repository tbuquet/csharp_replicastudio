using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ReplicaStudio.Editor.TransverseLayer.Constants
{
    sealed class EditorConstants
    {
        #region Properties
        #region Notifications Singleton
        /// <summary>
        /// Unique instance of ReplicaConfig
        /// </summary>
        private static volatile EditorConstants instance = null;
        /// <summary>
        /// Synchronization's object
        /// </summary>
        private static object syncRoot = new Object();
        /// <summary>
        /// Get the unique instance of ReplicaConfig
        /// </summary>
        /// 

        public static EditorConstants Instance
        {
            get
            {
                // Quick test
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        // Thread safe test
                        if (instance == null)
                        {
                            // First call, create the instance
                            instance = new EditorConstants();
                        }
                    }
                }
                return (instance);
            }
        }

        /// <summary>
        /// Private constructor of the singleton
        /// </summary>
        private EditorConstants()
        {
        }
        #endregion

        #region ProjectPanel
        /// <summary>
        /// Stages
        /// </summary>
        public string PROJECTPANEL_STAGES { get { return Culture.Language.EditorConstants.PROJECTPANEL_STAGES; } }

        /// <summary>
        /// Backgrounds
        /// </summary>
        public string PROJECTPANEL_DECORS { get { return Culture.Language.EditorConstants.PROJECTPANEL_DECORS; } }

        /// <summary>
        /// Animations
        /// </summary>
        public string PROJECTPANEL_ANIMATIONS { get { return Culture.Language.EditorConstants.PROJECTPANEL_ANIMATIONS; } }

        /// <summary>
        /// Characters
        /// </summary>
        public string PROJECTPANEL_CHARACTERS { get { return Culture.Language.EditorConstants.PROJECTPANEL_CHARACTERS; } }

        /// <summary>
        /// Musics
        /// </summary>
        public string PROJECTPANEL_MUSICS { get { return Culture.Language.EditorConstants.PROJECTPANEL_MUSICS; } }

        /// <summary>
        /// Index de l'image de dossier inactif
        /// </summary>
        public const int PROJECTPANEL_FOLDER_INACTIVE = 0;

        /// <summary>
        /// Index de l'image de dossier actif
        /// </summary>
        public const int PROJECTPANEL_FOLDER_ACTIVE = 1;

        /// <summary>
        /// Index de l'image de l'animation inactive
        /// </summary>
        public const int PROJECTPANEL_ANIMATION_INACTIVE = 2;

        /// <summary>
        /// Index de l'image de l'animation active
        /// </summary>
        public const int PROJECTPANEL_ANIMATION_ACTIVE = 3;

        /// <summary>
        /// Index de l'image du décor inactive
        /// </summary>
        public const int PROJECTPANEL_DECOR_INACTIVE = 4;

        /// <summary>
        /// Index de l'image du décor active
        /// </summary>
        public const int PROJECTPANEL_DECOR_ACTIVE = 5;

        /// <summary>
        /// Index de l'image d'une scène inactive
        /// </summary>
        public const int PROJECTPANEL_STAGE_INACTIVE = 6;

        /// <summary>
        /// Index de l'image d'une scène active
        /// </summary>
        public const int PROJECTPANEL_STAGE_ACTIVE = 7;

        /// <summary>
        /// Index de l'image d'une musique inactive
        /// </summary>
        public const int PROJECTPANEL_MUSIC_INACTIVE = 8;

        /// <summary>
        /// Index de l'image d'une musique active
        /// </summary>
        public const int PROJECTPANEL_MUSIC_ACTIVE = 9;

        /// <summary>
        /// Index de l'image d'un character inactif
        /// </summary>
        public const int PROJECTPANEL_CHARACTER_INACTIVE = 10;

        /// <summary>
        /// Index de l'image d'un character actif
        /// </summary>
        public const int PROJECTPANEL_CHARACTER_ACTIVE = 11;
        #endregion

        #region StageObjectPanel
        /// <summary>
        /// Backgrounds
        /// </summary>
        public string STAGEOBJECTSPANEL_DECORS { get { return Culture.Language.EditorConstants.STAGEOBJECTSPANEL_DECORS; } }

        /// <summary>
        /// Animations
        /// </summary>
        public string STAGEOBJECTSPANEL_ANIMATIONS { get { return Culture.Language.EditorConstants.STAGEOBJECTSPANEL_ANIMATIONS; } }

        /// <summary>
        /// Characters
        /// </summary>
        public string STAGEOBJECTSPANEL_CHARACTERS { get { return Culture.Language.EditorConstants.STAGEOBJECTSPANEL_CHARACTERS; } }

        /// <summary>
        /// Hotspots
        /// </summary>
        public string STAGEOBJECTSPANEL_HOTSPOTS { get { return Culture.Language.EditorConstants.STAGEOBJECTSPANEL_HOTSPOTS; } }

        /// <summary>
        /// Walkable areas
        /// </summary>
        public string STAGEOBJECTSPANEL_WALKABLE { get { return Culture.Language.EditorConstants.STAGEOBJECTSPANEL_WALKABLE; } }

        /// <summary>
        /// Regions
        /// </summary>
        public string STAGEOBJECTSPANEL_REGIONS { get { return Culture.Language.EditorConstants.STAGEOBJECTSPANEL_REGIONS; } }

        /// <summary>
        /// Index de l'image de dossier inactif
        /// </summary>
        public const int STAGEOBJECTSPANEL_FOLDER_INACTIVE = 0;

        /// <summary>
        /// Index de l'image de dossier actif
        /// </summary>
        public const int STAGEOBJECTSPANEL_FOLDER_ACTIVE = 1;

        /// <summary>
        /// Index de l'image de l'animation inactive
        /// </summary>
        public const int STAGEOBJECTSPANEL_ANIMATION_INACTIVE = 2;

        /// <summary>
        /// Index de l'image de l'animation active
        /// </summary>
        public const int STAGEOBJECTSPANEL_ANIMATION_ACTIVE = 3;

        /// <summary>
        /// Index de l'image du décor inactive
        /// </summary>
        public const int STAGEOBJECTSPANEL_DECOR_INACTIVE = 4;

        /// <summary>
        /// Index de l'image du décor active
        /// </summary>
        public const int STAGEOBJECTSPANEL_DECOR_ACTIVE = 5;

        /// <summary>
        /// Index de l'image d'un character inactif
        /// </summary>
        public const int STAGEOBJECTSPANEL_CHARACTER_INACTIVE = 6;

        /// <summary>
        /// Index de l'image d'un character actif
        /// </summary>
        public const int STAGEOBJECTSPANEL_CHARACTER_ACTIVE = 7;

        /// <summary>
        /// Index de l'image d'un hotspot inactif
        /// </summary>
        public const int STAGEOBJECTSPANEL_HOTSPOT_INACTIVE = 8;

        /// <summary>
        /// Index de l'image d'un hotspot actif
        /// </summary>
        public const int STAGEOBJECTSPANEL_HOTSPOT_ACTIVE = 9;

        /// <summary>
        /// Index de l'image d'un walkable inactif
        /// </summary>
        public const int STAGEOBJECTSPANEL_WALK_INACTIVE = 10;

        /// <summary>
        /// Index de l'image d'un walkable actif
        /// </summary>
        public const int STAGEOBJECTSPANEL_WALK_ACTIVE = 11;

        /// <summary>
        /// Index de l'image d'un region inactif
        /// </summary>
        public const int STAGEOBJECTSPANEL_REGION_INACTIVE = 12;

        /// <summary>
        /// Index de l'image d'un region actif
        /// </summary>
        public const int STAGEOBJECTSPANEL_REGION_ACTIVE = 13;
        #endregion

        #region Directions
        public string MOV_UP { get { return Culture.Language.EditorConstants.MOV_UP; } }
        public string MOV_UPRIGHT { get { return Culture.Language.EditorConstants.MOV_UPRIGHT; } }
        public string MOV_RIGHT { get { return Culture.Language.EditorConstants.MOV_RIGHT; } }
        public string MOV_RIGHTDOWN { get { return Culture.Language.EditorConstants.MOV_RIGHTDOWN; } }
        public string MOV_DOWN { get { return Culture.Language.EditorConstants.MOV_DOWN; } }
        public string MOV_LEFTDOWN { get { return Culture.Language.EditorConstants.MOV_LEFTDOWN; } }
        public string MOV_LEFT { get { return Culture.Language.EditorConstants.MOV_LEFT; } }
        public string MOV_UPLEFT { get { return Culture.Language.EditorConstants.MOV_UPLEFT; } }
        #endregion

        #region StatusBar
        /// <summary>
        /// New Project
        /// </summary>
        public string STATUS_DESC_NEW { get { return Culture.Language.EditorConstants.STATUS_DESC_NEW; }  }

        /// <summary>
        /// Load Project
        /// </summary>
        public string STATUS_DESC_LOAD { get { return Culture.Language.EditorConstants.STATUS_DESC_LOAD; } }

        /// <summary>
        /// Save Project
        /// </summary>
        public string STATUS_DESC_SAVE { get { return Culture.Language.EditorConstants.STATUS_DESC_SAVE; } }

        /// <summary>
        /// Exit Project
        /// </summary>
        public string STATUS_DESC_EXIT { get { return Culture.Language.EditorConstants.STATUS_DESC_EXIT; } }

        /// <summary>
        /// General Settings
        /// </summary>
        public string STATUS_DESC_GENERALSETTINGS { get { return Culture.Language.EditorConstants.STATUS_DESC_GENERALSETTINGS; } }

        /// <summary>
        /// File
        /// </summary>
        public string STATUS_DESC_FILE { get { return Culture.Language.EditorConstants.STATUS_DESC_FILE; } }

        /// <summary>
        /// Edition
        /// </summary>
        public string STATUS_DESC_EDITION { get { return Culture.Language.EditorConstants.STATUS_DESC_EDITION; } }

        /// <summary>
        /// Cut
        /// </summary>
        public string STATUS_DESC_CUT { get { return Culture.Language.EditorConstants.STATUS_DESC_CUT; } }

        /// <summary>
        /// Copy
        /// </summary>
        public string STATUS_DESC_COPY { get { return Culture.Language.EditorConstants.STATUS_DESC_COPY; } }

        /// <summary>
        /// Paste
        /// </summary>
        public string STATUS_DESC_PASTE { get { return Culture.Language.EditorConstants.STATUS_DESC_PASTE; } }

        /// <summary>
        /// Delete
        /// </summary>
        public string STATUS_DESC_DELETE { get { return Culture.Language.EditorConstants.STATUS_DESC_DELETE; } }

        /// <summary>
        /// View
        /// </summary>
        public string STATUS_DESC_VIEW { get { return Culture.Language.EditorConstants.STATUS_DESC_VIEW; } }

        /// <summary>
        /// View Toolbar
        /// </summary>
        public string STATUS_DESC_VIEW_TOOLBAR { get { return Culture.Language.EditorConstants.STATUS_DESC_VIEW_TOOLBAR; } }

        /// <summary>
        /// View left bar
        /// </summary>
        public string STATUS_DESC_VIEW_LEFT_BAR { get { return Culture.Language.EditorConstants.STATUS_DESC_VIEW_LEFT_BAR; } }

        /// <summary>
        /// View right bar
        /// </summary>
        public string STATUS_DESC_VIEW_RIGHT_BAR { get { return Culture.Language.EditorConstants.STATUS_DESC_VIEW_RIGHT_BAR; } }

        /// <summary>
        /// Mode
        /// </summary>
        public string STATUS_DESC_MODE { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE; } }

        /// <summary>
        /// Mode décors
        /// </summary>
        public string STATUS_DESC_MODE_DECORS { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE_DECORS; } }

        /// <summary>
        /// Mode objets
        /// </summary>
        public string STATUS_DESC_MODE_OBJECTS { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE_OBJECTS; } }

        /// <summary>
        /// Mode characters
        /// </summary>
        public string STATUS_DESC_MODE_CHARACTERS { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE_CHARACTERS; } }

        /// <summary>
        /// Mode events
        /// </summary>
        public string STATUS_DESC_MODE_EVENTS { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE_EVENTS; } }

        /// <summary>
        /// Mode walkable
        /// </summary>
        public string STATUS_DESC_MODE_WALKABLE { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE_WALKABLE; } }

        /// <summary>
        /// Mode regions
        /// </summary>
        public string STATUS_DESC_MODE_REGIONS { get { return Culture.Language.EditorConstants.STATUS_DESC_MODE_REGIONS; } }

        /// <summary>
        /// Drawing
        /// </summary>
        public string STATUS_DESC_DRAWING { get { return Culture.Language.EditorConstants.STATUS_DESC_DRAWING; } }

        /// <summary>
        /// Pencil
        /// </summary>
        public string STATUS_DESC_PENCIL { get { return Culture.Language.EditorConstants.STATUS_DESC_PENCIL; } }

        /// <summary>
        /// Rectangle
        /// </summary>
        public string STATUS_DESC_RECTANGLE { get { return Culture.Language.EditorConstants.STATUS_DESC_RECTANGLE; } }

        /// <summary>
        /// Circle
        /// </summary>
        public string STATUS_DESC_CIRCLE { get { return Culture.Language.EditorConstants.STATUS_DESC_CIRCLE; } }

        /// <summary>
        /// Pencil
        /// </summary>
        public string STATUS_DESC_POINTER { get { return Culture.Language.EditorConstants.STATUS_DESC_POINTER; } }

        /// <summary>
        /// Import mask
        /// </summary>
        public string STATUS_DESC_IMPORTMASK { get { return Culture.Language.EditorConstants.STATUS_DESC_IMPORTMASK; } }

        /// <summary>
        /// Zoom
        /// </summary>
        public string STATUS_DESC_ZOOM { get { return Culture.Language.EditorConstants.STATUS_DESC_ZOOM; } }

        /// <summary>
        /// Zoom 1:1
        /// </summary>
        public string STATUS_DESC_ZOOM11 { get { return Culture.Language.EditorConstants.STATUS_DESC_ZOOM11; } }

        /// <summary>
        /// Zoom 1:2
        /// </summary>
        public string STATUS_DESC_ZOOM12 { get { return Culture.Language.EditorConstants.STATUS_DESC_ZOOM12; } }

        /// <summary>
        /// Zoom 1:4
        /// </summary>
        public string STATUS_DESC_ZOOM14 { get { return Culture.Language.EditorConstants.STATUS_DESC_ZOOM14; } }

        /// <summary>
        /// Zoom 1:8
        /// </summary>
        public string STATUS_DESC_ZOOM18 { get { return Culture.Language.EditorConstants.STATUS_DESC_ZOOM18; } }

        /// <summary>
        /// Resources Tab
        /// </summary>
        public string STATUS_DESC_RESOURCES { get { return Culture.Language.EditorConstants.STATUS_DESC_RESOURCES; } }

        /// <summary>
        /// Create Map
        /// </summary>
        public string STATUS_DESC_CREATEMAP { get { return Culture.Language.EditorConstants.STATUS_DESC_CREATEMAP; } }

        /// <summary>
        /// Database
        /// </summary>
        public string STATUS_DESC_DATABASE { get { return Culture.Language.EditorConstants.STATUS_DESC_DATABASE; }  }

        /// <summary>
        /// Resources Manager
        /// </summary>
        public string STATUS_DESC_RESOURCESMANAGER { get { return Culture.Language.EditorConstants.STATUS_DESC_RESOURCESMANAGER; } }

        /// <summary>
        /// Resources Manager
        /// </summary>
        public string STATUS_DESC_SPRITECREATOR { get { return Culture.Language.EditorConstants.STATUS_DESC_SPRITECREATOR; } }

        /// <summary>
        /// Build
        /// </summary>
        public string STATUS_DESC_BUILD { get { return Culture.Language.EditorConstants.STATUS_DESC_BUILD; } }

        /// <summary>
        /// Export to Windows
        /// </summary>
        public string STATUS_DESC_BUILDTOWINDOWS { get { return Culture.Language.EditorConstants.STATUS_DESC_BUILDTOWINDOWS; } }

        /// <summary>
        /// Game
        /// </summary>
        public string STATUS_DESC_GAME { get { return Culture.Language.EditorConstants.STATUS_DESC_GAME; } }

        /// <summary>
        /// Try
        /// </summary>
        public string STATUS_DESC_TRY { get { return Culture.Language.EditorConstants.STATUS_DESC_TRY; } }

        /// <summary>
        /// Try fullscreen
        /// </summary>
        public string STATUS_DESC_FULLSCREEN_TRY { get { return Culture.Language.EditorConstants.STATUS_DESC_FULLSCREEN_TRY; } }

        /// <summary>
        /// About
        /// </summary>
        public string STATUS_DESC_ABOUT { get { return Culture.Language.EditorConstants.STATUS_DESC_ABOUT; } }

        /// <summary>
        /// About
        /// </summary>
        public string STATUS_DESC_LANGUAGE { get { return Culture.Language.EditorConstants.STATUS_DESC_LANGUAGE; } }

        /// <summary>
        /// About us
        /// </summary>
        public string STATUS_DESC_ABOUTUS { get { return Culture.Language.EditorConstants.STATUS_DESC_ABOUTUS; } }

        /// <summary>
        /// Documentation
        /// </summary>
        public string STATUS_DESC_DOCUMENT { get { return Culture.Language.EditorConstants.STATUS_DESC_DOCUMENT; } }

        /// <summary>
        /// Website
        /// </summary>
        public string STATUS_DESC_WEBSITE { get { return Culture.Language.EditorConstants.STATUS_DESC_WEBSITE; } }

        /// <summary>
        /// Information Panel
        /// </summary>
        public string STATUS_DESC_INFOPANEL { get { return Culture.Language.EditorConstants.STATUS_DESC_INFOPANEL; } }

        /// <summary>
        /// Stage
        /// </summary>
        public string STATUS_DESC_STAGE { get { return Culture.Language.EditorConstants.STATUS_DESC_STAGE; } }

        /// <summary>
        /// Project Panel
        /// </summary>
        public string STATUS_DESC_PROJECTPANEL { get { return Culture.Language.EditorConstants.STATUS_DESC_PROJECTPANEL; } }
        
        /// <summary>
        /// Layers Panel
        /// </summary>
        public string STATUS_DESC_LAYERSPANEL { get { return Culture.Language.EditorConstants.STATUS_DESC_LAYERSPANEL; } }

        /// <summary>
        /// Preview Panel
        /// </summary>
        public string STATUS_DESC_PREVIEWPANEL { get { return Culture.Language.EditorConstants.STATUS_DESC_PREVIEWPANEL; } }

        /// <summary>
        /// StageObjects Panel
        /// </summary>
        public string STATUS_DESC_STAGEOBJECTSPANEL { get { return Culture.Language.EditorConstants.STATUS_DESC_STAGEOBJECTSPANEL; } }
        #endregion

        #region Terminology
        /// <summary>
        /// Nouveau jeu
        /// </summary>
        public string TERM_NEWGAME { get { return Culture.Language.EditorConstants.TERM_NEWGAME; } }

        /// <summary>
        /// Charger un jeu
        /// </summary>
        public string TERM_LOADGAME { get { return Culture.Language.EditorConstants.TERM_LOADGAME; } }

        /// <summary>
        /// Options
        /// </summary>
        public string TERM_OPTIONS { get { return Culture.Language.EditorConstants.TERM_OPTIONS; } }

        /// <summary>
        /// Quitter le jeu
        /// </summary>
        public string TERM_LEAVEGAME { get { return Culture.Language.EditorConstants.TERM_LEAVEGAME; } }

        /// <summary>
        /// Retourner à l'écran titre
        /// </summary>
        public string TERM_RETURNTITLE { get { return Culture.Language.EditorConstants.TERM_RETURNTITLE; } }

        /// <summary>
        /// Sauvegarder le jeu
        /// </summary>
        public string TERM_SAVEGAME { get { return Culture.Language.EditorConstants.TERM_SAVEGAME; } }

        /// <summary>
        /// Save State
        /// </summary>
        public string TERM_SAVESTATE { get { return Culture.Language.EditorConstants.TERM_SAVESTATE; } }

        /// <summary>
        /// Choix suivants
        /// </summary>
        public string TERM_CHOICENEXT { get { return Culture.Language.EditorConstants.TERM_CHOICENEXT; } }

        /// <summary>
        /// Choix précédents
        /// </summary>
        public string TERM_CHOICEPREVIOUS { get { return Culture.Language.EditorConstants.TERM_CHOICEPREVIOUS; } }
        #endregion

        #region EventManager
        /// <summary>
        /// Pas de classe
        /// </summary>
        public string EVENTMANAGER_NO_CLASSES { get { return Culture.Language.EditorConstants.EVENTMANAGER_NO_CLASSES; } }
        #endregion 

        #region Export
        /// <summary>
        /// Export OK
        /// </summary>
        public string EXPORT_SUCCESS { get { return Culture.Language.EditorConstants.EXPORT_SUCCESS; } }
        #endregion
        #endregion

        #region Constants
        #region Performances
        /// <summary>
        /// Nbr de pixels editeur
        /// </summary>
        public const PixelFormat PERF_EDITOR_BITSPERPIXEL = PixelFormat.Format32bppArgb;
        #endregion

        #region Script Wait

        public const int WAIT_MIN_VALUE = 1;

        public const int WAIT_MAX_VALUE = 1000;

        #endregion

        #region Config
        public const string CONFIG_KEY_GAMEFOLDER = "GamesFolder";
        public const string CONFIG_KEY_VSYNC = "Vsync";
        public const string CONFIG_KEY_VIEWERPATH = "ViewerPath";
        public const string CONFIG_KEY_LANGUAGE = "Language";
        public const string CONFIG_KEY_TRANSPARENTBLOCKSIZE = "TransparentBlockSize";
        public const string CONFIG_KEY_TRANSPARENTCOLOR1 = "TransparentColor1";
        public const string CONFIG_KEY_TRANSPARENTCOLOR2 = "TransparentColor2";
        public const string CONFIG_KEY_HIGHLIGHTNINGCOLOR = "HighlightningColor";
        public const string CONFIG_KEY_HIGHLIGHTNINGBRUSHCOLOR = "HighlightningBrushColor";
        public const string CONFIG_KEY_SELECTEDCOORDSPOTCOLOR = "SelectedCoordSpotColor";
        public const string CONFIG_KEY_SELECTIONCOORDS = "SelectionCoords";
        public const string CONFIG_KEY_ANIMATIONFREQUENCY = "AnimationFrequency";
        public const string CONFIG_KEY_STAGEPADDING = "StagePadding";
        public const string CONFIG_KEY_MESSAGEDURATION = "MessageDuration";
        public const string CONFIG_KEY_MESSAGEFONTSIZE = "MessageFontSize";
        public const string CONFIG_KEY_SHOWANIMWHILEMASKING = "ShowAnimationsWhileMasking";
        public const string CONFIG_KEY_SHOWCHARWHILEMASKING = "ShowCharactersWhileMasking";
        public const string CONFIG_KEY_SELECTEDHOTSPOTCOLOR = "SelectedHotSpotColor";
        public const string CONFIG_KEY_VECTORPOINTSSIZE = "VectorPointsSize";
        public const string CONFIG_KEY_ACTIVATEZOOMWITHWHEEL = "ActivateZoomWithWheel";
        public const string CONFIG_APPSETTINGS = "appSettings";
        #endregion

        #region External Tools
        public const string PATH_SCRIPT_CREATOR = "\\Tools\\PCSpriteCreator.exe";
        #endregion
        #endregion
    }
}
