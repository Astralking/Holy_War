using Holy_War.Enumerations;
using Holy_War.Events;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors.BoxActors
{
    public class SelectionBoxActor : Box, IUserActor
    {
        public SelectionBoxActor(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer)
        {
            SetScreenLocation(location);
        }

		public Team Team { get { return Team.Neutral; } }

        public void Action(SelectionBoxActor userActor)
        {
            GameScreen.CurrentWorld.SelectUserActorAtSelectionBox();
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
    }
}
