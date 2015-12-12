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

        private const int DEFENSE_BASE = 10;
        private const float DEFENSE_VARIANCE = 0.05f;

        private const int SPEED_BASE = 20;
        private const float SPEED_VARIANCE = 0.05f;

        public static CharacterFighter CreateFighter()
        {
            return new CharacterFighter("Fighter",
                VarianceHelper.GetResult(HEALTH_BASE, HEALTH_VARIANCE),
                VarianceHelper.GetResult(DEFENSE_BASE, DEFENSE_VARIANCE),
                VarianceHelper.GetResult(SPEED_BASE, SPEED_VARIANCE));
        }

        public CharacterFighter(string publicName, int hp, int defense, int speed) : base("Fighter" + _fighterCount++, publicName, hp, defense, speed)
        {

        }

        public override List<BaseEvent> UseAbility1(BattleModel battle)
        {
            var monsters = battle.Monsters;

            // Get the highest health monster.
            monsters.Sort((x,y) => x.Hp.CompareTo(y.Hp));
            monsters.Reverse();

            var target = monsters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(ATTACK_BASE, 0.05f)*4 - VarianceHelper.GetResult(other.Defense, 0.05f)*2;

            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical, damageFormula));

            return new List<BaseEvent>() { new AbilityDamageEvent(this, target, damage.DamageTaken) };
        }

        public override List<BaseEvent> UseAbility2(BattleModel battle)
        {
            var monsters = battle.Monsters;

            // Get the highest health monster.
            monsters.Sort((x, y) => x.Hp.CompareTo(y.Hp));
            monsters.Reverse();

            var target = monsters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(ATTACK_BASE, 0.05f) * 4 - VarianceHelper.GetResult(other.Defense, 0.05f) * 1;

            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical, damageFormula));

            // Slow Attack
            SpeedModifier = -10;

            return new List<BaseEvent>() { new AbilityDamageEvent(this, target, damage.DamageTaken) };
        }

        public override List<BaseEvent> UseAbility3(BattleModel battle)
        {
            throw new NotImplementedException();
        }

        public override List<BaseEvent> UseWait(BattleModel battle)
        {
            SpeedModifier = 50;
            return new List<BaseEvent>() { new WaitedEvent(this) };
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }
    }
}
