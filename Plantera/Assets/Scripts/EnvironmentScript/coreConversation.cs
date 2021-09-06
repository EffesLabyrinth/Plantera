using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coreConversation : MonoBehaviour
{
    public Conversation conversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartConversation();
            gameObject.SetActive(false);
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
        }
    }
}
