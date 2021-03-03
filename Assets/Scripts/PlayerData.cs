using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {//galiba bu save için oluşturulucak dosya. tekrar bak.
    public int highScore;
    public int coins;

    public PlayerData (ScoreBoard score)
    {
        highScore = score.highScore;
    }
}
//Shop Data Holder
[System.Serializable]
public class ShopData
{
    public List<int> purchasedItemsIndexes = new List<int>();
}
