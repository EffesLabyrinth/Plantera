using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectiveGoTo : ObjectiveBase
{
    public string checkPointName;
    public bool reachedCheckPoint;

    public ObjectiveGoTo() { }
    public ObjectiveGoTo(int questId, int partId, string title, string checkPointName)
    {
        this.questId = questId;
        objectivePartId = partId;
        objectiveTitle = title;
        type = ObjectiveType.goTo;
        isCompleted = false;
        isPartCompleted = false;
        isFailed = false;
        this.checkPointName = checkPointName;
        reachedCheckPoint = false;
    }
    public void SetReachedCheckPoint(bool value)
    {
        if (!isCompleted)
        {
            reachedCheckPoint = value;
            Evaluate();
        }
    }
    public override void Evaluate()
    {
        if (reachedCheckPoint)
        {
            isCompleted = true;
            ObjectiveCompleted();
        }
    }
}
