using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.TransverseLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère la database des items
    /// </summary>
    class CoordsBusiness: BaseBusiness
    {
        #region Members
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public CoordsBusiness()
        {
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
            return ImageManager.GetImageBackground(new VO_BackgroundSerial(size, EditorSettings.Instance.TransparentBlockSize));
        }
        #endregion
    }
}
