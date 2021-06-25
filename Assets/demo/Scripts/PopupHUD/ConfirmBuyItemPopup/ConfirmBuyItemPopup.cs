using Ftech.Lib.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBuyItemPopup : ConfirmPopup
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private Image imgConfirm;
    [SerializeField] private ItemDisplayer priceDisplayer;

    public ConfirmBuyItemPopup SetIcon(Sprite icon)
    {
        imgIcon.sprite = icon;
        return this;
    }

    public ConfirmBuyItemPopup SetColorConfirm(Color color)
    {
        imgConfirm.color = color;
        return this;
    }

    public ConfirmBuyItemPopup SetPrice(ItemSlot price)
    {
        priceDisplayer.SetModel(price).Show();
        return this;
    }
}
