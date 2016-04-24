using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// ImageManager pour Viewer
    /// </summary>
    public class ImageManager
    {
        #region Members
        #region Statiques
        private static GraphicsDevice _GraphicsDevice;
        private static SpriteBatch _SpriteBatch;
        private static BlendState _BlendColor;
        private static BlendState _BlendAlpha;

        private static ImageManager _CurrentStage;
        private static Dictionary<Guid, ImageManager> _Stages;
        #endregion

        /// <summary>
        /// Ressources permanentes
        /// </summary>
        private static Dictionary<string, Texture2D> _PermanentResources;

        /// <summary>
        /// Ressources de l'écran
        /// </summary>
        private Dictionary<string, Texture2D> _ScreenResources;

        /// <summary>
        /// Ressources colorées
        /// </summary>
        private Dictionary<string, Texture2D> _ColoredResources;
        #endregion

        #region Properties
        /// <summary>
        /// Récupérer stage courant
        /// </summary>
        public static ImageManager CurrentStage
        {
            get
            {
                return _CurrentStage;
            }
        }

        public static ImageManager Stages(Guid id)
        {
            if (_Stages == null)
                _Stages = new Dictionary<Guid, ImageManager>();
            if (!_Stages.ContainsKey(id))
                _Stages.Add(id, new ImageManager());
            return _Stages[id];
        }

        public static void SetCurrentStage(Guid id)
        {
            if (_Stages == null)
                _Stages = new Dictionary<Guid, ImageManager>();
            if (!_Stages.ContainsKey(id))
                _Stages.Add(id, new ImageManager());
            _CurrentStage = _Stages[id];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialisation du Manager
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="spritebatch"></param>
        public static void InitImageManager(GraphicsDevice graphicsDevice, SpriteBatch spritebatch)
        {
            _GraphicsDevice = graphicsDevice;
            _SpriteBatch = spritebatch;
        }

        #region Tools
        /// <summary>
        /// Charge une image en mémoire et libère le lock sur le fichier
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns></returns>
        private static Texture2D FromFileThenClose(string url)
        {
            Texture2D file = null;
            RenderTarget2D result = null;

            FileStream fileOpen = File.Open(url, FileMode.Open);
            file = Texture2D.FromStream(_GraphicsDevice, fileOpen);
            fileOpen.Close();

            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(_GraphicsDevice, file.Width, file.Height);
            _GraphicsDevice.SetRenderTarget(result);
            _GraphicsDevice.Clear(Color.Black);


            //Multiply each color by the source alpha, and write in just the color values into the final texture
            if (_BlendColor == null)
            {
                _BlendColor = new BlendState();
                _BlendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;
                _BlendColor.AlphaDestinationBlend = Blend.Zero;
                _BlendColor.ColorDestinationBlend = Blend.Zero;
                _BlendColor.AlphaSourceBlend = Blend.SourceAlpha;
                _BlendColor.ColorSourceBlend = Blend.SourceAlpha;
            }

            _SpriteBatch.Begin(SpriteSortMode.Immediate, _BlendColor);
            _SpriteBatch.Draw(file, file.Bounds, Color.White);
            _SpriteBatch.End();

            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            if (_BlendAlpha == null)
            {
                _BlendAlpha = new BlendState();
                _BlendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;
                _BlendAlpha.AlphaDestinationBlend = Blend.Zero;
                _BlendAlpha.ColorDestinationBlend = Blend.Zero;
                _BlendAlpha.AlphaSourceBlend = Blend.One;
                _BlendAlpha.ColorSourceBlend = Blend.One;
            }

            _SpriteBatch.Begin(SpriteSortMode.Immediate, _BlendAlpha);
            _SpriteBatch.Draw(file, file.Bounds, Color.White);
            _SpriteBatch.End();

            //Release the GPU back to drawing to the screen
            _GraphicsDevice.SetRenderTarget(null);

            return result as Texture2D;
        }

        /// <summary>
        /// Récupère les dimensions d'une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Size</returns>
        private static VO_Size GetDimensionsOfAPicture(string url)
        {
            Texture2D pic = FromFileThenClose(url);
            VO_Size size = new VO_Size((int)pic.Width, (int)pic.Height);
            pic.Dispose();
            return size;
        }

        /// <summary>
        /// Renvoie une image colorée
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <param name="color">Modifications de couleur</param>
        /// <param name="type">Type de ressource</param>
        /// <returns>Image</returns>
        public Texture2D GetColoredImage(string url, VO_ColorTransformation color, ViewerEnums.ImageResourceType type)
        {
            string serial = url + ";" + color.ToString();
            if (_ColoredResources != null && _ColoredResources.ContainsKey(serial))
                return _ColoredResources[serial];

            //Dictionnaire d'image
            if (_ColoredResources == null)
                _ColoredResources = new Dictionary<string, Texture2D>();

            Texture2D originalImage = null;
            switch (type)
            {
                case ViewerEnums.ImageResourceType.Permanent:
                    originalImage = GetPermanentImage(url);
                    break;
                case ViewerEnums.ImageResourceType.Screen:
                    originalImage = GetScreenImage(url);
                    break;
            }

            try
            {
                //if (!color.IsUnmodifiedColor())
                //    ApplyColorFilters(serial, color, new Texture2D(url));
                //else
                    _ColoredResources.Add(serial, originalImage);
                return _ColoredResources[serial];
            }
            catch (InsufficientMemoryException ie)
            {
                LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                LogTools.WriteDebug(ie.Message);
                FreeScreenImages();
                return GetColoredImage(url, color, type);
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                LogTools.WriteDebug(e.Message);
                _ColoredResources.Add(serial, null);
            }
            return null;
        }

        /// <summary>
        /// Applique différents paramètres de couleurs à une image en fonction de paramètres définis
        /// </summary>
        /// <param name="image">Instance de l'image</param>
        /// <param name="Red">Rouge</param>
        /// <param name="Green">Vert</param>
        /// <param name="Blue">Bleu</param>
        /// <param name="Grey">Gris</param>
        /// <param name="Alpha">Opacité</param>
        private void ApplyColorFilters(string serial, VO_ColorTransformation color, Texture2D image)
        {
            /*//Dictionnaire d'image
            if (_ColoredResources == null)
                _ColoredResources = new Dictionary<string, Texture2D>();

            float alphaTouch = (float)color.Opacity / 255.0f;
            if (color.Grey == 0)
            {
                for (uint i = 0; i < image.Width; i++)
                {
                    for (uint j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        image.SetPixel(i, j, new Color(GetColorByte(pixel.R + color.Red), GetColorByte(pixel.G + color.Green), GetColorByte(pixel.B + color.Blue), GetColorByte((int)((float)pixel.A * alphaTouch))));
                    }
                }
            }
            else
            {
                int rat = color.Grey * 100 / 255;
                //float ratGrey = rat / 100;
                //float ratColor = (100 - rat) / 100;
                for (uint i = 0; i < image.Width; i++)
                {
                    for (uint j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        byte grayScale = GetColorByte((int)(((pixel.R + color.Red) * .33) + ((pixel.G + color.Green) * .34) + ((pixel.B + color.Blue) * .33)));
                        byte red = GetColorByte((int)((pixel.R + color.Red) * (100 - rat) / 100) + (int)(grayScale * rat / 100));
                        byte green = GetColorByte((int)((pixel.G + color.Green) * (100 - rat) / 100) + (int)(grayScale * rat / 100));
                        byte blue = GetColorByte((int)((pixel.B + color.Blue) * (100 - rat) / 100) + (int)(grayScale * rat / 100));
                        image.SetPixel(i, j, new Color(red, green, GetColorByte(blue), GetColorByte((int)((float)pixel.A * alphaTouch))));
                    }
                }
            }
            LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, serial, Logs.MANAGER_COLORED));
            _ColoredResources.Add(serial, image);*/
        }

        /// <summary>
        /// Prend en paramètre un int et s'assure de renvoyer un Byte de 8bits.
        /// </summary>
        /// <param name="color">Code couleur</param>
        /// <returns>Byte de couleur 8bits</returns>
        private byte GetColorByte(int color)
        {
            if (color > 255)
                color = 255;
            else if (color < 0)
                color = 0;
            return Convert.ToByte(color);
        }

        /// <summary>
        /// Copie une texture sur une autre
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="source"></param>
        private static void Copy(Texture2D dest, Rectangle destination, Rectangle source)
        {
            //Multiply each color by the source alpha, and write in just the color values into the final texture
            if (_BlendColor == null)
            {
                _BlendColor = new BlendState();
                _BlendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;
                _BlendColor.AlphaDestinationBlend = Blend.Zero;
                _BlendColor.ColorDestinationBlend = Blend.Zero;
                _BlendColor.AlphaSourceBlend = Blend.SourceAlpha;
                _BlendColor.ColorSourceBlend = Blend.SourceAlpha;
            }

            _SpriteBatch.Begin(SpriteSortMode.Immediate, _BlendColor);
            _SpriteBatch.Draw(dest, destination, source, Color.White);
            _SpriteBatch.End();

            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            if (_BlendAlpha == null)
            {
                _BlendAlpha = new BlendState();
                _BlendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;
                _BlendAlpha.AlphaDestinationBlend = Blend.Zero;
                _BlendAlpha.ColorDestinationBlend = Blend.Zero;
                _BlendAlpha.AlphaSourceBlend = Blend.One;
                _BlendAlpha.ColorSourceBlend = Blend.One;
            }

            _SpriteBatch.Begin(SpriteSortMode.Immediate, _BlendAlpha);
             _SpriteBatch.Draw(dest, destination, source, Color.White);
            _SpriteBatch.End();
        }

        #region Creation de menu
        /// <summary>
        /// Récupérer une image resource
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Texture2D GetPermanentMenu(int width, int height, ViewerEnums.MenuType type)
        {
            //Dictionnaire d'image
            if (_PermanentResources == null)
                _PermanentResources = new Dictionary<string, Texture2D>();

            string serial = "MENU|" + VO_GUI.RefResource + ";" + width + ";" + height + ";" + type.ToString();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_PermanentResources.ContainsKey(serial))
                CreateMenu(width, height, type);

            //Renvoie de l'image
            try
            {
                return _PermanentResources[serial];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, serial));
                LogTools.WriteDebug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Crée une image de menu
        /// </summary>
        /// <param name="width">Largeur du menu</param>
        /// <param name="height">Hauteur du menu</param>
        /// <param name="type">Type de menu (back ou front)</param>
        public static void CreateMenu(int width, int height, ViewerEnums.MenuType type)
        {
            //Dictionnaire d'image
            if (_PermanentResources == null)
                _PermanentResources = new Dictionary<string, Texture2D>();

            string serial = "MENU|" + VO_GUI.RefResource + ";" + width + ";" + height + ";" + type.ToString();

            //Ajout de l'image
            if (!_PermanentResources.ContainsKey(serial))
            {
                try
                {
                    switch (type)
                    {
                        case ViewerEnums.MenuType.Back:
                            _PermanentResources.Add(serial, CreateMenuBack(width, height));
                            break;
                        case ViewerEnums.MenuType.Front:
                            _PermanentResources.Add(serial, CreateMenuFront(width, height));
                            break;
                    }

                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, serial, Logs.MANAGER_PERMANENT));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    FreePermanentImages();
                    CreateMenu(width, height, type);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, serial));
                    LogTools.WriteDebug(e.Message);
                    _PermanentResources.Add(serial, null);
                }
            }
        }

        /// <summary>
        /// Crée un menu back
        /// </summary>
        /// <param name="width">Largeur</param>
        /// <param name="height">Hauteur</param>
        /// <returns>Image du menu</returns>
        private static Texture2D CreateMenuBack(int width, int height)
        {
            Texture2D res = ImageManager.GetPermanentImage(VO_GUI.RefResource);

            int blockSize = VO_GUI.BackL.Width;
            int miniWidth = width - blockSize - blockSize;
            int miniHeight = height - blockSize - blockSize;

            #region Remplissage milieu
            RenderTarget2D center = new RenderTarget2D(_GraphicsDevice, miniWidth, miniHeight);
            _GraphicsDevice.SetRenderTarget(center);
            _GraphicsDevice.Clear(Color.Black);
            for (int i = 0; i <= miniWidth; i += blockSize)
            {
                for (int j = 0; j <= miniHeight; j += blockSize)
                {
                    Copy(res, new Rectangle(i, j, VO_GUI.BackC.Width, VO_GUI.BackC.Height), VO_GUI.BackC.Source);
                }
            }
            #endregion

            #region Côté gauche
            RenderTarget2D left = new RenderTarget2D(_GraphicsDevice, blockSize, miniHeight);
            _GraphicsDevice.SetRenderTarget(left);
            _GraphicsDevice.Clear(Color.Black);
            for (int i = 0; i <= miniHeight; i += blockSize)
            {
                Copy(res, new Rectangle(0, i, VO_GUI.BackL.Width, VO_GUI.BackL.Height), VO_GUI.BackL.Source);
            }
            #endregion

            #region Côté droit
            RenderTarget2D right = new RenderTarget2D(_GraphicsDevice, blockSize, miniHeight);
            _GraphicsDevice.SetRenderTarget(right);
            _GraphicsDevice.Clear(Color.Black);
            for (int i = 0; i <= miniHeight; i += blockSize)
            {
                Copy(res, new Rectangle(0, i, VO_GUI.BackR.Width, VO_GUI.BackR.Height), VO_GUI.BackR.Source);
            }
            #endregion

            #region Côté haut
            RenderTarget2D top = new RenderTarget2D(_GraphicsDevice, miniWidth, blockSize);
            _GraphicsDevice.SetRenderTarget(top);
            _GraphicsDevice.Clear(Color.Black);
            for (int i = 0; i <= miniWidth; i += blockSize)
            {
                Copy(res, new Rectangle(i, 0, VO_GUI.BackT.Width, VO_GUI.BackT.Height), VO_GUI.BackT.Source);
            }
            #endregion

            #region Côté bas
            RenderTarget2D bottom = new RenderTarget2D(_GraphicsDevice, miniWidth, blockSize);
            _GraphicsDevice.SetRenderTarget(bottom);
            _GraphicsDevice.Clear(Color.Black);
            for (int i = 0; i <= miniWidth; i += blockSize)
            {
                Copy(res, new Rectangle(i, 0, VO_GUI.BackB.Width, VO_GUI.BackB.Height), VO_GUI.BackB.Source);
            }
            #endregion

            #region Côté leftTop
            RenderTarget2D leftTop = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
            _GraphicsDevice.SetRenderTarget(leftTop);
            _GraphicsDevice.Clear(Color.Black);
            Copy(res, new Rectangle(0, 0, VO_GUI.BackLT.Width, VO_GUI.BackLT.Height), VO_GUI.BackLT.Source);
            #endregion

            #region Côté leftBottom
            RenderTarget2D leftBottom = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
            _GraphicsDevice.SetRenderTarget(leftBottom);
            _GraphicsDevice.Clear(Color.Black);
            Copy(res, new Rectangle(0, 0, VO_GUI.BackLB.Width, VO_GUI.BackLB.Height), VO_GUI.BackLB.Source);
            #endregion

            #region Côté rightTop
            RenderTarget2D rightTop = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
            _GraphicsDevice.SetRenderTarget(rightTop);
            _GraphicsDevice.Clear(Color.Black);
            Copy(res, new Rectangle(0, 0, VO_GUI.BackRT.Width, VO_GUI.BackRT.Height), VO_GUI.BackRT.Source);
            #endregion

            #region Côté rightBottom
            RenderTarget2D rightBottom = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
            _GraphicsDevice.SetRenderTarget(rightBottom);
            _GraphicsDevice.Clear(Color.Black);
            Copy(res, new Rectangle(0, 0, VO_GUI.BackRB.Width, VO_GUI.BackRB.Height), VO_GUI.BackRB.Source);
            #endregion

            #region Réunion
            RenderTarget2D menu = new RenderTarget2D(_GraphicsDevice, width, height);
            _GraphicsDevice.SetRenderTarget(menu);
            _GraphicsDevice.Clear(Color.Black);
            Copy(center, new Rectangle(blockSize, blockSize, center.Width, center.Height), center.Bounds);
            Copy(left, new Rectangle(0, blockSize, left.Width, left.Height), left.Bounds);
            Copy(right, new Rectangle(miniWidth + blockSize, blockSize, right.Width, right.Height), right.Bounds);
            Copy(top, new Rectangle(blockSize, 0, top.Width, top.Height), top.Bounds);
            Copy(bottom, new Rectangle(blockSize, miniHeight + blockSize, bottom.Width, bottom.Height), bottom.Bounds);
            Copy(leftTop, new Rectangle(0, 0, leftTop.Width, leftTop.Height), leftTop.Bounds);
            Copy(leftBottom, new Rectangle(0, miniHeight + blockSize, leftBottom.Width, leftBottom.Height), leftBottom.Bounds);
            Copy(rightTop, new Rectangle(miniWidth + blockSize, 0, rightTop.Width, rightTop.Height), rightTop.Bounds);
            Copy(rightBottom, new Rectangle(miniWidth + blockSize, miniHeight + blockSize, rightBottom.Width, rightBottom.Height), rightBottom.Bounds);
            #endregion

            top.Dispose();
            left.Dispose();
            right.Dispose();
            bottom.Dispose();
            center.Dispose();
            leftTop.Dispose();
            leftBottom.Dispose();
            rightTop.Dispose();
            rightBottom.Dispose();
            
            //Release the GPU back to drawing to the screen
            _GraphicsDevice.SetRenderTarget(null);

            return menu as Texture2D;
        }

        /// <summary>
        /// Crée un menu front
        /// </summary>
        /// <param name="width">Largeur</param>
        /// <param name="height">Hauteur</param>
        /// <returns>Image du menu</returns>
        private static Texture2D CreateMenuFront(int width, int height)
        {
            Texture2D res = ImageManager.GetPermanentImage(VO_GUI.RefResource);

            int blockSize = VO_GUI.FrontL.Width;
            int miniWidth = width - blockSize - blockSize;
            int miniHeight = height - blockSize - blockSize;

            #region Remplissage milieu
            RenderTarget2D center = null;
            if (miniHeight > 0 && miniWidth > 0)
            {
                center = new RenderTarget2D(_GraphicsDevice, miniWidth, miniHeight);
                _GraphicsDevice.SetRenderTarget(center);
                _GraphicsDevice.Clear(Color.Black);
                for (int i = 0; i <= miniWidth; i += blockSize)
                {
                    for (int j = 0; j <= miniHeight; j += blockSize)
                    {
                        Copy(res, new Rectangle(i, j, VO_GUI.FrontC.Width, VO_GUI.FrontC.Height), VO_GUI.FrontC.Source);
                    }
                }
            }
            #endregion

            #region Côté gauche
            RenderTarget2D left = null;
            if (miniHeight > 0)
            {
                left = new RenderTarget2D(_GraphicsDevice, blockSize, miniHeight);
                _GraphicsDevice.SetRenderTarget(left);
                _GraphicsDevice.Clear(Color.Black);
                for (int i = 0; i <= miniHeight; i += blockSize)
                {
                    Copy(res, new Rectangle(0, i, VO_GUI.FrontL.Width, VO_GUI.FrontL.Height), VO_GUI.FrontL.Source);
                }
            }
            #endregion

            #region Côté droit
            RenderTarget2D right = null;
            if (miniHeight > 0)
            {
                right = new RenderTarget2D(_GraphicsDevice, blockSize, miniHeight);
                _GraphicsDevice.SetRenderTarget(right);
                _GraphicsDevice.Clear(Color.Black);
                for (int i = 0; i <= miniHeight; i += blockSize)
                {
                    Copy(res, new Rectangle(0, i, VO_GUI.FrontR.Width, VO_GUI.FrontR.Height), VO_GUI.FrontR.Source);
                }
            }
            #endregion

            #region Côté haut
            RenderTarget2D top = null;
            if (miniWidth > 0)
            {
                top = new RenderTarget2D(_GraphicsDevice, miniWidth, blockSize);
                _GraphicsDevice.SetRenderTarget(top);
                _GraphicsDevice.Clear(Color.Black);
                for (int i = 0; i <= miniWidth; i += blockSize)
                {
                    Copy(res, new Rectangle(i, 0, VO_GUI.FrontT.Width, VO_GUI.FrontT.Height), VO_GUI.FrontT.Source);
                }
            }
            #endregion

            #region Côté bas
            RenderTarget2D bottom = null;
            if (miniWidth > 0)
            {
                bottom = new RenderTarget2D(_GraphicsDevice, miniWidth, blockSize);
                _GraphicsDevice.SetRenderTarget(bottom);
                _GraphicsDevice.Clear(Color.Black);
                for (int i = 0; i <= miniWidth; i += blockSize)
                {
                    Copy(res, new Rectangle(i, 0, VO_GUI.FrontB.Width, VO_GUI.FrontB.Height), VO_GUI.FrontB.Source);
                }
            }
            #endregion

            #region Côté leftTop
            RenderTarget2D leftTop = null;
            if (miniHeight > 0 && miniWidth > 0)
            {
                leftTop = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
                _GraphicsDevice.SetRenderTarget(leftTop);
                _GraphicsDevice.Clear(Color.Black);
                Copy(res, new Rectangle(0, 0, VO_GUI.FrontLT.Width, VO_GUI.FrontLT.Height), VO_GUI.FrontLT.Source);
            }
            #endregion

            #region Côté leftBottom
            RenderTarget2D leftBottom = null;
            if (miniHeight > 0 && miniWidth > 0)
            {
                leftBottom = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
                _GraphicsDevice.SetRenderTarget(leftBottom);
                _GraphicsDevice.Clear(Color.Black);
                Copy(res, new Rectangle(0, 0, VO_GUI.FrontLB.Width, VO_GUI.FrontLB.Height), VO_GUI.FrontLB.Source);
            }
            #endregion

            #region Côté rightTop
            RenderTarget2D rightTop = null;
            if (miniHeight > 0 && miniWidth > 0)
            {
                rightTop = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
                _GraphicsDevice.SetRenderTarget(rightTop);
                _GraphicsDevice.Clear(Color.Black);
                Copy(res, new Rectangle(0, 0, VO_GUI.FrontRT.Width, VO_GUI.FrontRT.Height), VO_GUI.FrontRT.Source);
            }
            #endregion

            #region Côté rightBottom
            RenderTarget2D rightBottom = null;
            if (miniHeight > 0 && miniWidth > 0)
            {
                rightBottom = new RenderTarget2D(_GraphicsDevice, blockSize, blockSize);
                _GraphicsDevice.SetRenderTarget(rightBottom);
                _GraphicsDevice.Clear(Color.Black);
                Copy(res, new Rectangle(0, 0, VO_GUI.FrontRB.Width, VO_GUI.FrontRB.Height), VO_GUI.FrontRB.Source);
            }
            #endregion

            #region Réunion
            RenderTarget2D menu = new RenderTarget2D(_GraphicsDevice, width, height);
            _GraphicsDevice.SetRenderTarget(menu);
            _GraphicsDevice.Clear(Color.Black);
            if (center != null)
                Copy(center, new Rectangle(blockSize, blockSize, center.Width, center.Height), center.Bounds);
            if (left != null)
                Copy(left, new Rectangle(0, blockSize, left.Width, left.Height), left.Bounds);
            if (right != null)
                Copy(right, new Rectangle(miniWidth + blockSize, blockSize, right.Width, right.Height), right.Bounds);
            if (top != null)
                Copy(top, new Rectangle(blockSize, 0, top.Width, top.Height), top.Bounds);
            if (bottom != null)
                Copy(bottom, new Rectangle(blockSize, miniHeight + blockSize, bottom.Width, bottom.Height), bottom.Bounds);
            if (leftTop != null)
                Copy(leftTop, new Rectangle(0, 0, leftTop.Width, leftTop.Height), leftTop.Bounds);
            if (leftBottom != null)
                Copy(leftBottom, new Rectangle(0, miniHeight + blockSize, leftBottom.Width, leftBottom.Height), leftBottom.Bounds);
            if (rightTop != null)
                Copy(rightTop, new Rectangle(miniWidth + blockSize, 0, rightTop.Width, rightTop.Height), rightTop.Bounds);
            if (rightBottom != null)
                Copy(rightBottom, new Rectangle(miniWidth + blockSize, miniHeight + blockSize, rightBottom.Width, rightBottom.Height), rightBottom.Bounds);
            #endregion

            if (top != null)
                top.Dispose();
            if (left != null)
                left.Dispose();
            if (right != null)
                right.Dispose();
            if (bottom != null)
                bottom.Dispose();
            if (center != null)
                center.Dispose();
            if (leftTop != null)
                leftTop.Dispose();
            if (leftBottom != null)
                leftBottom.Dispose();
            if (rightBottom != null)
                rightBottom.Dispose();
            if (rightTop != null)
                rightTop.Dispose();

            //Release the GPU back to drawing to the screen
            _GraphicsDevice.SetRenderTarget(null);

            return menu as Texture2D;
        }
        #endregion
        #endregion

        #region Free
        /// <summary>
        /// Libère les ressource du menu titre
        /// </summary>
        public void FreeScreenImages()
        {
            if (_ScreenResources != null)
            {
                foreach (Texture2D image in _ScreenResources.Values)
                {
                    if (image != null)
                        image.Dispose();
                }
            }
            _ScreenResources = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Libère les ressource du menu titre
        /// </summary>
        public static void FreePermanentImages()
        {
            if (_PermanentResources != null)
            {
                foreach (Texture2D image in _PermanentResources.Values)
                {
                    if (image != null)
                        image.Dispose();
                }
            }
            _PermanentResources = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Libère les ressource du menu titre
        /// </summary>
        public void FreeColoredImages()
        {
            if (_ColoredResources != null)
            {
                foreach (Texture2D image in _ColoredResources.Values)
                {
                    image.Dispose();
                }
            }
            _ColoredResources = new Dictionary<string, Texture2D>();
        }
        #endregion

        #region Create
        /// <summary>
        /// Crée une nouvelle image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public void CreateNewScreenImage(string url)
        {
            //Dictionnaire d'image
            if (_ScreenResources == null)
                _ScreenResources = new Dictionary<string, Texture2D>();

            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return;

            //Ajout de l'image
            if (!_ScreenResources.ContainsKey(url))
            {
                try
                {
                    _ScreenResources.Add(url, FromFileThenClose(url));
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, url, Logs.MANAGER_SCREEN));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    FreeScreenImages();
                    CreateNewScreenImage(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                    _ScreenResources.Add(url, null);
                }
            }
        }

        /// <summary>
        /// Crée une nouvelle image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public static void CreateNewPermanentImage(string url)
        {
            //Dictionnaire d'image
            if (_PermanentResources == null)
                _PermanentResources = new Dictionary<string, Texture2D>();

            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return;

            //Ajout de l'image
            if (!_PermanentResources.ContainsKey(url))
            {
                try
                {
                    _PermanentResources.Add(url, FromFileThenClose(url));
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_IMAGE_CREATED, url, Logs.MANAGER_PERMANENT));
                }
                catch (InsufficientMemoryException ie)
                {
                    LogTools.WriteInfo(Logs.MANAGER_MEMORY_ERROR);
                    LogTools.WriteDebug(ie.Message);
                    FreePermanentImages();
                    CreateNewPermanentImage(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                    _PermanentResources.Add(url, null);
                }
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Supprimer une image
        /// </summary>
        /// <param name="url">Url de l'image</param>
        public void DeleteScreenImage(string url)
        {
            if (_ScreenResources.ContainsKey(url))
            {
                try
                {
                    _ScreenResources[url].Dispose();
                    _ScreenResources.Remove(url);
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
        public static void DeletePermanentImage(string url)
        {
            if (_PermanentResources.ContainsKey(url))
            {
                try
                {
                    _PermanentResources[url].Dispose();
                    _PermanentResources.Remove(url);
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
        public void DeleteColoredImage(string url)
        {
            if (_ColoredResources.ContainsKey(url))
            {
                try
                {
                    _ColoredResources[url].Dispose();
                    _ColoredResources.Remove(url);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_NOT_FOUND, url));
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
        public Texture2D GetScreenImage(string url)
        {
            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return null;

            //Dictionnaire d'image
            if (_ScreenResources == null)
                _ScreenResources = new Dictionary<string, Texture2D>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_ScreenResources.ContainsKey(url))
                CreateNewScreenImage(url);

            //Renvoie de l'image
            try
            {
                return _ScreenResources[url];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, url));
                LogTools.WriteDebug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Récupérer une image resource
        /// </summary>
        /// <param name="url">Url de l'image</param>
        /// <returns>Image</returns>
        public static Texture2D GetPermanentImage(string url)
        {
            //Pas d'image envoyée
            if (string.IsNullOrEmpty(url))
                return null;

            //Dictionnaire d'image
            if (_PermanentResources == null)
                _PermanentResources = new Dictionary<string, Texture2D>();

            //Création et ajout de l'image si non présente dans le dictionnaire
            if (!_PermanentResources.ContainsKey(url))
                CreateNewPermanentImage(url);

            //Renvoie de l'image
            try
            {
                return _PermanentResources[url];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_IMAGE_KEY_NOT_FOUND, url));
                LogTools.WriteDebug(e.Message);
                return null;
            }
        }
        #endregion
        #endregion
    }
}
