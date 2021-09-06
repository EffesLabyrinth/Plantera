using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour,ISlow,IConfuse,IPoison,IFlee
{
    Animator anim;
    public string enemyName;
    public int damage;
    public int health;
    public int curHealth;
    public float moveSpeed;
    [HideInInspector]public float currentMoveSpeed;
    protected bool isAlive;
    public EnemyHpBarScript hpBar;

    //debuff
    public bool isConfused;
    public bool isPoisoned;
    public bool isFeared;
    public Transform fearedTarget;

    public void Start()
    {
        curHealth = health;
        anim = GetComponent<Animator>();
        isAlive = true;
        currentMoveSpeed = moveSpeed;
    }
    public int Damage()
    {
        return damage;
    }
    public virtual void Hurt(int x)
    {
        curHealth -= x;
        if (curHealth <= 0 && isAlive)
        {
            isAlive = false;
            PlayEventManager.instance.TriggerOnKillEvent(enemyName, 1);
            gameObject.SetActive(false);
            return;
        }
        anim.SetTrigger("enemyHurt");
        hpBar.UpdateBar();
    }

    public void SlowMovement(float multiplier, float duration)
    {
        StopCoroutine("Slow");
        StartCoroutine(Slow(multiplier,duration));
    }
    IEnumerator Slow(float multiplier,float duration)
    {
        currentMoveSpeed = multiplier * moveSpeed;
        while (duration>0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        currentMoveSpeed = moveSpeed;
    }

    public void Confused(float duration)
    {
        StopCoroutine("ConfusedDebuff");
        StartCoroutine(ConfusedDebuff(duration));
    }
    IEnumerator ConfusedDebuff(float duration)
    {
        isConfused = true;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        isConfused = false;
    }

    public void Poisoned(float duration, int damageOverTime)
    {
        StopCoroutine("PoisonedDebuff");
        StartCoroutine(PoisonedDebuff(duration, damageOverTime));
    }
    IEnumerator PoisonedDebuff(float duration, int damageOverTime)
    {
        isPoisoned = true;
        float cooldown = 0.5f;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            if (cooldown > 0) cooldown -= Time.deltaTime;
            else
            {
                Hurt(damageOverTime);
                cooldown = 0.5f;
            }
            yield return null;
        }
        isPoisoned = false;
    }

    public void Feared(float duration, Transform target)
    {
        StopCoroutine("FearedDebuff");
        StartCoroutine(FearedDebuff(duration, target));
    }
    IEnumerator FearedDebuff(float duration,Transform target)
    {
        isFeared = true;
        fearedTarget = target;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        isFeared = false;
        fearedTarget = null;
    }
}
