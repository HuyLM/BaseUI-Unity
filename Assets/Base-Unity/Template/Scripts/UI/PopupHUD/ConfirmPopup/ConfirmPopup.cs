using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{

    public class ConfirmPopup : BasePopup
    {
        [SerializeField] private ButtonBase btnConfirm;
        [SerializeField] private ButtonBase btnCancel;
        [SerializeField] private Text txtConfirm;
        [SerializeField] private Text txtCancel;


        private Action onConfirm;
        private Action onCancel;

        protected override void Start()
        {
            base.Start();
            btnConfirm.onClick.AddListener(OnConfirmButtonClicked);
            btnCancel.onClick.AddListener(OnCancelButtonClicked);
        }


        public ConfirmPopup SetOnConfirm(Action onConfirm)
        {
            this.onConfirm = onConfirm;
            return this;
        }

        public ConfirmPopup SetOnCancel(Action onCancel)
        {
            this.onCancel = onCancel;
            return this;
        }

        public ConfirmPopup SetConfirmText(string text)
        {
            if (txtConfirm)
            {
                txtConfirm.text = text;
            }
            return this;
        }

        public ConfirmPopup SetCancelText(string text)
        {
            if (txtCancel)
            {
                txtCancel.text = text;
            }
            return this;
        }

        private void OnConfirmButtonClicked()
        {
            onConfirm?.Invoke();
            Close();
        }

        private void OnCancelButtonClicked()
        {
            onCancel?.Invoke();
            Close();
        }
    }
}