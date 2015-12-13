using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Enemies
{
    class MonsterGem : BaseMonster
    {
        // public int ATTACK_BASE = 5;

        public static MonsterGem CreateMonster(BattleModel battle, int battlePosition)
        {
            return new MonsterGem(battle, "Gem", battlePosition, 100, 5, 5, 10);
        }

        public MonsterGem(BattleModel battle, string publicName, int battlePosition, int health, int power, int defense, int speed) : base(battle, "Gem", publicName, battlePosition, Monsters.Gem, health, power, defense, speed)
        {
            BaseDamageStrategy = new MagicAbsorbDamageStrategy(this);
        }

        public override List<BaseEvent> GetAction(BattleModel battle)
        {
            var target = battle.Characters[UnityEngine.Random.Range(0, battle.Characters.Count)];
            Func<ICombatEntity, int> damageFormula =
                other =>
                        VarianceHelper.GetResult(Power, 0.1f) *4 - VarianceHelper.GetResult(other.Defense, 0.1f) *2;
            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical, damageFormula));

            return damage;
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }

        public override string GetMonsterInformation()
        {
            return "Absorbs arcane damage and becomes powerful.";
        }
    }
}
