using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoam : MonoBehaviour
{
    private Animator anim;
    public Vector2 targetPos;
    public float radius;
    public float speed;
    public float minDelay;
    public float maxDelay;
    public bool hasTarget;
    public bool isRoaming;
    bool isDelay;
    float startDelayTime;
    public LayerMask borderLayer;

    private void Start()
    {
        hasTarget = false;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!hasTarget)
        {
            SetNewTargetPos();
            SetDelay();
        }
        else
        {
            if (isRoaming)
            {
                if (!isDelay)
                {
                    Move();
                    anim.SetBool("isRoaming", true);
                }
                else
                {
                    anim.SetBool("isRoaming", false);
                    if (startDelayTime > 0) startDelayTime -= Time.deltaTime;
                    else isDelay = false;
                }
                
            }
        }
        Debug.DrawLine(transform.position, new Vector3(targetPos.x, targetPos.y, transform.position.z));
    }
    public void SetNewTargetPos()
    {
        do
        {
            targetPos = new Vector2(Random.Range(transform.position.x - radius, transform.position.x + radius), Random.Range(transform.position.y - radius, transform.position.y + radius));
        } while (CheckOverlapBorder() || Vector2.Distance(new Vector2(transform.position.x, transform.position.y), targetPos) > radius);
        hasTarget = true;

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
    private void SetDelay()
    {
        isDelay = true;
        startDelayTime = Random.Range(minDelay, maxDelay);
    }
    private void Move()
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        if (transform.position.Equals(new Vector3(targetPos.x, targetPos.y, transform.position.z)))
        {
            hasTarget = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
}
