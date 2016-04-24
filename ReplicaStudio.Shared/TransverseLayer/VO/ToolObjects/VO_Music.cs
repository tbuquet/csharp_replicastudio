using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Converters;
using System.ComponentModel;
using System.Reflection;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    [TypeConverter(typeof(MusicTypeConvertor))]
    public class VO_Music
    {
        #region Properties
        [TypeConverter(typeof(MusicStringTypeConvertor))]
        [DescriptionAttribute("Music that will play during the stage")]
        /// <summary>
        /// Nom du fichier
        /// </summary>
        public string Filename { get; set; }

        [TypeConverter(typeof(FrequencyIntTypeConvertor))]
        [DescriptionAttribute("Frequency of the music playback. 100 is the default value.")]
        /// <summary>
        /// Frequency
        /// </summary>
        public int Frequency { get; set; }
        #endregion

        #region Constructor
        public VO_Music()
        {
        }
        #endregion

        #region Methods
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this);
        }

        public override string ToString()
        {
            return Filename;
        }
        #endregion
    }
}
