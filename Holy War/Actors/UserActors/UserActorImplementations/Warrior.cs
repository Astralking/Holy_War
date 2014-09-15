using System.Collections.Generic;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors.UserActorImplementations
{
    public class Warrior : UserActorWithStats, IUserActor
    {
        internal Warrior(Point location, Team team): base(
			GenerateMenuActions(),  
			GenerateActorStats(team), 
			GetTeamTexture("Actors/UserActors/Warrior", team), 
			location, 
			Layer.Ground)
        {
		}

		private static ActorStats GenerateActorStats(Team team)
		{
			return new ActorStats(
				primaryStat: Enumerations.ActorStats.PrimaryStat.Strength,
				attackType: Enumerations.ActorStats.AttackType.Slashing,
				armorType: Enumerations.ActorStats.ArmorType.Medium,
				team: team,
				hp: 12,
				strength: 7,
				dexterity: 3,
				intelligence: 3,
				attackRange: 1,
				movement: 2);
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
