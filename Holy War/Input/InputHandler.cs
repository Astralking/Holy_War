using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Input
{
	public class InputHandler
	{
		internal readonly IInputCommand _up;
		internal readonly IInputCommand _down;
		internal readonly IInputCommand _left;
		internal readonly IInputCommand _right;

		internal readonly IInputCommand _action;
		internal readonly IInputCommand _back;
		internal readonly IInputCommand _menu;

		KeyboardState _previousKeyboardState;
		KeyboardState _currentKeyboardState;

		#region Key Constants
			const Keys UP_BUTTON = Keys.Up;
			const Keys DOWN_BUTTON = Keys.Down;
			const Keys LEFT_BUTTON = Keys.Left;
			const Keys RIGHT_BUTTON = Keys.Right;
			const Keys ACTION_BUTTON = Keys.Enter;
			const Keys BACK_BUTTON = Keys.Back;
			const Keys MENU_BUTTON = Keys.Escape;
		#endregion

		public IInputCommand HandleInput()
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
