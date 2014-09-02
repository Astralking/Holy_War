﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors;
using Holy_War.Actors.UserActors;
using Holy_War.Enumerations;
using Holy_War.Helpers;
using Holy_War.Sprite;
using Microsoft.Xna.Framework;

namespace Holy_War.Input.Commands
{
    internal class MoveCommand : IInputCommand
    {
        private readonly Orientation _orientation;

        public MoveCommand(Orientation orientation)
        {
            _orientation = orientation;
        }

        public void Execute(UserActor userActor, GameTime gameTime)
        {
            if (userActor != null)
            {
                var direction = Converter.OrientationToDirection(_orientation);

                userActor.Move(direction, gameTime);
            }
        }
    }
}
