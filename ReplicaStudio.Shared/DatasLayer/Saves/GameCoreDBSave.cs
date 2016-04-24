using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Shared.DatasLayer.Saves
{
    /// <summary>
    /// Classe Data clone
    /// </summary>
    public class GameCoreDBSave
    {
        #region Données
        public List<VO_Animation> ObjectAnimations;
        public List<VO_Character> Characters;
        public List<VO_PlayableCharacter> PlayableCharacters;
        public List<VO_Item> Items;
        public List<VO_Action> Actions;
        public List<VO_Event> Events;
        public List<VO_Class> Classes;
        public List<VO_Trigger> Triggers;
        public List<VO_GlobalEvent> GlobalEvents;
        public List<VO_Script> InteractionScripts;
        public List<VO_Variable> Variables;
        public VO_Menu Menu;
        public VO_Project Project;
        public VO_Terminology Terminology;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GameCoreDBSave()
        {
            ObjectAnimations = new List<VO_Animation>();
            Characters = new List<VO_Character>();
            PlayableCharacters = new List<VO_PlayableCharacter>();
            Items = new List<VO_Item>();
            Actions = new List<VO_Action>();
            Events = new List<VO_Event>();
            Classes = new List<VO_Class>();
            Triggers = new List<VO_Trigger>();
            GlobalEvents = new List<VO_GlobalEvent>();
            InteractionScripts = new List<VO_Script>();
            Menu = new VO_Menu();
            Project = new VO_Project();
            Variables = new List<VO_Variable>();
            Terminology = new VO_Terminology();
        }
        #endregion
    }
}
