using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Tiles
{
	public abstract class Tile : Sprite.Sprite
	{
		const int _defaultHeight = 64;
		const int _defaultWidth = 64;

        private Point _location;

	    public Point Location
	    {
	        get { return _location; }
	        set { _location = value; }
	    }

	    public int Height { get { return _defaultHeight; } }
		public int Width { get { return _defaultWidth; } }

		protected Tile(Texture2D texture, Point location, Layer layer): base(texture, layer)
		{
		    _location = location;
		}

	    public static int GetDefaultTileWidth()
	    {
	        return _defaultWidth;
	    }

        public static int GetDefaultTileHeight()
        {
            return _defaultHeight;
        }

        public static Vector2 GetLocationInPixels(Point location)
        {
            return new Vector2(location.X * _defaultWidth, location.Y * _defaultHeight);
        }

	    public abstract void Draw(SpriteBatch spriteBatch);
	}
}
