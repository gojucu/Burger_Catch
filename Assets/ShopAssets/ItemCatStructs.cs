using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Shopping/Category"))]
public class ItemCatStructs : ScriptableObject
{
    public int id;
    public string name;
    public int selectedItemID;// Burda buna gerek kalmayabilir
}
