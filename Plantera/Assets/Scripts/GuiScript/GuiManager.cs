using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public static GuiManager instance { get; private set; }
    public DialogBox dialogBox;
    public QuestBox questBox;
    public DeathPanelScript deathPanel;
    public PausePanelScript pausePanel;
    public characteristicsPanelScript characteristicPanel;
    public PlantPanelScript plantPanel;
    public HpBarScript hpBar;
    public AudioClip buttonClick;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
