using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_4EventManager : MonoBehaviour
{
    [SerializeField] GameObject bossTrigger;
    [SerializeField] GameObject[] spikes;
    [SerializeField] BossStat boss;
    [SerializeField] GameObject bossDefeatedText;
    public GameObject coreConversation;
    public AudioClip levelClip;
    public Conversation bossDefeatedConversation;
    public GameObject dispersionTalk;
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
            spikes[1].SetActive(true);
            bossTrigger.SetActive(true);
            coreConversation.SetActive(true);
            PlayEventManager.instance.onKillEvent += Boss2;
        }
        if (!PlayerManager.instance.playerStat.dispersedTalk[1]) dispersionTalk.SetActive(true);
        SoundManager.instance.PlayMusic(levelClip);
    }
    public void Boss2(string target, int count)
    {
        if (target == boss.enemyName)
        {
            PlayerManager.instance.playerStat.bossKilled[1] = true;
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].SetActive(false);
            }
            Camera.main.GetComponent<CameraMovement>().StartNormalView();
            Instantiate(bossDefeatedText, GameObject.FindGameObjectWithTag("GUI").transform);
            PlayEventManager.instance.onKillEvent -= Boss2;
            SoundManager.instance.PlayMusic(levelClip);
            StartConversation();
        }
    }
    public void OnDestroy()
    {
        PlayEventManager.instance.onKillEvent -= Boss2;
    }
    void StartConversation()
    {
        GuiManager.instance.dialogBox.LoadConversation(bossDefeatedConversation);
        Quest5Accept();
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
    }
    void Quest5Accept()
    {
        QuestManager.instance.AddQuest(new Quest5());
    }
}