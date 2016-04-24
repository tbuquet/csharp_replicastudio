using System;
using System.Collections.Generic;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Reflection;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Drawing.Imaging;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;

namespace ReplicaStudio.Editor.TransverseLayer.Managers
{
    /// <summary>
    /// Gestionnaire de ressources image
    /// </summary>
    public static class ImageManager
    {
        #region Members
        /// <summary>
        /// Liste d'images ressources
        /// </summary>
        static Dictionary<string, Image> _ImagesResources;

        /// <summary>
        /// Liste de backgrounds
        /// </summary>
        static Dictionary<string, Image> _ImagesBackgrounds;

        /// <summary>
        /// Liste des décors
        /// </summary>
        static Dictionary<string, Image> _ImagesStageDecors;

        /// <summary>
        /// Liste d'objets anim
        /// </summary>
        static Dictionary<Guid, Image> _ImagesStageAnims;

        /// <summary>
        /// Liste d'objets chars
        /// </summary>
        static Dictionary<Guid, Image> _ImagesStageChars;

        /// <summary>
        /// Image non trouvée
        /// </summary>
        static Image _NotFoundPicture;
        #endregion

        #region Properties
        public static Image NotFoundPicture
        {
            get
            {
                if(_NotFoundPicture == null)
                    CreateNotFoundPicture();
                return _NotFoundPicture;
            }
        }
        #endregion

        #region Methods
        #region Tools
        /// <summary>
        /// Charge une image en mémoire et libère le lock sur le fichier
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns></returns>
        private static Image FromFileThenClose(string url)
        {
            Image i = Image.FromFile(url);
            Image output = new Bitmap(i);
            i.Dispose();
            return output;
        }

        /// <summary>
        /// Récupère les dimensions d'une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Size</returns>
        private static Size GetDimensionsOfAPicture(string url)
        {
            Image pic = FromFileThenClose(url);
            Size size = pic.Size;
            pic.Dispose();
            return size;
        }
        #endregion

        #region Reset
        /// <summary>
        /// Si erreur de mémoire, on reset la mémoire du ResourcesManager
        /// </summary>
        public static void ResetResources()
        {
            LogTools.WriteDebug(Logs.MANAGER_RESETING_MANAGER);

            //_ImagesResources
            if (_ImagesResources != null)
            {
                foreach (Image image in _ImagesResources.Values)
                {
                    image.Dispose();
                }
            }
            _ImagesResources = new Dictionary<string, Image>();

            //_ImagesBackgrounds
            if (_ImagesBackgrounds != null)
            {
                foreach (Image image in _ImagesBackgrounds.Values)
                {
                    image.Dispose();
                }
            }
            _ImagesBackgrounds = new Dictionary<string, Image>();

            ResetStageResources();
        }

        /// <summary>
        /// Si erreur de mémoire, on reset la mémoire du ResourcesManager
        /// </summary>
        public static void ResetStageResources()
        {
            //_ImagesStageDecors
            if (_ImagesStageDecors != null)
            {
                foreach (Image image in _ImagesStageDecors.Values)
                {
                    image.Dispose();
                }
            }
            _ImagesStageDecors = new Dictionary<string, Image>();

            //_ImagesStageAnims
            if (_ImagesStageAnims != null)
            {
                foreach (Image image in _ImagesStageAnims.Values)
                {
                    image.Dispose();
                }
            }
            _ImagesStageAnims = new Dictionary<Guid, Image>();

            //_ImagesStageChars
            if (_ImagesStageChars != null)
            {
                foreach (Image image in _ImagesStageChars.Values)
                {
                    image.Dispose();
                }
            }
            _ImagesStageChars = new Dictionary<Guid, Image>();
        }
        #endregion

        #region Create
        /// <summary>
        /// Image non trouvée
        /// </summary>
        private static void CreateNotFoundPicture()
        {
            if (_NotFoundPicture == null)
                _NotFoundPicture = new Bitmap(1, 1);//_NotFoundPicture = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GlobalConstants.ANIMATION_EMPTY_RESOURCE));
        }

        /// <summary>
        /// Crée une nouvelle image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void CreateNewImageResource(string url)
        {
            //Dictionnaire d'image
            if (_ImagesResources == null)
                _ImagesResources = new Dictionary<string, Image>();

            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return;

            //Ajout de l'image
            if (!_ImagesResources.ContainsKey(url))
            {
                try
                {
                    _ImagesResources.Add(url, FromFileThenClose(url));
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED,url,Logs.MANAGER_RESOURCES));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    ResetResources();
                    CreateNewImageResource(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                    _ImagesResources.Add(url, NotFoundPicture);
                }
            }
        }

