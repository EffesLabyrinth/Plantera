using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyLatexAnimation : MonoBehaviour
{
    SpriteRenderer sprite;
    public float duration;
    float startDuration;
    float ratio;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDuration > 0)
        {
            startDuration -= Time.deltaTime;
            ratio = startDuration / duration;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, ratio);
        }
        else Destroy(gameObject);

    }
}
