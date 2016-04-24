using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.ServiceLayer;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.DataLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using Microsoft.Xna.Framework.Input;

namespace ReplicaStudio.Viewer.PresentationLayer
{
    public class StageScreen : Screen
    {
        #region Members
        #region Permanent
        /// <summary>
        /// Référence au service
        /// </summary>
        private StageService _Service;

        /// <summary>
        /// Référence au Game
        /// </summary>
        private Viewer _Game;
        #endregion

        #region Par Stage
        /// <summary>
        /// Stage référence
        /// </summary>
        private VO_Stage _Stage;
        #endregion

        #region Interface
        /// <summary>
        /// Référence à l'interface
        /// </summary>
        private Interface _Interface;
        #endregion

        #region Scripts
        /// <summary>
        /// Script de fin lancés?
        /// </summary>
        private bool _LaunchedEndingScripts = false;
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Guid du stage en cours
        /// </summary>
        public Guid CurrentStageGuid { get; set; }

        /// <summary>
        /// Position de départ
        /// </summary>
        public Point StartingPosition { get; set; }

        /// <summary>
        /// Ignorer scripts de départ
        /// </summary>
        public bool IgnoreStartingScripts { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public StageScreen(Viewer game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            _Game = game;
            _Service = new StageService();

            //Interface
            if (_Interface == null)
            {
                _Interface = new Interface(_SpriteBatch, this.Game);
                _Interface.ChangingScreen += new ChangeScreenEventHandler(_Interface_ChangingScreen);
            }

            //Camera
            Camera2D.InitializeCamera(game);
        }
        #endregion

        #region Inherited Methods
        public override void DrawScene(GameTime gameTime)
        {
            #region Debug
            DebugConsole.Update();
            #endregion

            #region Scripts
            if (!_Interface.IsInterfaceBlocking())
            {
                _Service.CheckParallelProcessAndAutomaticAndContactScripts(_ProjectData.Resolution.MatrixPrecision);
                ScriptManager.RunParallelScripts();
                ScriptManager.RunScript(ScriptManager.CurrentScript, false);
            }

            if (ScriptManager.GoToScreen != null && !_LaunchedEndingScripts)
            {
                LaunchEndingScript();
            }

            if (ScriptManager.GameOver)
            {
                VO_RunningScript runningScript = new VO_RunningScript();
                runningScript.Id = _ProjectData.GameOver.Id;
                runningScript.Lines = _ProjectData.GameOver.Lines;
                ScriptManager.CurrentScript = runningScript;
                ScriptManager.GameOver = false;
            }
            #endregion

            _SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Camera2D.GetTransformation(this.GraphicsDevice));
            #region Décors et objets derrière perso
            int i = 1;
            _Stage.ListCharacters.OrderBy(p => p.Location.Y);
            int playerLayerIndex = _Service.GetLayerIndexFromCharacterLocation(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location);
            foreach (VO_Layer layer in _Stage.ListLayers)
            {
                RefreshLayer(layer, layer.MainLayer, !_Interface.IsInterfaceBlocking());
                RefreshCharacters(i, layer.MainLayer, playerLayerIndex, !_Interface.IsInterfaceBlocking());
                i++;
            }
            #endregion
            _SpriteBatch.End();

            _SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            #region Interface
            if(ScriptManager.ScriptUserControls)
                _Interface.Draw(gameTime, ScriptManager.BlockType);
            #endregion
            #region Messages
            if (!_Interface.IsInterfaceBlocking())
            {
                RefreshMessage(ScriptManager.CurrentScript);
                RefreshChoiceMessage(ScriptManager.CurrentScript, gameTime);
                if (ScriptManager.ParallelScripts != null)
                    foreach (VO_RunningScript script in ScriptManager.ParallelScripts)
                    {
                        RefreshMessage(script);
                    }
            }
            #endregion
            #region Debug
            DebugConsole.Draw();
            #endregion
            _SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            #region Gestion Camera
            if (ScriptManager.CamCharacter != Guid.Empty)
            {
                Camera2D.Pos = _Service.GetCameraCoords(_Service.GetCharacterSprite(ScriptManager.CamCharacter).Location);
            }
            else if (ScriptManager.CamAnimation != Guid.Empty)
            {
                Camera2D.Pos = _Service.GetCameraCoords(_Service.GetAnimatedSprite(ScriptManager.CamAnimation, Enums.StageObjectType.Animations).Location);
            }
            else if (!ScriptManager.LocationsInUse)
                Camera2D.Pos = _Service.GetCameraCoords(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location);
            #endregion
        }
        #endregion

