using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    /// <summary>
    /// Classe outil d'inventaire
    /// </summary>
    public class VO_InventoryItem
    {
        #region Properties
        public Guid ItemId { get; set; }
        public Point Location { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_InventoryItem(Guid itemId, Point location)
        {
            ItemId = itemId;
            Location = location;
        }
        #endregion
    }
}
