using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    /// <summary>
    /// VO Objet animation sur scène
    /// </summary>
    [TypeConverter(typeof(AnimationTypeConvertor))]
    public class VO_StageAnimation : VO_StageObject
    {
        #region Properties
        [Browsable(false)]
        /// <summary>
        /// Color
        /// </summary>
        public VO_ColorTransformation Color { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Référence Animation
        /// </summary>
        public Guid AnimationId { get; set; }
        #endregion

        #region Constructors
        public VO_StageAnimation()
        {
        }
        #endregion

        #region Methods
        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(this);
            PropertyDescriptorCollection output = new PropertyDescriptorCollection(null);
            foreach (PropertyDescriptor descr in collection)
            {
                if (descr.Name != "Id" &&
                    descr.Name != "ClassId" &&
                    descr.Name != "PlayerPositionPoint" &&
                    descr.Name != "Filename" &&
                    descr.Name != "ObjectType" &&
                    descr.Name != "PlayerMustMove" &&
                    descr.Name != "PlayerMoveEndDirection" && 
                    descr.IsBrowsable)
                    output.Add(descr);
            }
            return output;
        }
        #endregion
    }
}
