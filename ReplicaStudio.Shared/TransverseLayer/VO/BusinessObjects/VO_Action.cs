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
    public class VO_Action : VO_Base
    {
        #region Properties
        /// <summary>
        /// Description de l'item
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// Icone d'inventaire
        /// </summary>
        public Guid InventoryIcon
        {
            get;
            set;
        }
        /// <summary>
        /// Icone avec souris inactive
        /// </summary>
        public Guid Icon
        {
            get;
            set;
        }
        /// <summary>
        /// Icone avec souris active
        /// </summary>
        public Guid ActiveIcon
        {
            get;
            set;
        }

        /// <summary>
        /// GoAction (true = non supprimable)
        /// </summary>
        public bool GoAction
        {
            get;
            set;
        }

        /// <summary>
        /// UseAction (true = non supprimable)
        /// </summary>
        public bool UseAction
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public VO_Action()
        {
        }

        public VO_Action(Guid pID)
        {
            Id = pID;
        }

        #endregion

        #region Methods
        public override void Delete()
        {
            try
            {
                GameCore.Instance.Game.Actions.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Action #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Action Clone()
        {
            return (VO_Action)this.MemberwiseClone();
        }
        #endregion
    }
}
