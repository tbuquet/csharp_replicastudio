using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ReplicaStudio.Shared.DatasLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Project
    {
        #region Properties
        /// <summary>
        /// Titre du projet
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Fichier du projet
        /// </summary>
        public string ProjectFileName { get; set; }

        /// <summary>
        /// Résolution du projet
        /// </summary>
        public VO_Resolution Resolution { get; set; }

        /// <summary>
        /// RoothPath du projet
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Version de l'appli avec laquelle le projet est enregistré
        /// </summary>
        public double Version { get; set; }

        /// <summary>
        /// Le projet est il enregistré depuis une Beta ou non
        /// </summary>
        public bool BetaVersion { get; set; }

        /// <summary>
        /// Nombres de directions de mouvements
        /// </summary>
        public int MovementDirections { get; set; }

        /// <summary>
        /// Personnage de départ
        /// </summary>
        public Guid StartingCharacter { get; set; }

        /// <summary>
        /// Script de GameOver
        /// </summary>
        public VO_Script GameOver { get; set; }

        /// <summary>
        /// SplashScreen de chargement
        /// </summary>
        public Guid SplashScreenAnimation { get; set; }

        /// <summary>
        /// Auteur
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Musique menu principal
        /// </summary>
        public VO_Music MainMenuMusic { get; set; }

        /// <summary>
        /// Musique game over
        /// </summary>
        public VO_Music GameOverMusic { get; set; }

        /// <summary>
        /// Son mouvement de bouton
        /// </summary>
        public string MovementButtonSound { get; set; }

        /// <summary>
        /// Son choix
        /// </summary>
        public string ChoiceButtonSound { get; set; }

        /// <summary>
        /// Resource barre de vie
        /// </summary>
        public string LifeBarResource { get; set; }

        /// <summary>
        /// Ressource background de barre de vie
        /// </summary>
        public string LifeBarBackground { get; set; }

        /// <summary>
        /// Coordonnées de la barre de vie
        /// </summary>
        public Point LifeBarCoords { get; set; }

        /// <summary>
        /// Ressource du GUI
        /// </summary>
        public string GuiResource { get; set; }
        #endregion

        #region Constructor
        public VO_Project()
        {
        }

        public VO_Project(string pTitle, VO_Resolution pResolution, string pRootPath)
        {
            Title = pTitle;
            Resolution = pResolution;
            RootPath = pRootPath;
        }
        #endregion

        #region Methods
        public void Update()
        {
            try
            {
                GameCore.Instance.Game.Project = (VO_Project)this.MemberwiseClone();
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_UPDATE_VO + "Project :" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Project Clone()
        {
            return (VO_Project)this.MemberwiseClone();
        }
        #endregion
    }
}
