using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;
using System.Drawing;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Character : VO_Base
    {
        #region Properties
        /// <summary>
        /// Animations du perso
        /// </summary>
        public List<VO_Animation> Animations
        {
            get;
            set;
        }

        /// <summary>
        /// vitesse mouvement valeur comprise entre 1 et 100
        /// </summary>
        public int Speed
        {
            get;
            set;
        }

        /// <summary>
        /// animation de type face
        /// </summary>
        public Guid Face
        {
            get;
            set;
        }

        /// <summary>
        /// animation de type face
        /// </summary>
        public Guid TalkingFace
        {
            get;
            set;
        }

        /// <summary>
        /// A un personne est attribué une couleur
        /// </summary>
        public VO_Color DialogColor
        {
            get;
            set;
        }

        /// <summary>
        /// Animation d'arrêt
        /// </summary>
        public Guid StandingAnim
        {
            get;
            set;
        }

        /// <summary>
        /// Animation de déplacement
        /// </summary>
        public Guid WalkingAnim
        {
            get;
            set;
        }

        /// <summary>
        /// Animation de discussion
        /// </summary>
        public Guid TalkingAnim
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_Character()
        {
        }

        public VO_Character(Guid pID)
        {
            Id = pID;
        }
        #endregion

        #region Methods
        public VO_Animation GetAnimationById(Guid animation)
        {
            return Animations.Find(p => p.Id == animation);
        }

        public List<VO_Base> GetAnimations()
        {
            List<VO_Base> items = new List<VO_Base>();
            foreach (VO_Base item in Animations)
            {
                items.Add(item);
            }
            return items;
        }

        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.Characters.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Character #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Character Clone()
        {
            VO_Character character = (VO_Character)this.MemberwiseClone();
            character.Animations = new List<VO_Animation>();
            foreach (VO_Animation anim in Animations)
            {
                character.Animations.Add(anim.Clone());
            }
            return character;
        }
        #endregion
    }

}