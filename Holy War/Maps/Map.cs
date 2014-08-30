using Holy_War.Actors;
using Holy_War.Tiles;
using Holy_War.Tiles.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Maps
{
	public class Map
	{
		public Tile[,] GroundMapArray { get; private set; }
		public Tile[,] ActorMapArray { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

	    private ContentManager _contentManager;

		public Map(int height, int width)
		{
            GroundMapArray = new Tile[height, width];
            ActorMapArray = new Tile[height, width];
			Width = width;
			Height = height;
		}

		public void PopulateMap(ContentManager contentManager)
		{
		    _contentManager = contentManager;

            InitialiseGround();
		    InitialiseActors();
		}

	    private void InitialiseGround()
	    {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    GroundMapArray[i, j] = new Grassland(_contentManager.Load<Texture2D>("GrassTile"), new Point(i, j));
                }
            }
	    }

        private void InitialiseActors()
        {
            int x = 0;
            int y = 0;

            ActorMapArray[x, y] = new UserActor(_contentManager.Load<Texture2D>("BlankActor"), new Point(x, y));
        }

	    public void Draw(SpriteBatch _spriteBatch)
	    {
            DrawGround(_spriteBatch);
            DrawActors(_spriteBatch);
	    }

        private void DrawGround(SpriteBatch _spriteBatch)
	    {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var tile = GroundMapArray[i, j];

                    tile.Draw(_spriteBatch);                 
                }
            }
	    }

        private void DrawActors(SpriteBatch _spriteBatch)
	    {
	        for (int i = 0; i < Width; i++)
	        {
                for (int j = 0; j < Height; j++)
	            {
                    var tile = ActorMapArray[i, j];

	                if (tile != null)
                        tile.Draw(_spriteBatch);
	            }
	        }
	    }
	}
}
