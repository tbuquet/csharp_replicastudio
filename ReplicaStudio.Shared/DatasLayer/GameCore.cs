using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer.Saves;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ReplicaStudio.Shared.DatasLayer
{
    /// <summary>
    /// Classe Data principale
    /// </summary>
    public class GameCore
    {
        #region Members
        /// <summary>
        /// Instance singleton
        /// </summary>
        private static GameCore _Instance;
        #endregion

        #region Saves
        public GameCoreDBSave SAVEDB;
        public GameCoreEventSave SAVEEVENT;
        public GameCoreTriggerSave SAVETRIGGERS;
        public GameCoreVariableSave SAVEVARIABLES;
        public GameCoreAnimationSave SAVEANIM;
        #endregion

        #region Données
        public VO_Game Game { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// Instance singleton
        /// </summary>
        public static GameCore Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameCore();
                }
                return _Instance;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        private GameCore()
        {
            Game = new VO_Game();
            Game.Stages = new List<VO_Stage>();
            Game.Characters = new List<VO_Character>();
            Game.PlayableCharacters = new List<VO_PlayableCharacter>();
            Game.Items = new List<VO_Item>();
            Game.Actions = new List<VO_Action>();
            Game.Classes = new List<VO_Class>();
            Game.Triggers = new List<VO_Trigger>();
            Game.GlobalEvents = new List<VO_GlobalEvent>();
            Game.InteractionScripts = new List<VO_Script>();
            Game.Menu = new VO_Menu();
            Game.Project = new VO_Project();
            Game.Variables = new List<VO_Variable>();
            Game.Terminology = new VO_Terminology();
            Game.ObjectAnimations = new List<VO_Animation>();
            Game.CharFacesAnimations = new List<VO_Animation>();
            Game.IconsAnimations = new List<VO_Animation>();
            Game.MenusAnimations = new List<VO_Animation>();
        }
        #endregion

        #region Methods
        #region Static Methods
        /// <summary>
        /// Charger un projet
        /// </summary>
        /// <param name="pPath">Lien vers le fichier XML de chargement.</param>
        public bool LoadProject(string path)
        {
            Type[] ScriptTypes = AppTools.GetScriptTypes();

            XmlSerializer XML_Project = new XmlSerializer(typeof(VO_Game), ScriptTypes);

            XmlReader reader = new XmlTextReader(path);
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return false;

            //Récupération
            reader.ReadStartElement(XML.NODE_ROOT);
            Game = (VO_Game)XML_Project.Deserialize(reader);
            reader.ReadEndElement();

            Game.Project.RootPath =Path.GetDirectoryName(path) + "\\";
            Game.Project.ProjectFileName = Path.GetFileNameWithoutExtension(path);
            reader.Close();
            return true;
        }
        #endregion

        /// <summary>
        /// Reset le gamecore
        /// </summary>
        public void ResetGameCore()
        {
            _Instance = null;
        }

        #region Tools
        /// <summary>
        /// Sauvegarde les objets pour la DB
        /// </summary>
        public void SaveDB()
        {
            SAVEDB = new GameCoreDBSave();
            //ObjectAnimations
            foreach (VO_Animation anims in Game.ObjectAnimations)
            {
                SAVEDB.ObjectAnimations.Add(anims.Clone());
            }

            //Characters
            foreach (VO_Character chars in Game.Characters)
            {
                SAVEDB.Characters.Add(chars.Clone());
            }

            //PlayableCharacters
            foreach (VO_PlayableCharacter chars in Game.PlayableCharacters)
            {
                SAVEDB.PlayableCharacters.Add(chars.Clone());
            }

            //Items
            foreach (VO_Item item in Game.Items)
            {
                SAVEDB.Items.Add(item.Clone());
            }

            //Actions
            foreach (VO_Action action in Game.Actions)
            {
                SAVEDB.Actions.Add(action.Clone());
            }

            //Classes
            foreach (VO_Class classe in Game.Classes)
            {
                SAVEDB.Classes.Add(classe.Clone());
            }

            //Triggers
            foreach (VO_Trigger trigger in Game.Triggers)
            {
                SAVEDB.Triggers.Add(trigger.Clone());
            }

            //Variables
            foreach (VO_Variable variable in Game.Variables)
            {
                SAVEDB.Variables.Add(variable.Clone());
            }

            //GlobalEvents
            foreach (VO_GlobalEvent globalEvent in Game.GlobalEvents)
            {
                SAVEDB.GlobalEvents.Add(globalEvent.Clone());
            }

            //Scripts
            foreach (VO_Script script in Game.InteractionScripts)
            {
                SAVEDB.InteractionScripts.Add(script);
            }

            //Menus
            SAVEDB.Menu = Game.Menu.Clone();

            //Projets
            SAVEDB.Project = Game.Project.Clone();

            //Terminology
            SAVEDB.Terminology = Game.Terminology.Clone();
        }

        /// <summary>
        /// Restaure les objets pour la DB
        /// </summary>
        public void RestoreDB()
        {
            if (SAVEDB != null)
            {
                //ObjectAnimations
                Game.ObjectAnimations = new List<VO_Animation>();
                foreach (VO_Animation anim in SAVEDB.ObjectAnimations)
                {
                    Game.ObjectAnimations.Add(anim.Clone());
                }

                //Characters
                Game.Characters = new List<VO_Character>();
                foreach (VO_Character chars in SAVEDB.Characters)
                {
                    Game.Characters.Add(chars.Clone());
                }

                //PlayableCharacters
                Game.PlayableCharacters = new List<VO_PlayableCharacter>();
                foreach (VO_PlayableCharacter chars in SAVEDB.PlayableCharacters)
                {
                    Game.PlayableCharacters.Add(chars.Clone());
                }

                //Items
                Game.Items = new List<VO_Item>();
                foreach (VO_Item item in SAVEDB.Items)
                {
                    Game.Items.Add(item.Clone());
                }

                //Actions
                Game.Actions = new List<VO_Action>();
                foreach (VO_Action action in SAVEDB.Actions)
                {
                    Game.Actions.Add(action.Clone());
                }

                //Classes
                Game.Classes = new List<VO_Class>();
                foreach (VO_Class classe in SAVEDB.Classes)
                {
                    Game.Classes.Add(classe.Clone());
                }

                //Triggers
                Game.Triggers = new List<VO_Trigger>();
                foreach (VO_Trigger trigger in SAVEDB.Triggers)
                {
                    Game.Triggers.Add(trigger.Clone());
                }

                //Variables
                Game.Variables = new List<VO_Variable>();
                foreach (VO_Variable variable in SAVEDB.Variables)
                {
                    Game.Variables.Add(variable.Clone());
                }

                //GlobalEvents
                Game.GlobalEvents = new List<VO_GlobalEvent>();
                foreach (VO_GlobalEvent globalEvent in SAVEDB.GlobalEvents)
                {
                    Game.GlobalEvents.Add(globalEvent.Clone());
                }

                //Scripts
                Game.InteractionScripts = new List<VO_Script>();
                foreach (VO_Script script in SAVEDB.InteractionScripts)
                {
                    Game.InteractionScripts.Add(script.Clone());
                }

                //Menus
                Game.Menu = new VO_Menu();
                Game.Menu = SAVEDB.Menu.Clone();

                //Projet
                Game.Project = new VO_Project();
                Game.Project = SAVEDB.Project.Clone();

                //Terminology
                Game.Terminology = new VO_Terminology();
                Game.Terminology = SAVEDB.Terminology.Clone();
            }
        }

        /// <summary>
        /// Sauvegarde les données d'un type d'anim à l'ouverture de l'Animation Manager
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        public void SaveAnim(Enums.AnimationType type, Guid characterId)
        {
            SAVEANIM = new GameCoreAnimationSave();

            VO_Character character = GetCharacterById(characterId);
            SAVEANIM.CharacterId = characterId;
            SAVEANIM.CharAnimations = new List<VO_Animation>();
            foreach (VO_Animation anims in character.Animations)
            {
                SAVEANIM.CharAnimations.Add(anims.Clone());
            }
        }

        /// <summary>
        /// Sauvegarde les données d'un type d'anim à l'ouverture de l'Animation Manager
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        public void SaveAnim(Enums.AnimationType type)
        {
            SAVEANIM = new GameCoreAnimationSave();

            switch (type)
            {
                case Enums.AnimationType.CharacterFace:
                    foreach (VO_Animation anims in Game.CharFacesAnimations)
                    {
                        SAVEANIM.CharFaces.Add(anims.Clone());
                    }
                    break;
                case Enums.AnimationType.IconAnimation:
                    foreach (VO_Animation anims in Game.IconsAnimations)
                    {
                        SAVEANIM.Icons.Add(anims.Clone());
                    }
                    break;
                case Enums.AnimationType.Menu:
                    foreach (VO_Animation anims in Game.MenusAnimations)
                    {
                        SAVEANIM.Menus.Add(anims.Clone());
                    }
                    break;
                case Enums.AnimationType.ObjectAnimation:
                    foreach (VO_Animation anims in Game.ObjectAnimations)
                    {
                        SAVEANIM.ObjectAnimations.Add(anims.Clone());
                    }
                    break;
            }
        }

        /// <summary>
        /// Restaure les données d'un type d'anim à la fermeture de l'Animation Manager
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        public void RestoreAnim(Enums.AnimationType pType)
        {
            switch (pType)
            {
                case Enums.AnimationType.CharacterAnimation:
                    VO_Character character = GetCharacterById(SAVEANIM.CharacterId);
                    character.Animations = new List<VO_Animation>();
                    foreach (VO_Animation anim in SAVEANIM.CharAnimations)
                    {
                        character.Animations.Add(anim.Clone());
                    }
                    break;
                case Enums.AnimationType.CharacterFace:
                    Game.CharFacesAnimations = new List<VO_Animation>();
                    foreach (VO_Animation anim in SAVEANIM.CharFaces)
                    {
                        Game.CharFacesAnimations.Add(anim.Clone());
                    }
                    break;
                case Enums.AnimationType.IconAnimation:
                    Game.IconsAnimations = new List<VO_Animation>();
                    foreach (VO_Animation anim in SAVEANIM.Icons)
                    {
                        Game.IconsAnimations.Add(anim.Clone());
                    }
                    break;
                case Enums.AnimationType.Menu:
                    Game.MenusAnimations = new List<VO_Animation>();
                    foreach (VO_Animation anim in SAVEANIM.Menus)
                    {
                        Game.MenusAnimations.Add(anim.Clone());
                    }
                    break;
                case Enums.AnimationType.ObjectAnimation:
                    Game.ObjectAnimations = new List<VO_Animation>();
                    foreach (VO_Animation anim in SAVEANIM.ObjectAnimations)
                    {
                        Game.ObjectAnimations.Add(anim.Clone());
                    }
                    break;
            }
        }

        /// <summary>
        /// Sauvegarde les boutons pour le TriggerManager
        /// </summary>
        public void SaveTriggers()
        {
            SAVETRIGGERS = new GameCoreTriggerSave();

            //Triggers
            foreach (VO_Trigger trigger in Game.Triggers)
            {
                SAVETRIGGERS.Triggers.Add(trigger.Clone());
            }
        }

        /// <summary>
        /// Restaure les boutons pour le TriggerManager
        /// </summary>
        public void RestoreTriggers()
        {
            if (SAVETRIGGERS != null)
            {
                //Triggers
                Game.Triggers = new List<VO_Trigger>();
                foreach (VO_Trigger trigger in SAVETRIGGERS.Triggers)
                {
                    Game.Triggers.Add(trigger.Clone());
                }
            }
        }

        /// <summary>
        /// Sauvegarde les boutons pour le TriggerManager
        /// </summary>
        public void SaveVariables()
        {
            SAVEVARIABLES = new GameCoreVariableSave();

            //Triggers
            foreach (VO_Variable variable in Game.Variables)
            {
                SAVEVARIABLES.Variables.Add(variable.Clone());
            }
        }

        /// <summary>
        /// Restaure les boutons pour le TriggerManager
        /// </summary>
        public void RestoreVariables()
        {
            if (SAVEVARIABLES != null)
            {
                //Variables
                Game.Variables = new List<VO_Variable>();
                foreach (VO_Variable variable in SAVEVARIABLES.Variables)
                {
                    Game.Variables.Add(variable.Clone());
                }
            }
        }

        /// <summary>
        /// Sauvegarde les boutons pour le EventManager
        /// </summary>
        public void SaveEvent(VO_Event v_event)
        {
            SAVEEVENT = new GameCoreEventSave();
            SAVEEVENT.Event = v_event.Clone();
        }

        /// <summary>
        /// Restaure les boutons pour le EventManager
        /// </summary>
        public VO_Event RestoreEvent()
        {
            return SAVEEVENT.Event.Clone();
        }
        #endregion

        #region Lists
        /// <summary>
        /// Récupérer l'ensemble des characters non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetCharacters()
        {
            List<VO_Base> characters = new List<VO_Base>();
            foreach (VO_Base character in Game.Characters)
            {
                characters.Add(character);
            }
            return characters;
        }

        /// <summary>
        /// Récupérer l'ensemble des characters jouables non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetPlayableCharacters()
        {
            List<VO_Base> characters = new List<VO_Base>();
            foreach (VO_Base character in Game.PlayableCharacters)
            {
                characters.Add(character);
            }
            return characters;
        }

        /// <summary>
        /// Récupérer l'ensemble des stages non clonés
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetStages()
        {
            List<VO_Base> items = new List<VO_Base>();
            foreach (VO_Base item in Game.Stages)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Récupérer l'ensemble des items non clonés
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetItems()
        {
            List<VO_Base> items = new List<VO_Base>();
            foreach (VO_Base item in Game.Items)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Récupérer l'ensemble des actions non clonés
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetActions()
        {
            List<VO_Base> items = new List<VO_Base>();
            foreach (VO_Base item in Game.Actions)
            {
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Récupérer l'ensemble des classes non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetClasses()
        {
            List<VO_Base> classes = new List<VO_Base>();
            foreach (VO_Base classe in Game.Classes)
            {
                classes.Add(classe);
            }
            return classes;
        }

        /// <summary>
        /// Récupérer l'ensemble des boutons non clonés
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetTriggers()
        {
            List<VO_Base> triggers = new List<VO_Base>();
            foreach (VO_Base trigger in Game.Triggers)
            {
                triggers.Add(trigger);
            }
            return triggers;
        }

        /// <summary>
        /// Récupérer l'ensemble des variables non clonés
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetVariables()
        {
            List<VO_Base> variables = new List<VO_Base>();
            foreach (VO_Base variable in Game.Variables)
            {
                variables.Add(variable);
            }
            return variables;
        }

        /// <summary>
        /// Récupérer l'ensemble des evenements globaux non clonés
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetGlobalEvents()
        {
            List<VO_Base> globalEvents = new List<VO_Base>();
            foreach (VO_Base globalEvent in Game.GlobalEvents)
            {
                globalEvents.Add(globalEvent);
            }
            return globalEvents;
        }

        /// <summary>
        /// Récupérer l'ensemble des animations non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetAnimations()
        {
            List<VO_Base> animations = new List<VO_Base>();
            foreach (VO_Base animation in Game.ObjectAnimations)
            {
                animations.Add(animation);
            }
            return animations;
        }

        /// <summary>
        /// Récupérer l'ensemble des charfaces non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetCharFaces()
        {
            List<VO_Base> animations = new List<VO_Base>();
            foreach (VO_Base animation in Game.CharFacesAnimations)
            {
                animations.Add(animation);
            }
            return animations;
        }

        /// <summary>
        /// Récupérer l'ensemble des charanimations non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetCharAnimations(Guid value)
        {
            List<VO_Base> animations = new List<VO_Base>();
            VO_Character character = GameCore.Instance.GetCharacterById(value);
            if (character.Animations != null)
            {
                foreach (VO_Base animation in character.Animations)
                {
                    animations.Add(animation);
                }
            }
            return animations;
        }

        /// <summary>
        /// Récupérer l'ensemble des icons non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetIcons()
        {
            List<VO_Base> animations = new List<VO_Base>();
            foreach (VO_Base animation in Game.IconsAnimations)
            {
                animations.Add(animation);
            }
            return animations;
        }

        /// <summary>
        /// Récupérer l'ensemble des menus non clonées
        /// </summary>
        /// <returns></returns>
        public List<VO_Base> GetMenus()
        {
            List<VO_Base> animations = new List<VO_Base>();
            foreach (VO_Base animation in Game.MenusAnimations)
            {
                animations.Add(animation);
            }
            return animations;
        }
        #endregion

        #region ById
        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId">ID du character</param>
        /// <returns>VO_Character</returns>
        public VO_Character GetCharacterById(Guid id)
        {
            VO_Character character = Game.Characters.Find(p => p.Id == id);
            if (character != null)
                return character;
            return (VO_Character)ValidationTools.CreateEmptyRessource(new VO_Character());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId">ID du character</param>
        /// <returns>VO_PlayableCharacter</returns>
        public VO_PlayableCharacter GetPlayableCharacterById(Guid id)
        {
            VO_PlayableCharacter character = Game.PlayableCharacters.Find(p => p.Id == id);
            if (character != null)
                return character;
            return (VO_PlayableCharacter)ValidationTools.CreateEmptyRessource(new VO_PlayableCharacter());
        }

        /// <summary>
        /// Récupère un item
        /// </summary>
        /// <param name="id">ID de l'item</param>
        /// <returns>VO_Item</returns>
        public VO_Item GetItemById(Guid id)
        {
            VO_Item item = Game.Items.Find(p => p.Id == id);
            if (item != null)
                return item;
            return (VO_Item)ValidationTools.CreateEmptyRessource(new VO_Item());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un item
        /// </summary>
        /// <param name="id">ID de l'action</param>
        /// <returns>VO_Action</returns>
        public VO_Action GetActionById(Guid id)
        {
            VO_Action action = Game.Actions.Find(p => p.Id == id);
            if (action != null)
                return action;
            return (VO_Action)ValidationTools.CreateEmptyRessource(new VO_Action());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un item
        /// </summary>
        /// <param name="id">ID de l'action</param>
        /// <returns>VO_Action</returns>
        public VO_Stage GetStageById(Guid id)
        {
            VO_Stage stage = Game.Stages.Find(p => p.Id == id);
            if (stage != null)
                return stage;
            return (VO_Stage)ValidationTools.CreateEmptyRessource(new VO_Stage());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'une classe
        /// </summary>
        /// <param name="id">ID de l'action</param>
        /// <returns>VO_Class</returns>
        public VO_Class GetClassById(Guid id)
        {
            VO_Class classes = Game.Classes.Find(p => p.Id == id);
            if (classes != null)
                return classes;
            return (VO_Class)ValidationTools.CreateEmptyRessource(new VO_Class());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un bouton
        /// </summary>
        /// <param name="id">ID du bouton</param>
        /// <returns>VO_Trigger</returns>
        public VO_Trigger GetTriggerById(Guid id)
        {
            VO_Trigger trigger = Game.Triggers.Find(p => p.Id == id);
            if (trigger != null)
                return trigger;
            return (VO_Trigger)ValidationTools.CreateEmptyRessource(new VO_Trigger());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'une variable
        /// </summary>
        /// <param name="id">ID du bouton</param>
        /// <returns>VO_Trigger</returns>
        public VO_Variable GetVariableById(Guid id)
        {
            VO_Variable variable = Game.Variables.Find(p => p.Id == id);
            if (variable != null)
                return variable;
            return (VO_Variable)ValidationTools.CreateEmptyRessource(new VO_Variable());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un evenement global
        /// </summary>
        /// <param name="id">ID de l'evenement global</param>
        /// <returns>VO_GlobalEvent</returns>
        public VO_GlobalEvent GetGlobalEventById(Guid id)
        {
            VO_GlobalEvent globalEvent = Game.GlobalEvents.Find(p => p.Id == id);
            if (globalEvent != null)
                return globalEvent;
            return (VO_GlobalEvent)ValidationTools.CreateEmptyRessource(new VO_GlobalEvent());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un script
        /// </summary>
        /// <param name="id">ID du script</param>
        /// <returns>VO_Script</returns>
        public VO_Script GetInteractionScriptsById(Guid id)
        {
            return Game.InteractionScripts.Find(p => p.Id == id);
        }

        /// <summary>
        /// Détruit un interactionScript
        /// </summary>
        /// <param name="id"></param>
        public void RemoveInteractionScriptById(Guid id)
        {
            VO_Script script = Game.InteractionScripts.Find(p => p.Id == id);
            if(script != null)
                Game.InteractionScripts.Remove(script);
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public VO_Animation GetAnimationById(Guid id)
        {
            VO_Animation animation = Game.ObjectAnimations.Find(p => p.Id == id);
            if (animation != null)
                return animation;
            return (VO_Animation)ValidationTools.CreateEmptyRessource(new VO_Animation());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public VO_Animation GetCharFaceById(Guid id)
        {
            VO_Animation charFace = Game.CharFacesAnimations.Find(p => p.Id == id);
            if (charFace != null)
                return charFace;
            return (VO_Animation)ValidationTools.CreateEmptyRessource(new VO_Animation());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public VO_Animation GetCharAnimationById(Guid idChar, Guid idAnim)
        {
            VO_Character character = GameCore.Instance.GetCharacterById(idChar);
            if (character.Id != Guid.Empty)
            {
                VO_Animation charAnimation = character.Animations.Find(p => p.Id == idAnim);
                if (charAnimation != null)
                    return charAnimation;
            }
            return (VO_Animation)ValidationTools.CreateEmptyRessource(new VO_Animation());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public VO_Animation GetIconById(Guid id)
        {
            VO_Animation iconAnimation = Game.IconsAnimations.Find(p => p.Id == id);
            if (iconAnimation != null)
                return iconAnimation;
            return (VO_Animation)ValidationTools.CreateEmptyRessource(new VO_Animation());
        }

        /// <summary>
        /// Récupère une nouvelle instance d'un objet
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public VO_Animation GetMenuById(Guid id)
        {
            VO_Animation menuAnimation = Game.MenusAnimations.Find(p => p.Id == id);
            if (menuAnimation != null)
                return menuAnimation;
            return (VO_Animation)ValidationTools.CreateEmptyRessource(new VO_Animation());
        }

        public VO_StageCharacter GetStageCharacter(Guid character)
        {
            foreach (VO_Stage item in Game.Stages)
            {
                VO_StageCharacter stageCharacter = item.ListCharacters.Find(i => i.Id == character);
                if (stageCharacter != null)
                    return stageCharacter;
            }
            VO_StageCharacter NewStageCharacter = new VO_StageCharacter();
            NewStageCharacter.Id = Guid.Empty;
            NewStageCharacter.Title = Culture.Language.NotFound.RESSOURCE_NOT_FOUND;
            return NewStageCharacter;
        }

        public VO_StageAnimation GetStageAnimation(Guid animation)
        {
            foreach (VO_Stage item in Game.Stages)
            {
                foreach (VO_Layer layer in item.ListLayers)
                {
                    VO_StageAnimation stageAnimation = layer.ListAnimations.Find(i => i.Id == animation);
                    if (stageAnimation != null)
                        return stageAnimation;
                }
            }
            return (VO_StageAnimation)ValidationTools.CreateEmptyRessource(new VO_StageAnimation());
        }
        #endregion
        #endregion
    }
}
