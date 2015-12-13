using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using Assets.TwoButtonRPGEngine.Event;
using UnityEngine.UI;

public class ActionButtonControllerScript : MonoBehaviour
{
    public BattleQueueScript BattleQueueScript;

    public List<Button> ActionButtonList;

	// Update is called once per frame
	void Update () {

	    if (BattleQueueScript != null && BattleQueueScript.Battle != null)
	    {
	        var playerCharacter = BattleQueueScript.Battle.Characters.FirstOrDefault(x => x == BattleQueueScript.Battle.CurrentTurnEntity);

	        if (playerCharacter != null)
	        {
                ActionButtonList[0].gameObject.SetActive(true);
                ActionButtonList[1].gameObject.SetActive(true);
                ActionButtonList[2].gameObject.SetActive(true);
                ActionButtonList[3].gameObject.SetActive(true);

                ActionButtonList[0].GetComponentInChildren<Text>().text = playerCharacter.Ability1().Name;
	            ActionButtonList[1].GetComponentInChildren<Text>().text = playerCharacter.Ability2().Name;
	            ActionButtonList[2].GetComponentInChildren<Text>().text = playerCharacter.Ability3().Name;
	            ActionButtonList[3].GetComponentInChildren<Text>().text = playerCharacter.Ability4().Name;
	        }
	        else
	        {
                ActionButtonList[0].gameObject.SetActive(false);
                ActionButtonList[1].gameObject.SetActive(false);
                ActionButtonList[2].gameObject.SetActive(false);
                ActionButtonList[3].gameObject.SetActive(false);
            }
	    }
	}

    public void OnActionUsed(int abilityNumber)
    {
        var playerCharacter = BattleQueueScript.Battle.Characters.FirstOrDefault(x => x == BattleQueueScript.Battle.CurrentTurnEntity);

        if (playerCharacter != null && BattleQueueScript.BattleQueue.GetEvent().WaitingForInput)
        {

            List<BaseEvent> actionList = new List<BaseEvent>();
            switch (abilityNumber)
            {
                case 1:
                    actionList = playerCharacter.Ability1().UseAbility(BattleQueueScript.Battle);
                    actionList.ForEach(x => BattleQueueScript.BattleQueue.AddEvent(x));
                    break;
                case 2:
                    actionList = playerCharacter.Ability2().UseAbility(BattleQueueScript.Battle);
                    actionList.ForEach(x => BattleQueueScript.BattleQueue.AddEvent(x));
                    break;
                case 3:
                    actionList = playerCharacter.Ability3().UseAbility(BattleQueueScript.Battle);
                    actionList.ForEach(x => BattleQueueScript.BattleQueue.AddEvent(x));
                    break;
                case 4:
                    actionList = playerCharacter.Ability4().UseAbility(BattleQueueScript.Battle);
                    actionList.ForEach(x => BattleQueueScript.BattleQueue.AddEvent(x));
                    break;
                default:
                    Debug.LogError("Ability Button not formatted properly");
                    break;
            }

            BattleQueueScript.BattleQueue.ResolveEvent();
        }
    }

}
