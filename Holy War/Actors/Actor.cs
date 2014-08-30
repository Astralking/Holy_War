using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Tiles;
using Holy_War.Enumerations;

namespace Holy_War.Actors
{
	public abstract class Actor : Tile, IActor
	{
		protected Actor(Texture2D texture, Point location)
            : base(texture, location, Layer.Actor)
		{
        }

		public virtual void Action()
		{
			throw new System.NotImplementedException();
		}

        public override void Draw(SpriteBatch spriteBatch)
        {
            var locationInPixels = GetLocationInPixels(Location);

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

	    public virtual void Move(Point direction)
	    {

	    }
	}
}
