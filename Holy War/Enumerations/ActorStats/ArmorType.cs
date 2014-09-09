using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holy_War.Enumerations.ActorStats
{
    public enum ArmorType
    {
        None,
        Light,
        Medium,
        Heavy,
        Godly
    }

    public static partial class Extensions
    {
        public static int PerformReduction(this ArmorType armorType, AttackType attackType, int attackPower)
        {
            switch (armorType)
            {
                case ArmorType.None:
                    return attackPower;
                case ArmorType.Light:
                    return (int)GenerateLightArmorReduction(attackType, attackPower);
                case ArmorType.Medium:
                    return (int)GenerateMediumArmorReduction(attackType, attackPower);
                case ArmorType.Heavy:
                    return (int)GenerateHeavyArmorReduction(attackType, attackPower);
                case ArmorType.Godly:
                    return (int)GenerateGodlyArmorReduction(attackType, attackPower);
                default:
                    throw new ArgumentOutOfRangeException("armorType");
            }
        }

        private static float GenerateLightArmorReduction(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat * 0.8f;
                case AttackType.Slashing:
                    return stat * 0.8f;
                case AttackType.Blunt:
                    return stat * 0.8f;
                case AttackType.Magical:
                    return stat * 0.8f;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }

        private static float GenerateMediumArmorReduction(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat * 0.6f;
                case AttackType.Slashing:
                    return stat * 0.8f;
                case AttackType.Blunt:
                    return stat * 0.6f;
                case AttackType.Magical:
                    return stat * 0.7f;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }

        private static float GenerateHeavyArmorReduction(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat * 0.4f;
                case AttackType.Slashing:
                    return stat * 0.4f;
                case AttackType.Blunt:
                    return stat * 0.4f; 
                case AttackType.Magical:
                    return stat * 0.7f;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }

        private static float GenerateGodlyArmorReduction(AttackType attackType, float stat)
        {
            switch (attackType)
            {
                case AttackType.Piercing:
                    return stat * 0.6f;
                case AttackType.Slashing:
                    return stat * 0.6f;
                case AttackType.Blunt:
                    return stat * 0.6f;
                case AttackType.Magical:
                    return stat * 0.5f;
                default:
                    throw new ArgumentOutOfRangeException("attackType");
            }
        }
    }
}
