using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    private Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private Transform sprite;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public Vector2 directionFromJoystick;
    [HideInInspector] public bool isFacingRight;
    public bool isMovable;
    public bool isAttackable;

    float attackCooldown = 0.6f;
    float startAttackCooldown;
    float attackingTime = 0.2f;
    float startAttackingTime;
    float staggerTime = 0.1f;
    float startStaggerTime;

    public float checkPlantRad;
    public LayerMask plantLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        startAttackCooldown = 0;
        startAttackingTime = 0;
        startStaggerTime = 0;
        isMovable = true;
        isAttackable = true;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector2.zero;
        
        if (startAttackCooldown > 0) startAttackCooldown -= Time.deltaTime;
        if (startStaggerTime > 0) startStaggerTime -= Time.deltaTime;
        if (startAttackingTime > 0) startAttackingTime -= Time.deltaTime;
        else
        {
            if (isMovable)
            {
                direction = directionFromJoystick;
                if (direction.magnitude == 0)
                {
                    if (Input.GetKey(KeyCode.W)) direction.y++;
                    if (Input.GetKey(KeyCode.A)) direction.x--;
                    if (Input.GetKey(KeyCode.S)) direction.y--;
                    if (Input.GetKey(KeyCode.D)) direction.x++;
                }
            }
            if (direction.magnitude != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
            if ((isFacingRight && direction.x < 0) || (!isFacingRight && direction.x > 0)) Flip();
        }
        

        /*for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPos, Color.red);
        }*/
    }
    private void FixedUpdate()
    {
        rb.velocity = direction.normalized * playerManager.playerStat.moveSpeed;
    }
    public void Flip()
    {
        sprite.localScale = new Vector2(sprite.localScale.x * -1, sprite.localScale.y);
        attackOffset.x = attackOffset.x * -1;
        isFacingRight = !isFacingRight;
    }
    public void Attack()
    {
        if (isAttackable && playerManager.playerStat.isAlive && startAttackCooldown <= 0)
        {
            anim.SetTrigger("playerAttack");
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position + attackOffset, playerManager.playerStat.attackRange, playerManager.playerStat.enemyLayer);
            List<GameObject> attackedEnemies = new List<GameObject>();
            for (int i = 0; i < enemies.Length; i++)
            {
                bool wasAttacked = false;
                for (int j = 0; j < attackedEnemies.Count; j++)
                {
                    if (enemies[i].gameObject.Equals(attackedEnemies[j]))
                    {
                        wasAttacked = true;
                        break;
                    }
                }
                if (!wasAttacked)
                {
                    enemies[i].gameObject.GetComponent<EnemyStat>().Hurt(playerManager.playerStat.attackDamage);
                    attackedEnemies.Add(enemies[i].gameObject);
                }
            }
            startAttackCooldown = attackCooldown;
            startAttackingTime = attackingTime;
        }
    }
    public void Stagger()
    {
        startStaggerTime = staggerTime;
    }
    public Collider2D[] CheckPlantCover()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, checkPlantRad, plantLayer);
        return collider;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + attackOffset, playerManager.playerStat.attackRange);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, checkPlantRad);
    }
}
