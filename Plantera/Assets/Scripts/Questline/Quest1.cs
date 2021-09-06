using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest1 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 1;
        questTitle = "Pertemuan Pertama";

        noOfObjectivePart = 5;
        objectives = new ObjectiveBase[noOfObjectivePart];
        objectives[0] = new ObjectiveGather(questId, 1, "Ambil penggembur di tepi rumah", "ItemFork", 1, 0);
        objectives[1] = new ObjectiveGoTo(questId, 2, "Pergi ke kebun", "Quest1Kebun");
        objectives[2] = new ObjectiveGoTo(questId, 3, "Lari dari makhluk misteri", "Quest1Lari");
        objectives[3] = new ObjectiveGather(questId, 4, "Kumpul duri dari pokok bunga mawar", "duri",16, 0);
        objectives[4] = new ObjectiveKill(questId, 5, "Kalahkan makhluk Bayang", "ShadowWolf", 2, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
        requireStartNextObjective[1] = true;
        requireStartNextObjective[2] = true;
        requireStartNextObjective[3] = true;
        requireStartNextObjective[4] = false;
    }
}
