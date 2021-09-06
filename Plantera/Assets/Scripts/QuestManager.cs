using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }
    [SerializeField] QuestBox questBox;

    public List<Quest> quests;
    [SerializeField] List<ObjectiveGather> gatherObjectives;
    [SerializeField] List<ObjectiveGoTo> goToObjectives;
    [SerializeField] List<ObjectiveKill> killObjectives;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        questBox = GetComponent<QuestBox>();
        PlayEventManager.instance.onGatherEvent += ObjectiveGatherEvaluation;
        PlayEventManager.instance.onGoToEvent += ObjectiveGoToEvaluation;
        PlayEventManager.instance.onKillEvent += ObjectiveKillEvaluation;
    }
    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        for (int i = 0; i < quest.objectives.Length; i++)
        {
            if (quest.objectives[i].type == ObjectiveType.gather) gatherObjectives.Add((ObjectiveGather)quest.objectives[i]);
            else if (quest.objectives[i].type == ObjectiveType.goTo) goToObjectives.Add((ObjectiveGoTo)quest.objectives[i]);
            else if (quest.objectives[i].type == ObjectiveType.kill) killObjectives.Add((ObjectiveKill)quest.objectives[i]);
        }
        UpdateQuestBox();
    }
    public void UpdateQuestBox()
    {
        questBox.UpdateQuestText();
    }
    public void ObjectiveGatherEvaluation(string gatherName, int count)
    {
        List<int> updatedQuest = new List<int>();
        for (int i = 0; i < gatherObjectives.Count; i++)
        {
            if (gatherObjectives[i].itemRequired == gatherName && !gatherObjectives[i].isCompleted)
            {
                gatherObjectives[i].AddAmountCurrent(count);
                bool isInUpdatedList = false;
                for (int j = 0; j < updatedQuest.Count; j++)
                {
                    if (updatedQuest[j] == gatherObjectives[i].questId)
                    {
                        isInUpdatedList = true;
                        break;
                    }
                }
                if (!isInUpdatedList) updatedQuest.Add(gatherObjectives[i].questId);
            }
        }
        if (updatedQuest.Count > 0)
        {
            for (int i = 0; i < updatedQuest.Count; i++)
            {
                for (int j = 0; j < quests.Count; j++)
                {
                    if (quests[j].questId == updatedQuest[i])
                    {
                        quests[j].UpdateQuest();
                        break;
                    }
                }
            }
            UpdateQuestBox();
        }
    }
    public void ObjectiveGoToEvaluation(string checkPointName)
    {
        List<int> updatedQuest = new List<int>();
        for (int i = 0; i < goToObjectives.Count; i++)
        {
            if (goToObjectives[i].checkPointName == checkPointName && !goToObjectives[i].isCompleted)
            {
                goToObjectives[i].SetReachedCheckPoint(true);
                bool isInUpdatedList = false;
                for (int j = 0; j < updatedQuest.Count; j++)
                {
                    if (updatedQuest[j] == goToObjectives[i].questId)
                    {
                        isInUpdatedList = true;
                        break;
                    }
                }
                if (!isInUpdatedList) updatedQuest.Add(goToObjectives[i].questId);
            }
        }
        if (updatedQuest.Count > 0)
        {
            for (int i = 0; i < updatedQuest.Count; i++)
            {
                for (int j = 0; j < quests.Count; j++)
                {
                    if (quests[j].questId == updatedQuest[i])
                    {
                        quests[j].UpdateQuest();
                        break;
                    }
                }
            }
            questBox.UpdateQuestText();
        }
    }
    public void ObjectiveKillEvaluation(string killName, int count)
    {
        List<int> updatedQuest = new List<int>();
        for (int i = 0; i < killObjectives.Count; i++)
        {
            if (killObjectives[i].killTarget == killName && !killObjectives[i].isCompleted)
            {
                killObjectives[i].AddCurrent(count);
                bool isInUpdatedList = false;
                for (int j = 0; j < updatedQuest.Count; j++)
                {
                    if (updatedQuest[j] == killObjectives[i].questId)
                    {
                        isInUpdatedList = true;
                        break;
                    }
                }
                if (!isInUpdatedList) updatedQuest.Add(killObjectives[i].questId);
            }
        }
        if (updatedQuest.Count > 0)
        {
            for (int i = 0; i < updatedQuest.Count; i++)
            {
                for (int j = 0; j < quests.Count; j++)
                {
                    if (quests[j].questId == updatedQuest[i])
                    {
                        quests[j].UpdateQuest();
                        break;
                    }
                }
            }
            UpdateQuestBox();
        }
    }
    public void QuestEvaluation(string gatherName,int count)
    {
        
    }
    public void StartNextObjective(int questId)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].questId == questId)
            {
                quests[i].StartNextObjectivePart();
                UpdateQuestBox();
                return;
            }
        }
    }
    public void RemoveAllQuest()
    {
        quests = new List<Quest>();
        RemoveAllObjectives();
        UpdateQuestBox();
    }
    public void RemapAllObjectives()
    {
        RemoveAllObjectives();
        for (int i = 0; i < quests.Count; i++)
        {
            if (!quests[i].isCompleted)
            {
                for (int j = 0; j < quests[i].objectives.Length; j++)
                {
                    if (!quests[i].objectives[j].isCompleted)
                    {
                        if(quests[i].objectives[j].type == ObjectiveType.gather) gatherObjectives.Add((ObjectiveGather)quests[i].objectives[j]);
                        else if (quests[i].objectives[j].type == ObjectiveType.goTo) goToObjectives.Add((ObjectiveGoTo)quests[i].objectives[j]);
                        else if (quests[i].objectives[j].type == ObjectiveType.kill) killObjectives.Add((ObjectiveKill)quests[i].objectives[j]);
                    }
                }
            }
        }
    }
    public void RemoveAllObjectives()
    {
        gatherObjectives = new List<ObjectiveGather>();
        killObjectives = new List<ObjectiveKill>();
        goToObjectives = new List<ObjectiveGoTo>();
    }
    public bool CheckQuestReceived(Quest quest)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].questId == quest.questId) return true;
        }
        return false;
    }
}