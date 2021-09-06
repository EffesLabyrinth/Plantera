using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHome : MonoBehaviour
{
    Animator anim;
    Transform target;
    public float homeSpeed;
    Vector2 lastTargetPos;
    bool hasTarget;

    bool isDelay;
    public float delay;
    float startDelayTime;

    public float minDistanceStopHome;

    public bool isHoming;
    bool isStopHome;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isHoming)
        {
            if (hasTarget)
            {
                if (startDelayTime > 0) startDelayTime -= Time.deltaTime;
                else if (isDelay)
                {
                    anim.SetBool("isRoaming", false);
                    SetTarget(target);
                }
                else
                {
                    Home();
                    anim.SetBool("isRoaming", true);
                }
            }
            Debug.DrawLine(transform.position, lastTargetPos);
        }
    }

    private void Home()
    {
        if (CheckMinStopDistance()||isStopHome)
        {
            isStopHome = true;
            Vector2 newPos = Vector2.MoveTowards(transform.position, lastTargetPos, homeSpeed * Time.deltaTime);
            transform.position = newPos;
            if (transform.position.Equals(lastTargetPos))
            {
                startDelayTime = delay;
                isDelay = true;
            }
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, target.position, homeSpeed * Time.deltaTime);
            transform.position = newPos;
            lastTargetPos = target.position;
        }
    }
    public void SetTarget(Transform x)
    {
        target = x;
        lastTargetPos = target.position;
        hasTarget = true;
        isStopHome = false;
        isDelay = false;
    }
    private bool CheckMinStopDistance()
    {
        if (Vector2.Distance(transform.position, target.position) <= minDistanceStopHome) return true;
        else return false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, minDistanceStopHome);
    }
    
}
