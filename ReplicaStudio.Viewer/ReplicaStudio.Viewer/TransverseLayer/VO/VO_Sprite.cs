using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReplicaStudio.Viewer.TransverseLayer.Interfaces;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_Sprite : IEntity
    {
        #region Members
        private int _width;
        private int _height;
        private Texture2D _image;
        private Rectangle _Source;
        #endregion

        #region Properties
        public Guid Id { get; set; }

        public string ResourceUrl { get; set; }

        public Texture2D Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                _width = _image.Width;
                _height = _image.Height;
            }
        }

        public Vector2 Position { get; set; }

        public Rectangle Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                _width = _Source.Width;
                _height = _Source.Height;
            }
        }

        public Rectangle Destination { get { return new Rectangle((int)Position.X, (int)Position.Y, _width, _height); } }

        public Vector2 Scale { get; set; }

        public int Width { get { return _width; } }

        public int Height { get { return _height; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public VO_Sprite()
        {
        }

        public VO_Sprite(Guid id)
        {
            Id = id;
        }

        public VO_Sprite(Texture2D texture)
        {
            Image = texture;
        }
        #endregion

        #region Methods
        public void Dispose()
        {

        }
        #endregion
    }
}
