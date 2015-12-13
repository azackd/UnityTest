using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Enemies;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    class MagicAbsorbDamageStrategy : BaseDamageStrategy
    {
        public MagicAbsorbDamageStrategy(ICombatEntity entity) : base(entity)
        {
        }

        public override List<BaseEvent> TakeDamage(DamageSource damageSource)
        {
            if (damageSource.DamageType == DamageSource.DamageTypes.Arcane)
            {
                return new List<BaseEvent>() { new DamageAbsorbEvent(damageSource.Attacker, Entity, 2 * Mathf.Max(1, damageSource.BaseDamageFormula(Entity))) };
            }

            return new List<BaseEvent>()
            {
                new AbilityDamageEvent(damageSource.Attacker, Entity,
                    Mathf.Max(1, damageSource.BaseDamageFormula(Entity)))
            };
        }


    }
}
