using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Holy_War.Enumerations;
using Holy_War.Helpers;

namespace Holy_War.Tiles.Terrain
{
    public class GrasslandPath : Terrain, ITerrain
    {
        public GrasslandPath(Texture2D texture, Point location, float movementCost)
            : base(texture, location, Layer.Terrain, movementCost)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var locationInPixels = Converter.GetLocationInPixels(ScreenLocation);

            spriteBatch.Draw(
                base.Texture,
                locationInPixels,
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                Converter.LayerTofloat(Layer));
        }

        public float MovementCost { get { return _movementCost; } }
    }
}
