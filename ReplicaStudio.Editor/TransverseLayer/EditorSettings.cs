using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Editor;
using System.Windows.Forms;
using ReplicaStudio.Editor.Forms;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using System.Drawing;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Xml.Serialization;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Xml;
using System.Configuration;

namespace ReplicaStudio.Editor.TransverseLayer
{
    /// <summary>
    /// Gestionnaire de formulaires utiles à toute l'application
    /// </summary>
    class EditorSettings
    {
        #region Members
        /// <summary>
        /// Instance singleton
        /// </summary>
        private static EditorSettings _Instance;
        #endregion

        #region Properties
        /// <summary>
        /// Instance singleton
        /// </summary>
        public static EditorSettings Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EditorSettings();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Couleur du brush
        /// </summary>
        public Color HighlightningBrushColor { get; set; }

        #region Paramètres
        /// <summary>
        /// Active ou désactive la synchro verticale
        /// </summary>
        public bool VSync { get; set; }

        /// <summary>
        /// Active ou désactive le debug
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// Chemin vers le viewer
        /// </summary>
        public string ViewerPath { get; set; }

        /// <summary>
        /// Dossier des projets
        /// </summary>
        public string GamesFolder { get; set; }

        /// <summary>
        /// Taille d'un bloc transparent
        /// </summary>
        public int TransparentBlockSize { get; set; }

        /// <summary>
        /// Couleur du bloc transparent 1
        /// </summary>
        public Color TransparentColor1 { get; set; }

        /// <summary>
        /// Couleur du bloc transparent 2
        /// </summary>
        public Color TransparentColor2 { get; set; }

        /// <summary>
        /// Couleur de surlignement
        /// </summary>
        public Pen HighlightningColor { get; set; }

        /// <summary>
        /// Couleur de remplissage surligné
        /// </summary>
        public Brush HighlightningBrush { get; set; }

        /// <summary>
        /// Couleur de selection de coordonnées
        /// </summary>
        public Pen SelectionCoords { get; set; }

        /// <summary>
        /// Couleur du point HotSpot en mode sélectioné
        /// </summary>
        public Pen SelectedHotSpotColor { get; set; }

        /// <summary>
        /// Taille des poignées des vecteurs
        /// </summary>
        public int VectorPointsSize { get; set; }

        /// <summary>
        /// Fréquence d'animation par défaut
        /// </summary>
        public int AnimationFrequency { get; set; }

        /// <summary>
        /// Padding de la scène
        /// </summary>
        public int StagePadding { get; set; }

        /// <summary>
        /// Durée des messages (secondes)
        /// </summary>
        public int MessageDuration { get; set; }

        /// <summary>
        /// Taille de la police des messages
        /// </summary>
        public int MessageFontSize { get; set; }

        /// <summary>
        /// Afficher la couche animation avec les masques
        /// </summary>
        public bool ShowAnimationsWhileMasking { get; set; }

        /// <summary>
        /// Afficher la couche characters avec les masques
        /// </summary>
        public bool ShowCharactersWhileMasking { get; set; }

        /// <summary>
        /// Activer le zoom à la roulette
        /// </summary>
        public bool ActivateZoomWithWheel { get; set; }

        /// <summary>
        /// Langue
        /// </summary>
        public string Language { get; set; }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        private EditorSettings()
        {
            LoadEditorSettings();
        }
        #endregion

