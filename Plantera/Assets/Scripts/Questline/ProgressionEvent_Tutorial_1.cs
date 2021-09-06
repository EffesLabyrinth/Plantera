using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ProgressionEvent_Tutorial_1 : BaseProgressionEvent
{
    //player
    [SerializeField] int initialHealth;
    [SerializeField] int initialMaxApple;
    //item-fork
    [SerializeField] GameObject fork;
    //gui
    DialogBox dialogBox;
    //manager
    GuiManager guiManager;
    ControllerManager controllerManager;
    //Conversation1 - player talk
    public Conversation conversation1;
    //Conversation2 - introduction to player movement
    public Conversation conversation2;
    //Conversation3 - objective
    public Conversation conversation3;
    //Conversation4 - fork
    public Conversation conversation4;
    //Conversation5 - monsterAppear
    public Conversation conversation5;
    //Conversation6 - Nakama
    public Conversation conversation6;
    //Conversation7 - gather
    public Conversation conversation7;
    //Conversation8 - special characteristics explanation
    public Conversation conversation8;
    //Conversation9 - plant
    public Conversation conversation9;
    //Conversation10 - explaining the world
    public Conversation conversation10;
    //Conversation11 - explaining the world
    public Conversation conversation11;
    //Conversation12 - apal
    public Conversation conversation12;
    public GameObject[] enemies;
    public GameObject[] enemies2;
    public GameObject[] spikes;
    public GameObject[] cannotGoThereYet1;
    public GameObject companion;
    int thornCount = 0;
    public int enemyKillCount = 0;

    public GameObject flashScreen;
    void OnEnable()
    {
        if (QuestManager.instance != null) QuestManager.instance.RemoveAllQuest();
        if (CompanionManager.instance)
        {
            CompanionManager.instance.transform.position = new Vector2(19.84f, -15.558f);
            CompanionManager.instance.transform.localScale = new Vector3(-0.5912374f, 0.5912374f, 0.5912374f);
            CompanionManager.instance.compAnimation.isFacingRight = false;
            CompanionManager.instance.gameObject.SetActive(false);
        }

        PlayerManager.instance.gameObject.transform.position = new Vector2(18.81f, 0.93f);
        PlayerManager.instance.gameObject.transform.localScale = new Vector3(1, 1, 1);

        PlayerManager.instance.playerAnimation.DisableFork();
        PlayerManager.instance.playerInventory.items = new List<InventoryItem>();

        PlayerStat playerStat = PlayerManager.instance.playerStat;
        playerStat.health = initialHealth;
        playerStat.curHealth = initialHealth;
        playerStat.maxApple = initialMaxApple;
        playerStat.curApple = initialMaxApple;
        GuiManager.instance.hpBar.UpdateBar(initialHealth,initialHealth);
        for (int i = 0; i < playerStat.unlockedPlant.Length; i++) playerStat.unlockedPlant[i] = false;

        controllerManager = GameObject.FindGameObjectWithTag("ControllerManager").GetComponent<ControllerManager>();
        controllerManager.joystick.gameObject.SetActive(false);
        controllerManager.buttonPlant.gameObject.SetActive(false);
        controllerManager.buttonApple.gameObject.SetActive(false);
        controllerManager.buttonGather.gameObject.SetActive(false);
        controllerManager.buttonChangeMode.gameObject.SetActive(false);

        fork.GetComponent<ItemFork>().SetIteractable(false);

        guiManager = GameObject.FindGameObjectWithTag("GUI").GetComponent<GuiManager>();
        dialogBox = guiManager.dialogBox;
        dialogBox.gameObject.SetActive(false);
        guiManager.questBox.gameObject.SetActive(false);

        enemyKillCount = 0;

        Invoke("StartConversation1", 1f);
    }
    void StartConversation1()
    {
        dialogBox.LoadConversation(conversation1);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartConversation2;
    }
    void StartConversation2()
    {
        controllerManager.joystick.gameObject.SetActive(true);
        dialogBox.LoadConversation(conversation2);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartConversation3;
    }
    void StartConversation3()
    {
        guiManager.questBox.gameObject.SetActive(true);

        dialogBox.LoadConversation(conversation3);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += EnableInteractionFork;
    }
    void EnableInteractionFork()
    {
        QuestManager.instance.AddQuest(new Quest1());
        fork.GetComponent<ItemFork>().SetIteractable(true);

        controllerManager.buttonPlant.gameObject.SetActive(true);
        controllerManager.buttonPlant.SetIsEnabledPlantAttack(false);
        dialogBox.LoadConversation(conversation4);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
    }
    void StartConversation5()
    {
        dialogBox.LoadConversation(conversation5);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartEnemyAttack;
    }
    public void StartEnemyAttack()
    {
        QuestManager.instance.StartNextObjective(1);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true);
        }
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].SetActive(true);
        }
        for (int i = 0; i < cannotGoThereYet1.Length; i++)
        {
            cannotGoThereYet1[i].SetActive(false);
        }
    }
    void StartConversation6()
    {
        dialogBox.LoadConversation(conversation6);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartConversation7;
    }
    void StartConversation7()
    {
        QuestManager.instance.StartNextObjective(1);
        PlayEventManager.instance.onGatherEvent += GatherThornObjective;
        controllerManager.buttonGather.gameObject.SetActive(true);
        dialogBox.LoadConversation(conversation7);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
    }
    public void GatherThornObjective(string gatherName, int count)
    {
        if(gatherName == Characteristic.duri.ToString())
        {
            thornCount += count;
            if (thornCount >= 16)
            {
                PlayEventManager.instance.onGatherEvent -= GatherThornObjective;
                StartConversation8();
            }
        }
    }
    void StartConversation8()
    {
        dialogBox.LoadConversation(conversation8);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartConversation9;
    }
    void StartConversation9()
    {
        controllerManager.buttonChangeMode.gameObject.SetActive(true);
        PlayerManager.instance.playerStat.unlockedPlant[0] = true;
        CompanionManager.instance.SetIsFollowigPlayer(true);
        QuestManager.instance.StartNextObjective(1);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies2[i].SetActive(true);
        }

        PlayEventManager.instance.onKillEvent += EnemyKill;

        dialogBox.LoadConversation(conversation9);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
    }
    public void EnemyKill(string name, int count)
    {
        if (name == "ShadowWolf")
        {
            enemyKillCount += count;
            if (enemyKillCount >= 2)
            {
                PlayEventManager.instance.onKillEvent -= EnemyKill;
                StartConversation10();
            }
        }
    }
    public void StartConversation10()
    {
        dialogBox.LoadConversation(conversation10);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartConversation11;
    }
    public void StartConversation11()
    {
        QuestManager.instance.AddQuest(new Quest2());
        dialogBox.LoadConversation(conversation11);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += StartConversation12;
    }
    public void StartConversation12()
    {
        controllerManager.buttonApple.gameObject.SetActive(true);
        PlayerManager.instance.playerStat.archiveEnabled = true;
        dialogBox.LoadConversation(conversation12);
        dialogBox.StartConversationFromBeginning();
        if (dialogBox.onConversationEnd != null)
        {
            foreach (DialogBox.OnConversationEnd nextDel in dialogBox.onConversationEnd.GetInvocationList())
            {
                dialogBox.onConversationEnd -= nextDel;
            }
        }
        dialogBox.onConversationEnd += LoadSceneForest;
    }
    public void LoadSceneForest()
    {
        PlayerManager.instance.UpdatePlayerSaveData();
        PlayerManager.instance.playerSaveData.currentScene = "Forest_1";
        SceneManager.LoadScene("Forest_1");
    }
    public override void ActionForTheProgression(int questId, int questPartId, int actionNo)
    {
        if (questId == 1)
        {
            if (questPartId == 2 && actionNo == 1)
            {
                StartConversation5();
            }
            else if(questPartId == 3 && actionNo == 1)
            {
                PlayerManager.instance.playerController.isMovable = false;
                PlayerManager.instance.playerController.isAttackable = false;
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].SetActive(false);
                    Invoke("StartConversation6",1.5f);
                }
                if (CompanionManager.instance)
                {
                    CompanionManager.instance.gameObject.SetActive(true);
                    CompanionManager.instance.SetIsFollowigPlayer(false);
                }
                else
                {
                    companion.SetActive(true);
                    companion.GetComponent<CompanionManager>().SetIsFollowigPlayer(false);
                }
                flashScreen.SetActive(true);
            }
        }
    }
    private void OnDestroy()
    {
        PlayEventManager.instance.onKillEvent -= EnemyKill;
    }
}
