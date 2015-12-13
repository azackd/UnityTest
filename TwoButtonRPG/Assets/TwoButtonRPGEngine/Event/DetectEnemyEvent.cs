using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Enemies;

namespace Assets.TwoButtonRPGEngine.Event
{
    class DetectEnemyEvent : BaseEvent
    {
        public DetectEnemyEvent(ICombatEntity target)
            : base((int)EventID.DetectEnemyEventId, "DetectEnemyEvent", null, target, false)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = String.Format("Info: {0}", ((BaseMonster)TargetEntity).GetMonsterInformation());
        }
    }
}
