using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using System.Collections;

namespace ReplicaStudio.Shared.TransverseLayer.Converters
{
    public class ClassTypeConvertor : TypeConverter
    {
        Dictionary<string, Class> Classes = new Dictionary<string, Class>();

        public ClassTypeConvertor()
        {
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext
        context)
        {
            //true means show a combobox
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext
        context)
        {
            //true will limit to list. false will show the list, but allow free-formentry
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is Guid)
                if (new Guid(value.ToString()) != Guid.Empty)
                    return GameCore.Instance.GetClassById(new Guid(value.ToString())).Title;
                else
                    return value;
            if (value != null)
                return ((Class)value).Title;
            return null;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return false;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if(value != null)
                return new Class(Classes[value.ToString()].Id);
            return null;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            Class[] array = new Class[GameCore.Instance.Game.Classes.Count];
            int i = 0;
            foreach (VO_Class _class in GameCore.Instance.Game.Classes)
            {
                array[i] = new Class(_class.Id);
                if(!Classes.ContainsKey(array[i].Title))
                    Classes.Add(array[i].Title, array[i]);
                i++;
            }
            return new StandardValuesCollection(array);
        }
    }

    public class Class
    {
        public Guid Id{ get; set; }

        public string Title { get; set; }

        public Class(Guid value)
        {
            Id = value;
            Title = GameCore.Instance.GetClassById(Id).Title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
