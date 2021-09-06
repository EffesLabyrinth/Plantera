using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_7EventManager : MonoBehaviour
{
    [SerializeField] GameObject spikes;
    public GameObject lastConversationTrigger;
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
        if (!PlayerManager.instance.playerStat.bossKilled[4])
        {
            spikes.SetActive(true);
        }
        else
        {
            if (!PlayerManager.instance.playerStat.answeredLastQuestion)
            {
                lastConversationTrigger.SetActive(true);
            }
        }
        SoundManager.instance.PlayMusic(levelClip);
    }
}
