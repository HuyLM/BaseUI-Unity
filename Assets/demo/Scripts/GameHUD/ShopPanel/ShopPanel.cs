using AtoLib.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : DOTweenFrame
{
    [SerializeField] private ShopItemCollector shopItemCollector;
    [SerializeField] private InventoryItemCollector inventoryItemCollector;
    [SerializeField] private ShopTabControl shopTabControl;

    protected override void OnShow(Action onCompleted = null, bool instant = false)
    {
        base.OnShow(onCompleted, instant);
        shopTabControl.ForceSelectTab(0);
    }


    public void ShowShop()
    {
        List<GameItem> shopItems = ShopData.Instance.GetShopItems();
        shopItemCollector.SetOnBuyShopItemClicked(OnBuyShopItemClicked).SetCapacity(shopItems.Count).SetItems(shopItems).Show();
    }

    public void ShowInventory()
    {
        List<GameItem> inventoryItems = ShopData.Instance.GetInventoryItems();
        inventoryItemCollector.SetOnExpiryDateItem(OnExpiryDateItem).SetOnUseItem(OnUseInventoryItemClicked).SetCapacity(inventoryItems.Count).SetItems(inventoryItems).Show();
    }

    private void OnBuyShopItemClicked(ShopItemDisplayer displayer)
    {
        ShowShop();
    }

    private void OnUseInventoryItemClicked(InventoryItemDisplayer displayer)
    {
        ShowInventory();
    }

    private void OnExpiryDateItem(InventoryItemDisplayer displayer)
    {
        ShowInventory();
    }
}

