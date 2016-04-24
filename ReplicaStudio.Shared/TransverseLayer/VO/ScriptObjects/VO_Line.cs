using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Line
    {
        #region Properties
        public Guid Id { get; set; }
        public bool Valid { get; set; }
        #endregion

        #region Constructors
        public VO_Line()
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
