using Holy_War.Actors.UserActors;
using Holy_War.Enumerations;
using Holy_War.Zones;

namespace Holy_War.Menus.MenuActions
{
    public class AttackMenuAction : MenuAction
    {
        public AttackMenuAction(string displayText) : base(displayText)
        {
        }

        public override void Execute(UserActorWithStats userActor)
        {
            userActor.ResetZoneOrigins(userActor.GridLocation);
            userActor.HighlightZone(userActor.AttackZone);

            userActor.SetState(UserActorState.Attacking);

            base.Execute(userActor);
        }
    }
}
