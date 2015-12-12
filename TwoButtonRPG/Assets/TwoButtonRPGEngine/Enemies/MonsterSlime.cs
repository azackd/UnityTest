using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using Assets.TwoButtonRPGEngine.Helpers;

namespace Assets.TwoButtonRPGEngine.Enemies
{
    class MonsterSlime : BaseMonster
    {
        public int ATTACK_BASE = 5;

        public static MonsterSlime CreateMonster()
        {
            return new MonsterSlime("Slimy Slime", 20, 10, 10, 20);
        }

        public MonsterSlime(string publicName, int health, int power, int defense, int speed) : base("Slime0", publicName, health, power, defense, speed)
        {
            BaseDamageStrategy = new MagicImmuneBaseDamageStrategy(this);
        }

        public override List<BaseEvent> GetAction(BattleModel battle)
        {
            var target = battle.Characters[0];
            Func<ICombatEntity, int> damageFormula =
                other =>
                    VarianceHelper.GetResult(ATTACK_BASE, 0.05f) * 4 - VarianceHelper.GetResult(other.Defense, 0.05f) * 2;
            var damage = target.BaseDamageStrategy.TakeDamage(new DamageSource(this, DamageSource.DamageTypes.Physical, damageFormula));

            return new List<BaseEvent>() { new AbilityDamageEvent(this, target, damage.DamageTaken) };
        }

        public override BaseDamageStrategy BaseDamageStrategy { get; set; }
    }
}