        #region Methods
        #region Refresh
        /// <summary>
        /// Rafraichi les messages
        /// </summary>
        private void RefreshMessage(VO_RunningScript script)
        {
            if (script != null && script.CurrentLine != null && script.CurrentLine is VO_Script_Message)
            {
                VO_Dialog dialog = ((VO_Script_Message)script.CurrentLine).Dialog;
                VO_Message message = dialog.Messages[script.ObjectState - 1];
                Draw(_Service.FormatText(message, new VO_Size(_ProjectData.Resolution.Width, _ProjectData.Resolution.Height), new Point((int)Camera2D.Pos.X, (int)Camera2D.Pos.Y)));

                //Dessiner les faces
                if (dialog.UseFaces)
                    Draw(_Service.GetAnimatedFaces(dialog));
            }
        }

        /// <summary>
        /// Rafraichi les choices
        /// </summary>
        /// <param name="script"></param>
        private void RefreshChoiceMessage(VO_RunningScript script, GameTime gametime)
        {
            if (script != null && script.CurrentLine != null && script.CurrentLine is VO_Script_ChoiceMessage)
            {
                ScriptManager.CurrentChoice.Draw(gametime);
            }
        }

        /// <summary>
        /// Rafraichi un calque
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="main"></param>
        private void RefreshLayer(VO_Layer layer, bool main, bool moveOrAnimation)
        {
            //Decors
            foreach (VO_StageDecor item in layer.ListDecors)
            {
                Draw(SpriteManager.GetScreenSprite(item.Id));
            }

            //Animations
            foreach (VO_StageAnimation item in layer.ListAnimations)
            {
                VO_AnimatedSprite anim = _Service.DrawAnimated(item);
                if (anim != null)
                    Draw(anim, _Service.GetRatioFromMatrix(new Point(item.Location.X, item.Location.Y), _ProjectData.Resolution.MatrixPrecision), moveOrAnimation);
                if (moveOrAnimation && anim.CurrentSpriteIndex == 0 && anim.ReadyToExecScript)
                {
                    _Service.ExecuteAnimationScript(item, Enums.TriggerExecutionType.BeginingAnimation);
                    anim.ReadyToExecScript = false;
                }
                else if (moveOrAnimation && anim.CurrentSpriteIndex == anim.SpritesCount - 1 && anim.ReadyToExecScript)
                {
                    _Service.ExecuteAnimationScript(item, Enums.TriggerExecutionType.EndingAnimation);
                    anim.ReadyToExecScript = false;
                }

            }
        }

        /// <summary>
        /// Rafraichi les persos
        /// </summary>
        /// <param name="moveOrAnimation"></param>
        private void RefreshCharacters(int layerIndex, bool main, int playerIndex, bool moveOrAnimation)
        {
            bool playerDrawn = false;
            foreach (VO_StageCharacter item in _Stage.ListCharacters)
            {
                VO_CharacterSprite character = _Service.DrawCharacter(item);
                if (character != null)
                {
                    int characterIndex = _Service.GetLayerIndexFromCharacterLocation(character.Location);
                    if (characterIndex == 0 && main)
                        characterIndex = layerIndex;
                    if (playerIndex == 0 && main)
                        playerIndex = layerIndex;
                    if (layerIndex == characterIndex)

                        if (ScriptManager.IsPlayerVisible && !playerDrawn && PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location.Y < character.Location.Y && playerIndex == layerIndex)
                        {
                            Draw(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite, _Service.GetRatioFromMatrix(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location, _ProjectData.Resolution.MatrixPrecision), moveOrAnimation);
                            playerDrawn = true;
                        }
                    Draw(character, _Service.GetRatioFromMatrix(character.Location, _ProjectData.Resolution.MatrixPrecision), moveOrAnimation);
                }
            }
            if ((_Stage.ListCharacters.Count == 0 && (!playerDrawn && playerIndex == layerIndex && ScriptManager.IsPlayerVisible)) || (!playerDrawn && playerIndex == layerIndex && ScriptManager.IsPlayerVisible))
                Draw(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite, _Service.GetRatioFromMatrix(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location, _ProjectData.Resolution.MatrixPrecision), moveOrAnimation);
        }
        #endregion

        /// <summary>
        /// Renvoie les coordonnées souris corrects pour les events de scène
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public Point FixViewCoords(MouseState e)
        {
            return new Point((int)Camera2D.Pos.X + e.X, (int)Camera2D.Pos.Y + e.Y);
        }

