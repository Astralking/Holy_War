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

        public override void Execute(UserActorWithZones userActor)
        {
            userActor.ResetZoneOrigins(userActor.ScreenLocation);
            userActor.HighlightZone(userActor.AttackZone);

            userActor.SetState(UserActorState.Attacking);

            base.Execute(userActor);
        }
    }
}
