using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Shopping/Item Config"))]
public class ItemConfig : ScriptableObject
{
    public Transform placeTransform;
    [SerializeField] GameObject itemPrefab = null;

    public GameObject GetItemPrefab()
    {
        return itemPrefab;
    }
}
