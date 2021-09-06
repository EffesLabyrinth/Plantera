using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        if (DataTransferBetweenScene.dataValid == 1)
        {
            PlayerManager playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

            LevelInfo levelInfo = GetComponent<LevelInfo>();
            if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Left))
            {
                playerManager.transform.position = levelInfo.warpRPos.arriveLocation.position;
            }
            else if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Top))
            {
                playerManager.transform.position = levelInfo.warpBPos.arriveLocation.position;
            }
            else if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Right))
            {
                playerManager.transform.position = levelInfo.warpLPos.arriveLocation.position;
            }
            else if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Bottom))
            {
                playerManager.transform.position = levelInfo.warpTPos.arriveLocation.position;
            }
        }
        LevelSaveState levelSave = GetComponent<LevelSaveState>();
        levelSave.LoadLevelState();
    }
}
