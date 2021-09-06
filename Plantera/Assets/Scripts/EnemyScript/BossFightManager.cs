using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    [SerializeField] GameObject hpBar;
    GameObject temp;
    BossStat stat;
    private void OnEnable()
    {
        temp = Instantiate(hpBar,GameObject.FindGameObjectWithTag("GUI").transform);
        stat = GetComponent<BossStat>();
        temp.GetComponent<BossHpScript>().InitializeBossHp(stat);
    }
    private void OnDisable()
    {
        if (temp)
        {
            temp.GetComponent<BossHpScript>().DeInitializeBossHp();
        }
    }
}
