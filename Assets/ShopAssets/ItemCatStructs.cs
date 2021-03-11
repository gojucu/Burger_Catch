using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Shopping/Category"))]
public class ItemCatStructs : ScriptableObject//Bunla Item categories in ya içeriğini yada isimlerini değiştir
{
    public int id;
    public string categoryName;
    public int selectedItemID;// Burda buna gerek kalmayabilir
}
