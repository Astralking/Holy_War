using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Enumerations;
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
            spriteBatch.Draw(base.Texture, new Vector2(Location.X, Location.Y), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, LayerToFloat(Layer));
        }
    }
}
