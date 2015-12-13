using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Event;

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
                var nextTurnEntity = Battle.GetNextTurnEntity();
                CombatEventQueue.Add(new NextEntityTurnEvent(nextTurnEntity));

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
