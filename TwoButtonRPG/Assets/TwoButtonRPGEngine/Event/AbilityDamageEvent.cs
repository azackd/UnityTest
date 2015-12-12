using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class AbilityDamageEvent : BaseEvent
    {
        public int Damage { get; set; }

        public AbilityDamageEvent(ICombatEntity source, ICombatEntity target, int damage)
            : base((int)BaseEvent.EventID.AbilityDamageEventId, "AbilityDamageEvent", source, target)
        {
            Damage = damage;
        }

        public override void ResolveEvent(out string message) 
        {
            TargetEntity.Hp -= Damage;
            message = String.Format("{0} took {1} damage!", TargetEntity.PublicName, Damage);
        }
    }
}
