using Holy_War.Actors;

namespace Holy_War.Input
{
	public interface IInputCommand
	{
        void Execute(UserActor userActor);
	}
}
