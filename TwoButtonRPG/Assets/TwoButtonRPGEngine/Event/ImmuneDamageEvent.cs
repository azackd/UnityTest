using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class ImmuneDamageEvent : BaseEvent
    {
        public int Damage { get; set; }

        public ImmuneDamageEvent(ICombatEntity source, ICombatEntity target, int damage)
            : base((int)BaseEvent.EventID.ImmuneDamageEventId, "ImmuneDamageEvent", source, target)
        {
            Damage = damage;
        }

        public override void ResolveEvent(out string message) 
        {
            TargetEntity.Health -= Damage;
            message = String.Format("{0} is nearly immune!", TargetEntity.PublicName);

            // TODO: Replace with an event hook!
            var character = SourceEntity.Battle.Characters.FirstOrDefault(x => x.Equals(SourceEntity));
            if (character != null && character.CharacterClass == BaseCharacter.CharacterClasses.Rogue && TargetEntity.Health <= 0)
            {
                SourceEntity.MaxHealth += 3;
                SourceEntity.Health += 3;

                SourceEntity.Power += 2;
                SourceEntity.Defense += 1;

                SourceEntity.Speed += 2;
            }
        }
    }
}
