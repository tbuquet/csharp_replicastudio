using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using System.Windows.Forms;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.TransverseLayer.Managers
{
    /// <summary>
    /// Classe d'instanciations d'objets métiers
    /// </summary>
    public static class ObjectsFactory
    {
        #region Inserts du GameCore
        /// <summary>
        /// Crée un stage
        /// </summary>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        /// <returns></returns>
        public static VO_Stage CreateStage(int width, int height)
        {
            //Création de l'objet
            VO_Stage stage = new VO_Stage(Guid.NewGuid())
            {
                Title = GlobalConstants.STAGE_NEW_STAGE,
                Dimensions = new Size(width, height),
                ListLayers = new List<VO_Layer>(),
                ListHotSpots = new List<VO_StageHotSpot>(),
                ListRegions = new List<VO_StageRegion>(),
                Music = new VO_Music(),
                Color = new VO_ColorTransformation(),
                EndingFirstScript = CreateScript( Enums.ScriptType.StageEvents),
                StartingFirstScript = CreateScript(Enums.ScriptType.StageEvents),
                EndingScript = CreateScript(Enums.ScriptType.StageEvents),
                StartingScript = CreateScript(Enums.ScriptType.StageEvents),
                ListCharacters = new List<VO_StageCharacter>(),
                Region = 100
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Stages.Add(stage);

            //Ajout d'un calque clé
            CreateLayer(stage, GlobalConstants.STAGE_NEW_LAYER, 0, true);

            return stage;
        }

        /// <summary>
        /// Insère un character
        /// </summary>
        /// <returns>VO_Character</returns>
        public static VO_Character CreateCharacter()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.Characters.Find(p => p.Title == string.Format(GlobalConstants.CHARACTERS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.CHARACTERS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Character character = new VO_Character(Guid.NewGuid())
            {
                Title = title,
                Speed = GlobalConstants.CHARACTERS_NORMAL_SPEED,
                DialogColor = FormsTools.GetVOColorFromGDIColor(Color.Black),
                Animations = new List<VO_Animation>()
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Characters.Add(character);

            //Animations
            VO_Animation standing = CreateCharAnimation(character.Id);
            standing.Title = GlobalConstants.CHARACTERS_STANDING;
            VO_Animation walking = CreateCharAnimation(character.Id);
            walking.Title = GlobalConstants.CHARACTERS_WALKING;
            VO_Animation talking = CreateCharAnimation(character.Id);
            talking.Title = GlobalConstants.CHARACTERS_TALKING;
            character.TalkingAnim = talking.Id;
            character.StandingAnim = standing.Id;
            character.WalkingAnim = walking.Id;

            return character;
        }

        /// <summary>
        /// Insère un character
        /// </summary>
        /// <returns>VO_PlayableCharacter</returns>
        public static VO_PlayableCharacter CreatePlayableCharacter()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.PlayableCharacters.Find(p => p.Title == string.Format(GlobalConstants.PLAYABLECHARACTERS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.PLAYABLECHARACTERS_NEW_ITEM, i);
                    break;
                }

            }

            VO_PlayableCharacter character = new VO_PlayableCharacter(Guid.NewGuid())
            {
                Title = title,
                PvMax = 100,
                PvAtStart = 100,
                StartPosition = Enums.Movement.Down,
                CoordsCharacter = new VO_Coords(new Point(), Guid.Empty)
            };

            //Insertion de l'objet
            GameCore.Instance.Game.PlayableCharacters.Add(character);

            //Attachement de l'action "Aller"
            character.Actions = new List<Guid>();
            foreach (VO_Action action in GameCore.Instance.Game.Actions)
            {
                if (action.GoAction)
                {
                    character.Actions.Add(action.Id);
                }
                else if (action.UseAction)
                {
                    character.Actions.Add(action.Id);
                }
            }

            character.Items = new List<Guid>();

            return character;
        }

        /// <summary>
        /// Insère un item
        /// </summary>
        /// <returns></returns>
        public static VO_Item CreateItem()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.Items.Find(p => p.Title == string.Format(GlobalConstants.ITEM_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ITEM_NEW_ITEM, i);
                    break;
                }

            }

            VO_Item newItem = new VO_Item(Guid.NewGuid())
            {
                Title = title,
                ItemInteraction = new List<VO_ItemInteraction>(),
                Scripts = new List<VO_ActionOnItemScript>()
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Items.Add(newItem);
            return newItem;
        }

        /// <summary>
        /// Insère une action
        /// </summary>
        /// <returns></returns>
        public static VO_Action CreateAction()
        {
            return CreateAction(Guid.NewGuid());
        }

        /// <summary>
        /// Insère une action
        /// </summary>
        /// <returns></returns>
        public static VO_Action CreateAction(Guid value)
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.Actions.Find(p => p.Title == string.Format(GlobalConstants.ACTION_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ACTION_NEW_ITEM, i);
                    break;
                }

            }

            VO_Action newItem = new VO_Action(value)
            {
                Title = title
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Actions.Add(newItem);
            return newItem;
        }

        /// <summary>
        /// Insère une action
        /// </summary>
        /// <returns></returns>
        public static VO_Class CreateClass()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.Classes.Find(p => p.Title == string.Format(GlobalConstants.CLASS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.CLASS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Class newItem = new VO_Class(Guid.NewGuid())
            {
                Title = title,
                BadInteractions = new List<VO_BadInteraction>()
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Classes.Add(newItem);
            return newItem;
        }

        /// <summary>
        /// Insère un bouton
        /// </summary>
        /// <returns></returns>
        public static VO_Trigger CreateTrigger()
        {
            //Création de l'objet
            VO_Trigger newTrigger = new VO_Trigger(Guid.NewGuid())
            {
                Title = GlobalConstants.TRIGGER_NEW_ITEM,
                Value = false
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Triggers.Add(newTrigger);
            return newTrigger;
        }

        /// <summary>
        /// Insère une variable
        /// </summary>
        /// <returns></returns>
        public static VO_Variable CreateVariable()
        {
            //Création de l'objet
            VO_Variable newVariable = new VO_Variable(Guid.NewGuid())
            {
                Title = GlobalConstants.VARIABLE_NEW_ITEM,
                Value = 0
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Variables.Add(newVariable);
            return newVariable;
        }

        /// <summary>
        /// Insère un evenement global
        /// </summary>
        /// <returns></returns>
        public static VO_GlobalEvent CreateGlobalEvent()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.GlobalEvents.Find(p => p.Title == string.Format(GlobalConstants.GLOBALEVENT_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.GLOBALEVENT_NEW_ITEM, i);
                    break;
                }

            }

            VO_GlobalEvent newGlobalEvent = new VO_GlobalEvent(Guid.NewGuid())
            {
                Title = title,
                Script = CreateScript(Enums.ScriptType.GlobalEvents)
            };

            //Insertion de l'objet
            GameCore.Instance.Game.GlobalEvents.Add(newGlobalEvent);
            return newGlobalEvent;
        }

        /// <summary>
        /// Insère un menu
        /// </summary>
        /// <returns></returns>
        public static VO_Menu CreateMenu()
        {
            //Création de l'objet
            VO_Menu newMenu = new VO_Menu()
            {
                ActivateEchapMenu = true,
                ActivateLoadingMenu = true,
                ActivateMainMenu = true,
                ActivateSaveMenu = true,
                InventoryBackButtonCoords = new Point(0, 0),
                InventoryCoords = new Point(0, 0),
                ItemWidth = 32,
                ItemHeight = 32,
                GridHeight = 5,
                GridWidth = 5
            };

            //Insertion de l'objet
            GameCore.Instance.Game.Menu = newMenu;
            return newMenu;
        }

        /// <summary>
        /// Insère un terminology
        /// </summary>
        /// <returns></returns>
        public static VO_Terminology CreateTerminology()
        {
            VO_Terminology terminology = new VO_Terminology()
            {
                NewGame = EditorConstants.Instance.TERM_NEWGAME,
                LoadGame = EditorConstants.Instance.TERM_LOADGAME,
                LeaveGame = EditorConstants.Instance.TERM_LEAVEGAME,
                Options = EditorConstants.Instance.TERM_OPTIONS,
                ReturnTitle = EditorConstants.Instance.TERM_RETURNTITLE,
                SaveGame = EditorConstants.Instance.TERM_SAVEGAME,
                SaveState = EditorConstants.Instance.TERM_SAVESTATE,
                ChoiceNext = EditorConstants.Instance.TERM_CHOICENEXT,
                ChoicePrevious = EditorConstants.Instance.TERM_CHOICEPREVIOUS
            };

            GameCore.Instance.Game.Terminology = terminology;
            return terminology;
        }

        /// <summary>
        /// Insère un script
        /// </summary>
        /// <returns></returns>
        public static VO_Script CreateScript(Enums.ScriptType type)
        {
            return CreateScript(false, type);
        }

        /// <summary>
        /// Insère un script
        /// </summary>
        /// <returns></returns>
        public static VO_Script CreateScript(bool interactionScript, Enums.ScriptType type)
        {
            //Création de l'objet
            VO_Script newScript = new VO_Script(Guid.NewGuid())
            {
                Lines = new List<VO_Line>()
            };

            //Insertion de l'objet
            if(interactionScript)
                GameCore.Instance.Game.InteractionScripts.Add(newScript);

            newScript.Lines = new List<VO_Line>();
            newScript.ScriptType = type;
            return newScript;
        }
        #endregion

        #region Inserts du GameCoreAnimations
        /// <summary>
        /// Insère une animation
        /// </summary>
        /// <returns>VO_Animation</returns>
        public static VO_Animation CreateAnimation()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.ObjectAnimations.Find(p => p.Title == string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Animation animation = new VO_Animation(Guid.NewGuid(), Enums.AnimationType.ObjectAnimation);
            animation.Frequency = GlobalConstants.ANIMATION_NORMAL_FREQUENCY;
            animation.Title = title;

            //Insertion de l'objet
            GameCore.Instance.Game.ObjectAnimations.Add(animation);
            return animation;
        }

        /// <summary>
        /// Insère une animation
        /// </summary>
        /// <returns>VO_Animation</returns>
        public static VO_Animation CreateCharFace()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.CharFacesAnimations.Find(p => p.Title == string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Animation animation = new VO_Animation(Guid.NewGuid(), Enums.AnimationType.CharacterFace);
            animation.Frequency = GlobalConstants.ANIMATION_NORMAL_FREQUENCY;
            animation.Title = title;

            //Insertion de l'objet
            GameCore.Instance.Game.CharFacesAnimations.Add(animation);
            return animation;
        }

        /// <summary>
        /// Insère une animation
        /// </summary>
        /// <returns>VO_Animation</returns>
        public static VO_Animation CreateCharAnimation(Guid guidChar)
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.GetCharacterById(guidChar).Animations.Find(p => p.Title == string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Animation animation = new VO_Animation(Guid.NewGuid(), Enums.AnimationType.CharacterAnimation)
            {
                ParentCharacter = guidChar,
                Frequency = 50,
                SpriteHeight = 1,
                SpriteWidth = 1,
                Title = title
            };

            //Insertion de l'objet
            GameCore.Instance.GetCharacterById(guidChar).Animations.Add(animation);
            return animation;
        }

        /// <summary>
        /// Insère une animation
        /// </summary>
        /// <returns>VO_Animation</returns>
        public static VO_Animation CreateIconAnimation()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.IconsAnimations.Find(p => p.Title == string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Animation animation = new VO_Animation(Guid.NewGuid(), Enums.AnimationType.IconAnimation);
            animation.Title = title;

            //Insertion de l'objet
            GameCore.Instance.Game.IconsAnimations.Add(animation);
            return animation;
        }

        /// <summary>
        /// Insère une animation
        /// </summary>
        /// <returns>VO_Animation</returns>
        public static VO_Animation CreateMenuAnimation()
        {
            //Création de l'objet
            int i = 1;
            string title = string.Empty;
            while (true)
            {
                if (GameCore.Instance.Game.MenusAnimations.Find(p => p.Title == string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i)) != null)
                {
                    i++;
                    continue;
                }
                else
                {
                    title = string.Format(GlobalConstants.ANIMATIONS_NEW_ITEM, i);
                    break;
                }

            }

            VO_Animation animation = new VO_Animation(Guid.NewGuid(), Enums.AnimationType.Menu);
            animation.Title = title;

            //Insertion de l'objet
            GameCore.Instance.Game.MenusAnimations.Add(animation);
            return animation;
        }
        #endregion

        #region Inserts d'objets tools
        /// <summary>
        /// Créateur de Bad Interactions
        /// </summary>
        /// <returns>Bad Interaction</returns>
        public static VO_BadInteraction CreateBadInteraction()
        {
            //Création de l'objet
            VO_BadInteraction badInteraction = new VO_BadInteraction()
            {
                Id = Guid.NewGuid(),
                Dialog = CreateDialog(),
                Action = new Guid(),
                Character = new Guid()
            };
            badInteraction.Dialog.ParentObjectId = badInteraction.Id;

            return badInteraction;
        }

        /// <summary>
        /// Créateur de dialogues
        /// </summary>
        /// <returns></returns>
        public static VO_Dialog CreateDialog()
        {
            //Création de l'objet
            VO_Dialog dialog = new VO_Dialog()
            {
                Messages = new List<VO_Message>()
            };

            return dialog;
        }

        /// <summary>
        /// Créateur de messages
        /// </summary>
        /// <returns></returns>
        public static VO_Message CreateMessage()
        {
            //Création de l'objet
            VO_Message message = new VO_Message()
            {
                Id = Guid.NewGuid(),
                Duration = EditorSettings.Instance.MessageDuration,
                FontSize = EditorSettings.Instance.MessageFontSize,
            };

            return message;
        }
        #endregion

        #region Inserts d'objets calque
        public static void CreateDecor(VO_Layer layer, Point position, string file)
        {
            if (layer.ListDecors.Count < GlobalConstants.PERF_MAX_DECORS_PER_LAYERS)
            {
                //Récupérer la taille de l'image
                Size picSize = ImageManager.GetImageStageDecor(GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + file).Size;

                VO_StageDecor newDecor = new VO_StageDecor();
                newDecor.Id = Guid.NewGuid();
                newDecor.Title = Path.GetFileNameWithoutExtension(file);
                newDecor.Location = position;
                newDecor.Size = picSize;
                newDecor.Filename = file;
                newDecor.ObjectType = Enums.StageObjectType.Decors;
                newDecor.Stage = layer.Stage;
                newDecor.Layer = layer.Id;
                layer.ListDecors.Add(newDecor);
            }
            else
                MessageBox.Show(string.Format(Errors.STAGE_MAX_DECORS, GlobalConstants.PERF_MAX_DECORS_PER_LAYERS), Errors.ERROR_BOX_TITLE);
        }

        /// <summary>
        /// Insère un event
        /// </summary>
        /// <returns></returns>
        public static VO_Event CreateEvent(Enums.EventType currentEvent, Guid objectId)
        {
            VO_Event newItem = new VO_Event();
            newItem.PageList = new List<VO_Page>();
            VO_Page FirstPage = CreatePage(currentEvent, 0, objectId);
            FirstPage.PageEventType = currentEvent;
            FirstPage.PageNumber = newItem.PageList.Count + 1;

            VO_Script Script = null;
            switch (currentEvent)
            {
                case Enums.EventType.Animation:
                    Script = ObjectsFactory.CreateScript(Enums.ScriptType.AnimationEvents);
                    break;
                case Enums.EventType.Character:
                    Script = ObjectsFactory.CreateScript(Enums.ScriptType.CharacterEvents);
                    break;
                case Enums.EventType.Event:
                    Script = ObjectsFactory.CreateScript(Enums.ScriptType.Events);
                    break;
            }
            

            FirstPage.Script = Script;

            newItem.PageList.Add(FirstPage);
            return newItem;
        }

        public static VO_Page CreatePage(Enums.EventType CurrentEvent, int PageNumber, Guid objectId)
        {
            VO_Page NewPage = new VO_Page(new Guid());
            NewPage.PageNumber = PageNumber;
            NewPage.PageEventType = CurrentEvent;

            VO_Script Script = null;
            switch (CurrentEvent)
            {
                case Enums.EventType.Animation:
                    Script = ObjectsFactory.CreateScript(Enums.ScriptType.AnimationEvents);
                    NewPage.AnimationFrequency = GlobalConstants.ANIMATION_NORMAL_FREQUENCY;
                    break;
                case Enums.EventType.Character:
                    Script = ObjectsFactory.CreateScript(Enums.ScriptType.CharacterEvents);
                    VO_Character character = GameCore.Instance.GetCharacterById(objectId);
                    NewPage.CharacterAnimationFrequency = GlobalConstants.ANIMATION_NORMAL_FREQUENCY;
                    NewPage.CharacterDirection = Enums.Movement.Down;
                    NewPage.CharacterSpeed = GlobalConstants.CHARACTERS_NORMAL_SPEED;
                    NewPage.CharacterStandingType = character.StandingAnim;
                    NewPage.CharacterWalkingType = character.WalkingAnim;
                    NewPage.CharacterTalkingType = character.TalkingAnim;
                    break;
                case Enums.EventType.Event:
                    Script = ObjectsFactory.CreateScript(Enums.ScriptType.Events);
                    break;
            }

            NewPage.Script = Script;
            return NewPage;
        }

        public static void CreateAnimation(VO_Layer layer, Point position, Guid animId)
        {
            if (layer.ListAnimations.Count < GlobalConstants.PERF_MAX_ANIMATION_PER_LAYERS)
            {
                VO_Animation animation = GameCore.Instance.GetAnimationById(animId);
                VO_StageAnimation newAnim = new VO_StageAnimation();
                newAnim.Id = Guid.NewGuid();
                newAnim.Title = animation.Title;
                newAnim.Location = position;
                newAnim.Size = new Size(animation.SpriteWidth, animation.SpriteHeight);
                newAnim.AnimationId = animId;
                newAnim.ObjectType = Enums.StageObjectType.Animations;
                newAnim.Stage = layer.Stage;
                newAnim.Layer = layer.Id;
                newAnim.Event = CreateEvent(Enums.EventType.Animation, animation.Id);
                layer.ListAnimations.Add(newAnim);
            }
            else
                MessageBox.Show(string.Format(Errors.STAGE_MAX_ANIMATIONS, GlobalConstants.PERF_MAX_ANIMATION_PER_LAYERS), Errors.ERROR_BOX_TITLE);
        }

        public static void CreateCharacter(VO_Stage stage, Point position, Guid charId)
        {
            if (stage.ListCharacters.Count < GlobalConstants.PERF_MAX_CHARACTERS_PER_LAYERS)
            {
                VO_Character character = GameCore.Instance.GetCharacterById(charId);
                VO_StageCharacter newChar = new VO_StageCharacter();

                //Animation standing
                VO_Animation standingAnim = character.GetAnimationById(character.StandingAnim);
                newChar.AnimationId = standingAnim.Id;
                newChar.Id = Guid.NewGuid();
                newChar.Title = character.Title;
                newChar.Location = position;
                newChar.Size = new Size(standingAnim.SpriteWidth, standingAnim.SpriteHeight);
                newChar.CharacterId = charId;
                newChar.ObjectType = Enums.StageObjectType.Characters;
                newChar.Stage = stage.Id;
                newChar.PlayerPositionPoint = new VO_Coords();
                newChar.Event = CreateEvent(Enums.EventType.Character, character.Id);
                stage.ListCharacters.Add(newChar);
            }
            else
                MessageBox.Show(string.Format(Errors.STAGE_MAX_CHARACTERS, GlobalConstants.PERF_MAX_CHARACTERS_PER_LAYERS), Errors.ERROR_BOX_TITLE);
        }
        #endregion

        #region Inserts d'objets Stage
        public static VO_Layer CreateLayer(VO_Stage stage, string pTitle, int pLastOrdinal, bool pMain)
        {
            VO_Layer vLayer = new VO_Layer();
            vLayer.Id = Guid.NewGuid();
            vLayer.MainLayer = pMain;
            vLayer.Ordinal = pLastOrdinal;
            vLayer.ColorTransformations = new VO_ColorTransformation();
            vLayer.ListAnimations = new List<VO_StageAnimation>();
            vLayer.ListWalkableAreas = new List<VO_StageWalkable>();
            vLayer.Title = pTitle;
            vLayer.ListDecors = new List<VO_StageDecor>();
            vLayer.Stage = stage.Id;
            stage.ListLayers.Add(vLayer);
            return vLayer;
        }

        public static VO_StageHotSpot CreateHotSpot(VO_Stage stage, Point position)
        {
            if (stage.ListHotSpots.Count < GlobalConstants.PERF_MAX_HOTSPOT_PER_STAGE)
            {
                VO_StageHotSpot newHotSpot = new VO_StageHotSpot();
                newHotSpot.Id = Guid.NewGuid();
                newHotSpot.Title = GlobalConstants.HOTSPOT_NEW_ITEM;
                newHotSpot.ObjectType = Enums.StageObjectType.HotSpots;
                newHotSpot.Stage = stage.Id;
                newHotSpot.Points = new Point[1];
                newHotSpot.Points[0] = position;
                newHotSpot.Event = CreateEvent(Enums.EventType.Event, newHotSpot.Id);
                newHotSpot.PlayerPositionPoint = new VO_Coords();
                stage.ListHotSpots.Add(newHotSpot);
                return newHotSpot;
            }
            MessageBox.Show(string.Format(Errors.STAGE_MAX_HOTSPOT, GlobalConstants.PERF_MAX_HOTSPOT_PER_STAGE), Errors.ERROR_BOX_TITLE);
            return null;
        }

        public static VO_StageHotSpot CreateWalkableArea(VO_Layer layer, Point position)
        {
            if (layer.ListWalkableAreas.Count < GlobalConstants.PERF_MAX_WALK_PER_STAGE)
            {
                VO_StageWalkable newHotSpot = new VO_StageWalkable();
                newHotSpot.Id = Guid.NewGuid();
                newHotSpot.Title = GlobalConstants.WALK_NEW_ITEM;
                newHotSpot.ObjectType = Enums.StageObjectType.Walkables;
                newHotSpot.Stage = layer.Stage;
                newHotSpot.Points = new Point[1];
                newHotSpot.Points[0] = position;
                newHotSpot.Layer = layer.Id;
                newHotSpot.Event = null;
                layer.ListWalkableAreas.Add(newHotSpot);
                return newHotSpot;
            }
            MessageBox.Show(string.Format(Errors.STAGE_MAX_WALK, GlobalConstants.PERF_MAX_WALK_PER_STAGE), Errors.ERROR_BOX_TITLE);
            return null;
        }

        public static VO_StageRegion CreateRegion(VO_Stage stage, Point position)
        {
            if (stage.ListHotSpots.Count < GlobalConstants.PERF_MAX_REGIONS_PER_STAGE)
            {
                VO_StageRegion newHotSpot = new VO_StageRegion();
                newHotSpot.Id = Guid.NewGuid();
                newHotSpot.Title = GlobalConstants.REGION_NEW_ITEM;
                newHotSpot.ObjectType = Enums.StageObjectType.Regions;
                newHotSpot.Stage = stage.Id;
                newHotSpot.Ratio = GlobalConstants.CHARACTERS_NORMAL_RATIO;
                newHotSpot.Points = new Point[1];
                newHotSpot.Points[0] = position;
                newHotSpot.Event = null;
                newHotSpot.PlayerPositionPoint = new VO_Coords();
                stage.ListRegions.Add(newHotSpot);
                return newHotSpot;
            }
            MessageBox.Show(string.Format(Errors.STAGE_MAX_REGION, GlobalConstants.PERF_MAX_REGIONS_PER_STAGE), Errors.ERROR_BOX_TITLE);
            return null;
        }
        #endregion
    }
}
