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
    public class VO_GlobalEvent : VO_Base
    {
        #region Properties
        /// <summary>
        /// Script attaché
        /// </summary>
        public VO_Script Script
        {
            get;
            set;
        }

        /// <summary>
        /// Bouton de contrôle
        /// </summary>
        public Guid Trigger
        {
            get;
            set;
        }

        /// <summary>
        /// Utilise le bouton
        /// </summary>
        public bool UseTrigger
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public VO_GlobalEvent()
        {
        }

        public VO_GlobalEvent(Guid pID)
        {
            Id = pID;
        }

        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.GlobalEvents.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "GlobalEvent #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_GlobalEvent Clone()
        {
            return (VO_GlobalEvent)this.MemberwiseClone();
        }
        #endregion
    }
}
