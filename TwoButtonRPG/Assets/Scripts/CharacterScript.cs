using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.TwoButtonRPGEngine.Characters;
using UnityEditor;

public class CharacterScript : MonoBehaviour
{
    public BaseCharacter CharacterModel;

    public String CharacterName;
    public bool CurrentTurn;

    public int MaxHealth;
    public int CurrentHealth;

    public int MaxMana;
    public int CurrentMana;

    public SpriteRenderer SpriteRenderer;

    public TextMesh HealthTextMesh;
    public TextMesh ManaTextMesh;
    public TextMesh ManalessHealthMesh;

    public SpriteRenderer TurnIndicator;

    public CharacterSpriteDictionary SpriteDictionary;

//	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (CharacterModel != null)
	    {
            MaxHealth = CharacterModel.MaxHealth;
            CurrentHealth = CharacterModel.Health;

	        MaxMana = CharacterModel.MaxMana;
	        CurrentMana = CharacterModel.MaxMana;

	        SpriteRenderer.sprite =
	            SpriteDictionary.CharacterSpriteValues.First(x => x.CharacterClass == CharacterModel.CharacterClass).Sprite;
            CharacterName = CharacterModel.PublicName;

            // Customize Indicators
            TurnIndicator.gameObject.SetActive(CurrentTurn);

            SetHealthAndManaText();
        }
	}

    private void SetHealthAndManaText()
    {
        if (CharacterModel == null)
        {
            TurnIndicator.gameObject.SetActive(false);
            ManaTextMesh.gameObject.SetActive(false);
            HealthTextMesh.gameObject.SetActive(false);
            ManalessHealthMesh.gameObject.SetActive(false);
        }


        if (HealthTextMesh != null)
        {
            HealthTextMesh.text = String.Format("{0}/{1}", CurrentHealth, MaxHealth);
        }

        if (ManaTextMesh != null)
        {
            ManaTextMesh.text = String.Format("{0}/{1}", CurrentMana, MaxMana);
        }

        if (ManalessHealthMesh != null)
        {
            ManalessHealthMesh.text = String.Format("{0}/{1}", CurrentHealth, MaxHealth);
        }

        if (MaxMana == 0)
        {
            ManaTextMesh.gameObject.SetActive(false);
            HealthTextMesh.gameObject.SetActive(false);

            ManalessHealthMesh.gameObject.SetActive(true);
        }
        else
        {
            ManaTextMesh.gameObject.SetActive(true);
            HealthTextMesh.gameObject.SetActive(true);

            ManalessHealthMesh.gameObject.SetActive(false);
        }
    }
}
