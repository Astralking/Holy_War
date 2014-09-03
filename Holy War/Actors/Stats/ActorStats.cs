using Holy_War.Zones;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.Stats
{
    public class ActorStats
    {
        public int Movement { get; private set; }
        public int Team { get; private set; }

        public ActorStats(int team, int movement)
        {
            Movement = movement;
            Team = team;
        }
    }
}
