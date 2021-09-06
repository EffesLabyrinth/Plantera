using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_5EventManager : MonoBehaviour
{
    public GameObject newPlantTrigger2;
    public GameObject newPlantTrigger1;
    public GameObject[] enemies2;
    public GameObject[] enemies1;
    public AudioClip levelClip;
    public GameObject dispersedTalk;
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
        if (!PlayerManager.instance.playerStat.unlockedPlant[6])
        {
            newPlantTrigger2.SetActive(true);
            for (int i = 0; i < enemies2.Length; i++)
            {
                enemies2[i].SetActive(false);
            }
        }
        if (!PlayerManager.instance.playerStat.unlockedPlant[5])
        {
            newPlantTrigger1.SetActive(true);
            for (int i = 0; i < enemies1.Length; i++)
            {
                enemies1[i].SetActive(false);
            }
        }
        if (!PlayerManager.instance.playerStat.dispersedTalk[3]) dispersedTalk.SetActive(true);
        SoundManager.instance.PlayMusic(levelClip);
    }
}
