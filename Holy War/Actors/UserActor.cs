using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors
{
    public class UserActor : Actor
    {
        public UserActor(Texture2D texture, Point location)
            : base(texture, location)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Move(Point direction)
        {
            base.Action();
        }

        public override void Action()
        {
            base.Action();
        }
        

    }
}
