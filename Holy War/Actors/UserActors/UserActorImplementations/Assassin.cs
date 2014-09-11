using System.Collections.Generic;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors.UserActorImplementations
{
    public class Assassin : UserActorWithStats, IUserActor
    {
		internal Assassin(Point location, Team team): base(
			GenerateMenuActions(),  
			GenerateActorStats(team), 
			GetTeamTexture("Actors/UserActors/Assassin", team), 
			location, 
			Layer.Ground)
        {
		}

		private static ActorStats GenerateActorStats(Team team)
		{
			return new ActorStats(
				primaryStat: Enumerations.ActorStats.PrimaryStat.Dexterity,
				attackType: Enumerations.ActorStats.AttackType.Piercing,
				armorType: Enumerations.ActorStats.ArmorType.Light,
				team: team,
				hp: 8,
				strength: 3,
				dexterity: 8,
				intelligence: 4,
				attackRange: 1,
				movement: 5);
		}

		private static List<IMenuAction> GenerateMenuActions()
		{
			return new List<IMenuAction>
            {
                new EndTurnMenuAction("End Turn"),
                new AttackMenuAction("Attack")
            };
		}

		public Team Team { get { return Stats.Team; } }
    }
}
