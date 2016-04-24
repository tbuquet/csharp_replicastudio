using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Coords
    {
        #region Properties
        public Point Location
        {
            get;
            set;
        }

        public Guid Map
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_Coords()
        {
        }

        public VO_Coords(Point coords, Guid map)
        {
            Location = coords;
            Map = map;
        }
        #endregion

        #region Methods
        public VO_Coords Clone()
        {
            return (VO_Coords)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return Location.X + " x " + Location.Y;
        }
        #endregion
    }
}

