﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using UnityEngine;

namespace Assets.TwoButtonRPGEngine.Event
{
    class AbilityHealEvent : BaseEvent
    {
        public int Healing { get; set; }

        public AbilityHealEvent(ICombatEntity source, ICombatEntity target, int damage)
            : base((int)BaseEvent.EventIDs.AbilityHealEventId, "AbilityHealEvent", source, target)
        {
            Healing = damage;
        }

        public override void ResolveEvent(out string message) 
        {
            TargetEntity.Hp -= Healing;
            message = $"{TargetEntity.PublicName} healed {Healing} damage!";
        }
    }
}