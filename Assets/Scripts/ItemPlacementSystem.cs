using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacementSystem : MonoBehaviour
{
    //Her kategori için bunu ayrı yazmam gerekebilir** Plantla başla örneğe
    [SerializeField] ItemConfig currentItemConfig = null;//Plant olucak ilk

    GameObject itemObject;

    // Start is called before the first frame update
    void Start()
    {
        PutItemInPosition(currentItemConfig);
    }

    public void PutItemInPosition(ItemConfig itemToPlace)
    {
        currentItemConfig = itemToPlace;
        var itemPrefab = itemToPlace.GetItemPrefab();
        Destroy(itemObject);
        itemObject = Instantiate(itemPrefab, this.transform);
        itemObject.transform.localPosition = currentItemConfig.placeTransform.localPosition;
        itemObject.transform.localRotation = currentItemConfig.placeTransform.localRotation;
    }


}
