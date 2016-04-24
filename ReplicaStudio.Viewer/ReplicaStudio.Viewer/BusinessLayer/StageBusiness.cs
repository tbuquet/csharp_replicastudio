using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Algorithms;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Viewer.DataLayer;

namespace ReplicaStudio.Viewer.BusinessLayer
{
    /// <summary>
    /// Business Stage
    /// </summary>
    public class StageBusiness : BaseBusiness
    {
        #region Members
        /// <summary>
        /// Animations
        /// </summary>
        Dictionary<Guid, VO_AnimatedSprite> _Animations;

        /// <summary>
        /// Characters
        /// </summary>
        Dictionary<Guid, VO_CharacterSprite> _Characters;

        /// <summary>
        /// Référence au stage courant
        /// </summary>
        VO_Stage _CurrentStage;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public StageBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge une scène
        /// </summary>
        /// <param name="id">Id de la scène</param>
        /// <returns>VO_Stage</returns>
        public VO_Stage GetStageData(Guid id)
        {
            return GameCore.Instance.GetStageById(id);
        }

        /// <summary>
        /// Charge les informations lourdes du stage
        /// </summary>
        public void PreLoadStage(VO_Stage stage, int matrixPrecision)
        {
            _CurrentStage = stage;
            _Animations = new Dictionary<Guid, VO_AnimatedSprite>();
            _Characters = new Dictionary<Guid, VO_CharacterSprite>();
            foreach (VO_Layer layer in stage.ListLayers)
            {
                //Decors
                foreach (VO_StageDecor item in layer.ListDecors)
                {
                    SpriteManager.CreateScreenSprite(item.Id, PathTools.GetProjectPath(Enums.ProjectPath.Resources) + item.Filename, new Vector2(item.Location.X, item.Location.Y), new Rectangle(0, 0, item.Size.Width, item.Size.Height));
                    //SpriteManager.ChangeScreenSpriteColor(item.Id, layer.ColorTransformations);
                }

                //Animations
                foreach (VO_StageAnimation item in layer.ListAnimations)
                {
                    _Animations.Add(item.Id, new VO_AnimatedSprite(item.AnimationId, Enums.AnimationType.ObjectAnimation, item.Location.X, item.Location.Y));
                    _Animations[item.Id].SetColor(layer.ColorTransformations);
                }
            }

            //Characters
            foreach (VO_StageCharacter item in stage.ListCharacters)
            {
                _Characters.Add(item.Id, new VO_CharacterSprite(item));
            }

            //Matrice de déplacement
            MatrixManager.CurrentStage.LoadWalkableMatrix(stage.Dimensions, stage, matrixPrecision);

            //Matrice de région
            MatrixManager.CurrentStage.LoadRegionMatrix(stage.Dimensions, stage.ListRegions, matrixPrecision);

            //Matrice d'events
            MatrixManager.CurrentStage.LoadEventsMatrix(stage.Dimensions, stage.ListHotSpots, matrixPrecision);
        }

        /// <summary>
        /// Retourne une animation
        /// </summary>
        /// <param name="id">Id de l'animation</param>
        /// <param name="type">Type de l'animation</param>
        /// <returns></returns>
        public VO_AnimatedSprite GetAnimatedSprite(Guid id, Enums.StageObjectType type)
        {
            if (type == Enums.StageObjectType.Animations)
                return _Animations[id];
            else if (type == Enums.StageObjectType.Characters)
                return _Characters[id].Sprites;
            return null;
        }

        /// <summary>
        /// Détruit les ressources
        /// </summary>
        public void DisposeAnimations()
        {
            if (_Animations != null)
            {
                foreach (VO_AnimatedSprite item in _Animations.Values)
                {
                    item.Dispose();
                }
            }
            _Animations = new Dictionary<Guid, VO_AnimatedSprite>();

            if (_Characters != null)
            {
                foreach (VO_CharacterSprite item in _Characters.Values)
                {
                    item.Dispose();
                }
            }
            _Characters = new Dictionary<Guid, VO_CharacterSprite>();
        }

        #region Ratio
        /// <summary>
        /// Récupère le ratio actuel.
        /// </summary>
        /// <param name="point">Point à tester</param>
        /// <returns>Ratio</returns>
        public float GetRatioFromMatrix(Point point, int matrixPrecision)
        {
            int x = point.X;
            int y = point.Y;
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            return (float)MatrixManager.CurrentStage.RegionsMatrix[x / matrixPrecision, y / matrixPrecision] / 100.0f;
        }
        #endregion

