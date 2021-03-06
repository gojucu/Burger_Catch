using UnityEngine;

[CreateAssetMenu(menuName = ("Shopping/Shop Item"))]
[System.Serializable]
public class ShopItem : ScriptableObject
{

    public int itemID;
    //public int categoryID;
    public ItemCatStructs itemCategory;
    public Sprite image;
    public string name;
    public int price;
    public ItemConfig itemConfig;

    public bool isPurchased;

}
//public struct ShopItem
//{

//    public int itemID;
//    public int categoryID;
//    public Sprite image;
//    public string name;
//    public int price;
//    public ItemConfig itemConfig;

//    //public bool isDefault;
//    public bool isPurchased;
//}