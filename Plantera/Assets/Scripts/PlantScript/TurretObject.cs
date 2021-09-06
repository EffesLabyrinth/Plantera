using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
public class TurretObject : ScriptableObject
{
    public new string name;
    public Sprite plantIco;
    public MaterialCost[] materialCosts;
    public GameObject model;

    public string description;
}
[System.Serializable]
public class MaterialCost
{
    public Characteristic material;
    public int count;
}
