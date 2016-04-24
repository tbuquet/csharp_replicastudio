using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using System.Drawing;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.TransverseLayer;

namespace ReplicaStudio.Editor.TransverseLayer
{
    /// <summary>
    /// Classe d'aide qui stocke diverses informations pour le projet
    /// </summary>
    public class EditorHelper
    {
        #region Members
        /// <summary>
        /// Instance singleton
        /// </summary>
        private static EditorHelper _Instance;
        #endregion

        #region Properties
        /// <summary>
        /// Instance singleton
        /// </summary>
        public static EditorHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EditorHelper();
                }
                return _Instance;
            }
        }

        #region Drag & Drop
        /// <summary>
        /// Item en cours de drag & drop
        /// </summary>
        public string DragDropItemId { get; set; }

        /// <summary>
        /// Type de l'objet en cours de drag & drop
        /// </summary>
        public Enums.StageObjectType DragDropObjectType { get; set; }

        /// <summary>
        /// Objets sélectionnés sur scène
        /// </summary>
        public List<VO_StageObject> SelectedObjects { get; set; }

        /// <summary>
        /// HotStop sélectionné sur scène
        /// </summary>
        public VO_StageHotSpot SelectedHotSpot { get; set; }
        #endregion

        #region Currents
        /// <summary>
        /// Scène courante
        /// </summary>
        public Guid CurrentStage { get; set; }

        /// <summary>
        /// Renvoie l'ID du calque courant
        /// </summary>
        public Guid CurrentLayer { get; set; }

        /// <summary>
        /// Renvoie le type d'outil de dessin courant
        /// </summary>
        public Enums.DrawingTools CurrentDrawingTool { get; set; } 

        /// <summary>
        /// Valeur courante du zoom
        /// </summary>
        public int CurrentZoom { get; set; }

        /// <summary>
        /// Type d'édition courant
        /// </summary>
        public Enums.StagePanelState CurrentStageState { get; set; }
        #endregion

        #region Dessin
        /// <summary>
        /// Index du point du hotspot selectionné
        /// </summary>
        public int SelectedHotSpotPoint { get; set; }

        /// <summary>
        /// Edition mode des hotspot
        /// </summary>
        public bool HotSpotEditionMode { get; set; }

        /// <summary>
        /// Brush de transparence Zoom
        /// </summary>
        public Dictionary<int,Brush> TransparentBrushes { get; set; }
        #endregion

        /// <summary>
        /// Dernier ordinal de calque
        /// </summary>
        public int LastOrdinalLayer { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        private EditorHelper()
        {
            SelectedObjects = new List<VO_StageObject>();
            CurrentDrawingTool = Enums.DrawingTools.Pointer;
            CurrentStageState = Enums.StagePanelState.Decors;
            CurrentZoom = 1;
            SelectedHotSpotPoint = -1;

            TransparentBrushes = new Dictionary<int, Brush>();
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize);
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize / 2);
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize / 4);
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize / 8);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Recharger les blocs transparents
        /// </summary>
        public void ReloadTransparentBlocs()
        {
            foreach (Brush brush in TransparentBrushes.Values)
            {
                brush.Dispose();
            }
            TransparentBrushes = new Dictionary<int, Brush>();
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize);
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize / 2);
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize / 4);
            MakeTransparentBlocs(EditorSettings.Instance.TransparentBlockSize / 8);
        }

        /// <summary>
        /// Reset de l'instance
        /// </summary>
        public void ResetEditorHelper()
        {
            _Instance = null;
        }

        /// <summary>
        /// Renvoie la VO Stage courante
        /// </summary>
        /// <returns></returns>
        public VO_Stage GetCurrentStageInstance()
        {
            return GameCore.Instance.GetStageById(EditorHelper.Instance.CurrentStage);
        }

        /// <summary>
        /// Renvoie la VO Layer courante
        /// </summary>
        /// <returns></returns>
        public VO_Layer GetCurrentLayerInstance()
        {
            List<VO_Layer> layers = GetCurrentStageInstance().ListLayers;
            foreach (VO_Layer layer in layers)
                if (layer.Id == CurrentLayer)
                    return layer;
            return new VO_Layer();
        }

        /// <summary>
        /// Créer un Brush de transparence
        /// </summary>
        /// <param name="pSize">Taille des blocs</param>
        private void MakeTransparentBlocs(int size)
        {
            Image mainImage = new Bitmap(size * 2, size * 2, EditorConstants.PERF_EDITOR_BITSPERPIXEL);
            Graphics graphic = Graphics.FromImage(mainImage);
            Image transparentBlock = new Bitmap(size, size, EditorConstants.PERF_EDITOR_BITSPERPIXEL);
            Image transparentBlockAlt = new Bitmap(size, size, EditorConstants.PERF_EDITOR_BITSPERPIXEL);
            Graphics transparentGraphic = Graphics.FromImage(transparentBlock);
            transparentGraphic.FillRectangle(new SolidBrush(EditorSettings.Instance.TransparentColor1), new Rectangle(new Point(0, 0), transparentBlock.Size));
            Graphics transparentAltGraphic = Graphics.FromImage(transparentBlockAlt);
            transparentAltGraphic.FillRectangle(new SolidBrush(EditorSettings.Instance.TransparentColor2), new Rectangle(new Point(0, 0), transparentBlockAlt.Size));
            graphic.DrawImage(transparentBlock, new Point(0, 0));
            graphic.DrawImage(transparentBlock, new Point(size, size));
            graphic.DrawImage(transparentBlockAlt, new Point(0, size));
            graphic.DrawImage(transparentBlockAlt, new Point(size, 0));
            graphic.Dispose();
            transparentBlock.Dispose();
            transparentBlockAlt.Dispose();
            transparentGraphic.Dispose();
            transparentAltGraphic.Dispose();

            TransparentBrushes.Add(size, new TextureBrush(mainImage));
        }

        #endregion
    }
}
