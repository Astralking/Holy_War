using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors.UserActors;
using Holy_War.Enumerations.ActorStats;

namespace Holy_War.Menus.MenuActions.AbilityMenuActions.Warrior
{
    public class BattlecryAbilityMenuAction : AbilityMenuAction
    {
        public BattlecryAbilityMenuAction(string displayText) 
            : base(displayText)
        {
        }

        public override void Execute(UserActorWithStats userActor)
        {
            userActor.Stats.BuffArea(PrimaryStat.Primary, userActor.GridLocation, 2, 1);
        }
    }
}
