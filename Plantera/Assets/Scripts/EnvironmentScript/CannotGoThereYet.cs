using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotGoThereYet : MonoBehaviour
{
    public Conversation conversation;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogBox.instance.LoadConversation(conversation);
            DialogBox.instance.StartConversationFromBeginning();
        }
    }
}
