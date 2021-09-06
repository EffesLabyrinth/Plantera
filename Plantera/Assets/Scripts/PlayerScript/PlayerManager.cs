using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }

    public PlayerController playerController;
    public PlayerStat playerStat;
    public PlayerInventory playerInventory;
    public PlayerAnimation playerAnimation;
    public PlayerSound playerSound;

    public PlayerSaveData playerSaveData;
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
    public void UpdatePlayerSaveData()
    {
        playerSaveData.currentScene = SceneManager.GetActiveScene().name;
        playerSaveData.position = transform.position;

        playerSaveData.health = playerStat.health;
        playerSaveData.curHealth = playerStat.curHealth;
        playerSaveData.apple = playerStat.maxApple;
        playerSaveData.curApple = playerStat.curApple;

        playerSaveData.quests = QuestManager.instance.quests;
        playerSaveData.items = playerInventory.items;

        for (int i = 0; i < playerStat.unlockedPlant.Length; i++)
        {
            playerSaveData.unlockedPlant[i] = playerStat.unlockedPlant[i];
        }
        for (int i = 0; i < playerStat.gatheredPlant.Length; i++)
        {
            playerSaveData.gatheredPlant[i] = playerStat.gatheredPlant[i];
        }
        for (int i = 0; i < playerStat.biomeVisited.Length; i++)
        {
            playerSaveData.biomeVisited[i] = playerStat.biomeVisited[i];
        }
        for (int i = 0; i < playerStat.bossKilled.Length; i++)
        {
            playerSaveData.bossKilled[i] = playerStat.bossKilled[i];
        }
        for (int i = 0; i < playerStat.dispersedTalk.Length; i++)
        {
            playerSaveData.dispersedTalk[i] = playerStat.dispersedTalk[i];
        }
        playerSaveData.score = playerStat.score;
        playerSaveData.archiveEnabled = playerStat.archiveEnabled;
    }
    public void UnloadPlayerSaveData()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = playerSaveData.position;

        playerStat.health = playerSaveData.health;
        playerStat.curHealth = playerSaveData.curHealth;
        playerStat.maxApple = playerSaveData.apple;
        playerStat.curApple = playerSaveData.curApple;
        playerStat.UpdateAll();

        playerController.isMovable = true;
        QuestManager.instance.quests = playerSaveData.quests;
        QuestManager.instance.UpdateQuestBox();
        QuestManager.instance.RemapAllObjectives();
        playerInventory.items = playerSaveData.items;

        for (int i = 0; i < playerSaveData.unlockedPlant.Length; i++)
        {
            playerStat.unlockedPlant[i] = playerSaveData.unlockedPlant[i];
        }
        for (int i = 0; i < playerSaveData.gatheredPlant.Length; i++)
        {
            playerStat.gatheredPlant[i] = playerSaveData.gatheredPlant[i];
        }
        for (int i = 0; i < playerSaveData.biomeVisited.Length; i++)
        {
            playerStat.biomeVisited[i] = playerSaveData.biomeVisited[i];
        }
        for (int i = 0; i < playerSaveData.bossKilled.Length; i++)
        {
            playerStat.bossKilled[i] = playerSaveData.bossKilled[i];
        }
        for (int i = 0; i < playerSaveData.dispersedTalk.Length; i++)
        {
            playerStat.dispersedTalk[i] = playerSaveData.dispersedTalk[i];
        }
        playerStat.score = playerSaveData.score;
        playerStat.archiveEnabled = playerSaveData.archiveEnabled;
    }
}
[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public string currentScene;
    public Vector2 position;
    public bool isFromLoad;
    public int health;
    public int curHealth;
    public int apple;
    public int curApple;
    public bool[] unlockedPlant = new bool[7];
    public bool[] gatheredPlant = new bool[7];
    public List<InventoryItem> items;
    public List<Quest> quests;

    public bool[] bossKilled = new bool[5];
    public bool[] biomeVisited = new bool[3];
    public bool[] dispersedTalk = new bool[4];
    public bool answeredLastQuestion = false;
    public int score = 0;
    public bool archiveEnabled = false;
}
