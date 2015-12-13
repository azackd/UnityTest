using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class BattleStartEvent : BaseEvent
    {
        public BattleStartEvent()
            : base((int)EventID.BattleStartEventId, "BattleStartEvent", null, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = "";
        }
    }
}
