using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Managers;

namespace ReplicaStudio.Viewer.BusinessLayer
{
    /// <summary>
    /// Classe gérant les principales fonctions  de transition du jeu
    /// </summary>
    public class InventoryBusiness : BaseBusiness
    {
        #region Members
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur
        /// </summary>
        public InventoryBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Renvoie les informations du menu
        /// </summary>
        /// <returns>VO_Menu</returns>
        public VO_Menu GetMenuData()
        {
            return GameCore.Instance.Game.Menu;
        }

        /// <summary>
        /// Check si le curseur est sur le bouton inventaire
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool GetInventoryButtonEvent(Point point, Point buttonPoint, int buttonWidth, int buttonHeight)
        {
            Rectangle rect = new Rectangle(buttonPoint.X, buttonPoint.Y, buttonWidth, buttonHeight);
            if (rect.Contains(point))
                return true;
            return false;
        }

        /// <summary>
        /// Teste si la souris est dans la grille
        /// </summary>
        /// <param name="mousePos">Position souris</param>
        /// <returns>True si vérifié, false sinon</returns>
        public bool MouseIsInGrid(Point mousePos, Point tablePoint, int gridWidth, int gridHeight, int itemWidth, int itemHeight)
        {
            Rectangle rect = new Rectangle(tablePoint.X, tablePoint.Y, gridWidth * itemWidth, gridHeight * itemHeight);
            if (rect.Contains(mousePos))
                return true;
            return false;
        }

        /// <summary>
        /// Check si le curseur est sur le bouton inventaire
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public VO_InventoryItem GetInventoryItemEvent(Point point, Point tablePoint, int gridWidth, int gridHeight, int itemWidth, int itemHeight)
        {
            Guid[,] table = PlayableCharactersManager.CurrentPlayerCharacter.Items;
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    Rectangle rect = new Rectangle(tablePoint.X + j * itemWidth, tablePoint.Y + i * itemHeight, itemWidth, itemHeight);
                    if (rect.Contains(point))
                        return new VO_InventoryItem(table[j, i], new Point(j, i));
                }
            }
            return null;
        }

        /// <summary>
        /// Retire un élément de la grille
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void RemoveItemInGrid(VO_InventoryItem item)
        {
            PlayableCharactersManager.CurrentPlayerCharacter.Items[item.Location.X, item.Location.Y] = Guid.Empty;
        }

        /// <summary>
        /// Retire un élément de la grille
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void RemoveItemInGrid(Guid item, int gridWidth, int gridHeight)
        {
            Guid[,] table = PlayableCharactersManager.CurrentPlayerCharacter.Items;
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    if (table[j, i] == item)
                        table[j, i] = Guid.Empty;
                }
            }
        }

        /// <summary>
        /// Ajoute un élément dans la grille
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="item">Id de l'item à ajouter</param>
        public void AddItemInGrid(VO_InventoryItem item)
        {
            PlayableCharactersManager.CurrentPlayerCharacter.Items[item.Location.X, item.Location.Y] = item.ItemId;
        }

        /// <summary>
        /// Switche de place entre 2 items
        /// </summary>
        /// <param name="item1">item 1</param>
        /// <param name="item2">item 2</param>
        public void SwitchItemPlaces(VO_InventoryItem item1, VO_InventoryItem item2)
        {
            AddItemInGrid(new VO_InventoryItem(item1.ItemId, item2.Location));
            AddItemInGrid(new VO_InventoryItem(item2.ItemId, item1.Location));
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
            Guid[,] table = PlayableCharactersManager.CurrentPlayerCharacter.Items;
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    if (table[j, i] == itemId)
                        return new VO_InventoryItem(table[j, i], new Point(j, i));
                }
            }
            return null;
        }
        #endregion
    }
}
