using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpScript : MonoBehaviour
{
    [SerializeField] RectTransform container;
    [SerializeField] RectTransform followBar;
    [SerializeField] RectTransform healthBar;
    [SerializeField] Text text;
    [SerializeField] BossStat boss;

    [SerializeField] Image containerImage;

    int curHealth;
    int maxHealth;
    float followHealth;
    float followSpeed;
    bool isFadeOut;
    public float fadeSpeed;

    public void InitializeBossHp(BossStat boss)
    {
        this.boss = boss;
        boss.onBossHealthUpdate += UpdateBar;

        text.text = boss.enemyName;
        maxHealth = boss.health;
        curHealth = boss.curHealth;
        followSpeed = maxHealth / 5f;
        followHealth = boss.curHealth;
        float ratio = (float)curHealth / maxHealth;
        Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
        followBar.sizeDelta = size;
        healthBar.sizeDelta = size;
        isFadeOut = false;
    }
    public void DeInitializeBossHp()
    {
        if (boss && (boss.onBossHealthUpdate != null)) 
        {
            boss.onBossHealthUpdate -= UpdateBar;
        }
    }

    void LateUpdate()
    {
        if (curHealth < followHealth)
        {
            followHealth -= followSpeed * Time.deltaTime;
            float ratio = (float)followHealth / maxHealth;
            Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
            followBar.sizeDelta = size;
        }
        else if(followBar.sizeDelta.x <= 0 && !isFadeOut)
        {
            isFadeOut = true;
            StartCoroutine(FadeOutHpBar());
        }
    }
    IEnumerator FadeOutHpBar()
    {
        float time = 10f;
        float ratio;
        float containerOriginalAlpha = containerImage.color.a;
        float textOriginalAlpha = text.color.a;
        while (time > 0f)
        {
            time -= Time.deltaTime * fadeSpeed;
            if (time < 0) time = 0;
            ratio = time / 10f;
            containerImage.color = new Color(containerImage.color.r, containerImage.color.g, containerImage.color.b, ratio * containerOriginalAlpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, ratio * textOriginalAlpha);
            yield return null;
        }
        Destroy(gameObject);
    }
    public void UpdateBar()
    {
        if (boss)
        {
            curHealth = boss.curHealth;
            maxHealth = boss.health;
            float ratio = (float)curHealth / maxHealth;
            Vector2 size = new Vector2(container.sizeDelta.x * ratio, followBar.sizeDelta.y);
            healthBar.sizeDelta = size;
            if (curHealth > followHealth) followHealth = curHealth;
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        if (boss && (boss.onBossHealthUpdate != null))
        {
            boss.onBossHealthUpdate -= UpdateBar;
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        Destroy(gameObject);
    }
}
