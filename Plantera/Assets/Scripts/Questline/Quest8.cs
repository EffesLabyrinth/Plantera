using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Quest8 : Quest
{
    public override void QuestInitialization()
    {
        base.QuestInitialization();
        questId = 8;
        questTitle = "Soal Leafa";

        noOfObjectivePart = 1;
        objectives = new ObjectiveBase[noOfObjectivePart];

        objectives[0] = new ObjectiveGoTo(questId, 1, "Jawab soalan yang diberikan Leafa", "Quest8Pergi");

        requireStartNextObjective = new bool[noOfObjectivePart];
        requireStartNextObjective[0] = false;
    }
}
