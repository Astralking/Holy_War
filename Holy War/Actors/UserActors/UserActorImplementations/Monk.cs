using System.Collections.Generic;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors.UserActorImplementations
{
    public class Monk : UserActorWithStats, IUserActor
    {
        internal Monk(Point location, Team team): base(
			GenerateMenuActions(),  
			GenerateActorStats(team), 
			GetTeamTexture("Actors/UserActors/Monk", team), 
			location, 
			Layer.Ground)
        {
		}

		private static ActorStats GenerateActorStats(Team team)
		{
			return new ActorStats(
				primaryStat: Enumerations.ActorStats.PrimaryStat.Dexterity,
				attackType: Enumerations.ActorStats.AttackType.Blunt,
				armorType: Enumerations.ActorStats.ArmorType.None,
				team: team,
				hp: 10,
                mp: 4, 
				strength: 4,
				dexterity: 5,
				intelligence: 4,
				attackRange: 1,
				movement: 4);
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
