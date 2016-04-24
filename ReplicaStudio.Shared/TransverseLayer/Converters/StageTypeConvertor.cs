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
    public class StageTypeConvertor : TypeConverter
    {
        public VO_Stage Stage { get; set; }

        // Summary:
        //     Initializes a new System.Drawing.SizeConverter object.
        public StageTypeConvertor()
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
            VO_Stage stage = Stage;
            if(propertyValues["Title"] != null)
                stage.Title = propertyValues["Title"].ToString();
            if (propertyValues["Region"] != null)
                stage.Region = Convert.ToInt32(propertyValues["Region"]);
            if (propertyValues["Music"] != null)
                stage.Music = (VO_Music)propertyValues["Music"];
            if (propertyValues["Dimensions"] != null)
                stage.Dimensions = (Size)propertyValues["Dimensions"];
            if (propertyValues["EndingScript"] != null)
                stage.EndingScript = (VO_Script)propertyValues["EndingScript"];
            if (propertyValues["EndingFirstScript"] != null)
                stage.EndingFirstScript = (VO_Script)propertyValues["EndingFirstScript"];
            if (propertyValues["StartingScript"] != null)
                stage.StartingScript = (VO_Script)propertyValues["StartingScript"];
            if (propertyValues["StartingFirstScript"] != null)
                stage.StartingFirstScript = (VO_Script)propertyValues["StartingFirstScript"];
            return stage;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
            Stage = (VO_Stage)value;
            collection = Stage.GetProperties();
            return collection;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
