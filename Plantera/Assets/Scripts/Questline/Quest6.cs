using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest6 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 6;
        questTitle = "Teras Keempat - Gurun";

        noOfObjectivePart = 4;
        objectives = new ObjectiveBase[noOfObjectivePart];

        objectives[0] = new ObjectiveGoTo(questId, 1, "Pergi ke kawasan gurun", "Quest6Pergi");
        objectives[1] = new ObjectiveGoTo(questId, 2, "Cari tumbuhan yang dapat tahan cuaca kering", "Quest6Pokok");
        objectives[2] = new ObjectiveGoTo(questId, 3, "Cari teras keempat", "Quest6Boss");
        objectives[3] = new ObjectiveKill(questId, 4, "Hancurkan Teras Bayang", "ShadowBoar", 1, 0);

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
        requireStartNextObjective[1] = false;
        requireStartNextObjective[2] = false;
        requireStartNextObjective[3] = false;
    }
}
