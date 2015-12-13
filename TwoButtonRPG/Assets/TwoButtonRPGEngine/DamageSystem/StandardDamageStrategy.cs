using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Conditions;
using Assets.TwoButtonRPGEngine.Enemies;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    class StandardDamageStrategy : BaseDamageStrategy
    {
        public StandardDamageStrategy(ICombatEntity entity) : base(entity)
        {
        }

        public override List<BaseEvent> TakeDamage( DamageSource damageSource)
        {
            var evasion = Entity.Conditions.FirstOrDefault(x => x.ConditionId == BaseEntityCondition.ConditionID.Evasion);
            if (evasion != null)
            {
                return new List<BaseEvent>()
                {
                    new AbilityDamageEvent(damageSource.Attacker, Entity, 0)
                };
            }

            return new List<BaseEvent>()
            {
                new AbilityDamageEvent(damageSource.Attacker, Entity,
                    Mathf.Max(1, damageSource.BaseDamageFormula(Entity)))
            };
        }


    }
}
