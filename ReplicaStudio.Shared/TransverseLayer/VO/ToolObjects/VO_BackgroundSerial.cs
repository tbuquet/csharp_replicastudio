using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    public class VO_BackgroundSerial
    {
        #region Properties
        public Size Size
        {
            get;
            set;
        }

        public int BlockSize
        {
            get;
            set;
        }

        public int Padding
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_BackgroundSerial()
        {
        }

        public VO_BackgroundSerial(Size size, int blockSize)
        {
            Size = size;
            BlockSize = blockSize;
        }

        public VO_BackgroundSerial(Size size, int blockSize, int padding)
        {
            Size = size;
            BlockSize = blockSize;
            Padding = padding;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Size.Width + ";" + Size.Height + ";" + BlockSize + ";" + Padding;
        }
        #endregion
    }
}

