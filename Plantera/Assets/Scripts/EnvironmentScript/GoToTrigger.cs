using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTrigger : MonoBehaviour
{
    public string triggererTag;
    public string checkPointName;
    public int questId;
    public int questPartId;
    public int actionNo;
    BaseProgressionEvent levelProgression;

    public void Start()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<BaseProgressionEvent>()!=null) levelProgression = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<BaseProgressionEvent>();
    }

    public void SetGoToTrigger(string triggererTag,string checkPointName)
    {
        this.triggererTag = triggererTag;
        this.checkPointName = checkPointName;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggererTag))
        {
            for (int i = 0; i < QuestManager.instance.quests.Count; i++)
            {
                if (QuestManager.instance.quests[i].questId == questId && QuestManager.instance.quests[i].currentPart == questPartId)
                {
                    PlayEventManager.instance.TriggerOnGoToEvent(checkPointName);
                    if (levelProgression != null) levelProgression.ActionForTheProgression(questId, questPartId, actionNo);
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}
