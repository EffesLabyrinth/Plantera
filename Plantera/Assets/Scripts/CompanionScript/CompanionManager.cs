using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    public static CompanionManager instance { get; private set; }
    public CompanionAnimation compAnimation;
    public PlayerManager playerManager;

    bool isFollowingPlayer;
    public float playerRadius;
    public float farSpeed;
    public float nearSpeed;
    float speed;

    [SerializeField] LayerMask borderLayer;

    bool isWalking;
    Vector2 targetPos;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        isFollowingPlayer = true;
    }
    private void Start()
    {
        playerManager = PlayerManager.instance;
    }
    private void OnEnable()
    {
        playerManager = PlayerManager.instance;
    }
    private void Update()
    {
        if (playerManager && isFollowingPlayer)
        {
            if ((playerManager.transform.position - transform.position).magnitude > playerRadius)
            {
                targetPos = playerManager.transform.position;
                speed = farSpeed;
                isWalking = true;
                compAnimation.SetWalk(true);
            }
            else if (!isWalking)
            {
                do{
                    targetPos = new Vector2(Random.Range(playerManager.transform.position.x - playerRadius / 2, playerManager.transform.position.x + playerRadius / 2), Random.Range(playerManager.transform.position.y - playerRadius / 2, playerManager.transform.position.y + playerRadius / 2));
                } while (CheckOverlapBorder());

                speed = nearSpeed;
                isWalking = true;
                compAnimation.SetWalk(false);
            }
            if (isWalking)
            {
                if (targetPos.x < transform.position.x && compAnimation.isFacingRight)
                {
                    compAnimation.Flip();
                }
                else if (targetPos.x > transform.position.x && !compAnimation.isFacingRight)
                {
                    compAnimation.Flip();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (isFollowingPlayer && isWalking)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
            if ((Vector2)transform.position == targetPos)
            {
                isWalking = false;
            }
        }
    }
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
    void OnDrawGizmos()
    {
        if (playerManager)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerManager.transform.position, playerRadius);
        }
    }
    public void SetIsFollowigPlayer(bool value)
    {
        isFollowingPlayer = value;
    }
    public void TeleportToPlayer()
    {
        if (playerManager) transform.position = playerManager.gameObject.transform.position;
    }
}
