using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class WaitedEvent : BaseEvent
    {
        public WaitedEvent(ICombatEntity source)
            : base((int)EventID.WaitedEventId, "WaitedEvent", source, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = String.Format("{0} gets ready!", SourceEntity.PublicName);
        }
    }
}
