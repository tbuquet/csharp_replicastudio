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
    public class VO_Class : VO_Base
    {
        #region Properties
        /// <summary>
        /// Mauvaises interactions
        /// </summary>
        public List<VO_BadInteraction> BadInteractions
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public VO_Class()
        {
        }

        public VO_Class(Guid pID)
        {
            Id = pID;
        }

        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.Classes.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Class #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Class Clone()
        {
            VO_Class newClass = (VO_Class)this.MemberwiseClone();
            newClass.BadInteractions = new List<VO_BadInteraction>();
            foreach (VO_BadInteraction badInteraction in this.BadInteractions)
            {
                newClass.BadInteractions.Add(badInteraction.Clone());
            }
            return newClass;
        }
        #endregion
    }
}
