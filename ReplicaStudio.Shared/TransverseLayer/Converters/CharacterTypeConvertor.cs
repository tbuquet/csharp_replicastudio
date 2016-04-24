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
    public class CharacterTypeConvertor : TypeConverter
    {
        public VO_StageCharacter Character { get; set; }

        // Summary:
        //     Initializes a new System.Drawing.SizeConverter object.
        public CharacterTypeConvertor()
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
            VO_StageCharacter character = Character;

            if(propertyValues["Title"] != null)
                character.Title = propertyValues["Title"].ToString();
            if (propertyValues["Location"] != null)
                character.Location = (Point)propertyValues["Location"];
            if (propertyValues["PlayerMustMove"] != null)
                character.PlayerMustMove = (bool)propertyValues["PlayerMustMove"];
            if (propertyValues["PlayerPositionPoint"] != null)
                character.PlayerPositionPoint = (VO_Coords)propertyValues["PlayerPositionPoint"];
            if (propertyValues["PlayerMoveEndDirection"] != null)
                character.PlayerMoveEndDirection = (Enums.Movement)propertyValues["PlayerMoveEndDirection"];
            if (propertyValues["ClassId"] != null && propertyValues["ClassId"] is Class)
                character.ClassId = ((Class)propertyValues["ClassId"]).Id;
            return character;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
            Character = (VO_StageCharacter)value;
            collection = Character.GetProperties();
            return collection;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
