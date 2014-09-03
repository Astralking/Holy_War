using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Actors.Stats;
using Holy_War.Tiles;
using Holy_War.Enumerations;

namespace Holy_War.Actors
{
	public abstract class Actor : Tile, IActor
	{
        public Point StartingPosition { get; private set; }

        private bool _turnLocked = false;
	    private float _transparency = 1f;

        public bool TurnLocked
        {
            get { return _turnLocked; }
            set
            {
                _turnLocked = value;
                SetTransparency(_turnLocked);
            }
        }

		protected Actor(Texture2D texture, Point location, Layer layer)
            : base(texture, location, layer)
		{
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
                Color.White * _transparency, 
                0f, 
                new Vector2(0, 0), 
                1f,  
                SpriteEffects.None, 
                LayerToFloat(Layer));
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
