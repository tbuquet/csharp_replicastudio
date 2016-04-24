using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Algorithms;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    public class MatrixManager
    {
        #region Members
        private static MatrixManager _CurrentStage;
        private static Dictionary<Guid, MatrixManager> _Stages;

        private bool _WalkableCreated = false;
        private bool _EventsCreated = false;
        private bool _RegionsCreated = false;
        private byte[,] _WalkableMatrix = new byte[4096, 4096];
        private byte[,] _EventsMatrix = new byte[4096, 4096];
        private byte[,] _RegionsMatrix = new byte[4096, 4096];
        private PathFinderFast _WalkAlgo;
        private VO_Stage _Stage;
        #endregion

        #region Properties
        /// <summary>
        /// Récupérer stage courant
        /// </summary>
        public static MatrixManager CurrentStage
        {
            get
            {
                return _CurrentStage;
            }
        }

        public static MatrixManager Stages(Guid id)
        {
            if (_Stages == null)
                _Stages = new Dictionary<Guid, MatrixManager>();
            if (!_Stages.ContainsKey(id))
                _Stages.Add(id, new MatrixManager());
            return _Stages[id];
        }

        public static void SetCurrentStage(Guid id)
        {
            if (_Stages == null)
                _Stages = new Dictionary<Guid, MatrixManager>();
            if (!_Stages.ContainsKey(id))
                _Stages.Add(id, new MatrixManager());
            _CurrentStage = _Stages[id];
            _CurrentStage._Stage = GameCore.Instance.Game.Stages.Find(p => p.Id == id);
        }

        public byte[,] WalkableMatrix
        {
            get
            {
                return _WalkableMatrix;
            }
        }

        public byte[,] EventsMatrix
        {
            get
            {
                return _EventsMatrix;
            }
        }

        public byte[,] RegionsMatrix
        {
            get
            {
                return _RegionsMatrix;
            }
        }

        public PathFinderFast WalkAlgo
        {
            get
            {
                return _WalkAlgo;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge une matrice
        /// </summary>
        /// <param name="dimensions">Dimensions de la scène</param>
        /// <param name="hotspots">Liste de hotspots</param>
        /// <returns>Matrice d'octets</returns>
        public void LoadWalkableMatrix(System.Drawing.Size dimensions, VO_Stage stage, int matrixPrecision)
        {
            if (!_WalkableCreated)
            {
                int height = dimensions.Height / matrixPrecision;
                int width = dimensions.Width / matrixPrecision;
                if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + stage.Id.ToString() + "_w"))
                {
                    StreamReader myFile = new StreamReader(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + stage.Id.ToString() + "_w");
                    for (int y = 0; y < height; y++)
                    {
                        string line = myFile.ReadLine();
                        for (int x = 0; x < width; x++)
                        {
                            int nX = 3 * x;
                            _WalkableMatrix[x, y] = Convert.ToByte(line[nX].ToString() + line[nX + 1].ToString() + line[nX + 2].ToString());
                        }
                    }
                    myFile.Close();
                }
                else
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            _WalkableMatrix[x, y] = 0;
                            byte i = 1;
                            foreach (VO_Layer layer in stage.ListLayers)
                            {
                                foreach (VO_StageHotSpot hotspot in layer.ListWalkableAreas)
                                {
                                    if (Tools.PointInPolygon(new Point(x, y), ConvertPointsForMatrix(matrixPrecision, hotspot.Points)))
                                    {
                                        _WalkableMatrix[x, y] = i;
                                        break;
                                    }
                                }
                                i++;
                            }
                        }
                    }
                }
                _WalkAlgo = new PathFinderFast(MatrixManager.CurrentStage.WalkableMatrix);
                _WalkAlgo.Formula = HeuristicFormula.Manhattan;
                _WalkAlgo.SearchLimit = ViewerConstants.PATHFINDER_SEARCHLIMIT;
                _WalkAlgo.Diagonals = true;
                _WalkableCreated = true;
            }
        }

        /// <summary>
        /// Charge une matrice d'evenements
        /// </summary>
        /// <param name="dimensions">Dimensions de la scène</param>
        /// <param name="hotspots">Liste de hotspots</param>
        /// <param name="matrixPrecision"></param>
        /// <returns>Matrice d'octets</returns>
        public void LoadEventsMatrix(System.Drawing.Size dimensions, List<VO_StageHotSpot> hotspots, int matrixPrecision)
        {
            if (!_EventsCreated)
            {
                int height = dimensions.Height / matrixPrecision;
                int width = dimensions.Width / matrixPrecision;
                if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + _Stage.Id.ToString() + "_e"))
                {
                    StreamReader myFile = new StreamReader(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + _Stage.Id.ToString() + "_e");
                    for (int y = 0; y < height; y++)
                    {
                        string line = myFile.ReadLine();
                        for (int x = 0; x < width; x++)
                        {
                            int nX = 3 * x;
                            _EventsMatrix[x, y] = Convert.ToByte(line[nX].ToString() + line[nX + 1].ToString() + line[nX + 2].ToString());
                        }
                    }
                    myFile.Close();
                }
                else
                {
                    for (int y = 0; y <= height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            _EventsMatrix[x, y] = 0;
                            int i = 1;
                            foreach (VO_StageHotSpot hotspot in hotspots)
                            {
                                if (Tools.PointInPolygon(new Point(x, y), ConvertPointsForMatrix(matrixPrecision, hotspot.Points)))
                                {
                                    _EventsMatrix[x, y] = (byte)i;
                                }
                                i++;
                            }
                        }
                    }
                }
                _EventsCreated = true;
            }
        }

        /// <summary>
        /// Charge une matrice de régions
        /// </summary>
        /// <param name="dimensions">Dimensions de la scène</param>
        /// <param name="hotspots">Liste de hotspots</param>
        /// <param name="matrixPrecision"></param>
        /// <returns>Matrice d'octets</returns>
        public void LoadRegionMatrix(System.Drawing.Size dimensions, List<VO_StageRegion> hotspots, int matrixPrecision)
        {
            if (!_RegionsCreated)
            {
                int height = dimensions.Height / matrixPrecision;
                int width = dimensions.Width / matrixPrecision;
                if (File.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + _Stage.Id.ToString() + "_r"))
                {
                    StreamReader myFile = new StreamReader(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + _Stage.Id.ToString() + "_r");
                    for (int y = 0; y < height; y++)
                    {
                        string line = myFile.ReadLine();
                        for (int x = 0; x < width; x++)
                        {
                            int nX = 3 * x;
                            _RegionsMatrix[x, y] = Convert.ToByte(line[nX].ToString() + line[nX + 1].ToString() + line[nX + 2].ToString());
                        }
                    }
                    myFile.Close();
                }
                else
                {
                    for (int y = 0; y <= height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            _RegionsMatrix[x, y] = (byte)_Stage.Region;
                            foreach (VO_StageRegion hotspot in hotspots)
                            {
                                if (Tools.PointInPolygon(new Point(x, y), ConvertPointsForMatrix(matrixPrecision, hotspot.Points)))
                                {
                                    _RegionsMatrix[x, y] = (byte)ConvertTools.CastInt(hotspot.Ratio);
                                }
                            }
                        }
                    }
                }
                _RegionsCreated = true;
            }
        }

        /// <summary>
        /// Convertis les points au niveau de précision de la matrice
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private System.Drawing.Point[] ConvertPointsForMatrix(int matrixPrecision, System.Drawing.Point[] points)
        {
            if (matrixPrecision == 1)
                return points;
            System.Drawing.Point[] newPoints = new System.Drawing.Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                newPoints[i] = new System.Drawing.Point(points[i].X / matrixPrecision, points[i].Y / matrixPrecision);
            }
            return newPoints;
        }
        #endregion
    }
}
