using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngineInternal;

namespace Assets.TwoButtonRPGEngine.Characters
{
    public abstract class BaseCharacter : ICombatEntity
    {
        public enum CharacterClasses
        {
            Fighter,
            Rogue,
            Wizard,
            Cleric
        }

        private static int _currentCharacterId = 0;
        public int EntityId { get; set; }
        public string EngineName { get; set; }
        public string PublicName { get; set; }
        public bool IsPlayerControlled { get; private set; }

        public CharacterClasses CharacterClass { get; set; }

        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public int Mana { get; set; }
        public int MaxMana { get; set; }

        public int Power { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int SpeedModifier { get; set; }
        public int CurrentTimer { get; set; }


        public BaseCharacter(string engineName, string publicName, CharacterClasses characterClass, int health, int power, int defense, int speed)
        {
            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            CharacterClass = characterClass;

            Health = health;
            MaxHealth = health;

            Power = power;
            Defense = defense;
            Speed = speed;

            IsPlayerControlled = true;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }

        public BaseCharacter(string engineName, string publicName, CharacterClasses characterClass, int health, int mana, int power, int defense, int speed)
        {
            EntityId = ++_currentCharacterId;
            EngineName = engineName;
            PublicName = publicName;

            CharacterClass = characterClass;

            Health = health;
            MaxHealth = health;

            Mana = mana;
            MaxMana = mana;

            Power = power;
            Defense = defense;
            Speed = speed;

            IsPlayerControlled = true;
            SpeedModifier = 0;
            CurrentTimer = 0;
        }

        public List<BaseEvent> GetAction(BattleModel battle)
        {
            throw new NotImplementedException();
        }

        // Press Button 1
        public abstract CharacterAbility Ability1();

        // Hold Button 1
        public abstract CharacterAbility Ability2();

        // Press Button 2
        public abstract CharacterAbility Ability3();

        // Hold Button 2
        public abstract CharacterAbility AbilityWait();

        public abstract BaseDamageStrategy BaseDamageStrategy { get; set; }
    }

    public abstract class CharacterAbility
    {
        protected CharacterAbility(BaseCharacter character, string name, string description)
        {
            Character = character;
            Name = name;
            Description = description;
        }

        public BaseCharacter Character { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public abstract List<BaseEvent> UseAbility(BattleModel battle);
    }

    public class WaitAbility : CharacterAbility
    {

        public WaitAbility(BaseCharacter character) : base(character, "Wait", "Wait a little while for the perfect moment to act!")
        {
        }

        public override List<BaseEvent> UseAbility(BattleModel battle)
        {
            Character.SpeedModifier = 50;
            return new List<BaseEvent> {new WaitedEvent(Character) } ;
        }


    }
}
