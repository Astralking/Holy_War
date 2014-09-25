using System;
using Holy_War.Actors.UserActors;
using Holy_War.Enumerations;
using Holy_War.Enumerations.ActorStats;
using Holy_War.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Holy_War.Actors.Stats
{
    public class ActorStats
    {
        public PrimaryStat PrimaryStat { get; private set; }
        public AttackType AttackType { get; private set; }
        public ArmorType ArmorType { get; private set; }
        public Team Team { get; private set; }
        public int Movement { get; private set; }
        public int AttackRange { get; private set; }
        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Intelligence { get; private set; }
        public int HP { get; private set; }
        public int MP { get; private set; }
        public int DodgePercentage { get; private set; }

        public ActorStats(PrimaryStat primaryStat, AttackType attackType, ArmorType armorType, Team team, int hp, int mp, int strength, int dexterity, int intelligence,
            int attackRange, int movement)
        {
            PrimaryStat = primaryStat;
            AttackType = attackType;
            ArmorType = armorType;
            AttackRange = attackRange;
            Movement = movement;
            Team = team;
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
            HP = hp;
        }

        public void Buff(PrimaryStat stat, Point gridLocation, int amount, int turns)
        {
            if (stat == PrimaryStat.Primary)
                stat = PrimaryStat;

            switch (stat)
            {
                case PrimaryStat.Strength:
                    Strength += amount;
                    break;
                case PrimaryStat.Dexterity:
                    Dexterity += amount;
                    break;
                case PrimaryStat.Intelligence:
                    Intelligence += amount;
                    break;
                case PrimaryStat.Movement:
                    Movement += amount;
                    break;
                case PrimaryStat.Hp:
                    HP += amount;
                    break;
                case PrimaryStat.Mp:
                    MP += amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("stat");
            }
        }

        public void BuffArea(PrimaryStat stat, Point gridLocation, int amount, int turns)
        {
            var actorInAreaList = GameScreen.CurrentWorld.GetActorsInArea(gridLocation, amount);

            foreach (var actor in actorInAreaList)
            {
                var userActor = actor as UserActorWithStats;

                if(userActor != null)
                    userActor.Stats.Buff(stat, gridLocation, amount, turns);
            }
        }

        public int GetAttackPower()
        {         
            switch (PrimaryStat)
            {
                case PrimaryStat.Strength:
                    return PrimaryStat.Generate(AttackType, Strength);
                case PrimaryStat.Dexterity:
                    return PrimaryStat.Generate(AttackType, Dexterity);
                case PrimaryStat.Intelligence:
                    return PrimaryStat.Generate(AttackType, Intelligence);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int ReduceHp(int damage)
        {
            HP = HP - damage;

            return HP;
        }
    }
}
