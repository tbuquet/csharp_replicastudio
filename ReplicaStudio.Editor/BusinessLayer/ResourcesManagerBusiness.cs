using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.IO;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.Editor.BusinessLayer
{
    public class ResourcesManagerBusiness : BaseBusiness
    {
        #region Properties
        List<VO_Directory> DirectoriesList = new List<VO_Directory>();
        #endregion

        #region Constructor
        public ResourcesManagerBusiness()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Default method for binding all directories
        /// </summary>
        /// <param name="pProject"></param>
        /// <returns></returns>
        public List<VO_Directory> BindListFolder(VO_Project project)
        {
            DirectoriesList = InitListFolder(project);
            return DirectoriesList;
        }

        /// <summary>
        /// Return the filtered Directories List
        /// </summary>
        /// <param name="pProject"></param>
        /// <param name="pFilter"></param>
        /// <returns></returns>
        public List<VO_Directory> BindListFolder(VO_Project project, string filter)
        {
            DirectoriesList = InitListFolder(project);
            List<VO_Directory> filteredDir = new List<VO_Directory>();
            filteredDir.Add(DirectoriesList.Find(dir => dir.Name == filter));
            return filteredDir;
        }

        /// <summary>
        /// Create VO_Directory of each folder locate in the resources manager 
        /// </summary>
        /// <param name="pProject"></param>
        /// <returns></returns>
        private List<VO_Directory> InitListFolder(VO_Project project)
        {
            List<VO_Directory> rmFolders = new List<VO_Directory>();
            VO_Directory animationsCharactere = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERANIMATIONS,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory animationsFaces = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_CHARACTERFACES,
                Path = "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_CHARACTERFACES,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory animationsIcons = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_ICONS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_ICONS,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory animationsMenus = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_MENUS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_MENUS,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory animationsObjects = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_ANIMATIONS + "\\" + GlobalConstants.PROJECT_DIR_OBJECTANIMATIONS,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };

            VO_Directory decors = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_DECORS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_DECORS,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory fonts = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_FONTS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_FONTS,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory lifeBar = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_LIFEBAR,
                Path = "\\" + GlobalConstants.PROJECT_DIR_LIFEBAR,
                Extensions = "Images PNG (*.png)|*.png|Images JPEG (*.jpg)|*.jpg"
            };
            VO_Directory musics = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_MUSICS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_MUSICS,
                Extensions ="Musics (*.mp3)|" +  GlobalConstants.EXT_MUSIC_PATTERN
            };
            VO_Directory effects = new VO_Directory()
           {
               Name = GlobalConstants.PROJECT_DIR_EFFECTS,
               Path = "\\" + GlobalConstants.PROJECT_DIR_SOUNDS + "\\" + GlobalConstants.PROJECT_DIR_EFFECTS,
               Extensions = "Sounds (*.mp3)|" + GlobalConstants.EXT_SOUNDS_PATTERN
           };

            VO_Directory voices = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_VOICES,
                Path = "\\" + GlobalConstants.PROJECT_DIR_SOUNDS + "\\" + GlobalConstants.PROJECT_DIR_VOICES,
                Extensions = "Sounds (*.mp3)|" + GlobalConstants.EXT_SOUNDS_PATTERN
            };

            VO_Directory gui = new VO_Directory()
            {
                Name = GlobalConstants.PROJECT_DIR_GUIS,
                Path = "\\" + GlobalConstants.PROJECT_DIR_GUIS,
                Extensions = "Images PNG (*.png)|*.png"
            };

            rmFolders.Add(animationsCharactere);
            rmFolders.Add(animationsFaces);
            rmFolders.Add(animationsIcons);
            rmFolders.Add(animationsMenus);
            rmFolders.Add(animationsObjects);
            rmFolders.Add(decors);
            rmFolders.Add(fonts);
            rmFolders.Add(lifeBar);
            rmFolders.Add(musics);
            rmFolders.Add(effects);
            rmFolders.Add(voices);
            rmFolders.Add(gui);
            return rmFolders;
        }
        #endregion
    }
}
