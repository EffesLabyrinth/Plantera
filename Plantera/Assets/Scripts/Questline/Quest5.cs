using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest5 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 5;
        questTitle = "Teras Ketiga - Lembah Bersalji";

        noOfObjectivePart = 4;
        objectives = new ObjectiveBase[noOfObjectivePart];

        objectives[0] = new ObjectiveGoTo(questId, 1, "Jelajah lembah bersalji", "Quest5Pergi");
        objectives[1] = new ObjectiveGoTo(questId, 2, "Cari tumbuhan yang boleh tahan cuaca sejuk", "Quest5Pokok");
        objectives[2] = new ObjectiveGoTo(questId, 3, "Cari teras ketiga", "Quest5Boss");
        objectives[3] = new ObjectiveKill(questId, 4, "Hancurkan Teras Bayang", "ShadowBoar", 1, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
        requireStartNextObjective[1] = false;
        requireStartNextObjective[2] = false;
        requireStartNextObjective[3] = false;
    }
}
