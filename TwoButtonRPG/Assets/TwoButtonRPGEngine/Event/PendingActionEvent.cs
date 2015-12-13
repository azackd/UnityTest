using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class PendingActionEvent : BaseEvent
    {
        public PendingActionEvent(ICombatEntity source)
            : base((int)EventID.PendingActionEventId, "PendingActionEvent", source, null, source.IsPlayerControlled)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            SourceEntity.Battle.CurrentTurnEntity = SourceEntity;
            message = String.Format("{0} acts!", SourceEntity.PublicName);
        }
    }
}
