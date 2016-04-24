using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    [TypeConverter(typeof(WalkableTypeConvertor))]
    public class VO_StageWalkable : VO_StageHotSpot
    {
        #region Properties
        #endregion

        #region Constructor
        public VO_StageWalkable()
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
                    descr.Name != "ClassId" &&
                    descr.Name != "ObjectType" &&
                    descr.Name != "PlayerMustMove" &&
                    descr.Name != "PlayerMoveEndDirection" &&
                    descr.Name != "PlayerPositionPoint" &&
                    descr.IsBrowsable)
                    output.Add(descr);
            }
            return output;
        }
        #endregion
    }
}
