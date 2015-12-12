using System;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    internal class DamageSource
    {
        public enum DamageTypes
        {
            Physical,
            Arcane
        }

        public ICombatEntity Attacker { get; set; }
        public DamageTypes DamageType { get; set; }

        public Func<ICombatEntity, int> BaseDamageFormula { get; set; }

        public DamageSource(ICombatEntity attacker, DamageTypes damageType, Func<ICombatEntity, int> baseDamageFormula)
        {
            Attacker = attacker;
            DamageType = damageType;

            BaseDamageFormula = baseDamageFormula;
        }
    }
}
