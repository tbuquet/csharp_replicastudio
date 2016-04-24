using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    /// <summary>
    /// VO Objet Event sur scène
    /// </summary>
    [TypeConverter(typeof(EventTypeConvertor))]
    public class VO_StageHotSpot : VO_StageObject
    {
        #region Properties
        [Browsable(false)]
        /// <summary>
        /// Liste des points
        /// </summary>
        public Point[] Points { get; set; }
        #endregion

        #region Constructors
        public VO_StageHotSpot()
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
