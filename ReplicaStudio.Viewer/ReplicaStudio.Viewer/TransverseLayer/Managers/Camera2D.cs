using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    public class Camera2D
    {
        #region Members
        private static float _Zoom;
        private static Matrix _Transform;
        private static Vector2 _Pos;
        private static float _Rotation;
        private static Game _Game;
        #endregion

        #region Properties
        /// <summary>
        /// Zoom
        /// </summary>
        public static float Zoom
        {
            get { return _Zoom; }
            set { _Zoom = value; if (_Zoom < 0.1f) _Zoom = 0.1f; } // Negative zoom will flip image
        }

        /// <summary>
        /// Rotation
        /// </summary>
        public static float Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; }
        }

        /// <summary>
        /// Position de la caméra
        /// </summary>
        public static Vector2 Pos
        {
            get { return _Pos; }
            set { _Pos = value; }
        }

        /// <summary>
        /// Destination
        /// </summary>
        public static Rectangle Destination
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, _Game.GraphicsDevice.PresentationParameters.BackBufferWidth, _Game.GraphicsDevice.PresentationParameters.BackBufferHeight);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize la caméra
        /// </summary>
        /// <param name="game"></param>
        public static void InitializeCamera(Game game)
        {
            _Game = game;
            _Zoom = 1.0f;
            _Rotation = 0.0f;
            _Pos = Vector2.Zero;
        }

        /// <summary>
        /// Move camera
        /// </summary>
        /// <param name="amount"></param>
        public static void Move(Vector2 amount)
        {
            _Pos += amount;
        }
        
        /// <summary>
        /// Etat caméra
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <returns></returns>
        public static Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            _Transform = Matrix.CreateTranslation(new Vector3(-_Pos.X, -_Pos.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            return _Transform;
        }
        #endregion
    }
}