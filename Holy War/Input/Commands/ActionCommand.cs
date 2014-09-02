using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Actors.UserActors;
using Microsoft.Xna.Framework;

namespace Holy_War.Input.Commands
{
    public class ActionCommand : IInputCommand
    {
        public void Execute(SelectionBoxActor userActor)
        {
            userActor.Action(userActor);
        }

        public void Execute(UserActor userActor, GameTime gameTime)
        {
            var selectionBox = userActor as SelectionBoxActor;

            if(selectionBox != null)
                Execute(selectionBox);
            else
            userActor.Action(userActor);
        }
    }
}