        #region Events
        /// <summary>
        /// Récupère et traite l'event
        /// </summary>
        /// <param name="point"></param>
        /// <param name="matrixPrecision"></param>
        public bool GetEventFromMatrix(Point point, int matrixPrecision)
        {
            //Gestion des characters
            foreach (VO_StageCharacter character in _CurrentStage.ListCharacters)
            {
                VO_CharacterSprite characterSprite = GetCharacterSprite(character.Id);
                if (characterSprite.PointIsInCharacter(point))
                {
                    //Activer l'event
                    foreach (VO_Page page in character.Event.PageList)
                    {
                        if (IsActivePage(page))
                            return true;
                    }
                    return false;
                }
            }

            //Gestion des events
            int eventIndex = MatrixManager.CurrentStage.EventsMatrix[point.X / matrixPrecision, point.Y / matrixPrecision] - 1;

            //Récupérer l'event associé
            if (eventIndex > -1)
            {
                VO_Event eventSpot = _CurrentStage.ListHotSpots[eventIndex].Event;

                //Activer l'event
                foreach (VO_Page page in eventSpot.PageList)
                {
                    if (IsActivePage(page) && page.TriggerCondition == Enums.TriggerEventConditionType.ClickEvent)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Teste si un event est actif
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public bool IsActivePage(VO_Page page)
        {
            //Si le personnage ne correspond pas, on passe à la page suivante
            if (page.CharacterActivated && PlayableCharactersManager.CurrentPlayerCharacter.Id != page.CharacterId)
                return false;
            //Si la variable ne correspond pas aux conditions, on passe à la page suivante
            if (page.VariableActivated)
            {
                VO_Variable var = GameState.State.Variables.Find(p => p.Id == page.VariableId);
                if (var == null || var.Id == Guid.Empty || var.Value < page.VariableValue)
                    return false;
            }
            //Si le bouton ne correspond pas aux conditions, on passe à la page suivante
            if (page.TriggerActivated)
            {
                VO_Trigger trigger = GameState.State.Triggers.Find(p => p.Id == page.TriggerId);
                if (trigger == null || trigger.Id == Guid.Empty || !trigger.Value)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Teste si un event est executable
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public bool IsExecutablePage(VO_Page page)
        {
            //Si l'action ne correspond pas, on passe à la page suivante
            if (page.ActionActivated)
            {
                VO_Action action = GameCore.Instance.GetActionById(page.ActionId);
                if (action.Id == Guid.Empty || action.Id != ActionManager.CurrentAction.Id)
                    return false;
            }
            //Si l'item ne correspond pas, on passe à la page suivante
            if (page.ItemActivated)
            {
                VO_Item item = GameCore.Instance.GetItemById(page.ItemId);
                if (item.Id == Guid.Empty || item.Id != ActionManager.ItemInUse || !ActionManager.CurrentActionIsUse())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check les evenements s'executant automatiquement.
        /// </summary>
        public void CheckParallelProcessAndAutomaticAndContactScripts(int matrixPrecision)
        {
            foreach (VO_StageObject _event in _CurrentStage.ListHotSpots)
            {
                TestScript(_event, Enums.TriggerEventConditionType.ParallelProcess);
                TestScript(_event, Enums.TriggerEventConditionType.Automatic);
            }
            CheckContactScript(matrixPrecision);
        }

        /// <summary>
        /// Script par contact
        /// </summary>
        /// <param name="matrixPrecision"></param>
        private void CheckContactScript(int matrixPrecision)
        {
            VO_StageObject hotSpot = null;
            Point point = PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location;

            #region Récupère le script
            int eventIndex = MatrixManager.CurrentStage.EventsMatrix[point.X / matrixPrecision, point.Y / matrixPrecision] - 1;

            //Récupérer l'event associé
            if (eventIndex > -1)
            {
                hotSpot = _CurrentStage.ListHotSpots[eventIndex];
                TestScript(hotSpot, Enums.TriggerEventConditionType.ContactEvent);
            }
            #endregion
        }

        // EVENT PREVU POUR CHARACTERS & EVENTS, les Anim ne sont pas supposées être cliquables
        public bool ExecuteClickedEvent(Point point, int matrixPrecision)
        {
            VO_StageObject hotSpot = null;

            #region Récupère le script
            //Gestion des characters
            foreach (VO_StageCharacter character in _CurrentStage.ListCharacters)
            {
                VO_CharacterSprite characterSprite = GetCharacterSprite(character.Id);
                if (characterSprite.PointIsInCharacter(point))
                {
                    //Activer l'event
                    foreach (VO_Page page in character.Event.PageList)
                    {
                        if (IsActivePage(page))
                            return TestScript(character, Enums.TriggerEventConditionType.ClickEvent);
                    }
                }
            }

            //Gestion des events
            int eventIndex = MatrixManager.CurrentStage.EventsMatrix[point.X / matrixPrecision, point.Y / matrixPrecision] - 1;

            //Récupérer l'event associé
            if (eventIndex > -1)
            {
                hotSpot = _CurrentStage.ListHotSpots[eventIndex];
                return TestScript(hotSpot, Enums.TriggerEventConditionType.ClickEvent);
            }
            #endregion

            return false;
        }

        /// <summary>
        /// Execute les scripts d'animation
        /// </summary>
        /// <param name="animStage"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ExecuteAnimationScript(VO_StageAnimation animStage, Enums.TriggerExecutionType type)
        {
            foreach (VO_Page page in animStage.Event.PageList)
            {
                if (page.TriggerExecution == type)
                {
                    page.TriggerCondition = Enums.TriggerEventConditionType.ParallelProcess;
                    TestScript(animStage, Enums.TriggerEventConditionType.ParallelProcess);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Test et gère les scripts
        /// </summary>
        /// <param name="stageObject"></param>
        /// <param name="IsProcess"></param>
        /// <returns></returns>
        public bool TestScript(VO_StageObject stageObject, Enums.TriggerEventConditionType type)
        {
            VO_Event eventSpot = stageObject.Event;
            VO_Page _page = null;

            //Activer l'event
            foreach (VO_Page page in eventSpot.PageList)
            {
                if (page.TriggerCondition == type && IsActivePage(page) && IsExecutablePage(page))
                {
                    _page = page;
                    break;
                }
            }

            //Bad Interactions
            if (type == Enums.TriggerEventConditionType.ClickEvent)
            {
                if (_page == null && eventSpot != null && stageObject.ClassId != Guid.Empty)
                {
                    VO_Dialog dialog = FindBadInteraction(eventSpot, stageObject.ClassId);
                    if (dialog != null)
                    {
                        VO_Script dynamicScript = new VO_Script(Guid.NewGuid());
                        dynamicScript.Lines = new List<VO_Line>();
                        VO_Script_Message scriptMessage = new VO_Script_Message();
                        scriptMessage.Id = Guid.NewGuid();
                        scriptMessage.Dialog = dialog;
                        dynamicScript.Lines.Add(scriptMessage);
                        LaunchScript(stageObject, dynamicScript, type);
                        return true;
                    }
                }
            }

            if (_page == null)
                return false;

            #region Execute le script
            LaunchScript(stageObject, _page.Script, type);
            return true;
            #endregion
        }

        /// <summary>
        /// Lance un script
        /// </summary>
        /// <param name="hotSpot"></param>
        /// <param name="script"></param>
        private void LaunchScript(VO_StageObject hotSpot, VO_Script script, Enums.TriggerEventConditionType type)
        {
            if (type == Enums.TriggerEventConditionType.ClickEvent && hotSpot.PlayerMustMove)
            {
                VO_RunningScript newScript = new VO_RunningScript();
                newScript.Id = Guid.NewGuid();
                newScript.Lines = new List<VO_Line>();
                newScript.ScriptType = Enums.ScriptType.Dynamic;
                VO_Script_MovePlayer moveScript = new VO_Script_MovePlayer();
                moveScript.Id = Guid.NewGuid();
                moveScript.Coords = hotSpot.PlayerPositionPoint;
                newScript.Lines.Add(moveScript);
                VO_Script_ChangePlayerDirection playerDirection = new VO_Script_ChangePlayerDirection();
                playerDirection.Id = Guid.NewGuid();
                playerDirection.Direction = hotSpot.PlayerMoveEndDirection;
                newScript.Lines.Add(playerDirection);
                VO_Script_CallScript callScript = new VO_Script_CallScript();
                callScript.Id = Guid.NewGuid();
                callScript.Script = script;
                newScript.Lines.Add(callScript);
                ScriptManager.AddParallelScript(newScript);
            }
            else
            {
                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.Id = script.Id;
                runningScript.Lines = script.Lines;
                switch (type)
                {
                    case Enums.TriggerEventConditionType.Automatic:
                    case Enums.TriggerEventConditionType.ClickEvent:
                    case Enums.TriggerEventConditionType.ContactEvent:
                        if (ScriptManager.CurrentScript == null)
                            ScriptManager.CurrentScript = runningScript;
                        break;
                    case Enums.TriggerEventConditionType.ParallelProcess:
                        ScriptManager.AddParallelScript(runningScript);
                        break;
                }
            }
        }

        /// <summary>
        /// Renvoie une bad interaction
        /// </summary>
        /// <param name="eventSpot"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        private VO_Dialog FindBadInteraction(VO_Event eventSpot, Guid classId)
        {
            //Filtre go
            if (ActionManager.CurrentActionIsGo())
                return null;

            VO_Class _class = GameCore.Instance.GetClassById(classId);
            VO_Dialog outputDialog = null;
            bool isAll = false;
            bool isPartial = false;

            if (_class == null)
                return null;

            foreach (VO_BadInteraction badInteraction in _class.BadInteractions)
            {
                //Mauvaise action
                if (badInteraction.Action != Guid.Empty && ActionManager.CurrentAction.Id != badInteraction.Action)
                    continue;
                //Mauvais character
                if (badInteraction.Character != Guid.Empty && PlayableCharactersManager.CurrentPlayerCharacter.Id != badInteraction.Character)
                    continue;

                //Action Character
                if (badInteraction.Action == ActionManager.CurrentAction.Id && PlayableCharactersManager.CurrentPlayerCharacter.Id == badInteraction.Character)
                {
                    return badInteraction.Dialog;
                }
                //Action ALL
                else if (!isPartial && badInteraction.Action == ActionManager.CurrentAction.Id && PlayableCharactersManager.CurrentPlayerCharacter.Id == Guid.Empty)
                {
                    isAll = false;
                    isPartial = true;
                    outputDialog = badInteraction.Dialog;
                }
                //ALL Character
                else if (!isPartial && badInteraction.Action == Guid.Empty && PlayableCharactersManager.CurrentPlayerCharacter.Id == badInteraction.Character)
                {
                    isAll = false;
                    isPartial = true;
                    outputDialog = badInteraction.Dialog;
                }
                //ALL ALL
                else if (!isAll && !isPartial && badInteraction.Action == Guid.Empty && badInteraction.Character == Guid.Empty)
                {
                    isAll = true;
                    outputDialog = badInteraction.Dialog;
                }
            }
            return outputDialog;
        }
        #endregion

        #region Gets sur objets
        /// <summary>
        /// Récupère le CharacterSprite en fonction d'un characterId
        /// Attention ! Ne récupère qu'une instance de ce character.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public VO_CharacterSprite GetCharacterSprite(Guid characterId)
        {
            //Tester le joueur
            if (PlayableCharactersManager.CurrentPlayerCharacter.Id == characterId || characterId == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                return PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite;

            //Tester les PNJ
            foreach (VO_CharacterSprite characterSprite in _Characters.Values)
            {
                if (characterSprite.Id == characterId)
                    return characterSprite;
            }
            return null;
        }

        /// <summary>
        /// Prépare un perso à être dessiné.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public VO_CharacterSprite DrawCharacter(VO_StageCharacter character)
        {
            int i = 0;
            foreach (VO_Page page in character.Event.PageList)
            {
                if (IsActivePage(page))
                {
                    VO_CharacterSprite characterSprite = GetCharacterSprite(character.Id);
                    if (characterSprite.CurrentExecutingPage != i)
                    {
                        characterSprite.Speed = page.CharacterSpeed;
                        characterSprite.StandingAnim = page.CharacterStandingType;
                        characterSprite.WalkingAnim = page.CharacterWalkingType;
                        characterSprite.TalkingAnim = page.CharacterTalkingType;
                        characterSprite.CurrentDirection = page.CharacterDirection;
                        characterSprite.SetCurrentAnimation(Enums.CharacterAnimationType.Standing, page.CharacterStandingType);
                        characterSprite.SetCurrentAnimationFrequency(page.CharacterAnimationFrequency);
                        characterSprite.CurrentExecutingPage = i;
                    }
                    return characterSprite;
                }
                i++;
            }

            return null;
        }

        /// <summary>
        /// Récupère l'index du calque en fonction de la position d'un personnage
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        public int GetLayerIndexFromCharacterLocation(Point coords)
        {
            return MatrixManager.CurrentStage.WalkableMatrix[coords.X, coords.Y];
        }

        /// <summary>
        /// Prépare une animation à être dessinée
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public VO_AnimatedSprite DrawAnimated(VO_StageAnimation animation)
        {
            int i = 0;
            foreach (VO_Page page in animation.Event.PageList)
            {
                if (IsActivePage(page))
                {
                    VO_AnimatedSprite animSprite = GetAnimatedSprite(animation.Id, Enums.StageObjectType.Animations);
                    if (animSprite.CurrentExecutingPage != i)
                    {
                        animSprite.SetFrequency(page.AnimationFrequency);
                        animSprite.Frozen = page.AnimationFrozenAtStart;
                        animSprite.CurrentExecutingPage = i;
                    }
                    return animSprite;
                }
                i++;
            }

            return null;
        }
        #endregion

        #region Messages
        /// <summary>
        /// Récupère les faces à afficher du dialogue en cours
        /// </summary>
        /// <param name="dialog">Dialog</param>
        /// <returns>Listes des faces</returns>
        public List<VO_AnimatedSprite> GetAnimatedFaces(VO_Dialog dialog)
        {
            List<VO_AnimatedSprite> output = new List<VO_AnimatedSprite>();
            /*List<Guid> importedChars = new List<Guid>();
            foreach (VO_Message mess in dialog.Messages)
            {
                if (!importedChars.Contains(mess.Character) && mess.Character != Guid.Empty)
                {
                    importedChars.Add(mess.Character);
                    VO_CharacterSprite character = GetCharacterSprite(mess.Character);
                    character.FaceAnim.SetPosition(character.FaceCoords.Location.X, character.FaceCoords.Location.Y);
                    output.Add(character.FaceAnim);
                }
            }*/
            //TODO FaceCoords
            return output;
        }

        /// <summary>
        /// Format Text
        /// </summary>
        /// <param name="message">VO_message du message</param>
        /// <returns>Sprite de texte</returns>
        public List<VO_String2D> FormatText(VO_Message message, VO_Size container, Point camera)
        {
            int cameraLeft = (int)camera.X;
            int cameraTop = (int)camera.Y;
            VO_CharacterSprite character = null;
            List<VO_String2D> textes = new List<VO_String2D>();
            if (message.Character == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                character = PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite;
            else
                character = GetCharacterSprite(message.Character);
            if (character != null && character.IsTalking)
            {
                //Calcul du Width idéal
                // Valeur idéale = ViewerConstants.MESS_MAXIMUM_WIDTH / en 640
                int idealValue = ViewerConstants.MESS_MAXIMUM_WIDTH * container.Width / 640;
                int miniValue = ViewerConstants.MESS_MINIMUM_WIDTH * container.Width / 640;
                int xLeft = character.Location.X - cameraLeft - idealValue / 2;
                int xRight = character.Location.X - cameraLeft + idealValue / 2;

                //Cas où le texte mord sur la gauche
                if (xLeft < ViewerConstants.MESS_PADDING_BORDER)
                {
                    xLeft = ViewerConstants.MESS_PADDING_BORDER;
                    if (xRight - xLeft < miniValue)
                        xRight = miniValue;
                }
                //Cas où le texte mord sur la droite
                else if (xRight > container.Width - ViewerConstants.MESS_PADDING_BORDER)
                {
                    xRight = container.Width - 1 - ViewerConstants.MESS_PADDING_BORDER;
                    if (xRight - xLeft < miniValue)
                        xLeft = container.Width - miniValue - ViewerConstants.MESS_PADDING_BORDER;
                }

                //1 - On détermine la hauteur que devra prendre le texte
                int lineWidth = xRight - xLeft;
                string[] words = message.Text.Split(" ".ToCharArray());
                string finalMess = string.Empty;
                string tempMess = string.Empty;
                int height = ViewerConstants.MESS_PADDING_CHARACTER;
                VO_String2D text = AddText(message.FontSize, character.DialogColor);
                foreach (string word in words)
                {
                    //On test si le texte ne dépasse pas le width
                    if (string.IsNullOrEmpty(tempMess))
                        tempMess = word;
                    else
                        tempMess = finalMess + " " + word;
                    text.Text = tempMess;

                    if ((int)text.Destination.Width > lineWidth)
                    {
                        text.Text = finalMess;
                        height += (int)text.Destination.Height + ViewerConstants.MESS_PADDING_LINES;
                        textes.Add(text);

                        //New message
                        text = AddText(message.FontSize, character.DialogColor);
                        finalMess = word;
                    }
                    else
                        finalMess = tempMess;
                }
                text.Text = finalMess;
                height += (int)text.Destination.Height;
                textes.Add(text);

                int finalY = character.GetAnimPosition().Y - cameraTop - height;
                if (finalY < ViewerConstants.MESS_PADDING_BORDER)
                    finalY = ViewerConstants.MESS_PADDING_BORDER;
                Point pos = new Point(character.Location.X - cameraLeft, finalY);

                for (int i = 0; i < textes.Count; i++)
                {
                    int newX = character.Location.X - cameraLeft - ((int)textes[i].Destination.Width / 2);
                    int newX2 = newX + (int)textes[i].Destination.Width;
                    if (newX < ViewerConstants.MESS_PADDING_BORDER)
                        newX = ViewerConstants.MESS_PADDING_BORDER;
                    else if (newX2 > container.Width - ViewerConstants.MESS_PADDING_BORDER)
                        newX -= newX2 - container.Width + ViewerConstants.MESS_PADDING_BORDER;
                    textes[i].Position = new Vector2(newX, finalY + i * ((int)text.Destination.Height + ViewerConstants.MESS_PADDING_LINES));
                }
            }

            return textes;
        }

        /// <summary>
        /// Ajoute un texte
        /// </summary>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private VO_String2D AddText(int fontSize, VO_Color color)
        {
            VO_String2D text = new VO_String2D(ViewerConstants.FONT_MAINFONT, fontSize);
            text.Color = Tools.GetXNAColorFromVOColor(color);

            return text;
        }

        /// <summary>
        /// Récupère le stage courant
        /// </summary>
        /// <returns></returns>
        public VO_Stage GetCurrentStage()
        {
            return _CurrentStage;
        }
        #endregion

        #region Camera
        /// <summary>
        /// Fixer la caméra à un personnage
        /// </summary>
        /// <param name="character">Personnage</param>
        /// <returns>Position</returns>
        public Vector2 GetCameraCoords(Point location)
        {
            /*Rectangle rect = new Rectangle();

            //Left/Top
            rect.X = location.X - GameCore.Instance.Game.Project.Resolution.Width / 2;
            if (rect.Left < 0)
                rect.X = 0;
            rect.Y = location.Y - GameCore.Instance.Game.Project.Resolution.Height / 2;
            if (rect.Left < 0)
                rect.Y = 0;

            //Right/Bottom
            rect.Width = GameCore.Instance.Game.Project.Resolution.Width;
            if (rect.Right > _CurrentStage.Dimensions.Width)
            {
                rect.X = _CurrentStage.Dimensions.Width - GameCore.Instance.Game.Project.Resolution.Width;
                rect.Width = GameCore.Instance.Game.Project.Resolution.Width;
            }
            rect.Height = GameCore.Instance.Game.Project.Resolution.Height;
            if (rect.Bottom > _CurrentStage.Dimensions.Height)
            {
                rect.Y = _CurrentStage.Dimensions.Height - GameCore.Instance.Game.Project.Resolution.Height;
                rect.Height = rect.Top + GameCore.Instance.Game.Project.Resolution.Height;
            }

            return rect;*/

            //Left/Top
            int x = 0;
            int y = 0;
            int width = GameCore.Instance.Game.Project.Resolution.Width;
            int height = GameCore.Instance.Game.Project.Resolution.Height;

            x = location.X - width / 2;
            if (x < 0)
                x = 0;
            y = location.Y - height / 2;
            if (y < 0)
                y = 0;
            width += x;
            height += y;

            //Right/Bottom
            if (width > _CurrentStage.Dimensions.Width)
            {
                x = _CurrentStage.Dimensions.Width - GameCore.Instance.Game.Project.Resolution.Width;
            }
            if (height > _CurrentStage.Dimensions.Height)
            {
                y = _CurrentStage.Dimensions.Height - GameCore.Instance.Game.Project.Resolution.Height;
            }

            return new Vector2(x, y);
        }
        #endregion
        #endregion
    }
}
