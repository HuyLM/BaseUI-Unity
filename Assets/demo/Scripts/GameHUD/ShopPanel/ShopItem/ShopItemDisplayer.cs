using System;
using UnityEngine;
using UnityEngine.UI;
using Ftech.Lib.UI;
using Ftech.Lib.InventorySystem;

public class ShopItemDisplayer : Displayer<GameItem>
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private ItemDisplayer priceDisplayer;
    [SerializeField] private ButtonBase btnBuy;

    private Action<ShopItemDisplayer> onBuy;

    private void Start()
    {
        if (btnBuy != null)
        {
            btnBuy.onClick.AddListener(OnBuyButtonClicked);
        }
    }

    public override void Show()
    {
        if (Model == null)
        {
            return;
        }
        imgIcon.sprite = Model.Icon;
        priceDisplayer.SetModel(Model.Price).Show();
    }

    private void OnBuyButtonClicked()
    {
        onBuy?.Invoke(this);
    }

    public ShopItemDisplayer SetOnBuyShopItemClicked(Action<ShopItemDisplayer> onBuy)
    {
        this.onBuy = onBuy;
        return this;
    }
}
