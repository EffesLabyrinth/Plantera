using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherablePlant : MonoBehaviour,ISerializationCallbackReceiver
{
    public GatherableObject plantData;
    public SpriteRenderer plantSprite;
    public GameObject indicatorSprite;
    [HideInInspector] public float startGatherableResetTimer;
    public bool isGatherable;

    public void OnAfterDeserialize(){}
    public void OnBeforeSerialize(){if (plantData && plantData.readySprite) plantSprite.sprite = plantData.readySprite;}

    private void Awake()
    {
        if (plantData && plantData.readySprite) plantSprite.sprite = plantData.readySprite;
        isGatherable = true;
        indicatorSprite.SetActive(false);
    }
    private void Update()
    {
        if (startGatherableResetTimer > 0) startGatherableResetTimer -= Time.deltaTime;
        else if (!isGatherable)
        {
            isGatherable = true;
            plantSprite.sprite = plantData.readySprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isGatherable)
        {
            indicatorSprite.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().AddGatherableObjectInVacinity(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            indicatorSprite.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().RemoveGatherableObjectInVacinity(gameObject);
        }
    }
    public List<InventoryItem> GatherMaterial()
    {
        List<InventoryItem> inventoryItems = new List<InventoryItem>();
        if(isGatherable)
        {
            for (int i = 0; i < plantData.gatherMaterials.Length; i++)
            {
                InventoryItem item = new InventoryItem(plantData.gatherMaterials[i].material, Random.Range(plantData.gatherMaterials[i].minDrop, plantData.gatherMaterials[i].maxDrop + 1));
                inventoryItems.Add(item);
            }

            isGatherable = false;
            startGatherableResetTimer = Random.Range(plantData.gatherableResetTimerMin,plantData.gatherableResetTimerMax);
            plantSprite.sprite = plantData.waitSprite;
            indicatorSprite.SetActive(false);
            if (!PlayerManager.instance.playerStat.gatheredPlant[plantData.id])
            {
                PlayerManager.instance.playerStat.gatheredPlant[plantData.id] = true;
                GameObject card = Instantiate(Resources.Load("UIs/GatherableCard") as GameObject, GameObject.FindGameObjectWithTag("GUI").transform);
                card.transform.localPosition = new Vector3(0, 25);
                card.GetComponent<GatherableCardScript>().InitializeCard(plantData);
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0f;
            } 
        }
        return inventoryItems;
    }
}

