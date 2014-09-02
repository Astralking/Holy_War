using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;

namespace Holy_War.Tiles.Terrain
{
	public class Grassland : Tile
	{
		public Grassland(Texture2D texture, Point location)
			: base(texture, location, Layer.Ground)
		{
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
		    var locationInPixels = GetLocationInPixels(ScreenLocation);

            spriteBatch.Draw(
                base.Texture, 
                locationInPixels, 
                null, 
                Color.White, 
                0f, 
                new Vector2(0, 0), 
                1f, 
                SpriteEffects.None, 
                LayerToFloat(Layer));
		}
	}
}
