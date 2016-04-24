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
    public class VO_BadInteraction
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
        /// Action
        /// </summary>
        public Guid Action
        {
            get;
            set;
        }

        /// <summary>
        /// Character qui doit interargir avec la classe.
        /// </summary>
        public Guid Character
        {
            get;
            set;
        }

        /// <summary>
        /// Dialogue
        /// </summary>
        public VO_Dialog Dialog
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_BadInteraction()
        {
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Clone de l'objet
        /// </summary>
        /// <returns></returns>
        public VO_BadInteraction Clone()
        {
            return (VO_BadInteraction)this.MemberwiseClone();
        }
        #endregion
    }
}
