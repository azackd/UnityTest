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
    class CharacterFighter : BaseCharacter
    {
        private static int _fighterCount = 0;

        private const int HEALTH_BASE = 100;
        private const float HEALTH_VARIANCE = 0.05f;

        private const int ATTACK_BASE = 20;
        private const float ATTACK_VARIANCE = 0.05f;

        private const int DEFENSE_BASE = 10;
        private const float DEFENSE_VARIANCE = 0.05f;

        private const int SPEED_BASE = 20;
        private const float SPEED_VARIANCE = 0.05f;

        public static CharacterFighter CreateFighter()
        {
            return new CharacterFighter("Fighter",
                VarianceHelper.GetResult(HEALTH_BASE, HEALTH_VARIANCE),
                VarianceHelper.GetResult(ATTACK_BASE, ATTACK_VARIANCE),
                VarianceHelper.GetResult(DEFENSE_BASE, DEFENSE_VARIANCE),
                VarianceHelper.GetResult(SPEED_BASE, SPEED_VARIANCE));
        }

        public CharacterFighter(string publicName, int health, int power, int defense, int speed) : base("Fighter" + _fighterCount++, publicName, CharacterClasses.Fighter, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override CharacterAbility Ability1()
        {
            return new FastAttack(this);
        }

        public override CharacterAbility Ability2()
        {
            return new SlowAttack(this);
        }

        public override CharacterAbility Ability3()
        {
            return new RegenAbility(this);
        }

        public override CharacterAbility AbilityWait()
        {
            return new WaitAbility(this);
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }
    }

    class FastAttack : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle) {
            var monsters = battle.Monsters;

            // Get the highest health monster.
            monsters.Sort((x, y) => x.Health.CompareTo(y.Health));
            monsters.Reverse();

            var target = monsters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(Character.Power, 0.05f) * 4 - VarianceHelper.GetResult(other.Defense, 0.05f) * 2;

            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(Character, DamageSource.DamageTypes.Physical, damageFormula));

            return new List<BaseEvent>() { new AbilityDamageEvent(Character, target, damage.DamageTaken) };
        }

        public FastAttack(BaseCharacter character) : base(character, "Fast Attack", "Strike the enemy with the most health.")
        {
        }
    }

    class SlowAttack : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            var monsters = battle.Monsters;

            // Get the highest health monster.
            monsters.Sort((x, y) => x.Health.CompareTo(y.Health));
            monsters.Reverse();

            var target = monsters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(Character.Power, 0.05f) * 4 - VarianceHelper.GetResult(other.Defense, 0.05f) * 1;

            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(Character, DamageSource.DamageTypes.Physical, damageFormula));

            return new List<BaseEvent>() { new AbilityDamageEvent(Character, target, damage.DamageTaken) };
        }

        public SlowAttack(BaseCharacter character) : base(character, "Slow Attack", "Strike the enemy with the most health.  Good versus Armour.")
        {
        }
    }

    class RegenAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            Character.Health += 10;

            return new List<BaseEvent>() { new AbilityHealEvent(Character, Character, 10) };
        }

        public RegenAbility(BaseCharacter character) : base(character, "Heal", "Recover some health.")
        {
        }
    }
}
