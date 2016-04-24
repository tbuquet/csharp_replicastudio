using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    public class DialogService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        DialogBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DialogService()
        {
            _Business = new DialogBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Créateur de dialogues
        /// </summary>
        /// <returns></returns>
        public VO_Dialog CreateDialog()
        {
            VO_Dialog dialog = null;

            RunServiceTask(delegate
            {
                dialog = _Business.CreateDialog();
            }, Errors.ERROR_DIALOG_STR_CREATE);

            return dialog;
        }

        /// <summary>
        /// Créateur de messages
        /// </summary>
        /// <returns></returns>
        public VO_Message CreateMessage()
        {
            VO_Message message = null;

            RunServiceTask(delegate
            {
                message = _Business.CreateMessage();
            }, Errors.ERROR_MESSAGE_STR_CREATE);

            return message;
        }
        #endregion
    }
}
