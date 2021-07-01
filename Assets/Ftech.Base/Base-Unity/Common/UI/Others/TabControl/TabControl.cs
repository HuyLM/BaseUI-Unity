using UnityEngine;
using UnityEngine.Events;

namespace Ftech.Lib.UI
{
    public class TabControl : MonoBehaviour
    {
        [SerializeField] private TabButton[] tabButtons;
        [SerializeField] private UnityEvent<int> onTabChanged;
        protected int curTabIndex;

        public int CurTabIndex { get => curTabIndex; }

        private void Start()
        {
            for (int i = 0; i < tabButtons.Length; ++i)
            {
                tabButtons[i].SetOnTabClicked(OnTabButtonClicked);
            }
        }

        public virtual void Init()
        {

        }

        protected void OnTabButtonClicked(int index)
        {
            SelectTab(index);
        }

        protected void SelectTab(int index)
        {
            ChangeTab(index);
            onTabChanged?.Invoke(index);
        }

        public void ForceSelectTab(int index)
        {
            ChangeTab(index);
        }

        protected virtual void ChangeTab(int index)
        {
            for (int i = 0; i < tabButtons.Length; ++i)
            {
                if (index == tabButtons[i].TabIndex) // new tab button
                {
                    tabButtons[i].SetActiveTab(false);
                }
                else if (curTabIndex == tabButtons[i].TabIndex) // old tab button
                {
                    tabButtons[i].SetActiveTab(true);
                }
            }
            curTabIndex = index;
        }

    }
}
