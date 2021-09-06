using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameObject fork;
    [HideInInspector] Animator anim;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void EnableFork()
    {
        fork.SetActive(true);
    }
    public void DisableFork()
    {
        fork.SetActive(false);
    }
    public void TriggerPlant()
    {
        anim.SetTrigger("playerPlant");
    }
}
