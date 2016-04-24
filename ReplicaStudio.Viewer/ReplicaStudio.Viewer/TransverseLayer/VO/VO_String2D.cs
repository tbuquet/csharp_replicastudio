using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReplicaStudio.Viewer.TransverseLayer.Managers;

namespace ReplicaStudio.Viewer.TransverseLayer.VO
{
    public class VO_String2D
    {
        #region Members
        private Vector2 _TextSize;
        private string _Text;
        private int _FontSize;
        private string _Font;
        private Vector2 _Position;
        private float _X;
        private float _Y;
        #endregion

        #region Properties
        public Color Color { get; set; }

        public SpriteFont SpriteFont
        {
            get
            {
                return FontManager.GetSpriteFont(_Font, _FontSize);
            }
        }

        public Rectangle Destination
        {
            get
            {
                return new Rectangle((int)_Position.X, (int)_Position.Y, Width, Height);
            }
        }

        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                _TextSize = FontManager.GetSpriteFont(_Font, _FontSize).MeasureString(_Text);
            }
        }

        public Vector2 TextSize
        {
            get
            {
                return _TextSize;
            }
        }

        public int Width
        {
            get
            {
                return (int)_TextSize.X;
            }
        }

        public int Height
        {
            get
            {
                return (int)_TextSize.Y;
            }
        }

        public int FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value;
                _TextSize = FontManager.GetSpriteFont(_Font, _FontSize).MeasureString(_Text);
            }
        }

        public Vector2 Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
                _X = _Position.X;
                _Y = _Position.Y;
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public float X
        {
            get
            {

                return _X;
            }
        }

        public float Y
        {
            get
            {
                return _Y;
            }
        }
        #endregion

        #region Constructors
        public VO_String2D(string text, string font, int size)
        {
            _FontSize = size;
            _Font = font;
            Text = text;
        }

        public VO_String2D(string font, int size)
        {
            _FontSize = size;
            _Font = font;
        }

        public VO_String2D(string text, int size, Color color)
        {
            _FontSize = size;
            _Text = text;
            Color = color;
        }
        #endregion

        #region Methods
        #endregion
    }
}
