using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Classe de gestion des items et curseurs d'items
    /// </summary>
    public class ItemManager
    {
        #region Members
        /// <summary>
        /// Liste des sprites des actions
        /// </summary>
        private static Dictionary<Guid, VO_AnimatedSprite[]> _ItemsSprites;
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// Charger actions
        /// </summary>
        public static void LoadItems()
        {
            _ItemsSprites = new Dictionary<Guid, VO_AnimatedSprite[]>();

            foreach (VO_Item item in GameCore.Instance.Game.Items)
            {
                VO_AnimatedSprite[] animItem = new VO_AnimatedSprite[3];
                animItem[0] = new VO_AnimatedSprite(item.InventoryIcon, new Guid(), Enums.AnimationType.IconAnimation, 0, 0, ViewerEnums.ImageResourceType.Permanent);
                animItem[1] = new VO_AnimatedSprite(item.Icon, new Guid(), Enums.AnimationType.IconAnimation, 0, 0, ViewerEnums.ImageResourceType.Permanent);
                animItem[2] = new VO_AnimatedSprite(item.ActiveIcon, new Guid(), Enums.AnimationType.IconAnimation, 0, 0, ViewerEnums.ImageResourceType.Permanent);
                _ItemsSprites.Add(item.Id, animItem);
            }
        }

        /// <summary>
        /// Renvoie une anim d'item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static VO_AnimatedSprite GetItem(Guid id, ViewerEnums.TypeIcon type)
        {
            switch (type)
            {
                case ViewerEnums.TypeIcon.Inventory:
                    return _ItemsSprites[id][0];
                case ViewerEnums.TypeIcon.Icon:
                    return _ItemsSprites[id][1];
                case ViewerEnums.TypeIcon.ActiveIcon:
                    return _ItemsSprites[id][2];
                default:
                    return _ItemsSprites[id][0];
            }
        }

        /// <summary>
        /// Renvoie une anim d'item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static VO_AnimatedSprite[] GetFullItem(Guid id)
        {
            return _ItemsSprites[id];
        }

        /// <summary>
        /// set la position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /*public static void SetPosition(int x, int y)
        {
            foreach (VO_AnimatedSprite anim in _CurrentActionSprite)
            {
                anim.SetPosition(x, y);
            }
        }*/
        #endregion
    }
}
