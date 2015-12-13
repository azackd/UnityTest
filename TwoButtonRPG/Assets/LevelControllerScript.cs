using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Enemies;
using Assets.TwoButtonRPGEngine.Event;
using UnityEngine.UI;

public class LevelControllerScript : MonoBehaviour {

    public Text BattleStartText;
    public BattleQueueScript battleQueueScript;

    [Serializable]
    public class EnemyBlueprint
    {
        public int Position;
        public BaseMonster.Monsters EnemyName;
    }

    public List<EnemyBlueprint> EnemiesToSpawn; 

    // Use this for initialization
    void Start () {

        foreach (var enemy in EnemiesToSpawn)
        {
            switch (enemy.EnemyName)
            {
                case BaseMonster.Monsters.Slime:
                    battleQueueScript.BattleQueue.Battle.Monsters.Add(MonsterSlime.CreateMonster(battleQueueScript.Battle, enemy.Position));
                    break;
                case BaseMonster.Monsters.Gem:
                    battleQueueScript.BattleQueue.Battle.Monsters.Add(MonsterGem.CreateMonster(battleQueueScript.Battle, enemy.Position));
                    break;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {

	    if (battleQueueScript != null &&
            battleQueueScript.BattleQueue != null && battleQueueScript.BattleQueue.GetEvent() != null &&
            battleQueueScript.BattleQueue.GetEvent().EventId == (int)BaseEvent.EventID.BattleStartEventId)
	    {
	        BattleStartText.gameObject.SetActive(true);
	    }
	    else
	    {
            BattleStartText.gameObject.SetActive(false);
        }
	}
}
