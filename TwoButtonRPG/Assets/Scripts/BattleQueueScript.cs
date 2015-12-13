using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;
using UnityEngine.UI;

public class BattleQueueScript : MonoBehaviour
{
    public BattleModel Battle;
    public BattleQueue BattleQueue;

    public List<CharacterScript> GameCharacters = new List<CharacterScript>();
    public List<MonsterScript> GameMonsters = new List<MonsterScript>();

    public bool ResolvingEvent = false;

    public Text BattleQueueText;

	// Use this for initialization
	void Start ()
	{
        Battle = new BattleModel();

        Battle.Characters.Add(CharacterFighter.CreateCharacter());
        Battle.Characters.Add(CharacterCleric.CreateCharacter());
        Battle.Characters.Add(CharacterWizard.CreateCharacter());

        Battle.Monsters.Add(MonsterSlime.CreateMonster());
        Battle.Monsters.Add(MonsterSlime.CreateMonster());
        Battle.Monsters.Add(MonsterSlime.CreateMonster());

        BattleQueue = new BattleQueue(Battle);
	}
	
	// Update is called once per frame
	void Update () {

	    if (BattleQueueText != null && BattleQueue.LastMessage != null)
	    {
	        BattleQueueText.text = BattleQueue.LastMessage;
	    }

	    if (!ResolvingEvent)
	    {
	        var battleEvent = BattleQueue.GetEvent();
	        if (battleEvent != null && !battleEvent.WaitingForInput)
	        {
	            ResolvingEvent = true;
	            StartCoroutine(ResolveEvent());
	        }

	    }


	    for (int i = 0; i < 4; i++)
	    {
	        if (Battle.Characters.Count <= i)
	        {
                GameCharacters[i].gameObject.SetActive(false);
	            continue;
	        }

	        var character = Battle.Characters[i];
	        var gameCharacter = GameCharacters[i];

            gameCharacter.CharacterModel = character;
            gameCharacter.gameObject.SetActive(true);
            if (character == Battle.CurrentTurnEntity)
	        {
	            gameCharacter.CurrentTurn = true;
	        }
	        else
	        {
	            gameCharacter.CurrentTurn = false;
	        }
	    }

        for (int i = 0; i < 5; i++)
        {
            if (Battle.Monsters.Count <= i)
            {
                GameMonsters[i].gameObject.SetActive(false);
                continue;
            }

            var character = Battle.Monsters[i];
            var gameCharacter = GameMonsters[i];

            gameCharacter.gameObject.SetActive(true);
            gameCharacter.CharacterModel = character;

            if (character == Battle.CurrentTurnEntity)
            {
                gameCharacter.CurrentTurn = true;
            }
            else
            {
                gameCharacter.CurrentTurn = false;
            }


        }

    }

    public void OnStartBattleClick()
    {
        if (BattleQueue.IsEmpty())
        {
            var baseEvent = BattleQueue.GetEvent();

            Debug.Log(baseEvent);
        }
    }

    IEnumerator ResolveEvent()
    {
        yield return new WaitForSeconds(1.5f);
        BattleQueue.ResolveEvent();
        ResolvingEvent = false;
    }
}
