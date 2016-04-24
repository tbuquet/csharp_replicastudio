using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Viewer.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.TransverseLayer.VO;

namespace ReplicaStudio.Viewer.ServiceLayer
{
    public class InventoryService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au Business
        /// </summary>
        InventoryBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public InventoryService()
        {
            _Business = new InventoryBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renvoie les informations du menu
        /// </summary>
        /// <returns>VO_Menu</returns>
        public VO_Menu GetMenuData()
        {
            VO_Menu menu = null;

            RunServiceTask(delegate
            {
                menu = _Business.GetMenuData();
            }, ViewerErrors.STAGE_LOAD_MENU);

            return menu;
        }

        /// <summary>
        /// Check si le curseur est sur le bouton inventaire
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool GetInventoryButtonEvent(Point point, Point buttonPoint, int buttonWidth, int buttonHeight)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.GetInventoryButtonEvent(point, buttonPoint, buttonWidth, buttonHeight);
            }, ViewerErrors.STAGE_LOAD_MENU, false, point.ToString(), buttonPoint.ToString(), buttonWidth.ToString(), buttonHeight.ToString());

            return output;
        }

        /// <summary>
        /// Teste si la souris est dans la grille
        /// </summary>
        /// <param name="mousePos">Position souris</param>
        /// <returns>True si vérifié, false sinon</returns>
        public bool MouseIsInGrid(Point mousePos, Point tablePoint, int gridWidth, int gridHeight, int itemWidth, int itemHeight)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.MouseIsInGrid(mousePos, tablePoint, gridWidth, gridHeight, itemWidth, itemHeight);
            }, ViewerErrors.STAGE_LOAD_MENU, mousePos.ToString(), tablePoint.ToString(), gridWidth.ToString(), gridHeight.ToString(), itemWidth.ToString(), itemHeight.ToString());

            return output;
        }

        /// <summary>
        /// Check si le curseur est sur le bouton inventaire
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public VO_InventoryItem GetInventoryItemEvent(Point point, Point tablePoint, int gridWidth, int gridHeight, int itemWidth, int itemHeight)
        {
            VO_InventoryItem guid = null;

            RunServiceTask(delegate
            {
                guid = _Business.GetInventoryItemEvent(point, tablePoint, gridWidth, gridHeight, itemWidth, itemHeight);
            }, ViewerErrors.STAGE_LOAD_MENU, false, point.ToString(), tablePoint.ToString(), gridWidth.ToString(), gridHeight.ToString(), itemWidth.ToString(), itemHeight.ToString());

            return guid;
        }

        /// <summary>
        /// Retire un élément de la grille
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void RemoveItemInGrid(VO_InventoryItem item)
        {
            RunServiceTask(delegate
            {
                _Business.RemoveItemInGrid(item);
            }, ViewerErrors.STAGE_LOAD_MENU, item.ToString());
        }

        /// <summary>
        /// Retire un élément de la grille
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void RemoveItemInGrid(Guid item, int gridWidth, int gridHeight)
        {
            RunServiceTask(delegate
            {
                _Business.RemoveItemInGrid(item, gridWidth, gridHeight);
            }, ViewerErrors.STAGE_LOAD_MENU, item.ToString(), gridWidth.ToString(), gridHeight.ToString());
        }

        /// <summary>
        /// Ajoute un élément dans la grille
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="item">Id de l'item à ajouter</param>
        public void AddItemInGrid(VO_InventoryItem item)
        {
            RunServiceTask(delegate
            {
                _Business.AddItemInGrid(item);
            }, ViewerErrors.STAGE_LOAD_MENU, item.ToString());
        }

        /// <summary>
        /// Switche de place entre 2 items
        /// </summary>
        /// <param name="item1">item 1</param>
        /// <param name="item2">item 2</param>
        public void SwitchItemPlaces(VO_InventoryItem item1, VO_InventoryItem item2)
        {
            RunServiceTask(delegate
            {
                _Business.SwitchItemPlaces(item1, item2);
            }, ViewerErrors.STAGE_LOAD_MENU, item1.ToString(), item2.ToString());
        }

        /// <summary>
        /// Récupère un inventoryItem depuis la grille
        /// </summary>
        /// <param name="itemId">Id de l'item</param>
        /// <param name="gridWidth">Largeur de la grille</param>
        /// <param name="gridHeight">Hauteur de la grille</param>
        /// <returns>VO_InventoryItem</returns>
        public VO_InventoryItem GetItemFromGrid(Guid itemId, int gridWidth, int gridHeight)
        {
            VO_InventoryItem guid = null;

            RunServiceTask(delegate
            {
                guid = _Business.GetItemFromGrid(itemId, gridWidth, gridHeight);
            }, ViewerErrors.STAGE_LOAD_MENU, itemId.ToString(), gridWidth.ToString(), gridHeight.ToString());

            return guid;
        }
        #endregion
    }
}
