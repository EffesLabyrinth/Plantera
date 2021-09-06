using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public static DialogBox instance { get; private set; }

    [SerializeField] GameObject nameGameobject;
    [SerializeField] GameObject textShortGameobject;
    [SerializeField] GameObject textLongGameobject;
    [SerializeField] GameObject ImageGameobject;
    [SerializeField] Text speakerName;
    [SerializeField] Text speakerDialog;
    [SerializeField] Text speakerDialogLong;
    [SerializeField] Image speakerImage;

    Conversation conversation;
    int currentConversation;

    public delegate void OnConversationEnd();
    public OnConversationEnd onConversationEnd;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetInstance()
    {
        if (instance = null) instance = this;
    }
    public void SetDialog(DialogData data,Sprite speakerImage)
    {
        if (data.speakerName != "")
        {
            if (data.speakerName == "player") speakerName.text = PlayerManager.instance.playerSaveData.playerName;
            else speakerName.text = data.speakerName;
            nameGameobject.SetActive(true);

            ImageGameobject.SetActive(true);
            if (speakerImage != null)
            {
                this.speakerImage.sprite = speakerImage;
            }
            else
            {
                this.speakerImage.sprite = null;
            }

            textShortGameobject.SetActive(true);
            textLongGameobject.SetActive(false);
            speakerDialog.text = data.speakerDialog;
        }
        else
        {
            nameGameobject.SetActive(false);
            ImageGameobject.SetActive(false);
            textShortGameobject.SetActive(false);
            textLongGameobject.SetActive(true);
            speakerDialogLong.text = data.speakerDialog;
        }
    }

    public void LoadConversation(Conversation conversation)
    {
        this.conversation = conversation;
    }

    public void StartConversationFromBeginning()
    {
        PlayerManager.instance.playerController.isMovable = false;
        PlayerManager.instance.playerController.isAttackable = false;
        currentConversation = 0;
        SetDialog(conversation.dialogs[currentConversation], conversation.GetSpeakerSprite(conversation.dialogs[currentConversation].speakerName));
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        currentConversation++;
    }
    public void GetNextDialog()
    {
        if (currentConversation < conversation.dialogs.Length)
        {
            SetDialog(conversation.dialogs[currentConversation], conversation.GetSpeakerSprite(conversation.dialogs[currentConversation].speakerName));
            currentConversation++;
        }
        else
        {
            PlayerManager.instance.playerController.isMovable = true;
            PlayerManager.instance.playerController.isAttackable = true;
            conversation = null;
            gameObject.SetActive(false);
            onConversationEnd?.Invoke();
        }
    }
}
[System.Serializable]
public class DialogData
{
    public string speakerName = "";
    [TextArea(6,10)]
    public string speakerDialog = "";

    public DialogData(string name,string dialog)
    {
        speakerName = name;
        speakerDialog = dialog;
    }
    public void SetDialogData(DialogData data)
    {
        speakerName = data.speakerName;
        speakerDialog = data.speakerDialog;
    }
}
[System.Serializable]
public class Conversation
{
    public ImageNameLink[] speakers;
    public DialogData[] dialogs;

    public Sprite GetSpeakerSprite(string name)
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            if(speakers[i].name == name)
            {
                return speakers[i].image;
            }
        }
        return null;
    }
}
[System.Serializable]
public class ImageNameLink
{
    public string name;
    public Sprite image;
}
