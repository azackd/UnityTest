﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;

namespace Assets.TwoButtonRPGEngine.Battle_Queue
{
    class BattleModel
    {
        public List<BaseCharacter> Characters;
        public List<BaseMonster> Monsters;

        public List<ICombatEntity> CombatEntities
        {
            get
            {
                return Characters.Cast<ICombatEntity>().Union(Monsters.Cast<ICombatEntity>()).ToList();
            }
        } 

        public ICombatEntity CurrentTurnEntity;

        public BattleModel()
        {
            Characters = new List<BaseCharacter>();
            Monsters = new List<BaseMonster>();
        }

        public ICombatEntity GetNextTurnEntity()
        {
            var speedSanityCheck = CombatEntities.Any(x => x.Speed + x.SpeedModifier > 0);
            if (!speedSanityCheck) throw new Exception("At least one of the characters must have a speed greater than zero");

            // Sort them by the highest count
            CombatEntities.Sort(
                (x, y) =>
                    (x.CurrentTimer + x.Speed + x.SpeedModifier).CompareTo(y.CurrentTimer + y.Speed + y.SpeedModifier));

            // Find the entity with the highest count.
            CombatEntities.Reverse();

            // Check to see if there is a person over 100
            var fastestCharacter = CombatEntities[0];
            if (fastestCharacter.CurrentTimer >= 100)
            {
                fastestCharacter.CurrentTimer -= 100;
                return fastestCharacter;
            }

            CombatEntities.ForEach(x => x.CurrentTimer += x.Speed + x.SpeedModifier);
            return GetNextTurnEntity();
        }

    }
}
