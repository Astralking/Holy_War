using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Enumerations;
using Holy_War.Input.Commands;
using Holy_War.Sprite;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
