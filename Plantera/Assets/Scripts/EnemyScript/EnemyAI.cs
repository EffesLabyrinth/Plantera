using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyStat stat;
    public Transform target;
    public bool hasTarget;

    [HideInInspector] public Vector3 targetPos;
    float startSpeed;
    public float nextWaypointDistance;

    Path path;
    int currentWaypoint;

    public float roamRadius;
    public LayerMask borderLayer;

    public float detectRadius;
    public LayerMask targetLayer;

    [SerializeField] Seeker seeker;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CircleCollider2D solidCollider;

    void Start()
    {
        targetPos = transform.position;
        InvokeRepeating("UpdatePath", 0f, .5f);
        startSpeed = stat.currentMoveSpeed * 0.5f;
        currentWaypoint = 0;
    }
    void UpdatePath()
    {
        if (CheckForTarget()) targetPos = target.position;
        else hasTarget = false;
        if(seeker.IsDone())
            seeker.StartPath(rb.position, targetPos, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (stat.isFeared && stat.fearedTarget != null)
        {
            Vector2 dir = transform.position - stat.fearedTarget.position;
            rb.velocity = dir.normalized * startSpeed;
            solidCollider.enabled = true;
        }
        else
        {
            solidCollider.enabled = false;
            if (path == null) return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                if (!hasTarget)
                {
                    startSpeed = stat.currentMoveSpeed * 0.5f;
                    SetNewRoamTargetPos();
                }
                return;
            }
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            if (currentWaypoint < path.vectorPath.Count)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 velocity;
                if (stat.isConfused) velocity = Vector2.zero;
                else velocity = direction * startSpeed;

                rb.velocity = velocity;
            }
        }
    }
    public void SetTarget(Transform target)
    {
        hasTarget = true;
        this.target = target;
    }
    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }
    public void SetNewRoamTargetPos()
    {
        do
        {
            targetPos = new Vector2(Random.Range(transform.position.x - roamRadius, transform.position.x + roamRadius), Random.Range(transform.position.y - roamRadius, transform.position.y + roamRadius));
        } while (CheckOverlapBorder() || Vector2.Distance(new Vector2(transform.position.x, transform.position.y), targetPos) > roamRadius);

        bool CheckOverlapBorder()
        {
            if (Physics2D.OverlapCircle(targetPos, 0.1f, borderLayer) != null)
            {
                Debug.DrawLine(transform.position, new Vector3(targetPos.x, targetPos.y, transform.position.z), Color.red, 2f);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    bool CheckForTarget()
    {
        Collider2D[] targetsCol = Physics2D.OverlapCircleAll(transform.position, detectRadius, targetLayer);
        for (int i = 0; i < targetsCol.Length; i++)
        {
            if (targetsCol[i].CompareTag("Player"))
            {
                target = targetsCol[i].gameObject.transform;
                hasTarget = true;
                startSpeed = stat.currentMoveSpeed;
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStat>().Hurt(2);
        }
        if (collision.CompareTag("Turret"))
        {
            collision.GetComponent<PlantStat>().Hurt(2);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, roamRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
