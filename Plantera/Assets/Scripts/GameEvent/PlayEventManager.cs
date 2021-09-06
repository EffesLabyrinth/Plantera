using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEventManager : MonoBehaviour
{
    public static PlayEventManager instance { get; private set; }
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

    public delegate void OnGatherEvent(string gatherName,int count);
    public OnGatherEvent onGatherEvent;

    public void TriggerOnGatherEvent(string gatherName, int count)
    {
        onGatherEvent?.Invoke(gatherName,count);
    }

    public delegate void OnGoToEvent(string checkPointName);
    public OnGoToEvent onGoToEvent;

    public void TriggerOnGoToEvent(string checkPointName)
    {
        onGoToEvent?.Invoke(checkPointName);
    }

    public delegate void OnKillEvent(string killName, int count);
    public OnKillEvent onKillEvent;

    public void TriggerOnKillEvent(string killName, int count)
    {
        onKillEvent?.Invoke(killName, count);
    }

    public void DestroyInstance()
    {
        instance = null;
        Destroy(gameObject);
    }
}
