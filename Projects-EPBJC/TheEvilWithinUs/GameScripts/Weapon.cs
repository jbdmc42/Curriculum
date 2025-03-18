using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public int weaponId;
    public WeaponType weaponType;
    public GameObject prefab;
    public string shootClip;
    public string weaponClip;
    public float damage;
    public float range;
    public float fireRate;
    public int bulletsPerShot;
    public float spread;
    public float reloadTime;
    public int currentAmmo;
    public int magazineAmmo;
    public int storedAmmo;
    public int maxAmmo;
}

public enum WeaponType { Knife, Pistol, M4A1, AK47, Shotgun, Sniper }
