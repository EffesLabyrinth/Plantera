using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest3 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 3;
        questTitle = "Teras Pertama - Hutan";

        noOfObjectivePart = 3;
        objectives = new ObjectiveBase[noOfObjectivePart];
        objectives[0] = new ObjectiveGoTo(questId, 1, "Cari tumbuhan yang dapat membantu mengalahkan bayang", "Quest3Pokok");
        objectives[1] = new ObjectiveGoTo(questId, 2, "Cari teras pertama", "Quest3Boss");
        objectives[2] = new ObjectiveKill(questId, 3, "Hancurkan Teras Bayang", "ShadowBoar", 1, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
        requireStartNextObjective[1] = false;
        requireStartNextObjective[2] = false;
    }
}
