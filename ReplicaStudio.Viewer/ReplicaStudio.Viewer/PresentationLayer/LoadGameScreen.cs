using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.EventArgsClasses;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Viewer.TransverseLayer;
using ReplicaStudio.Viewer.DataLayer;

namespace ReplicaStudio.Viewer.PresentationLayer
{
    /// <summary>
    /// Ecran "vide" de tout affichage, servant uniquement au chargement des données persistantes.
    /// </summary>
    public class LoadGameScreen : Screen
    {
        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public LoadGameScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            //1 - Charge le GUI
            VO_GUI.LoadNewGui(PathTools.GetProjectPath(Enums.ProjectPath.GUI) + _ProjectData.GuiResource);

            //2 - Charge les actions et items
            ActionManager.LoadActions();
            ItemManager.LoadItems();

            //3 - Initialise le son
            SoundManager.Initialize(8);
        }
        #endregion

        #region Inherited Methods
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GameCore.Instance.Game.Menu.ActivateMainMenu)
                ChangeScreen(this, new GameScreenEventArgs(ViewerEnums.ScreenType.Title));
            else
            {
                GameState.NewGame();
                ChangeScreen(this, new GameScreenEventArgs(ViewerEnums.ScreenType.Stage, PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Map, new Point(PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Location.X, PlayableCharactersManager.CurrentPlayerCharacter.PlayableCharacter.CoordsCharacter.Location.Y)));
            }
        }
        #endregion

        #region Methods spécifiques
        #endregion
    }
}
