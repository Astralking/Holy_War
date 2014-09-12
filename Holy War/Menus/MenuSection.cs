using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Menus
{
    public class MenuSection : Sprite
    {
        private readonly float _height;
        private readonly float _width;
        public Vector2 PositionInPixels { get; private set; }

        public float Height
        {
            get { return _height; }
        }

        public float Width
        {
            get { return _width; }
        }

        internal readonly Vector2 _sectionOffset = new Vector2(75, -75);

        public MenuSection(Texture2D texture, Vector2 positionInPixels, Layer layer, float height, float width) 
            : base(texture, layer)
        {
            _height = height;
            _width = width;
            PositionInPixels = positionInPixels;
            PositionInPixels += _sectionOffset;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                base.Texture,
                PositionInPixels,
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                Converter.LayerTofloat(Layer));
        }

        public void UpdatePositionByPixels(Vector2 positionOffset)
        {
            PositionInPixels += positionOffset;
        }
    }
}
