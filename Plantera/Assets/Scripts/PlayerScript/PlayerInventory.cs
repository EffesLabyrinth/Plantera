using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    public List<InventoryItem> items = new List<InventoryItem>();
    public List<GameObject> gatherableObjects = new List<GameObject>();

    public void AddGatherableObjectInVacinity(GameObject gatherableObject)
    {
        for (int i = 0; i < gatherableObjects.Count; i++)
        {
            if (gatherableObjects[i] == gatherableObject) return;
        }
        gatherableObjects.Add(gatherableObject);
    }
    public void RemoveGatherableObjectInVacinity(GameObject gatherableObject)
    {
        for (int i = 0; i < gatherableObjects.Count; i++)
        {
            if (gatherableObjects[i] == gatherableObject)
            {
                gatherableObjects.Remove(gatherableObject);
                return;
            } 
        }
    }

    public void AddItem(InventoryItem item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].material == item.material)
            {
                items[i].count += item.count;
                return;
            }
        }
        items.Add(item);
    }

    public void GatherMaterialFromGatherableMaterial()
    {
        for (int i = 0; i < gatherableObjects.Count; i++)
        {
            List<InventoryItem> itemsFromGatherable = gatherableObjects[i].GetComponent<GatherablePlant>().GatherMaterial();
            for (int j = 0; j < itemsFromGatherable.Count; j++)
            {
                AddItem(itemsFromGatherable[j]);
                PlayEventManager.instance.TriggerOnGatherEvent(itemsFromGatherable[j].material.ToString(), itemsFromGatherable[j].count);
            }
        }
    }

    public void RemoveItem(Characteristic material,int count)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].material == material)
            {
                items[i].count -= count;
                if(items[i].count <= 0) items.RemoveAt(i);
                return;
            }
        }
    }

    public bool CheckItemIsInInventory(Characteristic material, int count)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].material == material && items[i].count >= count) return true;
        }
        return false;
    }
    public int SearchForItem(Characteristic material)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].material == material) return items[i].count;
        }
        return 0;
    }
}
[System.Serializable]
public class InventoryItem
{
    public Characteristic material;
    public int count;

    public InventoryItem() { }
    public InventoryItem(Characteristic material,int count)
    {
        this.material = material;
        this.count = count;
    }
}
