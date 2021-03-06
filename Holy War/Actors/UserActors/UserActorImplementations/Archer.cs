﻿using System.Collections.Generic;
using Holy_War.Actors.Stats;
using Holy_War.Enumerations;
using Holy_War.Managers;
using Holy_War.Menus.MenuActions;
using Microsoft.Xna.Framework;

namespace Holy_War.Actors.UserActors.UserActorImplementations
{
    public class Archer : UserActorWithStats, IUserActor
    {
        internal Archer(Point location, Team team): base(
			GenerateMenuActions(),  
			GenerateActorStats(team), 
			GetTeamTexture("Actors/UserActors/Archer", team), 
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
				hp: 6,
                mp: 4,
				strength: 3,
				dexterity: 5,
				intelligence: 5,
				attackRange: 2,
				movement: 3);
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
