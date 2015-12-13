using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Conditions;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Enemies
{
    class MonsterWolf : BaseMonster
    {
        // public int ATTACK_BASE = 5;

        public static MonsterWolf CreateMonster(BattleModel battle, int battlePosition)
        {
            return new MonsterWolf(battle, "Rabid Wolf", battlePosition, 60, 7, 5, 25);
        }

        public MonsterWolf(BattleModel battle, string publicName, int battlePosition, int health, int power, int defense, int speed) : base(battle, "Wolf", publicName, battlePosition, Monsters.Wolf, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override List<BaseEvent> GetAction(BattleModel battle)
        {
            var target = battle.Characters[UnityEngine.Random.Range(0, battle.Characters.Count)];

            // Attack the taunted target mindlessly.
            var tauntedTargets =
                battle.Characters.Where(
                    x => x.Conditions.FirstOrDefault(y => y.ConditionId == BaseEntityCondition.ConditionID.Taunt) != null);

            var tauntedTargetsList = tauntedTargets.ToList();
            if (tauntedTargetsList.Count != 0) target = tauntedTargetsList[UnityEngine.Random.Range(0, tauntedTargetsList.Count)];

            Func<ICombatEntity, int> damageFormula =
                other =>
                        VarianceHelper.GetResult(Power, 0.05f) *4 - VarianceHelper.GetResult(other.Defense, 0.05f) *2;
            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical, damageFormula));

            return damage;
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }

        public override string GetMonsterInformation()
        {
            return "A fast, mindless attacker";
        }
    }
}
