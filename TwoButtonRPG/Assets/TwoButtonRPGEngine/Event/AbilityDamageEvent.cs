using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class AbilityDamageEvent : BaseEvent
    {
        public int Damage { get; set; }

        public AbilityDamageEvent(ICombatEntity source, ICombatEntity target, int damage)
            : base((int)BaseEvent.EventID.AbilityDamageEventId, "AbilityDamageEvent", source, target)
        {
            Damage = damage;
        }

        public override void ResolveEvent(out string message) 
        {
            TargetEntity.Health -= Damage;
            message = String.Format("{0} took {1} damage!", TargetEntity.PublicName, Damage);

            // TODO: Replace with an event hook!
            var character = SourceEntity.Battle.Characters.FirstOrDefault(x => x.Equals(SourceEntity));
            if (character != null && character.CharacterClass == BaseCharacter.CharacterClasses.Rogue && TargetEntity.Health <= 0)
            {
                SourceEntity.MaxHealth += 0;
                SourceEntity.Health += 0;

                SourceEntity.Power += 1;
                SourceEntity.Defense += 0;

                SourceEntity.Speed += 2;
            }
        }
    }
}
