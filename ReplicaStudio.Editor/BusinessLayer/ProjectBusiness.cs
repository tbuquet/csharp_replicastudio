using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using System.IO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.DatasLayer;
using System.Xml.Serialization;
using System.Xml;
using ReplicaStudio.Editor.BusinessLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using System.Drawing;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.BusinessLayer;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace ReplicaStudio.BusinessLayer
{
    /// <summary>
    /// Classe métier qui gère les fonctions principales de création, sauvegarde, chargement des projets.
    /// </summary>
    public class ProjectBusiness : BaseBusiness
    {
        #region Members
        /// <summary>
        /// Game Process
        /// </summary>
        private Process _GameProcess;

        /// <summary>
        /// External Tool Process
        /// </summary>
        private Process _ExternalTool;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public ProjectBusiness()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge les résolutions compatibles avec l'application.
        /// </summary>
        /// <returns>Liste de résolutions</returns>
        public List<VO_Resolution> LoadResolutions()
        {
            List<VO_Resolution> resolutions = new List<VO_Resolution>();
            resolutions.Add(new VO_Resolution(320, 240, 1));
            resolutions.Add(new VO_Resolution(576, 420, 1));
            resolutions.Add(new VO_Resolution(640, 480, 1));
            resolutions.Add(new VO_Resolution(720, 576, 1));
            resolutions.Add(new VO_Resolution(720, 480, 2));
            resolutions.Add(new VO_Resolution(800, 600, 2));
            resolutions.Add(new VO_Resolution(920, 600, 2));
            resolutions.Add(new VO_Resolution(1024, 768, 2));
            resolutions.Add(new VO_Resolution(1280, 720, 2));
            resolutions.Add(new VO_Resolution(1280, 768, 2));
            resolutions.Add(new VO_Resolution(1280, 800, 2));
            resolutions.Add(new VO_Resolution(1360, 768, 4));
            resolutions.Add(new VO_Resolution(1440, 900, 4));
            resolutions.Add(new VO_Resolution(1600, 900, 4));
            resolutions.Add(new VO_Resolution(1600, 1200, 4));
            resolutions.Add(new VO_Resolution(1680, 1050, 4));
            resolutions.Add(new VO_Resolution(1920, 1080, 4));
            resolutions.Add(new VO_Resolution(1920, 1200, 4));
            return resolutions;
        }

        /// <summary>
        /// Création d'un projet
        /// </summary>
        /// <param name="pProject">VO_Project</param>
        public void CreateProject(VO_Project project)
        {
            //Nouvelles données projet.
            GameCore.Instance.ResetGameCore();
            GameCore.Instance.Game.Project = project;
            GameCore.Instance.Game.Project.ProjectFileName = project.Title;
            GameCore.Instance.Game.Project.Version = GlobalConstants.PROJECT_VERSION;
            GameCore.Instance.Game.Project.BetaVersion = GlobalConstants.BETA_VERSION;
            GameCore.Instance.Game.Project.RootPath = project.RootPath += "\\" + ValidationTools.NormalizeFolderName(project.Title) + "\\";
            GameCore.Instance.Game.Project.MovementDirections = 4;

            //Création des dossiers du projet
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_MANUALS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERFACES);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_ICONS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_MENUS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_DECORS);
            //Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_FONTS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_LIFEBAR);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_MUSICS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_SOUNDS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_SOUNDS + "\\" + GlobalConstants.PROJECT_DIR_VOICES);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_SOUNDS + "\\" + GlobalConstants.PROJECT_DIR_EFFECTS);
            Directory.CreateDirectory(project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_GUIS);

            project.GameOverMusic = new VO_Music();
            project.MainMenuMusic = new VO_Music();
            project.GameOver = ObjectsFactory.CreateScript(Enums.ScriptType.GameOverEvent);
            
            //Création de Map de base.
            EditorHelper.Instance.LastOrdinalLayer = 0;
            project.GameOver = ObjectsFactory.CreateScript(Enums.ScriptType.GameOverEvent);

            //Création de l'action "Aller"
            VO_Action go = ObjectsFactory.CreateAction(new Guid(GlobalConstants.ACTION_GO_ID));
            go.Title = GlobalConstants.ACTION_GO;
            go.GoAction = true;

            //Création de l'action "Utiliser"
            VO_Action use = ObjectsFactory.CreateAction(new Guid(GlobalConstants.ACTION_USE_ID));
            use.Title = GlobalConstants.ACTION_USE;
            use.UseAction = true;

            //Création des menus
            ObjectsFactory.CreateMenu();

            //Terminology
            ObjectsFactory.CreateTerminology();
        }

        /// <summary>
        /// Sauvegarde l'intégralité du projet (xml général et maps)
        /// </summary>
        public void SaveProject()
        {
            //Cleanup Projet
            ValidationTools.CleanupProject();

            Type[] ScriptTypes = AppTools.GetScriptTypes();

            XmlSerializer XML_Project = new XmlSerializer(typeof(VO_Game), ScriptTypes);

            TextWriter text = new StringWriter();
            XmlWriter xmlWriter = new XmlTextWriter(text);

            //Début de l'écriture
            xmlWriter.WriteStartElement(XML.NODE_ROOT);
            XML_Project.Serialize(xmlWriter, GameCore.Instance.Game);
            xmlWriter.WriteEndElement();

            StreamWriter stream = File.CreateText(GameCore.Instance.Game.Project.RootPath + "\\" + ValidationTools.NormalizeFolderName(GameCore.Instance.Game.Project.ProjectFileName) + GlobalConstants.EXT_PROJECT);
            stream.Write(text.ToString());
            stream.Close();

            xmlWriter.Close();
            text.Close();
        }

        /// <summary>
        /// Lance l'export
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Enums.ExportState LaunchExport(string path)
        {
            //Améliorations
            //Optimisation matrices
            /*foreach (VO_Stage valueStage in GameCore.Instance.Game.Stages)
            {
                CreateWalkableMatrix(valueStage.Dimensions, valueStage, GameCore.Instance.Game.Project.Resolution.MatrixPrecision);
                CreateEventsMatrix(valueStage, valueStage.Dimensions, valueStage.ListHotSpots, GameCore.Instance.Game.Project.Resolution.MatrixPrecision);
                CreateRegionMatrix(valueStage, valueStage.Dimensions, valueStage.ListRegions, GameCore.Instance.Game.Project.Resolution.MatrixPrecision);
            }*/
            //TODO

            // On sauvegarde l'objet
            string finalPath = path + "\\" + GameCore.Instance.Game.Project.ProjectFileName + "\\";
            string finalApp = finalPath + GameCore.Instance.Game.Project.ProjectFileName + GlobalConstants.EXT_EXPORTED_GAME;
            if (!Directory.Exists(finalPath))
                Directory.CreateDirectory(finalPath);
            AppTools.SaveObjectToFile(GameCore.Instance.Game, finalApp);
            AppTools.CopyFolder(GameCore.Instance.Game.Project.RootPath, finalPath);
            File.Delete(finalPath + GameCore.Instance.Game.Project.ProjectFileName + GlobalConstants.EXT_PROJECT);

            return Enums.ExportState.OK;
        }

        /// <summary>
        /// Charger un projet
        /// </summary>
        /// <param name="pPath">Lien vers le fichier XML de chargement.</param>
        public bool LoadProject(string path)
        {
            return GameCore.Instance.LoadProject(path);
        }

        /// <summary>
        /// Vérifie si un projet à créer existe à l'emplacement indiqué.
        /// </summary>
        /// <param name="pFile">Chemin vers le projet</param>
        /// <param name="pTitle">Titre du projet</param>
        /// <returns>True si le projet existe, false sinon</returns>
        public bool CheckIfProjectExist(string file, string title)
        {
            string normTitle = ValidationTools.NormalizeFolderName(title);
            if (File.Exists(file + "\\" + normTitle + "\\" + normTitle + ".pcs"))
                return true;
            return false;
        }

        /// <summary>
        /// Créer matrix walkable
        /// </summary>
        /// <param name="dimensions"></param>
        /// <param name="stage"></param>
        /// <param name="matrixPrecision"></param>
        private void CreateWalkableMatrix(System.Drawing.Size dimensions, VO_Stage stage, int matrixPrecision)
        {
            if (!Directory.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes)))
                Directory.CreateDirectory(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes));
            StreamWriter sw = new StreamWriter(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + stage.Id.ToString() + "_w");

            byte[,] matrix = new byte[4096, 4096];

            int height = dimensions.Height / matrixPrecision;
            int width = dimensions.Width / matrixPrecision;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[x, y] = 0;
                    byte i = 1;
                    foreach (VO_Layer layer in stage.ListLayers)
                    {
                        foreach (VO_StageHotSpot hotspot in layer.ListWalkableAreas)
                        {
                            if (FormsTools.PointInPolygon(new Point(x, y), ConvertPointsForMatrix(matrixPrecision, hotspot.Points)))
                            {
                                matrix[x, y] = i;
                                break;
                            }
                        }
                        i++;
                    }
                    sw.Write(GetValueFromByte(matrix[x, y]));
                }
                sw.Write("\r\n");
            }
            sw.Close();
        }

        /// <summary>
        /// Charge une matrice d'evenements
        /// </summary>
        /// <param name="dimensions">Dimensions de la scène</param>
        /// <param name="hotspots">Liste de hotspots</param>
        /// <param name="matrixPrecision"></param>
        /// <returns>Matrice d'octets</returns>
        private void CreateEventsMatrix(VO_Stage stage, System.Drawing.Size dimensions, List<VO_StageHotSpot> hotspots, int matrixPrecision)
        {
            if (!Directory.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes)))
                Directory.CreateDirectory(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes));
            StreamWriter sw = new StreamWriter(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + stage.Id.ToString() + "_e");

            byte[,] matrix = new byte[4096, 4096];

            int height = dimensions.Height / matrixPrecision;
            int width = dimensions.Width / matrixPrecision;
            for (int y = 0; y <= height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[x, y] = 0;
                    int i = 1;
                    foreach (VO_StageHotSpot hotspot in hotspots)
                    {

                        if (FormsTools.PointInPolygon(new Point(x, y), ConvertPointsForMatrix(matrixPrecision, hotspot.Points)))
                        {
                            matrix[x, y] = (byte)i;
                        }
                        i++;
                    }
                    sw.Write(GetValueFromByte(matrix[x, y]));
                }
                sw.Write("\r\n");
            }
            sw.Close();
        }

        /// <summary>
        /// Charge une matrice de régions
        /// </summary>
        /// <param name="dimensions">Dimensions de la scène</param>
        /// <param name="hotspots">Liste de hotspots</param>
        /// <param name="matrixPrecision"></param>
        /// <returns>Matrice d'octets</returns>
        public void CreateRegionMatrix(VO_Stage stage, System.Drawing.Size dimensions, List<VO_StageRegion> hotspots, int matrixPrecision)
        {
            if (!Directory.Exists(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes)))
                Directory.CreateDirectory(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes));
            StreamWriter sw = new StreamWriter(PathTools.GetProjectPath(Enums.ProjectPath.Matrixes) + stage.Id.ToString() + "_r");

            byte[,] matrix = new byte[4096, 4096];

            int height = dimensions.Height / matrixPrecision;
            int width = dimensions.Width / matrixPrecision;
            for (int y = 0; y <= height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[x, y] = (byte)stage.Region;
                    foreach (VO_StageRegion hotspot in hotspots)
                    {
                        if (FormsTools.PointInPolygon(new Point(x, y), ConvertPointsForMatrix(matrixPrecision, hotspot.Points)))
                        {
                            matrix[x, y] = (byte)ConvertTools.CastInt(hotspot.Ratio);
                        }
                    }
                    sw.Write(GetValueFromByte(matrix[x, y]));
                }
                sw.Write("\r\n");
            }
            sw.Close();
                
        }

        /// <summary>
        /// GetValueFromByte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetValueFromByte(byte value)
        {
            string byteString = value.ToString();
            if (byteString.Length == 1)
            {
                byteString = "00" + byteString;
            }
            else if (byteString.Length == 2)
            {
                byteString = "0" + byteString;
            }
            return byteString;
        }

        /// <summary>
        /// Convertis les points au niveau de précision de la matrice
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private Point[] ConvertPointsForMatrix(int matrixPrecision, Point[] points)
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

        /// <summary>
        /// Lancer un projet
        /// </summary>
        /// <param name="fullscreen"></param>
        public void LaunchProject(bool fullscreen)
        {
            if(_GameProcess == null)
                _GameProcess = new Process();
            _GameProcess.StartInfo.FileName = EditorSettings.Instance.ViewerPath;
            _GameProcess.StartInfo.Arguments = "\"" + GameCore.Instance.Game.Project.RootPath + GameCore.Instance.Game.Project.ProjectFileName + ".pcs\" -sound";
            if (fullscreen)
                _GameProcess.StartInfo.Arguments += " -fullscreen";
            if (EditorSettings.Instance.VSync)
                _GameProcess.StartInfo.Arguments += " -vsync";
            _GameProcess.StartInfo.UseShellExecute = false;
            _GameProcess.StartInfo.RedirectStandardOutput = false;
            _GameProcess.Start();
        }

        /// <summary>
        /// Lancer un programme externe
        /// </summary>
        /// <param name="path"></param>
        public void LaunchExternalTool(string path)
        {
            if(_ExternalTool == null)
                _ExternalTool = new Process();
            _ExternalTool.StartInfo.FileName = path;
            _ExternalTool.StartInfo.UseShellExecute = false;
            _ExternalTool.StartInfo.RedirectStandardOutput = false;
            _ExternalTool.Start();
        }
        #endregion
    }
}
