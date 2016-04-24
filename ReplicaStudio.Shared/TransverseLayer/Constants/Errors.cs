using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.Constants
{
    public class Errors
    {
        #region Général
        /// <summary>
        /// Titre de message d'une erreur
        /// </summary>
        public const string ERROR_BOX_TITLE = "Error";

        /// <summary>
        /// Titre du message d'erreur de titre unique
        /// </summary>
        public const string ERROR_UNIQUE_TITLE = "An item title must be unique.";

        /// <summary>
        /// Nom d'un item récupéré avec erreur.
        /// </summary>
        public const string ERROR_ITEM_NAME = "#ERROR#";

        /// <summary>
        /// Message d'erreur de delete de VO
        /// </summary>
        public const string ERROR_DELETE_VO = "Error while deleting VO_";

        /// <summary>
        /// Message d'erreur d'update de VO
        /// </summary>
        public const string ERROR_UPDATE_VO = "Error while updating VO_";

        /// <summary>
        /// Erreur du méthode
        /// </summary>
        public const string ERROR_METHOD = "Error on method ";

        /// <summary>
        /// Erreur utilisateur chargement liste d'items
        /// </summary>
        public const string ERROR_STR_LIST_PROVISION = "The items list cannot be loaded";

        /// <summary>
        /// Erreur utilisateur sauvegarde db
        /// </summary>
        public const string ERROR_STR_DBSAVE = "The database cannot be saved";

        /// <summary>
        /// Erreur utilisateur restoration db
        /// </summary>
        public const string ERROR_STR_DBRESTORE = "The database cannot be restored";
        #endregion

        #region Project
        /// <summary>
        /// Message d'erreur en cas de titre de projet vide
        /// </summary>
        public const string PROJECT_TITLE_EMPTY = "Title is empty.";

        /// <summary>
        /// Message d'erreur en cas de titre de scène vide
        /// </summary>
        public const string STAGE_TITLE_EMPTY = "Stage title is empty.";

        /// <summary>
        /// Message d'erreur du dossier projet incorrecte
        /// </summary>
        public const string PROJECT_FOLDER_INCORRECT = "Project folder is incorrect.";

        /// <summary>
        /// Erreur utilisateur chargement décors
        /// </summary>
        public const string ERROR_PROJECT_STR_LOAD_DECORS = "Cannot get list of decors resources";

        /// <summary>
        /// Erreur utilisateur chargement musiques
        /// </summary>
        public const string ERROR_PROJECT_STR_LOAD_MUSICS = "Cannot get list of musics resources";

        /// <summary>
        /// Erreur utilisateur création projet
        /// </summary>
        public const string ERROR_PROJECT_STR_CREATE = "The project cannot be created";

        /// <summary>
        /// Erreur utilisateur création projet
        /// </summary>
        public const string ERROR_PROJECT_STR_EXPORTED = "The project cannot be exported";

        /// <summary>
        /// Erreur utilisateur sauvegarde projet
        /// </summary>
        public const string ERROR_PROJECT_STR_SAVE = "The project cannot be saved";

        /// <summary>
        /// Erreur utilisateur chargement projet
        /// </summary>
        public const string ERROR_PROJECT_STR_LOAD = "The project cannot be loaded";

        /// <summary>
        /// Erreur lancement projet
        /// </summary>
        public const string ERROR_PROJECT_LAUNCH = "The project cannot be launched. Be sure to have set the " + GlobalConstants.VIEWER_NAME + " path.";
        #endregion

        #region Layers
        /// <summary>
        /// Message d'erreur en cas de titre de projet vide
        /// </summary>
        public const string LAYER_TITLE_EMPTY = "Title is empty.";

        /// <summary>
        /// Message d'erreur en cas de limite d'utilisation des calques
        /// </summary>
        public const string LAYER_LIMIT = "You can't use more than {0} layers for a single stage";

        /// <summary>
        /// Erreur utilisateur chargement calques
        /// </summary>
        public const string ERROR_LAYER_STR_LOAD = "Layers cannot be loaded";

        /// <summary>
        /// Erreur utilisateur création calque
        /// </summary>
        public const string ERROR_LAYER_STR_CREATE = "A layer cannot be created";

        /// <summary>
        /// Erreur utilisateur suppression calque
        /// </summary>
        public const string ERROR_LAYER_STR_DELETE = "A layer cannot be deleted";
        #endregion

        #region Animations
        /// <summary>
        /// Erreur utilisateur chargement animation
        /// </summary>
        public const string ERROR_ANIMATION_STR_LOAD = "An animation cannot be loaded";

        /// <summary>
        /// Erreur utilisateur sauvegarde db animation
        /// </summary>
        public const string ERROR_ANIMATION_STR_DBSAVE = "The animations database cannot be saved";

        /// <summary>
        /// Erreur utilisateur restoration db animation
        /// </summary>
        public const string ERROR_ANIMATION_STR_DBRESTORE = "The animations database cannot be restored";

        /// <summary>
        /// Erreur utilisateur création d'animation
        /// </summary>
        public const string ERROR_ANIMATION_STR_CREATE = "Error during animation creation";

        /// <summary>
        /// Lien vers la ressource image d'erreur.
        /// </summary>
        public const string ERROR_ANIMATION_RESOURCE = "ReplicaStudio.Editor.Resources.notfound.png";

        /// <summary>
        /// Largeur de l'image d'erreur
        /// </summary>
        public const int ERROR_ANIMATION_RESOURCE_WIDTH = 1;

        /// <summary>
        /// Hauteur de l'image d'erreur
        /// </summary>
        public const int ERROR_ANIMATION_RESOURCE_HEIGHT = 1;
        #endregion

        #region Stage
        /// <summary>
        /// Max décors
        /// </summary>
        public const string STAGE_MAX_DECORS = "The decor can't be added because you can't add more than {0} decors per layer.";

        /// <summary>
        /// Max characters
        /// </summary>
        public const string STAGE_MAX_CHARACTERS = "The character can't be added because you can't add more than {0} characters per layer.";

        /// <summary>
        /// Max animations
        /// </summary>
        public const string STAGE_MAX_ANIMATIONS = "The animations can't be added because you can't add more than {0} animations per layer.";

        /// <summary>
        /// Max hotspot
        /// </summary>
        public const string STAGE_MAX_HOTSPOT = "The hotspot can't be added because you can't add more than {0} hotspots per stage.";

        /// <summary>
        /// Max walk
        /// </summary>
        public const string STAGE_MAX_WALK = "The walkable area can't be added because you can't add more than {0} walkable areas per stage.";

        /// <summary>
        /// Max region
        /// </summary>
        public const string STAGE_MAX_REGION = "The region can't be added because you can't add more than {0} regions per stage.";

        /// <summary>
        /// Erreur utilisateur chargement scène
        /// </summary>
        public const string ERROR_STAGE_STR_LOAD = "Stage cannot be rendered";
        #endregion

        #region Coords
        /// <summary>
        /// Erreur utilisateur chargement background
        /// </summary>
        public const string ERROR_CORDS_STR_LOAD = "Background cannot be rendered";
        #endregion

        #region Characters
        /// <summary>
        /// Erreur utilisateur création de character
        /// </summary>
        public const string ERROR_CHARACTER_STR_CREATE = "A character cannot be created";
        #endregion

        #region Items
        /// <summary>
        /// Erreur utilisateur création de item
        /// </summary>
        public const string ERROR_ITEM_STR_CREATE = "An item cannot be created";

        /// <summary>
        /// Erreur suppression action "Aller"
        /// </summary>
        public const string ERROR_ITEM_DELETION = "Sorry but you cannot delete this action";
        #endregion

        #region Actions
        /// <summary>
        /// Erreur utilisateur création de action
        /// </summary>
        public const string ERROR_ACTION_STR_CREATE = "An action cannot be created";
        #endregion

        #region Events
        /// <summary>
        /// Erreur utilisateur création d'Event
        /// </summary>
        public const string ERROR_EVENT_STR_CREATE = "An Event cannot be created";
        #endregion

        #region Classes
        /// <summary>
        /// Erreur utilisateur création de classe
        /// </summary>
        public const string ERROR_CLASS_STR_CREATE = "A class cannot be created";

        /// <summary>
        /// Erreur création bad interaction
        /// </summary>
        public const string ERROR_CLASS_BADINTERACTION_CREATE = "A bad interaction line cannot be created";
        #endregion

        #region Dialogs
        /// <summary>
        /// Erreur utilisateur création de dialogue
        /// </summary>
        public const string ERROR_DIALOG_STR_CREATE = "A dialog cannot be created";

        /// <summary>
        /// Erreur utilisateur création de message
        /// </summary>
        public const string ERROR_MESSAGE_STR_CREATE = "A message cannot be created";
        #endregion

        #region GlobalEvents
        /// <summary>
        /// Erreur utilisateur création d'evenement global
        /// </summary>
        public const string ERROR_GLOBALEVENT_STR_CREATE = "A global event cannot be created";
        #endregion

        #region Triggers
        /// <summary>
        /// Erreur utilisateur création de bouton
        /// </summary>
        public const string ERROR_TRIGGER_STR_CREATE = "A trigger cannot be created";

        /// <summary>
        /// Erreur utilisateur sauvegarde db triggers
        /// </summary>
        public const string ERROR_TRIGGER_STR_DBSAVE = "The triggers database cannot be saved";

        /// <summary>
        /// Erreur utilisateur restoration db triggers
        /// </summary>
        public const string ERROR_TRIGGER_STR_DBRESTORE = "The triggers database cannot be restored";
        #endregion

        #region Variables
        /// <summary>
        /// Erreur utilisateur création de variable
        /// </summary>
        public const string ERROR_VARIABLE_STR_CREATE = "A variable cannot be created";

        /// <summary>
        /// Erreur utilisateur sauvegarde db variables
        /// </summary>
        public const string ERROR_VARIABLE_STR_DBSAVE = "The variables database cannot be saved";

        /// <summary>
        /// Erreur utilisateur restoration db variables
        /// </summary>
        public const string ERROR_VARIABLE_STR_DBRESTORE = "The variables database cannot be restored";
        #endregion

        #region ResourcesManager
        /// <summary>
        /// Erreur utilisateur chargement ressources
        /// </summary>
        public const string ERROR_RESOURCESMANAGER_STR_LOAD = "Resources cannot be loaded";
        #endregion

        #region Export
        /// <summary>
        /// Error exportation
        /// </summary>
        public const string ERROR_EXPORT = "Project cannot be exported. Make sure you selected a valid character and a valid stage to start the game.";
        #endregion
    }
}
