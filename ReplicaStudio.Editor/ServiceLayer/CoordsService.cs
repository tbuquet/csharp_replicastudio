using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Drawing;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    public class CoordsService : BaseService
    {
        /// <summary>
        /// Classe service qui gère les items dans la database
        /// </summary>
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        CoordsBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public CoordsService()
        {
            _Business = new CoordsBusiness();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Charge un background vide en temps que ressource permanente
        /// </summary>
        /// <param name="pSizeContainer">Taille de la surface</param>
        /// <returns>Surface</returns>
        public Image LoadBackground(Size size, bool useCurrentStageBackground)
        {
            Image image = null;

            RunServiceTask(delegate
            {
                image = _Business.LoadBackground(size, useCurrentStageBackground);
            }, Errors.ERROR_CORDS_STR_LOAD, size.ToString(), useCurrentStageBackground.ToString());

            return image;
        }
        #endregion
    }
}
