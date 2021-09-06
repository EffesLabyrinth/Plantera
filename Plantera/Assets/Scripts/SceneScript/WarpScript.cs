using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour
{
    public WarpLocation warpLocation;
    LevelInfo levelInfo;
    public Transform arriveLocation;
    public void Start()
    {
        levelInfo = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) levelInfo.Warp(warpLocation);
    }
}
