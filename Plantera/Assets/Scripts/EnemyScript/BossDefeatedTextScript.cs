using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDefeatedTextScript : MonoBehaviour
{
    Text text;
    float fadeSpeed;
    private void Start()
    {
        fadeSpeed = 10f;
        text = GetComponent<Text>();
        StartCoroutine(AnimationForText());
    }
    IEnumerator AnimationForText()
    {
        float time = 10f;
        float ratio;
        float textOriginalAlpha = text.color.a;
        while (time > 0f)
        {
            time -= Time.deltaTime * fadeSpeed;
            if (time < 0) time = 0;
            ratio = time / 10f;
            text.color = new Color(1f - ratio * 1f, 1f - ratio * 1f, 1f - ratio * 1f, textOriginalAlpha);
            yield return null;
        }
        time = 10f;
        while (time > 0f)
        {
            time -= Time.deltaTime * fadeSpeed;
            if (time < 0) time = 0;
            ratio = time / 10f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, ratio * textOriginalAlpha);
            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
