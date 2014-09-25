using Holy_War.Helpers;
using Holy_War.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Tiles
{
	public abstract class Tile : Sprite
	{
        const int _defaultTileHeight = 64;
        const int _defaultTileWidth = 64;
       
        internal Point _location;
	    internal Point _newGridLocation;
        internal Point _savedGridLocation;
	    internal float _transparency = 1f;

	    internal virtual Point ScreenLocation
	    {
	        get { return _location; }
	        set
	        {
	            if (value.X >= 0
	                && value.Y >= 0
                    && value.X < GameScreen.CurrentWorld.WidthInTiles
                    && value.Y < GameScreen.CurrentWorld.WidthInTiles)
	            {
	                _location = value;
	            }
	        }
	    }

        internal bool Animating { get; set; }

        public Point SavedGridLocation { get { return _savedGridLocation; } }

        protected Tile(Texture2D texture, Point location, Layer layer): base(texture, layer)
		{
		    _location = location;
		    _savedGridLocation = location;
		}

	    public static int GetDefaultTileWidth()
	    {
            return _defaultTileWidth;
	    }

        public static int GetDefaultTileHeight()
        {
            return _defaultTileHeight;
        }

	    public abstract void Draw(SpriteBatch spriteBatch);
	    public abstract void Update(GameTime gametime);
	}
}
