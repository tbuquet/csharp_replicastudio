using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    public class VO_EditorSettings
    {
        #region Properties
        /// <summary>
        /// Active ou désactive la synchro verticale
        /// </summary>
        public bool VSync { get; set; }

        /// <summary>
        /// Chemin vers le viewer
        /// </summary>
        public string ViewerPath { get; set; }

        /// <summary>
        /// Dossier des projets
        /// </summary>
        public string GamesFolder { get; set; }

        /// <summary>
        /// Taille d'un bloc transparent
        /// </summary>
        public int TransparentBlockSize { get; set; }

        /// <summary>
        /// Couleur du bloc transparent 1
        /// </summary>
        public VO_Color TransparentColor1 { get; set; }

        /// <summary>
        /// Couleur du bloc transparent 2
        /// </summary>
        public VO_Color TransparentColor2 { get; set; }

        /// <summary>
        /// Couleur de surlignement
        /// </summary>
        public VO_Color HighlightningColor { get; set; }

        /// <summary>
        /// Couleur de remplissage surligné
        /// </summary>
        public VO_Color HighlightningBrush { get; set; }

        /// <summary>
        /// Couleur du point HotSpot en mode sélectioné
        /// </summary>
        public VO_Color SelectedHotSpotColor { get; set; }

        /// <summary>
        /// Couleur du pointeur de coords
        /// </summary>
        public VO_Color SelectionCoords { get; set; }

        /// <summary>
        /// Taille des poignées des vecteurs
        /// </summary>
        public int VectorPointsSize { get; set; }

        /// <summary>
        /// Fréquence d'animation par défaut
        /// </summary>
        public int AnimationFrequency { get; set; }

        /// <summary>
        /// Padding de la scène
        /// </summary>
        public int StagePadding { get; set; }

        /// <summary>
        /// Durée des messages (secondes)
        /// </summary>
        public int MessageDuration { get; set; }

        /// <summary>
        /// Taille de la police des messages
        /// </summary>
        public int MessageFontSize { get; set; }

        /// <summary>
        /// Afficher la couche animation avec les masques
        /// </summary>
        public bool ShowAnimationsWhileMasking { get; set; }

        /// <summary>
        /// Afficher la couche characters avec les masques
        /// </summary>
        public bool ShowCharactersWhileMasking { get; set; }

        /// <summary>
        /// Activer le zoom à la roulette
        /// </summary>
        public bool ActivateZoomWithWheel { get; set; }
        #endregion

        #region Constructors
        public VO_EditorSettings()
        {
        }
        #endregion
    }
}

