using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : EnemyStat
{
    public delegate void OnBossHealthUpdate();
    public OnBossHealthUpdate onBossHealthUpdate;
    float immuneTime = 0.1f;
    float startImmuneTime;

    public override void Hurt(int x)
    {
        if (isAlive && startImmuneTime<=0)
        {
            //startImmuneTime = immuneTime;
            curHealth -= x;
            if (curHealth <= 0)
            {
                curHealth = 0;
                PlayEventManager.instance.TriggerOnKillEvent(enemyName, 1);
                isAlive = false;
                onBossHealthUpdate?.Invoke();
                gameObject.SetActive(false);
                return;
            }
            onBossHealthUpdate?.Invoke();
        }
    }
    private void Update()
    {
        //if (startImmuneTime > 0) startImmuneTime -= Time.deltaTime;
    }
}
