using System;
using UnityEngine;
using System.Collections;
using Assets.TwoButtonRPGEngine.Battle_Queue;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public CampaignModel Campaign { get; set; }


    private LevelControllerScript _levelController;

    private bool _loadingLevel;
    public int CurrentLevel;
    public String[] LevelNames;

    void Awake()
    {
        Campaign = new CampaignModel();
    }

	// Use this for initialization
	void Start ()
	{
	    DontDestroyOnLoad(this);

        var levelControllerObject = GameObject.FindGameObjectWithTag("LevelController");
        _levelController = levelControllerObject.GetComponent<LevelControllerScript>();
        _loadingLevel = false;
    }
	
	// Update is called once per frame
	void Update () {

	    if (!_loadingLevel && CurrentLevel != LevelNames.Length - 1 && _levelController.battleQueueScript.BattleWon)
	    {
            _loadingLevel = true;
	        StartCoroutine(LoadLevelAfterPause());
	    }
	}

    void OnLevelWasLoaded(int level)
    {
        // Reset the Battle Timers
        Campaign.Characters.ForEach(x => x.CurrentTimer = 0);
        Campaign.Characters.ForEach(x => x.SpeedModifier = 0);

        var levelControllerObject = GameObject.FindGameObjectWithTag("LevelController");
        _levelController = levelControllerObject.GetComponent<LevelControllerScript>();
        _loadingLevel = false;
    }

    IEnumerator LoadLevelAfterPause()
    {
        yield return new WaitForSeconds(2f);

        CurrentLevel++;
        Application.LoadLevel(LevelNames[CurrentLevel]);
    }

}
