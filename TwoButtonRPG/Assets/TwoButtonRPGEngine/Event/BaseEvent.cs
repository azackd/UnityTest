using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using JetBrains.Annotations;

namespace Assets.TwoButtonRPGEngine.Event
{
    public abstract class BaseEvent
    {
        public enum EventID
        {
            BattleWonEventId = -1,
            BattleLostEventId = -2,

            BattleStartEventId = 0,

            NextEntityTurnEventId = 1,
            WaitedEventId = 2,
            DeadCharacterEventId = 3,
            DeadMonsterEventId = 4,
            PendingActionEventId = 5,
            AbilityDamageEventId = 100,
            AbilityHealEventId = 101,
            ImmuneDamageEventId = 102,
            DamageAbsorbEventId = 103,

            DetectEnemyEventId = 201,

            ConditionGainedEventId = 301,
            ConditionFadesEventId = 302,
        }

        public int EventId { get; private set; }
        public String EventName { get; private set; }
        public bool WaitingForInput { get; private set; }

        public ICombatEntity SourceEntity { get; private set; }
        public ICombatEntity TargetEntity { get; private set; }

        public BaseEvent(int EventId, String EventName, ICombatEntity SourceEntity,
            ICombatEntity TargetEntity, bool WaitingForInput = false)
        {
            this.EventId = EventId;
            this.EventName = EventName;
            this.SourceEntity = SourceEntity;
            this.TargetEntity = TargetEntity;
            this.WaitingForInput = WaitingForInput;
        }

        /// <summary>
        /// Resolve the event
        /// </summary>
        /// <returns> Remove the Event from the Event Queue. </returns>
        public abstract void ResolveEvent(out string message);
    }
}
