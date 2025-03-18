using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class Consumable : ScriptableObject
{
    public string consumableName;
    public int consumableId;
    public ConsumableType consumableType;
    public GameObject prefab;
}

public enum ConsumableType { Ammo, Medkit, AmmoBox, MedkitBox }
