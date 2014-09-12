using System;
using Holy_War.Enumerations;
using Holy_War.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors.BoxActors
{
    public class TargetBoxActor : Box, IUserActor
    {
        public TargetBoxActor(Texture2D texture, Point location, Layer layer) 
            : base(texture, location, layer)
        {
        }

		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				if(value == false)
					OnHighlight(null, new Events.OnHighlightEventArgs(null));

				base.Visible = value;
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public Team Team { get { return Team.Neutral; } }
    }
}
