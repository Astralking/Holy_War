using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Sprite;

namespace Holy_War.Input.Commands
{
    internal class MoveCommand : IInputCommand
    {
        private readonly Orientation _orientation;

        public MoveCommand(Orientation orientation)
        {
            _orientation = orientation;
        }

        public void Execute(UserActor userActor)
        {
            if (userActor != null)
            {
                var direction = Converter.OrientationToDirection(_orientation);

                userActor.Move(direction);
            }
        }
    }
}
