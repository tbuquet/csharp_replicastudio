using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using System.Drawing;
using System.Reflection;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.BusinessLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.Editor.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère le panneau de calques et fenêtres enfants.
    /// </summary>
    public class LayersPanelBusiness : BaseBusiness
    {
        #region Members
        /// <summary>
        /// Image de la clé notifiant du calque principal, non supprimable
        /// </summary>
        Image _BtnKey;

        /// <summary>
        /// Image de la clé vide, notifiant du calque supprimable
        /// </summary>
        Image _BtnEmptyKey;

        /// <summary>
        /// Image notifiant d'un calque affiché
        /// </summary>
        Image _BtnShow;

        /// <summary>
        /// Image notifiant d'un calque masqué
        /// </summary>
        Image _BtnHidden;

        /// <summary>
        /// Image du ColorManager
        /// </summary>
        Image _BtnColors;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public LayersPanelBusiness()
        {
            _BtnKey = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.LayersPanel.key.png"));
            _BtnShow = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.LayersPanel.show.png"));
            _BtnHidden = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.LayersPanel.hidden.png"));
            _BtnColors = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ReplicaStudio.Editor.Resources.LayersPanel.color.png"));
            _BtnEmptyKey = new Bitmap(1, 1);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Récupère une grille de données à utiliser sur le LayersPanel
        /// </summary>
        /// <returns>Liste de DataGridViewRow</returns>
        public List<DataGridViewRow> RefreshDatasForLayersPanel()
        {
            List<DataGridViewRow> list = new List<DataGridViewRow>();
            List<VO_Layer> layers = EditorHelper.Instance.GetCurrentStageInstance().ListLayers;
            layers.Sort();

            foreach (VO_Layer layer in layers)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewTextBoxCell id = new DataGridViewTextBoxCell();
                DataGridViewImageCell key = new DataGridViewImageCell();
                DataGridViewImageCell hidden = new DataGridViewImageCell();

                //TODO: Reactiver le DataGridViewImageCell pour le ColorPanel
                //DataGridViewImageCell color = new DataGridViewImageCell();
                DataGridViewTextBoxCell color = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell name = new DataGridViewTextBoxCell();

                //Valeurs
                name.Value = layer.Title;
                id.Value = layer.Id;
                if (layer.MainLayer)
                    key.Value = _BtnKey;
                else
                    key.Value = _BtnEmptyKey;
                if (layer.Hidden)
                    hidden.Value = _BtnHidden;
                else
                    hidden.Value = _BtnShow;

                //Ajouts
                //TODO: Reactiver ColorPanel. Le Value et ReadOnly actuellement en place auront a être supprimé
                //color.Value = _BtnColors;
                color.Value = string.Empty;
                newRow.Cells.Add(id);
                newRow.Cells.Add(key);
                newRow.Cells.Add(hidden);
                newRow.Cells.Add(color);
                newRow.Cells.Add(name);
                newRow.Cells[3].ReadOnly = true;
                list.Add(newRow);

                EditorHelper.Instance.LastOrdinalLayer = layer.Ordinal + 1;
            }
            return list;
        }

        /// <summary>
        /// Passe du status d'afficher à masquer d'un calque
        /// </summary>
        /// <param name="pIdLayer">ID du calque</param>
        public void ChangeVisibilityOfLayer(Guid idLayer, DataGridViewImageCell cell)
        {
            //Recherche et assignation du layer courant
            List<VO_Layer> layers = EditorHelper.Instance.GetCurrentStageInstance().ListLayers;
            foreach (VO_Layer layer in layers)
                if (layer.Id == idLayer)
                {
                    if (layer.Hidden)
                    {
                        layer.Hidden = false;
                        cell.Value = _BtnShow;
                    }
                    else
                    {
                        layer.Hidden = true;
                        cell.Value = _BtnHidden;
                    }
                }
        }

        /// <summary>
        /// Crée un nouveau calque
        /// </summary>
        /// <param name="pTitle">Titre du calque</param>
        public void CreateLayer(string title)
        {
            EditorHelper.Instance.CurrentLayer = ObjectsFactory.CreateLayer(EditorHelper.Instance.GetCurrentStageInstance(), title, EditorHelper.Instance.LastOrdinalLayer + 1, false).Id;
        }

        /// <summary>
        /// Supprime un calque
        /// </summary>
        /// <param name="pIdLayer">Id du calque à supprimer</param>
        public void DeleteLayer(Guid idLayer)
        {
            VO_Layer selectedLayer = null;
            List<VO_Layer> layers = EditorHelper.Instance.GetCurrentStageInstance().ListLayers;
            foreach (VO_Layer layer in layers)
            {
                if (!layer.MainLayer && layer.Id == idLayer)
                    selectedLayer = layer;
            }
            selectedLayer.Delete();
        }

        /// <summary>
        /// Change le nom d'un calque
        /// </summary>
        /// <param name="pIdLayer">Id du calque</param>
        /// <param name="pTitle">Nouveau nom</param>
        public void ChangeLayerName(Guid idLayer, string title)
        {
            List<VO_Layer> layers = EditorHelper.Instance.GetCurrentStageInstance().ListLayers;
            foreach (VO_Layer layer in layers)
            {
                if (layer.Id == idLayer)
                {
                    layer.Title = title;
                    break;
                }
            }
        }

        /// <summary>
        /// Inverse les ordinaux de deux calques.
        /// </summary>
        /// <param name="pFirstLayer">Id du premier calque</param>
        /// <param name="pSecondLayer">Id du second calque</param>
        public void SwitchOrdinalBetweenLayers(Guid firstLayer, Guid secondLayer)
        {
            List<VO_Layer> layers = EditorHelper.Instance.GetCurrentStageInstance().ListLayers;
            VO_Layer voFirstLayer = null;
            VO_Layer voSecondLayer = null;
            int switchValue = 0;

            foreach (VO_Layer layer in layers)
            {
                if (layer.Id == firstLayer)
                    voFirstLayer = layer;
                else if (layer.Id == secondLayer)
                    voSecondLayer = layer;
            }
            switchValue = voFirstLayer.Ordinal;
            voFirstLayer.Ordinal = voSecondLayer.Ordinal;
            voSecondLayer.Ordinal = switchValue;
        }
        #endregion
    }
}
