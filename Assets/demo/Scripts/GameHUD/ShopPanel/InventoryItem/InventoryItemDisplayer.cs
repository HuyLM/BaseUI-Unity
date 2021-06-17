using AtoLib.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryItemDisplayer : Displayer<GameItem>
{
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private GameObject imgUsed;
    [SerializeField] private GameObject txtUse;
    [SerializeField] private ButtonBase btnUse;
    [SerializeField] private Timer timer;

    private Action<InventoryItemDisplayer> onUseItem;
    private Action<InventoryItemDisplayer> onExpiryDateItem;

    private void Start()
    {
        btnUse.onClick.AddListener(OnUseButtonClicked);
    }

    public override void Show()
    {
        if (Model == null)
        {
            return;
        }
        imgIcon.sprite = Model.Icon;
        bool isUsing = ShopData.Instance.CurrentUsingItemID == Model.Id;
        imgUsed.SetActive(isUsing);
        txtUse.SetActive(!isUsing);
        SetStateUseButton(!isUsing, true);
        timer.Countdown(Model.ExpiryDate - DateTime.Now, elapsed =>
        {
            txtTime.text = elapsed.ToString(@"hh\:mm\:ss");
        }, OnItemExpiryDate, true);
    }

    private void OnItemExpiryDate()
    {
        onExpiryDateItem.Invoke(this);
    }

    private void OnUseButtonClicked()
    {
        onUseItem?.Invoke(this);
    }

    public InventoryItemDisplayer SetOnUseItem(Action<InventoryItemDisplayer> onUseItem)
    {
        this.onUseItem = onUseItem;
        return this;
    }

    public InventoryItemDisplayer SetOnExpiryDate(Action<InventoryItemDisplayer> onExpiryDateItem)
    {
        this.onExpiryDateItem = onExpiryDateItem;
        return this;
    }

    private void SetStateUseButton(bool interaction, bool show)
    {
        if (btnUse)
        {
            btnUse.gameObject.SetActive(show);
            if (show)
            {
                btnUse.SetState(interaction);
            }
        }
    }
}
