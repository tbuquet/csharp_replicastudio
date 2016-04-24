using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Drawing;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    #region Class
    [Serializable]
    public class VO_Page
    {
        #region Properties

        #region Event Type

        public Enums.EventType PageEventType = Enums.EventType.Event;

        #endregion

        #region Infos Page
        public Guid PageId = Guid.Empty;
        public string PageName = String.Empty;
        public int PageNumber = 0;
        #endregion

        #region Conditions d'apparition
        /// <summary>
        /// Les conditions d'évènements peuvent êtres cumulatives
        /// </summary>

        #region Trigger
        /// <summary>
        /// Trigger Condition Event
        /// </summary>
        public bool TriggerActivated = false;
        public Guid TriggerId = Guid.Empty;
        #endregion

        #region Variable
        /// <summary>
        /// Variable Condition Event
        /// </summary>
        public bool VariableActivated = false;
        public Guid VariableId = Guid.Empty;
        public int VariableValue = 0;
        #endregion

        #region Character
        /// <summary>
        /// Character Condition Event
        /// </summary>
        public bool CharacterActivated = false;
        public Guid CharacterId = Guid.Empty;
        #endregion
        #endregion

        #region Conditions d'executions

        #region Item

        public bool ItemActivated = false;
        public Guid ItemId = Guid.Empty;
        public Guid ItemUsedId = Guid.Empty;

        #endregion

        #region Action

        public bool ActionActivated = false;

        /// <summary>
        /// Dummy Implementation of Action Selection
        /// </summary>
        public Guid ActionId = Guid.Empty;
        public Guid ActionUsedId = Guid.Empty;

        #endregion

        #endregion

        #region Animation Condition
        public Enums.TriggerExecutionType TriggerExecution = Enums.TriggerExecutionType.EndingAnimation;
        public int AnimationFrequency { get; set; }
        public bool AnimationFrozenAtStart { get; set; }
        #endregion

        #region Event Condition
        public Enums.TriggerEventConditionType TriggerCondition = Enums.TriggerEventConditionType.ClickEvent;
        #endregion

        #region Character Condition
        public Enums.Movement CharacterDirection { get; set; }
        public int CharacterSpeed { get; set; }
        public int CharacterAnimationFrequency { get; set; }
        public Guid CharacterStandingType { get; set; }
        public Guid CharacterWalkingType { get; set; }
        public Guid CharacterTalkingType { get; set; }
        #endregion

        public Point PlayerPositionPoint = new Point();

        public VO_Script Script { get; set; }
        #endregion

        #region Constructors
        public VO_Page()
        {

        }

        public VO_Page(Guid IdTo)
        {
            PageId = IdTo;
        }
        #endregion

        public VO_Page Clone()
        {
            VO_Page NewPage = (VO_Page)this.MemberwiseClone();

            VO_Script NewScript = NewPage.Script.Clone();
            NewPage.Script = NewScript;

            return NewPage;
        }
    }
    #endregion
}
