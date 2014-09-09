﻿using System;
using Holy_War.Enumerations;
using Holy_War.Enumerations.ActorStats;
using Holy_War.Zones;
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
        public int DodgePercentage { get; private set; }

        public ActorStats(PrimaryStat primaryStat, AttackType attackType, ArmorType armorType, Team team, int hp, int strength, int dexterity, int intelligence,
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
