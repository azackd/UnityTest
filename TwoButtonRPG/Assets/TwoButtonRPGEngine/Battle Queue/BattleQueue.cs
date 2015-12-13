using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assets.TwoButtonRPGEngine.Event;
using UnityEngine.SocialPlatforms.GameCenter;

namespace Assets.TwoButtonRPGEngine.Battle_Queue
{
    public class BattleQueue
    {
        private List<BaseEvent> CombatEventQueue;

        public BattleModel Battle { get; set; }
        public string LastMessage { get; set; }

        public BattleQueue(BattleModel battle)
        {
            Battle = battle;

            CombatEventQueue = new List<BaseEvent>();
            CombatEventQueue.Add(new BattleStartEvent());
        }

        public bool IsEmpty()
        {
            return CombatEventQueue.Count == 0;
        }

        /// <summary>
        /// Get the next event in the queue
        /// </summary>
        /// <returns>The next event in the queue</returns>
        public BaseEvent GetEvent()
        {
            if (CombatEventQueue.Count == 0)
            {
                if (Battle.Characters.Count == 0)
                {
                    CombatEventQueue.Add(new BattleWonEvent());
                    return CombatEventQueue[0];
                }
                else if (Battle.Monsters.Count == 0)
                {
                    CombatEventQueue.Add(new BattleWonEvent());
                    return CombatEventQueue[0];
                }

                var deadCharacters = Battle.Characters.Where(x => x.Health <= 0);
                var deadMonsters = Battle.Monsters.Where(x => x.Health <= 0);

                deadCharacters.ToList().ForEach(x => CombatEventQueue.Add(new DeadCharacterEvent(x)));
                deadMonsters.ToList().ForEach(x => CombatEventQueue.Add(new DeadMonsterEvent(x)));

                if (CombatEventQueue.Count != 0) return CombatEventQueue[0];

                var nextTurnEntity = Battle.GetNextTurnEntity();

                // Start a new turn
                CombatEventQueue.Add(new NextEntityTurnEvent(nextTurnEntity));

                // Apply all of the condition effects.
                var conditionStartTurnList = new List<BaseEvent>();
                nextTurnEntity.Conditions.ForEach(x => conditionStartTurnList.AddRange(x.OnTurnStart()));
                conditionStartTurnList.RemoveAll(x => x == null);

                var fadingConditions = nextTurnEntity.Conditions.Where(x => x.Duration == 0).ToList();
                fadingConditions.ForEach(x => conditionStartTurnList.Add(new ConditionFadesEvent(nextTurnEntity, x)));

                // Get the player's input.
                CombatEventQueue.AddRange(conditionStartTurnList);
                CombatEventQueue.Add(new PendingActionEvent(nextTurnEntity));

                if (!nextTurnEntity.IsPlayerControlled)
                {
                    CombatEventQueue.AddRange(nextTurnEntity.GetAction(Battle));
                }
            }

            return CombatEventQueue[0];
        }

        /// <summary>
        /// Mark the event in the queue as resolved, removing it.
        /// </summary>
        public void ResolveEvent()
        {
            if (CombatEventQueue.Count != 0)
            {
                var message = "";

                CombatEventQueue[0].ResolveEvent(out message);
                CombatEventQueue.RemoveAt(0);

                LastMessage = message;
            }
        }

        /// <summary>
        /// Allows the game to add player interactions into the queue.
        /// </summary>
        /// <param name="addedEvent"></param>
        public void AddEvent (BaseEvent addedEvent) {
            CombatEventQueue.Add(addedEvent);
        }
    }
}
