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
    public class VO_Variable : VO_Base
    {
        #region Properties
        /// <summary>
        /// Valeur du bouton
        /// </summary>
        public int Value
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public VO_Variable()
        {
        }

        public VO_Variable(Guid guid)
        {
            Id = guid;
        }
        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.Variables.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Variable #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Variable Clone()
        {
            return (VO_Variable)this.MemberwiseClone();
        }
        #endregion
    }
}
