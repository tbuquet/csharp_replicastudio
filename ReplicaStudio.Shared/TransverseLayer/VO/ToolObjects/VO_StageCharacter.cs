using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    /// <summary>
    /// VO Objet animation sur scène
    /// </summary>
    [TypeConverter(typeof(CharacterTypeConvertor))]
    public class VO_StageCharacter : VO_StageObject
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
        public Guid CharacterId { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Animation sélectionée
        /// </summary>
        public Guid AnimationId { get; set; }
        #endregion

        #region Constructors
        public VO_StageCharacter()
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
                    descr.Name != "Filename" &&
                    descr.Name != "ObjectType" &&
                    descr.IsBrowsable)
                    output.Add(descr);
            }
            return output;
        }
        #endregion
    }
}
