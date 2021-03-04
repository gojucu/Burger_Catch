using UnityEngine;
using UnityEngine.UI;
//using TMPro;
using UnityEngine.Events;

public class ItemUI : MonoBehaviour
{
	[SerializeField] Color itemNotSelectedColor;
	[SerializeField] Color itemSelectedColor;

	[Space(20f)]
	[SerializeField] int itemID;
	[SerializeField] Image itemImage;

	[SerializeField] Button itemPurchaseButton;
	[SerializeField] GameObject buttonText;

	[SerializeField] GameObject itemPrice;
	[SerializeField] Text ItemPriceText;


	[Space(20f)]
	[SerializeField] Button itemButton;
	//[SerializeField] Image itemImage;
	[SerializeField] Outline itemOutline;

	public void SetItemImage(Sprite sprite)
	{
		itemImage.sprite = sprite;
	}
	public void SetItemID(int id)
	{
		itemID = id;
	}
	public int GetItemID()
	{
		return itemID;
	}
	public void SetItemPrice(int price)
	{
		ItemPriceText.text = price.ToString();
	}

	public void SetItemAsPurchased()
	{
		itemPurchaseButton.interactable = false;

		itemPrice.SetActive(false);
		buttonText.SetActive(true);
		buttonText.GetComponent<Text>().text = "OWNED";

		//itemPurchaseButton.transform.GetChild(0).GetComponent<Text>().text = "OWNED";
		itemButton.interactable = true;

		//itemImage = itemNotSelectedColor;
	}
	public void OnItemPurchase(int itemIndex,int catID, UnityAction<int,int> action)
	{
		itemPurchaseButton.onClick.RemoveAllListeners();
		itemPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex, catID));
	}

	public void OnItemSelect(int itemIndex,int catID, UnityAction<int,int> action)
	{
		itemButton.interactable = true;

		itemButton.onClick.RemoveAllListeners();
		itemButton.onClick.AddListener(() => action.Invoke(itemIndex,catID));
	}

	public void SelectItem()
	{
		itemOutline.enabled = true;
		buttonText.GetComponent<Text>().text = "SELECTED";
		//itemImage.color = itemSelectedColor;
		itemButton.interactable = false;
	}

	public void DeselectItem()
	{
		itemOutline.enabled = false;
		buttonText.GetComponent<Text>().text = "OWNED";
		//itemImage.color = itemNotSelectedColor;
		itemButton.interactable = true;
	}
}
