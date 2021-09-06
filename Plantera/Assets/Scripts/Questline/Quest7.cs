using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest7 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 7;
        questTitle = "Teras Kelima";

        noOfObjectivePart = 2;
        objectives = new ObjectiveBase[noOfObjectivePart];

        objectives[0] = new ObjectiveGoTo(questId, 1, "Cari teras kelima", "Quest7Boss");
        objectives[1] = new ObjectiveKill(questId, 2, "Hancurkan Teras Bayang", "ShadowBoar", 1, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
        requireStartNextObjective[1] = false;
    }
}
