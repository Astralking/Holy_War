using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Tiles
{
	public abstract class Tile : Sprite.Sprite
	{
		const int _defaultHeight = 64;
		const int _defaultWidth = 64;
	    private bool _updated = false;

        private Point _location;
	    private Point _newGridLocation;
        private Point _currentGridLocation;

	    public Point ScreenLocation
	    {
	        get { return _location; }
	        set
	        {
	            if (value.X >= 0
	                && value.Y >= 0
                    && value.X < MainGame.CurrentWorld.WidthInTiles
                    && value.Y < MainGame.CurrentWorld.WidthInTiles)
	            {
	                _location = value;
	            }
	        }
	    }

	    public int Height { get { return _defaultHeight; } }
		public int Width { get { return _defaultWidth; } }
	    public Point NewGridLocation { get { return _newGridLocation; } }
        public Point CurrentGridLocation { get { return _currentGridLocation; } }

	    public bool Updated
	    {
	        get { return _updated; }
	        internal set
	        {
	            _updated = value;

	            if (_updated)
	                _newGridLocation = _location;
	            else
	                _currentGridLocation = _newGridLocation;
	        }
	    }
    
		protected Tile(Texture2D texture, Point location, Layer layer): base(texture, layer)
		{
		    _location = location;
		    _currentGridLocation = location;
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

        public static Vector2 GetLocationInTiles(Point location)
        {
            return new Vector2(location.X, location.Y);
        }

	    public abstract void Draw(SpriteBatch spriteBatch);
	}
}
