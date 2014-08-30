using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;

namespace Holy_War.Input.Commands
{
    public class ActionCommand : IInputCommand
    {
        public void Execute(SelectionBoxActor userActor)
        {
            userActor.Action(userActor);
        }

        public void Execute(UserActor userActor)
        {
            var selectionBox = userActor as SelectionBoxActor;

            if(selectionBox != null)
                Execute(selectionBox);
            else
                userActor.Action(userActor);
        }
    }
}
