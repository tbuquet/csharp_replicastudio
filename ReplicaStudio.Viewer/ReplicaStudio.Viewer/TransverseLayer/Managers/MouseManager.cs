using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Classe de gestion des fonts
    /// </summary>
    public class MouseManager
    {
        #region Members
        private static int _MouseX;
        private static int _MouseY;
        private static Game _Game;
        private static bool _LeftPressing;
        private static bool _OnLeftClick;
        private static bool _OnLeftReleased;
        private static bool _RightPressing;
        private static bool _OnRightClick;
        private static bool _OnRightReleased;
        #endregion

        #region Properties
        public static int MouseX { get { return _MouseX; } }

        public static int MouseY { get { return _MouseY; } }

        public static bool OnLeftClick { get { return _OnLeftClick; } }

        public static bool OnRightClick { get { return _OnRightClick; } }

        public static bool OnLeftReleased { get { return _OnLeftReleased; } }

        public static bool OnRightReleased { get { return _OnRightReleased; } }
        #endregion

        #region Methods
        public static void InitMouseManager(Game game)
        {
            _Game = game;
        }

        public static void Update()
        {
            MouseState mouseState = Mouse.GetState();
            _MouseX = mouseState.X;
            _MouseY = mouseState.Y;
            _OnLeftClick = false;
            _OnRightClick = false;
            _OnLeftReleased = false;
            _OnRightReleased = false;
            if(mouseState.LeftButton == ButtonState.Pressed && !_LeftPressing)
            {
                _LeftPressing = true;
                _OnLeftClick = true;
            }
            else if (mouseState.LeftButton == ButtonState.Released && _LeftPressing)
            {
                _LeftPressing = false;
                _OnLeftReleased = true;
            }

            if(mouseState.RightButton == ButtonState.Pressed && !_RightPressing)
            {
                _RightPressing = true;
                _OnRightClick = true;
            }
            else if (mouseState.RightButton == ButtonState.Released && _RightPressing)
            {
                _RightPressing = false;
                _OnRightReleased = true;
            }
        }

        public static bool Moved()
        {
            MouseState mouseState = Mouse.GetState();
            if (_MouseX != mouseState.X || _MouseY != mouseState.Y)
                return true;
            return false;
        }

        public static bool IsInWindow()
        {
            if(_MouseX < 0f || _MouseX > _Game.GraphicsDevice.PresentationParameters.BackBufferWidth || _MouseY < 0f || _MouseY > _Game.GraphicsDevice.PresentationParameters.BackBufferHeight)
                return false;
            return true;
        }
        #endregion
    }
}
