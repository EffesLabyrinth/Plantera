using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret4 : MonoBehaviour,IPlantTurret
{
    public PlantStat stat;
    Animator anim;
    public GameObject parent;

    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void PlantTurret(Vector3 pos)
    {
        if (GuiManager.instance.plantPanel.nearTurret)
        {
            parent = GuiManager.instance.plantPanel.nearTurret;
            Instantiate(this, parent.transform.position, Quaternion.identity, parent.transform);
            GuiManager.instance.plantPanel.nearTurret.GetComponent<PlantStat>().isCovered = true;
            stat.InitializeStat();
        }
    }
    
    public void Start()
    {
        Biome biome = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelInfo>().biome;
        if (biome == Biome.hot)
        {
            StartCoroutine(HotDebuff());
        }
        else if (biome == Biome.windy)
        {
            StartCoroutine(WindyDebuff());
        }
    }
    IEnumerator WindyDebuff()
    {
        int internalCooldown = 3;
        float startInternalCooldown = internalCooldown;
        while (true)
        {
            if (startInternalCooldown > 0) startInternalCooldown -= Time.deltaTime;
            else
            {
                startInternalCooldown = internalCooldown;
                stat.Hurt(1);
            }
            yield return null;
        }
    }
    IEnumerator HotDebuff()
    {
        int internalCooldown = 3;
        float startInternalCooldown = internalCooldown;
        while (true)
        {
            if (startInternalCooldown > 0) startInternalCooldown -= Time.deltaTime;
            else
            {
                startInternalCooldown = internalCooldown;
                stat.Hurt(3);
            }
            yield return null;
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        parent.GetComponent<PlantStat>().isCovered = false;
    }
}
