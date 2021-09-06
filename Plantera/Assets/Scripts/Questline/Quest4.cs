using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest4 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 4;
        questTitle = "Teras Kedua - Pantai";

        noOfObjectivePart = 4;
        objectives = new ObjectiveBase[noOfObjectivePart];

        objectives[0] = new ObjectiveGoTo(questId, 1, "Pergi ke kawasan pantai", "Quest4Pergi");
        objectives[1] = new ObjectiveGoTo(questId, 2, "Cari tumbuhan yang dapat membantu mengalahkan bayang", "Quest4Pokok");
        objectives[2] = new ObjectiveGoTo(questId, 3, "Cari teras kedua", "Quest4Boss");
        objectives[3] = new ObjectiveKill(questId, 4, "Hancurkan Teras Bayang", "ShadowBoar", 1, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
        requireStartNextObjective[1] = false;
        requireStartNextObjective[2] = false;
        requireStartNextObjective[3] = false;
    }
}
