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
    public class VO_Trigger : VO_Base
    {
        #region Properties
        /// <summary>
        /// Valeur du bouton
        /// </summary>
        public bool Value
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_Trigger()
        {
        }

        public VO_Trigger(Guid guid)
        {
            Id = guid;
        }
        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.Triggers.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Trigger #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Trigger Clone()
        {
            return (VO_Trigger)this.MemberwiseClone();
        }
        #endregion
    }
}
