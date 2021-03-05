using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDatax {//galiba bu save için oluşturulucak dosya. tekrar bak.
    public int highScore;
    public int coins;

    public PlayerDatax (ScoreBoard score)
    {
        highScore = score.highScore;
    }
}
//Shop Data Holder
[System.Serializable]
public class ShopDatax
{
    public List<int> purchasedItemsIndexes = new List<int>();
}
