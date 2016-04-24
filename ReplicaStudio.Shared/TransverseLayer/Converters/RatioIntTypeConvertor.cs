using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.Converters
{
    public class RatioIntTypeConvertor : Int32Converter
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
            int[] array = new int[GlobalConstants.CHARACTERS_MAX_RATIO];
            for (int i = GlobalConstants.CHARACTERS_MIN_RATIO - 1; i < GlobalConstants.CHARACTERS_MAX_RATIO; i++)
            {
                array[i] = i + 1;
            }
            return new StandardValuesCollection(array);
        }
    }
}
