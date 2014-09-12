using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Sprites
{
    public class SelectionArrow : Sprite
    {
        private Vector2 offset = new Vector2(150, 0);

        public SelectionArrow(Texture2D texture, Layer layer) : base(texture, layer)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 positionInPixels)
        {
            spriteBatch.Draw(
                Texture,
                positionInPixels + offset,
                null,
                Color.Black,
                0f,
                new Vector2(0,0),
                1f,
                SpriteEffects.FlipHorizontally,
                Converter.LayerTofloat(Layer.MasterText));
        }
    }
}
