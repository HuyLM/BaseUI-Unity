using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ftech.Lib.UI;

namespace Ftech.Lib.InventorySystem
{
    public class ItemDisplayer : Displayer<IItemInstance>
    {
        [SerializeField] private Image imgIcon;
        [SerializeField] private TextMeshProUGUI txtAmount;
        public override void Show()
        {
            if (Model == null)
            {

                return;
            }
            imgIcon.sprite = Model.Icon;
            txtAmount.text = Model.Amount.ToString();
        }
    }
}
