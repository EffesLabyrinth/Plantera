using UnityEngine;
using UnityEngine.SceneManagement;

public enum WarpLocation
{
    Left, Top, Right, Bottom
}
public enum Biome
{
    forest,windy,snowy,hot
}
public class LevelInfo : MonoBehaviour
{
    public Biome biome;
    public float borderL, borderT, borderR, borderB;
    public string warpLScene, warpTScene, warpRScene, warpBScene;
    public WarpScript warpLPos, warpTPos, warpRPos, warpBPos;
    
    public void Warp(WarpLocation warpLocation)
    {
        DataTransferBetweenScene.SavePlayerData();
        LevelSaveState levelSave = GetComponent<LevelSaveState>();
        levelSave.SaveLevelState();


        if (warpLocation.Equals(WarpLocation.Left))
        {
            DataTransferBetweenScene.lastWarpLocation = WarpLocation.Left;
            SceneManager.LoadScene(warpLScene);
        }
        else if (warpLocation.Equals(WarpLocation.Top))
        {
            DataTransferBetweenScene.lastWarpLocation = WarpLocation.Top;
            SceneManager.LoadScene(warpTScene);
        }
        else if (warpLocation.Equals(WarpLocation.Right))
        {
            DataTransferBetweenScene.lastWarpLocation = WarpLocation.Right;
            SceneManager.LoadScene(warpRScene);
        }
        else
        {
            DataTransferBetweenScene.lastWarpLocation = WarpLocation.Bottom;
            SceneManager.LoadScene(warpBScene);
        }
    }
    public void Arrive()
    {
        if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Left))
        {
            PlayerManager.instance.transform.position = warpRPos.arriveLocation.position;
        }
        else if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Top))
        {
            PlayerManager.instance.transform.position = warpBPos.arriveLocation.position;
        }
        else if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Right))
        {
            PlayerManager.instance.transform.position = warpLPos.arriveLocation.position;
        }
        else if (DataTransferBetweenScene.lastWarpLocation.Equals(WarpLocation.Bottom))
        {
            PlayerManager.instance.transform.position = warpTPos.arriveLocation.position;
        }
        CompanionManager.instance.transform.position = PlayerManager.instance.transform.position;
    }
}