        #region Methods
        #region Get
        /// <summary>
        /// Récupérer une valeur des settings
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetValue(string key, string defaultValue)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add(key, defaultValue);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(EditorConstants.CONFIG_APPSETTINGS);
                return defaultValue;
            }
            return ConfigurationManager.AppSettings[key];
        }
        public string GetValue(string key, bool defaultValue)
        {
            return GetValue(key, defaultValue.ToString());
        }
        public string GetValue(string key, int defaultValue)
        {
            return GetValue(key, defaultValue.ToString());
        }
        public string GetValue(string key, Color defaultValue)
        {
            return GetValue(key, string.Format("{0};{1};{2};{3}", defaultValue.A, defaultValue.R, defaultValue.G, defaultValue.B));
        }
        public string GetValue(VO_Color defaultValue)
        {
            return string.Format("{0};{1};{2};{3}", defaultValue.A, defaultValue.R, defaultValue.G, defaultValue.B);
        }
        public Color GetColorFromValue(string value)
        {
            string[] values = value.Split(";".ToCharArray());
            return Color.FromArgb(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToInt32(values[3]));
        }
        #endregion

        #region Update
        private void UpdateValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(EditorConstants.CONFIG_APPSETTINGS);
        }
        private void UpdateValue(string key, VO_Color value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, GetValue(value));
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(EditorConstants.CONFIG_APPSETTINGS);
        }
        #endregion

        /// <summary>
        /// Charger config file
        /// </summary>
        public void LoadEditorSettings()
        {
            Debug = GeneralSettingsConstants.DEFAULT_DEBUG;

            //GamesFolder
            GamesFolder = GetValue(EditorConstants.CONFIG_KEY_GAMEFOLDER, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + GeneralSettingsConstants.DEFAULT_FOLDER);
            
            //VSync
            try
            {
                VSync = Convert.ToBoolean(GetValue(EditorConstants.CONFIG_KEY_VSYNC, GeneralSettingsConstants.DEFAULT_TRY_VSYNC));
            }
            catch
            {
                VSync = GeneralSettingsConstants.DEFAULT_TRY_VSYNC;
            }

            //ViewerPath
            ViewerPath = GetValue(EditorConstants.CONFIG_KEY_VIEWERPATH, Application.StartupPath + "\\" + GlobalConstants.VIEWER_NAME);

            //Language
            Language = GetValue(EditorConstants.CONFIG_KEY_LANGUAGE, GeneralSettingsConstants.DEFAULT_LANGUAGE);

            //TransparentBlockSize
            try
            {
                TransparentBlockSize = Convert.ToInt32(GetValue(EditorConstants.CONFIG_KEY_TRANSPARENTBLOCKSIZE, GeneralSettingsConstants.DEFAULT_TRANSPARENT_BLOCK_SIZE));
            }
            catch
            {
                TransparentBlockSize = GeneralSettingsConstants.DEFAULT_TRANSPARENT_BLOCK_SIZE;
            }

            //TransparentColor1
            try
            {
                TransparentColor1 = GetColorFromValue(GetValue(EditorConstants.CONFIG_KEY_TRANSPARENTCOLOR1, GeneralSettingsConstants.DEFAULT_TRANSPARENT_COLOR_1));
            }
            catch
            {
                TransparentColor1 = GeneralSettingsConstants.DEFAULT_TRANSPARENT_COLOR_1;
            }

            //TransparentColor2
            try
            {
                TransparentColor2 = GetColorFromValue(GetValue(EditorConstants.CONFIG_KEY_TRANSPARENTCOLOR2, GeneralSettingsConstants.DEFAULT_TRANSPARENT_COLOR_2));
            }
            catch
            {
                TransparentColor2 = GeneralSettingsConstants.DEFAULT_TRANSPARENT_COLOR_2;
            }

            //HighlightningColor
            try
            {
                HighlightningColor = new Pen(GetColorFromValue(GetValue(EditorConstants.CONFIG_KEY_HIGHLIGHTNINGCOLOR, GeneralSettingsConstants.DEFAULT_HIGHLIGHTING_COLOR.Color)));
            }
            catch
            {
                HighlightningColor = GeneralSettingsConstants.DEFAULT_HIGHLIGHTING_COLOR;
            }

            //SelectionCoords
            try
            {
                SelectionCoords = new Pen(GetColorFromValue(GetValue(EditorConstants.CONFIG_KEY_SELECTEDCOORDSPOTCOLOR, GeneralSettingsConstants.DEFAULT_SELECTION_COORDS.Color)));
            }
            catch
            {
                SelectionCoords = GeneralSettingsConstants.DEFAULT_SELECTION_COORDS;
            }

            //HighlightningBrushColor
            try
            {
                HighlightningBrushColor = GetColorFromValue(GetValue(EditorConstants.CONFIG_KEY_HIGHLIGHTNINGBRUSHCOLOR, GeneralSettingsConstants.DEFAULT_HIGHLIGHTING_COLOR_BRUSH_COLOR));
            }
            catch
            {
                HighlightningBrushColor = GeneralSettingsConstants.DEFAULT_HIGHLIGHTING_COLOR_BRUSH_COLOR;
            }
            HighlightningBrush = new SolidBrush(HighlightningBrushColor);

            //AnimationFrequency
            try
            {
                AnimationFrequency = Convert.ToInt32(GetValue(EditorConstants.CONFIG_KEY_ANIMATIONFREQUENCY, GeneralSettingsConstants.DEFAULT_FREQUENCY));
            }
            catch
            {
                AnimationFrequency = GeneralSettingsConstants.DEFAULT_FREQUENCY;
            }

            //StagePadding
            try
            {
                StagePadding = Convert.ToInt32(GetValue(EditorConstants.CONFIG_KEY_STAGEPADDING, GeneralSettingsConstants.DEFAULT_STAGE_PADDING));
            }
            catch
            {
                StagePadding = GeneralSettingsConstants.DEFAULT_STAGE_PADDING;
            }

            //MessageDuration
            try
            {
                MessageDuration = Convert.ToInt32(GetValue(EditorConstants.CONFIG_KEY_MESSAGEDURATION, GeneralSettingsConstants.DEFAULT_MESSAGE_DURATION));
            }
            catch
            {
                MessageDuration = GeneralSettingsConstants.DEFAULT_MESSAGE_DURATION;
            }

            //MessageFontSize
            try
            {
                MessageFontSize = Convert.ToInt32(GetValue(EditorConstants.CONFIG_KEY_MESSAGEFONTSIZE, GeneralSettingsConstants.DEFAULT_MESSAGE_FONTSIZE));
            }
            catch
            {
                MessageFontSize = GeneralSettingsConstants.DEFAULT_MESSAGE_FONTSIZE;
            }

            //ShowAnimationsWhileMasking
            try
            {
                ShowAnimationsWhileMasking = Convert.ToBoolean(GetValue(EditorConstants.CONFIG_KEY_SHOWANIMWHILEMASKING, GeneralSettingsConstants.DEFAULT_SHOW_ANIMATIONS_DURING_MASKS));
            }
            catch
            {
                ShowAnimationsWhileMasking = GeneralSettingsConstants.DEFAULT_SHOW_ANIMATIONS_DURING_MASKS;
            }

            //ShowCharactersWhileMasking
            try
            {
                ShowCharactersWhileMasking = Convert.ToBoolean(GetValue(EditorConstants.CONFIG_KEY_SHOWCHARWHILEMASKING, GeneralSettingsConstants.DEFAULT_SHOW_CHARACTERS_DURING_MASKS));
            }
            catch
            {
                ShowCharactersWhileMasking = GeneralSettingsConstants.DEFAULT_SHOW_CHARACTERS_DURING_MASKS;
            }

            //SelectedHotSpotColor
            try
            {
                SelectedHotSpotColor = new Pen(GetColorFromValue(GetValue(EditorConstants.CONFIG_KEY_SELECTEDHOTSPOTCOLOR, GeneralSettingsConstants.DEFAULT_SELECTEDHOTSPOT_COLOR)));
            }
            catch
            {
                SelectedHotSpotColor = new Pen(GeneralSettingsConstants.DEFAULT_SELECTEDHOTSPOT_COLOR);
            }

            //VectorPointsSize
            try
            {
                VectorPointsSize = Convert.ToInt32(GetValue(EditorConstants.CONFIG_KEY_VECTORPOINTSSIZE, GeneralSettingsConstants.VECTOR_POINT_SIZE));
            }
            catch
            {
                VectorPointsSize = GeneralSettingsConstants.VECTOR_POINT_SIZE;
            }

            //ActivateZoomWithWheel
            try
            {
                ActivateZoomWithWheel = Convert.ToBoolean(GetValue(EditorConstants.CONFIG_KEY_ACTIVATEZOOMWITHWHEEL, GeneralSettingsConstants.ACTIVATE_ZOOM_WHEEL));
            }
            catch
            {
                ActivateZoomWithWheel = GeneralSettingsConstants.ACTIVATE_ZOOM_WHEEL;
            }
        }

        /// <summary>
        /// Save config file
        /// </summary>
        /// <param name="settings"></param>
        public void SaveSettings(VO_EditorSettings settings)
        {
            UpdateValue(EditorConstants.CONFIG_KEY_ACTIVATEZOOMWITHWHEEL, settings.ActivateZoomWithWheel.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_ANIMATIONFREQUENCY, settings.AnimationFrequency.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_GAMEFOLDER, settings.GamesFolder.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_HIGHLIGHTNINGCOLOR, settings.HighlightningColor);
            UpdateValue(EditorConstants.CONFIG_KEY_HIGHLIGHTNINGBRUSHCOLOR, settings.HighlightningBrush);
            UpdateValue(EditorConstants.CONFIG_KEY_MESSAGEDURATION, settings.MessageDuration.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_MESSAGEFONTSIZE, settings.MessageFontSize.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_SELECTEDCOORDSPOTCOLOR, settings.SelectionCoords);
            UpdateValue(EditorConstants.CONFIG_KEY_SELECTEDHOTSPOTCOLOR, settings.SelectedHotSpotColor);
            UpdateValue(EditorConstants.CONFIG_KEY_SHOWANIMWHILEMASKING, settings.ShowAnimationsWhileMasking.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_SHOWCHARWHILEMASKING, settings.ShowCharactersWhileMasking.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_STAGEPADDING, settings.StagePadding.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_TRANSPARENTBLOCKSIZE, settings.TransparentBlockSize.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_TRANSPARENTCOLOR1, settings.TransparentColor1);
            UpdateValue(EditorConstants.CONFIG_KEY_TRANSPARENTCOLOR2, settings.TransparentColor2);
            UpdateValue(EditorConstants.CONFIG_KEY_VECTORPOINTSSIZE, settings.VectorPointsSize.ToString());
            UpdateValue(EditorConstants.CONFIG_KEY_VIEWERPATH, settings.ViewerPath);
            UpdateValue(EditorConstants.CONFIG_KEY_VSYNC, settings.VSync.ToString());
        }

        /// <summary>
        /// Save config file
        /// </summary>
        /// <param name="settings"></param>
        public void SaveSettings(string language)
        {
            UpdateValue(EditorConstants.CONFIG_KEY_LANGUAGE, language);
        }
        #endregion
    }
}
