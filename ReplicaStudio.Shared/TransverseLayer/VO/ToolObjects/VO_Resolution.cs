using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    /// <summary>
    /// VO Résolution
    /// </summary>
    public class VO_Resolution
    {
        #region Properties
        /// <summary>
        /// Largeur de l'écran
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Hauteur de l'écran
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Titre de la résolution
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Précision de la matrice associée
        /// </summary>
        public int MatrixPrecision { get; set; }
        #endregion

        #region Constructors
        public VO_Resolution(int pWidth, int pHeight, int matrixPrecision)
        {
            Width = pWidth;
            Height = pHeight;
            MatrixPrecision = matrixPrecision;
            Title = pWidth + " x " + pHeight;
        }

        public VO_Resolution()
        {
        }
        #endregion
    }
}
