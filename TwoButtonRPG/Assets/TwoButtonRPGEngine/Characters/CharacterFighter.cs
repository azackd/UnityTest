using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Event;
using Random = UnityEngine.Random;

namespace Assets.TwoButtonRPGEngine.Characters
{
    class CharacterFighter : BaseCharacter
    {
        private static int _fighterCount = 0;

        private const int HEALTH_MIN = 20;
        private const int HEALTH_MAX = 20;

        private const int DEFENSE_MIN = 3;
        private const int DEFENSE_MAX = 3;

        private const int SPEED_MIN = 20;
        private const int SPEED_MAX = 20;

        public static CharacterFighter CreateFighter()
        {
            return new CharacterFighter("Fighter", Random.Range(HEALTH_MIN, HEALTH_MAX), Random.Range(DEFENSE_MIN, DEFENSE_MAX), Random.Range(SPEED_MIN, SPEED_MAX));
        }

        public CharacterFighter(string publicName, int hp, int defense, int speed) : base("Fighter" + _fighterCount++, publicName, hp, defense, speed)
        {

        }

        public override BaseEvent UseAbility1(BattleModel battle)
        {
            throw new NotImplementedException();
        }

        public override BaseEvent UseAbility2(BattleModel battle)
        {
            throw new NotImplementedException();
        }

        public override BaseEvent UseAbility3(BattleModel battle)
        {
            throw new NotImplementedException();
        }

        public override BaseEvent UseWait(BattleModel battle)
        {
            throw new NotImplementedException();
        }
    }
}
