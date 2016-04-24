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
    public class VO_PlayableCharacter : VO_Base
    {
        #region Properties
        /// <summary>
        /// liste d'action
        /// </summary>
        public List<Guid> Actions
        {
            get;
            set;
        }

        /// <summary>
        /// Coordonnées de départ
        /// </summary>
        public VO_Coords CoordsCharacter { get; set; }

        /// <summary>
        /// liste d'items
        /// </summary>
        public List<Guid> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Reférence à l'ID du template character
        /// </summary>
        public Guid CharacterId
        {
            get;
            set;
        }

        /// <summary>
        /// Personnage en vie OUI/NON
        /// </summary>
        public bool ActivateLife
        {
            get;
            set;
        }

        /// <summary>
        /// PV du personnage au début du jeu
        /// </summary>
        public int PvAtStart
        {
            get;
            set;
        }

        /// <summary>
        /// Position du personnage au lancement du jeu
        /// </summary>
        public Enums.Movement StartPosition
        {
            get;
            set;
        }

        /// <summary>
        /// PV Max
        /// </summary>
        public int PvMax
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_PlayableCharacter()
        {
        }

        public VO_PlayableCharacter(Guid pID)
        {
            Id = pID;
        }
        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                GameCore.Instance.Game.PlayableCharacters.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "PlayableCharacter #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_PlayableCharacter Clone()
        {
            VO_PlayableCharacter character = (VO_PlayableCharacter)this.MemberwiseClone();
            character.Actions = new List<Guid>();
            character.Items = new List<Guid>();
            foreach (Guid guid in Actions)
            {
                character.Actions.Add(guid);
            }
            foreach (Guid guid in Items)
            {
                character.Items.Add(guid);
            }
            return character;
        }
        #endregion
    }

}