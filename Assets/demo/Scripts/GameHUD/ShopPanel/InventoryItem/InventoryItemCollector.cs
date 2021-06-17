using AtoLib.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemCollector : Collector<GameItem>
{
    [SerializeField] private InventoryItemDisplayer prefab;
    [SerializeField] private Transform layout;

    private Action<InventoryItemDisplayer> onUseItem;
    private Action<InventoryItemDisplayer> onExpiryDateItem;

    protected readonly List<InventoryItemDisplayer> displayers = new List<InventoryItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public InventoryItemDisplayer GetDisplayer(int index)
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

            InventoryItemDisplayer displayer = GetDisplayer(i);
            if (displayer)
            {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++)
        {
            InventoryItemDisplayer displayer = GetDisplayer(i);
            if (displayer)
            {
                displayer.gameObject.SetActive(false);
            }
        }
    }

    public void SetupDisplayer(InventoryItemDisplayer displayer, GameItem item)
    {
        if (displayer == null)
        {
            return;
        }
        displayer.SetOnExpiryDate(onExpiryDateItem).SetOnUseItem(onUseItem).SetModel(item).Show();
    }

    protected InventoryItemDisplayer CreateDisplayer()
    {
        InventoryItemDisplayer viewItem = Instantiate(prefab, layout.transform);
        return viewItem;
    }

    public InventoryItemCollector SetOnUseItem(Action<InventoryItemDisplayer> onBuy)
    {
        this.onUseItem = onBuy;
        return this;
    }

    public InventoryItemCollector SetOnExpiryDateItem(Action<InventoryItemDisplayer> onExpiryDateItem)
    {
        this.onExpiryDateItem = onExpiryDateItem;
        return this;
    }
}
