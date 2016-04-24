using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace ReplicaStudio.Shared.TransverseLayer.Constants
{
    #region Surcharge d'Enum pour rajouter des valeurs littérales...

    /// <summary>
    /// Provides a description for an enumerated type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field,
     AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;

        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        /// <value>The description stored in the attribute.</value>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EnumDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description to store in this attribute.
        /// </param>
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }

    /// <summary>
    /// Provides a static utility object of methods and properties to interact
    /// with enumerated types.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the <see cref="DescriptionAttribute" /> of an <see cref="Enum" /> 
        /// type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum" /> type value.</param>
        /// <returns>A string containing the text of the
        /// <see cref="DescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attributes =
               (EnumDescriptionAttribute[])
             fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }

        /// <summary>
        /// Converts the <see cref="Enum" /> type to an <see cref="IList" /> 
        /// compatible object.
        /// </summary>
        /// <param name="type">The <see cref="Enum"/> type.</param>
        /// <returns>An <see cref="IList"/> containing the enumerated
        /// type value and description.</returns>
        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }
    }

    #endregion

    public class Enums
    {
        public enum EventType
        {
            Character = 1,
            Event = 2,
            Animation = 3
        }

        public enum ScriptType
        {
            Events,
            AnimationEvents,
            CharacterEvents,
            StageEvents,
            GameOverEvent,
            ItemEvents,
            GlobalEvents,
            ClassDialogs,
            Dynamic
        }

        public enum Direction
        {
            Up = 1,
            Down = 2
        }

        public enum StagePanelState
        {
            Decors = 1,
            Objects = 2,
            Characters = 3,
            HotSpots = 4,
            WalkableAreas = 5,
            Regions = 6
        }

        public enum AnimationType
        {
            CharacterFace = 1,
            CharacterAnimation = 2,
            ObjectAnimation = 3,
            IconAnimation = 4,
            TransitionAnimation= 5,
            Menu = 6
        }

        public enum ProjectPath
        {
            All,
            ObjectAnimations,
            CharAnimations,
            CharFaces,
            Decors,
            Icons,
            LifeBar,
            Menus,
            Musics,
            Resources,
            Manuals,
            Stages,
            Sounds,
            Fonts,
            GUI,
            Voice,
            Matrixes
        }

        public enum ExportState
        {
            OK,
            InProgress,
            Error
        }

        public enum Movement
        {
            [EnumDescription("Up")]
            Up = 0,
            [EnumDescription("Right")]
            Right = 1,
            [EnumDescription("Down")]
            Down = 2,
            [EnumDescription("Left")]
            Left = 3,
            [EnumDescription("Up-Right")]
            UpRight = 4,
            [EnumDescription("Down-Right")]
            DownRight = 5,
            [EnumDescription("Down-Left")]
            DownLeft = 6,
            [EnumDescription("Up-Left")]
            UpLeft = 7,
        }

        public enum StageObjectType
        {
            Animations = 1,
            Decors = 2,
            Characters = 3,
            Musics = 4,
            Stages = 5,
            HotSpots = 6,
            Walkables = 7,
            Regions = 8
        }

        public enum DrawingTools
        {
            Pointer = 1,
            Pencil = 2,
            Rectangle = 3,
            Circle = 4
        }

        public enum ComparativeOperator
        {
            [EnumDescription("Equal")]
            Equal = 0,

            [EnumDescription("More than")]
            More = 1,

            [EnumDescription("More or equal than")]
            MoreOrEqual = 2,

            [EnumDescription("Less than")]
            Less = 3,

            [EnumDescription("Less or equal than")]
            LessOrEqual = 4,

            [EnumDescription("Different than")]
            Different = 5
        }

        public enum ChangeOperator
        {
            [EnumDescription("Set")]
            Set = 1,

            [EnumDescription("Add")]
            Add = 2,

            [EnumDescription("Substract")]
            Sub = 3,

            [EnumDescription("Divide")]
            Divide = 4,

            [EnumDescription("Multiply")]
            Multiply = 5
        }

        public enum TriggerExecutionType
        {
            [EnumDescription("End of the animation")]
            EndingAnimation = 0,

            [EnumDescription("Begining of the animation")]
            BeginingAnimation = 1,
        }

        public enum TriggerEventConditionType
        {
            [EnumDescription("Click on the event")]
            ClickEvent = 0,

            [EnumDescription("Contact with the player")]
            ContactEvent = 1,

            [EnumDescription("Automatic")]
            Automatic = 2,

            [EnumDescription("Parallel process")]
            ParallelProcess = 3
        }

        public enum CharacterAnimationType
        {
            [EnumDescription("Standing")]
            Standing = 1,

            [EnumDescription("Walking")]
            Walking = 2,

            [EnumDescription("Talking")]
            Talking = 3
        }
    }
}
