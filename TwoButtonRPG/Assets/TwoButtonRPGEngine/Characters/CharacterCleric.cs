using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;
using Mono.Cecil.Cil;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.TwoButtonRPGEngine.Characters
{
    class CharacterCleric : BaseCharacter
    {
        private static int _clericCount = 0;

        private const int HEALTH_BASE = 60;
        private const float HEALTH_VARIANCE = 0.05f;

        private const int ATTACK_BASE = 20;
        private const float ATTACK_VARIANCE = 0.05f;

        private const int DEFENSE_BASE = 5;
        private const float DEFENSE_VARIANCE = 0.05f;

        private const int SPEED_BASE = 18;
        private const float SPEED_VARIANCE = 0.05f;

        public static CharacterCleric CreateCharacter(int battlePosition)
        {
            return new CharacterCleric("Cleric", battlePosition,
                VarianceHelper.GetResult(HEALTH_BASE, HEALTH_VARIANCE),
                VarianceHelper.GetResult(ATTACK_BASE, ATTACK_VARIANCE),
                VarianceHelper.GetResult(DEFENSE_BASE, DEFENSE_VARIANCE),
                VarianceHelper.GetResult(SPEED_BASE, SPEED_VARIANCE));
        }

        public CharacterCleric(string publicName, int position, int health, int power, int defense, int speed) : base("Cleric" + _clericCount++, publicName, position, CharacterClasses.Cleric, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override CharacterAbility Ability1()
        {
            return new HealAbility(this);
        }

        public override CharacterAbility Ability2()
        {
            return new HealAllAbility(this);
        }

        public override CharacterAbility Ability3()
        {
            return new RestoreAbility(this);
        }

        public override CharacterAbility Ability4()
        {
            return new WaitAbility(this);
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }
    }

    class HealAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            // Reset the Speed Modifier
            Character.SpeedModifier = 0;

            var characters = battle.Characters;

            var injuredCharacters = characters.FindAll(x => x.Health != x.MaxHealth);

            // Get the highest health monster.
            injuredCharacters.Sort((x, y) => x.Health.CompareTo(y.Health));

            // If there are no targets to heal, heal yourself.
            ICombatEntity target = Character;
            if (injuredCharacters.Count != 0)
            {
                target = injuredCharacters[0];
            }


            return new List<BaseEvent>() { new AbilityHealEvent(Character, target, VarianceHelper.GetResult(Character.Power, 0.2f) * 2) };
        }

        public HealAbility(BaseCharacter character) : base(character, "Heal", "Heal the character with the most wounds.")
        {
        }
    }

    class HealAllAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            // Reset the Speed Modifier
            Character.SpeedModifier = 0;

            var characters = battle.Characters;
            var healingList = new List<BaseEvent>();

            foreach (var target in characters)
            {
                healingList.Add(new AbilityHealEvent(Character, target, VarianceHelper.GetResult(Character.Power, 0.2f) * 1))
                ;
            }
            return healingList;
        }

        public HealAllAbility(BaseCharacter character) : base(character, "Heal All", "Heal every character for a tiny amount")
        {
        }
    }
}
