using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.DamageSystem;
using Assets.TwoButtonRPGEngine.Event;

namespace Assets.TwoButtonRPGEngine.Battle_Queue
{
    interface ICombatEntity
    {
        int EntityId { get; set; }

        string EngineName { get; set; }
        string PublicName { get; set; }

        bool IsPlayerControlled { get; }
        List<BaseEvent> GetAction(BattleModel battle);

        int Hp { get; set; }
        
        int Defense { get; set; }

        int Speed { get; set; }
        int SpeedModifier { get; set; }
        int CurrentTimer { get; set; }

        BaseDamageStrategy BaseDamageStrategy { get; set;  }
    }
}
