using Holy_War.Tiles;
using Holy_War.Tiles.Terrain;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Maps
{
	public class Map
	{
		public Tile[,] MapArray { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public Map(int height, int width)
		{
			MapArray = new Tile[height, width];
			Width = width;
			Height = height;
		}

		public void PopulateMap(ContentManager contentManager)
		{
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					MapArray[i, j] = new Grassland(contentManager.Load<Texture2D>("GrassTile"), 64, 64);
				}
			}
		}
	}
}
