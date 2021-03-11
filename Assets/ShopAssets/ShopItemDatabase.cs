﻿using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item shop database")]
public class ShopItemDatabase : ScriptableObject
{

    public ShopItem[] items;
    public ShopItem tempItem;

    public ItemCategories[] categories;

    public int ItemsCount// Bunu kullanıcakmısın ****
    {
        get { return items.Length; }
    }

    public int CategoriesCount
    {
        get { return categories.Length; }
    }
    //GetItem
    public ShopItem GetShopItem(int index,int catID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopItem shopItem = items[i];
            if (shopItem.itemCategory.id == catID && shopItem.itemID == index)
            {
                tempItem = shopItem;
            }
        }
        return tempItem;
    }

    public int GetSelectedItemsID(int catID)// Bu ne ?
    {
        return categories[catID].selectedItemID;
    }
    public void PurchaseItem(int id, int catID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopItem shopItem1 = items[i];
            if (shopItem1.itemID == id && shopItem1.itemCategory.id == catID)
            {
                items[i].isPurchased = true;
            }
        }
    }

}
