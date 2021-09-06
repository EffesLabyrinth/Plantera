using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarScript : MonoBehaviour
{
    public RectTransform container;
    public RectTransform followBar;
    public RectTransform healthBar;

    int curHealth;
    int maxHealth;
    float followHealth;
    public float followSpeed;
    void Start()
    {
        PlayerStat playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        playerStat.onHealthUpdate += UpdateBar;

        maxHealth = playerStat.health;
        curHealth = playerStat.curHealth;
        followHealth = playerStat.curHealth;
        float ratio = (float)curHealth / maxHealth;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        followBar.sizeDelta = size;
        healthBar.sizeDelta = size;
    }

    void Update()
    {
        if (curHealth < followHealth)
        {
            followHealth -= followSpeed;
            float ratio = (float)followHealth / maxHealth;
            Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
            followBar.sizeDelta = size;
        }
    }
    public void UpdateBar(int health,int curHealth)
    {
        this.curHealth = curHealth;
        maxHealth = health;
        float ratio = (float)curHealth / health;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        healthBar.sizeDelta = size;
        if (curHealth > followHealth) followHealth = curHealth;
    }
}
