using System;
using UnityEngine;
using System.Collections;
using Assets.TwoButtonRPGEngine.Characters;
using Assets.TwoButtonRPGEngine.Enemies;

public class MonsterScript : MonoBehaviour {

    public BaseMonster CharacterModel;

    public bool CurrentTurn;

    public int MaxHealth;
    public int CurrentHealth;

    public SpriteRenderer SpriteRenderer;
    public TextMesh HealthTextMesh;
    public SpriteRenderer TurnIndicator;

    // Use this for initialization
    void Start () {

    }

    private void SetHealthAndManaText()
    {
        if (CharacterModel == null)
        {
            TurnIndicator.gameObject.SetActive(false);
            HealthTextMesh.gameObject.SetActive(false);
        }


        if (HealthTextMesh != null)
        {
            HealthTextMesh.text = String.Format("{0}/{1}", CurrentHealth, MaxHealth);
        }
    }

    // Update is called once per frame
    void Update () {
        if (CharacterModel != null)
        {
            MaxHealth = CharacterModel.MaxHealth;
            CurrentHealth = CharacterModel.Health;

            // Customize Indicators
            TurnIndicator.gameObject.SetActive(CurrentTurn);

            SetHealthAndManaText();
        }
    }
}
