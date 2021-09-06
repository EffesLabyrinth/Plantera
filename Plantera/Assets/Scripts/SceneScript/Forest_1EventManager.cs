using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_1EventManager : MonoBehaviour
{
    public AudioClip levelClip;
    public Conversation conversation;
    
    private void Start()
    {
        GetComponent<LevelSaveState>().LoadLevelState();
        GuiManager.instance.deathPanel.gameObject.SetActive(false);
        if(DataTransferBetweenScene.dataValid == 1)
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
        SoundManager.instance.PlayMusic(levelClip);

        if(!QuestManager.instance.CheckQuestReceived(new Quest3()))
        {
            StartConversation();
        }
    }
    public void StartConversation()
    {
        GuiManager.instance.dialogBox.LoadConversation(conversation);
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
        DialogBox.instance.onConversationEnd += Quest3Accept;
    }
    public void Quest3Accept()
    {
        QuestManager.instance.AddQuest(new Quest3());
    }
}
