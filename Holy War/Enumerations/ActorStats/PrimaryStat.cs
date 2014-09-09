using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Enumerations.ActorStats
{
    public enum PrimaryStat
    {
        Strength,
        Dexterity,
        Intelligence
    }

    public static partial class Extensions
    {
        public static int Generate(this PrimaryStat primaryStat, AttackType attackType, int stat)
        {
            switch (primaryStat)
            {
                case PrimaryStat.Strength:
                    return (int)GenerateStrengthAttackPower(attackType, stat);
                case PrimaryStat.Dexterity:
                    return (int)GenerateDexterityAttackPower(attackType, stat);
                case PrimaryStat.Intelligence:
                    return (int)GenerateIntelligenceAttackPower(attackType, stat);
                default:
                    throw new ArgumentOutOfRangeException("primaryStat");
            }
        }

        private static float GenerateStrengthAttackPower(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat;
                case AttackType.Slashing:
                    return stat;
                case AttackType.Blunt:
                    return stat * 1.5f;
                case AttackType.Magical:
                    return stat;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }

        private static float GenerateDexterityAttackPower(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat * 1.5f;
                case AttackType.Slashing:
                    return stat;
                case AttackType.Blunt:
                    return stat;
                case AttackType.Magical:
                    return stat;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }

        private static float GenerateIntelligenceAttackPower(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat;
                case AttackType.Slashing:
                    return stat;
                case AttackType.Blunt:
                    return stat;
                case AttackType.Magical:
                    return stat * 1.5f;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }
    }
}
