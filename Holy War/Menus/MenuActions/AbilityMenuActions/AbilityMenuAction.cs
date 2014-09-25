using Holy_War.Actors.UserActors;

namespace Holy_War.Menus.MenuActions.AbilityMenuActions
{
    public abstract class AbilityMenuAction : IMenuAction
    {
        protected AbilityMenuAction(string displayText)
        {
            DisplayText = displayText;
        }

        public string DisplayText { get; set; }

        public abstract void Execute(UserActorWithStats userActor);
    }
}
