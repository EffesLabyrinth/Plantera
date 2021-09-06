using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
[System.Serializable]
[XmlInclude(typeof(Quest1))]
[XmlInclude(typeof(Quest2))]
[XmlInclude(typeof(Quest3))]
[XmlInclude(typeof(Quest4))]
[XmlInclude(typeof(Quest5))]
[XmlInclude(typeof(Quest6))]
[XmlInclude(typeof(Quest7))]
[XmlInclude(typeof(Quest8))]
public class Quest
{
    public int questId;
    public string questTitle;

    public int noOfObjectivePart;
    public int currentPart;
    public bool[] requireStartNextObjective;
    public ObjectiveBase[] objectives;

    public bool isCompleted;
    public bool hasClaimedReward;

    public Quest()
    {
        QuestInitialization();
    }

    public bool CheckIfCurrentPartCompleted()
    {
        if (currentPart > noOfObjectivePart) return false;
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].objectivePartId == currentPart && objectives[i].isCompleted == false) return false;
        }
        return true;
    }
    public void StartNextObjectivePart()
    {
        if (!isCompleted && CheckIfCurrentPartCompleted())
        {
            currentPart++;
            if (currentPart > noOfObjectivePart) isCompleted = true;
        }
    }
    public void CompleteQuest()
    {
        if (currentPart > noOfObjectivePart)
        {
            isCompleted = true;
        }
    }
    public void UpdateQuest()
    {
        if (!isCompleted)
        {
            if(!requireStartNextObjective[currentPart-1])
            StartNextObjectivePart();
        }
        if (isCompleted)
        {
            //sadasfsafas
        }
    }
    public virtual void QuestInitialization()
    {
        isCompleted = false;
        hasClaimedReward = false;
        currentPart = 1;
    }
    public virtual void QuestCompleted()
    {
        //onCompleted
    }
}
