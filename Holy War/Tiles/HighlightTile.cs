using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Tiles
{
    public class HighlightTile : Tile
    {
        public HighlightTile(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer)
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
    }
}
