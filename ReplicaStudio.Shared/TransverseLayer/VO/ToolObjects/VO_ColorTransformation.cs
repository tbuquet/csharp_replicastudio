using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_ColorTransformation
    {
        #region Properties
        /*//NON UTILISE, PASSAGE A RGB
        /// <summary>
        /// Valeur de la teinte, valeur par défaut 0
        /// </summary>
        public float Hue { get; set; }

        /// <summary>
        /// Valeur de saturation, valeur par défaut 100
        /// </summary>
        public float Saturation { get; set; }

        /// <summary>
        /// Valeur de luminance, valeur par défaut 0
        /// </summary>
        public float Brightness { get; set; }

        /// <summary>
        /// Valeur de transparence
        /// </summary>
        public float Opacity { get; set; }*/

        /// <summary>
        /// Valeur de transparence 0-255
        /// </summary>
        public int Opacity { get; set; }

        /// <summary>
        /// Valeur de rouge -255-255
        /// </summary>
        public int Red { get; set; }

        /// <summary>
        /// Valeur de vert -255-255
        /// </summary>
        public int Green { get; set; }

        /// <summary>
        /// Valeur de bleu -255-255
        /// </summary>
        public int Blue { get; set; }

        /// <summary>
        /// Valeur de gris 0-255
        /// </summary>
        public int Grey { get; set; }
        #endregion

        #region Constructors
        public VO_ColorTransformation()
        {
            /*Hue = 0f;
            Saturation = 100.0f;
            Brightness = 0f;
            Opacity = 100.0f;*/

            Red = 0;
            Green = 0;
            Blue = 0;
            Grey = 0;
            Opacity = 255;
        }
        #endregion

        #region Methods
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this);
        }

        public override string ToString()
        {
            return Red + ";" + Green + ";" + Blue + ";" + Grey + ";" + Opacity + ";";
        }

        public bool IsUnmodifiedColor()
        {
            if (Red == 0 && Green == 0 && Blue == 0 && Grey == 0 && Opacity == 255)
                return true;
            return false;
        }
        #endregion
    }
}
