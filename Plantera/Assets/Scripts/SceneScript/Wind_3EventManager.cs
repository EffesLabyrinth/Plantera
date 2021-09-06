using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_3EventManager : MonoBehaviour
{
    public AudioClip levelClip;
    public GameObject dispersedTalk;
    public GameObject cannotGoDesert;
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
        if (!PlayerManager.instance.playerStat.dispersedTalk[2] && PlayerManager.instance.playerStat.bossKilled[1]) dispersedTalk.SetActive(true);
        if (PlayerManager.instance.playerStat.bossKilled[2]) cannotGoDesert.SetActive(false);
        SoundManager.instance.PlayMusic(levelClip);
    }
}
