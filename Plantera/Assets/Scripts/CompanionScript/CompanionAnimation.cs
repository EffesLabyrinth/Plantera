using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    public bool isFacingRight;
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public void SetWalk(bool value)
    {
        anim.SetBool("isWalking", value);
    }
}
