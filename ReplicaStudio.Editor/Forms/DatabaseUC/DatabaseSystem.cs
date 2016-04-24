using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire System de la database
    /// </summary>
    public partial class DatabaseSystem : UserControl
    {
        #region Members
        /// <summary>
        /// Liste de persos
        /// </summary>
        List<VO_Base> Characters;
        #endregion

        #region Properties
        /// <summary>
        /// Projet
        /// </summary>
        public VO_Project Project
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseSystem()
        {
            InitializeComponent();
            rscMainMenuMusic.Filter = GlobalConstants.PROJECT_DIR_MUSICS;
            rscGameOverMusic.Filter = GlobalConstants.PROJECT_DIR_MUSICS;
            rscSystemGUI.Filter = GlobalConstants.PROJECT_DIR_GUIS;
            rscChoiceButtonSound.Filter = GlobalConstants.PROJECT_DIR_EFFECTS;
            rscMovementButtonSound.Filter = GlobalConstants.PROJECT_DIR_EFFECTS;
            rscLifeBar.Filter = GlobalConstants.PROJECT_DIR_LIFEBAR;
            rscLifeBarDeco.Filter = GlobalConstants.PROJECT_DIR_LIFEBAR;
            txtResolution.Enabled = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBSystem()
        {
            //Code de chargement
            Project = GameCore.Instance.Game.Project.Clone();

            //Désactiver events
            rscMainMenuMusic.ValueChanged -= new EventHandler(rscMainMenuMusic_ValueChanged);
            rscGameOverMusic.ValueChanged -= new EventHandler(rscGameOverMusic_ValueChanged);
            rscChoiceButtonSound.ValueChanged -= new EventHandler(rscChoiceButtonSound_ValueChanged);
            rscMovementButtonSound.ValueChanged -= new EventHandler(rscMovementButtonSound_ValueChanged);
            rscLifeBar.ValueChanged -= new EventHandler(rscLifeBar_ValueChanged);
            rscLifeBarDeco.ValueChanged -= new EventHandler(rscLifeBarDeco_ValueChanged);
            txtTitle.TextChanged -= new EventHandler(txtTitle_TextChanged);
            rscSystemGUI.ValueChanged -= new EventHandler(rscSystemGUI_ValueChanged);
            txtAuthor.TextChanged -= new EventHandler(txtAuthor_TextChanged);
            crdLifeBar.ValueChanged -= new EventHandler(crdLifeBar_ValueChanged);
            radMov4.CheckedChanged -= new EventHandler(radMov4_CheckedChanged);
            
            //Bind des infos dans les contrôles
            LoadLists();
            ScriptManager.LoadScript(Project.GameOver);
            rscMainMenuMusic.ResourceString = Project.MainMenuMusic.Filename;
            rscGameOverMusic.ResourceString = Project.GameOverMusic.Filename;
            rscChoiceButtonSound.ResourceString = Project.ChoiceButtonSound;
            rscMovementButtonSound.ResourceString = Project.MovementButtonSound;
            rscLifeBar.ResourceString = Project.LifeBarResource;
            rscLifeBarDeco.ResourceString = Project.LifeBarBackground;
            rscSystemGUI.ResourceString = Project.GuiResource;
            txtResolution.Text = Project.Resolution.Title;
            txtTitle.Text = Project.Title;
            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Menus) + Project.LifeBarResource))
            {
                Image pic = ImageManager.GetImageResource(PathTools.GetProjectPath(Enums.ProjectPath.Menus) + Project.LifeBarResource);
                crdLifeBar.Coords = new Rectangle(Project.LifeBarCoords, pic.Size);
            }
            else
            {
                crdLifeBar.Coords = new Rectangle(Project.LifeBarCoords, new Size(0,0));
            }
            crdLifeBar.SourceResolution = new Size(Project.Resolution.Width, Project.Resolution.Height);
            if (Project.SplashScreenAnimation != new Guid())
            {
                AnimLoading.LoadAnimation(Project.SplashScreenAnimation);
                AnimLoading.Start();
            }
            else
                AnimLoading.LoadAnimation(new Guid());
            radMov4.Checked = false;
            radMov8.Checked = false;
            if (Project.MovementDirections == 4)
                radMov4.Checked = true;
            else if (Project.MovementDirections == 8)
                radMov8.Checked = true;

            //Désactiver events
            rscMainMenuMusic.ValueChanged += new EventHandler(rscMainMenuMusic_ValueChanged);
            rscGameOverMusic.ValueChanged += new EventHandler(rscGameOverMusic_ValueChanged);
            rscChoiceButtonSound.ValueChanged += new EventHandler(rscChoiceButtonSound_ValueChanged);
            rscMovementButtonSound.ValueChanged += new EventHandler(rscMovementButtonSound_ValueChanged);
            rscLifeBar.ValueChanged += new EventHandler(rscLifeBar_ValueChanged);
            rscLifeBarDeco.ValueChanged += new EventHandler(rscLifeBarDeco_ValueChanged);
            rscSystemGUI.ValueChanged += new EventHandler(rscSystemGUI_ValueChanged);
            txtTitle.TextChanged += new EventHandler(txtTitle_TextChanged);
            txtAuthor.TextChanged += new EventHandler(txtAuthor_TextChanged);
            crdLifeBar.ValueChanged += new EventHandler(crdLifeBar_ValueChanged);
            radMov4.CheckedChanged += new EventHandler(radMov4_CheckedChanged);
        }

        /// <summary>
        /// Charger les listes
        /// </summary>
        private void LoadLists()
        {
            ddpCharacterStart.SelectedValueChanged -= new EventHandler(ddpCharacterStart_SelectedValueChanged);
            Characters = GameCore.Instance.GetPlayableCharacters();
            ddpCharacterStart.DisplayMember = "Title";
            ddpCharacterStart.ValueMember = "Id";
            ddpCharacterStart.DataSource = Characters;
            ddpCharacterStart.SelectedValue = (Guid)Project.StartingCharacter;
            ddpCharacterStart.SelectedValueChanged += new EventHandler(ddpCharacterStart_SelectedValueChanged);
        }

        /// <summary>
        /// Mets à jour la hauteur des anim des personnages
        /// </summary>
        private void UpdateCharacterAnimations()
        {
            List<VO_Animation> anims = new List<VO_Animation>();
            foreach (VO_Character character in GameCore.Instance.Game.Characters)
            {
                foreach (VO_Animation anim in character.Animations)
                {
                    if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.CharAnimations) + anim.ResourcePath))
                    {
                        Size imageSize = ImageManager.GetImageResource(PathTools.GetProjectPath(Enums.ProjectPath.CharAnimations) + anim.ResourcePath).Size;
                        int newValue = imageSize.Height / Project.MovementDirections;
                        if (newValue <= 0)
                            newValue = 1;
                        anim.SpriteHeight = newValue;
                        anims.Add(anim);
                    }
                }
            }
        }

        public void EnableTreeNodeColor()
        {
            this.ScriptManager.EnableDrawManager();
        }

        public void DisableTreeNodeColor()
        {
            this.ScriptManager.DisableDrawManager();
        }

        #endregion

        #region EventHandlers
        /// <summary>
        /// Les coordonnées de la lifebar changent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crdLifeBar_ValueChanged(object sender, EventArgs e)
        {
            Project.LifeBarCoords = crdLifeBar.Coords.Location;
            Project.Update();
        }

        /// <summary>
        /// La valeur de la ressource musique change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rscMainMenuMusic_ValueChanged(object sender, EventArgs e)
        {
            VO_Music music = new VO_Music();
            music.Filename = rscMainMenuMusic.ResourceString;
            Project.MainMenuMusic = music;
            Project.Update();
        }

        /// <summary>
        /// Ressource change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rscLifeBarDeco_ValueChanged(object sender, EventArgs e)
        {
            Project.LifeBarBackground = rscLifeBarDeco.ResourceString;
            Project.Update();
        }

        /// <summary>
        /// Ressource change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rscLifeBar_ValueChanged(object sender, EventArgs e)
        {
            Project.LifeBarResource = rscLifeBar.ResourceString;
            Project.Update();

            if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.LifeBar) + Project.LifeBarResource))
            {
                Image pic = ImageManager.GetImageResource(PathTools.GetProjectPath(Enums.ProjectPath.LifeBar) + Project.LifeBarResource);
                crdLifeBar.Coords = new Rectangle(Project.LifeBarCoords, pic.Size);
            }
            else
            {
                crdLifeBar.Coords = new Rectangle(Project.LifeBarCoords, new Size(0, 0));
            }
        }

        /// <summary>
        /// La valeur de la ressource musique change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rscGameOverMusic_ValueChanged(object sender, EventArgs e)
        {
            VO_Music music = new VO_Music();
            music.Filename = rscGameOverMusic.ResourceString;
            Project.GameOverMusic = music;
            Project.Update();
        }

        /// <summary>
        /// La valeur de la ressource GUI change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rscSystemGUI_ValueChanged(object sender, EventArgs e)
        {
            Project.GuiResource = rscSystemGUI.ResourceString;
            Project.Update();
        }

        /// <summary>
        /// Chargement d'animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimLoading_AnimationLoading(object sender, EventArgs e)
        {
            Project.SplashScreenAnimation = AnimLoading.Animation;
            Project.Update();
        }

        /// <summary>
        /// La valeur de la ressource sound 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rscMovementButtonSound_ValueChanged(object sender, EventArgs e)
        {
            string sound = rscMovementButtonSound.ResourceString;
            Project.MovementButtonSound = sound;
            Project.Update();
        }

        /// <summary>
        /// La valeur de la ressource sound 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rscChoiceButtonSound_ValueChanged(object sender, EventArgs e)
        {
            string sound = rscChoiceButtonSound.ResourceString;
            Project.ChoiceButtonSound = sound;
            Project.Update();
        }

        /// <summary>
        /// Le titre a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            Project.Title = txtTitle.Text;
            Project.Update();
        }

        /// <summary>
        /// L'auteur a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtAuthor_TextChanged(object sender, EventArgs e)
        {
            Project.Author = txtAuthor.Text;
            Project.Update();
        }

        /// <summary>
        /// La visibilité du controle change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtResolution_VisibleChanged(object sender, EventArgs e)
        {
            LoadLists();
        }

        /// <summary>
        /// La valeur du perso de départ change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpCharacterStart_SelectedValueChanged(object sender, EventArgs e)
        {
            Project.StartingCharacter = (Guid)ddpCharacterStart.SelectedValue;
            Project.Update();
        }

        /// <summary>
        /// Changement de nombre de mouvements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radMov4_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (radMov4.Checked)
                Project.MovementDirections = 4;
            else
                Project.MovementDirections = 8;
            Project.Update();
            UpdateCharacterAnimations();

            Cursor.Current = DefaultCursor;
        }
        #endregion
    }
}
