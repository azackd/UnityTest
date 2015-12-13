using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;

namespace Assets.TwoButtonRPGEngine.Event
{
    class BattleWonEvent : BaseEvent
    {
        public BattleWonEvent()
            : base((int)EventID.BattleWonEventId, "BattleWonEvent", null, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = String.Format("You won!");
        }
    }
}
