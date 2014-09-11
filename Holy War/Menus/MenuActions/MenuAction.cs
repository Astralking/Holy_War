using Holy_War.Actors.UserActors;

namespace Holy_War.Menus.MenuActions
{
    public class MenuAction : IMenuAction
    {
        public MenuAction(string displayText)
        {
            DisplayText = displayText;
        }

        public string DisplayText { get; set; }

        public virtual void Execute(UserActorWithStats userActor)
        {
        }
    }
}
