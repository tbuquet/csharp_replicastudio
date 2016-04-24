using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Editor.BusinessLayer;
using System.Drawing;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.ServiceLayer
{
    /// <summary>
    /// Classe service de la scène
    /// </summary>
    public class StageService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        StageBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public StageService()
        {
            _Business = new StageBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge un background vide
        /// </summary>
        /// <param name="pSizeContainer">Taille de la surface totale</param>
        /// <param name="pSizeStage">Taille de la scène</param>
        /// <returns>Surface</returns>
        public void LoadEmptyBackground(Size sizeContainer, Size sizeStage)
        {
            RunServiceTask(delegate
            {
                _Business.LoadEmptyBackground(sizeContainer, sizeStage);
            }, Errors.ERROR_STAGE_STR_LOAD, sizeContainer.ToString(), sizeStage.ToString());
        }

        /// <summary>
        /// Rafraichi la scène
        /// </summary>
        /// <returns>Surface</returns>
        public Image RefreshStage()
        {
            Image image = null;

            //Execution de la méthode
            RunServiceTask(delegate
            {
                image = _Business.RefreshStage();
            },
            //Problème de mémoire
            delegate
            {
                ImageManager.ResetResources();
                image = RefreshStage();
            }, Errors.ERROR_STAGE_STR_LOAD, false);

            return image;
        }

        /// <summary>
        /// Passe une mémoire une image de la scène antérieur et postérieur au calque et au mode courant.
        /// </summary>
        public void LoadMinusMaximusLayers()
        {
            RunServiceTask(delegate
            {
                _Business.LoadMinusMaximusLayers();
            }, Errors.ERROR_STAGE_STR_LOAD);
        }

        /// <summary>
        /// Récupère le point sur scène en tenant compte du zoom
        /// </summary>
        /// <param name="pPoint">Coordonnées à traiter</param>
        /// <returns>Coordonnées traitées</returns>
        public Point GetDragStageCoords(Point point)
        {
            Point newPoint = new Point();

            RunServiceTask(delegate
            {
                newPoint = _Business.GetDragStageCoords(point);
            }, Errors.ERROR_STAGE_STR_LOAD, false, point.ToString());

            return newPoint;
        }

        #region Création/Suppression
        /// <summary>
        /// Recharge les ressources de la scène
        /// </summary>
        public void ResetStageResources()
        {
            RunServiceTask(delegate
            {
                _Business.ResetStageResources();
            }, Errors.ERROR_STAGE_STR_LOAD);
        }

        /// <summary>
        /// Créer un décor
        /// </summary>
        /// <param name="pLocation">Location sur scène</param>
        /// <param name="pFile">Fichier décor</param>
        public void CreateDecor(Point location, string file)
        {
            RunServiceTask(delegate
            {
                _Business.CreateDecor(location, file);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString(), file);
        }

        /// <summary>
        /// Créer un HotSpot
        /// </summary>
        /// <param name="location">Localisation du scène</param>
        public VO_StageHotSpot CreateHotSpot(Point location)
        {
            VO_StageHotSpot hotSpot = null;

            RunServiceTask(delegate
            {
                hotSpot = _Business.CreateHotSpot(location);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString());

            return hotSpot;
        }

        /// <summary>
        /// Créer une walkable area
        /// </summary>
        /// <param name="location">Localisation du scène</param>
        public VO_StageHotSpot CreateWalkableArea(Point location)
        {
            VO_StageHotSpot hotSpot = null;

            RunServiceTask(delegate
            {
                hotSpot = _Business.CreateWalkableArea(location);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString());

            return hotSpot;
        }

        /// <summary>
        /// Créer une region
        /// </summary>
        /// <param name="location">Localisation du scène</param>
        public VO_StageHotSpot CreateRegion(Point location)
        {
            VO_StageHotSpot hotSpot = null;

            RunServiceTask(delegate
            {
                hotSpot = _Business.CreateRegion(location);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString());

            return hotSpot;
        }

        /// <summary>
        /// Créer un nouveau point de HotSpot
        /// </summary>
        /// <param name="location">Coordonnées du point</param>
        /// <returns>Index sélectioné</returns>
        public int CreateNewPointToTheCurrentSelectedHotSpot(Point location, int i)
        {
            int output = -1;

            RunServiceTask(delegate
            {
                output = _Business.CreateNewPointToTheCurrentSelectedHotSpot(location, i);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString(), i.ToString());

            return output;
        }

        /// <summary>
        /// Déplace un objet en avant ou arrière plan.
        /// </summary>
        /// <param name="direction">Avant ou arrière plan</param>
        /// <param name="max">Au premier ou au dernier plan</param>
        public void MoveObjectInPlan(Enums.Direction direction, bool max)
        {
            RunServiceTask(delegate
            {
                _Business.MoveObjectInPlan(direction, max);
            }, Errors.ERROR_STAGE_STR_LOAD, direction.ToString(), max.ToString());
        }

        /// <summary>
        /// Supprime un point de hotspot
        /// </summary>
        /// <param name="location">Coordonnées du point</param>
        /// <returns>true si un point a été supprimé, sinon false</returns>
        public bool RemovePointOfTheCurrentSelectedHotSpot(Point location)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.RemovePointOfTheCurrentSelectedHotSpot(location);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString());

            return output;
        }

        /// <summary>
        /// Mets à jour la location de la zone
        /// </summary>
        /// <param name="hotSpot">HotSpot</param>
        public void UpdateAreaLocation(VO_StageHotSpot hotSpot)
        {
            RunServiceTask(delegate
            {
                _Business.UpdateAreaLocation(hotSpot);
            }, Errors.ERROR_STAGE_STR_LOAD, hotSpot.Title);
        }

        /// <summary>
        /// Créer une animation
        /// </summary>
        /// <param name="pLocation">Localisation sur scène</param>
        /// <param name="pAnimId">ID d'animation</param>
        public void CreateAnimation(Point location, Guid animId)
        {
            RunServiceTask(delegate
            {
                _Business.CreateAnimation(location, animId);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString(), animId.ToString());
        }

        /// <summary>
        /// Créer un character
        /// </summary>
        /// <param name="pLocation">Localisation sur scène</param>
        /// <param name="pAnimId">ID d'animation</param>
        public void CreateCharacter(Point location, Guid animId)
        {
            RunServiceTask(delegate
            {
                _Business.CreateCharacter(location, animId);
            }, Errors.ERROR_STAGE_STR_LOAD, location.ToString(), animId.ToString());
        }

        /// <summary>
        /// Rafraichie les zones vectorielles
        /// </summary>
        public void RefreshVectorPoints(Graphics e)
        {
            RunServiceTask(delegate
            {
                _Business.RefreshVectorPoints(e);
            }, Errors.ERROR_STAGE_STR_LOAD);
        }

        /// <summary>
        /// Ajoute une ligne sur la position mouvante.
        /// </summary>
        /// <param name="e">Graphic</param>
        /// <param name="position">Position souris</param>
        public void RefreshVectorMovingPoint(Graphics e, Point position)
        {
            RunServiceTask(delegate
            {
                _Business.RefreshVectorPoints(e);
            }, Errors.ERROR_STAGE_STR_LOAD, position.ToString());
        }

        /// <summary>
        /// Récupère un point et renvoie son index
        /// </summary>
        /// <param name="point">point</param>
        /// <returns>index</returns>
        public int GetVectorPoint(Point mousePoint)
        {
            int output = -1;

            RunServiceTask(delegate
            {
                output = _Business.GetVectorPoint(mousePoint);
            }, Errors.ERROR_STAGE_STR_LOAD, mousePoint.ToString());

            return output;
        }

        /// <summary>
        /// Vérifie si un point se trouve sur le polygon sélectionné
        /// </summary>
        /// <param name="point">Point à tester</param>
        /// <param name="addPoint">Ajouter le point au polygone</param>
        /// <returns></returns>
        public bool IsOnTheSelectedPolygon(Point checkedPoint, bool addPoint)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.IsOnTheSelectedPolygon(checkedPoint, addPoint);
            }, Errors.ERROR_STAGE_STR_LOAD, checkedPoint.ToString(), addPoint.ToString());

            return output;
        }

        /// <summary>
        /// Déplace le point choisi en index
        /// </summary>
        /// <param name="mousePoint">position</param>
        /// <param name="index">index</param>
        public void MoveVectorPoint(Point mousePoint, int index)
        {
            RunServiceTask(delegate
            {
                _Business.MoveVectorPoint(mousePoint, index);
            }, Errors.ERROR_STAGE_STR_LOAD, mousePoint.ToString(), index.ToString());
        }
        #endregion

        #region Opérations sur scène
        /// <summary>
        /// Génère la liste des items sélectionnés
        /// </summary>
        /// <param name="pPosition">Position de la souris relative à la scène</param>
        /// <param name="CtrlPressed">Bouton Ctrl pressé</param>
        public void GetSelectedItem(Point position, bool ctrlPressed)
        {
            RunServiceTask(delegate
            {
                _Business.GetSelectedItem(position, ctrlPressed);
            }, Errors.ERROR_STAGE_STR_LOAD, position.ToString(), ctrlPressed.ToString());
        }

        /// <summary>
        /// Enregistre la position des objets sélectionnés au démarrage du Drag & Drop.
        /// </summary>
        /// <param name="pPosition">Position brute de la souris</param>
        public void StartObjectDrag(Point position)
        {
            RunServiceTask(delegate
            {
                _Business.StartObjectDrag(position);
            }, Errors.ERROR_STAGE_STR_LOAD, position.ToString());
        }

        /// <summary>
        /// Déplacer les objets en fonction du point souris original
        /// </summary>
        /// <param name="pPosition">Position brute de la souris</param>
        public void MoveObjectDragDrop(Point position)
        {
            RunServiceTask(delegate
            {
                _Business.MoveObjectDragDrop(position);
            }, Errors.ERROR_STAGE_STR_LOAD, position.ToString());
        }

        /// <summary>
        /// Synchronise les points avec le padding de stage
        /// </summary>
        /// <param name="points">liste de points</param>
        /// <returns>liste de point</returns>
        public Point[] MovePoints(Point[] points, int x, int y)
        {
            Point[] output = null;

            RunServiceTask(delegate
            {
                output = _Business.MovePoints(points, x, y);
            }, Errors.ERROR_STAGE_STR_LOAD, points.ToString(), x.ToString(), y.ToString());

            return output;
        }
        #endregion
        #endregion
    }
}
