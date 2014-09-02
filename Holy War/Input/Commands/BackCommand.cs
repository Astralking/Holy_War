using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Actors.UserActors;
using Microsoft.Xna.Framework;

namespace Holy_War.Input.Commands
{
    public class BackCommand : IInputCommand
    {
        public void Execute(UserActor userActor, GameTime gameTime)
        {
            userActor.Back();
        }
    }
}
