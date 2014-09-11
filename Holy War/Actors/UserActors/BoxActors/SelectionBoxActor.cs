using Holy_War.Enumerations;
using Holy_War.Events;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors.BoxActors
{
    public class SelectionBoxActor : Box, IUserActor
    {
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        private bool _visible = true;

        public SelectionBoxActor(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer)
        {
            SetScreenLocation(location);
        }

        public void SetLocation(Point location)
        {
            var newLocation = new Point(location.X, location.Y);

            SetScreenLocation(newLocation);
        }

		public Team Team { get { return Team.Neutral; } }

	    public override void Draw(SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                var locationInPixels = Converter.GetLocationInPixels(ScreenLocation);

                spriteBatch.Draw(Texture,
                    locationInPixels,
                    null,
                    Color.DarkRed,
                    0f,
                    new Vector2(0, 0),
                    1f,
                    SpriteEffects.None,
                    Converter.LayerToFloat(Layer.Zones));
            }
        }

        public void Action(SelectionBoxActor userActor)
        {
            GameScreen.CurrentWorld.SelectUserActorAtSelectionBox();
        }

		public override void Update(GameTime gameTime)
		{
			var highlightedActor = GameScreen.CurrentWorld.GroundMapArray[ScreenLocation.X, ScreenLocation.Y] as UserActorWithStats;

			if (highlightedActor != null)
				OnHighlight(highlightedActor, new OnHighlightEventArgs(highlightedActor.Stats));
		}
    }
}
