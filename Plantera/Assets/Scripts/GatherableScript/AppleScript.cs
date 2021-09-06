using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] float speed;
    [SerializeField] Transform sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerStat>().AddApple(1)) gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(AnimateApple());
    }
    IEnumerator AnimateApple()
    {
        float height = Random.Range(minHeight, maxHeight);
        while (height > 0.315f)
        {
            sprite.transform.localPosition = new Vector2(0, height);
            height -= Time.deltaTime * speed;
            yield return null;
        }
        sprite.transform.localPosition = new Vector2(0, 0.315f);
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
