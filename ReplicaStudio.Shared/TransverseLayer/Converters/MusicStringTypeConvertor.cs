using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Shared.TransverseLayer.Converters
{
    public class MusicStringTypeConvertor : StringConverter
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
            string[] files = Directory.GetFiles(PathTools.GetProjectPath(Constants.Enums.ProjectPath.Musics));
            string[] newFiles = new string[files.Length + 1];
            newFiles[0] = string.Empty;
            for (int i = 1; i < newFiles.Length; i++)
            {
                newFiles[i] = Path.GetFileName(files[i - 1]);
            }
            return new StandardValuesCollection(newFiles);
        }
    }
}
