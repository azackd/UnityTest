using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Event;

namespace Assets.TwoButtonRPGEngine.Battle_Queue
{
    class BattleQueue
    {
        private List<BaseEvent> CombatEventQueue;

        public BattleModel Battle;

        public BattleQueue(BattleModel battle)
        {
            Battle = battle;

            CombatEventQueue = new List<BaseEvent>();
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
                CombatEventQueue.RemoveAt(0);
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
