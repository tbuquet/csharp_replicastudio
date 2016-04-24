using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;
using System.Drawing;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Animation : VO_Base
    {
        #region Properties
        public string ResourcePath
        {
            get;
            set;
        }

        public int SpriteWidth
        {
            get;
            set;
        }

        public int SpriteHeight
        {
            get;
            set;
        }

        public int Frequency
        {
            get;
            set;
        }

        public Enums.AnimationType AnimationType
        {
            get;
            set;
        }

        public Guid ParentCharacter
        {
            get;
            set;
        }

        public int Row
        {
            get;
            set;
        }

        public Point OriginPoint
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public VO_Animation()
        {
        }

        public VO_Animation(Guid pID, Enums.AnimationType pType)
        {
            Id = pID;
            AnimationType = pType;
        }

        public VO_Animation(Guid pID, string pName, string pResourcePath, int pSpriteWidth, int pSpriteHeight, int pFrequency)
        {
            Id = pID;
            Title = pName;
            ResourcePath = pResourcePath;
            SpriteWidth = pSpriteWidth;
            SpriteHeight = pSpriteHeight;
            Frequency = pFrequency;
        }
        #endregion

        #region Methods
        public void Delete()
        {
            try
            {
                switch (AnimationType)
                {
                    case Enums.AnimationType.CharacterAnimation:
                        GameCore.Instance.GetCharacterById(ParentCharacter).Animations.Remove(this);
                        break;
                    case Enums.AnimationType.CharacterFace:
                        GameCore.Instance.Game.CharFacesAnimations.Remove(this);
                        break;
                    case Enums.AnimationType.IconAnimation:
                        GameCore.Instance.Game.IconsAnimations.Remove(this);
                        break;
                    case Enums.AnimationType.Menu:
                        GameCore.Instance.Game.MenusAnimations.Remove(this);
                        break;
                    case Enums.AnimationType.ObjectAnimation:
                        GameCore.Instance.Game.ObjectAnimations.Remove(this);
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_DELETE_VO + "Animation #" + this.Id + ":" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Animation Clone()
        {
            return (VO_Animation)this.MemberwiseClone();
        }
        #endregion
    }
}
