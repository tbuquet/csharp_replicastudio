using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.IO;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Layer : VO_Base, IComparable<VO_Layer>
    {
        #region Members
        #endregion

        #region Properties
        /// <summary>
        /// Liste des walkable areas
        /// </summary>
        public List<VO_StageWalkable> ListWalkableAreas { get; set; }

        /// <summary>
        /// Liste de décors
        /// </summary>
        public List<VO_StageDecor> ListDecors { get; set; }

        /// <summary>
        /// Liste d'animations
        /// </summary>
        public List<VO_StageAnimation> ListAnimations { get; set; }

        /// <summary>
        /// Détermine si ce calque est le calque principal, non removable.
        /// </summary>
        public bool MainLayer { get; set; }

        /// <summary>
        /// Id du stage associé
        /// </summary>
        public Guid Stage { get; set; }

        /// <summary>
        /// Ordre d'apparition du calque
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Indique si le calque est caché
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Filtres de couleurs
        /// </summary>
        public VO_ColorTransformation ColorTransformations { get; set; }
        #endregion

        #region Constructors
        public VO_Layer()
        {
        }
        #endregion

        #region Methods
        public void Delete()
        {
            GameCore.Instance.GetStageById(Stage).ListLayers.Remove(this);
        }

        public VO_Layer Clone()
        {
            return (VO_Layer)this.MemberwiseClone();
        }

        public int CompareTo(VO_Layer obj)
        {
            return Ordinal.CompareTo(obj.Ordinal);
        }
        #endregion
    }
}