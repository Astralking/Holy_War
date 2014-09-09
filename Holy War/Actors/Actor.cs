using Holy_War.Actors.UserActors;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Actors.Stats;
using Holy_War.Tiles;
using Holy_War.Enumerations;

namespace Holy_War.Actors
{
	public abstract class Actor : Tile
	{
	    private bool _visible = true;
	    private float _transparency = 1f;

		protected Actor(Texture2D texture, Point location, Layer layer)
            : base(texture, location, layer)
		{
		}

        public virtual void Show()
        {
            _visible = true;
        }

        public virtual void Hide()
        {
            _visible = false;
        }

        public virtual void Action()
		{          
		}

	    public override void Draw(SpriteBatch spriteBatch)
	    {
	        if (_visible)
	        {	        
	            var locationInPixels = Converter.GetLocationInPixels(ScreenLocation);

	            spriteBatch.Draw(
	                base.Texture,
	                locationInPixels,
	                null,
	                Color.White*_transparency,
	                0f,
	                new Vector2(0, 0),
	                1f,
	                SpriteEffects.None,
	                Converter.LayerToFloat(Layer));
	        }
	}

	    public virtual void Move(Point direction, GameTime gameTime)
	    {

	    }

	    internal void SetTransparency(bool turnUsed)
	    {
            _transparency = turnUsed ? 0.5f : 1f;
	    }
	}
}
