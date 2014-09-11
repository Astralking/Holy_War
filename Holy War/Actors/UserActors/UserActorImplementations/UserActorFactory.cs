using Holy_War.Enumerations;
using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors.UserActorImplementations
{
	public static class UserActorFactory
	{
		public static IUserActor Create(string userActorName, Team team, Point gridLocation)
		{
			switch (userActorName)
			{
				case "Archer":
					return new Archer(gridLocation, team);
				case "Warrior":
					return new Warrior(gridLocation, team);
				case "Sorcerer":
					return new Sorcerer(gridLocation, team);
				case "Monk":
					return new Monk(gridLocation, team);
				case "Assassin":
					return new Assassin(gridLocation, team);
			}

			return null;
		}
	}
}
