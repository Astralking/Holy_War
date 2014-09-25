using Holy_War.Enumerations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors
{
	public interface IUserActor
	{
		void Action();
	    void Back();
	    void Move(Point direction, GameTime gameTime);
	    bool TurnLocked { get; set; }
        bool Updated { get; set; }
        Point GridLocation { get; }
		Team Team { get; }
		void Draw(SpriteBatch spriteBatch);
		void Update(GameTime gameTime);
	}
}
