using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace MegaMaple.Managers
{
    public class InputManager
    {

        private KeyboardState _oldKS;
        private KeyboardState _KS;
        public Keys[] currentPressedKeys;// = currentKeyboardState.GetPressedKeys();

        public InputManager()
        {
            Statics.INPUT = this;
        }

        public void Update()
        {
            if (_KS != null)
                _oldKS = _KS;

            currentPressedKeys = _KS.GetPressedKeys();
            _KS = Keyboard.GetState();
        }

        public bool isKeyPressed(Keys k)
        {
            return (_oldKS.IsKeyUp(k) && _KS.IsKeyDown(k));
        }

        public bool isKeyDown(Keys k)
        {
            return (_oldKS.IsKeyDown(k) && _KS.IsKeyDown(k));
        }

        public bool isKeyRelease(Keys k)
        {
            return (_oldKS.IsKeyUp(k) && _KS.IsKeyDown(k));
        }

        public KeyboardState currentState()
        {
            return this._KS;
        }
    }
}
