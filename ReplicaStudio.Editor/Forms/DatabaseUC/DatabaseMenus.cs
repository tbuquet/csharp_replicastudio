using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Menus de la database
    /// </summary>
    public partial class DatabaseMenus : UserControl
    {
        #region Members
        #endregion

        #region Properties
        /// <summary>
        /// Instance de l'objet Menu
        /// </summary>
        VO_Menu Menu;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseMenus()
        {
            InitializeComponent();
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBMenus()
        {
            //Code de chargement
            Menu = GameCore.Instance.Game.Menu;

            //Désactiver events
            this.ddpGridWidth.ValueChanged -= new System.EventHandler(this.ddpGridWidth_ValueChanged);
            this.ddpGridHeight.ValueChanged -= new System.EventHandler(this.ddpGridHeight_ValueChanged);
            this.ddpItemWidth.ValueChanged -= new System.EventHandler(this.ddpItemWidth_ValueChanged);
            this.ddpItemHeight.ValueChanged -= new System.EventHandler(this.ddpGridHeight_ValueChanged);
            chkActivateMain.CheckedChanged -= new EventHandler(chkActivateMain_CheckedChanged);
            chkEchapMenu.CheckedChanged -= new EventHandler(chkEchapMenu_CheckedChanged);
            chkLoadingMenu.CheckedChanged -= new EventHandler(chkLoadingMenu_CheckedChanged);
            chkSaveMenu.CheckedChanged -= new EventHandler(chkSaveMenu_CheckedChanged);
            AnimMainMenu.AnimationLoading -= new EventHandler(AnimPrincipalMenu_AnimationLoading);
            AnimInventoryMenu.AnimationLoading -=new EventHandler(AnimInventoryMenu_AnimationLoading);
            AnimBackButton.AnimationLoading -= new EventHandler(AnimBackButton_AnimationLoading);

            //Bind des infos dans les contrôles
            chkActivateMain.Checked = Menu.ActivateMainMenu;
            chkEchapMenu.Checked = Menu.ActivateEchapMenu;
            chkLoadingMenu.Checked = Menu.ActivateLoadingMenu;
            chkSaveMenu.Checked = Menu.ActivateSaveMenu;
            
            if (Menu.InventoryAnimation != new Guid())
            {
                AnimInventoryMenu.LoadAnimation(Menu.InventoryAnimation);
                AnimInventoryMenu.Start();
            }
            else
                AnimInventoryMenu.LoadAnimation(new Guid());
            if (Menu.InventoryBackButtonAnimation != new Guid())
            {
                AnimBackButton.LoadAnimation(Menu.InventoryBackButtonAnimation);
                AnimBackButton.Start();
            }
            else
                AnimBackButton.LoadAnimation(new Guid());
            if (Menu.MainMenuAnimation != new Guid())
            {
                AnimMainMenu.LoadAnimation(Menu.MainMenuAnimation);
                AnimMainMenu.Start();
            }
            else
                AnimMainMenu.LoadAnimation(new Guid());

            crdInventoryPosition.SourceResolution = new Size(GameCore.Instance.Game.Project.Resolution.Width, GameCore.Instance.Game.Project.Resolution.Height);
            crdBackButtonPosition.SourceResolution = new Size(GameCore.Instance.Game.Project.Resolution.Width, GameCore.Instance.Game.Project.Resolution.Height);
            crdInventoryBackground.SourceResolution = new Size(GameCore.Instance.Game.Project.Resolution.Width, GameCore.Instance.Game.Project.Resolution.Height);
            crdInventoryPosition.Coords = new Rectangle(Menu.InventoryCoords, new Size(Menu.GridWidth * Menu.ItemWidth, Menu.GridHeight * Menu.ItemHeight));

            VO_Animation animInventory = GameCore.Instance.Game.MenusAnimations.Find(p => p.Id == Menu.InventoryAnimation);
            if(animInventory != null)
                crdInventoryBackground.Coords = new Rectangle(Menu.InventoryBackgroundCoords, new Size(animInventory.SpriteWidth, animInventory.SpriteHeight));
            else
                crdInventoryBackground.Coords = new Rectangle(Menu.InventoryBackgroundCoords, new Size(0, 0));
            VO_Animation animBackButton = GameCore.Instance.Game.MenusAnimations.Find(p => p.Id == Menu.InventoryBackButtonAnimation);
            if(animBackButton != null)
                crdBackButtonPosition.Coords = new Rectangle(Menu.InventoryBackButtonCoords, new Size(animBackButton.SpriteWidth, animBackButton.SpriteHeight));
            else
                crdBackButtonPosition.Coords = new Rectangle(Menu.InventoryBackButtonCoords, new Size(0, 0));
            ddpGridWidth.Value = Menu.GridWidth;
            ddpGridHeight.Value = Menu.GridHeight;
            ddpItemWidth.Value = Menu.ItemWidth;
            ddpItemHeight.Value = Menu.ItemHeight;

            //Activer les events
            this.ddpGridWidth.ValueChanged += new System.EventHandler(this.ddpGridWidth_ValueChanged);
            this.ddpGridHeight.ValueChanged += new System.EventHandler(this.ddpGridHeight_ValueChanged);
            this.ddpItemWidth.ValueChanged += new System.EventHandler(this.ddpItemWidth_ValueChanged);
            this.ddpItemHeight.ValueChanged += new System.EventHandler(this.ddpItemHeight_ValueChanged);
            AnimBackButton.AnimationLoading += new EventHandler(AnimBackButton_AnimationLoading);
            AnimInventoryMenu.AnimationLoading +=new EventHandler(AnimInventoryMenu_AnimationLoading);
            AnimMainMenu.AnimationLoading += new EventHandler(AnimPrincipalMenu_AnimationLoading);
            chkActivateMain.CheckedChanged += new EventHandler(chkActivateMain_CheckedChanged);
            chkEchapMenu.CheckedChanged += new EventHandler(chkEchapMenu_CheckedChanged);
            chkLoadingMenu.CheckedChanged += new EventHandler(chkLoadingMenu_CheckedChanged);
            chkSaveMenu.CheckedChanged += new EventHandler(chkSaveMenu_CheckedChanged);
        }

        #endregion

        #region EventHandlers
        /// <summary>
        /// Activation MainMenu changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkActivateMain_CheckedChanged(object sender, EventArgs e)
        {
            Menu.ActivateMainMenu = chkActivateMain.Checked;
            Menu.Update();
        }

        /// <summary>
        /// Activation Menu Echap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEchapMenu_CheckedChanged(object sender, EventArgs e)
        {
            Menu.ActivateEchapMenu = chkEchapMenu.Checked;
            Menu.Update();
        }

        /// <summary>
        /// Activation Save Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSaveMenu_CheckedChanged(object sender, EventArgs e)
        {
            Menu.ActivateSaveMenu = chkSaveMenu.Checked;
            Menu.Update();
        }

        /// <summary>
        /// Activation Loading Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLoadingMenu_CheckedChanged(object sender, EventArgs e)
        {
            Menu.ActivateLoadingMenu = chkLoadingMenu.Checked;
            Menu.Update();
        }

        /// <summary>
        /// Chargement d'animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimPrincipalMenu_AnimationLoading(object sender, EventArgs e)
        {
            Menu.MainMenuAnimation = AnimMainMenu.Animation;
            Menu.Update();
        }

        /// <summary>
        /// Chargement d'animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimInventoryMenu_AnimationLoading(object sender, EventArgs e)
        {
            Menu.InventoryAnimation = AnimInventoryMenu.Animation;
            Menu.Update();

            VO_Animation animInventory = GameCore.Instance.Game.MenusAnimations.Find(p => p.Id == Menu.InventoryAnimation);
            if (animInventory != null)
                crdInventoryBackground.Coords = new Rectangle(Menu.InventoryBackgroundCoords, new Size(animInventory.SpriteWidth, animInventory.SpriteHeight));
            else
                crdInventoryBackground.Coords = new Rectangle(Menu.InventoryBackgroundCoords, new Size(0, 0));
        }

        /// <summary>
        /// Chargement d'animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimBackButton_AnimationLoading(object sender, EventArgs e)
        {
            Menu.InventoryBackButtonAnimation = AnimBackButton.Animation;
            Menu.Update();

            VO_Animation animBackButton = GameCore.Instance.Game.MenusAnimations.Find(p => p.Id == Menu.InventoryBackButtonAnimation);
            if (animBackButton != null)
                crdBackButtonPosition.Coords = new Rectangle(Menu.InventoryBackButtonCoords, new Size(animBackButton.SpriteWidth, animBackButton.SpriteHeight));
            else
                crdBackButtonPosition.Coords = new Rectangle(Menu.InventoryBackButtonCoords, new Size(0, 0));
        }

        /// <summary>
        /// Coordonnées inventaire changées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdInventoryPosition_ValueChanged(object sender, EventArgs e)
        {
            Menu.InventoryCoords = crdInventoryPosition.Coords.Location;
            Menu.Update();
        }

        /// <summary>
        /// Coordonnées Back Button changées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdBackButtonPosition_ValueChanged(object sender, EventArgs e)
        {
            Menu.InventoryBackButtonCoords = crdBackButtonPosition.Coords.Location;
            Menu.Update();
        }

        /// <summary>
        /// Coordonnées Background changées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdInventoryBackground_ValueChanged(object sender, EventArgs e)
        {
            Menu.InventoryBackgroundCoords = crdInventoryBackground.Coords.Location;
            Menu.Update();
        }

        /// <summary>
        /// Item Width
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpItemWidth_ValueChanged(object sender, EventArgs e)
        {
            Menu.ItemWidth = ConvertTools.CastInt(ddpItemWidth.Value);
            crdInventoryPosition.Coords = new Rectangle(Menu.InventoryCoords, new Size(Menu.GridWidth * Menu.ItemWidth, Menu.GridHeight * Menu.ItemHeight));
            Menu.Update();
        }

        /// <summary>
        /// Item Height
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpItemHeight_ValueChanged(object sender, EventArgs e)
        {
            Menu.ItemHeight = ConvertTools.CastInt(ddpItemHeight.Value);
            crdInventoryPosition.Coords = new Rectangle(Menu.InventoryCoords, new Size(Menu.GridWidth * Menu.ItemWidth, Menu.GridHeight * Menu.ItemHeight));
            Menu.Update();
        }

        /// <summary>
        /// Grid Width
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpGridWidth_ValueChanged(object sender, EventArgs e)
        {
            Menu.GridWidth = ConvertTools.CastInt(ddpGridWidth.Value);
            crdInventoryPosition.Coords = new Rectangle(Menu.InventoryCoords, new Size(Menu.GridWidth * Menu.ItemWidth, Menu.GridHeight * Menu.ItemHeight));
            Menu.Update();
        }

        /// <summary>
        /// Grid Height
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Menu.GridHeight = ConvertTools.CastInt(ddpGridHeight.Value);
            crdInventoryPosition.Coords = new Rectangle(Menu.InventoryCoords, new Size(Menu.GridWidth * Menu.ItemWidth, Menu.GridHeight * Menu.ItemHeight));
            Menu.Update();
        }
        #endregion 
    }
}
