using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.Converters
{
    public class EventTypeConvertor : TypeConverter
    {
        public VO_StageHotSpot HotSpot { get; set; }

        // Summary:
        //     Initializes a new System.Drawing.SizeConverter object.
        public EventTypeConvertor()
        {
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value.ToString();
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            VO_StageHotSpot hotspot = HotSpot;

            if(propertyValues["Title"] != null)
                hotspot.Title = propertyValues["Title"].ToString();
            if (propertyValues["Location"] != null)
                hotspot.Location = (Point)propertyValues["Location"];
            if (propertyValues["PlayerMustMove"] != null)
                hotspot.PlayerMustMove = (bool)propertyValues["PlayerMustMove"];
            if (propertyValues["PlayerPositionPoint"] != null)
                hotspot.PlayerPositionPoint = (VO_Coords)propertyValues["PlayerPositionPoint"];
            if (propertyValues["PlayerMoveEndDirection"] != null)
                hotspot.PlayerMoveEndDirection = (Enums.Movement)propertyValues["PlayerMoveEndDirection"];
            if (propertyValues["ClassId"] != null && propertyValues["ClassId"] is Class)
                hotspot.ClassId = ((Class)propertyValues["ClassId"]).Id;
            return hotspot;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
            HotSpot = (VO_StageHotSpot)value;
            collection = HotSpot.GetProperties();
            return collection;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
