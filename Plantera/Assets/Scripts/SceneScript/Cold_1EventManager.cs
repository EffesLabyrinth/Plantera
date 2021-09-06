using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cold_1EventManager : MonoBehaviour
{
    public Conversation snowyConversation;
    public GameObject[] enemies;
    public GameObject newPlantTrigger;
    public AudioClip levelClip;
    private void Start()
    {
        GetComponent<LevelSaveState>().LoadLevelState();
        GuiManager.instance.deathPanel.gameObject.SetActive(false);
        if (DataTransferBetweenScene.dataValid == 1)
        {
            GetComponent<LevelInfo>().Arrive();
            DataTransferBetweenScene.ClearAllData();
            PlayerManager.instance.UpdatePlayerSaveData();
        }
        else if (PlayerManager.instance.playerSaveData.isFromLoad)
        {
            PlayerManager.instance.playerSaveData.isFromLoad = false;
            PlayerManager.instance.UnloadPlayerSaveData();
            if (CompanionManager.instance) CompanionManager.instance.TeleportToPlayer();
        }
        if (!PlayerManager.instance.playerStat.biomeVisited[1])
        {
            StartConversation();
        }
        if (!PlayerManager.instance.playerStat.unlockedPlant[3])
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(false);
            }
            newPlantTrigger.SetActive(true);
        }
        SoundManager.instance.PlayMusic(levelClip);
    }
    void StartConversation()
    {
        PlayEventManager.instance.TriggerOnGoToEvent("Quest5Pergi");
        GuiManager.instance.dialogBox.LoadConversation(snowyConversation);
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
        DialogBox.instance.onConversationEnd += VisitedSnowy;
    }
    void VisitedSnowy()
    {
        PlayerManager.instance.playerStat.biomeVisited[1] = true;
    }
}
