using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Actors.Stats;
using Holy_War.Actors.UserActors;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors
{
    public class SelectionBoxActor : UserActor
    {
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        private bool _visible = true;

        public SelectionBoxActor(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer, null)
        {
            ScreenLocation = location;
        }

        public void SetLocation(Point location)
        {
            var newLocation = new Point(location.X, location.Y);

            ScreenLocation = newLocation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_visible)
            {
                var locationInPixels = GetLocationInPixels(ScreenLocation);

                spriteBatch.Draw(Texture,
                    locationInPixels,
                    null,
                    Color.DarkRed,
                    0f,
                    new Vector2(0, 0),
                    1f,
                    SpriteEffects.None,
                    LayerToFloat(Layer.Master));
            }
        }

        public void Action(SelectionBoxActor userActor)
        {
            MainGame.CurrentWorld.SelectUserActorAtSelectionBox();
        }
    }
}
