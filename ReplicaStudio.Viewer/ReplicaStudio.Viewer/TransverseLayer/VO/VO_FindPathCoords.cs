using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    /// <summary>
    /// Objet pour Thread FindPathCoords
    /// </summary>
    public class VO_FindPathCoords
    {
        #region Properties
        public Point Start { get; set; }
        public Point End { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="start">Point de départ</param>
        /// <param name="end">Point d'arrivé</param>
        public VO_FindPathCoords(Point start, Point end)
        {
            Start = start;
            End = end;
        }
        #endregion
    }
}
