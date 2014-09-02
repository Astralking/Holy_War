using Holy_War.Zones;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.Stats
{
    public class ActorStats
    {
        public int Movement { get; private set; }

        public ActorStats(ContentManager contentManager, int movement)
        {
            Movement = movement;
        }
    }
}
