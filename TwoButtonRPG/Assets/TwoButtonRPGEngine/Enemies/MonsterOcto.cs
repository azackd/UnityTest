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
    class MonsterOcto : BaseMonster
    {
        // public int ATTACK_BASE = 5;

        public bool isAttackReadied = false;

        public static MonsterOcto CreateMonster(BattleModel battle, int battlePosition)
        {
            return new MonsterOcto(battle, "OctoFiend", battlePosition, 400, 4, 10, 20);
        }

        public MonsterOcto(BattleModel battle, string publicName, int battlePosition, int health, int power, int defense, int speed) : base(battle, "OctoFiend", publicName, battlePosition, Monsters.Octo, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override List<BaseEvent> GetAction(BattleModel battle)
        {
            var damageList = new List<BaseEvent>();

            if (!isAttackReadied)
            {
                isAttackReadied = true;
                return new List<BaseEvent>() { new ReadyActionEvent(this) };
            }

            for (int i = 0; i < 8; i++)
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
                            VarianceHelper.GetResult(Power, 0.10f) * 4 - VarianceHelper.GetResult(other.Defense, 0.05f) * 2;
                var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical, damageFormula));

                damageList.AddRange(damage);
            }

            isAttackReadied = false;
            return damageList;

        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }

        public override string GetMonsterInformation()
        {
            return "A Octopus with multiple attacks. He needs to charge up between attacks.";
        }
    }
}
