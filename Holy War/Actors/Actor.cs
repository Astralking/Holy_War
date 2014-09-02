using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Actors.Stats;
using Holy_War.Tiles;
using Holy_War.Enumerations;

namespace Holy_War.Actors
{
	public abstract class Actor : Tile, IActor
	{
	    public ActorStats Stats { get; private set; }
        public Point StartingPosition { get; private set; }

		protected Actor(Texture2D texture, Point location, Layer layer, ActorStats stats)
            : base(texture, location, layer)
		{
		    Stats = stats;
		    StartingPosition = location;
		}

		public virtual void Action()
		{
            StartingPosition = ScreenLocation;
		}

        public override void Draw(SpriteBatch spriteBatch)
        {
            var locationInPixels = GetLocationInPixels(ScreenLocation);

            spriteBatch.Draw(
                base.Texture,
                locationInPixels, 
                null, 
                Color.White, 
                0f, 
                new Vector2(0, 0), 
                1f, 
                SpriteEffects.None, 
                LayerToFloat(Layer));
        }

	    public virtual void Move(Point direction, GameTime gameTime)
	    {

	    }
	}
}
