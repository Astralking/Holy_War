using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Tiles
{
	public abstract class Tile : Sprite.Sprite
	{
		const int _defaultHeight = 64;
		const int _defaultWidth = 64;

		public int Height { get { return _defaultHeight; } }
		public int Width { get { return _defaultWidth; } }

		protected Tile(Texture2D texture, Layer layer): base(texture, layer)
		{
		}

		public abstract void Draw();
	}
}
