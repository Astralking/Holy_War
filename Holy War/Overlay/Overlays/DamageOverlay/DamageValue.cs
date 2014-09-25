using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Overlay.Overlays.DamageOverlay
{
    public class DamageValue
    {
        private int _textMovement;
        private double _movementDelay = 0.5;
        private const int _movementIncrement = 2;

        private float _transparency = 1f;
        private float _mFadeIncrement = 0.05f;
        private double _mFadeDelay = .035;
        private const float _offsetX = 20f;

        public DamageValue(Vector2 gridLocationInPixels, int damage)
        {
            GridLocationInPixels = new Vector2(gridLocationInPixels.X + _offsetX, gridLocationInPixels.Y);
            DamageString = damage.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteManager.Fonts["Fonts/ContextMenuFont"],
                DamageString,
                GridLocationInPixels,
                Color.Red * _transparency,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                Converter.LayerTofloat(Layer.MasterText));
        }

        public Vector2 GridLocationInPixels { get; private set; }
        public string DamageString { get; private set; }
        public bool FinishedAnimating { get; private set; }

        public void Update(GameTime gameTime)
        {
            _mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (_mFadeDelay <= 0)
            {
                _mFadeDelay = .050;

                _transparency += _mFadeIncrement;
                GridLocationInPixels -= new Vector2(0, _movementIncrement);

                if (_transparency >= 1.5f)
                    _mFadeIncrement *= -1;

                //if (_transparency <= 0.5f)
                //{
                //    _mFadeIncrement *= -1;
                //}
            }

            if (_transparency < 0)
                FinishedAnimating = true;
        }
    }
}

//GridLocationInPixels += new Vector2(0, _textMovement);
//_textMovement += _movementIncrement;
//            else
                //FinishedAnimating = true;