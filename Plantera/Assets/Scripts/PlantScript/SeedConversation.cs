using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedConversation : MonoBehaviour
{
    public Conversation conversation;
    public int dispersedNo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartConversation();
            gameObject.SetActive(false);
        }
    }
    public void StartConversation()
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
        DialogBox.instance.onConversationEnd += DispersedTalk;
    }
    public void DispersedTalk()
    {
        PlayerManager.instance.playerStat.dispersedTalk[dispersedNo] = true;
    } 
}
