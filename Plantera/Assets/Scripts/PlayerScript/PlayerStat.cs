using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PlayerManager player;

    public bool isAlive;

    public int health;
    public int curHealth;

    public int maxApple;
    public int curApple;
    public int appleHealValue;

    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayer;

    public float moveSpeed;

    public float IFrame;
    float startIFrame;

    public bool[] unlockedPlant = new bool[7];
    public bool[] gatheredPlant = new bool[7];
    public bool[] biomeVisited = new bool[3];
    public bool[] bossKilled = new bool[5];
    public bool[] dispersedTalk = new bool[4];

    public bool answeredLastQuestion;
    public int score;

    public bool archiveEnabled;

    public delegate void OnHealthUpdate(int health, int curHealth);
    public OnHealthUpdate onHealthUpdate;

    public delegate void OnAppleUpdate(int maxApple, int curApple);
    public OnAppleUpdate onAppleUpdate;

    public void Start()
    {
        isAlive = true;
    }
    private void Update()
    {
        if (startIFrame > 0f) startIFrame -= Time.deltaTime;
    }
    public void Hurt(int damage)
    {
        if (startIFrame<=0)
        {
            curHealth -= damage;
            if (curHealth <= 0)
            {
                isAlive = false;
                curHealth = 0;
                onHealthUpdate?.Invoke(health, curHealth);
                GuiManager.instance.deathPanel.SetDeathPanelActive();
                gameObject.SetActive(false);
                return;
            }
            player.playerSound.PlayHurt();
            player.playerController.anim.SetTrigger("playerHurt");
            onHealthUpdate?.Invoke(health,curHealth);
            startIFrame = IFrame;
        }
    }
    public bool AddApple(int noOfApple)
    {
        if (curApple + noOfApple <= maxApple)
        {
            curApple += noOfApple;
            onAppleUpdate?.Invoke(maxApple, curApple);
            return true;
        }
        else return false;
    }
    public int GetCurApple()
    {
        return curApple;
    }
    public bool EatApple()
    {
        if (curApple - 1 >= 0)
        {
            if ((curHealth < health) && isAlive)
            {
                curApple--;
                curHealth += appleHealValue;
                if (curHealth > health) curHealth = health;
                onHealthUpdate?.Invoke(health, curHealth);
                onAppleUpdate?.Invoke(maxApple, curApple);
                return true;
            }
            else return false;
        }
        else return false;
    }
    public void UpdateAll()
    {
        onHealthUpdate?.Invoke(health, curHealth);
        onAppleUpdate?.Invoke(maxApple, curApple);
    }
    public void ClearDelegate()
    {
        if (onHealthUpdate != null) onHealthUpdate = null;
        Debug.Log(onHealthUpdate);
        if (onAppleUpdate != null) onAppleUpdate = null;
    }
}
