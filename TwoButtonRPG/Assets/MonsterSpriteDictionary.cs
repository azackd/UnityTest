using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;

public class MonsterSpriteDictionary : MonoBehaviour {

    [Serializable]
    public class CharacterSpriteValue
    {
        public BaseMonster.Monsters Monster;
        public Sprite Sprite;
    }

    public List<CharacterSpriteValue> MonsterSpriteValues; 
}
