using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.Forms.UserControls
{
    /// <summary>
    /// Formulaire de gestion de calques
    /// </summary>
    public partial class LayersPanel : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        LayersPanelService _Service;

        /// <summary>
        /// Référence au formulaire NewLayer
        /// </summary>
        NewLayer _NewLayer;

        /// <summary>
        /// Référence au formulaire de gestion de couleurs
        /// </summary>
        ImageColorManager _ColorManager;
        #endregion

        #region Events
        /// <summary>
        /// Survient quand un autre calque est sélectionné
        /// </summary>
        public event EventHandler CurrentLayerHasChanged;

        /// <summary>
        /// Survient quand la scène doit être rafraichie
        /// </summary>
        public event EventHandler StageNeedsToBeRefreshed;

        /// <summary>
        /// Survient quand un calque est supprimé
        /// </summary>
        public event EventHandler LayerDeleted;

        /// <summary>
        /// Survient quand la matrice de couleur d'un calque est changé
        /// </summary>
        public event EventHandler ColorTransformationChanged;

        /// <summary>
        /// Survient quand la souris survole le contrôle
        /// </summary>
        public event EventHandler MouseEnterCustom;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public LayersPanel()
        {
            InitializeComponent();
            _Service = new LayersPanelService();
            _NewLayer = new NewLayer();
            _NewLayer.FormClosing += new FormClosingEventHandler(_NewLayer_FormClosing);
            _ColorManager = new ImageColorManager();
            _ColorManager.ColorTransformationChanged += new EventHandler(_ColorManager_ColorTransformationChanged);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Vide les calques
        /// </summary>
        public void ResetLayers()
        {
            //this.BtnCreateLayer.Enabled = false;
            this.BtnDeleteLayer.Enabled = false;
            this.BtnDown.Enabled = false;
            this.BtnUp.Enabled = false;
            GrdLayers.Rows.Clear();
        }

        /// <summary>
        /// Méthode qui rafraichi la liste des calques
        /// </summary>
        public void RefreshLayers()
        {
            GrdLayers.Rows.Clear();

            if (EditorHelper.Instance.CurrentStage != Guid.Empty)
            {
                List<DataGridViewRow> listRows = _Service.RefreshDatasForLayersPanel();
                if (listRows.Count > 0)
                {
                    this.BtnUp.Enabled = true;
                    this.BtnDown.Enabled = true;
                }

                int selectedIndex = 0;
                foreach (DataGridViewRow vRow in listRows)
                {
                    GrdLayers.Rows.Add(vRow);
                    if (EditorHelper.Instance.CurrentLayer == new Guid(vRow.Cells[0].Value.ToString()))
                    {
                        selectedIndex = vRow.Index;
                        GrdLayers_RowEnter(new DataGridViewCellEventArgs(0, vRow.Index));
                    }
                    else
                        vRow.Selected = false;
                    GrdLayers.Rows[selectedIndex].Selected = true;
                }
            }
        }

        /// <summary>
        /// Survient lors d'une entrée dans la gridview
        /// </summary>
        /// <param name="e">Arguments contenant le numéro de ligne et colonne courant</param>
        private void GrdLayers_RowEnter(DataGridViewCellEventArgs e)
        {
            Guid idLayer = new Guid(GrdLayers.Rows[e.RowIndex].Cells[0].Value.ToString());

            //Recherche et assignation du layer courant
            VO_Layer selectedLayer = EditorHelper.Instance.GetCurrentStageInstance().ListLayers.Find(p => p.Id == idLayer);

            if (selectedLayer.MainLayer)
                BtnDeleteLayer.Enabled = false;
            else
                BtnDeleteLayer.Enabled = true;
        }
        #endregion

        #region EventHandlers 
        /// <summary>
        /// Click sur Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateLayer_Click(object sender, EventArgs e)
        {
            _NewLayer.ShowDialog();
        }

        /// <summary>
        /// Click sur une cellule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Gestion améliorée du RowEnter
            GrdLayers_RowEnter(e);

            //Test du cas où le calque sélectionné change
            Guid newCurrentLayer = new Guid(GrdLayers.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (EditorHelper.Instance.CurrentLayer != newCurrentLayer)
            {
                EditorHelper.Instance.CurrentLayer = newCurrentLayer;
                this.CurrentLayerHasChanged(this, new EventArgs());
            }

            switch (e.ColumnIndex)
            {
                //Hidden/Show
                case 2:
                    DataGridViewImageCell hidden = (DataGridViewImageCell)GrdLayers.Rows[e.RowIndex].Cells[2];
                    _Service.ChangeVisibilityOfLayer(EditorHelper.Instance.CurrentLayer, hidden);
                    this.CurrentLayerHasChanged(this, new EventArgs());
                    break;
                case 3:
                    //TODO: Reactiver le ColorManager du Panel
                    //DataGridViewImageCell color = (DataGridViewImageCell)GrdLayers.Rows[e.RowIndex].Cells[3];
                    //_ColorManager.LoadPanel();
                    //_ColorManager.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// Click lorsque la fenêtre de création de calques est fermée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _NewLayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.RefreshLayers();
            this.CurrentLayerHasChanged(this, new EventArgs());
        }

        /// <summary>
        /// Click sur delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.GetCurrentStageInstance() != null && GrdLayers.SelectedRows.Count > 0)
            {
                Guid idLayer = new Guid(GrdLayers.SelectedRows[0].Cells[0].Value.ToString());
                VO_Layer layer = EditorHelper.Instance.GetCurrentStageInstance().ListLayers.Find(p => p.Id == idLayer);
                if (!layer.MainLayer)
                {
                    _Service.DeleteLayer(idLayer);
                    GrdLayers.Rows.Remove(GrdLayers.SelectedRows[0]);

                    //On force la sélection sur le premier élément.
                    GrdLayers.ClearSelection();
                    GrdLayers.Rows[0].Selected = true;
                    idLayer = new Guid(GrdLayers.Rows[0].Cells[0].Value.ToString());
                    EditorHelper.Instance.CurrentLayer = idLayer;
                    GrdLayers_RowEnter(new DataGridViewCellEventArgs(0, 0));

                    this.LayerDeleted(this, new EventArgs());
                }
                else
                {
                    GrdLayers_RowEnter(new DataGridViewCellEventArgs(0, GrdLayers.SelectedRows[0].Index));
                }
            }
        }

        /// <summary>
        /// Survient quand une modification de la gridview est en cours de validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLayers_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            Guid  currentLayer = new Guid(GrdLayers.Rows[e.RowIndex].Cells[0].Value.ToString());

            switch (e.ColumnIndex)
            {
                //Layer Name
                case 4:
                    DataGridViewTextBoxCell name = (DataGridViewTextBoxCell)GrdLayers.Rows[e.RowIndex].Cells[4];
                    if (string.IsNullOrEmpty(name.Value.ToString()))
                        MessageBox.Show(Errors.LAYER_TITLE_EMPTY, Errors.ERROR_BOX_TITLE);
                    else
                        _Service.ChangeLayerName(currentLayer, name.Value.ToString());
                    break;
            }
        }

        /// <summary>
        /// Click sur Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.GetCurrentStageInstance() != null)
            {
                DataGridViewRow currentRow = GrdLayers.SelectedRows[0];
                int index = currentRow.Index;
                if (currentRow.Index != 0)
                {
                    DataGridViewRow rowToSwap = GrdLayers.Rows[currentRow.Index - 1];
                    Guid currentId = new Guid(currentRow.Cells[0].Value.ToString());
                    Guid idToSwap = new Guid(rowToSwap.Cells[0].Value.ToString());
                    _Service.SwitchOrdinalBetweenLayers(currentId, idToSwap);
                    this.CurrentLayerHasChanged(this, new EventArgs());
                    this.RefreshLayers();
                    this.StageNeedsToBeRefreshed(this, new EventArgs());
                    GrdLayers.ClearSelection();
                    GrdLayers.Rows[index - 1].Selected = true;
                }
            }
        }

        /// <summary>
        /// Click sur down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (EditorHelper.Instance.GetCurrentStageInstance() != null)
            {
                DataGridViewRow currentRow = GrdLayers.SelectedRows[0];
                int index = currentRow.Index;
                if (currentRow.Index < GrdLayers.Rows.Count - 1)
                {
                    DataGridViewRow rowToSwap = GrdLayers.Rows[currentRow.Index + 1];
                    Guid currentId = new Guid(currentRow.Cells[0].Value.ToString());
                    Guid idToSwap = new Guid(rowToSwap.Cells[0].Value.ToString());
                    _Service.SwitchOrdinalBetweenLayers(currentId, idToSwap);
                    this.CurrentLayerHasChanged(this, new EventArgs());
                    this.RefreshLayers();
                    this.StageNeedsToBeRefreshed(this, new EventArgs());
                    GrdLayers.ClearSelection();
                    GrdLayers.Rows[index + 1].Selected = true;
                }
            }
        }

        /// <summary>
        /// Code exécuté lorsque les couleurs de la fenêtre de gestion des couleurs sont changées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ColorManager_ColorTransformationChanged(object sender, EventArgs e)
        {
            this.ColorTransformationChanged(this, new EventArgs());
        }

        /// <summary>
        /// Survient quand la souris entre dans le contrôle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayersPanel_MouseEnter(object sender, EventArgs e)
        {
            this.MouseEnterCustom(null, new EventArgs());
        }
        #endregion
    }
}
