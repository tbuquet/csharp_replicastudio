using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.BusinessLayer;
using System.Windows.Forms;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Drawing;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using System.Reflection;
using ReplicaStudio.Editor.TransverseLayer;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Editor.TransverseLayer.Managers;

namespace ReplicaStudio.ServiceLayer
{
    /// <summary>
    /// Classe service des animations
    /// </summary>
    public class AnimationService : BaseService
    {
        #region Members
        /// <summary>
        /// Lien vers la couche Business
        /// </summary>
        AnimationBusiness _Business;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public AnimationService()
        {
            _Business = new AnimationBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge le VO_Animation en fonction du type
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        /// <param name="pId">Id de l'animation</param>
        /// <returns>Animation</returns>
        public VO_Animation LoadVOObject(Enums.AnimationType type, Guid id)
        {
            VO_Animation animation = null;

            RunServiceTask(delegate
            {
                animation = _Business.LoadVOObject(type, id);
            }, Errors.ERROR_ANIMATION_STR_LOAD, type.ToString(),id.ToString());

            return animation;
        }

        /// <summary>
        /// Charge le VO_Animation en fonction du type
        /// </summary>
        /// <param name="pParentCharacterId">Id d'un character</param>
        /// <param name="pId">Id de l'animation</param>
        /// <returns>Animation</returns>
        public VO_Animation LoadVOObject(Guid parentCharacterId, Guid id)
        {
            VO_Animation animation = null;

            RunServiceTask(delegate
            {
                animation = _Business.LoadVOObject(parentCharacterId, id);
            }, Errors.ERROR_ANIMATION_STR_LOAD, parentCharacterId.ToString(), id.ToString());

            return animation;
        }

        /// <summary>
        /// Charge la ressource image
        /// </summary>
        /// <param name="pURI">Chemin de la ressource</param>
        public void LoadSurfaceFromURI(string uri)
        {
            RunServiceTask(delegate
            {
                _Business.LoadSurfaceFromURI(uri);
            }, Errors.ERROR_ANIMATION_STR_LOAD, uri);
        }

        /// <summary>
        /// Charge une surface vide
        /// </summary>
        public void LoadEmptySurface()
        {
            RunServiceTask(delegate
            {
                _Business.LoadEmptySurface();
            }, Errors.ERROR_ANIMATION_STR_LOAD);
        }

        /// <summary>
        /// Rafraichi l'animation
        /// </summary>
        /// <param name="pContainer">Containeur de l'animation</param>
        /// <param name="pCurrentSprite">Sprite courant</param>
        /// <returns>Surface</returns>
        public Image RefreshAnimation(Rectangle container, Rectangle currentSprite)
        {
            Image image = null;

            //Execution de la méthode
            RunServiceTask(delegate
            {
                image = _Business.RefreshAnimation(container, currentSprite);
            },
            //Problème de mémoire
            delegate{
                ImageManager.ResetResources();
                image = RefreshAnimation(container, currentSprite);
            },Errors.ERROR_ANIMATION_STR_LOAD, false, container.ToString(), currentSprite.ToString());

            return image;
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
            Image image = null;

            //Execution de la méthode
            RunServiceTask(delegate
            {
                image = _Business.RefreshRessource(container, currentSprite, full);
            }, 
            //Problème de mémoire
            delegate
            {
                ImageManager.ResetResources();
                image = RefreshAnimation(container, currentSprite);
            }, Errors.ERROR_ANIMATION_STR_LOAD, false, container.ToString(), currentSprite.ToString(), full.ToString());

            return image;
        }

        /// <summary>
        /// Sauvegarde la base de données d'animations
        /// </summary>
        /// <param name="pType">Type d'animations à sauver</param>
        public void SaveAnim(Enums.AnimationType type)
        {
            RunServiceTask(delegate
            {
                _Business.SaveAnim(type);
            }, Errors.ERROR_ANIMATION_STR_DBSAVE, type.ToString());
        }

        /// <summary>
        /// Sauvegarde la base de données d'animations
        /// </summary>
        /// <param name="pType">Type d'animations à sauver</param>
        public void SaveAnim(Enums.AnimationType type, Guid charId)
        {
            RunServiceTask(delegate
            {
                _Business.SaveAnim(type, charId);
            }, Errors.ERROR_ANIMATION_STR_DBSAVE, type.ToString(), charId.ToString());
        }

        /// <summary>
        /// Restaure la base de données d'animations
        /// </summary>
        /// <param name="pType">Type d'animations à sauver</param>
        public void RestaureAnim(Enums.AnimationType type)
        {
            RunServiceTask(delegate
            {
                _Business.RestaureAnim(type);
            }, Errors.ERROR_ANIMATION_STR_DBRESTORE, type.ToString());
        }

        /// <summary>
        /// Retourne la largeur et hauteur de la ressource originale
        /// </summary>
        /// <returns>Size</returns>
        public Size GetSizeOfResource()
        {
            Size size = new Size();

            RunServiceTask(delegate
            {
                size = _Business.GetSizeOfResource();
            }, Errors.ERROR_ANIMATION_STR_LOAD);

            return size;
        }

        /// <summary>
        /// Charge la liste en fonction du type d'animation choisie
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList(Enums.AnimationType type)
        {
            List<VO_Base> list = null;

            RunServiceTask(delegate
            {
                list = _Business.ProvisionList(type);
            }, Errors.ERROR_STR_LIST_PROVISION, type.ToString());

            return list;
        }

        /// <summary>
        /// Charge la liste en fonction du type d'animation choisie
        /// </summary>
        /// <param name="pId">Id d'un character</param>
        /// <returns>Liste de VO_Base</returns>
        public List<VO_Base> ProvisionList(Guid id)
        {
            List<VO_Base> list = null;

            RunServiceTask(delegate
            {
                list = _Business.ProvisionList(id);
            }, Errors.ERROR_STR_LIST_PROVISION, id.ToString());

            return list;
        }

        /// <summary>
        /// Crée une animation
        /// </summary>
        /// <param name="pType">Type d'animation</param>
        /// <returns>VO_Animation</returns>
        public VO_Animation CreateAnimation(Enums.AnimationType type)
        {
            VO_Animation animation = null;

            RunServiceTask(delegate
            {
                animation = _Business.CreateAnimation(type);
            }, Errors.ERROR_ANIMATION_STR_CREATE, type.ToString());

            return animation;
        }

        /// <summary>
        /// Crée une animation
        /// </summary>
        /// <param name="pParentCharacter">Id du character</param>
        /// <returns>VO_Animation</returns>
        public VO_Animation CreateAnimation(Guid parentCharacter)
        {
            VO_Animation animation = null;

            RunServiceTask(delegate
            {
                animation = _Business.CreateAnimation(parentCharacter);
            }, Errors.ERROR_ANIMATION_STR_CREATE, parentCharacter.ToString());

            return animation;
        }
        #endregion
    }
}
