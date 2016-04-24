using System.Collections.Generic;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using System.Drawing.Imaging;
using ReplicaStudio.TransverseLayer;
using System.Reflection;
using System;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.BusinessLayer;

namespace ReplicaStudio.BusinessLayer
{
    /// <summary>
    /// Classe Métier qui gère les animations
    /// </summary>
    public class AnimationBusiness: BaseBusiness
    {
        #region Members
        /// <summary>
        /// Surface transparent de la source AnimPreview
        /// </summary>
        VO_BackgroundSerial _BackgroundAnimPreview;

        /// <summary>
        /// Surface ressource originale
        /// </summary>
        string _ResourceOriginal;

        /// <summary>
        /// Surface transparent de la surface Resource.
        /// </summary>
        VO_BackgroundSerial _BackgroundResource;

        /// <summary>
        /// Attribut particulier de transparence pour la sélection de la ressource
        /// </summary>
        ImageAttributes _ImageTransparency;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public AnimationBusiness()
        {
            LoadTransparency();
        }
        #endregion

        #region Methods
        #region Load
        /// <summary>
        /// Charge le VO_Animation en fonction du type
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        /// <param name="pId">Id de l'animation</param>
        /// <returns>Animation</returns>
        public VO_Animation LoadVOObject(Enums.AnimationType type, Guid id)
        {
            switch (type)
            {
                case Enums.AnimationType.CharacterFace:
                    return GameCore.Instance.GetCharFaceById(id);
                case Enums.AnimationType.IconAnimation:
                    return GameCore.Instance.GetIconById(id);
                case Enums.AnimationType.Menu:
                    return GameCore.Instance.GetMenuById(id);
                case Enums.AnimationType.ObjectAnimation:
                    return GameCore.Instance.GetAnimationById(id);
            }
            throw new Exception();
        }

        /// <summary>
        /// Charge le VO_Animation en fonction du type
        /// </summary>
        /// <param name="pParentCharacterId">Id d'un character</param>
        /// <param name="pId">Id de l'animation</param>
        /// <returns>Animation</returns>
        public VO_Animation LoadVOObject(Guid parentCharacterId, Guid id)
        {
            return GameCore.Instance.GetCharAnimationById(parentCharacterId, id);
        }

        /// <summary>
        /// Charge une Surface d'après une URI
        /// </summary>
        /// <param name="pURI">Lien vers la ressource</param>
        public void LoadSurfaceFromURI(string uri)
        {
            ImageManager.CreateNewImageResource(uri);
            _ResourceOriginal = uri;
        }

        /// <summary>
        /// Charge une surface vide
        /// </summary>
        public void LoadEmptySurface()
        {
            _ResourceOriginal = string.Empty;
        }
        #endregion

        /// <summary>
        /// Rafraichi l'animation
        /// </summary>
        /// <param name="pContainer">Containeur de l'animation</param>
        /// <param name="pCurrentSprite">Sprite courant</param>
        /// <returns>Surface</returns>
        public Image RefreshAnimation(Rectangle container, Rectangle currentSprite)
        {
            //Chargement background
            if (_BackgroundAnimPreview == null)
            {
                _BackgroundAnimPreview = new VO_BackgroundSerial(new Size(container.Width, container.Height), EditorSettings.Instance.TransparentBlockSize);
                ImageManager.CreateNewImageBackground(_BackgroundAnimPreview);
            }

            //Preview
            Image sprite = new Bitmap(currentSprite.Width, currentSprite.Height);
            Graphics graphicsSprite = Graphics.FromImage(sprite);
            graphicsSprite.DrawImage(ImageManager.GetImageResource(_ResourceOriginal), new Rectangle(new Point(0, 0), new Size(currentSprite.Width, currentSprite.Height)), new Rectangle(new Point(currentSprite.X, currentSprite.Y), new Size(currentSprite.Width, currentSprite.Height)), GraphicsUnit.Pixel);
            Image mainSurface = FormsTools.GetImageReducedAndCentered(sprite, ImageManager.GetImageBackground(_BackgroundAnimPreview), container, new Rectangle(new Point(0, 0), new Size(currentSprite.Width, currentSprite.Height)), false);
            graphicsSprite.Dispose();
            sprite.Dispose();
            return mainSurface;
        }

