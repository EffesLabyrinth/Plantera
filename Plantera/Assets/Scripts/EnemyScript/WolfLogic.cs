using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfLogic : MonoBehaviour
{
    public EnemyRoam enemyRoam;
    public EnemyHome enemyHome;
    public WolfAnim wolfAnim;

    public float detectRadius;
    public LayerMask targetLayer;

    public Transform target;
    public bool hasTarget;

    float startDetectTimer;

    private void Update()
    {
        if (startDetectTimer > 0) startDetectTimer -= Time.deltaTime;
        else
        {
            startDetectTimer = 0.7f;
            if (hasTarget&&target.gameObject.activeSelf)
            {
                if (!CheckDistanceBetweenTarget())
                {
                    hasTarget = false;
                    enemyHome.isHoming = false;
                    enemyRoam.isRoaming = true;
                    enemyRoam.SetNewTargetPos();
                }
            }
            else if (hasTarget && !target.gameObject.activeSelf)
            {
                hasTarget = false;
                enemyHome.isHoming = false;
                enemyRoam.isRoaming = true;
                enemyRoam.SetNewTargetPos();
            }
            else
            {
                if (CheckForTarget())
                {
                    enemyHome.isHoming = true;
                    enemyRoam.isRoaming = false;
                    enemyHome.SetTarget(target);
                }
            }
        }

        if (hasTarget)
        {
            if (target.position.x < transform.position.x && wolfAnim.isFacingRight)
            {
                wolfAnim.Flip();
            }
            else if (target.position.x > transform.position.x && !wolfAnim.isFacingRight)
            {
                wolfAnim.Flip();
            }
        }
        else if (enemyRoam.hasTarget)
        {
            if (enemyRoam.targetPos.x < transform.position.x && wolfAnim.isFacingRight)
            {
                wolfAnim.Flip();
            }
            else if (enemyRoam.targetPos.x > transform.position.x && !wolfAnim.isFacingRight)
            {
                wolfAnim.Flip();
            }
        }
    }

    bool CheckForTarget()
    {
        Collider2D[] targetsCol= Physics2D.OverlapCircleAll(transform.position, detectRadius, targetLayer);
        for (int i = 0; i < targetsCol.Length; i++)
        {
            if (targetsCol[i].CompareTag("Player"))
            {
                target = targetsCol[i].gameObject.transform;
                hasTarget = true;
                return true;
            }
        }
        return false;
    }
    bool CheckDistanceBetweenTarget()
    {
        if (Vector2.Distance(transform.position, target.position) <= detectRadius) return true;
        return false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
