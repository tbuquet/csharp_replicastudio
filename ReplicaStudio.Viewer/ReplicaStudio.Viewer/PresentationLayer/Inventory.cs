using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using Microsoft.Xna.Framework.Input;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Viewer.PresentationLayer
{
    /// <summary>
    /// Gestion de l'inventaire
    /// </summary>
    public class Inventory
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        InventoryService _Service;

        /// <summary>
        /// Référence aux menus
        /// </summary>
        VO_Menu _MenuData;

        /// <summary>
        /// Bouton Inventaire
        /// </summary>
        VO_AnimatedSprite _InventoryButton;

        /// <summary>
        /// Inventaire
        /// </summary>
        VO_AnimatedSprite _Inventory;

        /// <summary>
        /// Le contrôle peut être ouvert.
        /// </summary>
        bool _Enabled = true;

        /// <summary>
        /// Item en Drag & Drop
        /// </summary>
        VO_InventoryItem _ItemDragDrop;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        SpriteBatch _SpriteBatch;
        #endregion

        #region Properties
        /// <summary>
        /// Le contrôle peut être ouvert.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                if (!_Enabled)
                    Opened = false;
            }
        }

        /// <summary>
        /// L'inventaire est ouvert
        /// </summary>
        public bool Opened { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Inventory(SpriteBatch spriteBatch, Game game) 
        {
            _SpriteBatch = spriteBatch;
            _Service = new InventoryService();
            _MenuData = _Service.GetMenuData();
            if (_MenuData.InventoryBackButtonAnimation != Guid.Empty)
                _InventoryButton = new VO_AnimatedSprite(_MenuData.InventoryBackButtonAnimation, Guid.Empty, Enums.AnimationType.Menu, _MenuData.InventoryBackButtonCoords.X, _MenuData.InventoryBackButtonCoords.Y, ViewerEnums.ImageResourceType.Permanent);
            if (_MenuData.InventoryAnimation != Guid.Empty)
                _Inventory = new VO_AnimatedSprite(_MenuData.InventoryAnimation, Guid.Empty, Enums.AnimationType.Menu, _MenuData.InventoryBackgroundCoords.X, _MenuData.InventoryBackgroundCoords.Y, ViewerEnums.ImageResourceType.Permanent);
        }
        #endregion

        #region Methods
        #region Draw
        /// <summary>
        /// Dessine l'inventaire
        /// </summary>
        /// <param name="app"></param>
        public void  Draw(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Opened)
                {
                    Draw(_Inventory);
                    DrawInventory();
                }
            }
            Draw(_InventoryButton);
        }

        /// <summary>
        /// Dessiner sur scène
        /// </summary>
        /// <param name="animatedObjectToDraw"></param>
        public virtual void Draw(VO_AnimatedSprite animatedObjectToDraw)
        {
            //Cas particulier de l'animation
            if (animatedObjectToDraw != null && animatedObjectToDraw.Sprite != null && animatedObjectToDraw.Sprite.Image != null)
            {
                //Si l'animation est conforme, on l'affiche
                _SpriteBatch.Draw(animatedObjectToDraw.Sprite.Image, animatedObjectToDraw.Sprite.Destination, animatedObjectToDraw.Sprite.Source, Color.White);
                animatedObjectToDraw.GetNextSprite();
            }
            else if (animatedObjectToDraw != null && animatedObjectToDraw.Sprite != null && animatedObjectToDraw.Sprite.Image != null)
            {
                //Si les ressources ont été détruites, on regénére l'animation
                animatedObjectToDraw.RegenerateAnim();
                _SpriteBatch.Draw(animatedObjectToDraw.Sprite.Image, animatedObjectToDraw.Sprite.Destination, animatedObjectToDraw.Sprite.Source, Color.White);
            }
        }

        /// <summary>
        /// Dessine l'inventaire
        /// </summary>
        private void DrawInventory()
        {
            Guid[,] table = PlayableCharactersManager.CurrentPlayerCharacter.Items;
            for (int i = 0; i < _MenuData.GridHeight; i++)
            {
                for (int j = 0; j < _MenuData.GridWidth; j++)
                {
                    if (table[j, i] != Guid.Empty)
                    {
                        if (_ItemDragDrop != null && table[j, i] == _ItemDragDrop.ItemId)
                            continue;
                        else if (ActionManager.ItemAsAction && ActionManager.CurrentLinkedItem.Id == table[j, i])
                            continue;
                        else if (ActionManager.ItemInUse == table[j, i])
                            continue;

                        //On affiche les items
                        int x = _MenuData.InventoryCoords.X + j * _MenuData.ItemWidth;
                        int y = _MenuData.InventoryCoords.Y + i * _MenuData.ItemHeight;
                        VO_AnimatedSprite item = ItemManager.GetItem(table[j, i], ViewerEnums.TypeIcon.Inventory);
                        item.SetPosition(x, y);
                        Draw(item);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Switch l'état d'ouverture de l'inventaire
        /// </summary>
        public void SwitchInventory()
        {
            if (Opened)
                Opened = false;
            else
                Opened = true;
        }

        /// <summary>
        /// Test si la souris est sur le contrôle du bouton inventaire
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public bool GetInventoryButtonEvent(Point mousePos)
        {
            if (_InventoryButton == null)
                return false;
            return _Service.GetInventoryButtonEvent(mousePos, new Point(_MenuData.InventoryBackButtonCoords.X, _MenuData.InventoryBackButtonCoords.Y), (int)_InventoryButton.Width, (int)_InventoryButton.Height);
        }

        /// <summary>
        /// Test si la souris est sur le contrôle d'un item dans l'inventaire
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public VO_InventoryItem GetInventoryItemEvent(Point mousePos)
        {
            return _Service.GetInventoryItemEvent(mousePos, new Point(_MenuData.InventoryCoords.X, _MenuData.InventoryCoords.Y), _MenuData.GridWidth, _MenuData.GridHeight, _MenuData.ItemWidth, _MenuData.ItemHeight);
        }
        #endregion

        #region Eventhandlers
        public void MouseMove(MouseState e)
        {
            VO_InventoryItem item = GetInventoryItemEvent(new Point(e.X, e.Y));
            if (GetInventoryButtonEvent(new Point(e.X, e.Y)) || (item != null && item.ItemId != Guid.Empty))
                ActionManager.ClickedState = true;
            else
                ActionManager.ClickedState = false;
        }

        public void MousePress(MouseState e)
        {
            if (e.LeftButton == ButtonState.Pressed)
            {
                #region Quitter l'inventaire
                //Click en dehors de l'inventaire
                if (!_Service.MouseIsInGrid(new Point(e.X, e.Y), new Point(_MenuData.InventoryCoords.X,_MenuData.InventoryCoords.Y), _MenuData.GridWidth, _MenuData.GridHeight, _MenuData.ItemWidth, _MenuData.ItemHeight))
                {
                    Opened = false;
                    return;
                }

                //Click sur le bouton de l'inventaire
                if (GetInventoryButtonEvent(new Point(e.X, e.Y)))
                    SwitchInventory();
                #endregion

                VO_InventoryItem item = GetInventoryItemEvent(new Point(e.X, e.Y));
                if (ActionManager.ItemAsAction && item != null)
                {
                    VO_InventoryItem item2 = _Service.GetItemFromGrid(ActionManager.CurrentLinkedItem.Id, _MenuData.GridWidth, _MenuData.GridHeight);
                    if (item.ItemId != Guid.Empty && item2.ItemId != Guid.Empty && item2.ItemId != item.ItemId)
                    {
                        //Interaction d'objets
                        VO_Item itemObj = GameCore.Instance.GetItemById(item.ItemId);
                        VO_ItemInteraction itemInteraction = itemObj.ItemInteraction.Find(p => p.AssociatedItem == item2.ItemId);
                        if (itemInteraction != null)
                        {
                            VO_Script scriptObj = GameCore.Instance.GetInteractionScriptsById(itemInteraction.Script);
                            VO_RunningScript script = new VO_RunningScript();
                            script.Id = scriptObj.Id;
                            script.Lines = scriptObj.Lines;

                            ScriptManager.CurrentScript = script;
                        }
                    }
                    else
                    {
                        _Service.SwitchItemPlaces(item, item2);
                        _ItemDragDrop = null;
                        ActionManager.UnloadItem();
                    }
                }
                else if (item != null && item.ItemId != Guid.Empty)
                {
                    if (ActionManager.CurrentActionIsGo())
                    {
                        _ItemDragDrop = item;
                        ActionManager.SetCurrentItem(item.ItemId);
                    }
                    else
                    {
                        //Script sur action
                        VO_Item itemObj = GameCore.Instance.GetItemById(item.ItemId);
                        VO_ActionOnItemScript actionScript = itemObj.Scripts.Find(p => p.Id == ActionManager.CurrentAction.Id);
                        if (actionScript != null)
                        {
                            VO_RunningScript script = new VO_RunningScript();
                            script.Id = actionScript.Script.Id;
                            script.Lines = actionScript.Script.Lines;
                            ScriptManager.CurrentScript = script;
                        }
                    }
                }
            }
            else if (e.RightButton == ButtonState.Pressed)
            {
                PlayableCharactersManager.CurrentPlayerCharacter.ChangeNextAction();
            }
        }

        /// <summary>
        /// Bouton souris relaché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeftReleased(MouseState e)
        {
            if (_ItemDragDrop != null)
            {
                VO_InventoryItem newItem = GetInventoryItemEvent(new Point(e.X, e.Y));
                if (newItem == null)
                {
                    ActionManager.UnloadItem();
                }
                else if (newItem.ItemId != _ItemDragDrop.ItemId)
                {
                    ActionManager.UnloadItem();
                    _Service.SwitchItemPlaces(_ItemDragDrop, newItem);
                }
                _ItemDragDrop = null;
            }
        }
        #endregion
    }
}
