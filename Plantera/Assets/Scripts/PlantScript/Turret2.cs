using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour, IPlantTurret
{
    public float movementSlowMultiplier;
    public float movementSlowDuration;
    public PlantStat stat;
    public bool isCharging;
    [SerializeField] float rad;
    [SerializeField] LayerMask enemyLayer;
    Animator anim;
    float delay;
    [SerializeField] GameObject stickyLatex;
    public void Awake()
    {
        anim = GetComponent<Animator>();
        isCharging = false;
        delay = 0.5f;
    }
    public void PlantTurret(Vector3 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
        stat.InitializeStat();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isCharging && collision.CompareTag("Enemy"))
        {
            isCharging = true;
            anim.SetTrigger("attack");
            StartCoroutine(StartCharging());
        }
    }
    IEnumerator StartCharging()
    {
        delay += Time.deltaTime;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, rad, enemyLayer);
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].gameObject.GetComponent<IPoison>() != null) collider[i].gameObject.GetComponent<ISlow>().SlowMovement(movementSlowMultiplier, movementSlowDuration);
        }
        Instantiate(stickyLatex, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
        else if (biome == Biome.hot)
        {
            StartCoroutine(HotDebuff());
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
