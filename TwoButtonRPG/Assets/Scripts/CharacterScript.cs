using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Conditions;
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

    public ParticleSystem EvasionSmokePrefab;
    private ParticleSystem _evasionSmoke;

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

	        var spriteDictionary = GameObject.FindGameObjectWithTag("CharacterSpriteDictionary");
	        SpriteDictionary = spriteDictionary.GetComponent<CharacterSpriteDictionary>();

	        SpriteRenderer.sprite =
	            SpriteDictionary.CharacterSpriteValues.First(x => x.CharacterClass == CharacterModel.CharacterClass).Sprite;
            CharacterName = CharacterModel.PublicName;

            // Customize Indicators
            TurnIndicator.gameObject.SetActive(CurrentTurn);

            SetHealthAndManaText();

            var color = SpriteRenderer.color;
            color.b = 1f;
            color.g = 1f;
            color.r = 1f;
            color.a = 1f;
            SpriteRenderer.color = color;

            // Show off Taunt
            if (CharacterModel.Conditions.FirstOrDefault(x => x.ConditionId == BaseEntityCondition.ConditionID.Taunt) !=
	            null)
	        {
	            color.r = 1f;
	            color.g = 0.1f;
	            color.b = 0.1f;

	            SpriteRenderer.color = color;
	        }


                // Show off Evasion
                if (CharacterModel.Conditions.FirstOrDefault(x => x.ConditionId == BaseEntityCondition.ConditionID.Evasion) !=
	            null)
	        {
	            if (_evasionSmoke == null)
	            {
	                _evasionSmoke = (ParticleSystem) Instantiate(EvasionSmokePrefab, gameObject.transform.FindChild("SmokeTransform").position, Quaternion.identity);
	                _evasionSmoke.transform.parent = gameObject.transform.FindChild("SmokeTransform");
	            }

	            color = SpriteRenderer.color;
	            color.b = 0.1f;
	            color.g = 0.1f;
	            color.r = 0.1f;
	            color.a = 0.5f;
	            SpriteRenderer.color = color;
	        }
	        else
	        {
	            if (_evasionSmoke != null)
	            {
	                _evasionSmoke.startLifetime = _evasionSmoke.startLifetime/2;
                    Destroy(_evasionSmoke, 1.0f);
	                _evasionSmoke = null;
	            }
	        }
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
