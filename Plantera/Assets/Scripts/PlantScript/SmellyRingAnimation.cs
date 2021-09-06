using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellyRingAnimation : MonoBehaviour
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
            ratio = (1f - startDuration / duration)*4f;
            transform.localScale = new Vector3(ratio, ratio, 1);
        }
        else Destroy(gameObject);

    }
}
