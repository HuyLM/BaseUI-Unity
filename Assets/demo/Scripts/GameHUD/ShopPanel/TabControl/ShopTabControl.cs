using AtoLib.UI;
using UnityEngine;

public class ShopTabControl : TabControl
{
    [SerializeField] private GameObject shopTabView;
    [SerializeField] private GameObject inventoryView;
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
    }

    private void OpenInventoryTab()
    {
        inventoryView.SetActive(true);
        ShopPanel shopPanel = GameHUD.Instance.GetFrame<ShopPanel>();
        shopPanel.ShowInventory();
    }

    private void HideAllTab()
    {
        shopTabView.SetActive(false);
        inventoryView.SetActive(false);
    }
}
