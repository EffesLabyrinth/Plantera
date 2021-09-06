using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarScript : MonoBehaviour
{
    public EnemyStat enemyStat;
    public RectTransform container;
    public RectTransform followBar;
    public RectTransform healthBar;
    float followHealth;
    public float followSpeed;
    void Start()
    {
        followHealth = enemyStat.curHealth;
        float ratio = (float)enemyStat.curHealth / enemyStat.health;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        followBar.sizeDelta = size;
        healthBar.sizeDelta = size;
        container.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStat.curHealth < followHealth)
        {
            followHealth -= followSpeed;
            float ratio = (float)followHealth / enemyStat.health;
            Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
            followBar.sizeDelta = size;
        }
    }
    public void UpdateBar()
    {
        if (enemyStat.curHealth<enemyStat.health) container.gameObject.SetActive(true);
        else container.gameObject.SetActive(false);
        float ratio = (float)enemyStat.curHealth / enemyStat.health;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        healthBar.sizeDelta = size;
        if (enemyStat.curHealth > followHealth) followHealth = enemyStat.curHealth;
    }
}
