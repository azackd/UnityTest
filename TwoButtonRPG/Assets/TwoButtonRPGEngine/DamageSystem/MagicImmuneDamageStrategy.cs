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
    class MagicImmuneDamageStrategy : BaseDamageStrategy
    {
        public MagicImmuneDamageStrategy(ICombatEntity entity) : base(entity)
        {
        }

        public override List<BaseEvent> TakeDamage(DamageSource damageSource)
        {
            if (damageSource.DamageType == DamageSource.DamageTypes.Physical)
            {

                return new List<BaseEvent>() { new ImmuneDamageEvent(damageSource.Attacker, Entity, UnityEngine.Random.Range(1,5)) };
            }

            return new List<BaseEvent>()
            {
                new AbilityDamageEvent(damageSource.Attacker, Entity,
                    Mathf.Max(1, damageSource.BaseDamageFormula(Entity)))
            };
        }


    }
}
