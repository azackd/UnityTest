using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;
using Assets.TwoButtonRPGEngine.Event;
using UnityEngine.UI;

public class BattleQueueScript : MonoBehaviour
{
    public BattleModel Battle;
    public BattleQueue BattleQueue;

    public List<CharacterScript> GameCharacters = new List<CharacterScript>();
    public List<MonsterScript> GameMonsters = new List<MonsterScript>();

    public bool BattleWon;
    public bool BattleLost;
    public bool ResolvingEvent = false;

    public Text BattleQueueText;

	// Use this for initialization
	void Awake ()
	{
	    var gameController = GameObject.FindGameObjectWithTag("GameController");

	    if (gameController == null)
	    {
	        throw new Exception("Could not find GameController");
	    }

	    var gameControllerScript = gameController.gameObject.GetComponent<GameControllerScript>();

        Battle = new BattleModel(gameControllerScript.Campaign);
        BattleQueue = new BattleQueue(Battle);

        //        Battle.Monsters.Add(MonsterSlime.CreateMonster(Battle, 0));
        //        Battle.Monsters.Add(MonsterSlime.CreateMonster(Battle, 1));
        //        Battle.Monsters.Add(MonsterSlime.CreateMonster(Battle, 2));


    }
	
	// Update is called once per frame
	void Update () {

	    if (BattleQueueText != null && BattleQueue.LastMessage != null)
	    {
	        BattleQueueText.text = BattleQueue.LastMessage;
	    }

	    if (!ResolvingEvent && !(BattleWon || BattleLost))
	    {
	        var battleEvent = BattleQueue.GetEvent();
	        if (battleEvent != null && !battleEvent.WaitingForInput)
	        {
	            ResolvingEvent = true;
	            StartCoroutine(ResolveEvent());
	        }

	    }

        // Populate the player and monster views.
	    PopulateViews();
	}

    private void PopulateViews()
    {
        for (int i = 0; i < 4; i++)
        {
            var character = Battle.Characters.FirstOrDefault(x => x.BattlePosition == i);
            if (character == null)
            {
                GameCharacters[i].gameObject.SetActive(false);
                continue;
            }

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
            var character = Battle.Monsters.FirstOrDefault(x => x.BattlePosition == i);
            if (character == null)
            {
                GameMonsters[i].gameObject.SetActive(false);
                continue;
            }

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
        var lastEvent = BattleQueue.GetEvent();
        BattleQueue.ResolveEvent();

        if (lastEvent.EventId == (int)BaseEvent.EventID.BattleWonEventId)
        {
            BattleWon = true;
        }

        if(lastEvent.EventId == (int)BaseEvent.EventID.BattleLostEventId)
        {
            BattleLost = true;
        }

        ResolvingEvent = false;
    }
}
