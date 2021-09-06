using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ArchiveData", menuName = "PlantArchiveData")]
public class PlantArchiveData : ScriptableObject
{
    public string plantName;
    public Sprite plantImage;
    [TextArea(10,10)]
    public string description;
}
