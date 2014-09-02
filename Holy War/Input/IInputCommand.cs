using Holy_War.Actors;
using Holy_War.Actors.UserActors;
using Microsoft.Xna.Framework;

namespace Holy_War.Input
{
	public interface IInputCommand
	{
        void Execute(UserActor userActor, GameTime gameTime);
	}
}
