using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Enemies;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    abstract class BaseDamageStrategy
    {
        public BaseMonster Entity { get; set; }

        public BaseDamageStrategy(BaseMonster entity)
        {
            Entity = entity;
        }

        public abstract DamageResult TakeDamage(DamageSource damageSource);
    }
}
