using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class BattleLostEvent : BaseEvent
    {
        public BattleLostEvent()
            : base((int)EventID.BattleLostEventId, "BattleLostEvent", null, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = String.Format("You lost!");
        }
    }
}
