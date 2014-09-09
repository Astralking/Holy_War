using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holy_War.Actors.UserActors;

namespace Holy_War.Menus.MenuActions
{
    public interface IMenuAction
    {
        string DisplayText { get; set; }
        void Execute(UserActorWithZones userActor);
    }
}
