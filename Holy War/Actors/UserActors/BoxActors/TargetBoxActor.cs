using Holy_War.Enumerations;
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

		public Team Team { get { return Team.Neutral; } }
    }
}
