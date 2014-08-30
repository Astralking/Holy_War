using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Tiles.Terrain
{
	public class Grassland : Tile
	{
		public Grassland(Texture2D texture, int height, int width)
			: base(texture, Layer.Ground)
		{
		}

		public override void Draw()
		{
		}
	}
}
