using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Tools;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.VO;
using Microsoft.Xna.Framework;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Class management de sprite
    /// </summary>
    public static class SpriteManager
    {
        #region Members
        /// <summary>
        /// Sprites permanent
        /// </summary>
        private static Dictionary<Guid, VO_Sprite> _PermanentSprites;

        /// <summary>
        /// Sprites de l'écran
        /// </summary>
        private static Dictionary<Guid, VO_Sprite> _ScreenSprites;
        #endregion

        #region Methods
        #region Tools
        /// <summary>
        /// Charge l'interface
        /// </summary>
        /// <param name="url">Url de la ressource</param>
        public static void LoadGUI(string url)
        {
            if (!string.IsNullOrEmpty(VO_GUI.RefResource))
                ImageManager.DeletePermanentImage(VO_GUI.RefResource);

            VO_GUI.BackLT = LoadGUISprite(VO_GUI.BackLT, url, 0, 0);
            VO_GUI.BackT = LoadGUISprite(VO_GUI.BackT, url, 16, 0);
            VO_GUI.BackRT = LoadGUISprite(VO_GUI.BackRT, url, 32, 0);
            VO_GUI.BackL = LoadGUISprite(VO_GUI.BackL, url, 0, 16);
            VO_GUI.BackC = LoadGUISprite(VO_GUI.BackC, url, 16, 16);
            VO_GUI.BackR = LoadGUISprite(VO_GUI.BackR, url, 32, 16);
            VO_GUI.BackLB = LoadGUISprite(VO_GUI.BackLB, url, 0, 32);
            VO_GUI.BackB = LoadGUISprite(VO_GUI.BackB, url, 16, 32);
            VO_GUI.BackRB = LoadGUISprite(VO_GUI.BackRB, url, 32, 32);

            VO_GUI.FrontLT = LoadGUISprite(VO_GUI.FrontLT, url, 48, 0);
            VO_GUI.FrontT = LoadGUISprite(VO_GUI.FrontT, url, 64, 0);
            VO_GUI.FrontRT = LoadGUISprite(VO_GUI.FrontRT, url, 80, 0);
            VO_GUI.FrontL = LoadGUISprite(VO_GUI.FrontL, url, 48, 16);
            VO_GUI.FrontC = LoadGUISprite(VO_GUI.FrontC, url, 64, 16);
            VO_GUI.FrontR = LoadGUISprite(VO_GUI.FrontR, url, 80, 16);
            VO_GUI.FrontLB = LoadGUISprite(VO_GUI.FrontLB, url, 48, 32);
            VO_GUI.FrontB = LoadGUISprite(VO_GUI.FrontB, url, 64, 32);
            VO_GUI.FrontRB = LoadGUISprite(VO_GUI.FrontRB, url, 80, 32);

            VO_GUI.RefResource = url;
            VO_GUI.BlockSize = 16;
        }

        /// <summary>
        /// Charge un sprite d'interface
        /// </summary>
        /// <param name="sprite">Sprite</param>
        /// <param name="url">Url de la ressource system</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        private static VO_Sprite LoadGUISprite(VO_Sprite oldSprite, string url, int x, int y)
        {
            Guid id = Guid.NewGuid();
            if (oldSprite != null)
                DeletePermanentSprite(oldSprite.Id);
            CreatePermanentSprite(id, url, new Vector2(0, 0), new Rectangle(x, y, 16, 16));
            return GetPermanentSprite(id);
        }
        #endregion

        #region Free
        /// <summary>
        /// Libère les ressources des sprites
        /// </summary>
        public static void FreePermanentSprite()
        {
            if (_PermanentSprites != null)
            {
                foreach (VO_Sprite sprite in _PermanentSprites.Values)
                {
                    sprite.Dispose();
                }
            }
            _PermanentSprites = new Dictionary<Guid, VO_Sprite>();
        }

        /// <summary>
        /// Libère les ressources des sprites
        /// </summary>
        public static void FreeScreenSprite()
        {
            if (_ScreenSprites != null)
            {
                foreach (VO_Sprite sprite in _ScreenSprites.Values)
                {
                    sprite.Dispose();
                }
            }
            _ScreenSprites = new Dictionary<Guid, VO_Sprite>();
        }
        #endregion

        #region Create
        /// <summary>
        /// Créer un sprite
        /// </summary>
        /// <param name="id">Id du sprite</param>
        /// <param name="url">Url de l'image</param>
        /// <param name="location">Location</param>
        public static void CreatePermanentSprite(Guid id, string url, Vector2 location, Rectangle source)
        {
            CreatePermanentSprite(id, url, location, source, null);
        }
        /// <summary>
        /// Créer un sprite
        /// </summary>
        /// <param name="id">Id du sprite</param>
        /// <param name="url">Url de l'image</param>
        /// <param name="location">Location</param>
        /// <param name="color">Couleur</param>
        public static void CreatePermanentSprite(Guid id, string url, Vector2 location, Rectangle source, VO_ColorTransformation color)
        {
            //Dictionnaire d'image
            if (_PermanentSprites == null)
                _PermanentSprites = new Dictionary<Guid, VO_Sprite>();

            //Pas d'image envoyée
            if (id == new Guid())
                return;

            //Ajout de l'image
            if (!_PermanentSprites.ContainsKey(id))
            {
                try
                {
                    VO_Sprite newSprite = new VO_Sprite(id);
                    newSprite.ResourceUrl = url;
                    newSprite.Scale = new Vector2(1.0f, 1.0f);
                    newSprite.Image = ImageManager.GetPermanentImage(url);
                    newSprite.Position = new Vector2(location.X, location.Y);
                    newSprite.Source = source;
                    _PermanentSprites.Add(id, newSprite);
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_SPRITE_CREATED, id, Logs.MANAGER_PERMANENT));
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_SPRITE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                    _PermanentSprites.Add(id, null);
                }
            }
        }

        /// <summary>
        /// Créer un sprite
        /// </summary>
        /// <param name="id">Id du sprite</param>
        /// <param name="url">Url de l'image</param>
        /// <param name="location">Location</param>
        public static void CreateScreenSprite(Guid id, string url, Vector2 location, Rectangle source)
        {
            CreateScreenSprite(id, url, location, source, null);
        }
        /// <summary>
        /// Créer un sprite
        /// </summary>
        /// <param name="id">Id du sprite</param>
        /// <param name="url">Url de l'image</param>
        /// <param name="location">Location</param>
        /// <param name="color">Couleur</param>
        public static void CreateScreenSprite(Guid id, string url, Vector2 location, Rectangle source, VO_ColorTransformation color)
        {
            //Dictionnaire d'image
            if (_ScreenSprites == null)
                _ScreenSprites = new Dictionary<Guid, VO_Sprite>();

            //Pas d'image envoyée
            if (id == new Guid())
                return;

            //Ajout de l'image
            if (!_ScreenSprites.ContainsKey(id))
            {
                try
                {
                    VO_Sprite newSprite = new VO_Sprite(id);
                    newSprite.ResourceUrl = url;
                    newSprite.Scale = new Vector2(1.0f, 1.0f);
                    newSprite.Image = ImageManager.CurrentStage.GetScreenImage(url);
                    newSprite.Position = new Vector2(location.X, location.Y);
                    newSprite.Source = source;
                    _ScreenSprites.Add(id, newSprite);
                    LogTools.WriteDebug(string.Format(Logs.MANAGER_SPRITE_CREATED, id, Logs.MANAGER_SCREEN));
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_SPRITE_NOT_FOUND, url));
                    LogTools.WriteDebug(e.Message);
                    _ScreenSprites.Add(id, null);
                }
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Supprimer un sprite
        /// </summary>
        /// <param name="id">Id du sprite</param>
        public static void DeletePermanentSprite(Guid id)
        {
            if (_PermanentSprites.ContainsKey(id))
            {
                try
                {
                    _PermanentSprites[id].Dispose();
                    _PermanentSprites.Remove(id);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_SPRITE_NOT_FOUND, id));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }

        /// <summary>
        /// Supprimer un sprite
        /// </summary>
        /// <param name="id">Id du sprite</param>
        public static void DeleteScreenSprite(Guid id)
        {
            if (_ScreenSprites.ContainsKey(id))
            {
                try
                {
                    _ScreenSprites[id].Dispose();
                    _ScreenSprites.Remove(id);
                }
                catch (Exception e)
                {
                    LogTools.WriteInfo(string.Format(Logs.MANAGER_SPRITE_NOT_FOUND, id));
                    LogTools.WriteDebug(e.Message);
                }
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Récupérer le sprite resource
        /// </summary>
        /// <param name="id">Id du Sprite</param>
        /// <returns>Sprite</returns>
        public static VO_Sprite GetPermanentSprite(Guid id)
        {
            //Pas d'image envoyée
            if (id == new Guid() || _PermanentSprites == null || !_PermanentSprites.ContainsKey(id))
                return null;

            //Renvoie de l'image
            try
            {
                return _PermanentSprites[id];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_SPRITE_KEY_NOT_FOUND, id));
                LogTools.WriteDebug(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Récupérer le sprite resource
        /// </summary>
        /// <param name="id">Id du Sprite</param>
        /// <returns>Sprite</returns>
        public static VO_Sprite GetScreenSprite(Guid id)
        {
            //Pas d'image envoyée
            if (id == new Guid() || _ScreenSprites == null || !_ScreenSprites.ContainsKey(id))
                return null;

            //Renvoie de l'image
            try
            {
                return _ScreenSprites[id];
            }
            catch (Exception e)
            {
                LogTools.WriteInfo(string.Format(Logs.MANAGER_SPRITE_KEY_NOT_FOUND, id));
                LogTools.WriteDebug(e.Message);
                return null;
            }
        }
        #endregion
        #endregion
    }
}
