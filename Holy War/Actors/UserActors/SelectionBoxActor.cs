using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors
{
    public class SelectionBoxActor : UserActor
    {
        public SelectionBoxActor(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer, null, null)
        {
            Location = location;
        }

        public void SetLocation(Point location)
        {
            var newLocation = new Point(Converter.PointToGridPoint(location.X), Converter.PointToGridPoint(location.Y));

            Location = newLocation;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                new Rectangle(Location.X, Location.Y, GetDefaultTileWidth(), GetDefaultTileHeight()),
                null,
                Color.DarkRed,
                0f,
                new Vector2(0, 0),
                SpriteEffects.None,
                LayerToFloat(Layer.Master));
        }

        public void Action(SelectionBoxActor userActor)
        {
            MainGame.SelectActor(userActor.Location);
        }
    }
}
