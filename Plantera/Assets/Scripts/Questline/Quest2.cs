using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest2 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 2;
        questTitle = "Teras Makhluk Bayang";

        noOfObjectivePart = 1;
        objectives = new ObjectiveBase[noOfObjectivePart];
        objectives[0] = new ObjectiveKill(questId, 1, "Hancurkan kesemua teras bayang", "ShadowBoar", 5, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
    }
}
