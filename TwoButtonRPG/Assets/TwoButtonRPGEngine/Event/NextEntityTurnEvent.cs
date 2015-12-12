using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class NextEntityTurnEvent : BaseEvent
    {
        public NextEntityTurnEvent(ICombatEntity source)
            : base((int)EventID.NextEntityTurnEventId, "NextEntityTurnEvent", source, null, source.IsPlayerControlled)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = $"{SourceEntity.PublicName}'s Turn!";
        }
    }
}
