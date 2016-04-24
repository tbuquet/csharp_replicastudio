using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Item : VO_Base
    {
        #region Properties
        /// <summary>
        /// Description de l'item
        /// </summary>
        public string Description
        {
            get;
            set;
        }
        /// <summary>
        /// Icone d'inventaire
        /// </summary>
        public Guid InventoryIcon
        {
            get;
            set;
        }
        /// <summary>
        /// Icone avec souris inactive
        /// </summary>
        public Guid Icon
        {
            get;
            set;
        }
        /// <summary>
        /// Icone avec souris active
        /// </summary>
        public Guid ActiveIcon
        {
            get;
            set;
        }

        /// <summary>
        /// Interaction entre les items
        /// </summary>
        public List<VO_ItemInteraction> ItemInteraction
        {
            get;
            set;
        }

        /// <summary>
        /// Listes des scripts sur action
        /// </summary>
        public List<VO_ActionOnItemScript> Scripts
        {
            get;
            set;
        }
        #endregion

        #region Constructors

        public VO_Item()
        {
        }

        public VO_Item(Guid pID)
        {
            Id = pID;
        }

        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                foreach (VO_ItemInteraction itemInteraction in this.ItemInteraction)
                    GameCore.Instance.RemoveInteractionScriptById(itemInteraction.Script);
                GameCore.Instance.Game.Items.Remove(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Item #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Item Clone()
        {
            VO_Item newItem = (VO_Item)this.MemberwiseClone();
            newItem.ItemInteraction = new List<VO_ItemInteraction>();
            foreach (VO_ItemInteraction interaction in ItemInteraction)
            {
                newItem.ItemInteraction.Add(interaction);
            }
            return newItem;
        }
        #endregion
    }
}
