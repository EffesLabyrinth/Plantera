﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlantTriggerJeremin : MonoBehaviour
{
    public Conversation conversation;
    public GameObject[] enemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartConversation();
            gameObject.SetActive(false);
        }
    }
    void StartConversation()
    {
        GuiManager.instance.dialogBox.LoadConversation(conversation);
        GuiManager.instance.dialogBox.StartConversationFromBeginning();
        if (DialogBox.instance.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in DialogBox.instance.onConversationEnd.GetInvocationList())
            {
                DialogBox.instance.onConversationEnd -= nextDel;
            }
        }
        DialogBox.instance.onConversationEnd += UnlockPlant;
    }
    void UnlockPlant()
    {
        PlayerManager.instance.playerStat.unlockedPlant[5] = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }
    }
}
