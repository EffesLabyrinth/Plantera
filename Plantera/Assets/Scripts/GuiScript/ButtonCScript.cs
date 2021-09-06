using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCScript : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager;

    [SerializeField] private Text noOfAppleText;

    private void Awake()
    {
        controllerManager.onPlayerSet += ButtonCSetup;
    }
    void ButtonCSetup()
    {
        controllerManager.playerManager.playerStat.onAppleUpdate += UpdateText;
        noOfAppleText.text = controllerManager.playerManager.playerStat.GetCurApple().ToString();
    }
    public void UpdateText(int maxApple,int currentApple)
    {
        noOfAppleText.text = currentApple.ToString();
    }
    public void ButtonPress()
    {
        controllerManager.playerManager.playerStat.EatApple();
    }
}
