using System;
using UnityEngine;
using System.Collections;
using Assets.TwoButtonRPGEngine.Characters;
using UnityEditor;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterScript : MonoBehaviour
{
    public BaseCharacter Character;

    public String CharacterName;
    public bool CurrentTurn;

    public int MaxHealth;
    public int CurrentHealth;

    public int MaxMana;
    public int CurrentMana;

    public TextMesh HealthTextMesh;
    public TextMesh ManaTextMesh;
    public TextMesh ManalessHealthMesh;

//	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Character != null)
	    {
            MaxHealth = Character.MaxHealth;
            CurrentHealth = Character.Health;

            CharacterName = Character.PublicName;

            SetHealthAndManaText();
        }
	}

    private void SetHealthAndManaText()
    {
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
