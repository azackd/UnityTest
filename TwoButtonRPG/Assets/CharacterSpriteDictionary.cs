using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.TwoButtonRPGEngine.Characters;

public class CharacterSpriteDictionary : MonoBehaviour {

    [Serializable]
    public class CharacterSpriteValue
    {
        public BaseCharacter.CharacterClasses CharacterClass;
        public Sprite Sprite;
    }

    public List<CharacterSpriteValue> CharacterSpriteValues; 
}
