using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer;
using ReplicaStudio.Viewer.ServiceLayer;
using ReplicaStudio.Viewer.PresentationLayer;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Viewer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Viewer : Microsoft.Xna.Framework.Game
    {
        #region Members
        /// <summary>
        /// Service Reference
        /// </summary>
        GameHypervisorService _Hypervisor = new GameHypervisorService();

        /// <summary>
        /// Current Screen
        /// </summary>
        Screen _CurrentScreen;

        /// <summary>
        /// Graphics Manager XNA
        /// </summary>
        GraphicsDeviceManager _Graphics;

        /// <summary>
        /// SpriteManager XNA
        /// </summary>
        SpriteBatch _SpriteBatch;

        #region Ecrans
        /// <summary>
        /// Screen chargement
        /// </summary>
        LoadGameScreen _LoadingGameScreen;

        /// <summary>
        /// Screen Titre
        /// </summary>
        TitleScreen _TitleScreen;

        /// <summary>
        /// Screen Stage
        /// </summary>
        StageScreen _StageScreen;
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="parameters">User parameters</param>
        public Viewer(string[] parameters)
        {
            //1 - Load user parameters
            LoadSettings(parameters);

            //2 - Load the game
            ViewerEnums.LoadingState loadingState = _Hypervisor.LoadGame(ViewerSettings.AppPath);

            //3 - Renderer
            _Graphics = new GraphicsDeviceManager(this);
            _Graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(Graphics_PreparingDeviceSettings);

            //4 - Setup the parameters
            _Graphics.SynchronizeWithVerticalRetrace = ViewerSettings.VerticalSync;
            _Graphics.IsFullScreen = ViewerSettings.Fullscreen;
            this.IsMouseVisible = false;

            //5 - Initialize engines
            MouseManager.InitMouseManager(this);
        }
        #endregion

        #region Load/Unload
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _SpriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";
            //ResourceContentManager content = new ResourceContentManager(this.Services, Resources.Resources.ResourceManager);

            //5 - Initialise graphic contents
            ImageManager.InitImageManager(this.GraphicsDevice, this._SpriteBatch);
            FontManager.LoadFonts(Content, null); //TODO

            //6 - Load Screens
            _LoadingGameScreen = new LoadGameScreen(this, this._SpriteBatch);
            _TitleScreen = new TitleScreen(this, this._SpriteBatch);
            _StageScreen = new StageScreen(this, this._SpriteBatch);
            _CurrentScreen = _LoadingGameScreen;

            //7 - Listen for screen changement
            _CurrentScreen.ChangingScreen += new Screen.ChangeScreenEventHandler(OnChangingScreen);

            this.Components.Add(_LoadingGameScreen);
        }
        #endregion

        #region Refresh
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load user parameters
        /// </summary>
        /// <param name="param">Parameters</param>
        private void LoadSettings(string[] parameters)
        {
            //Parcours des arguments en mode associé [MODE AUTOMATIQUE]
            if (parameters.Length >= 1)
                ViewerSettings.AppPath = parameters[0];

            //Parcours des arguments et configuration [MODE MANUEL]
            for (int i = 1; i < parameters.Length; i++)
            {
                if (parameters[i][0] == '-')
                {
                    switch (parameters[i])
                    {
                        case ViewerConstants.ARG_FULLSCREEN:
                            ViewerSettings.Fullscreen = true;
                            break;
                        case ViewerConstants.ARG_VSYNC:
                            ViewerSettings.VerticalSync = true;
                            break;
                        case ViewerConstants.ARG_SOUND:
                            ViewerSettings.ActivateSound = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Leave a screen for another
        /// </summary>
        public void LeaveScreen()
        {
            for (int i = this.Components.Count - 1; i > -1; i--)
            {
                this.Components.RemoveAt(i);
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        public void Close()
        {
            Exit();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Modifies the display mode for the graphics device 
        /// when it is reset or recreated.
        /// </summary>
        void Graphics_PreparingDeviceSettings(object sender,
            PreparingDeviceSettingsEventArgs e)
        {
            foreach (Microsoft.Xna.Framework.Graphics.DisplayMode displayMode
                in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                if (displayMode.Width == GameCore.Instance.Game.Project.Resolution.Width && displayMode.Height == GameCore.Instance.Game.Project.Resolution.Height)
                {
                    e.GraphicsDeviceInformation.PresentationParameters.
                        BackBufferFormat = displayMode.Format;
                    e.GraphicsDeviceInformation.PresentationParameters.
                        BackBufferHeight = displayMode.Height;
                    e.GraphicsDeviceInformation.PresentationParameters.
                        BackBufferWidth = displayMode.Width;
                }
            }
        }

        /// <summary>
        /// Changing screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnChangingScreen(object sender, GameScreenEventArgs e)
        {
            _CurrentScreen.ChangingScreen -= new Screen.ChangeScreenEventHandler(OnChangingScreen);
            LeaveScreen();
            _CurrentScreen.UnloadScreen();

            switch (e.ScreenCalled)
            {
                case ViewerEnums.ScreenType.Title:
                    _CurrentScreen = _TitleScreen;
                    break;
                case ViewerEnums.ScreenType.Stage:
                    _StageScreen.CurrentStageGuid = e.ScreenId;
                    _StageScreen.StartingPosition = e.Position;
                    _StageScreen.IgnoreStartingScripts = e.IgnoreStartingScript;
                    _CurrentScreen = _StageScreen;
                    break;
                case ViewerEnums.ScreenType.Exit:
                    Close();
                    break;
            }

            _CurrentScreen.LoadScreen();
            this.Components.Add(_CurrentScreen);
            _CurrentScreen.LaunchMusic();
            _CurrentScreen.ChangingScreen += new Screen.ChangeScreenEventHandler(OnChangingScreen);
        }
        #endregion

        #region Launcher
        /// <summary>
        /// Launcher
        /// </summary>
        static class Program
        {
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            static void Main(string[] args)
            {
                using (Viewer game = new Viewer(args))
                {
                    game.Run();
                }
            }
        }
        #endregion
    }
}
