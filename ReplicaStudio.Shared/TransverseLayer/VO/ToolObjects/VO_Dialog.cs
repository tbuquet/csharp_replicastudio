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
    public class VO_Dialog
    {
        #region Properties
        /// <summary>
        /// Utilise les faces
        /// </summary>
        public bool UseFaces { get; set; }

        /// <summary>
        /// Messages du dialogue
        /// </summary>
        public List<VO_Message> Messages
        {
            get;
            set;
        }

        /// <summary>
        /// Id de l'objet parent
        /// </summary>
        public Guid ParentObjectId
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_Dialog()
        {
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Clone de l'objet
        /// </summary>
        /// <returns></returns>
        public VO_Dialog Clone()
        {
            VO_Dialog newDialog = (VO_Dialog)this.MemberwiseClone();
            newDialog.Messages = new List<VO_Message>();
            foreach (VO_Message message in this.Messages)
            {
                newDialog.Messages.Add(message.Clone());
            }
            return newDialog;
        }
        #endregion
    }
}
