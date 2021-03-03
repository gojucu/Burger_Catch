using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;//Bunlar silinicek Yerlerine Color kullanılıcak
    public Sprite tabHover;//
    public Sprite tabActive;//

    [SerializeField] GameObject g=null;

    public Color colorIdle;
    public Color colorHover;
    public Color colorActive;
    public TabButton selectedTab;
    public List<string> shopCategories;


    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab==null || button!=selectedTab)
        button.background.color = colorHover;
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void SelectDefaultTab()
    {
        OnTabSelected(tabButtons[0]);
    }
    public void OnTabSelected(TabButton button)
    {
        Shop shop =g.GetComponent<Shop>();
        selectedTab = button;
        ResetTabs();
        button.background.color = colorActive;

        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < shopCategories.Count; i++)
        {
            if (i == index)
            {
                shop.ListShopItems(index);
            }
            //else
            //{

            //}
        }
    }
    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab!=null&& button == selectedTab) { continue; }
            button.background.color = colorIdle;
        }
    }
}
