using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.IO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.DataLayer;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ReplicaStudio.Viewer.PresentationLayer
{
    /// <summary>
    /// Gestion de l'interface
    /// </summary>
    public class Interface
    {
        #region Members
        /// <summary>
        /// Les interfaces sont activés.
        /// </summary>
        private bool _Enabled = true;

        /// <summary>
        /// Menu Echap
        /// </summary>
        VO_SelectableMenu _EscapeMenu = null;

        /// <summary>
        /// Menu Echap
        /// </summary>
        VO_SelectableMenu _GameStatesMenu = null;

        /// <summary>
        /// Sprite background menu echap
        /// </summary>
        Texture2D _EscapeMenuBackground = null;

        /// <summary>
        /// LifeBarBackground
        /// </summary>
        VO_Sprite _LifeBarBackground = null;

        /// <summary>
        /// LifeBar
        /// </summary>
        VO_Sprite _LifeBar = null;

        /// <summary>
        /// Life bar width
        /// </summary>
        int _LifeBarFull;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        SpriteBatch _SpriteBatch;
        #endregion

        #region Events
        /// <summary>
        /// Changement de screen
        /// </summary>
        public event Screen.ChangeScreenEventHandler ChangingScreen;
        #endregion

        #region Properties
        /// <summary>
        /// Menu echap activé
        /// </summary>
        public bool EscapeMenuOpened { get; set; }

        /// <summary>
        /// Menu save activé
        /// </summary>
        public bool SaveMenuOpened { get; set; }

        /// <summary>
        /// Menu load activé
        /// </summary>
        public bool LoadMenuOpened { get; set; }

        /// <summary>
        /// Menu option activé
        /// </summary>
        public bool OptionMenuOpened { get; set; }

        /// <summary>
        /// LifeBarVisible
        /// </summary>
        public bool LifeBarVisible { get; set; }

        public bool SaveActive { get; set; }

        public bool LoadActive { get; set; }

        /// <summary>
        /// Activation des interfaces
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                if (_Enabled)
                {
                    Inventory.Enabled = true;
                }
                else
                {
                    Inventory.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Référence à l'inventaire
        /// </summary>
        public Inventory Inventory { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Interface(SpriteBatch spriteBatch, Game game) 
        {
            _SpriteBatch = spriteBatch;

            //Inventaire
            Inventory = new Inventory(spriteBatch, game);
            LifeBarVisible = true;

            //Background
            //Charger console background
            RenderTarget2D texture = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight);
            game.GraphicsDevice.SetRenderTarget(texture);
            game.GraphicsDevice.Clear(Color.Black);

            game.GraphicsDevice.SetRenderTarget(null);

            _EscapeMenuBackground = (Texture2D)texture;

            //Escape Menu
            List<string> menu = new List<string>();
            if (GameCore.Instance.Game.Menu.ActivateSaveMenu)
            {
                menu.Add(GameCore.Instance.Game.Terminology.SaveGame);
                SaveActive = true;
            }
            if (GameCore.Instance.Game.Menu.ActivateLoadingMenu)
            {
                menu.Add(GameCore.Instance.Game.Terminology.LoadGame);
                LoadActive = true;
            }
            if (GameCore.Instance.Game.Menu.ActivateMainMenu)
                menu.Add(GameCore.Instance.Game.Terminology.ReturnTitle);
            //menu.Add(GameCore.Instance.Game.Terminology.Options);
            menu.Add(GameCore.Instance.Game.Terminology.LeaveGame);
            _EscapeMenu = new VO_SelectableMenu(spriteBatch, game, menu, 10);
            _EscapeMenu.FontSize = 20;
            _EscapeMenu.Position = new Vector2(GameCore.Instance.Game.Project.Resolution.Width / 2 - _EscapeMenu.Width / 2, GameCore.Instance.Game.Project.Resolution.Height / 2 - _EscapeMenu.Height / 2);
            _EscapeMenu.OnClick += new VO_SelectableMenu.OnClickEventHandler(_EscapeMenu_OnClick);
            _EscapeMenu.SelectedValueChanged += new VO_SelectableMenu.SelectedValueChangedEventHandler(Menu_SelectedValueChanged);

            //Save/Load menu
            if (GameCore.Instance.Game.Menu.ActivateSaveMenu || GameCore.Instance.Game.Menu.ActivateLoadingMenu)
            {
                List<string> saveMenu = new List<string>();
                for (int i = 1; i <= 9; i++)
                {
                    saveMenu.Add(GameCore.Instance.Game.Terminology.SaveState + i);
                }
                _GameStatesMenu = new VO_SelectableMenu(spriteBatch, game, saveMenu, 10);
                _GameStatesMenu.FontSize = 16;
                _GameStatesMenu.Position = new Vector2(GameCore.Instance.Game.Project.Resolution.Width / 2 - _GameStatesMenu.Width / 2, GameCore.Instance.Game.Project.Resolution.Height / 2 - _GameStatesMenu.Height / 2);
                _GameStatesMenu.OnClick += new VO_SelectableMenu.OnClickEventHandler(_GameStatesMenu_OnClick);
                _GameStatesMenu.SelectedValueChanged += new VO_SelectableMenu.SelectedValueChangedEventHandler(Menu_SelectedValueChanged);
            }

            //LifeBar
            if (!string.IsNullOrEmpty(GameCore.Instance.Game.Project.LifeBarBackground) && File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.LifeBar) + GameCore.Instance.Game.Project.LifeBarBackground))
            {
                _LifeBarBackground = new VO_Sprite(ImageManager.GetPermanentImage(PathTools.GetProjectPath(Enums.ProjectPath.LifeBar) + GameCore.Instance.Game.Project.LifeBarBackground));
                _LifeBarBackground.Position = new Vector2(GameCore.Instance.Game.Project.LifeBarCoords.X, GameCore.Instance.Game.Project.LifeBarCoords.Y);
            }
            if (!string.IsNullOrEmpty(GameCore.Instance.Game.Project.LifeBarResource) && File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.LifeBar) + GameCore.Instance.Game.Project.LifeBarResource))
            {
                _LifeBar = new VO_Sprite(ImageManager.GetPermanentImage(PathTools.GetProjectPath(Enums.ProjectPath.LifeBar) + GameCore.Instance.Game.Project.LifeBarResource));
                _LifeBar.Position = new Vector2(GameCore.Instance.Game.Project.LifeBarCoords.X, GameCore.Instance.Game.Project.LifeBarCoords.Y);
                _LifeBarFull = (int)_LifeBar.Width;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Vérifie si une interface est active
        /// </summary>
        /// <returns></returns>
        public bool IsInterfaceActive()
        {
            if (Inventory.Opened)
                return true;
            if (EscapeMenuOpened)
                return true;
            if (SaveMenuOpened)
                return true;
            if (LoadMenuOpened)
                return true;
            if (OptionMenuOpened)
                return true;
            return false;
        }

        public bool IsInterfaceBlocking()
        {
            if (EscapeMenuOpened)
                return true;
            if (SaveMenuOpened)
                return true;
            if (LoadMenuOpened)
                return true;
            if (OptionMenuOpened)
                return true;
            return false;
        }


        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, ViewerEnums.BlockType type)
        {
            if (type != ViewerEnums.BlockType.BlockUserControlsAndHideInterfaces)
                Inventory.Draw(gameTime);
            if (LifeBarVisible && PlayableCharactersManager.CurrentPlayerCharacter.ActivateLife)
            {
                _SpriteBatch.Draw(_LifeBarBackground.Image, _LifeBarBackground.Destination, Color.White);
                int percentage = PlayableCharactersManager.CurrentPlayerCharacter.PvAtStart * _LifeBarFull / PlayableCharactersManager.CurrentPlayerCharacter.PvMax;
                _LifeBar.Source = new Rectangle(0, 0, percentage, (int)_LifeBar.Height);
                _SpriteBatch.Draw(_LifeBar.Image, _LifeBar.Destination, Color.White);

            }
            if (EscapeMenuOpened)
            {
                _SpriteBatch.Draw(_EscapeMenuBackground, new Rectangle(0, 0, _EscapeMenuBackground.Width, _EscapeMenuBackground.Height), Color.White * ViewerConstants.MENU_BACKGROUND_TRANSPARENCY);
                _EscapeMenu.Draw(gameTime);
            }
            else if (SaveMenuOpened || LoadMenuOpened)
            {
                _SpriteBatch.Draw(_EscapeMenuBackground, new Rectangle(0, 0, _EscapeMenuBackground.Width, _EscapeMenuBackground.Height), Color.White * ViewerConstants.MENU_BACKGROUND_TRANSPARENCY);
                _GameStatesMenu.Draw(gameTime);
            }
            else if (OptionMenuOpened)
            {
                //_SpriteBatch.Draw(_EscapeMenuBackground.Image, _EscapeMenuBackground.Destination, Color.White);
                //_GameStatesMenu.Draw(app);
            }
        }
        #endregion

        #region Eventhandlers
        /// <summary>
        /// Sound interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Menu_SelectedValueChanged(object sender, GameMenuEventArgs e)
        {
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.MovementButtonSound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.MovementButtonSound);
        }

        public void MouseMove(MouseState e)
        {
            if (Inventory.Opened)
                Inventory.MouseMove(e);
            else if (EscapeMenuOpened)
                _EscapeMenu.MouseMove(e);
            else if (SaveMenuOpened || LoadMenuOpened)
                _GameStatesMenu.MouseMove(e);
            /*else if (OptionMenuOpened)
                _GameStatesMenu.MouseMove(sender, e);*/
        }

        public void MousePress(MouseState e)
        {
            if (Inventory.Opened)
                Inventory.MousePress(e);
            else if (EscapeMenuOpened)
                _EscapeMenu.MousePress(e);
            else if (SaveMenuOpened || LoadMenuOpened)
                _GameStatesMenu.MousePress(e);
            /*else if (OptionMenuOpened)
                _GameStatesMenu.MouseMove(sender, e);*/
        }

        /// <summary>
        /// Bouton souris relaché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeftReleased(MouseState e)
        {
            if (Inventory.Opened)
                Inventory.MouseLeftReleased(e);
        }

        /// <summary>
        /// Touche clavier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyPress()
        {
            if (GameCore.Instance.Game.Menu.ActivateEchapMenu && KeyboardManager.OnClick(Keys.Escape))
            {
                Inventory.Opened = false;
                if (SaveMenuOpened)
                {
                    SaveMenuOpened = false;
                    EscapeMenuOpened = true;
                    return;
                }
                else if (LoadMenuOpened)
                {
                    LoadMenuOpened = false;
                    EscapeMenuOpened = true;
                    return;
                }
                else if (OptionMenuOpened)
                {
                    OptionMenuOpened = false;
                    EscapeMenuOpened = true;
                    return;
                }
                if (EscapeMenuOpened)
                    EscapeMenuOpened = false;
                else
                    EscapeMenuOpened = true;
            }
            else if (EscapeMenuOpened)
                _EscapeMenu.KeyPress();
            else if (SaveMenuOpened)
                _GameStatesMenu.KeyPress();
        }

        /// <summary>
        /// Gestion du menu échap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _EscapeMenu_OnClick(object sender, GameMenuEventArgs e)
        {
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound);
            if (e.Key == GameCore.Instance.Game.Terminology.LeaveGame)
            {
                this.ChangingScreen(sender, new GameScreenEventArgs(ViewerEnums.ScreenType.Exit));
                return;
            }
            if (e.Key == GameCore.Instance.Game.Terminology.ReturnTitle)
            {
                this.ChangingScreen(sender, new GameScreenEventArgs(ViewerEnums.ScreenType.Title));
                return;
            }
            if (e.Key == GameCore.Instance.Game.Terminology.SaveGame && SaveActive)
            {
                EscapeMenuOpened = false;
                SaveMenuOpened = true;
            }
            if (e.Key == GameCore.Instance.Game.Terminology.LoadGame && LoadActive)
            {
                EscapeMenuOpened = false;
                LoadMenuOpened = true;
            }
        }

        private void _GameStatesMenu_OnClick(object sender, GameMenuEventArgs e)
        {
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound))
                SoundManager.PlaySound(PathTools.GetProjectPath(Enums.ProjectPath.Sounds) + GameCore.Instance.Game.Project.ChoiceButtonSound);
            if (SaveMenuOpened)
            {
                GameState.SaveGameState(string.Format(ViewerConstants.SaveName, e.SelectedIndex + 1));
            }
            else if (LoadMenuOpened)
            {
                if (GameState.LoadGameState(string.Format(ViewerConstants.SaveName, e.SelectedIndex + 1)))
                {
                    VO_GameStateCharacter gameCharacter = GameState.State.Players.Find(p => p.Id == GameState.State.CurrentCharacter);
                    this.ChangingScreen(sender, new GameScreenEventArgs(ViewerEnums.ScreenType.Stage, gameCharacter.Coords.Map, new Point(gameCharacter.Coords.Location.X, gameCharacter.Coords.Location.Y), true));
                }
            }
            EscapeMenuOpened = false;
            SaveMenuOpened = false;
            LoadMenuOpened = false;
        }
        #endregion
    }
}
