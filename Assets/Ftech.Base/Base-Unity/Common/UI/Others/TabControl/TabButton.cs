using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.UI
{
    public class TabButton : MonoBehaviour
    {
        [SerializeField] private Button btnTab;
        [SerializeField] private int tabIndex;
        private Action<int> onTabClicked;

        public int TabIndex { get => tabIndex; }

        private void Start()
        {
            btnTab.onClick.AddListener(OnTabButtonClicked);
        }

        public virtual void SetActiveTab(bool isActive)
        {
            btnTab.interactable = (isActive);
        }

        private void OnTabButtonClicked()
        {
            onTabClicked?.Invoke(tabIndex);
        }

        public void SetOnTabClicked(Action<int> onTabClicked)
        {
            this.onTabClicked = onTabClicked;
        }
    }
}
