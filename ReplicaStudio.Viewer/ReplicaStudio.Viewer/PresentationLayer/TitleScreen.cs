using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using System.IO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.DataLayer;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using Microsoft.Xna.Framework.Input;

namespace ReplicaStudio.Viewer.PresentationLayer
{
    /// <summary>
    /// Ecran Screen
    /// </summary>
    public class TitleScreen : Screen
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        TitleService _Service;

        /// <summary>
        /// Référence aux données du menu
        /// </summary>
        VO_Menu _MenuData;

        /// <summary>
        /// Référence à l'anim de fond
        /// </summary>
        VO_AnimatedSprite _Background;

        /// <summary>
        /// Nouveau jeu
        /// </summary>
        VO_SelectableMenu _Menu;

        /// <summary>
        /// Menu Echap
        /// </summary>
        VO_SelectableMenu _GameStatesMenu = null;

        /// <summary>
        /// Menu Load enabled
        /// </summary>
        bool _LoadMenuEnabled = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public TitleScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            _Service = new TitleService();
            _MenuData = _Service.GetMenuData();
        }
        #endregion

        #region Inherited Methods
        /// <summary>
        /// Load Screen
        /// </summary>
        public override void LoadScreen()
        {
            base.LoadScreen();

            _LoadMenuEnabled = false;

            //Musique
            _Music = PathTools.GetProjectPath(Enums.ProjectPath.Musics) + _ProjectData.MainMenuMusic.Filename;

            //Configuration de l'image manager
            ImageManager.SetCurrentStage(Guid.Empty);

            //Création des sprites
            if (_MenuData.MainMenuAnimation != Guid.Empty)
                _Background = new VO_AnimatedSprite(_MenuData.MainMenuAnimation, Enums.AnimationType.Menu, 0, 0);

            //Préparation du menu
            List<string> listChoices = new List<string>();
            listChoices.Add(_TerminologyData.NewGame);
            if (_MenuData.ActivateLoadingMenu)
                listChoices.Add(_TerminologyData.LoadGame);
            //listChoices.Add(_TerminologyData.Options);
            listChoices.Add(_TerminologyData.LeaveGame);

            //Création du menu
            if (_Menu != null)
                _Menu.Dispose();
            _Menu = new VO_SelectableMenu(_SpriteBatch, this.Game, listChoices, 25);
            _Menu.FontSize = 20;
            _Menu.OnClick += new VO_SelectableMenu.OnClickEventHandler(OnClick);
            _Menu.SelectedValueChanged += new VO_SelectableMenu.SelectedValueChangedEventHandler(Menu_SelectedValueChanged);
            _Menu.Position = new Vector2(_ProjectData.Resolution.Width / 2 - _Menu.Width / 2, _ProjectData.Resolution.Height / 2 - _Menu.Height / 2);

            //Save/Load menu
            if (GameCore.Instance.Game.Menu.ActivateLoadingMenu)
            {
                List<string> saveMenu = new List<string>();
                for (int i = 1; i <= 9; i++)
                {
                    saveMenu.Add(GameCore.Instance.Game.Terminology.SaveState + i);
                }
                _GameStatesMenu = new VO_SelectableMenu(_SpriteBatch, this.Game, saveMenu, 10);
                _GameStatesMenu.FontSize = 16;
                _GameStatesMenu.Position = new Vector2(GameCore.Instance.Game.Project.Resolution.Width / 2 - _GameStatesMenu.Width / 2, GameCore.Instance.Game.Project.Resolution.Height / 2 - _GameStatesMenu.Height / 2);
                _GameStatesMenu.OnClick += new VO_SelectableMenu.OnClickEventHandler(_GameStatesMenu_OnClick);
                _GameStatesMenu.SelectedValueChanged += new VO_SelectableMenu.SelectedValueChangedEventHandler(Menu_SelectedValueChanged);
            }
            this.DrawOrder = 0;

            //Donner la main
            ScriptManager.ScriptUserControls = true;
        }

        /// <summary>
        /// Draw XNA
        /// </summary>
        /// <param name="gameTime"></param>
        public override void DrawScene(GameTime gameTime)
        {
            _SpriteBatch.Begin();
            Draw(_Background);
            if(this._LoadMenuEnabled)
                this._GameStatesMenu.Draw(gameTime);
            else
                this._Menu.Draw(gameTime);
            _SpriteBatch.End();
        }
        #endregion

        #region Input
        /// <summary>
        /// Mouse move
        /// </summary>
        /// <param name="mouseState"></param>
        public override void MouseMove(MouseState mouseState)
        {
            base.MouseMove(mouseState);

            if (this._LoadMenuEnabled)
            {
                this._GameStatesMenu.MouseMove(mouseState);
            }
            else
            {
                this._Menu.MouseMove(mouseState);
            }
        }

        /// <summary>
        /// Click gauche
        /// </summary>
        /// <param name="mouseState"></param>
        public override void MouseLeftPress(MouseState mouseState)
        {
            base.MouseLeftPress(mouseState);

            if (this._LoadMenuEnabled)
            {
                this._GameStatesMenu.MousePress(mouseState);
            }
            else
            {
                this._Menu.MousePress(mouseState);
            }
        }

        /// <summary>
        /// Clic droit
        /// </summary>
        /// <param name="mouseState"></param>
        public override void MouseRightPress(MouseState mouseState)
        {
            base.MouseRightPress(mouseState);

            _LoadMenuEnabled = false;
        }
        #endregion

        #region EventHandlers
        private void Menu_SelectedValueChanged(object sender, GameMenuEventArgs e)
        {
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.MovementButtonSound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.MovementButtonSound);
        }

        /// <summary>
        /// Click menu sauvegarde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _GameStatesMenu_OnClick(object sender, GameMenuEventArgs e)
        {
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound);
            if (GameState.LoadGameState(string.Format(ViewerConstants.SaveName, e.SelectedIndex + 1)))
            {
                VO_GameStateCharacter gameCharacter = GameState.State.Players.Find(p => p.Id == GameState.State.CurrentCharacter);
                ScriptManager.GoToScreen = new GameScreenEventArgs(ViewerEnums.ScreenType.Stage, gameCharacter.Coords.Map, new Point(gameCharacter.Coords.Location.X,gameCharacter.Coords.Location.Y) , true);
            }
        }

        /// <summary>
        /// Un élément du menu est sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnClick(object sender, GameMenuEventArgs e)
        {
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound);
            if (e.Key == _TerminologyData.NewGame)
            {
                GameState.NewGame();
                ScriptManager.GoToScreen = new GameScreenEventArgs(ViewerEnums.ScreenType.Stage, PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Map, new Point(PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Location.X, PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Location.Y));
            }
            else if (e.Key == _TerminologyData.LoadGame)
            {
                _LoadMenuEnabled = true;
            }
            else if (e.Key == _TerminologyData.Options)
            {
            }
            else if (e.Key == _TerminologyData.LeaveGame)
            {
                ScriptManager.GoToScreen = new GameScreenEventArgs(ViewerEnums.ScreenType.Exit);
            }
        }
        #endregion
    }
}
