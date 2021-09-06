using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtectPanelScript : MonoBehaviour
{
    public ArchiveManager archiveManager;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] text;
    [SerializeField] GameObject panel;
    private void OnEnable()
    {
        panel.SetActive(false);

        buttons[0].interactable = archiveManager.playerSaveData.unlockedPlant[0];
        buttons[1].interactable = archiveManager.playerSaveData.unlockedPlant[1];
        buttons[2].interactable = archiveManager.playerSaveData.unlockedPlant[2];
        buttons[3].interactable = archiveManager.playerSaveData.unlockedPlant[5];
        buttons[4].interactable = archiveManager.playerSaveData.unlockedPlant[6];

        text[0].SetActive(archiveManager.playerSaveData.unlockedPlant[0]);
        text[1].SetActive(archiveManager.playerSaveData.unlockedPlant[1]);
        text[2].SetActive(archiveManager.playerSaveData.unlockedPlant[2]);
        text[3].SetActive(archiveManager.playerSaveData.unlockedPlant[5]);
        text[4].SetActive(archiveManager.playerSaveData.unlockedPlant[6]);
    }
    public void BackToMenu()
    {
        gameObject.SetActive(false);
    }
}
