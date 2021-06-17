using AtoLib.UI;
using System;
using UnityEngine;

namespace AtoLib.UI
{
    public class TabButton : MonoBehaviour
    {
        [SerializeField] private ButtonBase btnTab;
        [SerializeField] private int tabIndex;
        private Action<int> onTabClicked;

        public int TabIndex { get => tabIndex; }

        private void Start()
        {
            btnTab.onClick.AddListener(OnTabButtonClicked);
        }

        public virtual void SetActiveTab(bool isActive)
        {
            btnTab.SetState(isActive);
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
