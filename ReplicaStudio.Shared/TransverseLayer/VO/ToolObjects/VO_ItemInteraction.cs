using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_ItemInteraction
    {
        #region Constants
        #endregion

        #region Properties
        public Guid Script
        {
            get;
            set;
        }

        public Guid AssociatedItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_ItemInteraction()
        {
        }
        #endregion
    }
}