        /// <summary>
        /// Crée une nouvelle image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void CreateNewImageStageDecor(string url)
        {
            //Dictionnaire d'image
            if (_ImagesStageDecors == null)
                _ImagesStageDecors = new Dictionary<string, Image>();

            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return;

            //Ajout de l'image
            if (!_ImagesStageDecors.ContainsKey(url))
            {
                try
                {
                    _ImagesStageDecors.Add(url, FromFileThenClose(url));
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, url, Logs.MANAGER_DECORS));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    ResetResources();
                    CreateNewImageStageDecor(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                    _ImagesStageDecors.Add(url, NotFoundPicture);
                }
            }
        }

        /// <summary>
        /// Crée une nouvelle image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void CreateNewImageStageAnim(Guid value)
        {
            //Dictionnaire d'image
            if (_ImagesStageAnims == null)
                _ImagesStageAnims = new Dictionary<Guid, Image>();

            //Pas d'image envoyée
            if (value == new Guid())
                return;

            //Ajout de l'image
            if (!_ImagesStageAnims.ContainsKey(value))
            {
                try
                {
                    VO_Animation animObject = GameCore.Instance.GetAnimationById(value);
                    _ImagesStageAnims.Add(value, FromFileThenClose(PathTools.GetProjectPath(Enums.AnimationType.ObjectAnimation) + animObject.ResourcePath));
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, value.ToString(), Logs.MANAGER_ANIMS));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    ResetResources();
                    CreateNewImageStageAnim(value);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, value));
                    LogTools.WriteDebug(e.Message);
                    _ImagesStageAnims.Add(value, NotFoundPicture);
                }
            }
        }

        /// <summary>
        /// Crée une nouvelle image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void CreateNewImageStageCharacter(Guid value)
        {
            //Dictionnaire d'image
            if (_ImagesStageChars == null)
                _ImagesStageChars = new Dictionary<Guid, Image>();

            //Pas d'image envoyée
            if (value == new Guid())
                return;

            //Ajout de l'image
            if (!_ImagesStageChars.ContainsKey(value))
            {
                try
                {
                    VO_Character charObject = GameCore.Instance.GetCharacterById(value);
                    VO_Animation animObject = GameCore.Instance.GetCharAnimationById(value, charObject.StandingAnim);
                    _ImagesStageChars.Add(value, FromFileThenClose(PathTools.GetProjectPath(Enums.AnimationType.CharacterAnimation) + animObject.ResourcePath));
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, value.ToString(), Logs.MANAGER_CHARS));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    ResetResources();
                    CreateNewImageStageCharacter(value);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, value));
                    LogTools.WriteDebug(e.Message);
                    _ImagesStageChars.Add(value, NotFoundPicture);
                }
            }
        }

        /// <summary>
        /// Crée une nouvelle image background
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void CreateNewImageBackground(VO_BackgroundSerial serial)
        {
            //Dictionnaire d'image
            if (_ImagesBackgrounds == null)
                _ImagesBackgrounds = new Dictionary<string, Image>();

            //Ajout de l'image
            string serialS = serial.ToString();
            if (!_ImagesBackgrounds.ContainsKey(serialS))
            {
                try
                {
                    Image mainBackground = null;

                    if (serial.Padding == 0)
                    {
                        mainBackground = new Bitmap(serial.Size.Width, serial.Size.Height, EditorConstants.PERF_EDITOR_BITSPERPIXEL);
                        Graphics graphic = Graphics.FromImage(mainBackground);
                        graphic.FillRectangle(EditorHelper.Instance.TransparentBrushes[serial.BlockSize], new Rectangle(new Point(0, 0), serial.Size));
                        graphic.Dispose();
                    }
                    else
                    {
                        int padding = serial.Padding * 2;
                        mainBackground = new Bitmap(serial.Size.Width + padding, serial.Size.Height + padding, EditorConstants.PERF_EDITOR_BITSPERPIXEL);
                        Graphics graphics = Graphics.FromImage(mainBackground);
                        graphics.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(new Point(0, 0), new Size(serial.Size.Width + padding, serial.Size.Height + padding)));

                        Image temp = new Bitmap(serial.Size.Width, serial.Size.Height, EditorConstants.PERF_EDITOR_BITSPERPIXEL);
                        Graphics graphicBackground = Graphics.FromImage(temp);
                        graphicBackground.FillRectangle(EditorHelper.Instance.TransparentBrushes[serial.BlockSize], new Rectangle(new Point(0, 0), serial.Size));

                        graphics.DrawImage(temp, new Rectangle(new Point(serial.Padding, serial.Padding), serial.Size));

                        temp.Dispose();
                        graphicBackground.Dispose();
                        graphics.Dispose();
                    }

                    _ImagesBackgrounds.Add(serialS, mainBackground);
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, serial.ToString(), Logs.MANAGER_BACKGROUNDS));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    ResetResources();
                    CreateNewImageBackground(serial);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, serial));
                    LogTools.WriteDebug(e.Message);
                    _ImagesBackgrounds.Add(serialS, NotFoundPicture);
                }
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Supprimer une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void DeleteImageResource(string url)
        {
            if (_ImagesResources.ContainsKey(url))
            {
                try
                {
                    _ImagesResources[url].Dispose();
                    _ImagesResources.Remove(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }

        /// <summary>
        /// Supprimer une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void DeleteImageStageDecor(string url)
        {
            if (_ImagesStageDecors.ContainsKey(url))
            {
                try
                {
                    _ImagesStageDecors[url].Dispose();
                    _ImagesStageDecors.Remove(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }

        /// <summary>
        /// Supprimer une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void DeleteImageStageAnim(Guid value)
        {
            if (_ImagesStageAnims.ContainsKey(value))
            {
                try
                {
                    _ImagesStageAnims[value].Dispose();
                    _ImagesStageAnims.Remove(value);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, value));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }

        /// <summary>
        /// Supprimer une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void DeleteImageStageChar(Guid value)
        {
            if (_ImagesStageChars.ContainsKey(value))
            {
                try
                {
                    _ImagesStageChars[value].Dispose();
                    _ImagesStageChars.Remove(value);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, value));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }

        /// <summary>
        /// Supprimer une image background
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void DeleteImageBackground(VO_BackgroundSerial serial)
        {
            string serialS = serial.ToString();
            if (_ImagesBackgrounds.ContainsKey(serialS))
            {
                try
                {
                    _ImagesBackgrounds[serialS].Dispose();
                    _ImagesBackgrounds.Remove(serialS);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, serial.ToString()));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Récupérer une image resource
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Image GetImageResource(string url)
        {
            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return NotFoundPicture;

            //Dictionnaire d'image
            if (_ImagesResources == null)
                _ImagesResources = new Dictionary<string, Image>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_ImagesResources.ContainsKey(url))
                CreateNewImageResource(url);

            //Renvoie de l'image
            try
            {
                return _ImagesResources[url];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, url));
                LogTools.WriteDebug(e.Message);
                return NotFoundPicture;
            }
        }

        /// <summary>
        /// Récupérer une image resource
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Image GetImageStageDecor(string url)
        {
            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return NotFoundPicture;

            //Dictionnaire d'image
            if (_ImagesStageDecors == null)
                _ImagesStageDecors = new Dictionary<string, Image>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_ImagesStageDecors.ContainsKey(url))
                CreateNewImageStageDecor(url);

            //Renvoie de l'image
            try
            {
                return _ImagesStageDecors[url];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, url));
                LogTools.WriteDebug(e.Message);
                return NotFoundPicture;
            }
        }

        /// <summary>
        /// Récupérer une image resource
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Image GetImageStageAnim(Guid value)
        {
            //Pas d'image envoyée
            if (value == new Guid())
                return NotFoundPicture;

            //Dictionnaire d'image
            if (_ImagesStageAnims == null)
                _ImagesStageAnims = new Dictionary<Guid, Image>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_ImagesStageAnims.ContainsKey(value))
                CreateNewImageStageAnim(value);

            //Renvoie de l'image
            try
            {
                return _ImagesStageAnims[value];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, value));
                LogTools.WriteDebug(e.Message);
                return NotFoundPicture;
            }
        }

        /// <summary>
        /// Récupérer une image resource
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Image GetImageStageChar(Guid value)
        {
            //Pas d'image envoyée
            if (value == new Guid())
                return NotFoundPicture;

            //Dictionnaire d'image
            if (_ImagesStageChars == null)
                _ImagesStageChars = new Dictionary<Guid, Image>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_ImagesStageChars.ContainsKey(value))
                CreateNewImageStageCharacter(value);

            //Renvoie de l'image
            try
            {
                return _ImagesStageChars[value];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, value));
                LogTools.WriteDebug(e.Message);
                return NotFoundPicture;
            }
        }

        /// <summary>
        /// Récupérer une image background
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Image GetImageBackground(VO_BackgroundSerial serial)
        {
            //Pas d'image envoyée
            if (serial == null)
                return NotFoundPicture;

            //Dictionnaire d'image
            if (_ImagesBackgrounds == null)
                _ImagesBackgrounds = new Dictionary<string, Image>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            string serialS = serial.ToString();
            if (!_ImagesBackgrounds.ContainsKey(serialS))
                CreateNewImageBackground(serial);

            //Renvoie de l'image
            try
            {
                return _ImagesBackgrounds[serialS];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, serial.ToString()));
                LogTools.WriteDebug(e.Message);
                return NotFoundPicture;
            }
        }
        #endregion
        #endregion
    }
}
