using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Managers;
using ReplicaStudio.Viewer.TransverseLayer.Interfaces;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.DatasLayer;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_AnimatedSprite : IEntity
    {
        #region Members
        /// <summary>
        /// Sprite à afficher
        /// </summary>
        int _CurrentSprite;

        /// <summary>
        /// Liste de sprites
        /// </summary>
        VO_Sprite[] _Sprites;

        /// <summary>
        /// Timer de fréquence
        /// </summary>
        int _FrequencyTimer = 0;

        /// <summary>
        /// Position X
        /// </summary>
        int _posX = 0;

        /// <summary>
        /// Position Y
        /// </summary>
        int _posY = 0;

        /// <summary>
        /// L'id du character source, pour regénération
        /// </summary>
        Guid _CharacterId;

        /// <summary>
        /// Type d'animation, pour regénération
        /// </summary>
        Enums.AnimationType _AnimationType;

        /// <summary>
        /// Type de ressource, pour regénération
        /// </summary>
        ViewerEnums.ImageResourceType _ResourceType;

        /// <summary>
        /// Zoom
        /// </summary>
        Vector2 _Scale;
        #endregion

        #region Properties
        /// <summary>
        /// Lorsqu'un script peut-être effectué
        /// </summary>
        public bool ReadyToExecScript { get; set; }

        /// <summary>
        /// Récupérer le sprite de l'index courant
        /// </summary>
        public int CurrentSpriteIndex
        {
            get
            {
                return _CurrentSprite;
            }
            set
            {
                _CurrentSprite = value;
            }
        }

        /// <summary>
        /// Nombre de sprites actuels
        /// </summary>
        public int SpritesCount
        {
            get
            {
                return _Sprites.Length;
            }
        }

        public Vector2 Scale { get { return _Scale; } }

        /// <summary>
        /// L'animation est gelée
        /// </summary>
        public bool Frozen { get; set; }

        /// <summary>
        /// Page courante executée
        /// </summary>
        public int CurrentExecutingPage { get; set; }

        /// <summary>
        /// Sprite à afficher
        /// </summary>
        public VO_Sprite Sprite
        {
            get
            {
                if (_Sprites == null)
                    return null;
                return _Sprites[_CurrentSprite];
            }
        }

        public Guid AnimationId { get; set; }

        public Point Location
        {
            get
            {
                return new Point(_posX, _posY);
            }
        }

        public int Frequency { get; set; }

        public uint Width { get; set; }

        public uint Height { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="type">Type d'anim</param>
        public VO_AnimatedSprite(Guid itemId, Enums.AnimationType type)
        {
            CreateAnimation(itemId, new Guid(), type, 0, 0, ViewerEnums.ImageResourceType.Screen);
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="type">Type d'anim</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public VO_AnimatedSprite(Guid itemId, Enums.AnimationType type, int x, int y)
        {
            CreateAnimation(itemId, new Guid(), type, x, y, ViewerEnums.ImageResourceType.Screen);
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="IdCharacter">Id du character</param>
        /// <param name="type">Type d'anim</param>
        public VO_AnimatedSprite(Guid itemId, Guid IdCharacter, Enums.AnimationType type)
        {
            CreateAnimation(itemId, IdCharacter, type, 0, 0, ViewerEnums.ImageResourceType.Screen);
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="IdCharacter">Id du character</param>
        /// <param name="type">Type d'anim</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public VO_AnimatedSprite(Guid itemId, Guid IdCharacter, Enums.AnimationType type, int x, int y)
        {
            CreateAnimation(itemId, IdCharacter, type, x, y, ViewerEnums.ImageResourceType.Screen);
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="IdCharacter">Id du character</param>
        /// <param name="type">Type d'anim</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public VO_AnimatedSprite(Guid itemId, Guid IdCharacter, Enums.AnimationType type, int x, int y, ViewerEnums.ImageResourceType resourceType)
        {
            CreateAnimation(itemId, IdCharacter, type, x, y, resourceType);
        }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="IdCharacter">Id du character</param>
        /// <param name="type">Type d'anim</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public VO_AnimatedSprite(Guid itemId, Guid IdCharacter, Enums.AnimationType type, int x, int y, ViewerEnums.ImageResourceType resourceType, int rowOverload)
        {
            CreateAnimation(itemId, IdCharacter, type, x, y, resourceType, rowOverload);
        }

        /// <summary>
        /// Constructeur standard
        /// </summary>
        private VO_AnimatedSprite()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create de l'animation
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="IdCharacter">Id du character</param>
        /// <param name="type">Type d'anim</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        private void CreateAnimation(Guid itemId, Guid IdCharacter, Enums.AnimationType type, int x, int y, ViewerEnums.ImageResourceType resourceType)
        {
            ReadyToExecScript = true;
            VO_Animation anim = null;
            switch (type)
            {
                case Enums.AnimationType.CharacterAnimation:
                    anim = GameCore.Instance.GetCharAnimationById(IdCharacter, itemId);
                    break;
                case Enums.AnimationType.CharacterFace:
                    anim = GameCore.Instance.GetCharFaceById(itemId);
                    break;
                case Enums.AnimationType.IconAnimation:
                    anim = GameCore.Instance.GetIconById(itemId);
                    break;
                case Enums.AnimationType.Menu:
                    anim = GameCore.Instance.GetMenuById(itemId);
                    break;
                case Enums.AnimationType.ObjectAnimation:
                    anim = GameCore.Instance.GetAnimationById(itemId);
                    break;
            }

            CreateAnimation(itemId, IdCharacter, type, x, y, resourceType, anim.Row);
        }

        /// <summary>
        /// Create de l'animation
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="IdCharacter">Id du character</param>
        /// <param name="type">Type d'anim</param>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        private void CreateAnimation(Guid itemId, Guid IdCharacter, Enums.AnimationType type, int x, int y, ViewerEnums.ImageResourceType resourceType, int rowOverload)
        {
            //Récupérer l'animation
            VO_Animation anim = null;
            switch (type)
            {
                case Enums.AnimationType.CharacterAnimation:
                    anim = GameCore.Instance.GetCharAnimationById(IdCharacter, itemId);
                    break;
                case Enums.AnimationType.CharacterFace:
                    anim = GameCore.Instance.GetCharFaceById(itemId);
                    break;
                case Enums.AnimationType.IconAnimation:
                    anim = GameCore.Instance.GetIconById(itemId);
                    break;
                case Enums.AnimationType.Menu:
                    anim = GameCore.Instance.GetMenuById(itemId);
                    break;
                case Enums.AnimationType.ObjectAnimation:
                    anim = GameCore.Instance.GetAnimationById(itemId);
                    break;
            }
            if (!string.IsNullOrEmpty(anim.ResourcePath))
            {
                Texture2D resource = null;

                ////TODO: Vérifier l'existence de la ressource, sinon mettre une image par défaut DANS L'IMAGE MANAGER !!!!!
                //String LocalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Resources\\";


                //if (File.Exists(PathTools.GetProjectPath(type) + anim.ResourcePath) == false)
                //{
                //    anim.ResourcePath = LocalPath + "Default.jpg";
                //}

                switch (resourceType)
                {
                    case ViewerEnums.ImageResourceType.Screen:
                        resource = ImageManager.CurrentStage.GetScreenImage(PathTools.GetProjectPath(type) + anim.ResourcePath);
                        break;
                    case ViewerEnums.ImageResourceType.Permanent:
                        resource = ImageManager.GetPermanentImage(PathTools.GetProjectPath(type) + anim.ResourcePath);
                        break;
                }
                if (resource != null)
                {
                    Frequency = (int)((double)(10000 / anim.Frequency) * 0.06);

                    Width = (uint)anim.SpriteWidth;
                    Height = (uint)anim.SpriteHeight;
                    int nbrSprites = (int)resource.Width / anim.SpriteWidth;
                    _Sprites = new VO_Sprite[nbrSprites];

                    _posX = x;
                    _posY = y;
                    AnimationId = anim.Id;
                    _CharacterId = IdCharacter;
                    _AnimationType = type;
                    _ResourceType = resourceType;

                    //Création des sprites
                    for (int i = 0; i < nbrSprites; i++)
                    {
                        Guid id = Guid.NewGuid();
                        int posX = i * (int)Width;
                        int posY = rowOverload * (int)Height;
                        switch (resourceType)
                        {
                            case ViewerEnums.ImageResourceType.Screen:
                                SpriteManager.CreateScreenSprite(id, PathTools.GetProjectPath(type) + anim.ResourcePath, new Vector2(x, y), new Rectangle(posX, rowOverload * (int)Height, (int)Width, (int)Height), null);
                                _Sprites[i] = SpriteManager.GetScreenSprite(id);
                                break;
                            case ViewerEnums.ImageResourceType.Permanent:
                                SpriteManager.CreatePermanentSprite(id, PathTools.GetProjectPath(type) + anim.ResourcePath, new Vector2(x, y), new Rectangle(posX, rowOverload * (int)Height, (int)Width, (int)Height), null);
                                _Sprites[i] = SpriteManager.GetPermanentSprite(id);
                                break;
                        }
                        _Sprites[i].Id = id;
                    }
                    _CurrentSprite = 0;
                    CurrentExecutingPage = -1;
                }
            }
            else
            {
                _Sprites = new VO_Sprite[1];
                _Sprites[0] = null;
            }

        }

        /// <summary>
        /// Récupère le prochain index d'animation
        /// </summary>
        public void GetNextSprite()
        {
            if (!Frozen)
            {
                _FrequencyTimer++;
                if (_FrequencyTimer == Frequency)
                {
                    ReadyToExecScript = true;
                    _FrequencyTimer = 0;
                    _CurrentSprite++;
                    if (_CurrentSprite == _Sprites.Length)
                        _CurrentSprite = 0;
                }
            }
        }

        /// <summary>
        /// Change le Scale de l'animation
        /// </summary>
        /// <param name="value"></param>
        public void SetScale(Vector2 value)
        {
            foreach (VO_Sprite sprite in _Sprites)
            {
                if (sprite != null)
                    sprite.Scale = value;
            }
        }

        /// <summary>
        /// Surcharger la fréquence
        /// </summary>
        /// <param name="frequency"></param>
        public void SetFrequency(int frequency)
        {
            Frequency = (int)((double)(10000 / frequency) * 0.06);
        }

        /// <summary>
        /// Changer la position de l'animation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            foreach (VO_Sprite sprite in _Sprites)
            {
                if (sprite != null)
                    sprite.Position = new Vector2(x, y);
            }
            _posX = x;
            _posY = y;
        }

        /// <summary>
        /// Changer la couleur
        /// </summary>
        /// <param name="color">Objet transformations de couleur</param>
        public void SetColor(VO_ColorTransformation color)
        {
            /*foreach (VO_Sprite sprite in _Sprites)
            {
                if (sprite != null)
                    SpriteManager.ChangeScreenSpriteColor(sprite.Id, color);
            }*/
        }

        /// <summary>
        /// Regénérer l'animation au cas où ses ressources auraient été détruites.
        /// </summary>
        public void RegenerateAnim()
        {
            CreateAnimation(AnimationId, _CharacterId, _AnimationType, _posX, _posY, _ResourceType);
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Détruire l'objet
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
