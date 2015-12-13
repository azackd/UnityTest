using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;

namespace Assets.TwoButtonRPGEngine.Event
{
    class DeadMonsterEvent : BaseEvent
    {
        public DeadMonsterEvent(ICombatEntity source)
            : base((int)EventID.DeadMonsterEventId, "DeadMonsterEvent", source, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = String.Format("{0} has died!", SourceEntity.PublicName);
            SourceEntity.Battle.Monsters.Remove((BaseMonster)SourceEntity);
        }
    }
}
