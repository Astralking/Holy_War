using Holy_War.Enumerations;
using Holy_War.Input.Commands;
using Microsoft.Xna.Framework.Content;

namespace Holy_War.Input
{
	public class InGameInputHandler : InputHandler
	{  
		public InGameInputHandler(ContentManager contentManager)
		{
            _up = new MoveCommand(Orientation.North);
            _down = new MoveCommand(Orientation.South);
            _left = new MoveCommand(Orientation.West);
            _right = new MoveCommand(Orientation.East);

		    _action = new ActionCommand();
            _back = new BackCommand();
		}
	}
}
