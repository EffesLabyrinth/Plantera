using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gatherable", menuName = "Gatherable")]
public class GatherableObject : ScriptableObject
{
    public int id;
    public string idName;
    public string gatherableName;
    public Sprite readySprite;
    public Sprite waitSprite;
    public GatherMaterial[] gatherMaterials;
    public float gatherableResetTimerMin;
    public float gatherableResetTimerMax;

    [TextArea(8,5)]
    public string description;
    public Sprite realSprite;
}
[System.Serializable]
public class GatherMaterial
{
    public Characteristic material;
    public string materialDescription;
    public int minDrop;
    public int maxDrop;
}
public enum Characteristic
{
    duri,buluhalus,getah,racun,bau,kulittebal
}

