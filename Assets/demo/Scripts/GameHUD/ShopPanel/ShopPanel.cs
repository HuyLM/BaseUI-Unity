using AtoLib.Common;
using AtoLib.InventorySystem;
using AtoLib.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : DOTweenFrame
{
    [SerializeField] private ShopItemCollector shopItemCollector;
    [SerializeField] private InventoryItemCollector inventoryItemCollector;
    [SerializeField] private ShopTabControl shopTabControl;
    [SerializeField] private ItemDisplayer keyCurrencyDisplayer;
    [SerializeField] private ButtonBase btnClose;

    private int curTabIndex;

    private void Start()
    {
        btnClose.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener<EventKey.OnInventoryChange>(OnInventoryChanged);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener<EventKey.OnInventoryChange>(OnInventoryChanged);
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false)
    {
        base.OnShow(onCompleted, instant);
        shopTabControl.ForceSelectTab(0);
        curTabIndex = 0;
        ShowKeyCurrency();
    }


    public void ShowShop()
    {
        curTabIndex = 0;
        List<GameItem> shopItems = ShopData.Instance.GetShopItems();
        shopItemCollector.SetOnBuyShopItemClicked(OnBuyShopItemClicked).SetCapacity(shopItems.Count).SetItems(shopItems).Show();
    }

    public void ShowInventory()
    {
        curTabIndex = 1;
        List<GameItem> inventoryItems = ShopData.Instance.GetInventoryItems();
        inventoryItemCollector.SetOnExpiryDateItem(OnExpiryDateItem).SetOnUseItem(OnUseInventoryItemClicked).SetCapacity(inventoryItems.Count).SetItems(inventoryItems).Show();
    }

    private void OnBuyShopItemClicked(ShopItemDisplayer displayer)
    {
        // check enough key
        GameItem item = displayer.Model;
        ItemSlot price = item.Price;
        ItemSlot current = Inventory.Instance.GetItem(price.Id);
        bool canBuy = current.Amount >= price.Amount;

        if (canBuy)
        {
            ConfirmBuyItemPopup confirm = PopupHUD.Instance.Show<ConfirmBuyItemPopup>();
            confirm.SetColorConfirm(canBuy ? Color.green : Color.red);
            confirm.SetTile($"{item.Name}");
            confirm.SetIcon(item.Icon);
            confirm.SetMessage($"Use item in {item.UseTime}");
            confirm.SetPrice(price);
            confirm.SetOnConfirm(() =>
            {
                Inventory.Instance.Remove(price);
                item.Buy();
                if (ShopData.Instance.GetInventoryItems().Count == 1)
                {
                    item.Use();
                }
                ShowShop();
            });
        }
        else
        {
            ConfirmPopup confirm = PopupHUD.Instance.Show<ConfirmPopup>();
            confirm.SetTile("Not enough money");
            confirm.SetMessage("Do you want to buy more?");
            confirm.SetConfirmText("Buy");
            confirm.SetOnConfirm(() =>
            {
                Debug.Log("Buy more 1000 keys");
                Inventory.Instance.Add(ConstantItemID.keyID, 1000);
            });
        }
    }

    private void OnUseInventoryItemClicked(InventoryItemDisplayer displayer)
    {
        GameItem preUsingItem = ShopData.Instance.CurrentUsingItem;
        if (preUsingItem != null)
        {

        }
        ShopData.Instance.CurrentUsingItem = displayer.Model;
        ShowInventory();
    }

    private void OnExpiryDateItem(InventoryItemDisplayer displayer)
    {
        ShowInventory();
    }

    private void ShowKeyCurrency()
    {
        keyCurrencyDisplayer.SetModel(Inventory.Instance.GetItem(ConstantItemID.keyID)).Show();
    }

    private void OnInventoryChanged(EventKey.OnInventoryChange param)
    {
        if (param.ID == ConstantItemID.keyID)
        {
            if (curTabIndex == 0)
            {
                ShowShop();
            }
            else if (curTabIndex == 1)
            {
                ShowInventory();
            }
            ShowKeyCurrency();
        }
    }

    private void OnCloseButtonClicked()
    {
        Back();
        // GameHUD.Instance.Show<HomePanel>();
    }
}

