using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Enumerations;
using Holy_War.Helpers;

namespace Holy_War.Input.Commands
{
    internal class MoveCommand : IInputCommand
    {
        private Orientation _orientation;

        public MoveCommand(Orientation orientation)
        {
            _orientation = orientation;
        }

        public void Execute(UserActor userActor)
        {
            Move(userActor);
        }

        public void Move(UserActor userActor)
        {
            var direction = DirectionConverter.OrientationToDirection(_orientation);

            userActor.Move(direction);
        }
    }
}