        /// <summary>
        /// Lance le script de démarrage et crée la save de stage si non existente
        /// </summary>
        public void LaunchStartingScript()
        {
            if (IgnoreStartingScripts)
            {
                IgnoreStartingScripts = false;
                return;
            }
            VO_GameStateStage stage = GameState.State.Stages.Find(p => p.StageId == CurrentStageGuid);
            if (stage == null)
            {
                //On crée le stage dans l'instance de jeu
                stage = new VO_GameStateStage();
                stage.StageId = CurrentStageGuid;
                GameState.State.Stages.Add(stage);
            }
            if (!stage.StartScriptDone)
            {
                //Première fois
                if (_Stage.StartingFirstScript.Lines.Count > 0)
                {
                    VO_RunningScript firstScript = new VO_RunningScript();
                    firstScript.Id = _Stage.StartingFirstScript.Id;
                    firstScript.Lines = _Stage.StartingFirstScript.Lines;
                    ScriptManager.CurrentScript = firstScript;
                }
                stage.StartScriptDone = true;
            }
            else if (_Stage.StartingScript.Lines.Count > 0)
            {
                //Fois suivantes
                VO_RunningScript script = new VO_RunningScript();
                script.Id = _Stage.StartingScript.Id;
                script.Lines = _Stage.StartingScript.Lines;
                ScriptManager.CurrentScript = script;
            }
        }

        /// <summary>
        /// Lance le script de démarrage et crée la save de stage si non existente
        /// </summary>
        public void LaunchEndingScript()
        {
            VO_GameStateStage stage = GameState.State.Stages.Find(p => p.StageId == CurrentStageGuid);
            if (stage == null)
            {
                //On crée le stage dans l'instance de jeu
                stage = new VO_GameStateStage();
                stage.StageId = CurrentStageGuid;
                GameState.State.Stages.Add(stage);
            }
            if (!stage.EndScriptDone)
            {
                //Première fois
                if (_Stage.EndingFirstScript.Lines.Count > 0)
                {
                    VO_RunningScript endFirstScript = new VO_RunningScript();
                    endFirstScript.Id = _Stage.EndingFirstScript.Id;
                    endFirstScript.Lines = _Stage.EndingFirstScript.Lines;
                    ScriptManager.CurrentScript = endFirstScript;
                }
                else
                    ScriptManager.CurrentScript = null;
                stage.StartScriptDone = true;
            }
            else if (_Stage.EndingScript.Lines.Count > 0)
            {
                //Fois suivantes
                VO_RunningScript script = new VO_RunningScript();
                script.Id = _Stage.EndingScript.Id;
                script.Lines = _Stage.EndingScript.Lines;
                ScriptManager.CurrentScript = script;
            }
            else
                ScriptManager.CurrentScript = null;
            _LaunchedEndingScripts = true;
        }
        #endregion

        #region Load/Unload
        /// <summary>
        /// Load Stage screen
        /// </summary>
        public override void LoadScreen()
        {
            base.LoadScreen();

            #region Debug
            DebugConsole.InitializeConsole((Viewer)this.Game, _SpriteBatch, _Service);
            #endregion

            //Reset de certaines propriétés
            _Interface.EscapeMenuOpened = false;
            
            //Stage
            _Stage = _Service.GetStageData(CurrentStageGuid);
            ScriptManager.InitScriptManager(_Service, _Game, _Stage, _Interface, _SpriteBatch);
            ScriptManager.ScriptDefaultCamera();
            

            //Configuration de l'image manager
            ImageManager.SetCurrentStage(_Stage.Id);
            MatrixManager.SetCurrentStage(_Stage.Id);

            //Chargements graphiques
            _Service.PreLoadStage(_Stage, _ProjectData.Resolution.MatrixPrecision);

            //Chargement sauvegarde courante
            if (GameState.State.CurrentStagePNJ != null)
            {
                GameState.LoadGamePNJ();
                GameState.State.CurrentStagePNJ = null;
            }

            //Musique
            _Music = PathTools.GetProjectPath(Enums.ProjectPath.Musics) + _Stage.Music.Filename;
            _MusicFrequency = _Stage.Music.Frequency;

            //Chargement du perso player
            float ratio = _Service.GetRatioFromMatrix(PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.Location, _ProjectData.Resolution.MatrixPrecision);
            PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.SetScale(new Vector2(ratio, ratio));
            PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.SetPosition(StartingPosition.X, StartingPosition.Y);
            PlayableCharactersManager.CurrentPlayerCharacter.CurrentStage = CurrentStageGuid;

            //Execution du script de démarrage
            _LaunchedEndingScripts = false;
            LaunchStartingScript();
        }

