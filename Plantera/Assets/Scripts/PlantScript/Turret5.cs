using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret5 : MonoBehaviour, IPlantTurret
{
    public PlantStat stat;
    public float delayBtwAttack;
    float startDelayBtwAttack;
    Animator anim;
    public void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlantTurret(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
        stat.InitializeStat();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (startDelayBtwAttack > 0) startDelayBtwAttack -= Time.deltaTime;
        else
        {
            if (collision.CompareTag("Enemy"))
            {
                anim.SetTrigger("attack");
                collision.gameObject.GetComponent<EnemyStat>().Hurt(stat.damage);
                startDelayBtwAttack = delayBtwAttack;
            }
        }
    }
    public void Start()
    {
        Biome biome = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelInfo>().biome;
        if (biome == Biome.windy)
        {
            StartCoroutine(WindyDebuff());
        }
        else if (biome == Biome.snowy)
        {
            StartCoroutine(SnowyDebuff());
        }
    }
    IEnumerator WindyDebuff()
    {
        int internalCooldown = 3;
        float startInternalCooldown = internalCooldown;
        while (true)
        {
            if (startInternalCooldown > 0) startInternalCooldown -= Time.deltaTime;
            else
            {
                startInternalCooldown = internalCooldown;
                stat.Hurt(1);
            }
            yield return null;
        }
    }
    IEnumerator SnowyDebuff()
    {
        int internalCooldown = 3;
        float startInternalCooldown = internalCooldown;
        while (true)
        {
            if (startInternalCooldown > 0) startInternalCooldown -= Time.deltaTime;
            else
            {
                startInternalCooldown = internalCooldown;
                stat.Hurt(2);
            }
            yield return null;
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
