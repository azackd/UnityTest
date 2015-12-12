using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;

public class BattleQueueScript : MonoBehaviour
{
    private BattleModel Battle;
    private BattleQueue BattleQueue;

    public List<CharacterScript> GameCharacters = new List<CharacterScript>();

	// Use this for initialization
	void Start ()
	{
        Battle = new BattleModel();
        CharacterFighter fighter = CharacterFighter.CreateFighter();

        MonsterSlime slime = MonsterSlime.CreateMonster();

        Battle.Characters.Add(fighter);
        Battle.Monsters.Add(slime);

	    BattleQueue = new BattleQueue(Battle);
	}
	
	// Update is called once per frame
	void Update () {

	    for (int i = 0; i < Battle.Characters.Count; i++)
	    {
	        var character = Battle.Characters[i];
	        var gameCharacter = GameCharacters[i];

	        gameCharacter.CharacterModel = character;
	    }

	}
}
