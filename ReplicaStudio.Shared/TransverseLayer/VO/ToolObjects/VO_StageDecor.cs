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
    /// VO Objet background sur scène
    /// </summary>
    [TypeConverter(typeof(DecorTypeConvertor))]
    public class VO_StageDecor : VO_StageObject
    {
        #region Properties
        [ReadOnly(true)]
        [Description("Filename of the resource")]
        [Category("General")]
        /// <summary>
        /// Nom du fichier
        /// </summary>
        public string Filename { get; set; }
        #endregion

        #region Constructors
        public VO_StageDecor()
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
