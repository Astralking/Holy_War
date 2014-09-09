using System.Collections.Generic;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors.UserActorImplementations
{
    public class Warrior : UserActorWithZones
    {
        public Warrior(List<IMenuAction> menuActions, ActorStats stats, Point location, Layer layer)
            : base(menuActions, stats, GetTeamTexture("Actors/UserActors/Warrior", stats.Team), location, layer)
        {
        }
    }
}
