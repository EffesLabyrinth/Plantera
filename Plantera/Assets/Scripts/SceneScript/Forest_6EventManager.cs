using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_6EventManager : MonoBehaviour
{
    [SerializeField] GameObject bossTrigger;
    [SerializeField] GameObject[] spikes;
    [SerializeField] BossStat boss;
    [SerializeField] GameObject bossDefeatedText;
    public GameObject coreConversation;
    public AudioClip levelClip;
    public Conversation bossDefeatedConversation;
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
        if (!PlayerManager.instance.playerStat.bossKilled[4])
        {
            spikes[1].SetActive(true);
            bossTrigger.SetActive(true);
            coreConversation.SetActive(true);
            PlayEventManager.instance.onKillEvent += Boss5;
        }
        else if(PlayerManager.instance.playerStat.bossKilled[4] && !PlayerManager.instance.playerStat.answeredLastQuestion)
        {
            spikes[0].SetActive(true);
        }
        SoundManager.instance.PlayMusic(levelClip);
    }
    public void Boss5(string target, int count)
    {
        if (target == boss.enemyName)
        {
            PlayerManager.instance.playerStat.bossKilled[4] = true;
            StartConversation();
            spikes[1].SetActive(false);

            Camera.main.GetComponent<CameraMovement>().StartNormalView();
            Instantiate(bossDefeatedText, GameObject.FindGameObjectWithTag("GUI").transform);
            PlayEventManager.instance.onKillEvent -= Boss5;
            SoundManager.instance.PlayMusic(levelClip);
        }
    }
    public void OnDestroy()
    {
        PlayEventManager.instance.onKillEvent -= Boss5;
    }
    void StartConversation()
    {
        GuiManager.instance.dialogBox.LoadConversation(bossDefeatedConversation);
        Quest8Accept();
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
    }
    void Quest8Accept()
    {
        QuestManager.instance.AddQuest(new Quest8());
    }
}