        /// <summary>
        /// Rafraichi la ressource
        /// </summary>
        /// <param name="pContainer">Containeur de l'animation</param>
        /// <param name="pCurrentSprite">Sprite courant</param>
        /// <param name="pFull">False si la ressource est redimmensionnée, sinon true</param>
        /// <returns>Surface</returns>
        public Image RefreshRessource(Rectangle container, Rectangle currentSprite, bool full)
        {
            //Chargement background
            if (_BackgroundResource == null)
            {
                _BackgroundResource = new VO_BackgroundSerial(new Size(container.Width, container.Height), EditorSettings.Instance.TransparentBlockSize);
                ImageManager.CreateNewImageBackground(_BackgroundAnimPreview);
            }

            //Ressource
            Image resourceInSurfaceControl;
            Image resource = (Image)ImageManager.GetImageResource(_ResourceOriginal).Clone();
            Graphics resourceGraphics = Graphics.FromImage(resource);

            Image highlighted = new Bitmap(currentSprite.Width, currentSprite.Height);
            Graphics highlightGraphics = Graphics.FromImage(highlighted);
            highlightGraphics.FillRectangle(EditorSettings.Instance.HighlightningBrush, new Rectangle(new Point(0, 0), new Size(highlighted.Width, highlighted.Height)));
            resourceGraphics.DrawImage(highlighted, new Rectangle(currentSprite.Location, new Size(resource.Width, resource.Height)), 0, 0, resource.Width, resource.Height, GraphicsUnit.Pixel, _ImageTransparency);

            if (!full)
                resourceInSurfaceControl = FormsTools.GetImageReducedAndCentered(resource, ImageManager.GetImageBackground(_BackgroundResource), container, new Rectangle(new Point(0, 0), ImageManager.GetImageResource(_ResourceOriginal).Size), full);
            else
                resourceInSurfaceControl = FormsTools.GetImageReducedAndCentered(resource, ImageManager.GetImageBackground(_BackgroundResource), container, container, full);

            resource.Dispose();
            resourceGraphics.Dispose();
            highlighted.Dispose();
            highlightGraphics.Dispose();

            return resourceInSurfaceControl;
        }

        /// <summary>
        /// Sauvegarde la base de données d'animations
        /// </summary>
        /// <param name="pType">Type d'animations à sauver</param>
        public void SaveAnim(Enums.AnimationType type)
        {
            GameCore.Instance.SaveAnim(type);
        }

        /// <summary>
        /// Sauvegarde la base de données d'animations
        /// </summary>
        /// <param name="pType">Type d'animations à sauver</param>
        public void SaveAnim(Enums.AnimationType type, Guid charId)
        {
            GameCore.Instance.SaveAnim(type, charId);
        }

        /// <summary>
        /// Restaure la base de données d'animations
        /// </summary>
        /// <param name="pType">Type d'animations à sauver</param>
        public void RestaureAnim(Enums.AnimationType type)
        {
            GameCore.Instance.RestoreAnim(type);
        }

        /// <summary>
        /// Retourne la largeur et hauteur de la ressource originale
        /// </summary>
        /// <returns>Size</returns>
        public Size GetSizeOfResource()
        {
            return ImageManager.GetImageResource(_ResourceOriginal).Size;
        }

        /// <summary>
        /// Charge la liste en fonction du type d'animation choisie
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList(Enums.AnimationType type)
        {
            switch (type)
            {
                case Enums.AnimationType.CharacterFace:
                    return GameCore.Instance.GetCharFaces();
                case Enums.AnimationType.IconAnimation:
                    return GameCore.Instance.GetIcons();
                case Enums.AnimationType.Menu:
                    return GameCore.Instance.GetMenus();
                case Enums.AnimationType.ObjectAnimation:
                    return GameCore.Instance.GetAnimations();
            }
            throw new Exception();
        }

        /// <summary>
        /// Charge la liste en fonction du type d'animation choisie
        /// </summary>
        /// <param name="pId">Id d'un character</param>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList(Guid id)
        {
            return GameCore.Instance.GetCharAnimations(id);
        }

        /// <summary>
        /// Crée une animation
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        /// <returns>VO_Animation</returns>
        public VO_Animation CreateAnimation(Enums.AnimationType type)
        {
            switch (type)
            {
                case Enums.AnimationType.CharacterFace:
                    return ObjectsFactory.CreateCharFace();
                case Enums.AnimationType.IconAnimation:
                    return ObjectsFactory.CreateIconAnimation();
                case Enums.AnimationType.Menu:
                    return ObjectsFactory.CreateMenuAnimation();
                case Enums.AnimationType.ObjectAnimation:
                    return ObjectsFactory.CreateAnimation();
            }
            throw new Exception();
        }

        /// <summary>
        /// Crée une animation
        /// </summary>
        /// <param name="pParentCharacter">Id du character</param>
        /// <returns>VO_Animation</returns>
        public VO_Animation CreateAnimation(Guid parentCharacter)
        {
            return ObjectsFactory.CreateCharAnimation(parentCharacter);
        }

        /// <summary>
        /// Charge la transparence
        /// </summary>
        public void LoadTransparency()
        {
            //Matrix
            ColorMatrix clrMatrix = new ColorMatrix(new float[][]
            {
                new float[]{1f, 0f, 0f, 0f, 0f},
                new float[]{0f, 1f, 0f, 0f, 0f},
                new float[]{0f, 0f, 1f, 0f, 0f},
                new float[]{0f, 0f, 0f, 0.5f, 0f},
                new float[]{0f, 0f, 0f, 0f, 1f},
            });
            _ImageTransparency = new ImageAttributes();
            _ImageTransparency.SetColorMatrix(clrMatrix);
        }
        #endregion
    }
}
