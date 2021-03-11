using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacementSystem : MonoBehaviour
{
    ItemConfig currentItemConfig = null;
    [SerializeField] GameObject plate;
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
                    if (shopItem.itemCategory.name != "Plate")
                    {
                        currentItemConfig = shopItem.itemConfig;

                        PutItemInPosition(currentItemConfig);
                    }
                    else
                    {
                        currentItemConfig = shopItem.itemConfig;
                        PutPlateInPosition(currentItemConfig);
                    }

                }
            }
        }
    }

    private void PutPlateInPosition(ItemConfig plateToPlace)
    {
        currentItemConfig = plateToPlace;
        var platePrefab = currentItemConfig.GetItemPrefab();
        itemObject = Instantiate(platePrefab, plate.transform);
        itemObject.transform.localPosition = currentItemConfig.placeTransform.localPosition;
        itemObject.transform.localRotation = currentItemConfig.placeTransform.localRotation;
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
