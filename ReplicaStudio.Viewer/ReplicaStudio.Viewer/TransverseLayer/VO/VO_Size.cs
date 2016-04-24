using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_Size
    {
        #region Properties
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        #region Constructors
        public VO_Size()
        {
        }

        public VO_Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion
    }
}
