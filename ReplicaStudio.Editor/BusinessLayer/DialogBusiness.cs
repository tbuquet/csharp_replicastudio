using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère la database des items
    /// </summary>
    class DialogBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DialogBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Créateur de dialogues
        /// </summary>
        /// <returns></returns>
        public VO_Dialog CreateDialog()
        {
            return ObjectsFactory.CreateDialog();
        }

        /// <summary>
        /// Créateur de messages
        /// </summary>
        /// <returns></returns>
        public VO_Message CreateMessage()
        {
            return ObjectsFactory.CreateMessage();
        }
        #endregion
    }
}
