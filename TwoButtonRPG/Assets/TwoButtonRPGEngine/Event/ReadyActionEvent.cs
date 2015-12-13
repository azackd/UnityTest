using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class ReadyActionEvent : BaseEvent
    {
        public ReadyActionEvent(ICombatEntity source)
            : base((int)EventID.ReadyActionEventId, "ReadyActionEvent", source, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            SourceEntity.Power += 3;
            message = String.Format("{0} powers up!", SourceEntity.PublicName);
        }
    }
}
