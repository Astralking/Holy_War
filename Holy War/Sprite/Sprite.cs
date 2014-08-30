using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Sprite
{
	public class Sprite
	{
		public Sprite(Texture2D texture, Layer layer)
		{
			Texture = texture;
			Layer = layer;
		}

		public Texture2D Texture { get; private set; }
		public Layer Layer { get; private set; }
	}
}
