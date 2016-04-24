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
    public class MusicTypeConvertor : TypeConverter
    {
        // Summary:
        //     Initializes a new System.Drawing.SizeConverter object.
        public MusicTypeConvertor()
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
            if (value != null)
                return value.ToString();
            else
                return value;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            VO_Music music = new VO_Music();
            if(propertyValues["Filename"] != null)
                music.Filename = propertyValues["Filename"].ToString();
            music.Frequency = Convert.ToInt32(propertyValues["Frequency"]);
            return music;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
            VO_Music pd = (VO_Music)value;
            collection = pd.GetProperties();
            return collection;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
