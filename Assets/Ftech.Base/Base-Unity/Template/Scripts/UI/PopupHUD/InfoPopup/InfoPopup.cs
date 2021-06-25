using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class InfoPopup : BasePopup
    {
        [SerializeField] private ButtonBase btnConfirm;
        [SerializeField] private Text txtConfirm;


        private Action onConfirm;

        protected override void Start()
        {
            base.Start();
            btnConfirm.onClick.AddListener(OnConfirmButtonClicked);
        }


        public InfoPopup SetOnConfirm(Action onConfirm)
        {
            this.onConfirm = onConfirm;
            return this;
        }

        public InfoPopup SetConfirmText(string text)
        {
            if (txtConfirm)
            {
                txtConfirm.text = text;
            }
            return this;
        }

        private void OnConfirmButtonClicked()
        {
            onConfirm?.Invoke();
            Close();
        }

    }
}
