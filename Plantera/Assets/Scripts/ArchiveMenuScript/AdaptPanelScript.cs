using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdaptPanelScript : MonoBehaviour
{
    public ArchiveManager archiveManager;
    public Button[] buttons;
    public GameObject panel;
    private void OnEnable()
    {
        panel.SetActive(false);

        buttons[0].interactable = archiveManager.playerSaveData.biomeVisited[0];
        buttons[1].interactable = archiveManager.playerSaveData.biomeVisited[1];
        buttons[2].interactable = archiveManager.playerSaveData.biomeVisited[2];
    }
    public void BackToMenu()
    {
        gameObject.SetActive(false);
    }
}
