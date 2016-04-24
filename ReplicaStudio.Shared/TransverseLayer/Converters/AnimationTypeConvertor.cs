using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.Drawing;

namespace ReplicaStudio.Shared.TransverseLayer.Converters
{
    public class AnimationTypeConvertor : TypeConverter
    {
        public VO_StageAnimation Animation { get; set; }

        // Summary:
        //     Initializes a new System.Drawing.SizeConverter object.
        public AnimationTypeConvertor()
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
            VO_StageAnimation decor = Animation;
            if(propertyValues["Title"] != null)
                decor.Title = propertyValues["Title"].ToString();
            if (propertyValues["Location"] != null)
                decor.Location = (Point)propertyValues["Location"];
            return decor;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
            Animation = (VO_StageAnimation)value;
            collection = Animation.GetProperties();
            return collection;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
