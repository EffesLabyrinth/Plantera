using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour
{
    [SerializeField] Text QuestText;

    public void UpdateQuestText()
    {
        List<Quest> quests = QuestManager.instance.quests;
        string text = "Objektif semasa:\n\n";

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].isCompleted)
            {
                text += quests[i].questTitle + " (selesai)\n";
            }
        }
        for (int i = 0; i < quests.Count; i++)
        {
            if (!quests[i].isCompleted)
            {
                text += "\n" + quests[i].questTitle + "\n";
                for (int j = 0; j < quests[i].objectives.Length; j++)
                {
                    if (quests[i].objectives[j].objectivePartId <= quests[i].currentPart)
                    {
                        if (quests[i].objectives[j].type == ObjectiveType.gather)
                            text += "  " + quests[i].objectives[j].objectiveTitle + " (" + ((ObjectiveGather)quests[i].objectives[j]).amountCurrent + "/" + ((ObjectiveGather)quests[i].objectives[j]).amountRequired + ")\n";
                        else if(quests[i].objectives[j].type == ObjectiveType.goTo)
                        {
                            if(((ObjectiveGoTo)quests[i].objectives[j]).reachedCheckPoint) text += "  " + quests[i].objectives[j].objectiveTitle + " (selesai)" + "\n";
                            else text += "  " + quests[i].objectives[j].objectiveTitle + " (---)" +"\n";
                        }
                        else if (quests[i].objectives[j].type == ObjectiveType.kill)
                            text += "  " + quests[i].objectives[j].objectiveTitle + " (" + ((ObjectiveKill)quests[i].objectives[j]).currentCount + "/" + ((ObjectiveKill)quests[i].objectives[j]).requiredCount + ")\n";
                    }
                }
            }
        }
        QuestText.text = text;
    }
}
