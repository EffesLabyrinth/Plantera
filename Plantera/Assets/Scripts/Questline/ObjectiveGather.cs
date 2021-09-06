using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectiveGather : ObjectiveBase
{
    public string itemRequired;
    public int amountRequired;
    public int amountCurrent;

    public ObjectiveGather() { }
    public ObjectiveGather(string title,string itemRequired,int amountRequired,int amountCurrent)
    {
        objectiveTitle = title;
        type = ObjectiveType.gather;
        isCompleted = false;
        isPartCompleted = false;
        isFailed = false;
        this.itemRequired = itemRequired;
        this.amountRequired = amountRequired;
        this.amountCurrent = amountCurrent;
    }
    public ObjectiveGather(int questId,int partId,string title, string itemRequired, int amountRequired, int amountCurrent)
    {
        this.questId = questId;
        objectivePartId = partId;
        objectiveTitle = title;
        type = ObjectiveType.gather;
        isCompleted = false;
        isPartCompleted = false;
        isFailed = false;
        this.itemRequired = itemRequired;
        this.amountRequired = amountRequired;
        this.amountCurrent = amountCurrent;
    }
    public void AddAmountCurrent(int count)
    {
        if (!isCompleted)
        {
            amountCurrent += count;
            if (amountCurrent > amountRequired) amountCurrent = amountRequired;
            Evaluate();
        }
    }
    public override void Evaluate()
    {
        if(amountCurrent >= amountRequired)
        {
            isCompleted = true;
            ObjectiveCompleted();
        }
    }
}
