using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastQuestionTrigger : MonoBehaviour
{
    public Conversation conversation;
    public GameObject questionPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartConversation();
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
        DialogBox.instance.onConversationEnd += StartQuestion;
    }
    void StartQuestion()
    {
        PlayEventManager.instance.TriggerOnGoToEvent("Quest8Pergi");
        Instantiate(questionPanel, GameObject.FindGameObjectWithTag("GUI").transform);
        gameObject.SetActive(false);
    }
}
