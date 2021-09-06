using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DispersePanelScript : MonoBehaviour
{
    public ArchiveManager archiveManager;
    public Button[] buttons;
    public GameObject panel;
    private void OnEnable()
    {
        panel.SetActive(false);

        buttons[0].interactable = archiveManager.playerSaveData.dispersedTalk[0];
        buttons[1].interactable = archiveManager.playerSaveData.dispersedTalk[1];
        buttons[2].interactable = archiveManager.playerSaveData.dispersedTalk[2];
        buttons[3].interactable = archiveManager.playerSaveData.dispersedTalk[3];
    }
    public void BackToMenu()
    {
        gameObject.SetActive(false);
    }
}
