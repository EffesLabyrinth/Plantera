using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Logic : MonoBehaviour
{
    [SerializeField]Transform sprites;
    [SerializeField]BossStat stat;
    Transform target;
    public float idleDuration;
    float startIdleDuration;
    public bool isCharging;
    [SerializeField] Animator anim;
    Rigidbody2D rb;
    bool isFacingRight;
    Vector2 dir;
    [SerializeField] GameObject particleAttack;
    [SerializeField] Transform attackPoint;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        startIdleDuration = idleDuration;
        anim.SetBool("isCharging", false);
        isFacingRight = true;
        isCharging = false;
    }
    private void Update()
    {
        if (target != null)
        {
            if (isCharging) return;
            else if (startIdleDuration > 0) startIdleDuration -= Time.deltaTime;
            else if (startIdleDuration <= 0)
            {
                isCharging = true;
                anim.SetBool("isCharging", true);
            }
            
        }
    }
    private void FixedUpdate()
    {
        if (isCharging && target != null) 
        {
            dir = (target.position - transform.position).normalized;
            if (dir.x < 0f && isFacingRight) Flip();
            else if (dir.x > 0f && !isFacingRight) Flip();
            rb.velocity = dir * stat.currentMoveSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCharging && collision.CompareTag("Player"))
        {
            particleAttack.SetActive(false);
            particleAttack.transform.position = collision.ClosestPoint(attackPoint.position);
            particleAttack.SetActive(true);
            collision.GetComponent<PlayerStat>().Hurt(stat.damage);
            anim.SetBool("isCharging", false);
            startIdleDuration = idleDuration;
            rb.velocity = Vector2.zero;
            isCharging = false;   
        }
        else if (collision.CompareTag("Turret") && isCharging)
        {
            particleAttack.SetActive(false);
            particleAttack.transform.position = collision.ClosestPoint(attackPoint.position);
            particleAttack.SetActive(true);
            collision.GetComponent<PlantStat>().Hurt(stat.damage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isCharging && collision.CompareTag("Player"))
        {
            particleAttack.transform.position = collision.ClosestPoint(attackPoint.position);
            particleAttack.SetActive(true);
            collision.GetComponent<PlayerStat>().Hurt(stat.damage);
            anim.SetBool("isCharging", false);
            startIdleDuration = idleDuration;
            rb.velocity = Vector2.zero;
            isCharging = false;
        }
    }
    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }
}
