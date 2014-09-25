using System.Collections.Generic;
using System.Linq;
using Holy_War.Enumerations;
using Holy_War.Events;
using Holy_War.Helpers;
using Holy_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Overlay.Overlays.DamageOverlay
{
    public class DamageOverlay : Overlay
    {
        private List<DamageValue> _damageValueList = new List<DamageValue>();
        private bool _animating;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_animating) 
                return;

            foreach (var damageValue in _damageValueList)
                damageValue.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_animating)
                return;

            foreach (var damageValue in _damageValueList)
            {
                damageValue.Update(gameTime);

                if (_damageValueList.All(damage => damage.FinishedAnimating))
                    _animating = false;
            }
        }

        public void OnDamage(object sender, OnDamageEventArgs e)
        {
            _damageValueList.Add(new DamageValue(e.GridLocationInPixels, e.Damage));
            _animating = true;
        }
    }
}
