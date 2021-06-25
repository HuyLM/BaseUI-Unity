using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class BasePopup : DOTweenFrame
    {

        [Header("[References]")]
        [SerializeField] protected TextMeshProUGUI tileText;
        [SerializeField] protected TextMeshProUGUI messageText;
        [SerializeField] protected Button closeTap;
        [SerializeField] protected Button closeButton;
        [SerializeField] protected TextMeshProUGUI closeText;

        protected Action closeAction;
        protected Action preCloseAction;


        protected virtual void Start()
        {
            if (closeButton)
            {
                closeButton.onClick.AddListener(OnCloseButtonClicked);
            }

            if (closeTap)
            {
                closeTap.onClick.AddListener(OnCloseButtonClicked);
            }
        }

        protected override void OnShow(Action onCompleted = null, bool instant = false)
        {
            base.OnShow(onCompleted, instant);
            SetTapState(true);
            SetTile(null, false);
            SetMessage(null, false);
            SetCloseContent(null, false);
            OnClose(null);
            SetCloseState(true, true);
        }

        public BasePopup SetTile(string tile, bool show = true)
        {
            if (this.tileText)
            {
                this.tileText.gameObject.SetActive(show);
                if (show)
                {
                    this.tileText.text = tile;
                }
            }
            return this;
        }

        public BasePopup SetMessage(string message, bool show = true)
        {
            if (this.messageText)
            {
                this.messageText.gameObject.SetActive(show);
                if (show)
                {
                    this.messageText.text = message;
                }
            }
            return this;
        }

        public BasePopup SetCloseContent(string content, bool show = true)
        {
            if (this.closeText)
            {
                this.closeText?.gameObject.SetActive(show);
                if (show)
                {
                    this.closeText.text = content;
                }
            }
            return this;
        }

        public BasePopup OnClose(Action closeAction)
        {
            this.closeAction = closeAction;
            return this;
        }

        public BasePopup OnPreClose(Action preCloseAction)
        {
            this.preCloseAction = preCloseAction;
            return this;
        }

        public BasePopup SetCloseState(bool interactable, bool show = true)
        {
            if (closeButton)
            {
                closeButton.gameObject.SetActive(show);
                if (show)
                {
                    closeButton.interactable = (interactable);
                }
            }
            return this;
        }

        protected virtual void OnCloseButtonClicked()
        {
            Close();
        }

        public override Frame Back()
        {
            Close();
            return this;
        }


        public BasePopup SetTapState(bool interactable, bool show = true)
        {
            if (closeTap)
            {
                closeTap.gameObject.SetActive(show);
                if (show)
                {
                    closeTap.interactable = (interactable);
                }
            }
            return this;
        }

        protected void Close()
        {
            SetTapState(false);
            preCloseAction?.Invoke();
            Hide();
            closeAction?.Invoke();
        }
    }
}