using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    [TypeConverter(typeof(StageTypeConvertor))]
    public class VO_Stage : VO_Base
    {
        #region Properties
        [DescriptionAttribute("Stage dimensions, in pixel")]
        [Category("General")]
        [ReadOnly(true)]
        [Browsable(false)]
        /// <summary>
        /// Dimension de la scène
        /// </summary>
        public Size Dimensions{ get; set; }

        [Browsable(false)]
        /// <summary>
        /// Liste de Calques
        /// </summary>
        public List<VO_Layer> ListLayers{ get; set; }

        [Browsable(false)]
        /// <summary>
        /// Liste de characters
        /// </summary>
        public List<VO_StageCharacter> ListCharacters { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Liste des hotspots
        /// </summary>
        public List<VO_StageHotSpot> ListHotSpots { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Liste des regions
        /// </summary>
        public List<VO_StageRegion> ListRegions { get; set; }

        [DescriptionAttribute("Music that will play during the stage")]
        [Category("General")]
        /// <summary>
        /// Musique jouée sur scène
        /// </summary>
        public VO_Music Music { get; set; }

        [DescriptionAttribute("Percentage of zoom of the standard region")]
        [Category("General")]
        [TypeConverter(typeof(RegionIntTypeConvertor))]
        /// <summary>
        /// Region
        /// </summary>
        public int Region { get; set; }

        [DescriptionAttribute("Color modifications applied on the stage")]
        [Category("General")]
        [Browsable(false)]
        /// <summary>
        /// Teinte générale de la scène
        /// </summary>
        public VO_ColorTransformation Color { get; set; }

        [DescriptionAttribute("Script that is launched when the player enter in the stage")]
        [DisplayName("Starting Script")]
        [Category("Scripts")]
        /// <summary>
        /// Script de démarrage de la scène
        /// </summary>
        public VO_Script StartingScript { get; set; }

        [DescriptionAttribute("Script that is launched when the player leave in the stage")]
        [DisplayName("Ending Script")]
        [Category("Scripts")]
        /// <summary>
        /// Script de fin de scène
        /// </summary>
        public VO_Script EndingScript { get; set; }

        [DescriptionAttribute("Script that is launched when the player enter in the stage first time")]
        [DisplayName("Starting Script First time")]
        [Category("Scripts")]
        /// <summary>
        /// Script de démarrage de la scène seulement la première fois
        /// </summary>
        public VO_Script StartingFirstScript { get; set; }

        [DescriptionAttribute("Script that is launched when the player leave in the stage the first time")]
        [DisplayName("Ending Script First time")]
        [Category("Scripts")]
        /// <summary>
        /// Script de fin de scène seulement la première fois
        /// </summary>
        public VO_Script EndingFirstScript { get; set; }
        #endregion

        #region Constructors
        public VO_Stage()
        {
        }

        public VO_Stage(Guid guid)
        {
            Id = guid;
        }
        #endregion

        #region Methods
        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(this);
            PropertyDescriptorCollection output = new PropertyDescriptorCollection(null);
            foreach (PropertyDescriptor descr in collection)
            {
                if (descr.Name != "Id" && descr.IsBrowsable)
                    output.Add(descr);
            }
            return output;
        }

        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.Stages.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Stage #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }
        #endregion
    }
}
