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
    public abstract class BaseMonster : ICombatEntity
    {
        private static int _currentCharacterId = 0;
        public int EntityId { get; set; }
        public string EngineName { get; set; }
        public string PublicName { get; set; }
        public bool IsPlayerControlled { get; private set; }

        public BaseMonster(string engineName, string publicName, int health, int power, int defense, int speed)
        {
            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            Health = health;
            MaxHealth = health;

            Power = power;
            Defense = defense;
            Speed = speed;

            IsPlayerControlled = false;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }


        public BaseMonster(string engineName, string publicName, int health, int mana, int power, int defense, int speed)
        {
            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            Health = health;
            MaxHealth = health;

            Mana = mana;
            MaxMana = mana;

            Defense = defense;
            Speed = speed;

            IsPlayerControlled = true;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }

        public abstract List<BaseEvent> GetAction(BattleModel battle);

        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public int Mana { get; set; }
        public int MaxMana { get; set; }

        public int Power { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int SpeedModifier { get; set; }
        public int CurrentTimer { get; set; }

        public abstract BaseDamageStrategy BaseDamageStrategy { get; set; }
    }
}
