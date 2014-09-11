using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors.UserActors;

namespace Holy_War.Menus.MenuActions
{
    public class EndTurnMenuAction : MenuAction
    {
        public EndTurnMenuAction(string displayText) : base(displayText)
        {
        }

        public override void Execute(UserActorWithStats userActor)
        {
            userActor.EndTurn();

            base.Execute(userActor);
        }
    }
}
