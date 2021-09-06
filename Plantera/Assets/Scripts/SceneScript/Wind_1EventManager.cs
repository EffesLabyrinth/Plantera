using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_1EventManager : MonoBehaviour
{
    public GameObject spikes;
    public Conversation windyConversation;
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
            if(CompanionManager.instance) CompanionManager.instance.TeleportToPlayer();
        }
        if (!PlayerManager.instance.playerStat.bossKilled[1]) spikes.SetActive(true);
        if (!PlayerManager.instance.playerStat.biomeVisited[0])
        {
            StartConversation();
        }
        if (!PlayerManager.instance.playerStat.unlockedPlant[2])
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
        PlayEventManager.instance.TriggerOnGoToEvent("Quest4Pergi");
        GuiManager.instance.dialogBox.LoadConversation(windyConversation);
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
        DialogBox.instance.onConversationEnd += VisitedWindy;
    }
    void VisitedWindy()
    {
        PlayerManager.instance.playerStat.biomeVisited[0] = true;
    }
}
