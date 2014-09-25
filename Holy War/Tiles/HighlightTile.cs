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
        private float _mFadeIncrement = 0.05f;
        private double _mFadeDelay = .035;
        private const float _defualtTransparency = 0.6f;

        public HighlightTile(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer)
        {
            _transparency = _defualtTransparency;

            Animating = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var locationInPixels = Converter.GetLocationInPixels(ScreenLocation);

            spriteBatch.Draw(
                base.Texture, 
                locationInPixels, 
                null,
                Color.White * _transparency, 
                0f, 
                new Vector2(0, 0), 
                1f, 
                SpriteEffects.None, 
                Converter.LayerTofloat(Layer));
        }

        public override void Update(GameTime gameTime)
        {
            _mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (_mFadeDelay <= 0)
            {
                _mFadeDelay = .050;

                _transparency += _mFadeIncrement;

                if (_transparency >= 1.0f)
                    _mFadeIncrement *= -1;

                if (_transparency <= 0.5f)
                {
                    _mFadeIncrement *= -1;
                }
            }
        }

        public void Reset()
        {
            _transparency = _defualtTransparency;
        }
    }
}
