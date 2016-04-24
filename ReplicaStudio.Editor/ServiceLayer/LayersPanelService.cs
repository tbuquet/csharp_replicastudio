using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Editor.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;

namespace ReplicaStudio.Editor.ServiceLayer
{
    /// <summary>
    /// Classe service du panneau de calques
    /// </summary>
    public class LayersPanelService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence à business
        /// </summary>
        LayersPanelBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public LayersPanelService()
        {
            _Business = new LayersPanelBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère une grille de données à utiliser sur le LayersPanel
        /// </summary>
        /// <returns>Liste de DataGridViewRow</returns>
        public List<DataGridViewRow> RefreshDatasForLayersPanel()
        {
            List<DataGridViewRow> list = null;

            RunServiceTask(delegate
            {
                list = _Business.RefreshDatasForLayersPanel();
            }, Errors.ERROR_LAYER_STR_LOAD);

            return list;
        }

        /// <summary>
        /// Passe du status d'afficher à masquer d'un calque
        /// </summary>
        /// <param name="pIdLayer">ID du calque</param>
        public void ChangeVisibilityOfLayer(Guid idLayer, DataGridViewImageCell cell)
        {
            RunServiceTask(delegate
            {
                _Business.ChangeVisibilityOfLayer(idLayer, cell);
            }, Errors.ERROR_LAYER_STR_LOAD, idLayer.ToString(), cell.ToString());
        }

        /// <summary>
        /// Crée un nouveau calque
        /// </summary>
        /// <param name="pTitle">Titre du calque</param>
        public void CreateLayer(string title)
        {
            RunServiceTask(delegate
            {
                _Business.CreateLayer(title);
            }, Errors.ERROR_LAYER_STR_LOAD, title);
        }

        /// <summary>
        /// Supprime un calque
        /// </summary>
        /// <param name="pIdLayer">Id du calque à supprimer</param>
        public void DeleteLayer(Guid idLayer)
        {
            RunServiceTask(delegate
            {
                _Business.DeleteLayer(idLayer);
            }, Errors.ERROR_LAYER_STR_LOAD, idLayer.ToString());
        }

        /// <summary>
        /// Change le nom d'un calque
        /// </summary>
        /// <param name="pIdLayer">Id du calque</param>
        /// <param name="pTitle">Nouveau nom</param>
        public void ChangeLayerName(Guid idLayer, string title)
        {
            RunServiceTask(delegate
            {
                _Business.ChangeLayerName(idLayer, title);
            }, Errors.ERROR_LAYER_STR_LOAD, idLayer.ToString(), title);
        }

        /// <summary>
        /// Inverse les ordinaux de deux calques.
        /// </summary>
        /// <param name="pFirstLayer">Id du premier calque</param>
        /// <param name="pSecondLayer">Id du second calque</param>
        public void SwitchOrdinalBetweenLayers(Guid firstLayer, Guid secondLayer)
        {
            RunServiceTask(delegate
            {
                _Business.SwitchOrdinalBetweenLayers(firstLayer, secondLayer);
            }, Errors.ERROR_LAYER_STR_LOAD, firstLayer.ToString(), secondLayer.ToString());
        }
        #endregion
    }
}
