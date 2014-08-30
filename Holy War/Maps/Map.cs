using Holy_War.Actors;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Input;
using Holy_War.Sprite;
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
		public Actor[,] ActorMapArray { get; private set; }
		public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }
        public SelectionBoxActor SelectionBox { get; private set; }

	    private ContentManager _contentManager;

		public Map(int height, int width)
		{
            GroundMapArray = new Tile[height, width];
            ActorMapArray = new Actor[height, width];
			WidthInTiles = width;
			HeightInTiles = height;
		}

		public void InitialiseMap(ContentManager contentManager)
		{
		    _contentManager = contentManager;

            InitialiseGround();
		    InitialiseActors();

            SelectionBox = new SelectionBoxActor(
                contentManager.Load<Texture2D>("SelectionBox"), 
                new Point(0, 0),
		        Layer.Master);
		}

	    private void InitialiseGround()
	    {
            for (var i = 0; i < WidthInTiles; i++)
            {
                for (var j = 0; j < HeightInTiles; j++)
                {
                    GroundMapArray[i, j] = new Grassland(_contentManager.Load<Texture2D>("GrassTile"), new Point(i, j));
                }
            }
	    }

        private void InitialiseActors()
        {
            int x = 0;
            int y = 0;

            ActorMapArray[x, y] = new UserActor(
                _contentManager.Load<Texture2D>("BlankActor"), 
                new Point(x, y), 
                Layer.Actor, 
                _contentManager.Load<Texture2D>("HighlightBox"),
                new ActorStats(2));
        }

	    public void Draw(SpriteBatch spriteBatch)
	    {
            DrawGround(spriteBatch);
            DrawActors(spriteBatch);

            SelectionBox.Draw(spriteBatch);
	    }

        private void DrawGround(SpriteBatch spriteBatch)
	    {
            for (int i = 0; i < WidthInTiles; i++)
            {
                for (int j = 0; j < HeightInTiles; j++)
                {
                    var tile = GroundMapArray[i, j];

                    tile.Draw(spriteBatch);                 
                }
            }
	    }

        private void DrawActors(SpriteBatch spriteBatch)
	    {
	        for (int i = 0; i < WidthInTiles; i++)
	        {
                for (int j = 0; j < HeightInTiles; j++)
	            {
                    var tile = ActorMapArray[i, j];

	                if (tile != null)
                        tile.Draw(spriteBatch);
	            }
	        }
	    }

	    public void Update()
	    {
            if(InputHandler.ActiveDevice == Device.Mouse)
                SelectionBox.SetLocation(new Point(InputHandler.CurrentMouseState.X, InputHandler.CurrentMouseState.Y));
	    }
	}
}
