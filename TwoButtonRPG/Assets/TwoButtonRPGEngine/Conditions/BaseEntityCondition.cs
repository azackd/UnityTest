using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Event;

namespace Assets.TwoButtonRPGEngine.Conditions
{
    public abstract class BaseEntityCondition
    {
        public enum ConditionID
        {
            Taunt = 1,
        }

        public String Name { get; set; }
        public ConditionID ConditionId { get; set; }
        public int Duration { get; set; }

        public abstract List<BaseEvent> OnTurnStart();
    }

    public class TauntCondition : BaseEntityCondition
    {
        public TauntCondition()
        {
            Name = "Taunt";
            ConditionId = ConditionID.Taunt;
            Duration = 1;
        }

        public override List<BaseEvent> OnTurnStart()
        {
            Duration--;
            return new List<BaseEvent>();
        }
    }

    
}
