using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Shared.DatasLayer.Saves
{
    /// <summary>
    /// Classe Data clone d'animations
    /// </summary>
    public class GameCoreAnimationSave
    {
        #region Données
        public List<VO_Animation> ObjectAnimations;
        public List<VO_Animation> CharFaces;
        public List<VO_Animation> CharAnimations;
        public Guid CharacterId;
        public List<VO_Animation> Icons;
        public List<VO_Animation> Menus;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public GameCoreAnimationSave()
        {
            ObjectAnimations = new List<VO_Animation>();
            CharFaces = new List<VO_Animation>();
            CharAnimations = new List<VO_Animation>();
            Icons = new List<VO_Animation>();
            Menus = new List<VO_Animation>();
        }
        #endregion
    }
}
