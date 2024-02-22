using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New_Item", menuName = "Inventory/ItemData")]

public class ItemData : ScriptableObject
{
    public WeaponType weaponType;
    public bool InInventory => inInventory;
    public string ShortName => shortName;
    public string Name => itemName;
    
    public List<string> Description => itemDesc;
    public Image Sprite => itemSprite;

    public  int MaxAmmo => maxAmmo;
    public  int CurrentAmmo => currentAmmo;

    public  int MagazineSize => magazineSize;
    public  int Magazine => magazine;

    public GameObject ItemModel => itemModel;

    [SerializeField] private string shortName;
    [SerializeField] private string itemName;
    [SerializeField] private List<string> itemDesc;
    [SerializeField] private Image itemSprite;
    
    public bool inInventory;
    public int maxAmmo;

    public int currentAmmo;
    public int magazineSize;
    public int magazine;

    public GameObject itemModel;
    
}

public enum WeaponType
{
    Melee, Pistol, Tranquilizer, Throwable, Healing, Wearable, Consumable
}
