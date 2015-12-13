using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Conditions;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class ConditionGainedEvent : BaseEvent
    {
        public BaseEntityCondition Condition { get; set; }

        public ConditionGainedEvent(ICombatEntity target, BaseEntityCondition condition)
            : base((int)EventID.ConditionGainedEventId, "ConditionGainedEvent", null, target)
        {
            Condition = condition;
        }

        public override void ResolveEvent(out string message)
        {
            TargetEntity.Conditions.Add(Condition);
            message = string.Format("{0} gained the {1} condition", TargetEntity.PublicName, Condition.Name);
        }
    }
}
