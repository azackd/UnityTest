using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Enemies;
using Assets.TwoButtonRPGEngine.Event;

namespace Assets.TwoButtonRPGEngine.DamageSystem
{
    public abstract class BaseDamageStrategy
    {
        public ICombatEntity Entity { get; set; }

        public BaseDamageStrategy(ICombatEntity entity)
        {
            Entity = entity;
        }

        public abstract List<BaseEvent> TakeDamage(DamageSource damageSource);
    }
}
