using Microsoft.Xna.Framework.Graphics;
using Holy_War.Tiles;
using Holy_War.Enumerations;

namespace Holy_War.Actors
{
	public abstract class Actor : Tile, IActor
	{
		protected Actor(Texture2D texture, int height, int width)
			: base(texture, Layer.Object)
		{
		}

		public void Action()
		{
			throw new System.NotImplementedException();
		}

		public override void Draw()
		{
			throw new System.NotImplementedException();
		}
	}
}
