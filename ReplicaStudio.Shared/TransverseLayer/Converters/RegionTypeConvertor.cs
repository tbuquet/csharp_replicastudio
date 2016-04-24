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
    public class RegionTypeConvertor : TypeConverter
    {
        public VO_StageRegion Region { get; set; }

        // Summary:
        //     Initializes a new System.Drawing.SizeConverter object.
        public RegionTypeConvertor()
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
            VO_StageRegion region = Region;

            if(propertyValues["Title"] != null)
                region.Title = propertyValues["Title"].ToString();
            if (propertyValues["Location"] != null)
                region.Location = (Point)propertyValues["Location"];
            if (propertyValues["Ratio"] != null)
                region.Ratio = Convert.ToDouble(propertyValues["Ratio"]);
            return region;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
            Region = (VO_StageRegion)value;
            collection = Region.GetProperties();
            return collection;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
