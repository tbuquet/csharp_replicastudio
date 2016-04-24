using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.Constants
{
    /// <summary>
    /// Constantes de logs
    /// </summary>
    public class Logs
    {
        /// <summary>
        /// Nom du fichier de log
        /// </summary>
        public const string LOG_FILENAME = "log.config";

        #region Service
        /// <summary>
        /// DEBUG entrée de méthode
        /// </summary>
        public const string SERVICE_DEBUG_ENTERING_METHOD = "SERVICE | Entering method: {0}({1})";
        #endregion

        #region Managers
        /// <summary>
        /// Image créée
        /// </summary>
        public const string MANAGER_IMAGE_CREATED = "MANAGER | Created Image {0} in {1}";

        /// <summary>
        /// Texte créé
        /// </summary>
        public const string MANAGER_TEXT_CREATED = "MANAGER | Created Text {0} in {1}";

        /// <summary>
        /// Font créé
        /// </summary>
        public const string MANAGER_FONT_CREATED = "MANAGER | Created Font {0}";

        /// <summary>
        /// Sprite créée
        /// </summary>
        public const string MANAGER_SPRITE_CREATED = "MANAGER | Create Sprite {0} in {1}";

        /// <summary>
        /// Resources
        /// </summary>
        public const string MANAGER_RESOURCES = "Resources";

        /// <summary>
        /// Permanent image
        /// </summary>
        public const string MANAGER_PERMANENT = "Permanent Image";

        /// <summary>
        /// Colored Image
        /// </summary>
        public const string MANAGER_COLORED = "Colored Image";

        /// <summary>
        /// Screen image
        /// </summary>
        public const string MANAGER_SCREEN = "Screen Image";

        /// <summary>
        /// Fleeting image
        /// </summary>
        public const string MANAGER_FLEETING = "Fleeting Image";

        /// <summary>
        /// Backgrounds
        /// </summary>
        public const string MANAGER_BACKGROUNDS = "Backgrounds";

        /// <summary>
        /// Décors
        /// </summary>
        public const string MANAGER_DECORS = "Decors";

        /// <summary>
        /// Animations
        /// </summary>
        public const string MANAGER_ANIMS = "Animations";

        /// <summary>
        /// Characters
        /// </summary>
        public const string MANAGER_CHARS = "Characters";

        /// <summary>
        /// Pas assez de mémoire
        /// </summary>
        public const string MANAGER_MEMORY_ERROR = "MANAGER | Not enough memory to create the image object...";

        /// <summary>
        /// Reset du ImageManager
        /// </summary>
        public const string MANAGER_RESETING_MANAGER = "MANAGER | Reseting the Image Manager...";

        /// <summary>
        /// Image not found
        /// </summary>
        public const string MANAGER_IMAGE_NOT_FOUND = "MANAGER | Image was not found: {0}";

        /// <summary>
        /// Character not loaded properly
        /// </summary>
        public const string MANAGER_CHARACTER_NOT_LOADED = "MANAGER | Character wasn't loaded properly: {0}";

        /// <summary>
        /// Text not found
        /// </summary>
        public const string MANAGER_TEXT_NOT_FOUND = "MANAGER | Text was not found: {0}";

        /// <summary>
        /// Font not found
        /// </summary>
        public const string MANAGER_FONT_NOT_FOUND = "MANAGER | Font was not found: {0}";

        /// <summary>
        /// Sprite not found
        /// </summary>
        public const string MANAGER_SPRITE_NOT_FOUND = "MANAGER | Sprite was not found: {0}";

        /// <summary>
        /// Image key not found
        /// </summary>
        public const string MANAGER_IMAGE_KEY_NOT_FOUND = "MANAGER | Image key was not found: {0}";

        /// <summary>
        /// Character key not found
        /// </summary>
        public const string MANAGER_CHARACTER_KEY_NOT_FOUND = "MANAGER | Character key was not found: {0}";

        /// <summary>
        /// Text key not found
        /// </summary>
        public const string MANAGER_TEXT_KEY_NOT_FOUND = "MANAGER | Text key was not found: {0}";

        /// <summary>
        /// Sprite key not found
        /// </summary>
        public const string MANAGER_SPRITE_KEY_NOT_FOUND = "MANAGER | Sprite key was not found: {0}";

        /// <summary>
        /// Image cannot be added
        /// </summary>
        public const string MANAGER_IMAGE_CANNOT_BE_ADDED = "Image cannot be added: {0}";
        #endregion
    }
}
