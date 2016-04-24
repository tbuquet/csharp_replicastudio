using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Game
    {
        #region Properties
        public List<VO_Animation> ObjectAnimations { get; set; }
        public List<VO_Animation> CharFacesAnimations { get; set; }
        public List<VO_Animation> IconsAnimations { get; set; }
        public List<VO_Animation> MenusAnimations { get; set; }
        public VO_Project Project { get; set; }
        public VO_Menu Menu { get; set; }
        public List<VO_Item> Items { get; set; }
        public List<VO_Action> Actions { get; set; }
        public List<VO_Character> Characters { get; set; }
        public List<VO_PlayableCharacter> PlayableCharacters { get; set; }
        public List<VO_GlobalEvent> GlobalEvents { get; set; }
        public List<VO_Stage> Stages { get; set; }
        public List<VO_Class> Classes { get; set; }
        public List<VO_Trigger> Triggers { get; set; }
        public List<VO_Script> InteractionScripts { get; set; }
        public List<VO_Variable> Variables { get; set; }
        public VO_Terminology Terminology { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public VO_Game()
        {
        }
        #endregion
    }
    
}
