using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_4EventManager : MonoBehaviour
{
    [SerializeField] GameObject bossTrigger;
    [SerializeField] GameObject spikes;
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
        if (!PlayerManager.instance.playerStat.bossKilled[0])
        {
            bossTrigger.SetActive(true);
            coreConversation.SetActive(true);
            PlayEventManager.instance.onKillEvent += Boss1;
        }
        SoundManager.instance.PlayMusic(levelClip);
    }
    public void Boss1(string target,int count)
    {
        if (target == boss.enemyName)
        {
            PlayerManager.instance.playerStat.bossKilled[0] = true;
            spikes.SetActive(false);
            Camera.main.GetComponent<CameraMovement>().StartNormalView();
            Instantiate(bossDefeatedText, GameObject.FindGameObjectWithTag("GUI").transform);
            PlayEventManager.instance.onKillEvent -= Boss1;
            SoundManager.instance.PlayMusic(levelClip);
            StartConversation();
        }
    }
    public void OnDestroy()
    {
        PlayEventManager.instance.onKillEvent -= Boss1;
    }
    void StartConversation()
    {
        GuiManager.instance.dialogBox.LoadConversation(bossDefeatedConversation);
        Quest4Accept();
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
    }
    void Quest4Accept()
    {
        QuestManager.instance.AddQuest(new Quest4());
    }
}
