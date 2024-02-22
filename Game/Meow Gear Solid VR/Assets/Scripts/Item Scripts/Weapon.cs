using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]

public class Weapon : Item
{
    public GameObject prefab;
    public int magazineSize;
    public int ammoCount;
    public WeaponType weaponType;
    public WeaponIndex weaponIndex;
}

public enum WeaponTypee
{
    Melee, Pistol, Tranquilizer, Throwable, Healing
}

public enum WeaponIndex
{
    None, Pistol, Tranquilizer, Throwable, Healing
}
