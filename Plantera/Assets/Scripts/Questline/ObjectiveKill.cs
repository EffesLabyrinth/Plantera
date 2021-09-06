using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectiveKill : ObjectiveBase
{
    public string killTarget;
    public int requiredCount;
    public int currentCount;

    public ObjectiveKill() { }
    public ObjectiveKill(int questId, int partId, string title, string killTarget,int requiredCount,int currentCount)
    {
        this.questId = questId;
        objectivePartId = partId;
        objectiveTitle = title;
        type = ObjectiveType.kill;
        isCompleted = false;
        isPartCompleted = false;
        isFailed = false;
        this.killTarget = killTarget;
        this.requiredCount = requiredCount;
        this.currentCount = currentCount;
    }
    public void AddCurrent(int count)
    {
        if (!isCompleted)
        {
            currentCount += count;
            if (currentCount > requiredCount) currentCount = requiredCount;
            Evaluate();
        }
    }
    public override void Evaluate()
    {
        if (currentCount >= requiredCount)
        {
            isCompleted = true;
            ObjectiveCompleted();
        }
    }
}
