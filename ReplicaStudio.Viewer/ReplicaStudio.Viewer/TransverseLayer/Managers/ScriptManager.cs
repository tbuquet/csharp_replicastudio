using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.PresentationLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.Algorithms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.DataLayer;
using ReplicaStudio.Viewer.ServiceLayer;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Gestion des scripts
    /// </summary>
    public static class ScriptManager
    {
        #region Members
        /// <summary>
        /// Scripts à détruire
        /// </summary>
        private static List<VO_RunningScript> _ToDeleteScripts;

        /// <summary>
        /// Référence au service
        /// </summary>
        private static StageService _Service;

        /// <summary>
        /// Référence au Game
        /// </summary>
        private static Viewer _Game;

        /// <summary>
        /// Stage courant
        /// </summary>
        private static VO_Stage _CurrentStage;

        /// <summary>
        /// Référence à l'interface
        /// </summary>
        private static Interface _Interface;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        private static SpriteBatch _SpriteBatch;
        #endregion

        #region Properties
        /// <summary>
        /// Scripts en parallel process
        /// </summary>
        public static List<VO_RunningScript> ParallelScripts { get; set; }

        /// <summary>
        /// Script courant
        /// </summary>
        public static VO_RunningScript CurrentScript { get; set; }

        /// <summary>
        /// Choice courant
        /// </summary>
        public static VO_SelectableMenu CurrentChoice { get; set; }

        #region Game Properties
        /// <summary>
        /// Is the Player visible?
        /// </summary>
        public static bool IsPlayerVisible { get; set; }

        /// <summary>
        /// Script stage interaction
        /// </summary>
        public static bool ScriptStagesInteractions { get; set; }

        /// <summary>
        /// Script user controls
        /// </summary>
        public static bool ScriptUserControls { get; set; }

        /// <summary>
        /// GameOver
        /// </summary>
        public static bool GameOver { get; set; }

        /// <summary>
        /// Informations de changement d'écran
        /// </summary>
        public static GameScreenEventArgs GoToScreen { get; set; }

        /// <summary>
        /// Controls
        /// </summary>
        public static ViewerEnums.BlockType BlockType { get; set; }

        /// <summary>
        /// Bloque les actions
        /// </summary>
        public static void BlockControls(ViewerEnums.BlockType type)
        {
            ReleaseControls();
            BlockType = type;

        }
        /// <summary>
        /// Libère toutes les actions
        /// </summary>
        public static void ReleaseControls()
        {
            BlockType = ViewerEnums.BlockType.Free;
            _Interface.Enabled = true;
        }
        #endregion

        #region Camera
        public static bool LocationsInUse { get; set; }
        public static Point CamLocations { get; set; }
        public static Guid CamAnimation { get; set; }
        public static Guid CamCharacter { get; set; }
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// Initialise le manager
        /// </summary>
        /// <param name="service"></param>
        /// <param name="game"></param>
        /// <param name="currentStage"></param>
        /// <param name="interfce"></param>
        /// <param name="spriteBatch"></param>
        public static void InitScriptManager(StageService service, Viewer game, VO_Stage currentStage, Interface interfce, SpriteBatch spriteBatch)
        {
            _Service = service;
            _Game = game;
            _CurrentStage = currentStage;
            _SpriteBatch = spriteBatch;
            _Interface = interfce;
            ScriptStagesInteractions = true;
            ScriptUserControls = true;
            IsPlayerVisible = true;
            BlockType = ViewerEnums.BlockType.Free;
        }

        /// <summary>
        /// Reset du script manager
        /// </summary>
        public static void ResetScriptManager()
        {
            CurrentScript = null;
            if (ParallelScripts != null)
                ParallelScripts.Clear();
        }

        /// <summary>
        /// Ajoute un script parallele
        /// </summary>
        /// <param name="script"></param>
        public static void AddParallelScript(VO_RunningScript script)
        {
            if (ParallelScripts == null)
                ParallelScripts = new List<VO_RunningScript>();
            if (ParallelScripts.Find(p => p.Id == script.Id) == null)
                ParallelScripts.Add(script);
        }

        /// <summary>
        /// Supprimer un script
        /// </summary>
        /// <param name="script"></param>
        public static void RemoveParallelScript(VO_RunningScript script)
        {
            if (_ToDeleteScripts == null)
                _ToDeleteScripts = new List<VO_RunningScript>();
            _ToDeleteScripts.Add(script);
        }

        /// <summary>
        /// Déclenche les scripts parallel
        /// </summary>
        public static void RunParallelScripts()
        {
            if (ParallelScripts != null)
            {
                foreach (VO_RunningScript script in ParallelScripts)
                {
                    RunScript(script, true);
                }

            }
            if (_ToDeleteScripts != null)
            {
                foreach (VO_RunningScript script in _ToDeleteScripts)
                {
                    ParallelScripts.Remove(script);
                }
                _ToDeleteScripts = null;
            }
        }

        /// <summary>
        /// Run un script
        /// </summary>
        /// <param name="script"></param>
        public static void RunScript(VO_RunningScript script, bool isProcess)
        {
            //Script actif
            if (script == null)
                return;

            //On teste si c'est le début du script
            if (script.CurrentLine == null && script.Lines.Count > 0)
            {
                if (!isProcess)
                    PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.StopPath();
                script.CurrentLine = script.Lines[0];
                if (script == CurrentScript)
                    BlockControls(ViewerEnums.BlockType.BlockUserControls);
            }

            //On décrémente le temps d'attente
            if (script.WaitFrames > 0)
                script.WaitFrames--;

            RunLine(script, script.CurrentLine);
        }

        /// <summary>
        /// Run Line
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="currentLine"></param>
        public static void RunLine(VO_RunningScript currentScript, VO_Line currentLine)
        {
            //Execute la ligne.
            ViewerEnums.ScriptReturn code = ExecuteLine(currentScript, currentLine);

            switch (code)
            {
                case ViewerEnums.ScriptReturn.Normal:
                    currentScript.WaitFrames = 0;
                    currentScript.ObjectState = 0;
                    currentScript.CurrentLine = FindNextLine(currentScript.Lines, currentLine);
                    if (currentScript.CurrentLine != null)
                    {
                        RunLine(currentScript, currentScript.CurrentLine);
                        return;
                    }
                    break;
                case ViewerEnums.ScriptReturn.Wait:
                    break;
                case ViewerEnums.ScriptReturn.Choice:
                    if (((VO_Script_ChoiceMessage)currentLine).Choices[currentScript.ObjectState].SubLines.Count > 0)
                        currentScript.CurrentLine = ((VO_Script_ChoiceMessage)currentLine).Choices[currentScript.ObjectState].SubLines[0];
                    else
                        currentScript.CurrentLine = null;
                    currentScript.WaitFrames = 0;
                    currentScript.ObjectState = 0;
                    if (currentScript.CurrentLine != null)
                    {
                        RunLine(currentScript, currentScript.CurrentLine);
                        return;
                    }
                    break;
                case ViewerEnums.ScriptReturn.If:
                    if (((VO_Script_Condition)currentLine).IfSubLines.Count > 0)
                        currentScript.CurrentLine = ((VO_Script_Condition)currentLine).IfSubLines[0];
                    else
                        currentScript.CurrentLine = null;
                    currentScript.WaitFrames = 0;
                    currentScript.ObjectState = 0;
                    if (currentScript.CurrentLine != null)
                    {
                        RunLine(currentScript, currentScript.CurrentLine);
                        return;
                    }
                    break;
                case ViewerEnums.ScriptReturn.Else:
                    if (((VO_Script_Condition)currentLine).ElseSubLines.Count > 0)
                        currentScript.CurrentLine = ((VO_Script_Condition)currentLine).ElseSubLines[0];
                    else
                        currentScript.CurrentLine = null;
                    currentScript.WaitFrames = 0;
                    currentScript.ObjectState = 0;
                    if (currentScript.CurrentLine != null)
                    {
                        RunLine(currentScript, currentScript.CurrentLine);
                        return;
                    }
                    break;
                case ViewerEnums.ScriptReturn.While:
                    if (((VO_Script_Loop)currentLine).WhileSubLines.Count > 0)
                        currentScript.CurrentLine = ((VO_Script_Loop)currentLine).WhileSubLines[0];
                    else
                        currentScript.CurrentLine = null;
                    currentScript.WaitFrames = 0;
                    currentScript.ObjectState = 0;
                    if (currentScript.CurrentLine != null)
                    {
                        RunLine(currentScript, currentScript.CurrentLine);
                        return;
                    }
                    break;
                case ViewerEnums.ScriptReturn.Break:
                    currentScript.WaitFrames = 0;
                    currentScript.ObjectState = 0;
                    currentScript.CurrentLine = FindNextLine(currentScript.Lines, currentScript.CurrentLine);
                    if (currentScript.CurrentLine != null)
                    {
                        RunLine(currentScript, currentScript.CurrentLine);
                        return;
                    }
                    break;
                case ViewerEnums.ScriptReturn.Abort:
                    if (currentScript == CurrentScript)
                    {
                        CurrentScript = null;
                        ReleaseControls();
                    }
                    else
                        RemoveParallelScript(currentScript);
                    break;
            }
            if (currentScript.CurrentLine == null)
            {
                if (currentScript == CurrentScript)
                {
                    CurrentScript = null;
                    ReleaseControls();
                }
                else
                    RemoveParallelScript(currentScript);
            }
        }

        /// <summary>
        /// Trouve la prochaine ligne du même niveau
        /// </summary>
        /// <param name="currentLine"></param>
        private static VO_Line FindNextLine(List<VO_Line> lines, VO_Line currentLine)
        {
            bool lineFound = false;
            foreach (VO_Line line in lines)
            {
                if (lineFound)
                    return line;
                if (line.Id == currentLine.Id)
                    lineFound = true;
                else if (line is VO_Script_Condition)
                {
                    VO_Line condition = null;
                    condition = FindNextLine(((VO_Script_Condition)line).IfSubLines, currentLine);
                    if (condition == null)
                    {
                        condition = FindNextLine(((VO_Script_Condition)line).ElseSubLines, currentLine);
                    }
                    if (condition != null)
                        return condition;
                }
                else if (line is VO_Script_Loop)
                {
                    VO_Line loop = FindNextLine(((VO_Script_Loop)line).WhileSubLines, currentLine);
                    if (loop != null)
                        return loop;
                }
                else if (line is VO_Script_ChoiceMessage)
                {
                    VO_Line choiceLine = null;
                    VO_Script_ChoiceMessage choiceScript = (VO_Script_ChoiceMessage)line;
                    foreach (VO_LineChoices choice in choiceScript.Choices)
                    {
                        choiceLine = FindNextLine(choice.SubLines, currentLine);
                        if (choiceLine != null)
                            return choiceLine;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Code d'execution d'une ligne
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="currentLine"></param>
        /// <returns></returns>
        public static ViewerEnums.ScriptReturn ExecuteLine(VO_RunningScript currentScript, VO_Line currentLine)
        {
            if (currentLine is VO_Script_Message)
                return ScriptShowMessage(currentScript, (VO_Script_Message)currentLine);
            if (currentLine is VO_Script_CallScript)
                return ScriptCallScript((VO_Script_CallScript)currentLine);
            if (currentLine is VO_Script_MovePlayer)
                return ScriptMovePlayer(currentScript, (VO_Script_MovePlayer)currentLine);
            if (currentLine is VO_Script_ChangePlayerDirection)
                return ScriptChangePlayerDirection(currentScript, (VO_Script_ChangePlayerDirection)currentLine);
            if (currentLine is VO_Script_ChoiceMessage)
                return ScriptChoices(currentScript, (VO_Script_ChoiceMessage)currentLine);
            if (currentLine is VO_Script_Wait)
                return ScriptWait(currentScript, (VO_Script_Wait)currentLine);
            if (currentLine is VO_Script_PressSwitch)
                return ScriptPressSwitch(currentScript, (VO_Script_PressSwitch)currentLine);
            if (currentLine is VO_Script_Condition)
                return ScriptCondition(currentScript, (VO_Script_Condition)currentLine);
            if (currentLine is VO_Script_Loop)
                return ScriptLoop(currentScript, (VO_Script_Loop)currentLine);
            if (currentLine is VO_Script_RollBackWhile)
                return ScriptRollBackWhile(currentScript, (VO_Script_RollBackWhile)currentLine);
            if (currentLine is VO_Script_Break)
                return ScriptBreak(currentScript, (VO_Script_Break)currentLine);
            if (currentLine is VO_Script_ChangeVariable)
                return ScriptChangeVariable(currentScript, (VO_Script_ChangeVariable)currentLine);
            if (currentLine is VO_Script_GoToAnchor)
                return ScriptGoToAnchor(currentScript, (VO_Script_GoToAnchor)currentLine);
            if (currentLine is VO_Script_Random)
                return ScriptRandom(currentScript, (VO_Script_Random)currentLine);
            if (currentLine is VO_Script_DisableStageInteractions)
                return ScriptDisableStageInteractions();
            if (currentLine is VO_Script_DisableUserControls)
                return ScriptDisableUserControls();
            if (currentLine is VO_Script_EnableStageInteractions)
                return ScriptEnableStageInteractions();
            if (currentLine is VO_Script_EnableUserControls)
                return ScriptEnableUserControls();
            if (currentLine is VO_Script_TitleScreen)
                return ScriptGoToTitle();
            if (currentLine is VO_Script_AddItem)
                return ScriptAddItem(currentScript, (VO_Script_AddItem)currentLine);
            if (currentLine is VO_Script_RemoveItem)
                return ScriptRemoveItem(currentScript, (VO_Script_RemoveItem)currentLine);
            if (currentLine is VO_Script_Teleport)
                return ScriptTeleport(currentScript, (VO_Script_Teleport)currentLine);
            if (currentLine is VO_Script_HideLifeBar)
                return ScriptHideLifeBar();
            if (currentLine is VO_Script_ShowLifeBar)
                return ScriptShowLifeBar();
            if (currentLine is VO_Script_GameOver)
                return ScriptGameOver();
            if (currentLine is VO_Script_SaveGame)
                return ScriptEnableSaves();
            if (currentLine is VO_Script_LoadGame)
                return ScriptDisableSaves();
            if (currentLine is VO_Script_ChangeHP)
                return ScriptChangePlayerHP(currentScript, (VO_Script_ChangeHP)currentLine);
            if (currentLine is VO_Script_ChangeMaxHP)
                return ScriptChangePlayerMaxHP(currentScript, (VO_Script_ChangeMaxHP)currentLine);
            if (currentLine is VO_Script_OpenInventory)
                return ScriptOpenInventory();
            if (currentLine is VO_Script_CloseInventory)
                return ScriptCloseInventory();
            if (currentLine is VO_Script_ChangeCurrentCharacter)
                return ScriptChangeCurrentCharacter(currentScript, (VO_Script_ChangeCurrentCharacter)currentLine);
            if (currentLine is VO_Script_StopCurrentPlayerMovement)
                return ScriptStopCurrentPlayerMovement();
            if (currentLine is VO_Script_StopCharacterMovements)
                return ScriptStopCharacterMovement(currentScript, (VO_Script_StopCharacterMovements)currentLine);
            if (currentLine is VO_Script_PlayMusic)
                return ScriptPlayMusic(currentScript, (VO_Script_PlayMusic)currentLine);
            if (currentLine is VO_Script_PlaySound)
                return ScriptPlaySound(currentScript, (VO_Script_PlaySound)currentLine);
            if (currentLine is VO_Script_StopMusic)
                return ScriptStopMusic();
            if (currentLine is VO_Script_StopSound)
                return ScriptStopSound();
            if (currentLine is VO_Script_ChangeMusicFrequency)
                return ScriptChangeMusicFrequency(currentScript, (VO_Script_ChangeMusicFrequency)currentLine);
            if (currentLine is VO_Script_ChangeCharacterDirection)
                return ScriptChangeCharacterDirection(currentScript, (VO_Script_ChangeCharacterDirection)currentLine);
            if (currentLine is VO_Script_MoveCharacter)
                return ScriptMoveCharacter(currentScript, (VO_Script_MoveCharacter)currentLine);
            if (currentLine is VO_Script_FreezeCharacterAnimation)
                return ScriptFreezeCharacterAnimation(currentScript, (VO_Script_FreezeCharacterAnimation)currentLine);
            if (currentLine is VO_Script_FreeCharacterAnimation)
                return ScriptFreeCharacterAnimation(currentScript, (VO_Script_FreeCharacterAnimation)currentLine);
            if (currentLine is VO_Script_ChangeCharacterAnimFrequency)
                return ScriptChangeCharacterAnimationFrequency(currentScript, (VO_Script_ChangeCharacterAnimFrequency)currentLine);
            if (currentLine is VO_Script_CallGlobalEvent)
                return ScriptCallGlobalEvent(currentScript, (VO_Script_CallGlobalEvent)currentLine);
            if (currentLine is VO_Script_AddPlayerAction)
                return ScriptAddPlayerAction(currentScript, (VO_Script_AddPlayerAction)currentLine);
            if (currentLine is VO_Script_RemovePlayerAction)
                return ScriptRemovePlayerAction(currentScript, (VO_Script_RemovePlayerAction)currentLine);
            if (currentLine is VO_Script_LookForwardPlayer)
                return ScriptLookForwardPlayer(currentScript, (VO_Script_LookForwardPlayer)currentLine);
            if (currentLine is VO_Script_ChangePlayerSpeed)
                return ScriptChangePlayerSpeed(currentScript, (VO_Script_ChangePlayerSpeed)currentLine);
            if (currentLine is VO_Script_ChangePlayerAnimation)
                return ScriptChangePlayerAnimation(currentScript, (VO_Script_ChangePlayerAnimation)currentLine);
            if (currentLine is VO_Script_FreezePlayerAnimation)
                return ScriptFreezePlayerAnimation(currentScript, (VO_Script_FreezePlayerAnimation)currentLine);
            if (currentLine is VO_Script_FreePlayerAnimation)
                return ScriptFreePlayerAnimation(currentScript, (VO_Script_FreePlayerAnimation)currentLine);
            if (currentLine is VO_Script_MoveCamera)
                return ScriptMoveCamera(currentScript, (VO_Script_MoveCamera)currentLine);
            if (currentLine is VO_Script_DefaultCamera)
                return ScriptDefaultCamera();
            if (currentLine is VO_Script_FocusOnCharacter)
                return ScriptFocusOnCharacter(currentScript, (VO_Script_FocusOnCharacter)currentLine);
            if (currentLine is VO_Script_FocusOnAnimation)
                return ScriptFocusOnAnimation(currentScript, (VO_Script_FocusOnAnimation)currentLine);
            if (currentLine is VO_Script_ShowPlayer)
                return ScriptShowPlayer();
            if (currentLine is VO_Script_HidePlayer)
                return ScriptHidePlayer();
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Récupère un perso CharacterSprite depuis un Id
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static VO_Player GetPlayerCharacterSpriteFromId(Guid value)
        {
            if (value == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                return PlayableCharactersManager.CurrentPlayerCharacter;
            else
                return PlayableCharactersManager.GetPlayer(value);
        }

        #region Scripts
        #region Player
        #region Choice Script
        /// <summary>
        /// Script Show Choices
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChoices(VO_RunningScript currentScript, VO_Script_ChoiceMessage message)
        {
            if (CurrentChoice == null)
            {
                CurrentChoice = new VO_SelectableMenu(_SpriteBatch, _Game, message.Choices, 10);
                CurrentChoice.FontSize = 20;
                CurrentChoice.LimitView = 4;
                CurrentChoice.Position = new Vector2(0, GameCore.Instance.Game.Project.Resolution.Height - CurrentChoice.Height);
                CurrentChoice.ForceWidth(GameCore.Instance.Game.Project.Resolution.Width);
                CurrentChoice.OnClick += new VO_SelectableMenu.OnClickEventHandler(CurrentChoice_OnClick);
                currentScript.ObjectState = -1;
                ActionManager.SetCurrentActionToGo();
            }
            if (currentScript.ObjectState < 0)
                return ViewerEnums.ScriptReturn.Wait;
            CurrentChoice.Dispose();
            CurrentChoice = null;
            return ViewerEnums.ScriptReturn.Choice;
        }

        static void CurrentChoice_OnClick(object sender, EventArgsClasses.GameMenuEventArgs e)
        {
            CurrentScript.ObjectState = e.SelectedIndex;
        }
        #endregion

        /// <summary>
        /// Script ShowPlayer
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptShowPlayer()
        {
            IsPlayerVisible = true;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script HidePlayer
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptHidePlayer()
        {
            IsPlayerVisible = false;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script FreezePlayerAnimation
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="freezePlayerAnimationScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptFreezePlayerAnimation(VO_RunningScript currentScript, VO_Script_FreezePlayerAnimation freezePlayerAnimationScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(freezePlayerAnimationScript.Character);
            if (player != null)
            {
                if (freezePlayerAnimationScript.FreezeAll)
                {
                    player.CharacterSprite.FreezeAnimation(player.CharacterSprite.StandingAnim);
                    player.CharacterSprite.FreezeAnimation(player.CharacterSprite.WalkingAnim);
                    player.CharacterSprite.FreezeAnimation(player.CharacterSprite.TalkingAnim);
                }
                else
                {
                    switch (freezePlayerAnimationScript.AnimationType)
                    {
                        case Enums.CharacterAnimationType.Standing:
                            player.CharacterSprite.FreezeAnimation(player.CharacterSprite.StandingAnim);
                            break;
                        case Enums.CharacterAnimationType.Walking:
                            player.CharacterSprite.FreezeAnimation(player.CharacterSprite.WalkingAnim);
                            break;
                        case Enums.CharacterAnimationType.Talking:
                            player.CharacterSprite.FreezeAnimation(player.CharacterSprite.TalkingAnim);
                            break;
                    }
                }
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script FreePlayerAnimation
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="freezePlayerAnimationScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptFreePlayerAnimation(VO_RunningScript currentScript, VO_Script_FreePlayerAnimation freePlayerAnimationScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(freePlayerAnimationScript.Character);
            if (player != null)
            {
                if (freePlayerAnimationScript.FreeAll)
                {
                    player.CharacterSprite.FreeAnimation(player.CharacterSprite.StandingAnim);
                    player.CharacterSprite.FreeAnimation(player.CharacterSprite.WalkingAnim);
                    player.CharacterSprite.FreeAnimation(player.CharacterSprite.TalkingAnim);
                }
                else
                {
                    switch (freePlayerAnimationScript.AnimationType)
                    {
                        case Enums.CharacterAnimationType.Standing:
                            player.CharacterSprite.FreeAnimation(player.CharacterSprite.StandingAnim);
                            break;
                        case Enums.CharacterAnimationType.Walking:
                            player.CharacterSprite.FreeAnimation(player.CharacterSprite.WalkingAnim);
                            break;
                        case Enums.CharacterAnimationType.Talking:
                            player.CharacterSprite.FreeAnimation(player.CharacterSprite.TalkingAnim);
                            break;
                    }
                }
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script ChangePlayerAnimation
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changePlayerAnimationScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangePlayerAnimation(VO_RunningScript currentScript, VO_Script_ChangePlayerAnimation changePlayerAnimationScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(changePlayerAnimationScript.Character);
            if (player != null)
            {
                switch (changePlayerAnimationScript.AnimationType)
                {
                    case Enums.CharacterAnimationType.Standing:
                        if (player.CharacterSprite.OldStanding == Guid.Empty)
                        {
                            if (!changePlayerAnimationScript.Loop)
                                player.CharacterSprite.OldStanding = player.CharacterSprite.StandingAnim;
                            player.CharacterSprite.StandingAnim = changePlayerAnimationScript.Animation;
                            player.CharacterSprite.CharacterStand();
                        }
                        break;
                    case Enums.CharacterAnimationType.Walking:
                        if (player.CharacterSprite.OldWalking == Guid.Empty)
                        {
                            if (!changePlayerAnimationScript.Loop)
                                player.CharacterSprite.OldWalking = player.CharacterSprite.WalkingAnim;
                            player.CharacterSprite.WalkingAnim = changePlayerAnimationScript.Animation;
                            if (player.CharacterSprite.CurrentPath != null)
                                player.CharacterSprite.CharacterWalk();

                        }
                        break;
                    case Enums.CharacterAnimationType.Talking:
                        if (player.CharacterSprite.OldTalking == Guid.Empty)
                        {
                            if (!changePlayerAnimationScript.Loop)
                                player.CharacterSprite.OldTalking = player.CharacterSprite.TalkingAnim;
                            player.CharacterSprite.TalkingAnim = changePlayerAnimationScript.Animation;
                            if (player.CharacterSprite.IsTalking)
                                player.CharacterSprite.CharacterTalk();
                        }
                        break;
                }
                player.CharacterSprite.ResetAnimationIndex();

            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script AddPlayerAction
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCurrentCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptAddPlayerAction(VO_RunningScript currentScript, VO_Script_AddPlayerAction addPlayerActionScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(addPlayerActionScript.CharacterId);
            if (player != null)
            {
                player.AddAction(addPlayerActionScript.ActionId);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script RemovePlayerAction
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCurrentCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptRemovePlayerAction(VO_RunningScript currentScript, VO_Script_RemovePlayerAction removePlayerActionScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(removePlayerActionScript.CharacterId);
            if (player != null)
            {
                player.RemoveAction(removePlayerActionScript.ActionId);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script ChangePlayerSpeed
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCurrentCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangePlayerSpeed(VO_RunningScript currentScript, VO_Script_ChangePlayerSpeed changePlayerSpeedScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(changePlayerSpeedScript.CharacterId);
            if (player != null)
            {
                player.CharacterSprite.Speed = Tools.GetVariableValue(changePlayerSpeedScript.Speed);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script StopCurrentPlayerMovement
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="addItemScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptStopCurrentPlayerMovement()
        {
            PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.StopPath();
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script ChangeCurrentCharacter
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCurrentCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangeCurrentCharacter(VO_RunningScript currentScript, VO_Script_ChangeCurrentCharacter changeCurrentCharacterScript)
        {
            if (changeCurrentCharacterScript.Character == PlayableCharactersManager.CurrentPlayerCharacter.Id)
                return ViewerEnums.ScriptReturn.Normal;

            VO_Stage destinationStage = null;
            if (!changeCurrentCharacterScript.UseOldCoords)
            {
                if (changeCurrentCharacterScript.Coords == null)
                    return ViewerEnums.ScriptReturn.Normal;
                destinationStage = GameCore.Instance.GetStageById(changeCurrentCharacterScript.Coords.Map);
                if (destinationStage == null)
                    return ViewerEnums.ScriptReturn.Normal;
            }

            VO_Coords currentCoords = new VO_Coords(new System.Drawing.Point(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.X, PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.Y), PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage);
            PlayableCharactersManager.CurrentPlayerCharacter = PlayableCharactersManager.GetPlayer(changeCurrentCharacterScript.Character);

            if (changeCurrentCharacterScript.UseOldCoords)
            {
                if (PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage != _CurrentStage.Id)
                {
                    VO_Script_Teleport teleport = new VO_Script_Teleport();
                    
                    if (PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage == new Guid())
                        teleport.Coords = currentCoords;
                    else
                        teleport.Coords = new VO_Coords(new System.Drawing.Point(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.X, PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.Y), PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage);
                    return ScriptTeleport(currentScript, teleport);
                }
                return ViewerEnums.ScriptReturn.Normal;
            }
            else
            {
                if (destinationStage != null && destinationStage.Id != _CurrentStage.Id)
                {
                    PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage = destinationStage.Id;
                    VO_Script_Teleport teleport = new VO_Script_Teleport();
                    teleport.Coords = changeCurrentCharacterScript.Coords;
                    return ScriptTeleport(currentScript, teleport);
                }
                else
                {
                    PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.SetPosition(changeCurrentCharacterScript.Coords.Location.X, changeCurrentCharacterScript.Coords.Location.Y);
                    return ViewerEnums.ScriptReturn.Normal;
                }
            }
        }

        /// <summary>
        /// Script ChangePlayerHP
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="addItemScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangePlayerHP(VO_RunningScript currentScript, VO_Script_ChangeHP changeHPScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(changeHPScript.CharacterId);
            switch (changeHPScript.Operator)
            {
                case Enums.ChangeOperator.Add:
                    player.PvAtStart += Tools.GetVariableValue(changeHPScript.Value);
                    break;
                case Enums.ChangeOperator.Divide:
                    player.PvAtStart /= Tools.GetVariableValue(changeHPScript.Value);
                    break;
                case Enums.ChangeOperator.Multiply:
                    player.PvAtStart *= Tools.GetVariableValue(changeHPScript.Value);
                    break;
                case Enums.ChangeOperator.Set:
                    player.PvAtStart = Tools.GetVariableValue(changeHPScript.Value);
                    break;
                case Enums.ChangeOperator.Sub:
                    player.PvAtStart -= Tools.GetVariableValue(changeHPScript.Value);
                    break;
            }
            if (player.PvAtStart <= 0)
            {
                return ScriptGameOver();
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script ChangePlayerHP
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="addItemScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangePlayerMaxHP(VO_RunningScript currentScript, VO_Script_ChangeMaxHP changeMaxHPScript)
        {
            VO_Player player = GetPlayerCharacterSpriteFromId(changeMaxHPScript.CharacterId);
            switch (changeMaxHPScript.Operator)
            {
                case Enums.ChangeOperator.Add:
                    player.PvAtStart += Tools.GetVariableValue(changeMaxHPScript.Value);
                    break;
                case Enums.ChangeOperator.Divide:
                    player.PvAtStart /= Tools.GetVariableValue(changeMaxHPScript.Value);
                    break;
                case Enums.ChangeOperator.Multiply:
                    player.PvAtStart *= Tools.GetVariableValue(changeMaxHPScript.Value);
                    break;
                case Enums.ChangeOperator.Set:
                    player.PvAtStart = Tools.GetVariableValue(changeMaxHPScript.Value);
                    break;
                case Enums.ChangeOperator.Sub:
                    player.PvAtStart -= Tools.GetVariableValue(changeMaxHPScript.Value);
                    break;
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script Teleport
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="addItemScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptTeleport(VO_RunningScript currentScript, VO_Script_Teleport teleportScript)
        {
            GoToScreen = new EventArgsClasses.GameScreenEventArgs(ViewerEnums.ScreenType.Stage, teleportScript.Coords.Map, new Point(teleportScript.Coords.Location.X, teleportScript.Coords.Location.Y));
            return ViewerEnums.ScriptReturn.Abort;
        }

        /// <summary>
        /// Script AddItem
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptAddItem(VO_RunningScript currentScript, VO_Script_AddItem addItemScript)
        {
            VO_Item item = GameCore.Instance.GetItemById(addItemScript.Item);
            if (item != null)
            {
                VO_Player player = null;
                if (addItemScript.Character == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                    player = PlayableCharactersManager.CurrentPlayerCharacter;
                else
                {
                    player = PlayableCharactersManager.GetPlayer(addItemScript.Character);
                }
                player.AddItem(addItemScript.Item);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script RemoveItem
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptRemoveItem(VO_RunningScript currentScript, VO_Script_RemoveItem removeItemScript)
        {
            VO_Item item = GameCore.Instance.GetItemById(removeItemScript.Item);
            if (item != null)
            {
                VO_Player player = null;
                if (removeItemScript.Character == new Guid(GlobalConstants.CURRENT_PLAYER_ID))
                    player = PlayableCharactersManager.CurrentPlayerCharacter;
                else
                {
                    player = PlayableCharactersManager.GetPlayer(removeItemScript.Character);
                }
                player.RemoveItem(item.Id);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script Show Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptShowMessage(VO_RunningScript currentScript, VO_Script_Message message)
        {
            VO_CharacterSprite charSprite = null;

            if (currentScript.WaitFrames == 0 && currentScript.ObjectState == 0)
            {
                currentScript.WaitFrames = message.Dialog.Messages[currentScript.ObjectState].Duration * 10;
                charSprite = _Service.GetCharacterSprite(message.Dialog.Messages[currentScript.ObjectState].Character);
                charSprite.CharacterTalk();
                currentScript.ObjectState++;

                //Parler
                string voice = message.Dialog.Messages[currentScript.ObjectState - 1].Voice;
                if (!string.IsNullOrEmpty(voice) && File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Voice) + voice))
                    SoundManager.PlayVoice(PathTools.GetProjectPath(Enums.ProjectPath.Voice) + voice);
            }

            if (currentScript.WaitFrames == 0)
            {
                if (currentScript.ObjectState == message.Dialog.Messages.Count)
                {
                    charSprite = _Service.GetCharacterSprite(message.Dialog.Messages[currentScript.ObjectState - 1].Character);
                    charSprite.CharacterStopTalking();
                    charSprite.CharacterStand();
                    return ViewerEnums.ScriptReturn.Normal;
                }
                currentScript.WaitFrames = message.Dialog.Messages[currentScript.ObjectState].Duration * 10;
                charSprite = _Service.GetCharacterSprite(message.Dialog.Messages[currentScript.ObjectState - 1].Character);
                charSprite.CharacterStopTalking();
                charSprite.CharacterStand();

                currentScript.ObjectState++;

                charSprite = _Service.GetCharacterSprite(message.Dialog.Messages[currentScript.ObjectState - 1].Character);
                charSprite.CharacterTalk();

                //Parler
                string voice = message.Dialog.Messages[currentScript.ObjectState - 1].Voice;
                if (!string.IsNullOrEmpty(voice) && File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Voice) + voice))
                    SoundManager.PlayVoice(PathTools.GetProjectPath(Enums.ProjectPath.Voice) + voice);
            }
            return ViewerEnums.ScriptReturn.Wait;
        }

        /// <summary>
        /// Script Move Player
        /// </summary>
        /// <param name="moveScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptMovePlayer(VO_RunningScript currentScript, VO_Script_MovePlayer moveScript)
        {
            if (currentScript.ObjectState == 0)
            {
                Point location = new Point(moveScript.Coords.Location.X, moveScript.Coords.Location.Y);
                while (location.X % GameCore.Instance.Game.Project.Resolution.MatrixPrecision != 0)
                    location.X--;
                while (location.Y % GameCore.Instance.Game.Project.Resolution.MatrixPrecision != 0)
                    location.Y--;
                if (location.X == PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.X && location.Y == PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.Y)
                    return ViewerEnums.ScriptReturn.Normal;
                PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.CurrentPath = null;
                PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.MoveCharacter(new Point(moveScript.Coords.Location.X, moveScript.Coords.Location.Y));
                currentScript.ObjectState = 1;
            }
            else if (PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.CurrentPath != null)
                currentScript.ObjectState = 2;
            else if (currentScript.ObjectState == 2 && PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.CurrentPath == null && moveScript.Coords.Location.X == PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.X && moveScript.Coords.Location.Y == PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.Y)
                return ViewerEnums.ScriptReturn.Normal;
            else if (currentScript.ObjectState == 2 && PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.CurrentPath == null)
                return ViewerEnums.ScriptReturn.Abort;

            return ViewerEnums.ScriptReturn.Wait;
        }

        /// <summary>
        /// Script ChangePlayerDirection
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changePlayerDirection"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangePlayerDirection(VO_RunningScript currentScript, VO_Script_ChangePlayerDirection changePlayerDirection)
        {
            PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.ChangeDirection(changePlayerDirection.Direction);
            return ViewerEnums.ScriptReturn.Normal;
        }
        #endregion

        #region Stage
        /// <summary>
        /// Script LookForwardPlayer
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptLookForwardPlayer(VO_RunningScript currentScript, VO_Script_LookForwardPlayer lookForwardPlayerScript)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(lookForwardPlayerScript.Character);
            if (characterSprite != null)
            {
                characterSprite.CurrentDirection = (Enums.Movement)(int)Tools.GetAngle(characterSprite.Location, PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location, GameCore.Instance.Game.Project.MovementDirections);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script ChangeAnimationFrequency
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangeCharacterAnimationFrequency(VO_RunningScript currentScript, VO_Script_ChangeCharacterAnimFrequency changeCharacterAnimFrequencyScript)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(changeCharacterAnimFrequencyScript.Character);
            if (characterSprite != null)
            {
                switch (changeCharacterAnimFrequencyScript.AnimationType)
                {
                    case Enums.CharacterAnimationType.Standing:
                        characterSprite.SetAnimationFrequency(characterSprite.StandingAnim, Tools.GetVariableValue(changeCharacterAnimFrequencyScript.Frequency));
                        break;
                    case Enums.CharacterAnimationType.Walking:
                        characterSprite.SetAnimationFrequency(characterSprite.WalkingAnim, Tools.GetVariableValue(changeCharacterAnimFrequencyScript.Frequency));
                        break;
                    case Enums.CharacterAnimationType.Talking:
                        characterSprite.SetAnimationFrequency(characterSprite.TalkingAnim, Tools.GetVariableValue(changeCharacterAnimFrequencyScript.Frequency));
                        break;
                }

            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script FreezeCharacterAnimation
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="freezeCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptFreezeCharacterAnimation(VO_RunningScript currentScript, VO_Script_FreezeCharacterAnimation freezeCharacterAnimationScript)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(freezeCharacterAnimationScript.Character);
            if (characterSprite != null)
            {
                if (freezeCharacterAnimationScript.FreezeAll)
                {
                    characterSprite.FreezeAnimation(characterSprite.StandingAnim);
                    characterSprite.FreezeAnimation(characterSprite.WalkingAnim);
                    characterSprite.FreezeAnimation(characterSprite.TalkingAnim);
                }
                else
                {
                    switch (freezeCharacterAnimationScript.AnimationType)
                    {
                        case Enums.CharacterAnimationType.Standing:
                            characterSprite.FreezeAnimation(characterSprite.StandingAnim);
                            break;
                        case Enums.CharacterAnimationType.Walking:
                            characterSprite.FreezeAnimation(characterSprite.WalkingAnim);
                            break;
                        case Enums.CharacterAnimationType.Talking:
                            characterSprite.FreezeAnimation(characterSprite.TalkingAnim);
                            break;
                    }
                }
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script FreeCharacterAnimation
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="freezeCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptFreeCharacterAnimation(VO_RunningScript currentScript, VO_Script_FreeCharacterAnimation FreeCharacterAnimationScript)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(FreeCharacterAnimationScript.Character);
            if (characterSprite != null)
            {
                if (FreeCharacterAnimationScript.FreeAll)
                {
                    characterSprite.FreeAnimation(characterSprite.StandingAnim);
                    characterSprite.FreeAnimation(characterSprite.WalkingAnim);
                    characterSprite.FreeAnimation(characterSprite.TalkingAnim);
                }
                else
                {
                    switch (FreeCharacterAnimationScript.AnimationType)
                    {
                        case Enums.CharacterAnimationType.Standing:
                            characterSprite.FreeAnimation(characterSprite.StandingAnim);
                            break;
                        case Enums.CharacterAnimationType.Walking:
                            characterSprite.FreeAnimation(characterSprite.WalkingAnim);
                            break;
                        case Enums.CharacterAnimationType.Talking:
                            characterSprite.FreeAnimation(characterSprite.TalkingAnim);
                            break;
                    }
                }
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script MoveCharacter
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="moveCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptMoveCharacter(VO_RunningScript currentScript, VO_Script_MoveCharacter moveCharacterScript)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(moveCharacterScript.Character);
            if (characterSprite != null)
            {

                if (currentScript.ObjectState == 0)
                {
                    Point location = new Point(moveCharacterScript.Coords.Location.X, moveCharacterScript.Coords.Location.Y);
                    while (location.X % GameCore.Instance.Game.Project.Resolution.MatrixPrecision != 0)
                        location.X--;
                    while (location.Y % GameCore.Instance.Game.Project.Resolution.MatrixPrecision != 0)
                        location.Y--;
                    if (location.X == characterSprite.Location.X && location.Y == characterSprite.Location.Y)
                        return ViewerEnums.ScriptReturn.Normal;
                    characterSprite.CurrentPath = null;
                    characterSprite.MoveCharacter(new Point(moveCharacterScript.Coords.Location.X, moveCharacterScript.Coords.Location.Y));
                    currentScript.ObjectState = 1;
                }
                else if (characterSprite.CurrentPath != null)
                    currentScript.ObjectState = 2;
                else if (currentScript.ObjectState == 2 && characterSprite.CurrentPath == null && moveCharacterScript.Coords.Location.X == characterSprite.Location.X && moveCharacterScript.Coords.Location.Y == characterSprite.Location.Y)
                    return ViewerEnums.ScriptReturn.Normal;
                else if (currentScript.ObjectState == 2 && characterSprite.CurrentPath == null)
                    return ViewerEnums.ScriptReturn.Abort;
            }

            return ViewerEnums.ScriptReturn.Wait;
        }

        /// <summary>
        /// Script ChangePlayerDirection
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changePlayerDirection"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangeCharacterDirection(VO_RunningScript currentScript, VO_Script_ChangeCharacterDirection changeCharacterDirection)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(changeCharacterDirection.CharacterId);
            if (characterSprite != null)
                characterSprite.ChangeDirection(changeCharacterDirection.Direction);
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script StopCurrentPlayerMovement
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="addItemScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptStopCharacterMovement(VO_RunningScript currentScript, VO_Script_StopCharacterMovements stopCharacterMovementsScript)
        {
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(stopCharacterMovementsScript.Character);
            if (characterSprite != null)
                characterSprite.StopPath();
            return ViewerEnums.ScriptReturn.Normal;
        }
        #endregion

        #region Internal Scripts
        /// <summary>
        /// Script Show CallScript
        /// </summary>
        /// <param name="callScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptCallScript(VO_Script_CallScript callScript)
        {
            CurrentScript = new VO_RunningScript();
            CurrentScript.Lines = callScript.Script.Lines;
            CurrentScript.Id = callScript.Script.Id;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script RollBackWhile
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="rollBackWhileScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptRollBackWhile(VO_RunningScript currentScript, VO_Script_RollBackWhile rollBackWhileScript)
        {
            currentScript.CurrentLine = rollBackWhileScript.WhileLine;
            return ViewerEnums.ScriptReturn.RollBackWhile;
        }
        #endregion

        #region Programmation
        /// <summary>
        /// Script ChangeAnimationFrequency
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptCallGlobalEvent(VO_RunningScript currentScript, VO_Script_CallGlobalEvent callGlobalEventScript)
        {
            VO_GlobalEvent global = GameCore.Instance.GetGlobalEventById(callGlobalEventScript.GlobalEvent);
            if (global != null)
            {
                if (global.UseTrigger)
                {
                    VO_Trigger trigger = GameState.State.Triggers.Find(p => p.Id == global.Trigger);
                    if (trigger == null || !trigger.Value)
                        return ViewerEnums.ScriptReturn.Normal;
                }
                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.Id = global.Script.Id;
                runningScript.Lines = global.Script.Lines;
                AddParallelScript(runningScript);
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        #region Break
        /// <summary>
        /// Script Break
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="breakScript"></param>
        /// <returns></returns>
        public static ViewerEnums.ScriptReturn ScriptBreak(VO_RunningScript currentScript, VO_Script_Break breakScript)
        {
            CurrentScript.CurrentLine = breakScript.Break;
            return ViewerEnums.ScriptReturn.Break;
        }

        /// <summary>
        /// Find Break
        /// </summary>
        private static void AssignBreakScriptInLoop(List<VO_Line> lines, VO_Line currentLine)
        {
            foreach (VO_Line line in lines)
            {
                if (line is VO_Script_Break)
                {
                    ((VO_Script_Break)line).Break = currentLine;
                }
                else if (line is VO_Script_Condition)
                {
                    AssignBreakScriptInLoop(((VO_Script_Condition)line).IfSubLines, currentLine);
                    AssignBreakScriptInLoop(((VO_Script_Condition)line).ElseSubLines, currentLine);
                }
                else if (line is VO_Script_Loop)
                {
                    AssignBreakScriptInLoop(((VO_Script_Loop)line).WhileSubLines, currentLine);
                }
                else if (line is VO_Script_ChoiceMessage)
                {
                    VO_Script_ChoiceMessage choiceScript = (VO_Script_ChoiceMessage)line;
                    foreach (VO_LineChoices choice in choiceScript.Choices)
                    {
                        AssignBreakScriptInLoop(choice.SubLines, currentLine);
                    }
                }
            }
        }
        #endregion

        #region Anchor
        /// <summary>
        /// Script GoToAnchor
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="goToScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptGoToAnchor(VO_RunningScript currentScript, VO_Script_GoToAnchor goToScript)
        {
            VO_Line line = FindAnchor(CurrentScript.Lines, goToScript.Anchor);
            if (line != null)
                CurrentScript.CurrentLine = line;
            return ViewerEnums.ScriptReturn.RollBackWhile;
        }

        /// <summary>
        /// Trouve la ligne dont l'ancre est...
        /// </summary>
        /// <param name="currentLine"></param>
        private static VO_Line FindAnchor(List<VO_Line> lines, string key)
        {
            foreach (VO_Line line in lines)
            {
                if (line is VO_Script_SetAnchor && ((VO_Script_SetAnchor)line).Anchor == key)
                    return line;
                else if (line is VO_Script_Condition)
                {
                    VO_Line condition = null;
                    condition = FindAnchor(((VO_Script_Condition)line).IfSubLines, key);
                    if (condition == null)
                    {
                        condition = FindAnchor(((VO_Script_Condition)line).ElseSubLines, key);
                    }
                    if (condition != null)
                        return condition;
                }
                else if (line is VO_Script_Loop)
                {
                    VO_Line loop = FindAnchor(((VO_Script_Loop)line).WhileSubLines, key);
                    if (loop != null)
                        return loop;
                }
                else if (line is VO_Script_ChoiceMessage)
                {
                    VO_Line choiceLine = null;
                    VO_Script_ChoiceMessage choiceScript = (VO_Script_ChoiceMessage)line;
                    foreach (VO_LineChoices choice in choiceScript.Choices)
                    {
                        choiceLine = FindAnchor(choice.SubLines, key);
                        if (choiceLine != null)
                            return choiceLine;
                    }
                }
            }
            return null;
        }
        #endregion

        /// <summary>
        /// Script Random
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="loopScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptRandom(VO_RunningScript currentScript, VO_Script_Random randomScript)
        {
            VO_Variable variable = GameState.State.Variables.Find(p => p.Id == randomScript.Variable);

            if (variable != null)
            {
                Random random = new Random();
                variable.Value = random.Next(Tools.GetVariableValue(randomScript.MinValue), Tools.GetVariableValue(randomScript.MaxValue));
            }

            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script Loop
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="loopScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptLoop(VO_RunningScript currentScript, VO_Script_Loop loopScript)
        {
            if (loopScript.UseButton)
            {
                VO_Trigger trigger = GameState.State.Triggers.Find(p => p.Id == loopScript.Button);
                if (trigger == null || trigger.Value != loopScript.ButtonValue)
                    return ViewerEnums.ScriptReturn.Normal;
            }

            if (loopScript.UseVariable)
            {
                VO_Variable variable = GameState.State.Variables.Find(p => p.Id == loopScript.Variable);
                if (variable != null)
                {
                    switch (loopScript.Operator)
                    {
                        case Enums.ComparativeOperator.Different:
                            if (variable.Value == Tools.GetVariableValue(loopScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.Equal:
                            if (variable.Value != Tools.GetVariableValue(loopScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.Less:
                            if (variable.Value >= Tools.GetVariableValue(loopScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.LessOrEqual:
                            if (variable.Value > Tools.GetVariableValue(loopScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.More:
                            if (variable.Value <= Tools.GetVariableValue(loopScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.MoreOrEqual:
                            if (variable.Value < Tools.GetVariableValue(loopScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                    }
                }
                else
                    return ViewerEnums.ScriptReturn.Normal;
            }

            if (loopScript.WhileSubLines.Count > 0)
                AssignBreakScriptInLoop(loopScript.WhileSubLines, loopScript);
            VO_Script_RollBackWhile rollBackWhile = new VO_Script_RollBackWhile();
            rollBackWhile.Id = Guid.NewGuid();
            rollBackWhile.WhileLine = (VO_Line)loopScript;
            loopScript.WhileSubLines.Add(rollBackWhile);
            return ViewerEnums.ScriptReturn.While;
        }

        /// <summary>
        /// Script ChangeVariable
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeVariableScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangeVariable(VO_RunningScript currentScript, VO_Script_ChangeVariable changeVariableScript)
        {
            VO_Variable variable = GameState.State.Variables.Find(p => p.Id == changeVariableScript.Variable);
            if (variable != null)
            {
                switch (changeVariableScript.Operator)
                {
                    case Enums.ChangeOperator.Add:
                        variable.Value += Tools.GetVariableValue(changeVariableScript.Value);
                        break;
                    case Enums.ChangeOperator.Divide:
                        variable.Value /= Tools.GetVariableValue(changeVariableScript.Value);
                        break;
                    case Enums.ChangeOperator.Multiply:
                        variable.Value *= Tools.GetVariableValue(changeVariableScript.Value);
                        break;
                    case Enums.ChangeOperator.Set:
                        variable.Value = Tools.GetVariableValue(changeVariableScript.Value);
                        break;
                    case Enums.ChangeOperator.Sub:
                        variable.Value -= Tools.GetVariableValue(changeVariableScript.Value);
                        break;
                }
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script Press Switch
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="pressSwitchScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptPressSwitch(VO_RunningScript currentScript, VO_Script_PressSwitch pressSwitchScript)
        {
            VO_Trigger trigger = GameState.State.Triggers.Find(p => p.Id == pressSwitchScript.Button);
            if (trigger != null)
                trigger.Value = pressSwitchScript.IsActive;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script Wait
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="waitScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptWait(VO_RunningScript currentScript, VO_Script_Wait waitScript)
        {
            if (currentScript.ObjectState == 0)
            {
                currentScript.ObjectState = 1;
                currentScript.WaitFrames = Tools.GetVariableValue(waitScript.SecondsToWait) * ViewerConstants.FPS;
            }
            else if (currentScript.ObjectState == 1 && currentScript.WaitFrames == 0)
            {
                return ViewerEnums.ScriptReturn.Normal;
            }
            return ViewerEnums.ScriptReturn.Wait;
        }

        /// <summary>
        /// Script Condition
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptCondition(VO_RunningScript currentScript, VO_Script_Condition conditionScript)
        {
            if (conditionScript.UseButton)
            {
                VO_Trigger trigger = GameState.State.Triggers.Find(p => p.Id == conditionScript.Button);
                if (trigger == null || trigger.Value != conditionScript.ButtonValue)
                    return ViewerEnums.ScriptReturn.Else;
            }

            if (conditionScript.UseVariable)
            {
                VO_Variable variable = GameState.State.Variables.Find(p => p.Id == conditionScript.Variable);
                if (variable != null)
                {
                    switch (conditionScript.Operator)
                    {
                        case Enums.ComparativeOperator.Different:
                            if (variable.Value == Tools.GetVariableValue(conditionScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.Equal:
                            if (variable.Value != Tools.GetVariableValue(conditionScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.Less:
                            if (variable.Value >= Tools.GetVariableValue(conditionScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.LessOrEqual:
                            if (variable.Value > Tools.GetVariableValue(conditionScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.More:
                            if (variable.Value <= Tools.GetVariableValue(conditionScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                        case Enums.ComparativeOperator.MoreOrEqual:
                            if (variable.Value < Tools.GetVariableValue(conditionScript.VariableValue))
                                return ViewerEnums.ScriptReturn.Normal;
                            break;
                    }
                }
                else
                    return ViewerEnums.ScriptReturn.Normal;
            }

            if (conditionScript.UsePlayer)
            {
                if (PlayableCharactersManager.CurrentPlayerCharacter.Id != conditionScript.Player)
                    return ViewerEnums.ScriptReturn.Else;
            }
            return ViewerEnums.ScriptReturn.If;
        }
        #endregion

        #region Interface
        /// <summary>
        /// Script DisableStageInteractions
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptDisableSaves()
        {
            _Interface.SaveActive = false;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script DisableStageInteractions
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptEnableSaves()
        {
            _Interface.SaveActive = true;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script DisableStageInteractions
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptDisableStageInteractions()
        {
            ScriptStagesInteractions = false;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script EnableStageInteractions
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptEnableStageInteractions()
        {
            ScriptStagesInteractions = true;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script DisableStageInteractions
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptDisableUserControls()
        {
            ScriptUserControls = false;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script EnableStageInteractions
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptEnableUserControls()
        {
            ScriptUserControls = true;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script HideLifeBar
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptHideLifeBar()
        {
            _Interface.LifeBarVisible = false;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script ShowHideBar
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptShowLifeBar()
        {
            _Interface.LifeBarVisible = true;
            return ViewerEnums.ScriptReturn.Normal;
        }
        #endregion

        #region Menu
        /// <summary>
        /// Script OpenInventory
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptOpenInventory()
        {
            _Interface.Inventory.Opened = true;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script CloseInventory
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptCloseInventory()
        {
            _Interface.Inventory.Opened = false;
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script GameOver
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptGameOver()
        {
            GameOver = true;
            return ViewerEnums.ScriptReturn.Abort;
        }

        /// <summary>
        /// Script GoToTitle
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptGoToTitle()
        {
            GoToScreen = new EventArgsClasses.GameScreenEventArgs(ViewerEnums.ScreenType.Title);
            return ViewerEnums.ScriptReturn.Abort;
        }
        #endregion

        #region Medias
        /// <summary>
        /// Script ChangeMusicFrequency
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeMusicFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptChangeMusicFrequency(VO_RunningScript currentScript, VO_Script_ChangeMusicFrequency changeMusicFrequencyScript)
        {
            SoundManager.SetFrequency(changeMusicFrequencyScript.Frequency);
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script PlayMusic
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCurrentCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptPlayMusic(VO_RunningScript currentScript, VO_Script_PlayMusic playMusicScript)
        {
            if (!string.IsNullOrEmpty(playMusicScript.Music) && File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Musics) + playMusicScript.Music))
                SoundManager.PlayMusic(PathTools.GetProjectPath(Enums.ProjectPath.Musics) + playMusicScript.Music);
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script PlaySound
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCurrentCharacterScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptPlaySound(VO_RunningScript currentScript, VO_Script_PlaySound playSoundScript)
        {
            if (!string.IsNullOrEmpty(playSoundScript.Sound) && File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + playSoundScript.Sound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + playSoundScript.Sound);
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script StopMusic
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptStopMusic()
        {
            SoundManager.StopMusic();
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script StopMusic
        /// </summary>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptStopSound()
        {
            SoundManager.StopSound();
            return ViewerEnums.ScriptReturn.Normal;
        }
        #endregion

        #region Effects
        /// <summary>
        /// Script FocusOnCharacter
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptFocusOnCharacter(VO_RunningScript currentScript, VO_Script_FocusOnCharacter focusCharacterScript)
        {
            //Perso à focuser
            VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(focusCharacterScript.Character);
            if (characterSprite != null)
            {
                VO_Script_MoveCamera cameraMove = new VO_Script_MoveCamera();
                cameraMove.Coords = new VO_Coords(new System.Drawing.Point(characterSprite.Location.X, characterSprite.Location.Y), Guid.Empty);
                cameraMove.Speed = focusCharacterScript.Speed;
                cameraMove.UseImmediately = focusCharacterScript.UseImmediately;
                if (ScriptMoveCamera(currentScript, cameraMove) == ViewerEnums.ScriptReturn.Normal)
                {
                    CamCharacter = focusCharacterScript.Character;
                }
                else
                    return ViewerEnums.ScriptReturn.Wait;
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script FocusOnAnimation
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptFocusOnAnimation(VO_RunningScript currentScript, VO_Script_FocusOnAnimation focusOnAnimationScript)
        {
            //Perso à focuser
            VO_AnimatedSprite animationSprite = _Service.GetAnimatedSprite(focusOnAnimationScript.Animation, Enums.StageObjectType.Animations);
            if (animationSprite != null)
            {
                VO_Script_MoveCamera cameraMove = new VO_Script_MoveCamera();
                cameraMove.Coords = new VO_Coords(new System.Drawing.Point(animationSprite.Location.X, animationSprite.Location.Y), Guid.Empty);
                cameraMove.Speed = focusOnAnimationScript.Speed;
                cameraMove.UseImmediately = focusOnAnimationScript.UseImmediately;
                if (ScriptMoveCamera(currentScript, cameraMove) == ViewerEnums.ScriptReturn.Normal)
                {
                    CamAnimation = focusOnAnimationScript.Animation;
                }
                else
                    return ViewerEnums.ScriptReturn.Wait;
            }
            return ViewerEnums.ScriptReturn.Normal;
        }

        /// <summary>
        /// Script MoveCamera
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        private static ViewerEnums.ScriptReturn ScriptMoveCamera(VO_RunningScript currentScript, VO_Script_MoveCamera moveCameraScript)
        {
            if (currentScript.ObjectState == 0)
            {
                ScriptDefaultCamera();
                currentScript.ObjectState = 1;
            }
            Point camLocation = new Point((int)Camera2D.Pos.X, (int)Camera2D.Pos.Y);
            if (moveCameraScript.UseImmediately)
            {
                Camera2D.Pos = _Service.GetCameraCoords(new Point(moveCameraScript.Coords.Location.X, moveCameraScript.Coords.Location.Y));
                CamLocations = new Point(moveCameraScript.Coords.Location.X, moveCameraScript.Coords.Location.Y);
                LocationsInUse = true;
                return ViewerEnums.ScriptReturn.Normal;
            }
            List<Point> points = null;
            if (LocationsInUse)
                points = Tools.RenderLine(CamLocations, new Point(moveCameraScript.Coords.Location.X, moveCameraScript.Coords.Location.Y));
            else
                points = Tools.RenderLine(camLocation, new Point(moveCameraScript.Coords.Location.X, moveCameraScript.Coords.Location.Y));
            int speed = moveCameraScript.Speed;
            if (points.Count <= moveCameraScript.Speed)
                return ViewerEnums.ScriptReturn.Normal;
            Camera2D.Pos = _Service.GetCameraCoords(points[speed]);
            CamLocations = points[speed];
            LocationsInUse = true;
            return ViewerEnums.ScriptReturn.Wait;
        }

        /// <summary>
        /// Script DefaultCamera
        /// </summary>
        /// <param name="currentScript"></param>
        /// <param name="changeCharacterAnimFrequencyScript"></param>
        /// <returns></returns>
        public static ViewerEnums.ScriptReturn ScriptDefaultCamera()
        {
            CamAnimation = Guid.Empty;
            CamCharacter = Guid.Empty;
            CamLocations = new Point();
            LocationsInUse = false;
            return ViewerEnums.ScriptReturn.Normal;
        }
        #endregion
        #endregion
        #endregion
    }
}
