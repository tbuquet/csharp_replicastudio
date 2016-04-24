using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    public class PathTools
    {
        /// <summary>
        /// Récupérer une URI d'un dossier en fonction de la ressource demandée
        /// </summary>
        /// <param name="pPath">Type ProjectPath</param>
        /// <returns>Chemin URI</returns>
        public static string GetProjectPath(Enums.ProjectPath path)
        {
            switch (path)
            {
                case Enums.ProjectPath.CharAnimations:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS + "\\";
                case Enums.ProjectPath.CharFaces:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERFACES + "\\";
                case Enums.ProjectPath.Icons:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_ICONS + "\\";
                case Enums.ProjectPath.Menus:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_MENUS + "\\";
                case Enums.ProjectPath.ObjectAnimations:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS + "\\";
                case Enums.ProjectPath.Manuals:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_MANUALS + "\\";
                case Enums.ProjectPath.Stages:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_STAGES + "\\";
                case Enums.ProjectPath.Resources:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\";
                case Enums.ProjectPath.Decors:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_DECORS + "\\";
                case Enums.ProjectPath.Musics:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_MUSICS + "\\";
                case Enums.ProjectPath.Fonts:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_FONTS + "\\";
                case Enums.ProjectPath.GUI:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_GUIS + "\\";
                case Enums.ProjectPath.LifeBar:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_LIFEBAR + "\\";
                case Enums.ProjectPath.Sounds:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_SOUNDS + "\\" + GlobalConstants.PROJECT_DIR_EFFECTS + "\\";
                case Enums.ProjectPath.Voice:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_SOUNDS + "\\" + GlobalConstants.PROJECT_DIR_VOICES + "\\";
                case Enums.ProjectPath.Matrixes:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_MATRIXES + "\\";
            }
            return string.Empty;
        }

        /// <summary>
        /// Récupérer une URI d'un dossier en fonction de la ressource demandée
        /// </summary>
        /// <param name="pPath">Type ProjectPath</param>
        /// <returns>Chemin URI</returns>
        public static string GetProjectPath(string path)
        {
            switch (path)
            {
                case GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS + "\\";
                case GlobalConstants.PROJECT_DIR_CHARACTERFACES:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERFACES + "\\";
                case GlobalConstants.PROJECT_DIR_ICONS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_ICONS + "\\";
                case GlobalConstants.PROJECT_DIR_MENUS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_MENUS + "\\";
                case GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS + "\\";
                case GlobalConstants.PROJECT_DIR_MANUALS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_MANUALS + "\\";
                case GlobalConstants.PROJECT_DIR_STAGES:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_STAGES + "\\";
                case GlobalConstants.PROJECT_DIR_RESOURCES:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\";
                case GlobalConstants.PROJECT_DIR_DECORS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_DECORS + "\\";
                case GlobalConstants.PROJECT_DIR_MUSICS:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_MUSICS + "\\";
            }
            return string.Empty;
        }

        /// <summary>
        /// Récupérer une URI d'un dossier en fonction de la ressource demandée
        /// </summary>
        /// <param name="pPath">Type d'animation</param>
        /// <returns>Chemin URI</returns>
        public static string GetProjectPath(Enums.AnimationType path)
        {
            switch (path)
            {
                case Enums.AnimationType.CharacterAnimation:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS + "\\";
                case Enums.AnimationType.CharacterFace:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERFACES + "\\";
                case Enums.AnimationType.IconAnimation:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_ICONS + "\\";
                case Enums.AnimationType.Menu:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_MENUS + "\\";
                case Enums.AnimationType.ObjectAnimation:
                    return GameCore.Instance.Game.Project.RootPath + GlobalConstants.PROJECT_DIR_RESOURCES + "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS + "\\";
            }
            return string.Empty;
        }
    }
}
