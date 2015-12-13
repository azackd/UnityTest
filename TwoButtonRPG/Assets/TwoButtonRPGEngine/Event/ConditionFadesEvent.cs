using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Conditions;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class ConditionFadesEvent : BaseEvent
    {
        public BaseEntityCondition Condition { get; set; }

        public ConditionFadesEvent(ICombatEntity target, BaseEntityCondition condition)
            : base((int)EventID.ConditionFadesEventId, "ConditionFadesEvent", null, target)
        {
            Condition = condition;
        }

        public override void ResolveEvent(out string message)
        {
            TargetEntity.Conditions.Remove(Condition);
            message = string.Format("{0}'s {1} condition fades", TargetEntity.PublicName, Condition.Name);
        }
    }
}
