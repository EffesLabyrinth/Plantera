using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnim : MonoBehaviour
{
    public Transform sprite;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    public bool isFacingRight;
    private void Awake()
    {
        isFacingRight = true;
    }
    private void LateUpdate()
    {
        if (isFacingRight && rb.velocity.x < 0) Flip();
        else if (!isFacingRight && rb.velocity.x > 00) Flip();
        if (rb.velocity.magnitude > 0.2) SetWalk();
        else SetIdle();

    }
    public void Flip()
    {
        sprite.localScale = new Vector2(sprite.localScale.x * -1, sprite.localScale.y);
        isFacingRight = !isFacingRight;
    }
    public void SetIdle()
    {
        anim.SetBool("isRoaming", false);
    }
    public void SetWalk()
    {
        anim.SetBool("isRoaming", true);
    }
}
