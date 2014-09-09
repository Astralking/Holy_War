using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors
{
	public interface IUserActor
	{
		void Action();
	    void Back();
	    void Move(Point direction, GameTime gameTime);
	    bool TurnLocked { get; set; }
        bool Updated { get; set; }
        Point ScreenLocation { get; }
	}
}
