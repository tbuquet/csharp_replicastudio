using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.Constants;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Classe de gestion des curseurs d'actions
    /// </summary>
    public class ActionManager
    {
        #region Members
        /// <summary>
        /// Liste des sprites des actions
        /// </summary>
        private static Dictionary<Guid, VO_AnimatedSprite[]> _ActionSprites;

        /// <summary>
        /// Action courante
        /// </summary>
        private static VO_Action _CurrentAction;

        /// <summary>
        /// Item courant
        /// </summary>
        private static VO_Item _CurrentItem;

        /// <summary>
        /// Sprites courants
        /// </summary>
        private static VO_AnimatedSprite[] _CurrentActionSprite;

        /// <summary>
        /// Action GO
        /// </summary>
        private static VO_Action _GoAction;

        /// <summary>
        /// Action USE
        /// </summary>
        private static VO_Action _UseAction;

        /// <summary>
        /// Sprites Action GO
        /// </summary>
        private static VO_AnimatedSprite[] _GoActionSprite;

        /// <summary>
        /// Sprites Action USE
        /// </summary>
        private static VO_AnimatedSprite[] _UseActionSprite;
        #endregion

        #region Properties
        /// <summary>
        /// Yes pour piocher dans les items
        /// </summary>
        public static bool ItemAsAction { get; set; }

        /// <summary>
        /// Item en mémoire
        /// </summary>
        public static Guid ItemInUse { get; set; }

        /// <summary>
        /// Bouton en mode cliqué
        /// </summary>
        public static bool ClickedState { get; set; }

        /// <summary>
        /// Action courante
        /// </summary>
        public static VO_Action CurrentAction { get { return _CurrentAction; } }

        /// <summary>
        /// Sprites courants
        /// </summary>
        public static VO_AnimatedSprite[] CurrentActionSprite { get { return _CurrentActionSprite; } }

        /// <summary>
        /// Item lié
        /// </summary>
        public static VO_Item CurrentLinkedItem { get { return _CurrentItem; } }
        #endregion

        #region Methods
        /// <summary>
        /// Charger actions
        /// </summary>
        public static void LoadActions()
        {
            _ActionSprites = new Dictionary<Guid, VO_AnimatedSprite[]>();

            foreach (VO_Action action in GameCore.Instance.Game.Actions)
            {
                VO_AnimatedSprite[] animItem = new VO_AnimatedSprite[3];
                if (action.InventoryIcon != Guid.Empty)
                    animItem[0] = new VO_AnimatedSprite(action.InventoryIcon, new Guid(), Enums.AnimationType.IconAnimation, 0, 0, ViewerEnums.ImageResourceType.Permanent);
                if (action.Icon != Guid.Empty)
                    animItem[1] = new VO_AnimatedSprite(action.Icon, new Guid(), Enums.AnimationType.IconAnimation, 0, 0, ViewerEnums.ImageResourceType.Permanent);
                if (action.ActiveIcon != Guid.Empty)
                    animItem[2] = new VO_AnimatedSprite(action.ActiveIcon, new Guid(), Enums.AnimationType.IconAnimation, 0, 0, ViewerEnums.ImageResourceType.Permanent);
                _ActionSprites.Add(action.Id, animItem);
                if (action.GoAction)
                {
                    _GoAction = action;
                    _GoActionSprite = animItem;
                }
                else if (action.UseAction)
                {
                    _UseAction = action;
                    _UseActionSprite = animItem;
                }
            }
        }

        /// <summary>
        /// Teste si l'action courante est l'action Go
        /// </summary>
        /// <returns>True si vérifié</returns>
        public static bool CurrentActionIsGo()
        {
            if (CurrentAction.Id == _GoAction.Id)
                return true;
            return false;
        }

        /// <summary>
        /// Teste si l'action courante est l'action Use
        /// </summary>
        /// <returns>True si vérifié</returns>
        public static bool CurrentActionIsUse()
        {
            if (CurrentAction.Id == _UseAction.Id)
                return true;
            return false;
        }

        /// <summary>
        /// Change d'action
        /// </summary>
        /// <param name="action">Id de l'action demandée</param>
        public static void SetCurrentAction(Guid action)
        {
            ItemAsAction = false;
            int x = (int)_CurrentActionSprite[0].Sprite.Position.X;
            int y = (int)_CurrentActionSprite[0].Sprite.Position.Y;
            _CurrentAction = GameCore.Instance.GetActionById(action);
            _CurrentItem = null;
            _CurrentActionSprite = _ActionSprites[action];
            SetPosition(x, y);
        }

        /// <summary>
        /// Change d'item
        /// </summary>
        /// <param name="item">Id de l'item demandé</param>
        public static void SetCurrentItem(Guid item)
        {
            ItemAsAction = true;
            ItemInUse = item;
            int x = (int)_CurrentActionSprite[0].Sprite.Position.X;
            int y = (int)_CurrentActionSprite[0].Sprite.Position.Y;
            _CurrentAction = _UseAction;
            _CurrentItem = GameCore.Instance.GetItemById(item);
            _CurrentActionSprite = ItemManager.GetFullItem(item);
            SetPosition(x, y);
        }

        /// <summary>
        /// Désactive l'item en mémoire
        /// </summary>
        public static void UnloadItem()
        {
            ItemInUse = Guid.Empty;
            SetCurrentActionToGo();
        }

        /// <summary>
        /// Change d'action pour le go
        /// </summary>
        public static void SetCurrentActionToGo()
        {
            int x = 0;
            int y = 0;
            ItemAsAction = false;

            if (_CurrentActionSprite != null)
            {
                x = (int)_CurrentActionSprite[1].Sprite.Position.X;
                y = (int)_CurrentActionSprite[1].Sprite.Position.Y;
            }
            _CurrentItem = null;
            _CurrentAction = _GoAction;
            _CurrentActionSprite = _GoActionSprite;
            SetPosition(x, y);
        }

        /// <summary>
        /// set la position
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public static void SetPosition(int x, int y)
        {
            if (_CurrentActionSprite != null)
            {
                foreach (VO_AnimatedSprite anim in _CurrentActionSprite)
                {
                    VO_Animation animation = GameCore.Instance.GetIconById(anim.AnimationId);

                    anim.SetPosition(x - animation.OriginPoint.X, y - animation.OriginPoint.Y);
                }
            }
        }
        #endregion
    }
}
