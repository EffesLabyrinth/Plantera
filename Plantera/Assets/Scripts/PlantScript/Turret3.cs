using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret3 : MonoBehaviour, IPlantTurret
{
    public float confuseDuration;
    public PlantStat stat;
    Animator anim;
    GameObject targetedEnemy;
    bool isConfusing;
    public void Awake()
    {
        anim = GetComponent<Animator>();
        isConfusing = false;
    }
    public void PlantTurret(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
        stat.InitializeStat();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isConfusing && collision.CompareTag("Enemy"))
        {
            isConfusing = true;
            anim.SetTrigger("attack");
            targetedEnemy = collision.gameObject;
            StartConfusing();
        }
    }
    void StartConfusing()
    {
        if (targetedEnemy.activeSelf)
        {
            targetedEnemy.GetComponent<IConfuse>().Confused(confuseDuration);
        }
        Destroy(gameObject,confuseDuration);
    }
    public void Start()
    {
        Biome biome = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelInfo>().biome;
        if (biome == Biome.snowy)
        {
            StartCoroutine(SnowyDebuff());
        }
        else if (biome == Biome.hot)
        {
            StartCoroutine(HotDebuff());
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
    IEnumerator HotDebuff()
    {
        int internalCooldown = 3;
        float startInternalCooldown = internalCooldown;
        while (true)
        {
            if (startInternalCooldown > 0) startInternalCooldown -= Time.deltaTime;
            else
            {
                startInternalCooldown = internalCooldown;
                stat.Hurt(3);
            }
            yield return null;
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
