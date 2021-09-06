using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_1EventManager : MonoBehaviour
{
    public BaseProgressionEvent[] events;
    public AudioClip levelClip;
    private void Start()
    {
        GuiManager.instance.deathPanel.gameObject.SetActive(false);
        PlayerManager.instance.playerSaveData.isFromLoad = false;
        PlayerManager.instance.playerController.isMovable = false;
        events[0].enabled = true;
        SoundManager.instance.PlayMusic(levelClip);
    }
}
