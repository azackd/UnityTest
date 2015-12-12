using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using JetBrains.Annotations;

namespace Assets.TwoButtonRPGEngine.Enemies
{
    abstract class BaseMonster : ICombatEntity
    {
        private static int _currentCharacterId = 0;
        public int EntityId { get; set; }
        public string EngineName { get; set; }
        public string PublicName { get; set; }
        public bool IsPlayerControlled { get; private set; }

        public BaseMonster(string engineName, string publicName, int hp, int defense, int speed)
        {
            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            Hp = hp;
            Defense = defense;
            Speed = speed;

            IsPlayerControlled = true;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }

        public abstract List<BaseEvent> GetAction(BattleModel battle);

        public int Hp { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int SpeedModifier { get; set; }
        public int CurrentTimer { get; set; }

        public abstract BaseDamageStrategy BaseDamageStrategy { get; set; }
    }
}
