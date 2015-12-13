using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class DamageAbsorbEvent : BaseEvent
    {
        public int Damage { get; set; }

        public DamageAbsorbEvent(ICombatEntity source, ICombatEntity target, int damage)
            : base((int)BaseEvent.EventID.DamageAbsorbEventId, "DamageAbsorbEvent", source, target)
        {
            Damage = damage;
        }

        public override void ResolveEvent(out string message) 
        {
            TargetEntity.MaxHealth += Damage;
            TargetEntity.Health = TargetEntity.MaxHealth;

            TargetEntity.Power += 10;
            TargetEntity.Defense += 10;

            TargetEntity.Speed += 10;

            message = String.Format("{0} absorbs spell damage and grows stronger", TargetEntity.PublicName);
        }
    }
}
