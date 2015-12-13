using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Conditions;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;
using Mono.Cecil.Cil;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.TwoButtonRPGEngine.Characters
{
    class CharacterRogue : BaseCharacter
    {
        private static int _rogueCount = 0;

        private const int HEALTH_BASE = 50;
        private const float HEALTH_VARIANCE = 0.05f;

        private const int ATTACK_BASE = 10;
        private const float ATTACK_VARIANCE = 0.05f;

        private const int DEFENSE_BASE = 5;
        private const float DEFENSE_VARIANCE = 0.05f;

        private const int SPEED_BASE = 30;
        private const float SPEED_VARIANCE = 0.05f;

        public static CharacterRogue CreateCharacter(int battlePosition)
        {
            return new CharacterRogue("Rogue", battlePosition,
                VarianceHelper.GetResult(HEALTH_BASE, HEALTH_VARIANCE),
                VarianceHelper.GetResult(ATTACK_BASE, ATTACK_VARIANCE),
                VarianceHelper.GetResult(DEFENSE_BASE, DEFENSE_VARIANCE),
                VarianceHelper.GetResult(SPEED_BASE, SPEED_VARIANCE));
        }

        public CharacterRogue(string publicName, int battlePosition, int health, int power, int defense, int speed) :
            base("Rogue" + _rogueCount++, publicName, battlePosition, CharacterClasses.Rogue, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override CharacterAbility Ability1()
        {
            return new SiphonAttack(this);
        }

        public override CharacterAbility Ability2()
        {
            return new EvasionAbility(this);
        }

        public override CharacterAbility Ability3()
        {
            return new DetectEnemyAbility(this);
        }

        public override CharacterAbility Ability4()
        {
            return new WaitAbility(this);
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }
    }

    class SiphonAttack : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            // Reset the Speed Modifier
            Character.SpeedModifier = 0;

            var monsters = battle.Monsters;

            // Get the lowest health monster.
            monsters.Sort((x, y) => x.Health.CompareTo(y.Health));

            var target = monsters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(Character.Power, 0.2f) * 4 - VarianceHelper.GetResult(other.Defense, 0.2f) * 2;

            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(Character, DamageSource.DamageTypes.Physical, damageFormula));

            return damage;
        }

        public SiphonAttack(BaseCharacter character) : base(character, "Siphon Enemy", "Steal the ability power of enemies on kill.")
        {
        }
    }

    class DetectEnemyAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            // Reset the Speed Modifier
            Character.SpeedModifier = -20;

            var monsters = battle.Monsters;

//            foreach()
//            Func<ICombatEntity, int> damageFormula =
//                other =>
//                    VarianceHelper.GetResult(Character.Power, 0.2f) * 4 - VarianceHelper.GetResult(other.Defense, 0.2f) * 1;
//
//            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(Character, DamageSource.DamageTypes.Physical, damageFormula));

            var list = new List<BaseEvent>();
            monsters.ForEach(x => list.Add(new DetectEnemyEvent(x)));

            return list;
        }

        public DetectEnemyAbility(BaseCharacter character) : base(character, "Study Enemy", "Study all enemies to learn their weaknesses.")
        {
        }
    }

    class EvasionAbility : CharacterAbility
    {
        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            // Reset the Speed Modifier
            Character.SpeedModifier = -20;

            var list = new List<BaseEvent>();
            list.Add(new ConditionGainedEvent(Character, new EvasionCondition()));
            return list;
        }

        public EvasionAbility(BaseCharacter character) : base(character, "Evasion", "Dodge all enemy attacks making them do nothing")
        {
        }
    }
}
