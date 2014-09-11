using Microsoft.Xna.Framework;

namespace Holy_War.Input
{
	interface IInputHandler
	{
		IInputCommand HandleInput(GameTime gameTime);
	}
}
