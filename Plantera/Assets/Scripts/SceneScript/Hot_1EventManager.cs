using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hot_1EventManager : MonoBehaviour
{
    [SerializeField] GameObject spike1;
    [SerializeField] GameObject spike2;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;

    public Conversation hotConversation;
    public GameObject[] enemies;
    public GameObject newPlantTrigger;
    public AudioClip levelClip;
    public GameObject GotoTriggerDesert;
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

        if (!PlayerManager.instance.playerStat.bossKilled[1])
        {
            spike1.SetActive(true);
            enemy1.SetActive(false);
        }
        if (!PlayerManager.instance.playerStat.bossKilled[2])
        {
            spike2.SetActive(true);
            enemy2.SetActive(false);
        }
        if (!PlayerManager.instance.playerStat.biomeVisited[2])
        {
            StartConversation();
        }
        if (!PlayerManager.instance.playerStat.unlockedPlant[4])
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
        PlayEventManager.instance.TriggerOnGoToEvent("Quest6Pergi");
        GuiManager.instance.dialogBox.LoadConversation(hotConversation);
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
        DialogBox.instance.onConversationEnd += VisitedHot;
    }
    void VisitedHot()
    {
        PlayerManager.instance.playerStat.biomeVisited[2] = true;
    }
}
