using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Message
    {
        #region Properties
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Texte du dialogue
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Personnage qui parle
        /// </summary>
        public Guid Character
        {
            get;
            set;
        }

        /// <summary>
        /// FontSize
        /// </summary>
        public int FontSize
        {
            get;
            set;
        }

        /// <summary>
        /// Duration
        /// </summary>
        public int Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Voix
        /// </summary>
        public string Voice
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public VO_Message()
        {
        }

        #endregion

        #region Methods
        public VO_Message Clone()
        {
            return (VO_Message)this.MemberwiseClone();
        }
        #endregion
    }
}
