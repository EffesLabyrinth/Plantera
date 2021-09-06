using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_2EventManager : MonoBehaviour
{
    [SerializeField] GameObject spikes;
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
        if (PlayerManager.instance.playerSaveData.bossKilled[0])
        {
            spikes.SetActive(false);
        }
        SoundManager.instance.PlayMusic(levelClip);
    }
}
