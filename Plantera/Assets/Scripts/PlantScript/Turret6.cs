using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret6 : MonoBehaviour, IPlantTurret
{
    public PlantStat stat;
    public float delayBtwAttack;
    float startDelayBtwAttack;
    public float rad;
    public LayerMask enemyLayer;
    public float fleeDuration;
    public GameObject smellyRing;

    public void PlantTurret(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
        stat.InitializeStat();
    }
    public void Start()
    {
        Biome biome = Biome.forest;
        if(GameObject.FindGameObjectWithTag("LevelManager"))
        biome = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelInfo>().biome;
        if (biome == Biome.windy)
        {
            StartCoroutine(WindyDebuff());
        }
        else if (biome == Biome.snowy)
        {
            StartCoroutine(SnowyDebuff());
        }
        else if (biome == Biome.hot)
        {
            StartCoroutine(HotDebuff());
        }
    }
    public void Update()
    {
        if (startDelayBtwAttack > 0) startDelayBtwAttack -= Time.deltaTime;
        else
        {
            startDelayBtwAttack = delayBtwAttack;
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, rad, enemyLayer);
            for (int i = 0; i < collider.Length; i++)
            {
                if (collider[i].gameObject.GetComponent<IFlee>() != null) collider[i].gameObject.GetComponent<IFlee>().Feared(fleeDuration,transform);
            }
            Instantiate(smellyRing, transform.position, Quaternion.identity);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}
