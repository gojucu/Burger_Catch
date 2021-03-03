using UnityEngine;

[System.Serializable]
public struct ShopItem
{

    public int itemID;
    public int categoryID;
    public Sprite image;
    public string name;
    public int price;

    public bool isDefault;
    public bool isPurchased;

}