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
    class MonsterScarecrow : BaseMonster
    {
        // public int ATTACK_BASE = 5;

        public bool UseAoe = false;

        public static MonsterScarecrow CreateMonster(BattleModel battle, int battlePosition)
        {
            return new MonsterScarecrow(battle, "Scarecrow", battlePosition, 200, 9, 20, 20);
        }

        public MonsterScarecrow(BattleModel battle, string publicName, int battlePosition, int health, int power, int defense, int speed) : base(battle, "Scarecrow", publicName, battlePosition, Monsters.Scarecrow, health, power, defense, speed)
        {
            BaseDamageStrategy = new StandardDamageStrategy(this);
        }

        public override List<BaseEvent> GetAction(BattleModel battle)
        {
            var damageList = new List<BaseEvent>();

            if (!UseAoe)
            {
                UseAoe = !UseAoe;

                var target = battle.Characters[UnityEngine.Random.Range(0, battle.Characters.Count)];
                Func<ICombatEntity, int> damageFormula =
                    other =>
                        VarianceHelper.GetResult(Power, 0.10f)*4 - VarianceHelper.GetResult(other.Defense, 0.05f)*2;
                var damage =
                    target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical,
                        damageFormula));

                damageList.AddRange(damage);
                return damageList;
            }
            else
            {
                UseAoe = !UseAoe;

                foreach (var target in battle.Characters)
                {
                    Func<ICombatEntity, int> damageFormula =
                        other =>
                            VarianceHelper.GetResult(Power, 0.10f)*4 -
                            VarianceHelper.GetResult(other.Defense, 0.05f)*2;
                    var damage =
                        target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Arcane,
                            damageFormula));

                    damageList.AddRange(damage);

                }
                return damageList;
            }
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }

        public override string GetMonsterInformation()
        {
            return "A Scarecrow that mixes up area damage and single target damage";
        }
    }
}
