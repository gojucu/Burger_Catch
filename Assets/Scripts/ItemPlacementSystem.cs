using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacementSystem : MonoBehaviour
{
    //Her kategori için bunu ayrı yazmam gerekebilir** Plantla başla örneğe
    [SerializeField] ItemConfig currentItemConfig = null;//Plant olucak ilk**Bu şekilde çalıştırdım hepsine gerek varmı düşün

    [SerializeField] ShopItemDatabase itemDB;
    GameObject itemObject;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemDB.categories.Length; i++)
        {
            var selectedItemID = SaveSystem.GetSelectedItemIndex(i);
            foreach(ShopItem shopItem in itemDB.items)
            {
                if (shopItem.itemCategory.id == i && shopItem.itemID == selectedItemID && shopItem.itemConfig != null)
                {
                    currentItemConfig = shopItem.itemConfig;

                    PutItemInPosition(currentItemConfig);
                }
            }
        }
    }

    public void PutItemInPosition(ItemConfig itemToPlace)
    {
        currentItemConfig = itemToPlace;
        var itemPrefab = itemToPlace.GetItemPrefab();
        //Destroy(itemObject);
        itemObject = Instantiate(itemPrefab, this.transform);
        itemObject.transform.localPosition = currentItemConfig.placeTransform.localPosition;
        itemObject.transform.localRotation = currentItemConfig.placeTransform.localRotation;
    }


}
