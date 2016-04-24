using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    public class KeyboardManager
    {
        #region Members
        private static bool[] _KeyPressed = new bool[256];
        private static List<Keys> _KeyPressing = new List<Keys>();
        private static bool _OnClick = false;
        #endregion

        #region Properties
        public static  bool StateChanged
        {
            get
            {
                return _OnClick;
            }
        }
        #endregion

        #region Methods
        private static void ResetDico()
        {
            for (int i = 0; i < 256; i++)
            {
                _KeyPressed[i] = false;
            }
        }

        private static void ControlPressing(KeyboardState e)
        {
            for (int i = _KeyPressing.Count - 1; i >= 0; i--)
            {
                if (e.IsKeyUp(_KeyPressing[i]))
                {
                    _KeyPressing.RemoveAt(i);
                }
            }
        }

        public static bool OnClick(Keys key)
        {
            int value = (int)key;

            if(_KeyPressed[value])
                return true;
            return false;
        }

        public static void Update()
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] keys = state.GetPressedKeys();
            _OnClick = false;
            ResetDico();
            ControlPressing(state);
            
            foreach (Keys key in keys)
            {
                if(!_KeyPressing.Contains(key))
                {
                    _KeyPressed[(int)key] = true;
                    _OnClick = true;
                    _KeyPressing.Add(key);
                }
            }
        }

        public static List<Keys> GetPressedKeys()
        {
            List<Keys> keys = new List<Keys>();
            for (int i = 0; i < 256; i++)
            {
                if (_KeyPressed[i])
                {
                    keys.Add((Keys)i);
                }
            }

            return keys;
        }
        #endregion
    }
}
