using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
//Shop Data Holder
[System.Serializable]
public class CharactersShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
    //public int categoryCount;//bu lazım değil gibi sil sonra
}

//Player Data Holder
[System.Serializable]
public class PlayerData//diğer burgerın playerData ile kesiştiği için 2 koydum sonuna
{
    public int highScore=0;
    public int coins = 3000;//Bu sayıyı 0 yap testler bitince buildden önce

    public List<int> selectedCharacterIndex = new List<int>();//Bunu Kullanmıyosun
    public ItemCategories[] itemCats = new ItemCategories[10];//Bu 10 u elinle manuel ayarlamak zorunda olabilirsin ****

}

public static class SaveSystem
{
    static PlayerData playerData = new PlayerData();
    static CharactersShopData charactersShopData = new CharactersShopData();


    static SaveSystem()
    {
        
        LoadPlayerData();
        LoadCharactersShopData();
    }

    //Player Data Methods -----------------------------------------------------------------------------
    public static int GetHighScore()
    {
        return playerData.highScore;
    }
    public static void SetHighScore(int highScore)
    {
        playerData.highScore = highScore;
        SavePlayerData();
    }

    public static int GetSelectedItemIndex(int catID)
    {
        return playerData.itemCats[catID].selectedItemID;

    }

    public static void SetSelectedItem(int itemID, int catID)
    {
        playerData.itemCats[catID].selectedItemID = itemID;
        SavePlayerData();
    }
    public static int GetSelectedItemsCount()//Bune 
    {
        return playerData.selectedCharacterIndex.Count;
    }
    public static int GetCoins()
    {
        return playerData.coins;
    }
    public static void AddCoins(int amount)
    {
        playerData.coins += amount;
        SavePlayerData();
    }
    public static bool CanSpendCoins(int amount)
    {
        return (playerData.coins >= amount);
    }
    public static void SpendCoins(int amount)
    {
        playerData.coins -= amount;
        SavePlayerData();
    }
    static void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/players.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
        }
    }
    static void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/players.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerData);
        stream.Close();

    }

    //Characters Shop Data Methods -----------------------------------------------------------------------------
    public static void AddPurchasedCharacter(int characterIndex)
    {
        charactersShopData.purchasedCharactersIndexes.Add(characterIndex);
        SaveCharactersShoprData();
    }

    public static List<int> GetAllPurchasedCharacter()
    {
        return charactersShopData.purchasedCharactersIndexes;
    }


    public static int GetPurchasedCharacter(int index)
    {
        return charactersShopData.purchasedCharactersIndexes[index];
    }

    static void LoadCharactersShopData()
    {
        string path = Application.persistentDataPath + "/shops.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            charactersShopData = formatter.Deserialize(stream) as CharactersShopData;
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
        }
    }

    static void SaveCharactersShoprData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shops.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, charactersShopData);
        stream.Close();
    }
}
