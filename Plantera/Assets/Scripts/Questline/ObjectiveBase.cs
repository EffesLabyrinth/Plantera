using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
[System.Serializable]
[XmlInclude(typeof(ObjectiveGather))]
[XmlInclude(typeof(ObjectiveGoTo))]
[XmlInclude(typeof(ObjectiveKill))]
public class ObjectiveBase
{
    public int questId;
    public int objectivePartId;
    public string objectiveTitle;
    public ObjectiveType type;
    public bool isCompleted;
    public bool isPartCompleted;
    public bool isFailed;
    public virtual void InitializeObjective()
    {
        //initializeObjective;
    }
    public virtual void Evaluate()
    {
        //evaluationprocess;
    }
    public virtual void ObjectiveCompleted()
    {
        
    }
}
public enum ObjectiveType
{
    kill,boss,gather,plant,goTo
}
