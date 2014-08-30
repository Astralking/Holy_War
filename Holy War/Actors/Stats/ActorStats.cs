using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Actors.Stats
{
    public class ActorStats
    {
        public int Movement { get; private set; }

        public ActorStats(int movement)
        {
            Movement = movement;
        }
    }
}
