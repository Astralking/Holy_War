using Holy_War.Actors;
using Holy_War.Enumerations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Input
{
	public class InputHandler
	{
        public static Device ActiveDevice { get { return Device.Keyboard; } }
        public static MouseState CurrentMouseState { get { return Mouse.GetState(); } }
        public static KeyboardState CurrentKeyboardState { get { return Keyboard.GetState(); } }

		internal IInputCommand _up;
		internal IInputCommand _down;
		internal IInputCommand _left;
		internal IInputCommand _right;

		internal IInputCommand _action;
		internal IInputCommand _back;
		internal IInputCommand _menu;

		internal KeyboardState _previousKeyboardState;
        internal KeyboardState _currentKeyboardState;

        internal MouseState _previousMouseState;
        internal MouseState _currentMouseState;

		#region Key Constants
			internal const Keys UP_BUTTON = Keys.Up;
            internal const Keys DOWN_BUTTON = Keys.Down;
            internal const Keys LEFT_BUTTON = Keys.Left;
            internal const Keys RIGHT_BUTTON = Keys.Right;
            internal const Keys ACTION_BUTTON = Keys.Enter;
            internal const Keys BACK_BUTTON = Keys.Back;
            internal const Keys MENU_BUTTON = Keys.Escape;
		#endregion
    
	    public IInputCommand HandleInput(GameTime gameTime)
	    {
            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;

	        _currentKeyboardState = CurrentKeyboardState;
	        _currentMouseState = CurrentMouseState;

			if (IsPressed(UP_BUTTON))
				return _up;
			if (IsPressed(DOWN_BUTTON))
				return _down;
			if (IsPressed(LEFT_BUTTON))
				return _left;
			if (IsPressed(RIGHT_BUTTON))
				return _right;

			if (IsPressedOnce(ACTION_BUTTON))
				return _action;
			if (IsPressedOnce(BACK_BUTTON))
				return _back;
			if (IsPressedOnce(MENU_BUTTON))
				return _menu;

			return null; 
		}

        private bool IsPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

		private bool IsPressedOnce(Keys key)
		{
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
		}

	}
}
