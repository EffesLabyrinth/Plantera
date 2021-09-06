using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransferBetweenScene : MonoBehaviour
{
    //TestInGame
    public static int dataValid;

    //PlayerLocationBefore
    public static WarpLocation lastWarpLocation;

    public static void ClearAllData()
    {
        dataValid = 0;
    }
    public static void SavePlayerData()
    {
        dataValid = 1;
    }
}
