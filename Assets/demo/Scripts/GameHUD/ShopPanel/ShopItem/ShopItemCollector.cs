using AtoLib.InventorySystem;
using AtoLib.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCollector : Collector<GameItem>
{
    [SerializeField] private ShopItemDisplayer prefab;
    [SerializeField] private Transform layout;

    private Action<ShopItemDisplayer> onBuyShopItemClicked;

    protected readonly List<ShopItemDisplayer> displayers = new List<ShopItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public ShopItemDisplayer GetDisplayer(int index)
    {
        if (index < 0 || index >= DisplayerCount)
        {
            return null;
        }
        return displayers[index];
    }

    public override void Show()
    {
        for (int i = 0; i < Capacity; i++)
        {
            if (DisplayerCount == i)
            {
                displayers.Add(CreateDisplayer());
            }

            ShopItemDisplayer displayer = GetDisplayer(i);
            if (displayer)
            {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++)
        {
            ShopItemDisplayer displayer = GetDisplayer(i);
            if (displayer)
            {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public void SetupDisplayer(ShopItemDisplayer displayer, GameItem item)
    {
        if (displayer == null)
        {
            return;
        }
        displayer.SetOnBuyShopItemClicked(onBuyShopItemClicked).SetModel(item).Show();
    }

    protected ShopItemDisplayer CreateDisplayer()
    {
        ShopItemDisplayer viewItem = Instantiate(prefab, layout.transform);
        return viewItem;
    }

    public ShopItemCollector SetOnBuyShopItemClicked(Action<ShopItemDisplayer> onBuy)
    {
        this.onBuyShopItemClicked = onBuy;
        return this;
    }
}