using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointAndClickStudio.Shared.TransverseLayer.Constantes;
using PointAndClickStudio.Shared.TransverseLayer.VO;
using PointAndClickStudio.Shared.DatasLayer;

namespace PointAndClickStudio.Editor.TransverseLayer
{
    public class StageHelper
    {
        #region Members
        private static StageHelper mInstance;
        #endregion

        #region Properties
        public static StageHelper Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new StageHelper();
                }
                return mInstance;
            }
        }

        /// <summary>
        /// Item en cours de drag & drop
        /// </summary>
        public string DragDropItemId { get; set; }

        /// <summary>
        /// Type de l'objet en cours de drag & drop
        /// </summary>
        public Enums.StageObjectType DragDropObjectType { get; set; }

        /// <summary>
        /// Scène courante
        /// </summary>
        public string CurrentStage { get; set; }

        /// <summary>
        /// Renvoie l'ID du calque courant
        /// </summary>
        public Guid CurrentLayer { get; set; }

        /// <summary>
        /// Valeur courante du zoom
        /// </summary>
        public int CurrentZoom { get; set; }

        /// <summary>
        /// Objets sélectionnés sur scène
        /// </summary>
        public List<VO_StageObject> SelectedObjects { get; set; }
        #endregion

        #region Constructors
        private StageHelper()
        {
            SelectedObjects = new List<VO_StageObject>();
            CurrentZoom = 1;
        }
        #endregion

        #region Methods
        public VO_Stage GetCurrentStageInstance()
        {
            return GameCore.Instance.Stages[StageHelper.Instance.CurrentStage];
        }

        public VO_Layer GetCurrentLayerInstance()
        {
            foreach (VO_Layer vLayer in GameCore.Instance.Stages[StageHelper.Instance.CurrentStage].ListLayers)
                if (vLayer.Id == CurrentLayer)
                    return vLayer;
            return new VO_Layer();
        }
        #endregion
    }
}
