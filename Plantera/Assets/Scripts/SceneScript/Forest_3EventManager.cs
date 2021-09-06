using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_3EventManager : MonoBehaviour
{
    public GameObject newPlantTrigger;
    public GameObject goToTrigger;
    public GameObject[] enemies;
    public AudioClip levelClip;
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
        if (!PlayerManager.instance.playerStat.unlockedPlant[1]) 
        {
            newPlantTrigger.SetActive(true);
            goToTrigger.SetActive(true);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(false);
            }
        }
        if (PlayerManager.instance.playerStat.bossKilled[0] && !PlayerManager.instance.playerStat.dispersedTalk[0]) dispersionTalk.SetActive(true);
        SoundManager.instance.PlayMusic(levelClip);
    }

}
