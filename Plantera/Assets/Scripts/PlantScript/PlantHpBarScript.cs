using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHpBarScript : MonoBehaviour
{
    public PlantStat plantStat;
    public RectTransform container;
    public RectTransform followBar;
    public RectTransform healthBar;
    float followHealth;
    public float followSpeed;
    void Start()
    {
        followHealth = plantStat.startHealth;
        float ratio = (float)plantStat.startHealth / plantStat.health;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        followBar.sizeDelta = size;
        healthBar.sizeDelta = size;
        container.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (plantStat.startHealth < followHealth)
        {
            followHealth -= followSpeed;
            float ratio = (float)followHealth / plantStat.health;
            Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
            followBar.sizeDelta = size;
        }
    }
    public void UpdateBar()
    {
        if (plantStat.startHealth < plantStat.health) container.gameObject.SetActive(true);
        else container.gameObject.SetActive(false);
        float ratio = (float)plantStat.startHealth / plantStat.health;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        healthBar.sizeDelta = size;
        if (plantStat.startHealth > followHealth) followHealth = plantStat.startHealth;
    }
}
