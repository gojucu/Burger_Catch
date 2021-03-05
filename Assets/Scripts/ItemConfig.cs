using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item"))]
public class ItemConfig : ScriptableObject
{
    public Transform placeTransform;
    [SerializeField] GameObject itemPrefab;

    public GameObject GetItemPrefab()
    {
        return itemPrefab;
    }
}
