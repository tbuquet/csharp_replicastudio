using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ReplicaStudio.Shared.TransverseLayer.Converters
{
    public class RegionIntTypeConvertor : Int32Converter
    {
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

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            int[] array = new int[200];
            for (int i = 0; i < 200; i++)
            {
                array[i] = i + 1;
            }
            return new StandardValuesCollection(
            array);
        }
    }
}
