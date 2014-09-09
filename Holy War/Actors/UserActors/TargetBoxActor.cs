using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.UserActors
{
    public class TargetBoxActor : UserActor
    {
        public TargetBoxActor(List<IMenuAction> menuActions, Texture2D texture, Point location, Layer layer, ActorStats stats) 
            : base(texture, location, layer)
        {
        }
    }
}
