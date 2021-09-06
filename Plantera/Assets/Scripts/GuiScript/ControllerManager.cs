using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public JoystickScript joystick;
    public ButtonAScript buttonPlant;
    public ButtonBScript buttonChangeMode;
    public ButtonCScript buttonApple;
    public ButtonDScript buttonGather;

    [HideInInspector] public PlayerManager playerManager;

    public delegate void OnPlayerSet();
    public OnPlayerSet onPlayerSet;

    public void SetPlayer(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        onPlayerSet?.Invoke();
    }

    private void Start()
    {
        SetPlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>());
    }
    private void OnDestroy()
    {
        if (onPlayerSet!=null)
        {
            foreach (OnPlayerSet nextDel in onPlayerSet.GetInvocationList())
            {
                onPlayerSet -= nextDel;
            }
        }
    }
}
