using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class Shop : MonoBehaviour
{
	#region Singlton:Shop

	public static Shop Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion


	[SerializeField] ShopItemDatabase itemDB;

	[SerializeField] Animator NoCoinsAnim;
 

	[SerializeField] GameObject ItemTemplate;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] GameObject ShopPanel;


    public void ListShopItems(int catID)
    {
		//Loop throw save purchased items and make them as purchased in the Database array
		for (int i = 0; i < SaveSystem.GetAllPurchasedCharacter().Count; i++)
		{
            List<int> hey = new List<int>//add yerine bunu önerdi solda yaptım 
            {
                SaveSystem.GetPurchasedCharacter(i)
            };
            for (int x = 0; x < hey.Count; x++)
            {
                int h = hey[x];
                ShopItem shopItem1 = itemDB.items[h];
                if (shopItem1.itemCategory.id == catID) { 
					itemDB.PurchaseItem(shopItem1.itemID, shopItem1.itemCategory.id);
				}
			}
			//int purchasedCharacterIndex = SaveSystem.GetPurchasedCharacter(i);
			//ShopItem shopItem = itemDB.items[i];
			//itemDB.PurchaseItem(shopItem.itemID,shopItem.itemCategory.id);
		}

		ResetShopList();//Bu hala lazımmı ?*** lazım gibi
		int len;
		len = itemDB.items.Where(x => x.itemCategory.id == catID).Count();

		for (int i = 0; i < len; i++)
		{
			ShopItem item = itemDB.GetShopItem(i,catID);
			ItemUI itemUI = Instantiate(ItemTemplate, ShopScrollView).GetComponent<ItemUI>();
			itemUI.SetItemImage(item.image);
			itemUI.SetItemPrice(item.price);
			itemUI.SetItemID(item.itemID);

			if (item.isPurchased)
			{
				itemUI.SetItemAsPurchased();
				itemUI.OnItemSelect(item.itemID, catID, OnItemSelected);
			}else if (item.itemID == 0&&item.isPurchased==false)//Burası test edilecek
            {
				itemUI.SetItemAsPurchased();
				OnItemPurchased(item.itemID, item.itemCategory.id);
			}
			else
			{
				itemUI.OnItemPurchase(item.itemID, catID, OnItemPurchased);
			}

		}
		int selectedItemID = SaveSystem.GetSelectedItemIndex(catID);
		//Set selected items in DataManager
		SetSelectedItems(selectedItemID, catID);
		//Select UI item
		SelectItemUI(selectedItemID, catID);
	}


    void SetSelectedItems(int selectedItemID,int categoryID)
    {
		//Set selected character
		SaveSystem.SetSelectedItem(selectedItemID, categoryID);
    }


    void OnItemSelected(int i,int catID)
    {
		//Select item in the UI
		SelectItemUI(i,catID);

		//Save Data
		SaveSystem.SetSelectedItem(i,catID);
	}

	void SelectItemUI(int selectedID,int catID)//Cat id kullanılmıyor gözüküyor temizle
	{
		var d = FindObjectsOfType<ItemUI>();
		foreach (ItemUI itemUI in d)
        {
			itemUI.DeselectItem();
		}

        ItemUI newUiItem = null;

        foreach (ItemUI itemUI in from ItemUI itemUI in d
                               where itemUI.GetComponent<ItemUI>().GetItemID() == selectedID
								  select itemUI)
        {
            newUiItem = itemUI;
            newUiItem.SelectItem();
        }
	}

	void ResetShopList()
    {
		foreach(Transform child in ShopScrollView)
        {
            Destroy(child.gameObject);
		}
    }

	void OnItemPurchased (int itemID,int catID)
	{
		int price;
        if (itemID == 0)
        {
			price = 0;
        }
        else
        {
			price = itemDB.items.Where(x => x.itemID == itemID && x.itemCategory.id == catID).FirstOrDefault().price;
		}

        if (SaveSystem.CanSpendCoins(price))
        {
			//Proceed with the purchase operation
			SaveSystem.SpendCoins(price);

			//Update DB's Data
			itemDB.PurchaseItem(itemID,catID);

			//change itemUI
			var d = FindObjectsOfType<ItemUI>();
            foreach (ItemUI itemUI in from ItemUI itemUI in d
                                   where itemUI.GetItemID() == itemID
									  select itemUI)
            {
                itemUI.SetItemAsPurchased();
                itemUI.OnItemSelect(itemID, catID, OnItemSelected);
            }

            //Add purchased item to Shop Data
			for(int i =0; i < itemDB.items.Length;i++)// Sorun burada. Bütün eşyaları satın alıyor Bir kere çalışsın!!!****
            {
				//ShopItem item = itemDB.GetShopItem(itemID, catID);
				ShopItem item = itemDB.items[i];
                if (item.itemID == itemID && item.itemCategory.id == catID)
                {
					SaveSystem.AddPurchasedCharacter(i);//Burdan itemDB.items daki itemin indexini yolluyor
				}
			}
			//change UI text: coins
			GameMenu gameMenu = FindObjectOfType<GameMenu>();
			gameMenu.UpdateAllCoinsUIText();
			//Game.Instance.UpdateAllCoinsUIText(); Ana menüdeki parayı güncelliyor
		}
        else
        {
			NoCoinsAnim.SetTrigger("NoCoins");
			Debug.Log("You don't have enough coins!!");
		}
	}

	/*---------------------Open & Close shop--------------------------*/ 
	public void OpenShop ()
	{
		ShopPanel.SetActive (true);
		//Fill the shop's UI list with items

		//Select first tab-> and call listShopItems
		TabGroup tabGroup = FindObjectOfType<TabGroup>();
		tabGroup.SelectDefaultTab();

		//ListShopItems(0);
	}

	public void CloseShop ()
	{
		ShopPanel.SetActive (false);

		ResetShopList();

	}

}
