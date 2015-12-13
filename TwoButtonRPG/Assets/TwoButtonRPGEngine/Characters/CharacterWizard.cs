using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    class CharacterWizard : BaseCharacter
    {
        private static int _wizardCount = 0;

        private const int HEALTH_BASE = 50;
        private const float HEALTH_VARIANCE = 0.05f;

        private const int ATTACK_BASE = 20;
        private const float ATTACK_VARIANCE = 0.15f;

        private const int DEFENSE_BASE = 8;
        private const float DEFENSE_VARIANCE = 0.05f;

        private const int SPEED_BASE = 20;
        private const float SPEED_VARIANCE = 0.05f;

        public static CharacterWizard CreateCharacter(int battlePosition)
        {
            return new CharacterWizard("Wizard", battlePosition,
                VarianceHelper.GetResult(HEALTH_BASE, HEALTH_VARIANCE),
                VarianceHelper.GetResult(ATTACK_BASE, ATTACK_VARIANCE),
                VarianceHelper.GetResult(DEFENSE_BASE, DEFENSE_VARIANCE),
                VarianceHelper.GetResult(SPEED_BASE, SPEED_VARIANCE));
        }

        public CharacterWizard(string publicName, int battlePosition, int health, int power, int defense, int speed) : base("Wizard" + _wizardCount++, publicName, battlePosition, CharacterClasses.Wizard, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override CharacterAbility Ability1()
        {
            return new FireblastAbility(this);
        }

        public override CharacterAbility Ability2()
        {
            return new FireWaveAbility(this);
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

    class FireblastAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle) {
            var monsters = battle.Monsters;

            // Reset the Speed Modifier
            Character.SpeedModifier = 0;

            // Get the highest health monster.
            monsters.Sort((x, y) => x.Health.CompareTo(y.Health));
            monsters.Reverse();

            var target = monsters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(Character.Power, 0.1f) * 4 - VarianceHelper.GetResult(other.Defense, 0.1f) * 2;

            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(Character, DamageSource.DamageTypes.Arcane, damageFormula));

            return damage;
        }

        public FireblastAbility(BaseCharacter character) : base(character, "Fireblast", "Fireblast the enemy with the most health.")
        {
        }
    }

    class FireWaveAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            // Reset the Speed Modifier
            Character.SpeedModifier = -10;

            var monsters = battle.Monsters;

            // Get the highest health monster.
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(Character.Power, 0.1f) * 3 - VarianceHelper.GetResult(other.Defense, 0.1f) * 2;

            var events = new List<BaseEvent>();
            foreach (var target in monsters)
            {
                var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(Character, DamageSource.DamageTypes.Arcane, damageFormula));
                events.AddRange(damage);
            }

            return events;
        }

        public FireWaveAbility(BaseCharacter character) : base(character, "Firewave", "Strike all enemies with fire!")
        {
        }
    }
}
