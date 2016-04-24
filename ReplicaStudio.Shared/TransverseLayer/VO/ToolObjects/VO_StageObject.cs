using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ReplicaStudio.Shared.DatasLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.ComponentModel;
using ReplicaStudio.Shared.TransverseLayer.Converters;
using System.Drawing.Design;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_StageObject : VO_Base
    {
        #region Properties
        [DisplayName("Player position")]
        [Category("General")]
        [Description("Position where the player will go before executing any event.")]
        /// <summary>
        /// Point de départ du personnage
        /// </summary>
        public VO_Coords PlayerPositionPoint { get; set; }

        [DisplayName("Player must move")]
        [Category("General")]
        [Description("Indicate if the player will have to move before executing any event.")]
        /// <summary>
        /// Indique si le joueur devra se déplacer avant d'effectuer l'action
        /// </summary>
        public bool PlayerMustMove { get; set; }

        [DisplayName("Player direction after move")]
        [Category("General")]
        [Description("Indicate if the player will have to change his direction before executing any event.")]
        /// <summary>
        /// Indique la direction dont pointera le perso à la fin du déplacement
        /// </summary>
        public Enums.Movement PlayerMoveEndDirection { get; set; }

        [DisplayName("Class")]
        [Category("General")]
        [Description("Class of the object")]
        [TypeConverter(typeof(ClassTypeConvertor))]
        /// <summary>
        /// Classe associée
        /// </summary>
        public Guid ClassId { get; set; }

        [DescriptionAttribute("Location of the object")]
        [Category("General")]
        /// <summary>
        /// Location
        /// </summary>
        public Point Location { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Taille
        /// </summary>
        public Size Size { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Id du stage associé
        /// </summary>
        public Guid Stage { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Id du calque associé
        /// </summary>
        public Guid Layer { get; set; }

        [ReadOnly(true)]
        [Category("General")]
        [DescriptionAttribute("Type of the object")]
        [DisplayName("Object type")]
        /// <summary>
        /// Type d'objet
        /// </summary>
        public Enums.StageObjectType ObjectType { get; set; }

        [Browsable(false)]
        /// <summary>
        /// Event
        /// </summary>
        public VO_Event Event { get; set; }
        #endregion

        #region Constructors
        public VO_StageObject()
        {
        }
        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                VO_Layer vSelectedLayer = null;
                foreach (VO_Layer vLayer in GameCore.Instance.GetStageById(Stage).ListLayers)
                    if (vLayer.Id == Layer)
                        vSelectedLayer = vLayer;

                switch (ObjectType)
                {
                    case Enums.StageObjectType.Decors:
                        vSelectedLayer.ListDecors.Remove((VO_StageDecor)this);
                        break;
                    case Enums.StageObjectType.Animations:
                        vSelectedLayer.ListAnimations.Remove((VO_StageAnimation)this);
                        break;
                    case Enums.StageObjectType.Characters:
                        GameCore.Instance.GetStageById(Stage).ListCharacters.Remove((VO_StageCharacter)this);
                        break;
                    case Enums.StageObjectType.HotSpots:
                        GameCore.Instance.GetStageById(Stage).ListHotSpots.Remove((VO_StageHotSpot)this);
                        break;
                    case Enums.StageObjectType.Walkables:
                        vSelectedLayer.ListWalkableAreas.Remove((VO_StageWalkable)this);
                        break;
                    case Enums.StageObjectType.Regions:
                        GameCore.Instance.GetStageById(Stage).ListRegions.Remove((VO_StageRegion)this);
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "StageObjet #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }
        #endregion
    }
}
