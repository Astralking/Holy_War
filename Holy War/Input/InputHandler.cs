using Holy_War.Actors;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Input
{
	public class InputHandler
	{
		internal IInputCommand _up;
		internal IInputCommand _down;
		internal IInputCommand _left;
		internal IInputCommand _right;

		internal IInputCommand _action;
		internal IInputCommand _back;
		internal IInputCommand _menu;

		private KeyboardState _previousKeyboardState;
		private KeyboardState _currentKeyboardState;

	    private MouseState _previousMouseState;
        private MouseState _currentMouseState;

		#region Key Constants
			internal const Keys UP_BUTTON = Keys.Up;
            internal const Keys DOWN_BUTTON = Keys.Down;
            internal const Keys LEFT_BUTTON = Keys.Left;
            internal const Keys RIGHT_BUTTON = Keys.Right;
            internal const Keys ACTION_BUTTON = Keys.Enter;
            internal const Keys BACK_BUTTON = Keys.Back;
            internal const Keys MENU_BUTTON = Keys.Escape;
		#endregion

		public IInputCommand HandleActionInput()
		{
			_currentKeyboardState = Keyboard.GetState();

			if (isPressed(UP_BUTTON))
				return _up;
			if (isPressed(DOWN_BUTTON))
				return _down;
			if (isPressed(LEFT_BUTTON))
				return _left;
			if (isPressed(RIGHT_BUTTON))
				return _right;
			if (isPressed(ACTION_BUTTON))
				return _action;
			if (isPressed(BACK_BUTTON))
				return _back;
			if (isPressed(MENU_BUTTON))
				return _menu;

			return null; 
		}

		private bool isPressed(Keys key)
		{
			return _currentKeyboardState.IsKeyDown(key);
		}
	}
}
