using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Enemies;
using Assets.TwoButtonRPGEngine.Helpers;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    class MagicImmuneBaseDamageStrategy : BaseDamageStrategy
    {
        public MagicImmuneBaseDamageStrategy(ICombatEntity entity) : base(entity)
        {
        }

        public override DamageResult TakeDamage(DamageSource damageSource)
        {
            if (damageSource.DamageType == DamageSource.DamageTypes.Physical)
            {
                return new DamageResult(1);
            }

            return new DamageResult(Mathf.Max(1, damageSource.BaseDamageFormula(Entity)));
        }


    }
}
