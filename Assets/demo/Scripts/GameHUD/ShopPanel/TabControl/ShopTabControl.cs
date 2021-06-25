using Ftech.Lib.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabControl : TabControl
{
    [SerializeField] private GameObject shopTabView;
    [SerializeField] private GameObject inventoryView;
    [SerializeField] private ScrollRect shopScrollRect;
    [SerializeField] private ScrollRect inventoryScrollRect;
    protected override void SelectTab(int index)
    {
        base.SelectTab(index);
        HideAllTab();
        switch (index)
        {
            case 0:
            {
                OpenShopTab();
                break;
            }
            case 1:
            {
                OpenInventoryTab();
                break;
            }
        }
    }

    private void OpenShopTab()
    {
        shopTabView.SetActive(true);
        ShopPanel shopPanel = GameHUD.Instance.GetFrame<ShopPanel>();
        shopPanel.ShowShop();
        shopScrollRect.verticalNormalizedPosition = 1;
    }

    private void OpenInventoryTab()
    {
        inventoryView.SetActive(true);
        ShopPanel shopPanel = GameHUD.Instance.GetFrame<ShopPanel>();
        shopPanel.ShowInventory();
        inventoryScrollRect.verticalNormalizedPosition = 1;
    }

    private void HideAllTab()
    {
        shopTabView.SetActive(false);
        inventoryView.SetActive(false);
    }
}
