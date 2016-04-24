using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.DatasLayer;
using System.IO;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.PresentationLayer;
using ReplicaStudio.Viewer.ServiceLayer;

namespace ReplicaStudio.Viewer.DataLayer
{
    /// <summary>
    /// Instance de jeu
    /// </summary>
    public static class GameState
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        private static StageService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Instance singleton
        /// </summary>
        public static VO_GameState State { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initialisation du state
        /// </summary>
        /// <param name="service"></param>
        public static void InitiliazeState(StageService service)
        {
            _Service = service;
        }

        /// <summary>
        /// Nouveau jeu
        /// </summary>
        public static void NewGame()
        {
            State = new VO_GameState();

            //Triggers
            State.Triggers = new List<VO_Trigger>();
            foreach (VO_Trigger trigger in GameCore.Instance.Game.Triggers)
                State.Triggers.Add(trigger);

            //Variables
            State.Variables = new List<VO_Variable>();
            foreach (VO_Variable variable in GameCore.Instance.Game.Variables)
                State.Variables.Add(variable);

            //Character
            PlayableCharactersManager.ResetCharacters();
            State.Players = new List<VO_GameStateCharacter>();
            State.CurrentCharacter = GameCore.Instance.Game.Project.StartingCharacter;
            PlayableCharactersManager.CurrentPlayerCharacter = PlayableCharactersManager.GetPlayer(State.CurrentCharacter);
            PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage = PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Map;

            //Stages
            State.Stages = new List<VO_GameStateStage>();

            //Running Scripts
            State.RunningScripts = new List<VO_GameStateRunningScript>();
        }

        /// <summary>
        /// Charge une sauvegarde
        /// </summary>
        /// <param name="path"></param>
        public static bool LoadGameState(string path)
        {
            if (!File.Exists(GameCore.Instance.Game.Project.RootPath + "\\" + path))
                return false;

            #region Récupération du GameState
            //Récupération
            VO_GameState tempGameState = (VO_GameState)AppTools.LoadObjectFromFile(GameCore.Instance.Game.Project.RootPath + "\\" + path);

            //GameCore.Instance.Game.Project.RootPath = Path.GetDirectoryName(path) + "\\";
            GameCore.Instance.Game.Project.ProjectFileName = Path.GetFileNameWithoutExtension(path);
            #endregion

            //Nouveau du jeu
            NewGame();

            //Reset des managers
            ScriptManager.ResetScriptManager();
            PlayableCharactersManager.ResetCharacters();

            //Chargement
            //Boutons
            foreach (VO_Trigger trigger in State.Triggers)
            {
                VO_Trigger updateTrigger = tempGameState.Triggers.Find(p => p.Id == trigger.Id);
                trigger.Value = updateTrigger.Value;
            }

            //Variables
            foreach (VO_Variable variable in State.Variables)
            {
                VO_Variable updateVariable = tempGameState.Variables.Find(p => p.Id == variable.Id);
                variable.Value = updateVariable.Value;
            }

            //Perso courant
            State.CurrentCharacter = tempGameState.CurrentCharacter;

            //Persos
            foreach (VO_GameStateCharacter characterGame in tempGameState.Players)
            {
                PlayableCharactersManager.CreatePlayer(characterGame.CharacterId);
                VO_Player charSprite = PlayableCharactersManager.GetPlayer(characterGame.Id);
                charSprite.Actions.Clear();
                foreach (Guid action in characterGame.Actions)
                {
                    charSprite.Actions.Add(action);
                }
                foreach (Guid item in characterGame.Items)
                {
                    charSprite.AddItem(item);
                }
                charSprite.CurrentStage = characterGame.Coords.Map;
                charSprite.CharacterSprite.CurrentDirection = characterGame.CurrentDirection;
                charSprite.CharacterSprite.Id = characterGame.Id;
                charSprite.CharacterSprite.CharacterId = characterGame.CharacterId;
                charSprite.CharacterSprite.CurrentExecutingPage = characterGame.CurrentExecutingPage;
                charSprite.CharacterSprite.IsTalking = characterGame.IsTalking;
                charSprite.CharacterSprite.CurrentPath = characterGame.CurrentPath;
                charSprite.CharacterSprite.SetCurrentAnimation(charSprite.CharacterSprite.CurrentCharacterAnimationType, characterGame.CurrentAnim);
            }
            PlayableCharactersManager.CurrentPlayerCharacter = PlayableCharactersManager.GetPlayer(State.CurrentCharacter);
            State.Players = tempGameState.Players;
            State.Stages = tempGameState.Stages;
            State.CurrentStagePNJ = tempGameState.CurrentStagePNJ;

            //RunningScript
            foreach (VO_GameStateRunningScript runningScriptState in tempGameState.RunningScripts)
            {
                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.Id = runningScriptState.Script;
                runningScript.ObjectState = runningScriptState.ObjectState;
                runningScript.WaitFrames = runningScriptState.WaitFrames;
                runningScript.Lines = FindScript(runningScriptState.Script).Lines;
                runningScript.CurrentLine = FindLine(runningScript.Lines, runningScriptState.CurrentLine);
                if (runningScript.Lines != null)
                    ScriptManager.AddParallelScript(runningScript);
            }
            return true;

        }

        /// <summary>
        /// Charge les PNJ
        /// </summary>
        /// <returns></returns>
        public static void LoadGamePNJ()
        {
            foreach (VO_GameStateCharacter character in State.CurrentStagePNJ)
            {
                VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(character.Id);
                characterSprite.SetPosition(character.Coords.Location.X, character.Coords.Location.Y);
                characterSprite.CurrentDirection = character.CurrentDirection;
                characterSprite.Id = character.Id;
                characterSprite.CharacterId = character.CharacterId;
                characterSprite.CurrentExecutingPage = character.CurrentExecutingPage;
                characterSprite.IsTalking = character.IsTalking;
                characterSprite.CurrentPath = character.CurrentPath;
                characterSprite.SetCurrentAnimation(characterSprite.CurrentCharacterAnimationType, character.CurrentAnim);
            }
        }

        /// <summary>
        /// Sauvegarde une partie
        /// </summary>
        /// <param name="path"></param>
        public static void SaveGameState(string path)
        {
            //Enregistrement des personnages
            List<VO_Player> players = PlayableCharactersManager.GetPlayers();
            State.Players = new List<VO_GameStateCharacter>();
            foreach (VO_Player player in players)
            {
                VO_GameStateCharacter gameCharacter = new VO_GameStateCharacter();
                gameCharacter.Id = player.Id;
                gameCharacter.CharacterId = player.CharacterSprite.CharacterId;
                gameCharacter.Coords = new VO_Coords(new System.Drawing.Point(player.CharacterSprite.Location.X, player.CharacterSprite.Location.Y), player.CurrentStage);
                gameCharacter.CurrentAnim = player.CharacterSprite.CurrentAnimationType;
                gameCharacter.CurrentDirection = player.CharacterSprite.CurrentDirection;
                gameCharacter.Actions = new List<Guid>();
                gameCharacter.Items = new List<Guid>();
                gameCharacter.CurrentExecutingPage = player.CharacterSprite.CurrentExecutingPage;
                gameCharacter.IsTalking = player.CharacterSprite.IsTalking;
                gameCharacter.CurrentPath = player.CharacterSprite.CurrentPath;
                foreach (Guid action in player.Actions)
                    gameCharacter.Actions.Add(action);
                for (int i = 0; i < GameCore.Instance.Game.Menu.GridHeight; i++)
                {
                    for (int j = 0; j < GameCore.Instance.Game.Menu.GridWidth; j++)
                    {
                        if (player.Items[j, i] != Guid.Empty)
                        {
                            gameCharacter.Items.Add(player.Items[j, i]);
                        }
                    }
                }
                GameState.State.Players.Add(gameCharacter);
            }

            //Enregistrement de stages
            VO_Stage stage = GameCore.Instance.GetStageById(PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage);
            State.CurrentStagePNJ = new List<VO_GameStateCharacter>();
            foreach (VO_StageCharacter character in stage.ListCharacters)
            {
                VO_CharacterSprite characterSprite = _Service.GetCharacterSprite(character.Id);
                VO_GameStateCharacter gameCharacter = new VO_GameStateCharacter();
                gameCharacter.Id = character.Id;
                gameCharacter.CharacterId = character.CharacterId;
                gameCharacter.Coords = new VO_Coords(new System.Drawing.Point(characterSprite.Location.X, character.Location.Y), Guid.Empty);
                gameCharacter.CurrentAnim = characterSprite.CurrentAnimationType;
                gameCharacter.CurrentDirection = characterSprite.CurrentDirection;
                gameCharacter.Actions = new List<Guid>();
                gameCharacter.Items = new List<Guid>();
                gameCharacter.CurrentExecutingPage = characterSprite.CurrentExecutingPage;
                gameCharacter.IsTalking = characterSprite.IsTalking;
                gameCharacter.CurrentPath = characterSprite.CurrentPath;
                GameState.State.CurrentStagePNJ.Add(gameCharacter);
            }

            //Enregistrement des Script courants
            if (ScriptManager.ParallelScripts != null)
            {
                foreach (VO_RunningScript runningScript in ScriptManager.ParallelScripts)
                {
                    VO_GameStateRunningScript runningScriptState = new VO_GameStateRunningScript();
                    runningScriptState.CurrentLine = runningScript.Id;
                    runningScriptState.ObjectState = runningScript.ObjectState;
                    runningScriptState.Script = runningScript.Id;
                    runningScriptState.WaitFrames = runningScript.WaitFrames;
                    State.RunningScripts.Add(runningScriptState);
                }
            }

            AppTools.SaveObjectToFile(GameState.State, GameCore.Instance.Game.Project.RootPath + "\\" + path);
        }

        /// <summary>
        /// Trouver un script
        /// </summary>
        /// <param name="scriptId"></param>
        /// <returns></returns>
        private static VO_Script FindScript(Guid scriptId)
        {
            //GlobalEvents
            foreach (VO_GlobalEvent globalEvent in GameCore.Instance.Game.GlobalEvents)
            {
                if (globalEvent.Script.Id == scriptId)
                    return globalEvent.Script;
            }

            foreach (VO_Stage stage in GameCore.Instance.Game.Stages)
            {
                //Events
                foreach (VO_StageObject stageObject in stage.ListHotSpots)
                {
                    foreach (VO_Page page in stageObject.Event.PageList)
                    {
                        if (page.Script.Id == scriptId)
                            return page.Script;
                    }
                }

                //Animations
                foreach (VO_Layer layer in stage.ListLayers)
                {
                    foreach (VO_StageObject stageObject in layer.ListAnimations)
                    {
                        foreach (VO_Page page in stageObject.Event.PageList)
                        {
                            if (page.Script.Id == scriptId)
                                return page.Script;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Trouve la prochaine ligne du même niveau
        /// </summary>
        /// <param name="currentLine"></param>
        private static VO_Line FindLine(List<VO_Line> lines, Guid currentLineId)
        {
            foreach (VO_Line line in lines)
            {
                if (line.Id == currentLineId)
                    return line;
                else if (line is VO_Script_Condition)
                {
                    VO_Line condition = null;
                    condition = FindLine(((VO_Script_Condition)line).IfSubLines, currentLineId);
                    if (condition == null)
                    {
                        condition = FindLine(((VO_Script_Condition)line).ElseSubLines, currentLineId);
                    }
                    if (condition != null)
                        return condition;
                }
                else if (line is VO_Script_Loop)
                {
                    VO_Line loop = FindLine(((VO_Script_Loop)line).WhileSubLines, currentLineId);
                    if (loop != null)
                        return loop;
                }
                else if (line is VO_Script_ChoiceMessage)
                {
                    VO_Line choiceLine = null;
                    VO_Script_ChoiceMessage choiceScript = (VO_Script_ChoiceMessage)line;
                    foreach (VO_LineChoices choice in choiceScript.Choices)
                    {
                        choiceLine = FindLine(choice.SubLines, currentLineId);
                        if (choiceLine != null)
                            return choiceLine;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
