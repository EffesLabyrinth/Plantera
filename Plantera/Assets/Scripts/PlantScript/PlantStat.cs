using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStat : MonoBehaviour
{
    public PlantHpBarScript hpBar;
    public int id;

    public int health;
    [HideInInspector] public float startHealth;

    public int damage;
    public bool isCovered;

    public void InitializeStat()
    {
        startHealth = health;
    }
    public void Hurt(int damage)
    {
        if (!isCovered)
        {
            startHealth -= damage;
            hpBar.UpdateBar();
            if (startHealth <= 0) Destroy(gameObject);
        }
    }
}
