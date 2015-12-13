using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Conditions;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using JetBrains.Annotations;

namespace Assets.TwoButtonRPGEngine.Enemies
{
    public abstract class BaseMonster : ICombatEntity
    {
        public enum Monsters
        {
            Slime = 1,
            Gem = 2,
            Octo = 3,
            Wolf = 4,
            Scarecrow = 5,
        }

        private static int _currentCharacterId = 0;

        public BattleModel Battle { get; set; }

        public int EntityId { get; set; }
        public Monsters Monster;

        public string EngineName { get; set; }
        public string PublicName { get; set; }
        public bool IsPlayerControlled { get; private set; }

        public BaseMonster(BattleModel battle, string engineName, string publicName, int battlePosition, Monsters monster, int health, int power, int defense, int speed)
        {
            Battle = battle;

            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            Monster = monster;

            Health = health;
            MaxHealth = health;

            BattlePosition = battlePosition;

            Power = power;
            Defense = defense;
            Speed = speed;

            Conditions = new List<BaseEntityCondition>();

            IsPlayerControlled = false;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }


        public BaseMonster(BattleModel battle, string engineName, string publicName, int battlePosition, int health, int mana, int power, int defense, int speed)
        {
            Battle = battle;

            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            Health = health;
            MaxHealth = health;

            Mana = mana;
            MaxMana = mana;

            BattlePosition = battlePosition;

            Defense = defense;
            Speed = speed;

            Conditions = new List<BaseEntityCondition>();

            IsPlayerControlled = true;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }

        public abstract List<BaseEvent> GetAction(BattleModel battle);

        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public int Mana { get; set; }
        public int MaxMana { get; set; }

        public int BattlePosition { get; set; }

        public int Power { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int SpeedModifier { get; set; }
        public int CurrentTimer { get; set; }

        public List<BaseEntityCondition> Conditions { get; set; }

        public abstract BaseDamageStrategy BaseDamageStrategy { get; set; }
        public abstract string GetMonsterInformation();
    }
}
