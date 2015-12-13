using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;

namespace Assets.TwoButtonRPGEngine.Event
{
    class DeadCharacterEvent : BaseEvent
    {
        public DeadCharacterEvent(ICombatEntity source)
            : base((int)EventID.DeadCharacterEventId, "DeadCharacterEvent", source, null)
        {
            
        }

        public override void ResolveEvent(out string message)
        {
            message = String.Format("{0} has died!", SourceEntity.PublicName);
            SourceEntity.Battle.Characters.Remove((BaseCharacter)SourceEntity);
        }
    }
}