        /// <summary>
        /// Unload Stage Screen
        /// </summary>
        public override void UnloadScreen()
        {
            ScriptManager.ResetScriptManager();

            base.UnloadScreen();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Changement d'écran
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _Interface_ChangingScreen(object sender, TransverseLayer.EventArgsClasses.GameScreenEventArgs e)
        {
            ScriptManager.GoToScreen = e;
        }

        /// <summary>
        /// Key press
        /// </summary>
        public override void KeyPress()
        {
            #region Debug
            if (KeyboardManager.OnClick(Keys.OemQuotes) && !DebugConsole.ReleaseMode)
            {
                DebugConsole.Visible = !DebugConsole.Visible;
            }
            #endregion

            if (!DebugConsole.Visible)
            {
                if (ScriptManager.BlockType == ViewerEnums.BlockType.Free)
                    _Interface.KeyPress();
                if (ScriptManager.CurrentChoice != null)
                    ScriptManager.CurrentChoice.KeyPress();
            }
        }

        /// <summary>
        /// MouseMove
        /// </summary>
        /// <param name="e"></param>
        public override void MouseMove(MouseState e)
        {
            if (!DebugConsole.Visible)
            {
                if (ScriptManager.BlockType <= ViewerEnums.BlockType.BlockUserMoves)
                {
                    if (_Interface.IsInterfaceActive())
                    {
                        _Interface.MouseMove(e);
                    }
                    else if (ScriptManager.ScriptStagesInteractions)
                    {
                        if (e.X >= 0f && e.Y > 0f)
                        {
                            Point fixedE = FixViewCoords(e);
                            if (_Service.GetEventFromMatrix(fixedE, _ProjectData.Resolution.MatrixPrecision) ||
                                _Interface.Inventory.GetInventoryButtonEvent(new Point(e.X, e.Y)))
                                ActionManager.ClickedState = true;
                            else
                                ActionManager.ClickedState = false;
                        }
                    }
                    else
                        ActionManager.ClickedState = false;
                }
                else if (ScriptManager.CurrentChoice != null)
                    ScriptManager.CurrentChoice.MouseMove(e);
            }
        }

        /// <summary>
        /// MouseLeftPress
        /// </summary>
        /// <param name="e"></param>
        public override void MouseLeftPress(MouseState e)
        {
            if (!DebugConsole.Visible)
            {
                if (ScriptManager.BlockType <= ViewerEnums.BlockType.BlockUserMoves)
                {
                    if (_Interface.IsInterfaceActive())
                    {
                        _Interface.MousePress(e);
                    }
                    else
                    {
                        Point eFixed = FixViewCoords(e);

                        //Si pas d'action, déplacement
                        if (!ActionManager.ClickedState && !_Interface.Inventory.Opened && ActionManager.CurrentActionIsGo() && ScriptManager.ScriptStagesInteractions)
                        {
                            PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.MoveCharacter(eFixed);
                        }
                        //Si action, on execute un event ou on gère l'interface
                        else
                        {
                            if (_Interface.Inventory.GetInventoryButtonEvent(new Point(e.X, e.Y)))
                            {
                                if (ActionManager.ItemAsAction)
                                    ActionManager.UnloadItem();
                                else
                                    _Interface.Inventory.SwitchInventory();
                            }
                            if (!_Interface.Inventory.Opened && ScriptManager.ScriptStagesInteractions)
                                if (!_Service.ExecuteClickedEvent(eFixed, _ProjectData.Resolution.MatrixPrecision))
                                    if (ActionManager.CurrentActionIsGo())
                                        PlayableCharactersManager.CurrentPlayerCharacter.CharacterSprite.MoveCharacter(eFixed);
                        }
                    }
                }
                else if (ScriptManager.CurrentChoice != null)
                    ScriptManager.CurrentChoice.MousePress(e);
            }
        }

        /// <summary>
        /// MouseRightPress
        /// </summary>
        /// <param name="e"></param>
        public override void MouseRightPress(MouseState e)
        {
            if (!DebugConsole.Visible)
            {
                if (ScriptManager.BlockType <= ViewerEnums.BlockType.BlockUserMoves)
                {
                    if (_Interface.IsInterfaceActive())
                    {
                        _Interface.MousePress(e);
                    }
                    else
                    {
                        PlayableCharactersManager.CurrentPlayerCharacter.ChangeNextAction();
                    }
                }
            }
        }

        /// <summary>
        /// Bouton souris relaché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseLeftReleased(MouseState e)
        {
            if (ScriptManager.BlockType <= ViewerEnums.BlockType.BlockUserMoves)
            {
                if (_Interface.IsInterfaceActive())
                {
                    _Interface.MouseLeftReleased(e);
                }
            }
        }
        #endregion
    }
}